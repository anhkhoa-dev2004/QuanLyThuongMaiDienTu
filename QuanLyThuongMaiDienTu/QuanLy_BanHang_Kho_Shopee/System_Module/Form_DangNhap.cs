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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    public partial class Form_DangNhap : Form
    {
        public Form_DangNhap()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            this.AcceptButton = btnDangNhap;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenTK = txtTenTK.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try

            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DangNhap", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenTK", tenTK);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Session.MaND = reader["MaND"].ToString();
                                    Session.HoTen = reader["HoTen"].ToString();
                                    Session.SoDT = reader["SoDT"].ToString();
                                    Session.TenQuyen = reader["TenQuyen"].ToString();
                                    Session.QuyenKho = Convert.ToBoolean(reader["QuyenKho"]);
                                    Session.QuyenBanHang = Convert.ToBoolean(reader["QuyenBanHang"]);
                                    Session.QuyenBaoCao = Convert.ToBoolean(reader["QuyenBaoCao"]);
                                    Session.DaDangNhap = true;

                                    MessageBox.Show("Đăng nhập thành công! Xin chào " + Session.HoTen, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form_TrangChuChinh trangChu = new Form_TrangChuChinh();
                                    trangChu.Show();
                                    this.Hide();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Chưa đăng nhập được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex) when (ex.Number >= 50000)
            {
                MessageBox.Show(ex.Message, "Chưa đăng nhập được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Clear();
                txtMatKhau.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDangKyTk_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tài khoản nhân viên do quản lý cửa hàng tạo.\nQuản lý đăng nhập rồi vào mục \"6. Tài khoản > Tạo tài khoản nhân viên\".",
                            "Tạo tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTroGiup_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Liên hệ quản trị viên hệ thống để được hỗ trợ.", "Trợ giúp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            using (Form_QuenMatKhau formQuenMk = new Form_QuenMatKhau())
            {
                formQuenMk.ShowDialog();
            }
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hãy nhờ quản lý cửa hàng đặt lại mật khẩu (mục \"6. Tài khoản > Phân quyền người dùng\", nút \"Đặt lại mật khẩu\").\n" +
                            "Đăng nhập được rồi thì nên đổi lại mật khẩu của riêng mình ở nút \"Đổi mật khẩu\".",
                            "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtTenTK_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void Form_DangNhap_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void lblMk_Click(object sender, EventArgs e)
        {
        }
    }
}