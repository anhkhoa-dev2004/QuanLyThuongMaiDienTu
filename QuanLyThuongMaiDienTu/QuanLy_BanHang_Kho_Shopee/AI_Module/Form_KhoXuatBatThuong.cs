using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_KhoXuatBatThuong : Form
    {
        private DataTable _ketQua;

        public Form_KhoXuatBatThuong()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            GiaoDien.ThemBangDuoi(this, "Phiếu xuất cần xem lại: không gắn đơn, xuất nhiều hơn khách đặt, lập ngoài giờ 22h - 8h", 240,
                () => AiHelper.Doc(
                    "SELECT LoaiBatThuong AS [Loại], MaPX AS [Phiếu xuất], NgayXuat AS [Ngày xuất], MaKho AS [Kho], ISNULL(MaDH, N'(không có)') AS [Mã đơn], " +
                    "       MaSP AS [Mã SP], TenSP AS [Tên sản phẩm], SoLuongDangNgo AS [Số lượng đáng ngờ], GiaTriTheoGiaVon AS [Giá trị theo giá vốn], " +
                    "       ISNULL(NguoiLap, N'') AS [Người lập], GhiChu AS [Ghi chú] " +
                    "FROM dbo.vw_XuatKhoBatThuong ORDER BY NgayXuat DESC"),
                null);
            lblGhiChu.Text = "So số lượng xuất mỗi ngày của từng sản phẩm với ngày bình thường; ngày nào xuất nhiều hoặc ít khác thường thì báo và lưu vào nhật ký được. Nút quét nhiều chỉ số xét cùng lúc số lượng xuất, mức so với trung bình và số phiếu trong ngày; kết quả chỉ để xem, không lưu.";
        }

        private void btnQuetNhieuChiSo_Click(object sender, EventArgs e)
        {
            QuetIsolationForest();
        }

        private void QuetIsolationForest()
        {
            int n = (int)numSoNgay.Value;
            DateTime den = dtpDenNgay.Value.Date, tu = den.AddDays(-(n - 1));
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT ct.MaSP, sp.TenSP, CAST(px.NgayXuat AS DATE) AS Ngay, SUM(ct.SoLuong) AS SL, COUNT(DISTINCT px.MaPX) AS SoPhieu " +
                    "FROM ChiTietPhieuXuat ct JOIN PhieuXuat px ON px.MaPX = ct.MaPX JOIN SanPham sp ON sp.MaSP = ct.MaSP " +
                    "WHERE px.NgayXuat >= @tu AND px.NgayXuat < @den GROUP BY ct.MaSP, sp.TenSP, CAST(px.NgayXuat AS DATE)",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den.AddDays(1)));
                int m = dt.Rows.Count;
                if (m < 20) { AiHelper.BaoTin("Mới có " + m + " cặp (sản phẩm, ngày có xuất) trong khoảng này, quá ít để so (cần ít nhất 20)."); return; }

                Dictionary<string, double> tongSP = new Dictionary<string, double>();
                foreach (DataRow r in dt.Rows)
                {
                    string ma = Convert.ToString(r["MaSP"]);
                    double v; tongSP.TryGetValue(ma, out v); tongSP[ma] = v + Convert.ToDouble(r["SL"]);
                }
                double[,] x = new double[m, 3];
                for (int i = 0; i < m; i++)
                {
                    double sl = Convert.ToDouble(dt.Rows[i]["SL"]);
                    double tb = tongSP[Convert.ToString(dt.Rows[i]["MaSP"])] / n;
                    x[i, 0] = sl; x[i, 1] = tb > 0 ? sl / tb : 0; x[i, 2] = Convert.ToDouble(dt.Rows[i]["SoPhieu"]);
                }
                double[] diem = QuetNhieuChiSo.Diem(DuaVeCungThang.LechSoVoiTrungBinhCot(x), 100, 256, 1);

                DataTable kq = new DataTable();
                kq.Columns.Add("MaSP", typeof(string)); kq.Columns.Add("TenSP", typeof(string)); kq.Columns.Add("Ngay", typeof(DateTime));
                kq.Columns.Add("SoLuongXuat", typeof(double)); kq.Columns.Add("SoPhieuXuat", typeof(int)); kq.Columns.Add("DiemBatThuong", typeof(double));
                for (int i = 0; i < m; i++)
                    if (diem[i] >= 0.6)
                        kq.Rows.Add(dt.Rows[i]["MaSP"], dt.Rows[i]["TenSP"], Convert.ToDateTime(dt.Rows[i]["Ngay"]).Date, x[i, 0], (int)x[i, 2], Math.Round(diem[i], 3));
                kq.DefaultView.Sort = "DiemBatThuong DESC";
                _ketQua = null;
                dgvKetQua.DataSource = kq.DefaultView.ToTable();
                AiHelper.DinhDangSo(dgvKetQua, "Ngay", "dd/MM/yyyy");
                if (kq.Rows.Count == 0) AiHelper.BaoTin("Không thấy ngày nào khác thường (điểm từ 0.6 trở lên) trong " + m + " cặp.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Form_KhoXuatBatThuong_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvKetQua);
            AiHelper.DinhDangLuoi(dgvLog);
            try
            {
                object mx = AiHelper.LayGiaTri("SELECT MAX(NgayXuat) FROM PhieuXuat");
                dtpDenNgay.Value = mx != null ? Convert.ToDateTime(mx).Date : DateTime.Today;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
            TaiLog();
        }

        private void TaiLog()
        {
            try
            {
                dgvLog.DataSource = AiHelper.Doc(
                    "SELECT TOP 200 l.Id, l.SanPhamId, sp.TenSP, l.SoLuongLech, l.ChiSo_ZScore, l.MucDoCaiBao, l.ThoiGian " +
                    "FROM Log_BatThuongKho l JOIN SanPham sp ON sp.MaSP = l.SanPhamId ORDER BY l.Id DESC");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnQuet_Click(object sender, EventArgs e)
        {
            int n = (int)numSoNgay.Value;
            double nguong = (double)numNguongZ.Value;
            DateTime den = dtpDenNgay.Value.Date, tu = den.AddDays(-(n - 1));
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT ct.MaSP, sp.TenSP, CAST(px.NgayXuat AS DATE) AS Ngay, SUM(ct.SoLuong) AS SL " +
                    "FROM ChiTietPhieuXuat ct JOIN PhieuXuat px ON px.MaPX = ct.MaPX JOIN SanPham sp ON sp.MaSP = ct.MaSP " +
                    "WHERE px.NgayXuat >= @tu AND px.NgayXuat < @den " +
                    "GROUP BY ct.MaSP, sp.TenSP, CAST(px.NgayXuat AS DATE)",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den.AddDays(1)));

                Dictionary<string, string> ten = new Dictionary<string, string>();
                Dictionary<string, double[]> chuoi = new Dictionary<string, double[]>();
                foreach (DataRow r in dt.Rows)
                {
                    string ma = Convert.ToString(r["MaSP"]);
                    if (!chuoi.ContainsKey(ma)) { chuoi[ma] = new double[n]; ten[ma] = Convert.ToString(r["TenSP"]); }
                    int i = (int)(Convert.ToDateTime(r["Ngay"]).Date - tu).TotalDays;
                    if (i >= 0 && i < n) chuoi[ma][i] += Convert.ToDouble(r["SL"]);
                }

                DataTable kq = new DataTable();
                kq.Columns.Add("MaSP", typeof(string)); kq.Columns.Add("TenSP", typeof(string)); kq.Columns.Add("Ngay", typeof(DateTime));
                kq.Columns.Add("SoLuongXuat", typeof(double)); kq.Columns.Add("TrungBinhNgay", typeof(double)); kq.Columns.Add("DoLechChuan", typeof(double));
                kq.Columns.Add("LechSoVoiTrungBinh", typeof(double)); kq.Columns.Add("MucDo", typeof(string)); kq.Columns.Add("SoLuongLech", typeof(int));

                foreach (KeyValuePair<string, double[]> kv in chuoi)
                {
                    double[] a = kv.Value;
                    double tb = 0; foreach (double v in a) tb += v; tb /= n;
                    double s2 = 0; foreach (double v in a) s2 += (v - tb) * (v - tb);
                    double sd = Math.Sqrt(s2 / (n - 1));
                    if (sd < 1e-9) continue;
                    for (int i = 0; i < n; i++)
                    {
                        if (a[i] <= 0) continue;
                        double z = (a[i] - tb) / sd;
                        if (Math.Abs(z) < nguong) continue;
                        kq.Rows.Add(kv.Key, ten[kv.Key], tu.AddDays(i), a[i], Math.Round(tb, 2), Math.Round(sd, 2), Math.Round(z, 2),
                                    Math.Abs(z) >= 3 ? "Nghiêm trọng" : "Chú ý", (int)Math.Round(a[i] - tb));
                    }
                }
                kq.DefaultView.Sort = "LechSoVoiTrungBinh DESC";
                _ketQua = kq.DefaultView.ToTable();
                dgvKetQua.DataSource = _ketQua;
                AiHelper.DinhDangSo(dgvKetQua, "Ngay", "dd/MM/yyyy");
                if (_ketQua.Rows.Count == 0)
                    AiHelper.BaoTin("Không có ngày nào lệch khỏi bình thường từ mức " + nguong.ToString("0.0") + " trong " + n + " ngày đến " + den.ToString("dd/MM/yyyy") + ".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLuuLog_Click(object sender, EventArgs e)
        {
            if (_ketQua == null || _ketQua.Rows.Count == 0) { AiHelper.BaoTin("Chưa có kết quả quét để lưu."); return; }
            int daLuu = 0;
            try
            {
                foreach (DataRow r in _ketQua.Rows)
                {
                    daLuu += AiHelper.Chay(
                        "IF NOT EXISTS (SELECT 1 FROM Log_BatThuongKho WHERE SanPhamId = @sp AND CAST(ThoiGian AS DATE) = @ngay) " +
                        "INSERT INTO Log_BatThuongKho (SanPhamId, SoLuongLech, ChiSo_ZScore, MucDoCaiBao, ThoiGian) VALUES (@sp, @lech, @z, @muc, @ngay)",
                        AiHelper.P("@sp", r["MaSP"]), AiHelper.P("@ngay", r["Ngay"]), AiHelper.P("@lech", r["SoLuongLech"]),
                        AiHelper.PDec("@z", Convert.ToDecimal(r["LechSoVoiTrungBinh"]), 10, 4), AiHelper.P("@muc", r["MucDo"]));
                }
                AiHelper.BaoTin("Đã lưu " + daLuu + " dòng mới (bỏ qua " + (_ketQua.Rows.Count - daLuu) + " dòng đã có sẵn cùng sản phẩm và ngày).");
                TaiLog();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
