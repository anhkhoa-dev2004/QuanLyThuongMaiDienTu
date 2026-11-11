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
    public partial class Form_LapPhieuXuat : Form
    {
        public Form_LapPhieuXuat()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            GiaoDien.ThemBangDuoi(this, "Đơn đang chờ xác nhận (chưa xuất kho) - bấm 1 dòng để chọn", 240,
                () => AiHelper.Doc(
                    "SELECT dh.MaDH AS [Mã đơn], dh.NgayDat AS [Ngày đặt], ISNULL(kh.TenKH, N'Khách vãng lai') AS [Khách hàng], " +
                    "ISNULL(dt.TenDoiTac, N'Bán lẻ') AS [Kênh bán], dh.TongTien AS [Tiền hàng], " +
                    "(SELECT COUNT(*) FROM ChiTietDonHang ct WHERE ct.MaDH = dh.MaDH) AS [Số món], " +
                    "DATEDIFF(HOUR, dh.NgayDat, GETDATE()) AS [Đã chờ (giờ)] " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH LEFT JOIN DoiTac dt ON dt.MaDoiTac = dh.MaDoiTac " +
                    "WHERE dh.TrangThai = N'Chờ xác nhận' ORDER BY dh.NgayDat"),
                r => txtMaDonHang.Text = Convert.ToString(r.Cells["Mã đơn"].Value));
        }

        private void Form_LapPhieuXuat_Load(object sender, EventArgs e)
        {
            txtMaDonHang.Text = "";
            txtMaKhoXuat.Text = "";

            LoadChiTietPhieuXuat();
        }

        private void LoadChiTietPhieuXuat()
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
                            px.MaKho AS [Mã Kho],
                            px.NgayXuat AS [Ngày Xuất],
                            ctx.MaSP AS [Mã SP],
                            sp.TenSP AS [Tên Sản Phẩm],
                            ctx.SoLuong AS [Số Lượng Xuất],
                            px.NguoiLap AS [Người Lập]
                        FROM PhieuXuat px WITH (NOLOCK)
                        JOIN ChiTietPhieuXuat ctx WITH (NOLOCK) ON px.MaPX = ctx.MaPX
                        JOIN SanPham sp WITH (NOLOCK) ON ctx.MaSP = sp.MaSP
                        WHERE px.MaDH = @MaDH OR @MaDH = ''
                        ORDER BY px.NgayXuat DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDH", maDH);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvChiTietPhieuXuat.DataSource = dt;
                        dgvChiTietPhieuXuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvChiTietPhieuXuat.ReadOnly = true;
                        dgvChiTietPhieuXuat.AllowUserToAddRows = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi tiết phiếu xuất: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXacNhanXuatKho_Click(object sender, EventArgs e)
        {
            string maDH = txtMaDonHang.Text.Trim();
            string maKho = txtMaKhoXuat.Text.Trim();

            if (string.IsNullOrEmpty(maDH) || string.IsNullOrEmpty(maKho))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã đơn hàng và Mã kho xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraRopTruocKhiXuat(maDH, maKho)) return;

            try
            {
                string maPX = DonHangShopee.XacNhanVaXuatKho(maDH, maKho);
                MessageBox.Show(DonHangShopee.ThongBaoSauXuatKho(maDH, maPX),
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadChiTietPhieuXuat();
                GiaoDien.TaiLai(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa xuất kho được: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMaDonHang_TextChanged(object sender, EventArgs e)
        {
            LoadChiTietPhieuXuat();
        }

        private void dgvChiTietPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChiTietPhieuXuat.Rows[e.RowIndex];
                txtMaDonHang.Text = row.Cells["Mã Đơn Hàng"].Value?.ToString();
                txtMaKhoXuat.Text = row.Cells["Mã Kho"].Value?.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaDonHang_Click(object sender, EventArgs e) { }
        private void lblMaKhoXuat_Click(object sender, EventArgs e) { }
        private void txtMaKhoXuat_TextChanged(object sender, EventArgs e) { }
        private void dgvChiTietPhieuXuat_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private bool KiemTraRopTruocKhiXuat(string maDH, string maKho)
        {
            try
            {
                object daXuat = AiHelper.LayGiaTri("SELECT COUNT(*) FROM PhieuXuat WHERE MaDH = @d", AiHelper.P("@d", maDH));
                if (Convert.ToInt32(daXuat ?? 0) > 0) return true;

                DataTable t = AiHelper.Doc(
                    "SELECT sp.TenSP, ct.SoLuong AS SoXuat, ISNULL(tk.SoLuongTon, 0) AS TonKho, " +
                    "       ISNULL((SELECT SUM(x.SoLuongTon) FROM TonKho x WHERE x.MaSP = ct.MaSP), 0) AS TongTon, " +
                    "       v.TonAnToan AS TonKhoAnToan, v.ROP AS DiemDatHangLai_ROP " +
                    "FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP " +
                    "LEFT JOIN vw_DuBaoSanPham v ON v.MaSP = ct.MaSP " +
                    "LEFT JOIN TonKho tk ON tk.MaSP = ct.MaSP AND tk.MaKho = @kho WHERE ct.MaDH = @d",
                    AiHelper.P("@kho", maKho), AiHelper.P("@d", maDH));

                StringBuilder sb = new StringBuilder();
                foreach (DataRow r in t.Rows)
                {
                    string ten = Convert.ToString(r["TenSP"]);
                    int xuat = Convert.ToInt32(r["SoXuat"]), tonKho = Convert.ToInt32(r["TonKho"]);
                    int sau = Convert.ToInt32(r["TongTon"]) - xuat;
                    if (xuat > tonKho)
                        sb.AppendLine("- " + ten + ": kho " + maKho + " chỉ còn " + tonKho + ", cần xuất " + xuat + " (không đủ, hệ thống sẽ từ chối).");
                    else if (r["TonKhoAnToan"] != DBNull.Value && sau < Convert.ToInt32(r["TonKhoAnToan"]))
                        sb.AppendLine("- " + ten + ": sau khi xuất tổng tồn còn " + sau + ", thấp hơn tồn dự phòng " + r["TonKhoAnToan"] + ".");
                    else if (r["DiemDatHangLai_ROP"] != DBNull.Value && sau <= Convert.ToInt32(r["DiemDatHangLai_ROP"]))
                        sb.AppendLine("- " + ten + ": sau khi xuất tổng tồn còn " + sau + ", chạm mức phải đặt hàng " + r["DiemDatHangLai_ROP"] + " (nên đặt hàng thêm).");
                }
                if (sb.Length == 0) return true;
                return MessageBox.Show("So với tồn dự phòng / mức phải đặt hàng:\n\n" + sb + "\nVẫn tiếp tục xuất kho?",
                                       "Cảnh báo tồn kho", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
            catch
            {
                return true;
            }
        }
    }
}