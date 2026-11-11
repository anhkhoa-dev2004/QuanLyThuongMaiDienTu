using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_ChamDiemSanPham : Form
    {
        private static readonly string[] CHI_SO = { "GiaBan", "SoLuongBan", "DoanhThu", "TonKho", "SoLuongNhap", "SoLuongTra" };
        private List<double> _x = new List<double>(), _y = new List<double>();
        private List<string> _nhan = new List<string>();
        private string _tieuDe = "";

        public Form_ChamDiemSanPham()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            AiHelper.BatDoubleBuffer(pnlBieuDo);
        }

        private void Form_ChamDiemSanPham_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvDiem);
            AiHelper.DinhDangLuoi(dgvTai);
            TaiKPI();
        }

        private static string Gt(string sql, string dinhDang)
        {
            object o = AiHelper.LayGiaTri(sql);
            return o == null ? "-" : Convert.ToDecimal(o).ToString(dinhDang);
        }

        private void TaiKPI()
        {
            try
            {
                lblKpi1.Text = "Tổng doanh thu (hóa đơn)\n" + Gt("SELECT SUM(CASE WHEN LoaiHoaDon = N'Điều chỉnh giảm' THEN -TongTien ELSE TongTien END) FROM HoaDon", "N0") + " đ";
                lblKpi2.Text = "Số đơn hàng\n" + Gt("SELECT COUNT(*) FROM DonHang", "N0");
                lblKpi3.Text = "Số khách hàng\n" + Gt("SELECT COUNT(*) FROM KhachHang", "N0");
                lblKpi4.Text = "Tổng tồn kho\n" + Gt("SELECT SUM(SoLuongTon) FROM TonKho", "N0");
                lblKpi5.Text = "SP dưới ngưỡng an toàn\n" + Gt("SELECT COUNT(*) FROM SanPham sp WHERE ISNULL((SELECT SUM(SoLuongTon) FROM TonKho t WHERE t.MaSP = sp.MaSP), 0) < sp.NguongAnToan", "N0");
                lblKpi6.Text = "Tỷ lệ trả hàng\n" + Gt("SELECT 100.0 * (SELECT SUM(SoLuong) FROM TraHang) / NULLIF((SELECT SUM(SoLuong) FROM ChiTietPhieuXuat) - ISNULL((SELECT SUM(SoLuong) FROM HangHoanVeKho), 0), 0)", "N2") + " %";
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT sp.MaSP, sp.TenSP, CAST(sp.GiaBan AS FLOAT) AS GiaBan, " +
                    " CAST(ISNULL((SELECT SUM(ct.SoLuong) FROM ChiTietDonHang ct JOIN DonHang dh ON dh.MaDH = ct.MaDH WHERE ct.MaSP = sp.MaSP AND ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%'), 0) AS FLOAT) AS SoLuongBan, " +
                    " CAST(ISNULL((SELECT SUM(ct.SoLuong * ct.DonGia) FROM ChiTietDonHang ct JOIN DonHang dh ON dh.MaDH = ct.MaDH WHERE ct.MaSP = sp.MaSP AND ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%'), 0) AS FLOAT) AS DoanhThu, " +
                    " CAST(ISNULL((SELECT SUM(SoLuongTon) FROM TonKho t WHERE t.MaSP = sp.MaSP), 0) AS FLOAT) AS TonKho, " +
                    " CAST(ISNULL((SELECT SUM(SoLuong) FROM ChiTietPhieuNhap n WHERE n.MaSP = sp.MaSP), 0) AS FLOAT) AS SoLuongNhap, " +
                    " CAST(ISNULL((SELECT SUM(SoLuong) FROM TraHang th WHERE th.MaSP = sp.MaSP), 0) AS FLOAT) AS SoLuongTra " +
                    "FROM SanPham sp ORDER BY sp.MaSP");
                int n = dt.Rows.Count, p = CHI_SO.Length;
                if (n < 3) { AiHelper.BaoTin("Cần ít nhất 3 sản phẩm để chấm điểm."); return; }

                double[,] x = new double[n, p];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < p; j++) x[i, j] = Convert.ToDouble(dt.Rows[i][CHI_SO[j]]);

                ChamDiemHelper.KetQua kq = ChamDiemHelper.Tinh(x);

                DataTable diem = new DataTable();
                diem.Columns.Add("MaSP", typeof(string)); diem.Columns.Add("TenSP", typeof(string));
                diem.Columns.Add("PC1", typeof(double)); diem.Columns.Add("PC2", typeof(double));
                _x.Clear(); _y.Clear(); _nhan.Clear();
                for (int i = 0; i < n; i++)
                {
                    double a = Math.Round(kq.Diem[i, 0], 3), b = p > 1 ? Math.Round(kq.Diem[i, 1], 3) : 0;
                    diem.Rows.Add(dt.Rows[i]["MaSP"], dt.Rows[i]["TenSP"], a, b);
                    _x.Add(a); _y.Add(b); _nhan.Add(Convert.ToString(dt.Rows[i]["MaSP"]));
                }
                dgvDiem.DataSource = diem;

                DataTable tai = new DataTable();
                tai.Columns.Add("ChiSo", typeof(string)); tai.Columns.Add("PC1", typeof(double)); tai.Columns.Add("PC2", typeof(double));
                for (int j = 0; j < p; j++) tai.Rows.Add(CHI_SO[j], Math.Round(kq.VectorRieng[j, 0], 3), p > 1 ? Math.Round(kq.VectorRieng[j, 1], 3) : 0);
                dgvTai.DataSource = tai;

                double p1 = kq.TyLePhuongSai[0] * 100, p2 = p > 1 ? kq.TyLePhuongSai[1] * 100 : 0;
                lblPhuongSai.Text = "Trục ngang giữ " + p1.ToString("0.0") + "% thông tin, trục dọc giữ " + p2.ToString("0.0") + "%  →  2 trục giữ " + (p1 + p2).ToString("0.0") + "% (bảng dưới: con số nào góp vào trục nào nhiều)";
                _tieuDe = "Sản phẩm trên 2 trục điểm (gần nhau = giống nhau)";
                pnlBieuDo.Invalidate();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void pnlBieuDo_Paint(object sender, PaintEventArgs e)
        {
            AiHelper.VeDiem(e.Graphics, pnlBieuDo.ClientRectangle, _x, _y, _nhan, _tieuDe, "Trục ngang", "Trục dọc");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
