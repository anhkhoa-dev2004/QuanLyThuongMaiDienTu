using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_DuBaoBanHang : Form
    {
        private List<string> _nhanX = new List<string>();
        private List<double> _thuc = new List<double>();
        private List<double> _duBao = new List<double>();
        private string _tieuDe = "";

        public Form_DuBaoBanHang()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            AiHelper.BatDoubleBuffer(pnlBieuDo);
            cboCachDuBao.SelectedIndex = 0;
            GiaoDien.TuChayKhiDoi(() => btnXem_Click(btnXem, EventArgs.Empty), cboSanPham, cboLoai, cboCachDuBao, numLichSu, numDuBao, numTB, dtpDenNgay);
            GiaoDien.ThemBangDuoi(this, "Dự đoán và việc nên làm cho từng sản phẩm (tự tính lại khi có đơn, phiếu mới)", 280,
                () => AiHelper.Doc(
                    "SELECT MaSP AS [Mã SP], TenSP AS [Sản phẩm], TonHienTai AS [Tồn], CAST(ADS AS DECIMAL(10, 2)) AS [Bán mỗi ngày], " +
                    "       Cau AS [Xu hướng bán], DuDoan30Ngay AS [Dự đoán bán 30 ngày tới], SoNgayConLai AS [Đủ bán (ngày)], " +
                    "       ROP AS [Mức phải đặt hàng], SoNenNhap AS [Nên nhập thêm], MonHayMuaChung AS [Hay mua chung với], " +
                    "       TrangThai AS [Tình trạng], ViecNenLam AS [Nên làm gì] " +
                    "FROM vw_DuBaoSanPham " +
                    "ORDER BY CASE TrangThai WHEN N'KHẨN CẤP' THEN 0 WHEN N'ĐẶT HÀNG NGAY' THEN 1 ELSE 2 END, " +
                    "         CASE Cau WHEN N'ĐANG HOT - bán nhanh lên' THEN 0 ELSE 1 END, MaSP"),
                r =>
                {
                    cboSanPham.SelectedValue = Convert.ToString(r.Cells["Mã SP"].Value);
                    btnXem_Click(btnXem, EventArgs.Empty);
                });
            lblGhiChu.Text = "Có 3 cách dự báo: lấy trung bình N ngày gần nhất (mặc định), theo xu hướng các ngày gần đây, hoặc theo xu hướng và thứ trong tuần. Nút tính mức phải đặt hàng lấy số ngày hàng về từ phiếu nhập (ngày nhập - ngày đặt hàng); ô số ngày hàng về chỉ dùng cho nhà cung cấp chưa có số liệu.";
        }

        private double[] TinhDuBao(double[] ls, DateTime tu, int d, int k, double tb, out string cach, out string canhBao)
        {
            canhBao = null;
            double[] trungBinh = new double[d];
            for (int i = 0; i < d; i++) trungBinh[i] = tb;
            cach = "trung bình " + k + " ngày gần nhất = " + tb.ToString("N2", CultureInfo.CurrentCulture);
            if (cboCachDuBao.SelectedIndex <= 0) return trungBinh;

            string loi;
            bool laArima = cboCachDuBao.SelectedIndex == 1;
            double[] kq = laArima ? DuBaoTheoXuHuong.DuBao(ls, d, out loi) : DuBaoTheoThuTrongTuan.DuBao(ls, tu, d, 1.0, out loi);
            if (kq == null)
            {
                canhBao = "Không dùng được " + (laArima ? "cách theo xu hướng" : "cách theo thứ trong tuần") + ": " + loi + " Đang dùng trung bình " + k + " ngày.";
                return trungBinh;
            }
            cach = laArima ? "theo xu hướng các ngày gần đây" : "theo xu hướng và thứ trong tuần";
            return kq;
        }

        private void Form_DuBaoBanHang_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvDuBao);
            try
            {
                DataTable sp = AiHelper.Doc("SELECT MaSP, MaSP + N' - ' + TenSP AS Hien FROM SanPham ORDER BY MaSP");
                AiHelper.NapCombo(cboSanPham, sp, "MaSP", "Hien");
                cboLoai.SelectedIndex = 0;

                object mx = AiHelper.LayGiaTri("SELECT MAX(NgayXuat) FROM PhieuXuat");
                dtpDenNgay.Value = mx != null && mx != DBNull.Value ? Convert.ToDateTime(mx).Date : DateTime.Today;
                if (cboSanPham.SelectedValue != null) btnXem_Click(btnXem, EventArgs.Empty);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;
            string ma = cboSanPham.SelectedValue.ToString();
            bool xuat = cboLoai.SelectedIndex == 0;
            int n = (int)numLichSu.Value, d = (int)numDuBao.Value, k = (int)numTB.Value;
            if (k > n) k = n;
            DateTime den = dtpDenNgay.Value.Date, tu = den.AddDays(-(n - 1));

            try
            {
                string sql = xuat
                    ? "SELECT CAST(px.NgayXuat AS DATE) AS Ngay, SUM(ct.SoLuong) AS SL FROM ChiTietPhieuXuat ct JOIN PhieuXuat px ON px.MaPX = ct.MaPX " +
                      "WHERE ct.MaSP = @sp AND px.NgayXuat >= @tu AND px.NgayXuat < @den GROUP BY CAST(px.NgayXuat AS DATE)"
                    : "SELECT CAST(pn.NgayNhap AS DATE) AS Ngay, SUM(ct.SoLuong) AS SL FROM ChiTietPhieuNhap ct JOIN PhieuNhap pn ON pn.MaPN = ct.MaPN " +
                      "WHERE ct.MaSP = @sp AND pn.NgayNhap >= @tu AND pn.NgayNhap < @den GROUP BY CAST(pn.NgayNhap AS DATE)";
                DataTable dt = AiHelper.Doc(sql, AiHelper.P("@sp", ma), AiHelper.P("@tu", tu), AiHelper.P("@den", den.AddDays(1)));

                Dictionary<DateTime, double> theoNgay = new Dictionary<DateTime, double>();
                foreach (DataRow r in dt.Rows) theoNgay[Convert.ToDateTime(r["Ngay"]).Date] = Convert.ToDouble(r["SL"]);

                double[] ls = new double[n];
                for (int i = 0; i < n; i++) { double v; ls[i] = theoNgay.TryGetValue(tu.AddDays(i), out v) ? v : 0; }

                double tb = 0; for (int i = n - k; i < n; i++) tb += ls[i]; tb /= k;

                string cach, canhBao;
                double[] db = TinhDuBao(ls, tu, d, k, tb, out cach, out canhBao);

                _nhanX.Clear(); _thuc.Clear(); _duBao.Clear();
                DataTable bang = new DataTable();
                bang.Columns.Add("Ngay", typeof(string)); bang.Columns.Add("Loai", typeof(string)); bang.Columns.Add("SoLuong", typeof(double));
                for (int i = 0; i < n; i++)
                {
                    _nhanX.Add(tu.AddDays(i).ToString("dd/MM")); _thuc.Add(ls[i]);
                    _duBao.Add(i == n - 1 ? ls[i] : double.NaN);
                    if (i >= n - 30) bang.Rows.Add(tu.AddDays(i).ToString("dd/MM/yyyy"), "Thực tế", ls[i]);
                }
                for (int i = 1; i <= d; i++)
                {
                    _nhanX.Add(den.AddDays(i).ToString("dd/MM")); _thuc.Add(double.NaN); _duBao.Add(db[i - 1]);
                    bang.Rows.Add(den.AddDays(i).ToString("dd/MM/yyyy"), "Dự báo", Math.Round(db[i - 1], 2));
                }
                dgvDuBao.DataSource = bang;
                _tieuDe = (xuat ? "Số lượng xuất" : "Số lượng nhập") + " mỗi ngày - " + ma + " (dự báo = " + cach + ")";
                pnlBieuDo.Invalidate();
                HienThongSo(ma, ls, n);
                if (canhBao != null) lblThongSo.Text += "  |  " + canhBao;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void HienThongSo(string ma, double[] ls, int n)
        {
            DataTable t = AiHelper.Doc(
                "SELECT TonAnToan AS TonKhoAnToan, ROP AS DiemDatHangLai_ROP, SoNgayConLai AS SoNgayConLai_DuKien, TonHienTai, Cau, DuDoan30Ngay " +
                "FROM vw_DuBaoSanPham WHERE MaSP = @sp", AiHelper.P("@sp", ma));
            if (t.Rows.Count == 0) { lblThongSo.Text = ""; return; }
            DataRow r = t.Rows[0];
            Func<string, string> f = c => r[c] == DBNull.Value ? "chưa tính" : Convert.ToDecimal(r[c]).ToString("N0");
            double tong = 0; foreach (double v in ls) tong += v;
            lblThongSo.Text = "Tồn hiện tại: " + Convert.ToInt32(r["TonHienTai"]).ToString("N0") +
                              "  |  Tồn dự phòng: " + f("TonKhoAnToan") +
                              "  |  Mức phải đặt hàng: " + f("DiemDatHangLai_ROP") +
                              "  |  Còn bán được: " + (r["SoNgayConLai_DuKien"] == DBNull.Value ? "chưa tính" : Convert.ToDecimal(r["SoNgayConLai_DuKien"]).ToString("N1") + " ngày") +
                              "  |  Xu hướng: " + r["Cau"] + "  |  Dự đoán bán 30 ngày tới: " + r["DuDoan30Ngay"] + " cái" +
                              "  |  TB/ngày trên biểu đồ: " + (tong / n).ToString("N2");
        }

        private void btnTinhROP_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable kq = AiHelper.ChayThuTuc("sp_TinhMucPhaiDatHang",
                    AiHelper.P("@SoNgayPhanTich", (int)numLichSu.Value),
                    AiHelper.P("@LeadTimeNgay", (int)numLeadTime.Value),
                    AiHelper.PDec("@HeSoZ", numZ.Value, 4, 2),
                    AiHelper.P("@DenNgay", dtpDenNgay.Value.Date));
                AiHelper.BaoTin("Đã tính mức phải đặt hàng cho " + kq.Rows.Count + " sản phẩm (cùng cách tính với Bảng thông báo, phần Kho hàng).\n\n" +
                                "Số ngày hàng về lấy từ phiếu nhập: ngày nhập - ngày đặt hàng, tính trung bình theo nhà cung cấp nhập gần nhất.\n" +
                                "Nhà cung cấp chưa có phiếu nào ghi ngày đặt hàng thì tạm dùng " + (int)numLeadTime.Value + " ngày (số trong ô).");
                if (cboSanPham.SelectedValue != null) btnXem_Click(sender, e);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void pnlBieuDo_Paint(object sender, PaintEventArgs e)
        {
            AiHelper.VeDuong(e.Graphics, pnlBieuDo.ClientRectangle, _nhanX, _thuc, _duBao, _tieuDe);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
