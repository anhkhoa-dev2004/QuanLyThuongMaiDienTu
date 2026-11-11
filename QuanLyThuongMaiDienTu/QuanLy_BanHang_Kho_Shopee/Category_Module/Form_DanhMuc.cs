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
    public partial class Form_DanhMuc : Form
    {
        public Form_DanhMuc()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void Form_DanhMuc_Load(object sender, EventArgs e)
        {
            LoadDataDanhMuc();
        }

        private void LoadDataDanhMuc()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaDanhMuc AS [Mã Danh Mục],
                            TenDanhMuc AS [Tên Danh Mục]
                        FROM DanhMuc WITH (NOLOCK)";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDanhMuc.DataSource = dt;
                    dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu danh mục: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];
                txtMaDanhMuc.Text = row.Cells[0].Value?.ToString();
                txtTenDanhMuc.Text = row.Cells[1].Value?.ToString();
            }
        }

        private void dgvDanhMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDanhMuc_CellClick(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maDM = txtMaDanhMuc.Text.Trim();
            string tenDM = txtTenDanhMuc.Text.Trim();

            if (string.IsNullOrEmpty(maDM) || string.IsNullOrEmpty(tenDM))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM DanhMuc WHERE MaDanhMuc = @MaDM";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaDM", maDM);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show($"Mã danh mục [{maDM}] đã tồn tại!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc) VALUES (@MaDM, @TenDM)";
                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaDM", maDM);
                        cmdInsert.Parameters.AddWithValue("@TenDM", tenDM);
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm danh mục mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataDanhMuc();
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm danh mục: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maDM = txtMaDanhMuc.Text.Trim();
            string tenDM = txtTenDanhMuc.Text.Trim();

            if (string.IsNullOrEmpty(maDM))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string updateQuery = "UPDATE DanhMuc SET TenDanhMuc = @TenDM WHERE MaDanhMuc = @MaDM";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaDM", maDM);
                        cmdUpdate.Parameters.AddWithValue("@TenDM", tenDM);
                        int affected = cmdUpdate.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Cập nhật danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataDanhMuc();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã danh mục để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa danh mục: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maDM = txtMaDanhMuc.Text.Trim();

            if (string.IsNullOrEmpty(maDM))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa danh mục [{maDM}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDM";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MaDM", maDM);
                        int affected = cmdDelete.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Xóa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataDanhMuc();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã danh mục để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa danh mục (Có thể do ràng buộc dữ liệu sản phẩm): " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetFields()
        {
            txtMaDanhMuc.Text = "";
            txtTenDanhMuc.Text = "";
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaDanhMuc_Click(object sender, EventArgs e) { }
        private void txtMaDanhMuc_TextChanged(object sender, EventArgs e) { }
        private void lblTenDanhMuc_Click(object sender, EventArgs e) { }
        private void txtTenDanhMuc_TextChanged(object sender, EventArgs e) { }
    }
}