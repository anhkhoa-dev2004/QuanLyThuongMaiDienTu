using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_DonDuLieuTuDong : Form
    {
        public Form_DonDuLieuTuDong()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            lblGhiChu.Text = "Làm sạch dữ liệu (số điện thoại, địa chỉ, lý do trả hàng) và điền dữ liệu còn trống bằng trung bình theo danh mục. Khung bên dưới: điền giá bán còn trống theo 3 sản phẩm giống nhất (nên chạy trước khi điền trung bình) và liệt kê giá trị bất thường để kiểm tra (không xóa).";
            KhoiTaoChucNang();
        }

        private void KhoiTaoChucNang()
        {
            AiHelper.DinhDangLuoi(dgvNgoaiLe);
        }

        private void btnDienGiaKnn_Click(object sender, EventArgs e)
        {
            DienGiaBanBangKnn();
        }

        private void btnTimNgoaiLe_Click(object sender, EventArgs e)
        {
            TimNgoaiLeIqr();
        }

        private void GhiNhatKy(string ten, int daSua, int loi)
        {
            AiHelper.Chay("INSERT INTO Log_TienXuLyDuLieu (TenTienTrinh, SoBanGhiDaSua, SoBanGhiLoi, ThoiGianChay) VALUES (@t, @s, @l, GETDATE())",
                          AiHelper.P("@t", ten), AiHelper.P("@s", daSua), AiHelper.P("@l", loi));
        }

        private void DienGiaBanBangKnn()
        {
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT sp.MaSP, sp.TenSP, sp.MaDanhMuc, sp.GiaBan, CAST(ISNULL(sp.NguongAnToan, 0) AS FLOAT) AS Nguong, " +
                    "       CAST(ISNULL((SELECT SUM(SoLuongTon) FROM TonKho WHERE MaSP = sp.MaSP), 0) AS FLOAT) AS TongTon, " +
                    "       CAST(ISNULL((SELECT SUM(SoLuong) FROM ChiTietDonHang WHERE MaSP = sp.MaSP), 0) AS FLOAT) AS TongBan " +
                    "FROM SanPham sp");
                int n = dt.Rows.Count;
                List<int> thieu = new List<int>();
                for (int i = 0; i < n; i++) if (dt.Rows[i]["GiaBan"] == DBNull.Value) thieu.Add(i);
                if (thieu.Count == 0) { AiHelper.BaoTin("Không có sản phẩm nào thiếu giá bán."); return; }

                List<string[]> duDoan = new List<string[]>();
                int khongDien = 0;
                foreach (int m in thieu)
                {
                    string dm = Convert.ToString(dt.Rows[m]["MaDanhMuc"]);
                    List<int> cungDm = Enumerable.Range(0, n).Where(i => Convert.ToString(dt.Rows[i]["MaDanhMuc"]) == dm).ToList();
                    int coGiaCungDm = cungDm.Count(i => i != m && dt.Rows[i]["GiaBan"] != DBNull.Value);
                    List<int> tap = coGiaCungDm >= 2 ? cungDm : Enumerable.Range(0, n).ToList();

                    double[,] x = new double[tap.Count, 3];
                    double?[] gia = new double?[tap.Count];
                    int viTri = 0;
                    for (int j = 0; j < tap.Count; j++)
                    {
                        DataRow r = dt.Rows[tap[j]];
                        x[j, 0] = Convert.ToDouble(r["Nguong"]); x[j, 1] = Convert.ToDouble(r["TongTon"]); x[j, 2] = Convert.ToDouble(r["TongBan"]);
                        gia[j] = r["GiaBan"] == DBNull.Value ? (double?)null : Convert.ToDouble(r["GiaBan"]);
                        if (tap[j] == m) viTri = j;
                    }
                    double? p = DienTheoMonGiong.DuDoan(DuaVeCungThang.VeKhoangKhongMotCot(x), gia, viTri, 3);
                    if (p.HasValue) duDoan.Add(new[] { Convert.ToString(dt.Rows[m]["MaSP"]), Convert.ToString(dt.Rows[m]["TenSP"]), Math.Round(p.Value, 0).ToString("0") });
                    else khongDien++;
                }
                if (duDoan.Count == 0) { AiHelper.BaoTin("Không sản phẩm nào có giá để làm mốc so sánh nên chưa điền được."); return; }

                StringBuilder sb = new StringBuilder();
                foreach (string[] d in duDoan) sb.AppendLine("- " + d[0] + " " + d[1] + ": " + decimal.Parse(d[2]).ToString("N0") + " đ");
                if (MessageBox.Show("Giá gợi ý (trung bình 3 sản phẩm giống nhất) cho " + duDoan.Count + " sản phẩm:\n\n" + sb + "\nGhi các giá này vào hệ thống?",
                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                int daSua = 0;
                foreach (string[] d in duDoan)
                    daSua += AiHelper.Chay("UPDATE SanPham SET GiaBan = @g WHERE MaSP = @m AND GiaBan IS NULL", AiHelper.PDec("@g", decimal.Parse(d[2]), 18, 2), AiHelper.P("@m", d[0]));
                GhiNhatKy("Điền giá bán còn trống", daSua, khongDien);
                lblKetQua.Text = "Đã điền " + daSua + " giá bán, " + khongDien + " sản phẩm không điền được.";
                LamMoi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void TimNgoaiLeIqr()
        {
            try
            {
                DataTable kq = new DataTable();
                kq.Columns.Add("Bảng / Cột", typeof(string)); kq.Columns.Add("Mã", typeof(string)); kq.Columns.Add("Giá Trị", typeof(decimal));
                kq.Columns.Add("Ngưỡng Dưới", typeof(decimal)); kq.Columns.Add("Ngưỡng Trên", typeof(decimal));
                string thongBao = "";
                thongBao += QuetMotCot(kq, "DonHang.TongTien", "SELECT MaDH, CAST(TongTien AS FLOAT) FROM DonHang WHERE TongTien IS NOT NULL AND ISNULL(TrangThai, N'') NOT LIKE N'%hủy%'");
                thongBao += QuetMotCot(kq, "SanPham.GiaBan", "SELECT MaSP, CAST(GiaBan AS FLOAT) FROM SanPham WHERE GiaBan IS NOT NULL AND GiaBan > 0");
                dgvNgoaiLe.DataSource = kq;
                GhiNhatKy("Tìm giá / tiền bất thường", 0, kq.Rows.Count);
                lblKetQua.Text = "Tìm thấy " + kq.Rows.Count + " giá trị bất thường. " + thongBao;
                LamMoi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private string QuetMotCot(DataTable kq, string ten, string sql)
        {
            DataTable d = AiHelper.Doc(sql);
            if (d.Rows.Count < 4) return "[" + ten + ": chưa đủ dữ liệu, có " + d.Rows.Count + " dòng, cần ít nhất 4] ";
            double[] v = new double[d.Rows.Count];
            for (int i = 0; i < v.Length; i++) v[i] = Convert.ToDouble(d.Rows[i][1]);
            double duoi, tren;
            bool[] ngoai = TimGiaTriLa.DanhDau(v, 1.5, out duoi, out tren);
            for (int i = 0; i < v.Length; i++)
                if (ngoai[i]) kq.Rows.Add(ten, Convert.ToString(d.Rows[i][0]), (decimal)v[i], (decimal)Math.Round(duoi, 0), (decimal)Math.Round(tren, 0));
            return "";
        }

        private void Form_DonDuLieuTuDong_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvThieu);
            AiHelper.DinhDangLuoi(dgvLog);
            LamMoi();
        }

        private void LamMoi()
        {
            try
            {
                dgvThieu.DataSource = AiHelper.Doc(
                    "SELECT N'SanPham.GiaBan (trống)' AS TruongDuLieu, COUNT(*) AS SoBanGhi FROM SanPham WHERE GiaBan IS NULL " +
                    "UNION ALL SELECT N'SanPham.Barcode (trống)', COUNT(*) FROM SanPham WHERE Barcode IS NULL OR LTRIM(RTRIM(Barcode)) = '' " +
                    "UNION ALL SELECT N'SanPham.HinhAnh (trống)', COUNT(*) FROM SanPham WHERE HinhAnh IS NULL OR LTRIM(RTRIM(HinhAnh)) = N'' " +
                    "UNION ALL SELECT N'SanPham.MaSKUShopee (trống)', COUNT(*) FROM SanPham WHERE MaSKUShopee IS NULL OR LTRIM(RTRIM(MaSKUShopee)) = '' " +
                    "UNION ALL SELECT N'KhachHang.SoDT (trống)', COUNT(*) FROM KhachHang WHERE SoDT IS NULL OR LTRIM(RTRIM(SoDT)) = '' " +
                    "UNION ALL SELECT N'KhachHang.DiaChi (trống)', COUNT(*) FROM KhachHang WHERE DiaChi IS NULL OR LTRIM(RTRIM(DiaChi)) = N'' " +
                    "UNION ALL SELECT N'KhachHang chưa chuẩn hóa SĐT/địa chỉ', COUNT(*) FROM KhachHang WHERE SoDienThoai_ChuanHoa IS NULL OR DiaChi_ChuanHoa IS NULL " +
                    "UNION ALL SELECT N'TraHang chưa làm sạch lý do', COUNT(*) FROM TraHang WHERE LyDoTra IS NOT NULL AND NoiDung_DaLamSach IS NULL");
                dgvLog.DataSource = AiHelper.Doc(
                    "SELECT TOP 100 Id, TenTienTrinh, SoBanGhiDaSua, SoBanGhiLoi, ThoiGianChay FROM Log_TienXuLyDuLieu ORDER BY Id DESC");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private string ChayLamSach()
        {
            DataTable kq = AiHelper.ChayThuTuc("sp_TuDongLamSach_DuLieuGiaoDich");
            return "Làm sạch: sửa " + AiHelper.SoDau(kq, "SoBanGhiDaSua") + " bản ghi, còn " + AiHelper.SoDau(kq, "SoBanGhiLoi") + " SĐT thiếu/sai.";
        }

        private string ChayDienThieu()
        {
            DataTable kq = AiHelper.ChayThuTuc("sp_PhatHienVaSua_DuLieuThieu");
            return "Điền thiếu: điền " + AiHelper.SoDau(kq, "SoBanGhiDaSua") + " ô, còn " + AiHelper.SoDau(kq, "SoBanGhiConThieu") + " chỗ không điền được.";
        }

        private void btnLamSach_Click(object sender, EventArgs e)
        {
            try { lblKetQua.Text = ChayLamSach(); LamMoi(); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnDienThieu_Click(object sender, EventArgs e)
        {
            try { lblKetQua.Text = ChayDienThieu(); LamMoi(); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnChayCaHai_Click(object sender, EventArgs e)
        {
            try { lblKetQua.Text = ChayLamSach() + "  |  " + ChayDienThieu(); LamMoi(); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
