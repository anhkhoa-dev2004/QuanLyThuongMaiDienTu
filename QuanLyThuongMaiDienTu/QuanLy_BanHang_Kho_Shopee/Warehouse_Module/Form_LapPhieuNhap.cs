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

namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    public partial class Form_LapPhieuNhap : Form
    {
        private int _soGoiY;

        public Form_LapPhieuNhap()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void KhoiTaoChucNang()
        {
            lblCauHinhEoq.Text = "Số gợi ý = đủ bán 30 ngày sau khi hàng về, tính lại mỗi lần có đơn hoặc phiếu mới.";
        }

        private void btnDienGoiY_Click(object sender, EventArgs e)
        {
            if (_soGoiY > 0) txtSoLuong.Text = _soGoiY.ToString();
            else MessageBox.Show("Chưa có số lượng gợi ý (chưa nhập mã sản phẩm hợp lệ, hoặc sản phẩm chưa cần nhập).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static bool CoMa(SqlConnection conn, string sqlDem, string ma)
        {
            using (SqlCommand cmd = new SqlCommand(sqlDem, conn))
            {
                cmd.Parameters.AddWithValue("@m", ma);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private static string DanhSachMa(SqlConnection conn, string sql)
        {
            List<string> ds = new List<string>();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read()) ds.Add(Convert.ToString(rd[0]));
            }
            return string.Join(", ", ds);
        }

        private void CapNhatGoiY()
        {
            if (lblGoiY == null) return;
            _soGoiY = 0;
            string ma = txtMaSanPham.Text.Trim();
            if (ma.Length == 0) { lblGoiY.Text = "Nhập Mã sản phẩm để xem gợi ý số lượng nhập."; return; }
            try
            {
                DuLieuAi.GoiYNhap g = DuLieuAi.TinhGoiYNhapTheoSP(ma);
                _soGoiY = g.GoiY;
                lblGoiY.Text = g.GiaiThich + (g.GoiY > 0 ? "\nGợi ý nhập: " + g.GoiY + " cái (đủ bán 30 ngày sau khi hàng về)." : "");
            }
            catch (Exception ex) { lblGoiY.Text = "Chưa tính được gợi ý: " + AiHelper.LoiDeHieu(ex); }
        }

        private void Form_LapPhieuNhap_Load(object sender, EventArgs e)
        {
            try { txtMaPhieuNhap.Text = AiHelper.LayMaMoi("PN"); }
            catch (Exception ex) { txtMaPhieuNhap.Text = ""; MessageBox.Show("Chưa lấy được mã phiếu nhập mới: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            txtChonKho.Text = "";
            txtNhaCungCap.Text = "";
            txtSoLuong.Text = "";
            txtGia.Text = "";

            LoadChiTietPhieuNhap();
        }

        private void LoadChiTietPhieuNhap()
        {
            string maPN = txtMaPhieuNhap.Text.Trim();

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            ct.MaPN AS [Mã Phiếu Nhập],
                            ct.MaSP AS [Mã Sản Phẩm],
                            sp.TenSP AS [Tên Sản Phẩm],
                            ct.SoLuong AS [Số Lượng],
                            ct.DonGia AS [Đơn Giá Nhập],
                            ct.ThanhTien AS [Thành Tiền]
                        FROM ChiTietPhieuNhap ct WITH (NOLOCK)
                        JOIN SanPham sp WITH (NOLOCK) ON ct.MaSP = sp.MaSP
                        WHERE ct.MaPN = @MaPN OR @MaPN = ''";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvChiTietPhieuNhap.DataSource = dt;
                        dgvChiTietPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvChiTietPhieuNhap.ReadOnly = true;
                        dgvChiTietPhieuNhap.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi tiết phiếu nhập: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPhieuNhap.Text.Trim();
            string maKho = txtChonKho.Text.Trim();
            string maNCC = txtNhaCungCap.Text.Trim();
            string maSP = txtMaSanPham.Text.Trim();

            if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã phiếu nhập và Mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương lớn hơn 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkPN = "SELECT COUNT(*) FROM PhieuNhap WHERE MaPN = @MaPN";
                    using (SqlCommand cmdCheck = new SqlCommand(checkPN, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaPN", maPN);
                        if ((int)cmdCheck.ExecuteScalar() == 0)
                        {
                            if (maNCC.Length == 0 || !CoMa(conn, "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @m", maNCC))
                            {
                                MessageBox.Show("Phiếu nhập mới cần Mã nhà cung cấp đúng.\nCác mã đang có: " +
                                                DanhSachMa(conn, "SELECT MaNCC FROM NhaCungCap ORDER BY MaNCC"),
                                                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (maKho.Length == 0 || !CoMa(conn, "SELECT COUNT(*) FROM Kho WHERE MaKho = @m", maKho))
                            {
                                MessageBox.Show("Phiếu nhập mới cần Mã kho đúng.\nCác mã đang có: " +
                                                DanhSachMa(conn, "SELECT MaKho FROM Kho ORDER BY MaKho"),
                                                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            object ngayDat = DBNull.Value;
                            if (dtpNgayDatHang != null && dtpNgayDatHang.Checked)
                            {
                                if (dtpNgayDatHang.Value.Date > DateTime.Today)
                                {
                                    MessageBox.Show("Ngày đặt hàng không được sau hôm nay (ngày nhập kho).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                ngayDat = dtpNgayDatHang.Value.Date;
                            }

                            string insertPN = @"INSERT INTO PhieuNhap (MaPN, NgayNhap, NgayDatHang, MaNCC, MaKho, TongTien)
                                               VALUES (@MaPN, GETDATE(), @NgayDatHang, @MaNCC, @MaKho, 0)";
                            using (SqlCommand cmdPN = new SqlCommand(insertPN, conn))
                            {
                                cmdPN.Parameters.AddWithValue("@MaPN", maPN);
                                cmdPN.Parameters.Add("@NgayDatHang", SqlDbType.DateTime).Value = ngayDat;
                                cmdPN.Parameters.AddWithValue("@MaNCC", maNCC);
                                cmdPN.Parameters.AddWithValue("@MaKho", maKho);
                                cmdPN.ExecuteNonQuery();
                            }
                        }
                    }

                    string insertCT = @"INSERT INTO ChiTietPhieuNhap (MaPN, MaSP, SoLuong, DonGia)
                                       VALUES (@MaPN, @MaSP, @SoLuong, @DonGia)";

                    using (SqlCommand cmdCT = new SqlCommand(insertCT, conn))
                    {
                        cmdCT.Parameters.AddWithValue("@MaPN", maPN);
                        cmdCT.Parameters.AddWithValue("@MaSP", maSP);
                        cmdCT.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdCT.Parameters.AddWithValue("@DonGia", donGia);

                        int rows = cmdCT.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show($"Đã thêm sản phẩm [{maSP}] vào phiếu nhập [{maPN}] thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadChiTietPhieuNhap();
                            txtMaSanPham.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm chi tiết phiếu nhập (Kiểm tra lại Mã SP hoặc Mã PN): " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPhieuNhap.Text.Trim();
            string maSP = txtMaSanPham.Text.Trim();

            if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn Mã phiếu nhập và Mã sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out int soLuong) || soLuong <= 0 ||
                !decimal.TryParse(txtGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Số lượng và Đơn giá không hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string updateCT = @"UPDATE ChiTietPhieuNhap
                                      SET SoLuong = @SoLuong, DonGia = @DonGia
                                      WHERE MaPN = @MaPN AND MaSP = @MaSP";

                    using (SqlCommand cmd = new SqlCommand(updateCT, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật chi tiết món nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadChiTietPhieuNhap();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm trong phiếu nhập này để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa chi tiết phiếu nhập: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPhieuNhap.Text.Trim();
            string maSP = txtMaSanPham.Text.Trim();

            if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trong danh sách bên dưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm [{maSP}] khỏi phiếu nhập [{maPN}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string deleteCT = "DELETE FROM ChiTietPhieuNhap WHERE MaPN = @MaPN AND MaSP = @MaSP";

                    using (SqlCommand cmd = new SqlCommand(deleteCT, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Đã xóa món hàng khỏi phiếu nhập!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadChiTietPhieuNhap();
                            txtMaSanPham.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa chi tiết phiếu nhập: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvChiTietPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChiTietPhieuNhap.Rows[e.RowIndex];
                txtMaPhieuNhap.Text = row.Cells["Mã Phiếu Nhập"].Value?.ToString();
                txtMaSanPham.Text = row.Cells["Mã Sản Phẩm"].Value?.ToString();
                txtSoLuong.Text = row.Cells["Số Lượng"].Value?.ToString();
                txtGia.Text = row.Cells["Đơn Giá Nhập"].Value?.ToString();
            }
        }

        private void txtMaPhieuNhap_TextChanged(object sender, EventArgs e)
        {
            LoadChiTietPhieuNhap();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaPhieuNhap_Click(object sender, EventArgs e) { }
        private void lblChonKho_Click(object sender, EventArgs e) { }
        private void txtChonKho_TextChanged(object sender, EventArgs e) { }
        private void lblSoLuong_Click(object sender, EventArgs e) { }
        private void txtSoLuong_TextChanged(object sender, EventArgs e) { }
        private void lblNhaCungCap_Click(object sender, EventArgs e) { }
        private void txtNhaCungCap_TextChanged(object sender, EventArgs e) { }
        private void lblMaSanPham_Click(object sender, EventArgs e) { }
        private void txtMaSanPham_TextChanged(object sender, EventArgs e)
        {
            CapNhatGoiY();
        }
        private void lblGia_Click(object sender, EventArgs e) { }
        private void txtGia_TextChanged(object sender, EventArgs e) { }
        private void dgvChiTietPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}