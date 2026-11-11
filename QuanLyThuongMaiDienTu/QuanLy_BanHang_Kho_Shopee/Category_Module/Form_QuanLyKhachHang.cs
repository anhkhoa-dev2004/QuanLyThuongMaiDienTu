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

namespace QuanLy_BanHang_Kho_Shopee.Category_Module
{
    public partial class Form_QuanLyKhachHang : Form
    {
        public Form_QuanLyKhachHang()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void Form_QuanLyKhachHang_Load(object sender, EventArgs e)
        {
            LoadDataKhachHang();
        }

        private void LoadDataKhachHang()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaKH AS [Mã Khách Hàng],
                            TenKH AS [Tên Khách Hàng],
                            ISNULL(SoDT, '') AS [Điện Thoại],
                            ISNULL(DiaChi, '') AS [Địa Chỉ],
                            Nhom_RFM AS [Nhóm Khách],
                            XacSuatRoiBo AS [Nguy Cơ Rời Bỏ],
                            CASE WHEN DanhSachDen_Blacklist = 1 THEN N'Có' ELSE N'' END AS [Danh Sách Đen]
                        FROM KhachHang WITH (NOLOCK)";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvKhachHang.DataSource = dt;
                    dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    DinhDangCotAi();
                    dgvKhachHang.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu Khách hàng: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvKhachHang.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKhachHang.Text = row.Cells[0].Value?.ToString();
                txtTenKhachHang.Text = row.Cells[1].Value?.ToString();
                txtSoDienThoai.Text = row.Cells[2].Value?.ToString();
                txtDiaChi.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvKhachHang_CellClick(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKhachHang.Text.Trim();
            string tenKH = txtTenKhachHang.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã KH và Tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaKH", maKH);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show($"Mã khách hàng [{maKH}] đã tồn tại!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO KhachHang (MaKH, TenKH, SoDT, DiaChi)
                        VALUES (@MaKH, @TenKH, @SoDT, @DiaChi)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaKH", maKH);
                        cmdInsert.Parameters.AddWithValue("@TenKH", tenKH);
                        cmdInsert.Parameters.AddWithValue("@SoDT", sdt);
                        cmdInsert.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm Khách hàng mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataKhachHang();
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm Khách hàng: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKhachHang.Text.Trim();
            string tenKH = txtTenKhachHang.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng chọn Khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string updateQuery = @"
                        UPDATE KhachHang
                        SET TenKH = @TenKH, SoDT = @SoDT, DiaChi = @DiaChi
                        WHERE MaKH = @MaKH";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaKH", maKH);
                        cmdUpdate.Parameters.AddWithValue("@TenKH", tenKH);
                        cmdUpdate.Parameters.AddWithValue("@SoDT", sdt);
                        cmdUpdate.Parameters.AddWithValue("@DiaChi", diaChi);
                        int affected = cmdUpdate.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin Khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataKhachHang();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã KH để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa Khách hàng: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKhachHang.Text.Trim();

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng chọn Khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Khách hàng [{maKH}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MaKH", maKH);
                        int affected = cmdDelete.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Xóa Khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataKhachHang();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã KH để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Khách hàng (Có thể khách hàng đã có đơn hàng liên kết): " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetFields()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaKhachHang_Click(object sender, EventArgs e) { }
        private void txtMaKhachHang_TextChanged(object sender, EventArgs e) { }
        private void lblDienThoai_Click(object sender, EventArgs e) { }
        private void txtSoDienThoai_TextChanged(object sender, EventArgs e) { }
        private void lblTenKhachHang_Click(object sender, EventArgs e) { }
        private void txtTenKhachHang_TextChanged(object sender, EventArgs e) { }
        private void lblDiaChi_Click(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }

        private void KhoiTaoChucNang()
        {
            dgvKhachHang.SelectionChanged += (s, e) => CapNhatChiTietAi();
        }

        private void btnPhanHangRFM_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = AiHelper.NgayDuLieuMoiNhat();
                AiHelper.ChayThuTuc("sp_ChiaNhomKhachHang", AiHelper.P("@NgayTinh", ngay));
                LoadDataKhachHang();
                MessageBox.Show("Đã chia nhóm khách tính đến ngày " + ngay.ToString("dd/MM/yyyy") + ".", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnNguyCoRoiBo_Click(object sender, EventArgs e)
        {
            try { AiHelper.ChayThuTuc("sp_UocLuongNguyCoRoiBo", AiHelper.P("@NgayTinh", AiHelper.NgayDuLieuMoiNhat())); LoadDataKhachHang(); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnDanhSachDen_Click(object sender, EventArgs e)
        {
            DoiDanhSachDen();
        }

        private void DinhDangCotAi()
        {
            if (dgvKhachHang.Columns.Contains("Nguy Cơ Rời Bỏ")) dgvKhachHang.Columns["Nguy Cơ Rời Bỏ"].DefaultCellStyle.Format = "P0";
            if (!dgvKhachHang.Columns.Contains("Danh Sách Đen")) return;
            foreach (DataGridViewRow dong in dgvKhachHang.Rows)
                if (Convert.ToString(dong.Cells["Danh Sách Đen"].Value) == "Có") dong.DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 205);
        }

        private void CapNhatChiTietAi()
        {
            if (lblChiTiet == null || dgvKhachHang.CurrentRow == null || !dgvKhachHang.Columns.Contains("Nhóm Khách")) return;
            DataGridViewRow r = dgvKhachHang.CurrentRow;
            string hang = Convert.ToString(r.Cells["Nhóm Khách"].Value);
            object nc = r.Cells["Nguy Cơ Rời Bỏ"].Value;
            string den = Convert.ToString(r.Cells["Danh Sách Đen"].Value);
            lblChiTiet.Text = "Khách " + Convert.ToString(r.Cells[0].Value) + ": " + (hang.Length == 0 ? "chưa chia nhóm" : hang) +
                          "; nguy cơ rời bỏ " + (nc == null || nc == DBNull.Value ? "chưa đủ lịch sử" : Convert.ToDecimal(nc).ToString("P0")) +
                          "; " + (den == "Có" ? "ĐANG TRONG DANH SÁCH ĐEN (form Lập đơn hàng sẽ hỏi lại khi chốt đơn)" : "không nằm trong danh sách đen") + ".";
        }

        private void DoiDanhSachDen()
        {
            if (dgvKhachHang.CurrentRow == null || dgvKhachHang.CurrentRow.Cells[0].Value == null) { MessageBox.Show("Hãy chọn một khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string ma = Convert.ToString(dgvKhachHang.CurrentRow.Cells[0].Value);
            try
            {
                AiHelper.Chay("UPDATE KhachHang SET DanhSachDen_Blacklist = CASE WHEN ISNULL(DanhSachDen_Blacklist, 0) = 1 THEN 0 ELSE 1 END WHERE MaKH = @m", AiHelper.P("@m", ma));
                LoadDataKhachHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}