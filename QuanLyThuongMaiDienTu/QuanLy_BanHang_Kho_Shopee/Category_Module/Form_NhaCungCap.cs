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
    public partial class Form_NhaCungCap : Form
    {
        public Form_NhaCungCap()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void KhoiTaoChucNang()
        {
            AiHelper.DinhDangLuoi(dgvUyTin);
            NapUyTin(false);
        }

        private void btnChamDiemNCC_Click(object sender, EventArgs e)
        {
            NapUyTin(true);
        }

        private void NapUyTin(bool hienHopThoaiKhiLoi = true)
        {
            try
            {
                DataTable t = AiHelper.ChayThuTuc("sp_DanhGiaUyTinNhaCungCap");
                dgvUyTin.DataSource = t;
                string[,] ten = { { "MaNCC", "Mã NCC" }, { "TenNCC", "Tên Nhà Cung Cấp" }, { "SoPhieuNhap", "Số Phiếu Nhập" }, { "TongSLNhap", "Tổng SL Nhập" },
                                  { "TongSLXuat", "Tổng SL Xuất" }, { "TongSLTra", "Tổng SL Trả" }, { "SoNgayHangVe_TB", "Số Ngày Hàng Về TB" }, { "TyLeTra_PhanTram", "Tỷ Lệ Trả (%)" }, { "DiemUyTin", "Điểm Uy Tín" } };
                for (int i = 0; i < ten.GetLength(0); i++)
                    if (dgvUyTin.Columns.Contains(ten[i, 0])) dgvUyTin.Columns[ten[i, 0]].HeaderText = ten[i, 1];
            }
            catch (Exception ex)
            {
                if (hienHopThoaiKhiLoi) AiHelper.BaoLoi(ex);
                else lblUyTin.Text = "Chưa chấm được điểm: " + AiHelper.LoiDeHieu(ex);
            }
        }

        private void Form_NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDataNhaCungCap();
        }

        private void LoadDataNhaCungCap()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaNCC AS [Mã NCC],
                            TenNCC AS [Tên Nhà Cung Cấp],
                            ISNULL(SoDT, '') AS [Số Điện Thoại],
                            ISNULL(DiaChi, '') AS [Địa Chỉ]
                        FROM NhaCungCap WITH (NOLOCK)";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvNhaCungCap.DataSource = dt;
                    dgvNhaCungCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvNhaCungCap.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu Nhà Cung Cấp: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvNhaCungCap.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                txtMaNhaCungCap.Text = row.Cells[0].Value?.ToString();
                txtTenNhaCungCap.Text = row.Cells[1].Value?.ToString();
                txtSoDienThoai.Text = row.Cells[2].Value?.ToString();
                txtDiaChi.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void dgvNhaCungCap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNhaCungCap_CellClick(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNhaCungCap.Text.Trim();
            string tenNCC = txtTenNhaCungCap.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(tenNCC))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã NCC và Tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaNCC", maNCC);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show($"Mã nhà cung cấp [{maNCC}] đã tồn tại!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO NhaCungCap (MaNCC, TenNCC, SoDT, DiaChi)
                        VALUES (@MaNCC, @TenNCC, @SoDT, @DiaChi)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmdInsert.Parameters.AddWithValue("@TenNCC", tenNCC);
                        cmdInsert.Parameters.AddWithValue("@SoDT", sdt);
                        cmdInsert.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm Nhà cung cấp mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataNhaCungCap();
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm Nhà cung cấp: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNhaCungCap.Text.Trim();
            string tenNCC = txtTenNhaCungCap.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string updateQuery = @"
                        UPDATE NhaCungCap
                        SET TenNCC = @TenNCC, SoDT = @SoDT, DiaChi = @DiaChi
                        WHERE MaNCC = @MaNCC";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmdUpdate.Parameters.AddWithValue("@TenNCC", tenNCC);
                        cmdUpdate.Parameters.AddWithValue("@SoDT", sdt);
                        cmdUpdate.Parameters.AddWithValue("@DiaChi", diaChi);
                        int affected = cmdUpdate.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin Nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataNhaCungCap();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã NCC để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa Nhà cung cấp: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNhaCungCap.Text.Trim();

            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Nhà cung cấp [{maNCC}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MaNCC", maNCC);
                        int affected = cmdDelete.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Xóa Nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataNhaCungCap();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã NCC để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Nhà cung cấp: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetFields()
        {
            txtMaNhaCungCap.Text = "";
            txtTenNhaCungCap.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaNhaCungCap_Click(object sender, EventArgs e) { }
        private void lblSoDienThoai_Click(object sender, EventArgs e) { }
        private void txtMaNhaCungCap_TextChanged(object sender, EventArgs e) { }
        private void txtSoDienThoai_TextChanged(object sender, EventArgs e) { }
        private void lblTenNhaCungCap_Click(object sender, EventArgs e) { }
        private void txtTenNhaCungCap_TextChanged(object sender, EventArgs e) { }
        private void lblDiaChi_Click(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
    }
}