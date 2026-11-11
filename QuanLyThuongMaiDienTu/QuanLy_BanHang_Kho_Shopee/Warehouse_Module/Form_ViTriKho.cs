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
    public partial class Form_ViTriKho : Form
    {
        public Form_ViTriKho()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void KhoiTaoChucNang()
        {
            AiHelper.DinhDangLuoi(dgvAbc);
        }

        private void btnGoiYAbc_Click(object sender, EventArgs e)
        {
            GoiYAbc();
        }

        private void GoiYAbc()
        {
            try
            {
                DataTable t = AiHelper.ChayThuTuc("sp_ChiaNhomBanChay", AiHelper.P("@TieuChi", "SanLuong"));
                t.Columns.Add("GoiYKhuVuc", typeof(string));
                foreach (DataRow r in t.Rows)
                {
                    string pl = Convert.ToString(r["PhanLoai"]);
                    r["GoiYKhuVuc"] = pl == "A" ? "Gần cửa xuất, kệ tầm tay" : (pl == "B" ? "Khu giữa" : "Khu trong cùng / kệ cao");
                }
                dgvAbc.DataSource = t;
                string[,] ten = { { "ThuHang", "Thứ Hạng" }, { "MaSP", "Mã SP" }, { "TenSP", "Tên Sản Phẩm" }, { "GiaTri", "Sản Lượng Bán" },
                                  { "TyLePhanTram", "Tỷ Lệ (%)" }, { "TyLeLuyKe", "Cộng Dồn (%)" }, { "PhanLoai", "Nhóm" }, { "GoiYKhuVuc", "Gợi Ý Khu Vực" } };
                for (int i = 0; i < ten.GetLength(0); i++)
                    if (dgvAbc.Columns.Contains(ten[i, 0])) dgvAbc.Columns[ten[i, 0]].HeaderText = ten[i, 1];

                DataTable o = AiHelper.Doc("SELECT COUNT(*) AS Tong, SUM(CASE WHEN TrangThai = N'Trống' THEN 1 ELSE 0 END) AS Trong FROM ViTriKho");
                lblAbc.Text = "Nhóm A: bán nhiều nhất (cộng dồn tới 80% sản lượng), B: tới 95%, C: phần còn lại. Tính trên toàn bộ đơn hàng." +
                               "\nBảng ViTriKho hiện có " + o.Rows[0]["Tong"] + " ô kệ (" + (o.Rows[0]["Trong"] == DBNull.Value ? 0 : o.Rows[0]["Trong"]) + " ô trống). Hệ thống chưa lưu hàng nằm ở ô nào nên chỉ gợi ý khu vực.";
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Form_ViTriKho_Load(object sender, EventArgs e)
        {
            LoadDanhSachViTri();
        }

        private void LoadDanhSachViTri()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            MaKho AS [Mã Vị Trí],
                            TenKho AS [Tên Vị Trí],
                            DiaChi AS [Ghi Chú]
                        FROM Kho WITH (NOLOCK)
                        ORDER BY MaKho ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvViTriKho.DataSource = dt;
                        dgvViTriKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvViTriKho.ReadOnly = true;
                        dgvViTriKho.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách kho: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maKho = txtMaViTri.Text.Trim();
            string tenKho = txtTenViTri.Text.Trim();
            string diaChi = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(maKho) || string.IsNullOrEmpty(tenKho))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã vị trí và Tên vị trí kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Kho WHERE MaKho = @MaKho";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaKho", maKho);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show($"Mã vị trí/kho [{maKho}] đã có trong hệ thống!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO Kho (MaKho, TenKho, DiaChi)
                                         VALUES (@MaKho, @TenKho, @DiaChi)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKho", maKho);
                        cmd.Parameters.AddWithValue("@TenKho", tenKho);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Thêm mới vị trí kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachViTri();
                            XoaTrangForm();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm vị trí kho: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKho = txtMaViTri.Text.Trim();
            string tenKho = txtTenViTri.Text.Trim();
            string diaChi = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(maKho) || string.IsNullOrEmpty(tenKho))
            {
                MessageBox.Show("Vui lòng chọn vị trí cần sửa và nhập Tên vị trí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string updateQuery = @"UPDATE Kho
                                         SET TenKho = @TenKho, DiaChi = @DiaChi
                                         WHERE MaKho = @MaKho";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKho", maKho);
                        cmd.Parameters.AddWithValue("@TenKho", tenKho);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin vị trí kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachViTri();
                            XoaTrangForm();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã kho để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa vị trí kho: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKho = txtMaViTri.Text.Trim();

            if (string.IsNullOrEmpty(maKho))
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Vị trí kho [{maKho}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM Kho WHERE MaKho = @MaKho";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKho", maKho);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa vị trí kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachViTri();
                            XoaTrangForm();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa kho [{maKho}] vì kho này đang có sản phẩm tồn kho hoặc có hóa đơn/phiếu nhập liên quan!", "Chưa xóa được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvViTriKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvViTriKho.Rows[e.RowIndex];
                txtMaViTri.Text = row.Cells["Mã Vị Trí"].Value?.ToString();
                txtTenViTri.Text = row.Cells["Tên Vị Trí"].Value?.ToString();
                txtGhiChu.Text = row.Cells["Ghi Chú"].Value?.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XoaTrangForm()
        {
            txtMaViTri.Clear();
            txtTenViTri.Clear();
            txtGhiChu.Clear();
            txtMaViTri.Focus();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaViTri_Click(object sender, EventArgs e) { }
        private void txtMaViTri_TextChanged(object sender, EventArgs e) { }
        private void lblTenViTri_Click(object sender, EventArgs e) { }
        private void txtTenViTri_TextChanged(object sender, EventArgs e) { }
        private void lblGhiChu_Click(object sender, EventArgs e) { }
        private void txtGhiChu_TextChanged(object sender, EventArgs e) { }
        private void dgvViTriKho_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}