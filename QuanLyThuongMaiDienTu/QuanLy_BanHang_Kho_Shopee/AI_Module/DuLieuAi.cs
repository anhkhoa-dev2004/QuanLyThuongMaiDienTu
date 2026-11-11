using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public static class DuLieuAi
    {
        public class GoiYNhap
        {
            public int GoiY;
            public double SoNhapMoiLan;
            public string GiaiThich;
        }

        public static double[] XuatTheoNgay(string maSP, DateTime tu, DateTime den)
        {
            int n = (int)(den.Date - tu.Date).TotalDays + 1;
            if (n < 1) return new double[0];
            double[] r = new double[n];
            DataTable dt = AiHelper.Doc(
                "SELECT CAST(px.NgayXuat AS DATE) AS Ngay, SUM(ct.SoLuong) AS SL FROM ChiTietPhieuXuat ct JOIN PhieuXuat px ON px.MaPX = ct.MaPX " +
                "WHERE ct.MaSP = @sp AND px.NgayXuat >= @tu AND px.NgayXuat < @den GROUP BY CAST(px.NgayXuat AS DATE)",
                AiHelper.P("@sp", maSP), AiHelper.P("@tu", tu.Date), AiHelper.P("@den", den.Date.AddDays(1)));
            foreach (DataRow row in dt.Rows)
            {
                int i = (int)(Convert.ToDateTime(row["Ngay"]).Date - tu.Date).TotalDays;
                if (i >= 0 && i < n) r[i] += Convert.ToDouble(row["SL"]);
            }
            return r;
        }

        public static GoiYNhap TinhGoiYNhapTheoSP(string maSP)
        {
            DataTable t = AiHelper.Doc(
                "SELECT SoNenNhap, TrangThai, Cau, DuDoan30Ngay, ViecNenLam FROM vw_DuBaoSanPham WHERE MaSP = @sp",
                AiHelper.P("@sp", maSP));
            if (t.Rows.Count == 0) return new GoiYNhap { GiaiThich = "Không tìm thấy sản phẩm." };
            DataRow r = t.Rows[0];
            return new GoiYNhap
            {
                GoiY = Convert.ToInt32(r["SoNenNhap"]),
                GiaiThich = "Tình trạng: " + r["TrangThai"] + "  |  Xu hướng bán: " + r["Cau"] +
                            "  |  Dự đoán bán 30 ngày tới: " + r["DuDoan30Ngay"] + " cái\nNên làm gì: " + r["ViecNenLam"]
            };
        }
    }

    public static class UyTinVanChuyen
    {
        public const string SQL_THONG_KE =
            "SELECT vd.DonViVC AS [Đơn Vị Vận Chuyển], COUNT(*) AS [Số Vận Đơn], " +
            "       SUM(CASE WHEN vd.TrangThai = N'Đã giao' THEN 1 ELSE 0 END) AS [Đã Giao], " +
            "       SUM(CASE WHEN vd.TrangThai = N'Giao không thành công' THEN 1 ELSE 0 END) AS [Hoàn/Thất Bại], " +
            "       MAX(vd.DiemUyTin_NVDV) AS [Điểm Đã Lưu] " +
            "FROM VanDon vd WHERE (@dv IS NULL OR vd.DonViVC = @dv) GROUP BY vd.DonViVC ORDER BY COUNT(*) DESC";

        public static DataTable ThongKe(string donVi)
        {
            return AiHelper.Doc(SQL_THONG_KE, AiHelper.P("@dv", string.IsNullOrWhiteSpace(donVi) ? null : donVi.Trim()));
        }

        public static decimal? Diem(int daGiao, int thatBai)
        {
            if (daGiao + thatBai == 0) return null;
            return Math.Round(100m * daGiao / (daGiao + thatBai), 2);
        }

        public static string MoTa(DataRow r)
        {
            int dg = Convert.ToInt32(r["Đã Giao"]), tb = Convert.ToInt32(r["Hoàn/Thất Bại"]);
            decimal? d = Diem(dg, tb);
            return r["Số Vận Đơn"] + " vận đơn, " + dg + " đã giao, " + tb + " giao không thành công -> " +
                   (d.HasValue ? "điểm uy tín " + d.Value.ToString("0.##") + "/100" : "chưa có vận đơn nào có kết quả nên chưa tính được điểm");
        }
    }

    public static class MoHinhHuyDon
    {
        public const int DON_TOI_THIEU = 20;
        public const int MOI_LOP_TOI_THIEU = 5;
        public static readonly string[] TEN_COT = { "SĐT lỗi hoặc số mẫu (0/1)", "Địa chỉ lỗi hoặc địa chỉ mẫu (0/1)", "Tổng tiền", "Đặt cuối tuần (0/1)", "Giờ đặt", "Trả khi nhận hàng - COD (0/1)" };

        public class KetQua
        {
            public bool DaHoc;
            public string ThongBao;
            public LuatPhanLoai Cay;
            public double[] Min, Max;
            public int SoDon, SoHuy, SoSinhThem;
        }

        private const string SQL_DAC_TRUNG =
            "SELECT CASE WHEN dbo.fn_ChuanHoaSoDienThoai(gn.SoDT) IS NULL THEN 1 ELSE 0 END AS SdtLoi, " +
            "       CASE WHEN dh.HinhThucNhan = N'Nhận tại cửa hàng' THEN 0 " +
            "            WHEN gn.DiaChi IS NULL OR LEN(gn.DiaChi) < 10 OR gn.DiaChi = N'TP.HCM' THEN 1 ELSE 0 END AS DiaChiLoi, " +
            "       CAST(ISNULL(dh.TongTien, 0) AS FLOAT) AS TongTien, dh.Feature_IsWeekend AS CuoiTuan, dh.Feature_Hour AS Gio, " +
            "       CASE WHEN ISNULL(dh.PhuongThucThanhToan, N'') LIKE N'%COD%' THEN 1 ELSE 0 END AS Cod, " +
            "       CASE WHEN dh.TrangThai = N'Đã hủy' THEN 1 ELSE 0 END AS Huy " +
            "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH " +
            "CROSS APPLY (SELECT ISNULL(NULLIF(dh.SdtNguoiNhan, ''), kh.SoDT) AS SoDT, " +
            "                    LTRIM(RTRIM(ISNULL(NULLIF(dh.DiaChiGiao, N''), kh.DiaChi))) AS DiaChi) gn " +
            "WHERE dh.TrangThai IN (N'Đã giao', N'Trả hàng/Hoàn tiền', N'Đã hủy')";

        public static KetQua Hoc()
        {
            KetQua kq = new KetQua();
            DataTable dt = AiHelper.Doc(SQL_DAC_TRUNG);
            int n = dt.Rows.Count, huy = 0;
            foreach (DataRow r in dt.Rows) if (Convert.ToInt32(r["Huy"]) == 1) huy++;
            kq.SoDon = n; kq.SoHuy = huy;
            if (n < DON_TOI_THIEU || huy < MOI_LOP_TOI_THIEU || n - huy < MOI_LOP_TOI_THIEU)
            {
                kq.ThongBao = "Chưa đủ dữ liệu để học: có " + n + " đơn, trong đó " + huy + " đơn hủy " +
                              "(cần ít nhất " + DON_TOI_THIEU + " đơn, mỗi loại hủy / không hủy ít nhất " + MOI_LOP_TOI_THIEU + " đơn). Đang dùng điểm theo quy tắc của thủ tục.";
                return kq;
            }

            int p = TEN_COT.Length;
            double[,] x = new double[n, p];
            int[] y = new int[n];
            for (int i = 0; i < n; i++)
            {
                DataRow r = dt.Rows[i];
                x[i, 0] = Convert.ToDouble(r["SdtLoi"]); x[i, 1] = Convert.ToDouble(r["DiaChiLoi"]); x[i, 2] = Convert.ToDouble(r["TongTien"]);
                x[i, 3] = Convert.ToDouble(r["CuoiTuan"]); x[i, 4] = Convert.ToDouble(r["Gio"]); x[i, 5] = Convert.ToDouble(r["Cod"]);
                y[i] = Convert.ToInt32(r["Huy"]);
            }
            kq.Min = new double[p]; kq.Max = new double[p];
            for (int j = 0; j < p; j++)
            {
                kq.Min[j] = double.MaxValue; kq.Max[j] = double.MinValue;
                for (int i = 0; i < n; i++) { kq.Min[j] = Math.Min(kq.Min[j], x[i, j]); kq.Max[j] = Math.Max(kq.Max[j], x[i, j]); }
            }

            List<double[]> hang = new List<double[]>();
            List<int> nhan = new List<int>();
            List<double[]> thieuSo = new List<double[]>();
            int lopHiem = huy <= n - huy ? 1 : 0;
            for (int i = 0; i < n; i++)
            {
                double[] d = new double[p];
                for (int j = 0; j < p; j++) d[j] = (x[i, j] - kq.Min[j]) / Math.Max(kq.Max[j] - kq.Min[j], 1e-12);
                hang.Add(d); nhan.Add(y[i]);
                if (y[i] == lopHiem) thieuSo.Add(d);
            }

            int soHiem = Math.Min(huy, n - huy), soNhieu = Math.Max(huy, n - huy);
            if (soHiem < 0.4 * n)
            {
                List<double[]> them = SinhThemMau.SinhThem(thieuSo, soNhieu - soHiem, 3, 7);
                foreach (double[] d in them) { hang.Add(d); nhan.Add(lopHiem); }
                kq.SoSinhThem = them.Count;
            }

            double[,] xh = new double[hang.Count, p];
            int[] yh = new int[hang.Count];
            for (int i = 0; i < hang.Count; i++) { for (int j = 0; j < p; j++) xh[i, j] = hang[i][j]; yh[i] = nhan[i]; }
            kq.Cay = LuatPhanLoai.Hoc(xh, yh, 3, 3);
            kq.DaHoc = true;
            kq.ThongBao = "Đã học từ " + n + " đơn đã xong (" + huy + " hủy)" + (kq.SoSinhThem > 0 ? ", tạo thêm " + kq.SoSinhThem + " đơn hủy giả lập từ các đơn hủy thật để số đơn hủy và không hủy ngang nhau" : "") +
                          ". Chưa kiểm tra độ chính xác trên dữ liệu riêng nên chỉ dùng để tham khảo.";
            return kq;
        }

        public static double? DuDoan(KetQua m, double[] dong)
        {
            if (m == null || !m.DaHoc) return null;
            double[] d = new double[dong.Length];
            for (int j = 0; j < d.Length; j++) d[j] = (dong[j] - m.Min[j]) / Math.Max(m.Max[j] - m.Min[j], 1e-12);
            return m.Cay.XacSuat1(d);
        }
    }

    public static class ThanhPhu
    {
        public static readonly Font Chu = new Font("Segoe UI", 9F);
        public static readonly Font ChuDam = new Font("Segoe UI", 9F, FontStyle.Bold);

        private static int X(Control cha, int v) { return v; }
        private static int Y(Control cha, int v) { return v; }

        public static Panel ThemDay(Form f, int cao)
        {
            int caoThat = Y(f, cao);
            f.ClientSize = new Size(f.ClientSize.Width, f.ClientSize.Height + caoThat);
            Panel p = new Panel { Dock = DockStyle.Bottom, Height = caoThat, BackColor = Color.White };
            p.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(214, 218, 227)))
                    e.Graphics.DrawLine(pen, 0, 0, p.Width, 0);
            };
            f.Controls.Add(p);
            return p;
        }

        public static Label Nhan(Control cha, int x, int y, int rong, int cao, string text, bool dam)
        {
            Label l = new Label { Left = X(cha, x), Top = Y(cha, y), Width = X(cha, rong), Height = Y(cha, cao), Text = text, Font = dam ? ChuDam : Chu, AutoSize = false };
            cha.Controls.Add(l);
            return l;
        }

        public static Button Nut(Control cha, string text, int x, int y, int rong, EventHandler click)
        {
            Button b = new Button { Left = X(cha, x), Top = Y(cha, y), Width = X(cha, rong), Height = Y(cha, 30), Text = text, Font = Chu, UseVisualStyleBackColor = true };
            b.Click += click;
            cha.Controls.Add(b);
            return b;
        }

        public static ComboBox Combo(Control cha, int x, int y, int rong, params string[] muc)
        {
            ComboBox c = new ComboBox { Left = X(cha, x), Top = Y(cha, y), Width = X(cha, rong), Font = Chu, DropDownStyle = ComboBoxStyle.DropDownList };
            c.Items.AddRange(muc);
            if (muc.Length > 0) c.SelectedIndex = 0;
            cha.Controls.Add(c);
            return c;
        }

        public static TextBox O(Control cha, int x, int y, int rong)
        {
            TextBox t = new TextBox { Left = X(cha, x), Top = Y(cha, y), Width = X(cha, rong), Font = Chu };
            cha.Controls.Add(t);
            return t;
        }

        public static NumericUpDown So(Control cha, int x, int y, int rong, int nho, int lon, int giaTri)
        {
            NumericUpDown n = new NumericUpDown { Left = X(cha, x), Top = Y(cha, y), Width = X(cha, rong), Minimum = nho, Maximum = lon, Value = giaTri, Font = Chu };
            cha.Controls.Add(n);
            return n;
        }

        public static DataGridView Luoi(Control cha, int x, int y, int rong, int cao)
        {
            DataGridView g = new DataGridView { Left = X(cha, x), Top = Y(cha, y), Width = X(cha, rong), Height = Y(cha, cao) };
            AiHelper.DinhDangLuoi(g);
            cha.Controls.Add(g);
            return g;
        }
    }
}
