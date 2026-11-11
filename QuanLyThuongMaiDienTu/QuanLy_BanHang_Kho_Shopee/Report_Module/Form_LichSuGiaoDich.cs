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

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    public partial class Form_LichSuGiaoDich : Form
    {
        public Form_LichSuGiaoDich()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void Form_LichSuGiaoDich_Load(object sender, EventArgs e)
        {
            cboLoaiGiaoDich.Items.Clear();
            cboLoaiGiaoDich.Items.AddRange(new object[]
            {
                "Tất cả",
                "Nhập kho",
                "Xuất kho",
                "Đơn hàng",
                "Hóa đơn",
                "Trả hàng",
                "Bảo hành",
                "Vận đơn"
            });
            cboLoaiGiaoDich.SelectedIndex = 0;

            txtTuNgay.Text = "01/01/" + DateTime.Now.Year;
            txtDenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTuKhoa.Text = "";

            LoadLichSuGiaoDich();
        }

        private void LoadLichSuGiaoDich()
        {
            string loaiGD = cboLoaiGiaoDich.SelectedItem?.ToString() ?? "Tất cả";
            string tuNgayStr = txtTuNgay.Text.Trim();
            string denNgayStr = txtDenNgay.Text.Trim();
            string tuKhoa = txtTuKhoa.Text.Trim();

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaLichSu AS [Mã LS],
                            LoaiGiaoDich AS [Loại Giao Dịch],
                            MaThamChieu AS [Mã Tham Chiếu],
                            ISNULL(TenSP, N'') AS [Sản Phẩm],
                            ISNULL(TenKho, N'') AS [Kho],
                            SoLuong AS [Số Lượng],
                            SoTien AS [Số Tiền],
                            ISNULL(NguoiThucHien, N'') AS [Người Thực Hiện],
                            ThoiGianGD AS [Thời Gian],
                            MoTa AS [Mô Tả]
                        FROM vw_LichSuGiaoDich
                        WHERE 1 = 1";

                    if (!string.IsNullOrEmpty(loaiGD) && loaiGD != "Tất cả")
                    {
                        query += " AND LoaiGiaoDich = @LoaiGiaoDich";
                    }

                    bool coTuNgay = DateTime.TryParseExact(tuNgayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tuNgay);
                    if (coTuNgay)
                    {
                        query += " AND ThoiGianGD >= @TuNgay";
                    }

                    bool coDenNgay = DateTime.TryParseExact(denNgayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime denNgay);
                    if (coDenNgay)
                    {
                        query += " AND ThoiGianGD < DATEADD(DAY, 1, @DenNgay)";
                    }

                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        query += " AND (MaThamChieu LIKE @TuKhoa OR MoTa LIKE @TuKhoa OR TenSP LIKE @TuKhoa)";
                    }

                    query += " ORDER BY ThoiGianGD DESC, MaLichSu DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(loaiGD) && loaiGD != "Tất cả")
                        {
                            cmd.Parameters.AddWithValue("@LoaiGiaoDich", loaiGD);
                        }

                        if (coTuNgay)
                        {
                            cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                        }

                        if (coDenNgay)
                        {
                            cmd.Parameters.AddWithValue("@DenNgay", denNgay);
                        }

                        if (!string.IsNullOrEmpty(tuKhoa))
                        {
                            cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvLichSuGiaoDich.DataSource = dt;
                        dgvLichSuGiaoDich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvLichSuGiaoDich.ReadOnly = true;
                        dgvLichSuGiaoDich.AllowUserToAddRows = false;

                        if (dt.Columns.Contains("Số Tiền"))
                        {
                            dgvLichSuGiaoDich.Columns["Số Tiền"].DefaultCellStyle.Format = "N0";
                        }

                        txtTongSoGiaoDich.Text = string.Format("{0:N0} giao dịch", dt.Rows.Count);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải Lịch sử giao dịch: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadLichSuGiaoDich();
        }

        private void cboLoaiGiaoDich_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLichSuGiaoDich();
        }

        private void txtTuNgay_TextChanged(object sender, EventArgs e)
        {
            LoadLichSuGiaoDich();
        }

        private void txtDenNgay_TextChanged(object sender, EventArgs e)
        {
            LoadLichSuGiaoDich();
        }

        private void txtTuKhoa_TextChanged(object sender, EventArgs e)
        {
            LoadLichSuGiaoDich();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblLoaiGiaoDich_Click(object sender, EventArgs e) { }
        private void lblTuNgay_Click(object sender, EventArgs e) { }
        private void lblDenNgay_Click(object sender, EventArgs e) { }
        private void lblTuKhoa_Click(object sender, EventArgs e) { }
        private void txtTongSoGiaoDich_TextChanged(object sender, EventArgs e) { }
        private void dgvLichSuGiaoDich_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
