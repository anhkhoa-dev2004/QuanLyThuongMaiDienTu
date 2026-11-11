using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    public partial class Form_QuenMatKhau : Form
    {
        public Form_QuenMatKhau()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void lblTenTK_Click(object sender, EventArgs e)
        {
        }

        private void txtTenTK_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblSoDT_Click(object sender, EventArgs e)
        {
        }

        private void txtSoDT_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblMk_Click(object sender, EventArgs e)
        {
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void lblThongBao_Click(object sender, EventArgs e)
        {
        }

        private void btnDoiMk_Click(object sender, EventArgs e)
        {
            string tenTK = txtTenTK.Text.Trim();
            string matKhauCu = txtSoDT.Text.Trim();
            string matKhauMoi = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi))
            {
                lblThongBao.Text = "Hãy nhập tên đăng nhập, mật khẩu đang dùng và mật khẩu mới.";
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi.Length < 6)
            {
                lblThongBao.Text = "Mật khẩu mới phải từ 6 ký tự trở lên!";
                MessageBox.Show("Mật khẩu mới phải từ 6 ký tự trở lên!", "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DoiMatKhau", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenTK", tenTK);
                        cmd.Parameters.AddWithValue("@MatKhauCu", matKhauCu);
                        cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                        cmd.ExecuteNonQuery();
                    }
                }
                lblThongBao.Text = "Đổi mật khẩu thành công!";
                MessageBox.Show("Đổi mật khẩu thành công! Lần sau đăng nhập bằng mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = AiHelper.LoiDeHieu(ex);
                MessageBox.Show("Chưa đổi được mật khẩu: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_QuenMatKhau_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
            txtSoDT.PasswordChar = '*';
            lblThongBao.Text = "";
            Text = "Đổi mật khẩu";
            lblTieuDe.Text = "ĐỔI MẬT KHẨU";
            lblSoDT.Text = "Mật khẩu đang dùng:";
        }
    }
}