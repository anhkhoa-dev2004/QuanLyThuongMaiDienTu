using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    public partial class Form_VanDon : Form
    {
        public Form_VanDon()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            KhoiTaoChucNang();
            GiaoDien.ThemBangDuoi(this, "Đơn chờ giao cho đơn vị vận chuyển và đơn đang giao", 260,
                () => AiHelper.Doc(
                    "SELECT TOP 300 dh.MaDH AS [Mã đơn], dh.NgayDat AS [Ngày đặt], ISNULL(kh.TenKH, N'Khách vãng lai') AS [Khách hàng], " +
                    "dh.TongTien AS [Tổng tiền], dh.TrangThai AS [Trạng thái đơn], ISNULL(vd.MaVanDon, N'Chưa tạo') AS [Vận đơn], " +
                    "vd.DonViVC AS [Đơn vị giao], vd.MaTracking AS [Mã vận đơn], vd.TrangThai AS [Tình trạng giao] " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH " +
                    "OUTER APPLY (SELECT TOP 1 v.MaVanDon, v.DonViVC, v.MaTracking, v.TrangThai FROM VanDon v WHERE v.MaDH = dh.MaDH ORDER BY v.NgayTao DESC) vd " +
                    "WHERE dh.TrangThai IN (N'Chờ lấy hàng', N'Đang giao') " +
                    "ORDER BY CASE dh.TrangThai WHEN N'Chờ lấy hàng' THEN 0 ELSE 1 END, dh.NgayDat"),
                r => txtMaDonHang.Text = Convert.ToString(r.Cells["Mã đơn"].Value));
        }

        private void KhoiTaoChucNang()
        {
            txtDonViVc.TextChanged += (s, e) => CapNhatUyTin();
        }

        private void btnQuetAnh_Click(object sender, EventArgs e)
        {
            using (Form_DocMaVanDonTuAnh frm = new Form_DocMaVanDonTuAnh()) frm.ShowDialog();
            CapNhatThongTinTheoMaDH();
        }

        private void btnDaGiao_Click(object sender, EventArgs e)
        {
            DanhDauKetQua(N_DA_GIAO, null);
        }

        private void btnThatBai_Click(object sender, EventArgs e)
        {
            string lyDo = DonHangShopee.HoiLyDo("Giao không thành công", "Vì sao giao không thành công? Đơn sẽ bị hủy, hàng chuyển sang \"Đang hoàn về\". Khi hàng tới kho, bấm \"Nhận hàng hoàn về kho\" ở Danh sách đơn hàng.",
                new[] { "Khách không nhận hàng", "Không liên lạc được khách", "Sai địa chỉ", "Hàng hư hỏng khi vận chuyển" });
            if (lyDo == null) return;
            DanhDauKetQua(N_THAT_BAI, lyDo);
        }

        private void btnLuuUyTin_Click(object sender, EventArgs e)
        {
            LuuDiemUyTin();
        }

        private const string N_DA_GIAO = "Đã giao";
        private const string N_THAT_BAI = "Giao không thành công";

        private void CapNhatUyTin()
        {
            if (lblUyTin == null) return;
            string dv = txtDonViVc.Text.Trim();
            if (dv.Length == 0) { lblUyTin.Text = ""; return; }
            try
            {
                DataTable t = UyTinVanChuyen.ThongKe(dv);
                lblUyTin.Text = t.Rows.Count == 0
                    ? "Đơn vị vận chuyển \"" + dv + "\": chưa có vận đơn nào trong hệ thống."
                    : "Đơn vị vận chuyển \"" + dv + "\": " + UyTinVanChuyen.MoTa(t.Rows[0]) +
                      ".\nĐiểm = đã giao / (đã giao + giao không thành công) x 100. Ngày giao xong được lưu từ lúc bấm \"Đã giao\".";
            }
            catch (Exception ex) { lblUyTin.Text = "Không tính được điểm uy tín: " + AiHelper.LoiDeHieu(ex); }
        }

        private void DanhDauKetQua(string trangThai, string ghiChu)
        {
            string maDH = txtMaDonHang.Text.Trim();
            if (maDH.Length == 0) { MessageBox.Show("Hãy nhập Mã đơn hàng cần đánh dấu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                AiHelper.ChayThuTuc("sp_CapNhatGiaoHang", AiHelper.P("@MaDH", maDH), AiHelper.P("@KetQua", trangThai), AiHelper.P("@GhiChu", ghiChu));
                MessageBox.Show(trangThai == N_DA_GIAO
                                    ? "Đơn " + maDH + " đã giao xong. Từ giờ đơn được tính phí sàn và tiền shop thực nhận; khách có thể đánh giá hoặc yêu cầu trả hàng."
                                    : "Đơn " + maDH + " giao không thành công: đơn đã chuyển sang \"Đã hủy\", hàng đang trên đường về kho (chưa cộng vào tồn). " +
                                      "Khi hàng tới kho, vào Danh sách đơn hàng bấm \"Nhận hàng hoàn về kho\".",
                                "Đã ghi kết quả giao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapNhatThongTinTheoMaDH();
                CapNhatUyTin();
                GiaoDien.TaiLai(this);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi đánh dấu: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void LuuDiemUyTin()
        {
            string dv = txtDonViVc.Text.Trim();
            if (dv.Length == 0) return;
            try
            {
                DataTable t = UyTinVanChuyen.ThongKe(dv);
                if (t.Rows.Count == 0) { MessageBox.Show("Chưa có vận đơn nào của đơn vị này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                decimal? d = UyTinVanChuyen.Diem(Convert.ToInt32(t.Rows[0]["Đã Giao"]), Convert.ToInt32(t.Rows[0]["Hoàn/Thất Bại"]));
                if (!d.HasValue) { MessageBox.Show("Chưa có vận đơn nào có kết quả (đã giao hoặc giao không thành công) nên chưa tính được điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                AiHelper.Chay("UPDATE VanDon SET DiemUyTin_NVDV = @d WHERE DonViVC = @dv", AiHelper.PDec("@d", d.Value, 5, 2), AiHelper.P("@dv", dv));
                MessageBox.Show("Đã lưu điểm " + d.Value.ToString("0.##") + "/100 cho các vận đơn của \"" + dv + "\".", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapNhatUyTin();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lưu điểm: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void Form_VanDon_Load(object sender, EventArgs e)
        {
            txtMaDonHang.Text = "";
            txtDonViVc.Text = "SPX Express";

            CapNhatThongTinTheoMaDH();
        }

        private void CapNhatThongTinTheoMaDH()
        {
            string maDH = txtMaDonHang.Text.Trim();

            if (string.IsNullOrEmpty(maDH))
            {
                txtMaTracking.Text = "";
                txtTrangThai.Text = "Chờ xử lý";
                return;
            }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkVDQuery = @"
                        SELECT TOP 1 MaTracking, DonViVC, TrangThai
                        FROM VanDon WITH (NOLOCK)
                        WHERE MaDH = @MaDH";

                    bool daCoVanDon = false;

                    using (SqlCommand cmd = new SqlCommand(checkVDQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDH", maDH);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMaTracking.Text = reader["MaTracking"].ToString();
                                txtDonViVc.Text = reader["DonViVC"].ToString();
                                txtTrangThai.Text = reader["TrangThai"].ToString();
                                daCoVanDon = true;
                            }
                        }
                    }

                    if (!daCoVanDon)
                    {
                        string checkDHQuery = "SELECT TOP 1 TrangThai FROM DonHang WITH (NOLOCK) WHERE MaDH = @MaDH";
                        using (SqlCommand cmdDH = new SqlCommand(checkDHQuery, conn))
                        {
                            cmdDH.Parameters.AddWithValue("@MaDH", maDH);
                            object resStatus = cmdDH.ExecuteScalar();

                            txtTrangThai.Text = (resStatus != null) ? "Đơn: " + resStatus : "Không tìm thấy đơn";
                        }

                        txtMaTracking.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    txtTrangThai.Text = "Chưa đọc được: " + AiHelper.LoiDeHieu(ex);
                }
            }
        }

        private void txtMaDonHang_TextChanged(object sender, EventArgs e)
        {
            CapNhatThongTinTheoMaDH();
        }

        private void btnTaoVanDon_Click(object sender, EventArgs e)
        {
            string maDH = txtMaDonHang.Text.Trim();
            string donViVC = txtDonViVc.Text.Trim();
            string maTracking = txtMaTracking.Text.Trim();

            if (string.IsNullOrEmpty(maDH) || string.IsNullOrEmpty(donViVC))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã đơn hàng và Đơn vị vận chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(maTracking))
            {
                MessageBox.Show("Hãy nhập mã vận đơn do đơn vị vận chuyển cấp (in trên tem dán ngoài gói hàng).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maVanDon;
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                using (SqlCommand cmd = new SqlCommand("sp_TaoVanDon", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaDH", SqlDbType.VarChar, 20).Value = maDH;
                    cmd.Parameters.Add("@DonViVC", SqlDbType.NVarChar, 100).Value = donViVC;
                    cmd.Parameters.Add("@MaTracking", SqlDbType.VarChar, 100).Value = maTracking;
                    SqlParameter ra = cmd.Parameters.Add("@MaVanDon", SqlDbType.VarChar, 50);
                    ra.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    maVanDon = Convert.ToString(ra.Value);
                }

                CapNhatThongTinTheoMaDH();
                GiaoDien.TaiLai(this);
                MessageBox.Show($"Đã giao đơn [{maDH}] cho đơn vị vận chuyển.\n" +
                                $"Vận đơn: {maVanDon}\nĐơn vị: {donViVC}\nMã vận đơn: {maTracking}\n" +
                                "Trạng thái đơn: Đang giao. Khi có kết quả, bấm \"Đã giao\" hoặc \"Giao không thành công\".",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa tạo được vận đơn: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaDonHang_Click(object sender, EventArgs e) { }
        private void lblDonViVc_Click(object sender, EventArgs e) { }
        private void txtDonViVc_TextChanged(object sender, EventArgs e) { }
        private void lblMaTracking_Click(object sender, EventArgs e) { }
        private void txtMaTracking_TextChanged(object sender, EventArgs e) { }
        private void lblTrangThai_Click(object sender, EventArgs e) { }
        private void txtTrangThai_TextChanged(object sender, EventArgs e) { }
    }
}