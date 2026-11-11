using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    public partial class Form_BaoCaoSanPhamBanChay : Form
    {
        public Form_BaoCaoSanPhamBanChay()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void KhoiTaoChucNang()
        {
            AiHelper.DinhDangLuoi(dgvMuaKem);
            cboAbc.SelectedIndex = 0;
            cboAbc.SelectedIndexChanged += (s, e) => ApDungLocAbc();
            dgvBaoCaoSanPhamBanChay.SelectionChanged += (s, e) => NapMuaKem();
        }

        private void btnMuaKem_Click(object sender, EventArgs e)
        {
            try
            {
                AiHelper.ChayThuTuc("sp_TimMonHayMuaChung");
                NapMuaKem();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void ThemCotAbc(DataTable dt, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                DataTable abc = AiHelper.ChayThuTuc("sp_ChiaNhomBanChay",
                    AiHelper.P("@TuNgay", tuNgay), AiHelper.P("@DenNgay", denNgay), AiHelper.P("@TieuChi", "SanLuong"));
                Dictionary<string, string> tra = new Dictionary<string, string>();
                foreach (DataRow r in abc.Rows) tra[Convert.ToString(r["MaSP"])] = Convert.ToString(r["PhanLoai"]);
                dt.Columns.Add("Nhóm Bán Chạy (A/B/C)", typeof(string));
                foreach (DataRow r in dt.Rows)
                {
                    string ma = Convert.ToString(r["Mã Sản Phẩm"]);
                    r["Nhóm Bán Chạy (A/B/C)"] = tra.ContainsKey(ma) ? tra[ma] : "";
                }
            }
            catch (Exception ex)
            {
                if (lblGhi != null) lblGhi.Text = "Chưa chia được nhóm A/B/C (A: món làm ra 80% số bán, B: tới 95%, C: còn lại): " + AiHelper.LoiDeHieu(ex);
            }
        }

        private void ApDungLocAbc()
        {
            DataTable dt = dgvBaoCaoSanPhamBanChay.DataSource as DataTable;
            if (dt == null || !dt.Columns.Contains("Nhóm Bán Chạy (A/B/C)")) return;
            dt.DefaultView.RowFilter = cboAbc.SelectedIndex <= 0 ? "" : "[Nhóm Bán Chạy (A/B/C)] = '" + cboAbc.SelectedItem + "'";
        }

        private void NapMuaKem()
        {
            if (dgvMuaKem == null) return;
            if (dgvBaoCaoSanPhamBanChay.CurrentRow == null || !dgvBaoCaoSanPhamBanChay.Columns.Contains("Mã Sản Phẩm")) { dgvMuaKem.DataSource = null; return; }
            string ma = Convert.ToString(dgvBaoCaoSanPhamBanChay.CurrentRow.Cells["Mã Sản Phẩm"].Value);
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT sp.MaSP AS [Mã SP Mua Kèm], sp.TenSP AS [Tên SP Mua Kèm], " +
                    "       CAST(100 * d.ChiSo_Confidence AS DECIMAL(6,1)) AS [Khách Mua SP Này Cũng Mua (%)], " +
                    "       CAST(d.ChiSo_Lift AS DECIMAL(8,2)) AS [Lift], CAST(100 * d.ChiSo_Support AS DECIMAL(6,2)) AS [Support (%)] " +
                    "FROM DeXuat_ComboKhuyenMai d JOIN SanPham sp ON sp.MaSP = d.SanPhamKemId " +
                    "WHERE d.SanPhamChinhId = @m ORDER BY d.ChiSo_Lift DESC, d.ChiSo_Confidence DESC", AiHelper.P("@m", ma));
                dgvMuaKem.DataSource = t;
                if (t.Rows.Count > 0)
                    lblGhi.Text = "Sản phẩm " + ma + " thường được mua kèm với các sản phẩm bên dưới. Chỉ lấy cặp có ở ít nhất 1% số đơn, ít nhất 30% khách mua món này có mua kèm, và mua chung nhiều hơn bình thường.";
                else
                {
                    object tong = AiHelper.LayGiaTri("SELECT COUNT(*) FROM DeXuat_ComboKhuyenMai");
                    lblGhi.Text = Convert.ToInt32(tong ?? 0) == 0
                        ? "Chưa tìm món hay mua chung. Bấm nút \"Tìm món hay mua chung\" ở trên (danh sách cũ sẽ được thay bằng danh sách mới)."
                        : "Không có cặp mua kèm nào của sản phẩm " + ma + " đạt ngưỡng (đơn mua ít nên khó có cặp lặp lại).";
                }
            }
            catch (Exception ex) { lblGhi.Text = "Chưa đọc được gợi ý mua kèm: " + AiHelper.LoiDeHieu(ex); }
        }

        private void Form_BaoCaoSanPhamBanChay_Load(object sender, EventArgs e)
        {
            txtChonTop.Text = "10";

            LoadDanhSachTopSanPham();
        }

        private void btnXemDanhSach_Click(object sender, EventArgs e)
        {
            LoadDanhSachTopSanPham();
        }

        private void LoadDanhSachTopSanPham()
        {
            if (!int.TryParse(txtChonTop.Text.Trim(), out int topN) || topN <= 0)
            {
                topN = 10;
                txtChonTop.Text = "10";
            }

            DateTime denNgay = DateTime.Today;
            try
            {
                object moiNhat = AiHelper.LayGiaTri("SELECT MAX(NgayDat) FROM DonHang");
                if (moiNhat != null && moiNhat != DBNull.Value) denNgay = Convert.ToDateTime(moiNhat).Date;
            }
            catch { }
            DateTime tuNgay = denNgay.AddDays(-364);

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = $@"
                        SELECT TOP ({topN})
                            MaSP AS [Mã Sản Phẩm],
                            TenSP AS [Tên Sản Phẩm],
                            TongSoLuongBan AS [Tổng Số Lượng Bán],
                            DoanhThu AS [Doanh Thu]
                        FROM dbo.fn_TopSanPhamBanChay(@TuNgay, @DenNgay)
                        ORDER BY TongSoLuongBan DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        ThemCotAbc(dt, tuNgay, denNgay);

                        dgvBaoCaoSanPhamBanChay.DataSource = dt;

                        dgvBaoCaoSanPhamBanChay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvBaoCaoSanPhamBanChay.ReadOnly = true;
                        dgvBaoCaoSanPhamBanChay.AllowUserToAddRows = false;
                        ApDungLocAbc();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải báo cáo: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblChonTop_Click(object sender, EventArgs e) { }
        private void txtChonTop_TextChanged(object sender, EventArgs e) { }
        private void dgvBaoCaoSanPhamBanChay_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}