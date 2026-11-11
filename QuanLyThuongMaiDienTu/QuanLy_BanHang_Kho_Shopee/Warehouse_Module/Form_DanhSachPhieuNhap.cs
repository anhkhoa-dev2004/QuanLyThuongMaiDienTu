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

namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    public partial class Form_DanhSachPhieuNhap : Form
    {
        public Form_DanhSachPhieuNhap()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private object NgayDatHangDaChon(DateTime ngayNhap, out string loi)
        {
            loi = null;
            if (dtpNgayDatHang == null || !dtpNgayDatHang.Checked) return DBNull.Value;
            if (dtpNgayDatHang.Value.Date > ngayNhap.Date)
            {
                loi = "Ngày đặt hàng không được sau ngày nhập kho.";
                return DBNull.Value;
            }
            return dtpNgayDatHang.Value.Date;
        }

        private void Form_DanhSachPhieuNhap_Load(object sender, EventArgs e)
        {
            txtNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTongTien.Text = "0";
            txtMaNhanVien.Text = Session.MaND;
            txtTongTien.ReadOnly = true;

            LoadDanhSachPhieuNhap();
        }

        private void LoadDanhSachPhieuNhap()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT
                            pn.MaPN AS [Mã Phiếu Nhập],
                            pn.MaNCC AS [Mã Đối Tác/NCC],
                            ncc.TenNCC AS [Tên Nhà Cung Cấp],
                            pn.MaKho AS [Mã Kho],
                            pn.NgayNhap AS [Ngày Nhập],
                            pn.NgayDatHang AS [Ngày Đặt Hàng],
                            DATEDIFF(DAY, pn.NgayDatHang, pn.NgayNhap) AS [Số Ngày Hàng Về],
                            pn.TongTien AS [Tổng Tiền]
                        FROM PhieuNhap pn WITH (NOLOCK)
                        LEFT JOIN NhaCungCap ncc WITH (NOLOCK) ON pn.MaNCC = ncc.MaNCC
                        ORDER BY pn.NgayNhap DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvDanhSachPhieuNhap.DataSource = dt;
                        dgvDanhSachPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDanhSachPhieuNhap.ReadOnly = true;
                        dgvDanhSachPhieuNhap.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách phiếu nhập: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPhieuNhap.Text.Trim();
            string maNCC = txtMaDoiTac.Text.Trim();
            string ngayNhapStr = txtNgayNhap.Text.Trim();

            if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã phiếu nhập và Mã đối tác (NCC)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParseExact(ngayNhapStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayNhap))
            {
                ngayNhap = DateTime.Now;
            }
            object ngayDat = NgayDatHangDaChon(ngayNhap, out string loiNgay);
            if (loiNgay != null)
            {
                MessageBox.Show(loiNgay, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    using (SqlCommand checkNcc = new SqlCommand("SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC", conn))
                    {
                        checkNcc.Parameters.AddWithValue("@MaNCC", maNCC);
                        if ((int)checkNcc.ExecuteScalar() == 0)
                        {
                            MessageBox.Show($"Mã Nhà cung cấp [{maNCC}] không có trong hệ thống. Xem danh sách ở màn hình Quản lý Nhà cung cấp.", "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string checkQuery = "SELECT COUNT(*) FROM PhieuNhap WHERE MaPN = @MaPN";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaPN", maPN);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Mã phiếu nhập này đã tồn tại trên hệ thống!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO PhieuNhap (MaPN, NgayNhap, NgayDatHang, MaNCC, MaKho, TongTien)
                                         VALUES (@MaPN, @NgayNhap, @NgayDatHang, @MaNCC, 'KHO01', 0)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.Parameters.AddWithValue("@NgayNhap", ngayNhap);
                        cmd.Parameters.Add("@NgayDatHang", SqlDbType.DateTime).Value = ngayDat;
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Thêm mới phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachPhieuNhap();
                            XoaTrangForm();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm phiếu nhập: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPhieuNhap.Text.Trim();
            string maNCC = txtMaDoiTac.Text.Trim();
            string ngayNhapStr = txtNgayNhap.Text.Trim();

            if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập Mã phiếu nhập và Mã đối tác (NCC)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParseExact(ngayNhapStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayNhap))
            {
                ngayNhap = DateTime.Now;
            }
            object ngayDat = NgayDatHangDaChon(ngayNhap, out string loiNgay);
            if (loiNgay != null)
            {
                MessageBox.Show(loiNgay, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkNccQuery = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand checkCmd = new SqlCommand(checkNccQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show($"Mã Nhà cung cấp [{maNCC}] không có trong hệ thống. Xem danh sách ở màn hình Quản lý Nhà cung cấp.", "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string updateQuery = @"UPDATE PhieuNhap
                                 SET MaNCC = @MaNCC, NgayNhap = @NgayNhap, NgayDatHang = @NgayDatHang
                                 WHERE MaPN = @MaPN";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.Parameters.AddWithValue("@NgayNhap", ngayNhap);
                        cmd.Parameters.Add("@NgayDatHang", SqlDbType.DateTime).Value = ngayDat;

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachPhieuNhap();
                            XoaTrangForm();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa phiếu nhập: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPhieuNhap.Text.Trim();

            if (string.IsNullOrEmpty(maPN))
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa trên bảng hoặc nhập Mã phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Phiếu nhập [{maPN}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM PhieuNhap WHERE MaPN = @MaPN";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachPhieuNhap();
                            XoaTrangForm();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phiếu nhập (Có thể do phiếu đã có chi tiết nhập hàng): " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvDanhSachPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachPhieuNhap.Rows[e.RowIndex];
                txtMaPhieuNhap.Text = row.Cells["Mã Phiếu Nhập"].Value?.ToString();
                txtMaDoiTac.Text = row.Cells["Mã Đối Tác/NCC"].Value?.ToString();
                txtTongTien.Text = string.Format("{0:N0}", row.Cells["Tổng Tiền"].Value ?? 0);

                if (DateTime.TryParse(row.Cells["Ngày Nhập"].Value?.ToString(), out DateTime dt))
                {
                    txtNgayNhap.Text = dt.ToString("dd/MM/yyyy");
                }
                object nd = row.Cells["Ngày Đặt Hàng"].Value;
                if (dtpNgayDatHang != null)
                {
                    if (nd == null || nd == DBNull.Value) dtpNgayDatHang.Checked = false;
                    else { dtpNgayDatHang.Value = Convert.ToDateTime(nd); dtpNgayDatHang.Checked = true; }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XoaTrangForm()
        {
            txtMaPhieuNhap.Clear();
            txtMaDoiTac.Clear();
            txtTongTien.Text = "0";
            txtNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (dtpNgayDatHang != null) dtpNgayDatHang.Checked = false;
            txtMaPhieuNhap.Focus();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaPhieuNhap_Click(object sender, EventArgs e) { }
        private void txtMaPhieuNhap_TextChanged(object sender, EventArgs e) { }
        private void lblMaDoiTac_Click(object sender, EventArgs e) { }
        private void txtMaDoiTac_TextChanged(object sender, EventArgs e) { }
        private void lblTongTien_Click(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
        private void lblMaNhanVien_Click(object sender, EventArgs e) { }
        private void txtMaNhanVien_TextChanged(object sender, EventArgs e) { }
        private void lblNgayNhap_Click(object sender, EventArgs e) { }
        private void txtNgayNhap_TextChanged(object sender, EventArgs e) { }
        private void dgvDanhSachPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}