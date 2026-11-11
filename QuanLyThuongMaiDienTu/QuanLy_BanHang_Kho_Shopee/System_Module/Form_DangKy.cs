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
    public partial class Form_DangKy : Form
    {
        public Form_DangKy()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void txtTenTK_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*';
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSoDT_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblTenTK_Click(object sender, EventArgs e)
        {
        }

        private void lblMk_Click(object sender, EventArgs e)
        {
        }

        private void lblXacNhanMK_Click(object sender, EventArgs e)
        {
        }

        private void lblHoTen_Click(object sender, EventArgs e)
        {
        }

        private void lblSoDT_Click(object sender, EventArgs e)
        {
        }

        private void btnDangKyTk_Click(object sender, EventArgs e)
        {
            string tenTK = txtTenTK.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string xacNhanMK = textBox1.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string soDT = txtSoDT.Text.Trim();

            if (string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMK) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tất cả các thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có độ dài từ 6 ký tự trở lên!", "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhau != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DangKyTaiKhoan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNDQuanLy", Session.MaND ?? "");
                        cmd.Parameters.AddWithValue("@TenTK", tenTK);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@SoDT", soDT);
                        SqlParameter pMa = cmd.Parameters.Add("@MaND", SqlDbType.VarChar, 20);
                        pMa.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã tạo tài khoản " + tenTK + " (mã " + pMa.Value + ").\nTài khoản mới chỉ có quyền bán hàng. " +
                                        "Muốn cho thêm quyền kho hoặc báo cáo thì vào \"Phân quyền người dùng\".\n" +
                                        "Nhắc nhân viên đăng nhập rồi tự đổi mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_DangKy_Load(object sender, EventArgs e)
        {
            Text = "Tạo tài khoản nhân viên";
            lblTieuDe.Text = "TẠO TÀI KHOẢN NHÂN VIÊN";
            btnDangKyTk.Text = "Tạo tài khoản";
            txtMatKhau.PasswordChar = '*';
            textBox1.PasswordChar = '*';
        }
    }
}