using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_PhanTichBanHang : Form
    {
        private class KhungVe : Panel
        {
            public KhungVe()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
                BorderStyle = BorderStyle.FixedSingle;
                BackColor = Color.White;
            }
        }

        private static readonly Color MauCam = Color.FromArgb(238, 77, 45);
        private static readonly Color MauXanhDuong = Color.SteelBlue;
        private static readonly Color MauXam = Color.FromArgb(160, 160, 160);
        private static readonly Color MauVui = Color.FromArgb(46, 160, 90);
        private static readonly Color MauBuon = Color.FromArgb(205, 60, 50);

        private readonly ComboBox cboDanhMuc;
        private readonly Label lblMoc;
        private readonly TextBox txtKetLuan;
        private readonly KhungVe pnlDoanhThu, pnlLyDo, pnlCamXuc, pnlGio;

        private readonly List<string> _nhanDT = new List<string>(), _nhanLD = new List<string>(), _nhanCX = new List<string>(), _nhanGio = new List<string>();
        private readonly List<double> _soDT = new List<double>(), _soLD = new List<double>(), _soCX = new List<double>(), _soGio = new List<double>();
        private readonly List<Color> _mauDT = new List<Color>(), _mauLD = new List<Color>(), _mauCX = new List<Color>(), _mauGio = new List<Color>();

        public Form_PhanTichBanHang()
        {
            Text = "Phân tích bán hàng và cảm xúc khách";
            Name = "Form_PhanTichBanHang";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1100, 985);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "PHÂN TÍCH BÁN HÀNG VÀ CẢM XÚC KHÁCH", AutoSize = true, Location = new Point(20, 10) };
            Label lblGhiChu = new Label
            {
                Text = "Mọi con số lấy thẳng từ dữ liệu bán hàng, đơn đã hủy không tính. Chọn danh mục là 4 biểu đồ và phần \"Nên làm gì\" đổi ngay. " +
                       "Thêm đơn, đánh giá hay câu trả lời mới xong thì bấm \"Tải lại\" (hoặc F5) là thấy số mới.",
                Location = new Point(22, 70),
                Size = new Size(1066, 40),
                ForeColor = Color.DimGray
            };
            Label lblDanhMuc = new Label { Text = "Xem danh mục:", AutoSize = true, Location = new Point(22, 120) };
            cboDanhMuc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(125, 116), Size = new Size(260, 26) };
            Button btnTaiLai = new Button { Text = "Tải lại", Location = new Point(395, 114), Size = new Size(100, 30) };
            lblMoc = new Label { AutoSize = true, Location = new Point(510, 120), ForeColor = Color.DimGray };

            pnlDoanhThu = new KhungVe { Location = new Point(22, 316), Size = new Size(529, 300) };
            pnlLyDo = new KhungVe { Location = new Point(559, 316), Size = new Size(529, 300) };
            pnlCamXuc = new KhungVe { Location = new Point(22, 624), Size = new Size(529, 300) };
            pnlGio = new KhungVe { Location = new Point(559, 624), Size = new Size(529, 300) };

            Label lblKetLuan = new Label { Text = "Nên làm gì (số liệu tự tính từ các con số ở trên; phần \"Gợi ý\" là lời khuyên viết sẵn):", AutoSize = true, Location = new Point(22, 152), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            txtKetLuan = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(22, 176),
                Size = new Size(1066, 130),
                Font = new Font("Segoe UI", 10F)
            };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(988, 946), Size = new Size(100, 30) };

            Controls.AddRange(new Control[] { lblTieuDe, lblGhiChu, lblDanhMuc, cboDanhMuc, btnTaiLai, lblMoc,
                                              pnlDoanhThu, pnlLyDo, pnlCamXuc, pnlGio, lblKetLuan, txtKetLuan, btnThoat });

            pnlDoanhThu.Paint += (s, e) => AiHelper.VeCot(e.Graphics, pnlDoanhThu.ClientRectangle, _nhanDT, _soDT, "Doanh thu từng tháng (triệu đồng)", _mauDT);
            pnlLyDo.Paint += (s, e) => AiHelper.VeCot(e.Graphics, pnlLyDo.ClientRectangle, _nhanLD, _soLD, "Khách mua vì lý do gì (số câu trả lời)", _mauLD);
            pnlCamXuc.Paint += (s, e) => AiHelper.VeCot(e.Graphics, pnlCamXuc.ClientRectangle, _nhanCX, _soCX, "Lời đánh giá: khen, bình thường hay chê (số lời)", _mauCX);
            pnlGio.Paint += (s, e) => AiHelper.VeCot(e.Graphics, pnlGio.ClientRectangle, _nhanGio, _soGio, "Khách chốt đơn theo khung giờ (số đơn; cột 20h là 20:00-20:59)", _mauGio);

            btnTaiLai.Click += (s, e) => TaiLai();
            cboDanhMuc.SelectionChangeCommitted += (s, e) => TaiLai();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) => Mo();

            GiaoDien.GanChoForm(this);
        }

        private void Mo()
        {
            try
            {
                DataTable dm = AiHelper.Doc("SELECT '' AS MaDanhMuc, N'Tất cả danh mục' AS TenDanhMuc, 0 AS ThuTu " +
                                            "UNION ALL SELECT MaDanhMuc, TenDanhMuc, 1 FROM DanhMuc ORDER BY ThuTu, TenDanhMuc");
                AiHelper.NapCombo(cboDanhMuc, dm, "MaDanhMuc", "TenDanhMuc");
                cboDanhMuc.SelectedIndex = 0;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
            TaiLai();
        }

        private void LoadDanhSach()
        {
            TaiLai();
        }

        private static double PhanTram(double a, double b)
        {
            return b == 0 ? 0 : 100.0 * a / b;
        }

        private void TaiLai()
        {
            string dm = Convert.ToString(cboDanhMuc.SelectedValue ?? "");
            List<string> ketLuan = new List<string>();
            try
            {
                object moi = AiHelper.LayGiaTri("SELECT MAX(NgayDat) FROM DonHang");
                if (moi == null) { txtKetLuan.Text = "Chưa có đơn hàng nào."; return; }
                DateTime den = Convert.ToDateTime(moi);
                lblMoc.Text = "Số liệu tính đến " + den.ToString("dd/MM/yyyy") + " (ngày có đơn mới nhất)";

                NapDoanhThu(dm, den, ketLuan);
                NapLyDo(dm, ketLuan);
                NapCamXuc(dm, ketLuan);
                NapGio(dm, ketLuan);
                NapMonHot(dm, ketLuan);
            }
            catch (Exception ex) { ketLuan.Add("Chưa đọc được dữ liệu: " + AiHelper.LoiDeHieu(ex)); }

            txtKetLuan.Text = string.Join(Environment.NewLine, ketLuan.Select((d, i) => (i + 1) + ". " + d));
            pnlDoanhThu.Invalidate(); pnlLyDo.Invalidate(); pnlCamXuc.Invalidate(); pnlGio.Invalidate();
        }

        private void NapDoanhThu(string dm, DateTime den, List<string> ketLuan)
        {
            DataTable t = AiHelper.Doc(
                "SELECT CONVERT(CHAR(7), dh.NgayDat, 120) AS Thang, SUM(ct.SoLuong * ct.DonGia) AS DoanhThu, COUNT(DISTINCT dh.MaDH) AS SoDon " +
                "FROM DonHang dh JOIN ChiTietDonHang ct ON ct.MaDH = dh.MaDH JOIN SanPham sp ON sp.MaSP = ct.MaSP " +
                "WHERE ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' AND (@dm = '' OR sp.MaDanhMuc = @dm) " +
                "  AND dh.NgayDat >= @tu AND dh.NgayDat < DATEADD(DAY, 1, @den) " +
                "GROUP BY CONVERT(CHAR(7), dh.NgayDat, 120) ORDER BY Thang",
                AiHelper.P("@dm", dm), AiHelper.P("@tu", new DateTime(den.Year, den.Month, 1).AddMonths(-11)), AiHelper.P("@den", den.Date));
            _nhanDT.Clear(); _soDT.Clear(); _mauDT.Clear();
            foreach (DataRow r in t.Rows)
            {
                string th = Convert.ToString(r["Thang"]);
                _nhanDT.Add("T" + th.Substring(5, 2) + "/" + th.Substring(2, 2));
                _soDT.Add(Math.Round(Convert.ToDouble(r["DoanhThu"]) / 1000000.0, 1));
                _mauDT.Add(MauXanhDuong);
            }
            if (t.Rows.Count == 0) { ketLuan.Add("12 tháng gần nhất chưa bán được đơn nào ở danh mục này."); return; }

            int cuoi = t.Rows.Count - 1;
            int caoNhat = 0;
            for (int i = 1; i < _soDT.Count; i++) if (_soDT[i] > _soDT[caoNhat]) caoNhat = i;
            _mauDT[caoNhat] = MauCam;

            string thangCuoi = Convert.ToString(t.Rows[cuoi]["Thang"]);
            string cau = "Tháng " + thangCuoi.Substring(5, 2) + "/" + thangCuoi.Substring(0, 4) + " bán được " + _soDT[cuoi].ToString("#,##0.#") +
                         " triệu từ " + t.Rows[cuoi]["SoDon"] + " đơn";
            if (den.AddDays(1).Month == den.Month) cau += " (tháng này mới tính tới ngày " + den.ToString("dd/MM") + ")";
            if (cuoi > 0 && _soDT[cuoi - 1] > 0)
            {
                double doi = PhanTram(_soDT[cuoi] - _soDT[cuoi - 1], _soDT[cuoi - 1]);
                cau += ", " + (doi >= 0 ? "tăng " : "giảm ") + Math.Abs(doi).ToString("0.#") + "% so với tháng trước";
            }
            cau += ". Tháng bán tốt nhất trong 12 tháng là " + _nhanDT[caoNhat] + " (" + _soDT[caoNhat].ToString("#,##0.#") + " triệu, cột màu cam).";
            ketLuan.Add(cau);
        }

        private void NapLyDo(string dm, List<string> ketLuan)
        {
            DataTable t = AiHelper.Doc(
                "SELECT p.NhomLyDo, COUNT(*) AS SoCau, MAX(ISNULL(c.KieuLyDo, N'')) AS Kieu, MAX(ISNULL(c.CachBanHop, N'')) AS CachBan " +
                "FROM PhanTich_LyDoMua p JOIN KhaoSat_LyDoMua k ON k.Id = p.KhaoSatId " +
                "LEFT JOIN CachBan_TheoLyDoMua c ON c.NhomLyDo = p.NhomLyDo " +
                "WHERE @dm = '' OR EXISTS (SELECT 1 FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP WHERE ct.MaDH = k.MaDH AND sp.MaDanhMuc = @dm) " +
                "GROUP BY p.NhomLyDo ORDER BY COUNT(*) DESC, p.NhomLyDo",
                AiHelper.P("@dm", dm));
            _nhanLD.Clear(); _soLD.Clear(); _mauLD.Clear();
            double tong = 0, camXuc = 0;
            foreach (DataRow r in t.Rows)
            {
                string kieu = Convert.ToString(r["Kieu"]);
                double so = Convert.ToDouble(r["SoCau"]);
                _nhanLD.Add(Convert.ToString(r["NhomLyDo"]));
                _soLD.Add(so);
                _mauLD.Add(kieu == "Cảm xúc" ? MauCam : (kieu == "Cần dùng thật" ? MauXanhDuong : MauXam));
                tong += so;
                if (kieu == "Cảm xúc") camXuc += so;
            }
            if (t.Rows.Count == 0) { ketLuan.Add("Chưa có câu trả lời nào cho câu hỏi \"Vì sao bạn mua?\" với danh mục này."); return; }
            ketLuan.Add(PhanTram(camXuc, tong).ToString("0.#") + "% câu trả lời là mua vì cảm xúc (cột màu cam). Lý do nhiều nhất: \"" + t.Rows[0]["NhomLyDo"] +
                        "\" (" + t.Rows[0]["SoCau"] + " câu). Gợi ý cách bán (lời khuyên viết sẵn, không tính từ số liệu): " + t.Rows[0]["CachBan"]);
        }

        private void NapCamXuc(string dm, List<string> ketLuan)
        {
            DataTable t = AiHelper.Doc(
                "SELECT SUM(CASE WHEN pc.NhomCamXuc = N'Khen' THEN 1 ELSE 0 END) AS Khen, " +
                "       SUM(CASE WHEN pc.NhomCamXuc = N'Chê' THEN 1 ELSE 0 END) AS Che, " +
                "       SUM(CASE WHEN pc.NhomCamXuc = N'Trung tính' THEN 1 ELSE 0 END) AS BinhThuong, " +
                "       COUNT(pc.DoiTuongId) AS DaPhanTich, " +
                "       COUNT(*) - COUNT(pc.DoiTuongId) AS ChuaPhanTich " +
                "FROM DanhGiaSanPham d JOIN SanPham sp ON sp.MaSP = d.MaSP " +
                "LEFT JOIN PhanTich_CamXuc_DanhGia pc ON pc.LoaiDoiTuong = N'Đánh giá' AND pc.DoiTuongId = d.MaDanhGia " +
                "WHERE @dm = '' OR sp.MaDanhMuc = @dm",
                AiHelper.P("@dm", dm));
            DataRow r = t.Rows[0];
            double khen = r["Khen"] == DBNull.Value ? 0 : Convert.ToDouble(r["Khen"]);
            double che = r["Che"] == DBNull.Value ? 0 : Convert.ToDouble(r["Che"]);
            double binhThuong = r["BinhThuong"] == DBNull.Value ? 0 : Convert.ToDouble(r["BinhThuong"]);
            double daPhanTich = Convert.ToDouble(r["DaPhanTich"]);
            double chuaPhanTich = Convert.ToDouble(r["ChuaPhanTich"]);
            _nhanCX.Clear(); _soCX.Clear(); _mauCX.Clear();
            _nhanCX.AddRange(new[] { "Khen", "Bình thường", "Chê" });
            _soCX.AddRange(new[] { khen, binhThuong, che });
            _mauCX.AddRange(new[] { MauVui, MauXam, MauBuon });
            if (daPhanTich == 0)
            {
                ketLuan.Add(chuaPhanTich > 0
                    ? "Có " + chuaPhanTich.ToString("0") + " lời đánh giá chưa được phân tích cảm xúc, nên chưa tính được tỷ lệ."
                    : "Chưa có lời đánh giá nào cho danh mục này.");
                return;
            }
            double cheP = PhanTram(che, daPhanTich);
            string cau = "Lời đánh giá: khen " + PhanTram(khen, daPhanTich).ToString("0.#") + "%, bình thường " +
                         PhanTram(binhThuong, daPhanTich).ToString("0.#") + "%, chê " + cheP.ToString("0.#") + "%. ";
            if (chuaPhanTich > 0) cau += "Còn " + chuaPhanTich.ToString("0") + " lời đánh giá chưa được phân tích cảm xúc, nên không tính vào các tỷ lệ trên. ";
            cau += cheP >= 20
                ? "Lời chê từ 20% trở lên: mở \"Khách hay phàn nàn chuyện gì\" để xem khách chê chuyện gì và sửa trước khi chạy quảng cáo."
                : "Lời chê dưới 20%: khách đang hài lòng, nên xin khách đánh giá kèm ảnh để người mua sau thấy.";
            ketLuan.Add(cau);
        }

        private void NapGio(string dm, List<string> ketLuan)
        {
            DataTable t = AiHelper.Doc(
                "SELECT DATEPART(HOUR, dh.NgayDat) AS Gio, COUNT(*) AS SoDon FROM DonHang dh " +
                "WHERE ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' " +
                "  AND (@dm = '' OR EXISTS (SELECT 1 FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP WHERE ct.MaDH = dh.MaDH AND sp.MaDanhMuc = @dm)) " +
                "GROUP BY DATEPART(HOUR, dh.NgayDat) ORDER BY Gio",
                AiHelper.P("@dm", dm));
            _nhanGio.Clear(); _soGio.Clear(); _mauGio.Clear();
            if (t.Rows.Count == 0) { ketLuan.Add("Chưa có đơn nào để xem giờ chốt đơn."); return; }
            int dau = Convert.ToInt32(t.Rows[0]["Gio"]), cuoi = Convert.ToInt32(t.Rows[t.Rows.Count - 1]["Gio"]);
            Dictionary<int, double> theoGio = t.Rows.Cast<DataRow>().ToDictionary(r => Convert.ToInt32(r["Gio"]), r => Convert.ToDouble(r["SoDon"]));
            int gioDinh = dau;
            for (int h = dau; h <= cuoi; h++)
            {
                double so;
                theoGio.TryGetValue(h, out so);
                _nhanGio.Add(h + "h");
                _soGio.Add(so);
                _mauGio.Add(MauXanhDuong);
                if (so > theoGio[gioDinh]) gioDinh = h;
            }
            _mauGio[gioDinh - dau] = MauCam;
            DataTable khoang = AiHelper.Doc(
                "SELECT MIN(dh.NgayDat) AS Tu, MAX(dh.NgayDat) AS Den FROM DonHang dh " +
                "WHERE ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' " +
                "  AND (@dm = '' OR EXISTS (SELECT 1 FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP WHERE ct.MaDH = dh.MaDH AND sp.MaDanhMuc = @dm))",
                AiHelper.P("@dm", dm));
            ketLuan.Add("Khách chốt nhiều đơn nhất trong khung " + gioDinh + "h đến " + (gioDinh + 1) + "h (tức " + gioDinh.ToString("00") + ":00 đến " + gioDinh.ToString("00") + ":59): " +
                        theoGio[gioDinh] + " đơn, cột màu cam. Tính trên các đơn đặt từ " + Convert.ToDateTime(khoang.Rows[0]["Tu"]).ToString("dd/MM/yyyy") +
                        " đến " + Convert.ToDateTime(khoang.Rows[0]["Den"]).ToString("dd/MM/yyyy") + ", đơn đã hủy không tính. Nên đăng bài, chạy quảng cáo và trực tin nhắn trước giờ này.");
        }

        private void NapMonHot(string dm, List<string> ketLuan)
        {
            DataTable t = AiHelper.Doc(
                "SELECT v.TenSP, v.DuDoan30Ngay, v.TrangThai FROM vw_DuBaoSanPham v JOIN SanPham sp ON sp.MaSP = v.MaSP " +
                "WHERE v.Cau = N'ĐANG HOT - bán nhanh lên' AND (@dm = '' OR sp.MaDanhMuc = @dm) ORDER BY v.XuHuongPT DESC",
                AiHelper.P("@dm", dm));
            if (t.Rows.Count == 0) { ketLuan.Add("Chưa có món nào đang hot ở danh mục này (30 ngày gần bán nhanh hơn trung bình 90 ngày từ 20%)."); return; }
            ketLuan.Add("Món đang hot: " + string.Join(", ", t.Rows.Cast<DataRow>().Select(r =>
                r["TenSP"] + " (dự đoán bán " + r["DuDoan30Ngay"] + " cái trong 30 ngày tới" +
                (Convert.ToString(r["TrangThai"]) == "KHẨN CẤP" || Convert.ToString(r["TrangThai"]) == "ĐẶT HÀNG NGAY" ? ", phải đặt hàng thêm" : "") + ")")) +
                ". Xem chi tiết ở \"Dự báo bán và lúc nào đặt hàng\".");
        }
    }
}
