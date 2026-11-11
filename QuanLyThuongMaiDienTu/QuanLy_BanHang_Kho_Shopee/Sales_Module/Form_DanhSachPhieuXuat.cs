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

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    public partial class Form_DanhSachPhieuXuat : Form
    {
        public Form_DanhSachPhieuXuat()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            LoadDanhSachPhieuXuat();
        }

        private void LoadDanhSachPhieuXuat()
        {
            string maDH = txtMaDonHang.Text.Trim();

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            px.MaPX AS [Mã Phiếu Xuất],
                            px.MaDH AS [Mã Đơn Hàng],
                            ISNULL(k.TenKho, N'Kho Tân Bình') AS [Kho Xuất],
                            px.NgayXuat AS [Ngày Xuất],
                            ISNULL(SUM(ctx.SoLuong), 0) AS [Tổng Số Lượng],
                            ISNULL(px.NguoiLap, N'Thủ kho') AS [Người Lập]
                        FROM PhieuXuat px WITH (NOLOCK)
                        LEFT JOIN Kho k WITH (NOLOCK) ON px.MaKho = k.MaKho
                        LEFT JOIN ChiTietPhieuXuat ctx WITH (NOLOCK) ON px.MaPX = ctx.MaPX
                        WHERE 1 = 1";

                    if (!string.IsNullOrEmpty(maDH))
                    {
                        query += " AND (px.MaDH LIKE @MaDH OR px.MaPX LIKE @MaDH)";
                    }

                    query += @" GROUP BY px.MaPX, px.MaDH, k.TenKho, px.NgayXuat, px.NguoiLap
                                ORDER BY px.NgayXuat DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(maDH))
                        {
                            cmd.Parameters.AddWithValue("@MaDH", "%" + maDH + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvDanhSachPhieuXuat.DataSource = dt;
                        dgvDanhSachPhieuXuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDanhSachPhieuXuat.ReadOnly = true;
                        dgvDanhSachPhieuXuat.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách phiếu xuất: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtMaDonHang_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachPhieuXuat();
        }

        private void btnLapPhieuXuat_Click(object sender, EventArgs e)
        {
            using (Form_LapPhieuXuat formLapPX = new Form_LapPhieuXuat())
            {
                formLapPX.ShowDialog();
            }

            LoadDanhSachPhieuXuat();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaDonHang_Click(object sender, EventArgs e) { }
        private void dgvDanhSachPhieuXuat_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Form_DanhSachPhieuXuat_Load_1(object sender, EventArgs e)
        {
        }
    }
}