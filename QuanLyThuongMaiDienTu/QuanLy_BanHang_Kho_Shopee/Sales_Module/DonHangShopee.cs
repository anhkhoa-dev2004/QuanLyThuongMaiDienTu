using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    public static class DonHangShopee
    {
        public const string CHO_XAC_NHAN = "Chờ xác nhận";
        public const string CHO_LAY_HANG = "Chờ lấy hàng";
        public const string DANG_GIAO = "Đang giao";
        public const string DA_GIAO = "Đã giao";
        public const string TRA_HANG = "Trả hàng/Hoàn tiền";
        public const string DA_HUY = "Đã hủy";

        public static readonly string[] TAT_CA_TRANG_THAI = { CHO_XAC_NHAN, CHO_LAY_HANG, DANG_GIAO, DA_GIAO, TRA_HANG, DA_HUY };

        public static readonly string[] CACH_TRA_TIEN =
        {
            "Thanh toán khi nhận hàng (COD)",
            "Chuyển khoản ngân hàng",
            "Ví ShopeePay",
            "Thẻ ngân hàng",
            "Tiền mặt tại cửa hàng"
        };

        public const string GIAI_THICH_LUONG_DON =
            "Đơn đi theo đúng thứ tự như trên Shopee: Chờ xác nhận → (shop xác nhận và xuất kho) Chờ lấy hàng → " +
            "(giao cho đơn vị vận chuyển) Đang giao → Đã giao. Khách nhận tại cửa hàng thì xác nhận & xuất kho xong là Đã giao luôn. " +
            "Hủy đơn lúc Chờ lấy hàng thì hàng vào lại kho ngay; giao không thành công thì hàng \"Đang hoàn về\", tới kho mới bấm \"Nhận hàng hoàn về kho\". " +
            "Đơn đã giao mà khách muốn trả thì làm \"Trả hàng & hoàn tiền\".";

        public static DataTable LayKenhBan()
        {
            return AiHelper.Doc("SELECT MaDoiTac, TenDoiTac, LoaiKenh FROM DoiTac ORDER BY MaDoiTac");
        }

        public static DataTable LayKho()
        {
            return AiHelper.Doc("SELECT MaKho, MaKho + N' - ' + TenKho AS Hien FROM Kho ORDER BY MaKho");
        }

        public static decimal UocTinhPhiSan(string maDoiTac, IDictionary<string, decimal> tienTheoMaSP, decimal giamGia, decimal ship, DateTime ngay, out string chiTiet)
        {
            chiTiet = "";
            if (string.IsNullOrEmpty(maDoiTac)) return 0;
            decimal tienHang = 0;
            foreach (decimal v in tienTheoMaSP.Values) tienHang += v;
            DataTable t = AiHelper.Doc(
                "SELECT p.LoaiPhi, p.CachTinh, p.GiaTri, p.MaDanhMuc, dm.TenDanhMuc FROM PhiSan p LEFT JOIN DanhMuc dm ON dm.MaDanhMuc = p.MaDanhMuc " +
                "WHERE p.MaDoiTac = @k AND p.TuNgay <= @n AND (p.DenNgay IS NULL OR p.DenNgay >= @n) ORDER BY p.Id",
                AiHelper.P("@k", maDoiTac), AiHelper.P("@n", ngay.Date));
            Dictionary<string, string> danhMucCuaSP = new Dictionary<string, string>();
            foreach (DataRow r in AiHelper.Doc("SELECT MaSP, MaDanhMuc FROM SanPham").Rows)
                danhMucCuaSP[Convert.ToString(r["MaSP"])] = Convert.ToString(r["MaDanhMuc"]);
            decimal tong = 0;
            List<string> ds = new List<string>();
            foreach (DataRow r in t.Rows)
            {
                string cach = Convert.ToString(r["CachTinh"]);
                decimal giaTri = Convert.ToDecimal(r["GiaTri"]);
                string maDM = r["MaDanhMuc"] == DBNull.Value ? null : Convert.ToString(r["MaDanhMuc"]);
                decimal tien;
                if (cach == "Phần trăm tiền hàng")
                {
                    decimal coSo = tienHang - giamGia;
                    if (maDM != null)
                    {
                        coSo = 0;
                        foreach (KeyValuePair<string, decimal> kv in tienTheoMaSP)
                        {
                            string dmSP;
                            if (danhMucCuaSP.TryGetValue(kv.Key, out dmSP) && dmSP == maDM) coSo += kv.Value;
                        }
                        if (coSo == 0) continue;
                    }
                    tien = coSo * giaTri / 100m;
                }
                else if (cach == "Phần trăm tiền khách trả") tien = (tienHang - giamGia + ship) * giaTri / 100m;
                else tien = giaTri;
                tien = Math.Round(tien, 0, MidpointRounding.AwayFromZero);
                tong += tien;
                ds.Add(r["LoaiPhi"] + (maDM != null ? " (" + r["TenDanhMuc"] + ")" : "") + " " + tien.ToString("N0") + " đ");
            }
            chiTiet = ds.Count == 0 ? "kênh này chưa nhập mức phí sàn" : string.Join(", ", ds);
            return tong;
        }

        public static string XacNhanVaXuatKho(string maDH, string maKho)
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand("sp_XacNhanVaXuatKho", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaDH", SqlDbType.VarChar, 20).Value = maDH;
                cmd.Parameters.Add("@MaKho", SqlDbType.VarChar, 20).Value = maKho;
                cmd.Parameters.Add("@NguoiLap", SqlDbType.NVarChar, 100).Value = string.IsNullOrEmpty(Session.HoTen) ? (object)DBNull.Value : Session.HoTen;
                SqlParameter ra = cmd.Parameters.Add("@MaPX", SqlDbType.VarChar, 20);
                ra.Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToString(ra.Value);
            }
        }

        public static void HuyDon(string maDH, string lyDo)
        {
            AiHelper.ChayThuTuc("sp_HuyDon", AiHelper.P("@MaDH", maDH), AiHelper.P("@LyDo", lyDo));
        }

        public static void NhanHangHoanVeKho(string maDH)
        {
            AiHelper.ChayThuTuc("sp_NhanHangHoanVeKho", AiHelper.P("@MaDH", maDH));
        }

        public static string TrangThaiDon(string maDH)
        {
            return Convert.ToString(AiHelper.LayGiaTri("SELECT TrangThai FROM DonHang WHERE MaDH = @d", AiHelper.P("@d", maDH)));
        }

        public static string ThongBaoSauXuatKho(string maDH, string maPX)
        {
            string tt = TrangThaiDon(maDH);
            if (tt == DA_GIAO)
                return "Đã xuất kho cho đơn " + maDH + " (phiếu xuất " + maPX + ").\nKhách nhận tại cửa hàng nên đơn đã chuyển sang \"Đã giao\".";
            return "Đã xác nhận đơn " + maDH + " và lập phiếu xuất " + maPX + ".\nĐơn chuyển sang \"" + tt + "\". " +
                   "Khi đơn vị vận chuyển tới lấy, vào \"Vận đơn & giao hàng\" để tạo vận đơn.";
        }

        public static string HoiLyDo(string tieuDe, string cauHoi, string[] goiY)
        {
            using (Form f = new Form())
            {
                f.Text = tieuDe;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MaximizeBox = false;
                f.MinimizeBox = false;
                f.StartPosition = FormStartPosition.CenterParent;
                f.ClientSize = new Size(460, 150);
                f.Font = new Font("Segoe UI", 9F);
                Label l = new Label { Left = 12, Top = 12, Width = 436, Height = 36, Text = cauHoi };
                ComboBox c = new ComboBox { Left = 12, Top = 52, Width = 436, DropDownStyle = ComboBoxStyle.DropDown };
                if (goiY != null) c.Items.AddRange(goiY);
                Button ok = new Button { Text = "Đồng ý", Left = 252, Top = 104, Width = 95, Height = 30, DialogResult = DialogResult.OK };
                Button huy = new Button { Text = "Thôi", Left = 353, Top = 104, Width = 95, Height = 30, DialogResult = DialogResult.Cancel };
                f.Controls.AddRange(new Control[] { l, c, ok, huy });
                f.AcceptButton = ok;
                f.CancelButton = huy;
                if (f.ShowDialog() != DialogResult.OK) return null;
                string kq = c.Text.Trim();
                return kq.Length == 0 ? null : kq;
            }
        }
    }
}
