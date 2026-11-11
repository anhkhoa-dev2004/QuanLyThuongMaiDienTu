using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    public partial class Form_BaoCaoXuatNhapTon : Form
    {
        public Form_BaoCaoXuatNhapTon()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void Form_BaoCaoXuatNhapTon_Load(object sender, EventArgs e)
        {
            DateTime den = DateTime.Today;
            try { den = AiHelper.NgayDuLieuMoiNhat(); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
            txtTuNgay.Text = den.AddYears(-1).AddDays(1).ToString("dd/MM/yyyy");
            txtDenNgay.Text = den.ToString("dd/MM/yyyy");

            LoadBaoCaoXuatNhapTon();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            LoadBaoCaoXuatNhapTon();
        }

        private void LoadBaoCaoXuatNhapTon()
        {
            string tuNgayStr = txtTuNgay.Text.Trim();
            string denNgayStr = txtDenNgay.Text.Trim();

            if (!DateTime.TryParseExact(tuNgayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tuNgay))
            {
                MessageBox.Show("Từ ngày không đúng định dạng (dd/MM/yyyy). Ví dụ: 01/01/2026", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParseExact(denNgayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime denNgay))
            {
                MessageBox.Show("Đến ngày không đúng định dạng (dd/MM/yyyy). Ví dụ: 31/12/2026", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày phải nhỏ hơn hoặc bằng Đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaSP AS [Mã Sản Phẩm],
                            TenSP AS [Tên Sản Phẩm],
                            TongNhap AS [Tổng Nhập],
                            TongXuat AS [Tổng Xuất],
                            HangHoanVe AS [Hàng Hoàn Về (Đơn Hủy)],
                            KhachTraNhapLai AS [Khách Trả Còn Bán Được],
                            ChenhLechKiemKe AS [Lệch Khi Kiểm Kê],
                            TonCuoiKy AS [Tồn Hiện Tại]
                        FROM dbo.fn_BaoCaoXuatNhapTon(@TuNgay, @DenNgay)
                        ORDER BY MaSP ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvBaoCaoXuatNhapTon.DataSource = dt;

                        dgvBaoCaoXuatNhapTon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvBaoCaoXuatNhapTon.ReadOnly = true;
                        dgvBaoCaoXuatNhapTon.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải báo cáo Xuất-Nhập-Tồn: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblTuNgay_Click(object sender, EventArgs e) { }
        private void txtTuNgay_TextChanged(object sender, EventArgs e) { }
        private void lblDenNgay_Click(object sender, EventArgs e) { }
        private void txtDenNgay_TextChanged(object sender, EventArgs e) { }
        private void dgvBaoCaoXuatNhapTon_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void KhoiTaoChucNang()
        {
            cboPhuongPhap.SelectedIndex = 0;
        }

        private void btnDuBao_Click(object sender, EventArgs e)
        {
            DuBaoSanPhamDangChon();
        }

        private void DuBaoSanPhamDangChon()
        {
            if (dgvBaoCaoXuatNhapTon.CurrentRow == null || !dgvBaoCaoXuatNhapTon.Columns.Contains("Mã Sản Phẩm"))
            {
                lblDuBao.Text = "Hãy bấm Xem báo cáo và chọn một sản phẩm trong bảng trước.";
                return;
            }
            string ma = Convert.ToString(dgvBaoCaoXuatNhapTon.CurrentRow.Cells["Mã Sản Phẩm"].Value);
            DateTime den;
            if (!DateTime.TryParseExact(txtDenNgay.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out den)) den = DateTime.Today;
            DateTime tu = den.AddDays(-59);
            try
            {
                double[] chuoi = DuLieuAi.XuatTheoNgay(ma, tu, den);
                int ngayCoXuat = chuoi.Count(v => v > 0);
                if (ngayCoXuat == 0)
                {
                    lblDuBao.Text = "Sản phẩm " + ma + " không có phiếu xuất nào trong 60 ngày tính đến " + den.ToString("dd/MM/yyyy") + ", chưa thể dự báo.";
                    return;
                }
                string loi;
                double[] kq = cboPhuongPhap.SelectedIndex == 0
                    ? DuBaoTheoXuHuong.DuBao(chuoi, 7, out loi)
                    : DuBaoTheoThuTrongTuan.DuBao(chuoi, tu, 7, 1.0, out loi);
                if (kq == null) { lblDuBao.Text = "Chưa dự báo được: " + loi; return; }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < kq.Length; i++) sb.Append(den.AddDays(i + 1).ToString("dd/MM") + ": " + kq[i].ToString("0.0") + (i < kq.Length - 1 ? " | " : ""));
                double tong = kq.Sum();
                string ton = Convert.ToString(dgvBaoCaoXuatNhapTon.CurrentRow.Cells["Tồn Hiện Tại"].Value);
                string dua = "";
                double tonSo;
                if (tong > 0 && double.TryParse(ton, out tonSo)) dua = "\nTồn hiện tại " + ton + " đủ bán khoảng " + (tonSo / (tong / 7)).ToString("0.0") + " ngày nếu bán đều như dự báo.";
                lblDuBao.Text = "Dự báo xuất của " + ma + " (" + cboPhuongPhap.SelectedItem + "): " + sb + "\nTổng 7 ngày: " + tong.ToString("0.0") + dua +
                                 (ngayCoXuat < 10 ? "\nLưu ý: chỉ " + ngayCoXuat + " trong 60 ngày có xuất hàng nên dự báo kém tin cậy." : "");
            }
            catch (Exception ex) { lblDuBao.Text = "Lỗi khi dự báo: " + AiHelper.LoiDeHieu(ex); }
        }
    }
}