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
    public partial class Form_CanhBaoTonKho : Form
    {
        public Form_CanhBaoTonKho()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
        }

        private void KhoiTaoChucNang()
        {
            dgvCanhBaoTonKho.SelectionChanged += dgvCanhBaoTonKho_SelectionChanged;
        }

        private void dgvCanhBaoTonKho_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCanhBaoTonKho.CurrentRow == null || !dgvCanhBaoTonKho.Columns.Contains("Nên Làm Gì")) return;
            lblGoiY.Text = "Nên làm gì: " + Convert.ToString(dgvCanhBaoTonKho.CurrentRow.Cells["Nên Làm Gì"].Value) +
                           "\n" + Convert.ToString(dgvCanhBaoTonKho.CurrentRow.Cells["ThongBao"].Value);
        }

        private void btnTinhLaiROP_Click(object sender, EventArgs e)
        {
            try
            {
                AiHelper.ChayThuTuc("sp_TinhMucPhaiDatHang");
                LoadDanhSachCanhBao();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void LoadDanhSachCanhBao()
        {
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT ISNULL(tk.MaKho, '') AS [Mã Kho], ISNULL(k.TenKho, N'') AS [Tên Kho], v.MaSP AS [Mã Sản Phẩm], v.TenSP AS [Tên Sản Phẩm], " +
                    "       ISNULL(tk.SoLuongTon, 0) AS [Tồn Hiện Tại], v.TonHienTai AS [Tổng Tồn Các Kho], sp.NguongAnToan AS [Ngưỡng An Toàn], " +
                    "       v.TrangThai AS [Tình Trạng], v.Cau AS [Xu Hướng Bán], v.DuDoan30Ngay AS [Dự Đoán Bán 30 Ngày Tới], " +
                    "       v.SoNgayConLai AS [Đủ Bán (Ngày)], v.ROP AS [Mức Phải Đặt Hàng], v.SoNenNhap AS [Nên Nhập Thêm], " +
                    "       v.ViecNenLam AS [Nên Làm Gì], v.ThongBao " +
                    "FROM vw_DuBaoSanPham v JOIN SanPham sp ON sp.MaSP = v.MaSP " +
                    "LEFT JOIN TonKho tk ON tk.MaSP = v.MaSP LEFT JOIN Kho k ON k.MaKho = tk.MaKho " +
                    "ORDER BY CASE v.TrangThai WHEN N'KHẨN CẤP' THEN 0 WHEN N'ĐẶT HÀNG NGAY' THEN 1 WHEN N'ỔN' THEN 2 WHEN N'TỒN NHIỀU' THEN 3 ELSE 4 END, " +
                    "         v.SoNgayConLai, v.MaSP, tk.MaKho");
                dgvCanhBaoTonKho.DataSource = dt;
                dgvCanhBaoTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                if (dgvCanhBaoTonKho.Columns.Contains("ThongBao")) dgvCanhBaoTonKho.Columns["ThongBao"].Visible = false;
                foreach (DataGridViewRow dong in dgvCanhBaoTonKho.Rows)
                    if (Convert.ToInt32(dong.Cells["Nên Nhập Thêm"].Value ?? 0) > 0)
                        dong.DefaultCellStyle.BackColor = Color.FromArgb(255, 236, 204);
                dgvCanhBaoTonKho.ReadOnly = true;
                dgvCanhBaoTonKho.AllowUserToAddRows = false;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnBaoNhapHang_Click(object sender, EventArgs e)
        {
            if (dgvCanhBaoTonKho.CurrentRow == null || dgvCanhBaoTonKho.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKho = dgvCanhBaoTonKho.CurrentRow.Cells["Mã Kho"].Value?.ToString();
            string maSP = dgvCanhBaoTonKho.CurrentRow.Cells["Mã Sản Phẩm"].Value?.ToString();
            string tenSP = dgvCanhBaoTonKho.CurrentRow.Cells["Tên Sản Phẩm"].Value?.ToString();
            int tonHienTai = Convert.ToInt32(dgvCanhBaoTonKho.CurrentRow.Cells["Tồn Hiện Tại"].Value ?? 0);
            int nguongAnToan = Convert.ToInt32(dgvCanhBaoTonKho.CurrentRow.Cells["Ngưỡng An Toàn"].Value ?? 0);
            if (string.IsNullOrEmpty(maKho))
            {
                MessageBox.Show("Sản phẩm [" + tenSP + "] chưa có trong kho nào. Hãy lập phiếu nhập kho trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tonHienTai > nguongAnToan)
            {
                DialogResult confirm = MessageBox.Show(
                    $"Sản phẩm [{tenSP}] hiện vẫn còn {tonHienTai} cái (trên ngưỡng an toàn là {nguongAnToan}).\nBạn có chắc chắn vẫn muốn báo nhập thêm không?",
                    "Xác nhận nhập thêm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.No) return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string maCanhBao = AiHelper.LayMaMoi("CB");

                    string insertQuery = @"INSERT INTO CanhBaoTonHet (MaCanhBao, MaKho, MaSP, NgayCanhBao, TrangThai)
                                 VALUES (@MaCB, @MaKho, @MaSP, GETDATE(), N'Chưa xử lý')";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCB", maCanhBao);
                        cmd.Parameters.AddWithValue("@MaKho", maKho);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Đã phát yêu cầu báo nhập hàng thành công cho sản phẩm [{tenSP}]!\nMã cảnh báo: {maCanhBao}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi ghi nhận yêu cầu báo nhập hàng: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblThongBao_Click(object sender, EventArgs e) { }
        private void dgvCanhBaoTonKho_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Form_CanhBaoTonKho_Load_1(object sender, EventArgs e)
        {
            LoadDanhSachCanhBao();
        }
    }
}