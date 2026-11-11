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
    public partial class Form_QuanLyDoiTac : Form
    {
        private readonly ComboBox cboLoaiKenh;

        public Form_QuanLyDoiTac()
        {
            InitializeComponent();
            Label lblLoaiKenh = new Label { Text = "Loại kênh:", Location = new Point(37, 183), Size = new Size(120, 19) };
            cboLoaiKenh = new ComboBox { Location = new Point(175, 180), Width = 198, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiKenh.Items.AddRange(new object[] { "Sàn thương mại điện tử", "Cửa hàng" });
            cboLoaiKenh.SelectedIndex = 0;
            Controls.Add(lblLoaiKenh);
            Controls.Add(cboLoaiKenh);
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();

            LoadDataDoiTac();
        }

        private void LoadDataDoiTac()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaDoiTac AS [Mã Đối Tác],
                            TenDoiTac AS [Tên Đối Tác],
                            ISNULL(SoDT, '') AS [Điện Thoại],
                            ISNULL(DiaChi, '') AS [Địa Chỉ],
                            LoaiKenh AS [Loại Kênh]
                        FROM DoiTac WITH (NOLOCK)";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDoiTac.DataSource = dt;
                    dgvDoiTac.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvDoiTac.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu Đối tác: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvDoiTac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDoiTac.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = dgvDoiTac.Rows[e.RowIndex];
                txtMaDoiTac.Text = row.Cells[0].Value?.ToString();
                txtTenDoiTac.Text = row.Cells[1].Value?.ToString();
                txtSoDienThoai.Text = row.Cells[2].Value?.ToString();
                txtDiaChi.Text = row.Cells[3].Value?.ToString();
                cboLoaiKenh.SelectedItem = row.Cells[4].Value?.ToString();
            }
        }

        private void dgvDoiTac_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDoiTac_CellClick(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maDT = txtMaDoiTac.Text.Trim();
            string tenDT = txtTenDoiTac.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maDT) || string.IsNullOrEmpty(tenDT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã đối tác và Tên đối tác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM DoiTac WHERE MaDoiTac = @MaDoiTac";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaDoiTac", maDT);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show($"Mã đối tác [{maDT}] đã tồn tại!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO DoiTac (MaDoiTac, TenDoiTac, SoDT, DiaChi, LoaiKenh)
                        VALUES (@MaDoiTac, @TenDoiTac, @SoDT, @DiaChi, @LoaiKenh)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaDoiTac", maDT);
                        cmdInsert.Parameters.AddWithValue("@TenDoiTac", tenDT);
                        cmdInsert.Parameters.AddWithValue("@SoDT", sdt);
                        cmdInsert.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmdInsert.Parameters.AddWithValue("@LoaiKenh", Convert.ToString(cboLoaiKenh.SelectedItem));
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm Đối tác mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataDoiTac();
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm Đối tác: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maDT = txtMaDoiTac.Text.Trim();
            string tenDT = txtTenDoiTac.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maDT))
            {
                MessageBox.Show("Vui lòng chọn Đối tác cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string updateQuery = @"
                        UPDATE DoiTac
                        SET TenDoiTac = @TenDoiTac, SoDT = @SoDT, DiaChi = @DiaChi, LoaiKenh = @LoaiKenh
                        WHERE MaDoiTac = @MaDoiTac";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaDoiTac", maDT);
                        cmdUpdate.Parameters.AddWithValue("@TenDoiTac", tenDT);
                        cmdUpdate.Parameters.AddWithValue("@SoDT", sdt);
                        cmdUpdate.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmdUpdate.Parameters.AddWithValue("@LoaiKenh", Convert.ToString(cboLoaiKenh.SelectedItem));
                        int affected = cmdUpdate.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin Đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataDoiTac();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã đối tác để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa Đối tác: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maDT = txtMaDoiTac.Text.Trim();

            if (string.IsNullOrEmpty(maDT))
            {
                MessageBox.Show("Vui lòng chọn Đối tác cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Đối tác [{maDT}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM DoiTac WHERE MaDoiTac = @MaDoiTac";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MaDoiTac", maDT);
                        int affected = cmdDelete.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Xóa Đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataDoiTac();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã đối tác để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Đối tác: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetFields()
        {
            txtMaDoiTac.Text = "";
            txtTenDoiTac.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
            cboLoaiKenh.SelectedIndex = 0;
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaDoiTac_Click(object sender, EventArgs e) { }
        private void txtMaDoiTac_TextChanged(object sender, EventArgs e) { }
        private void lblDienThoai_Click(object sender, EventArgs e) { }
        private void txtSoDienThoai_TextChanged(object sender, EventArgs e) { }
        private void lblTenDoiTac_Click(object sender, EventArgs e) { }
        private void txtTenDoiTac_TextChanged(object sender, EventArgs e) { }
        private void lblDiaChi_Click(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }

        private void Form_QuanLyDoiTac_Load_1(object sender, EventArgs e)
        {
        }

        private void KhoiTaoChucNang()
        {
            AiHelper.DinhDangLuoi(dgvVanChuyen);
            dgvDoiTac.SelectionChanged += (s, e) => CapNhatKenh();
            NapVanChuyen();
        }

        private void NapVanChuyen()
        {
            try
            {
                DataTable t = UyTinVanChuyen.ThongKe(null);
                t.Columns.Add("Điểm Hiện Tại", typeof(decimal));
                foreach (DataRow r in t.Rows)
                {
                    decimal? d = UyTinVanChuyen.Diem(Convert.ToInt32(r["Đã Giao"]), Convert.ToInt32(r["Hoàn/Thất Bại"]));
                    if (d.HasValue) r["Điểm Hiện Tại"] = d.Value;
                }
                dgvVanChuyen.DataSource = t;
            }
            catch (Exception ex) { lblKenh.Text = "Chưa đọc được dữ liệu vận chuyển: " + AiHelper.LoiDeHieu(ex); }
        }

        private void CapNhatKenh()
        {
            if (lblKenh == null || dgvDoiTac.CurrentRow == null || dgvDoiTac.CurrentRow.Cells[0].Value == null) return;
            string ma = Convert.ToString(dgvDoiTac.CurrentRow.Cells[0].Value);
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT COUNT(*) AS TongDon, " +
                    "       SUM(CASE WHEN ISNULL(dh.TrangThai, N'') LIKE N'%hủy%' THEN 1 ELSE 0 END) AS DonHuy, " +
                    "       SUM(CASE WHEN EXISTS (SELECT 1 FROM TraHang th WHERE th.MaDH = dh.MaDH) THEN 1 ELSE 0 END) AS DonCoTra, " +
                    "       SUM(ISNULL(dh.TongTien, 0)) AS DoanhThu " +
                    "FROM DonHang dh WHERE dh.MaDoiTac = @m", AiHelper.P("@m", ma));
                int tong = Convert.ToInt32(t.Rows[0]["TongDon"]);
                if (tong == 0) { lblKenh.Text = "Kênh " + ma + ": chưa có đơn hàng nào."; return; }
                int huy = Convert.ToInt32(t.Rows[0]["DonHuy"]), tra = Convert.ToInt32(t.Rows[0]["DonCoTra"]);
                lblKenh.Text = "Kênh " + ma + ": " + tong + " đơn, doanh thu " + Convert.ToDecimal(t.Rows[0]["DoanhThu"]).ToString("N0") + " đ. " +
                                huy + " đơn hủy (" + (100.0 * huy / tong).ToString("0.#") + "%), " +
                                tra + " đơn có phiếu trả hàng (" + (100.0 * tra / tong).ToString("0.#") + "%).";
            }
            catch (Exception ex) { lblKenh.Text = "Chưa tính được chỉ số kênh: " + AiHelper.LoiDeHieu(ex); }
        }
    }
}