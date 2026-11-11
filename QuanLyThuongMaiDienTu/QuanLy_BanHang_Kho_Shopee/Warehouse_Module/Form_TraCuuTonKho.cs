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
    public partial class Form_TraCuuTonKho : Form
    {
        public Form_TraCuuTonKho()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void Form_TraCuuTonKho_Load(object sender, EventArgs e)
        {
            TraCuuTonKho();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TraCuuTonKho();
        }

        private void TraCuuTonKho()
        {
            string timKho = txtChonKho.Text.Trim();
            string timSanPham = txtMaTenSanPham.Text.Trim();

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            ISNULL(tk.MaKho, 'KHO01') AS [Mã Kho],
                            ISNULL(k.TenKho, N'Kho Tân Bình') AS [Tên Kho],
                            sp.MaSP AS [Mã Sản Phẩm],
                            sp.TenSP AS [Tên Sản Phẩm],
                            ISNULL(tk.SoLuongTon, 0) AS [Số Lượng Tồn],
                            sp.NguongAnToan AS [Ngưỡng An Toàn]
                        FROM SanPham sp WITH (NOLOCK)
                        LEFT JOIN TonKho tk WITH (NOLOCK) ON sp.MaSP = tk.MaSP
                        LEFT JOIN Kho k WITH (NOLOCK) ON tk.MaKho = k.MaKho
                        WHERE 1 = 1";

                    if (!string.IsNullOrEmpty(timKho))
                    {
                        query += " AND (tk.MaKho LIKE @Kho OR k.TenKho LIKE @Kho)";
                    }

                    if (!string.IsNullOrEmpty(timSanPham))
                    {
                        query += " AND (sp.MaSP LIKE @SP OR sp.TenSP LIKE @SP)";
                    }

                    query += " ORDER BY sp.MaSP ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(timKho))
                        {
                            cmd.Parameters.AddWithValue("@Kho", "%" + timKho + "%");
                        }

                        if (!string.IsNullOrEmpty(timSanPham))
                        {
                            cmd.Parameters.AddWithValue("@SP", "%" + timSanPham + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvTonKho.DataSource = dt;
                        dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvTonKho.ReadOnly = true;
                        dgvTonKho.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tra cứu tồn kho: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtChonKho_TextChanged(object sender, EventArgs e)
        {
            TraCuuTonKho();
        }

        private void txtMaTenSanPham_TextChanged(object sender, EventArgs e)
        {
            TraCuuTonKho();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblChonKho_Click(object sender, EventArgs e) { }
        private void lblMaTenSanPham_Click(object sender, EventArgs e) { }
        private void dgvTonKho_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}