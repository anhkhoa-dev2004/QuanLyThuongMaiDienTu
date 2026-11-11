using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Service_Module
{
    public partial class Form_QuanLyTraHang : Form
    {
        private DataTable _ctDon;
        private bool _dangNap;
        private readonly ComboBox cboTinhTrang;

        public Form_QuanLyTraHang()
        {
            InitializeComponent();
            Label lblTinhTrang = new Label { Text = "Tình trạng hàng:", Location = new Point(190, 318), Size = new Size(150, 22) };
            cboTinhTrang = new ComboBox { Location = new Point(347, 315), Width = 198, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTinhTrang.Items.AddRange(new object[] { "Còn bán được", "Hỏng, không bán lại" });
            cboTinhTrang.SelectedIndex = 0;
            Label lblGhiChuTinhTrang = new Label
            {
                Text = "Còn bán được: hàng cộng lại vào kho. Hỏng: không cộng vào kho, tính là hàng mất.",
                Location = new Point(552, 312), Size = new Size(240, 40), ForeColor = Color.FromArgb(110, 110, 110)
            };
            Controls.AddRange(new Control[] { lblTinhTrang, cboTinhTrang, lblGhiChuTinhTrang });
            GiaoDien.GanChoForm(this);
            GiaoDien.ThemBangDuoi(this, "Hàng khách đã trả gần đây", 250,
                () => AiHelper.Doc(
                    "SELECT TOP 200 th.MaTraHang AS [Mã trả hàng], th.NgayTra AS [Ngày trả], th.MaDH AS [Mã đơn], sp.TenSP AS [Sản phẩm], " +
                    "th.SoLuong AS [Số lượng], th.SoTienHoan AS [Tiền hoàn], th.TinhTrangHang AS [Tình trạng hàng], th.LyDoTra AS [Khách nói], " +
                    "th.NhomLyDoChinh AS [Nhóm lý do], th.CamXucKhachHang AS [Giọng khách] " +
                    "FROM TraHang th LEFT JOIN SanPham sp ON sp.MaSP = th.MaSP ORDER BY th.NgayTra DESC"),
                r => txtMaDonHang.Text = Convert.ToString(r.Cells["Mã đơn"].Value));
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_dangNap) ChonSanPham();
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (!_dangNap) GoiYTienHoan();
        }

        private void CapNhatCamXuc()
        {
            if (lblCamXuc == null) return;
            if (txtLyDoTra.Text.Trim().Length == 0) { lblCamXuc.Text = ""; return; }
            try
            {
                decimal diem; string nhan;
                CamXucTuDien.Cham(txtLyDoTra.Text, out diem, out nhan);
                lblCamXuc.Text = "Giọng khách: " + nhan + "  |  Nhóm lý do trả: " + CamXucTuDien.NhomLyDoTra(txtLyDoTra.Text);
                lblCamXuc.ForeColor = nhan == "Chê" ? System.Drawing.Color.FromArgb(192, 57, 43) : (nhan == "Khen" ? System.Drawing.Color.FromArgb(30, 132, 73) : System.Drawing.Color.FromArgb(110, 110, 110));
            }
            catch (Exception ex) { lblCamXuc.Text = "Chưa chấm được: " + AiHelper.LoiDeHieu(ex); }
        }

        private void CapNhatRuiRo()
        {
            if (lblRuiRo == null) return;
            string maDH = txtMaDonHang.Text.Trim();
            if (maDH.Length == 0) { lblRuiRo.Text = ""; return; }
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT dh.CoNghiNghiepGianLan, ISNULL(kh.DanhSachDen_Blacklist, 0) AS DenDen, " +
                    "       (SELECT COUNT(*) FROM TraHang th JOIN DonHang d2 ON d2.MaDH = th.MaDH " +
                    "        WHERE d2.MaKH = dh.MaKH AND th.NgayTra >= DATEADD(DAY, -90, GETDATE())) AS SoLanTra90 " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH WHERE dh.MaDH = @d", AiHelper.P("@d", maDH));
                if (t.Rows.Count == 0) { lblRuiRo.Text = "Chưa thấy đơn hàng \"" + maDH + "\"."; return; }
                DataRow r = t.Rows[0];
                StringBuilder sb = new StringBuilder();
                if (Convert.ToInt32(r["DenDen"]) == 1) sb.AppendLine("- Khách đang nằm trong danh sách đen.");
                if (r["CoNghiNghiepGianLan"] != DBNull.Value && Convert.ToBoolean(r["CoNghiNghiepGianLan"])) sb.AppendLine("- Đơn này đã bị gắn cờ nghi ngờ gian lận.");
                int soLan = Convert.ToInt32(r["SoLanTra90"]);
                if (soLan >= 2) sb.AppendLine("- Khách này đã trả hàng " + soLan + " lần trong 90 ngày qua (chưa tính phiếu đang lập).");
                lblRuiRo.Text = sb.Length == 0
                    ? "Rủi ro đổi trả: chưa thấy dấu hiệu theo 3 quy tắc (danh sách đen, cờ gian lận, trả hàng nhiều lần). Khách đã trả " + soLan + " lần trong 90 ngày."
                    : "Rủi ro đổi trả: CẦN XEM KỸ trước khi hoàn tiền\n" + sb;
            }
            catch (Exception ex) { lblRuiRo.Text = "Chưa kiểm tra được rủi ro: " + AiHelper.LoiDeHieu(ex); }
        }

        private void NapSanPhamTrongDon()
        {
            if (cboSanPham == null) return;
            _dangNap = true;
            try
            {
                cboSanPham.DataSource = null;
                cboSanPham.Items.Clear();
                _ctDon = null;
                numSoLuong.Minimum = 0; numSoLuong.Maximum = 0; numSoLuong.Value = 0;
                string maDH = txtMaDonHang.Text.Trim();
                if (maDH.Length == 0) { lblConTra.Text = "Nhập Mã đơn hàng để xem các sản phẩm trong đơn."; return; }

                _ctDon = AiHelper.Doc(
                    "SELECT ct.MaSP, ct.MaSP + N' - ' + sp.TenSP AS Hien, ct.SoLuong AS DaMua, ct.DonGia, dh.TongTien, dh.GiamGiaCuaShop, " +
                    "       ISNULL((SELECT SUM(th.SoLuong) FROM TraHang th WHERE th.MaDH = ct.MaDH AND th.MaSP = ct.MaSP), 0) AS DaTra, " +
                    "       (SELECT TOP 1 px.MaKho FROM PhieuXuat px JOIN ChiTietPhieuXuat cx ON cx.MaPX = px.MaPX " +
                    "        WHERE px.MaDH = ct.MaDH AND cx.MaSP = ct.MaSP ORDER BY px.NgayXuat DESC) AS MaKho " +
                    "FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP JOIN DonHang dh ON dh.MaDH = ct.MaDH WHERE ct.MaDH = @d ORDER BY ct.MaSP",
                    AiHelper.P("@d", maDH));
                if (_ctDon.Rows.Count == 0) { lblConTra.Text = "Đơn này không có sản phẩm nào (hoặc chưa có đơn)."; return; }
                cboSanPham.DisplayMember = "Hien";
                cboSanPham.ValueMember = "MaSP";
                cboSanPham.DataSource = _ctDon;
            }
            catch (Exception ex) { lblConTra.Text = "Chưa đọc được đơn: " + AiHelper.LoiDeHieu(ex); }
            finally { _dangNap = false; }
            ChonSanPham();
        }

        private DataRow DongDangChon()
        {
            if (_ctDon == null || cboSanPham.SelectedIndex < 0 || cboSanPham.SelectedIndex >= _ctDon.Rows.Count) return null;
            return _ctDon.Rows[cboSanPham.SelectedIndex];
        }

        private void ChonSanPham()
        {
            DataRow r = DongDangChon();
            if (r == null) return;
            int daMua = Convert.ToInt32(r["DaMua"]), daTra = Convert.ToInt32(r["DaTra"]);
            int con = Math.Max(0, daMua - daTra);
            _dangNap = true;
            numSoLuong.Minimum = 0;
            numSoLuong.Maximum = con;
            numSoLuong.Value = con > 0 ? 1 : 0;
            numSoLuong.Minimum = con > 0 ? 1 : 0;
            _dangNap = false;
            decimal giam = Convert.ToDecimal(r["GiamGiaCuaShop"]);
            lblConTra.Text = "Đã mua " + daMua + ", đã trả " + daTra + ", còn trả được " + con + ". Đơn giá " + Convert.ToDecimal(r["DonGia"]).ToString("N0") + " đ" +
                              (giam > 0 ? " (đơn có giảm giá của shop " + giam.ToString("N0") + " đ, đã chia theo giá trị món: khách thực trả " + ToiDaHoan(r, 1).ToString("N0") + " đ/cái)." : ".") +
                              (r["MaKho"] == DBNull.Value ? "  Sản phẩm này CHƯA xuất kho nên chưa trả được." : "  Hàng còn bán được sẽ về kho " + r["MaKho"] + ".");
            GoiYTienHoan();
        }

        private static decimal ToiDaHoan(DataRow r, decimal soLuong)
        {
            decimal donGia = Convert.ToDecimal(r["DonGia"]);
            decimal tong = Convert.ToDecimal(r["TongTien"]);
            decimal giam = Convert.ToDecimal(r["GiamGiaCuaShop"]);
            if (tong <= 0) return 0;
            return Math.Round(soLuong * donGia * (tong - giam) / tong, 0, MidpointRounding.AwayFromZero);
        }

        private void GoiYTienHoan()
        {
            DataRow r = DongDangChon();
            if (r == null) return;
            txtSoTienHoan.Text = ToiDaHoan(r, numSoLuong.Value).ToString("0");
        }

        private void txtMaDonHang_TextChanged(object sender, EventArgs e)
        {
            CapNhatRuiRo();
            NapSanPhamTrongDon();
        }

        private void txtLyDoTra_TextChanged(object sender, EventArgs e)
        {
            CapNhatCamXuc();
        }

        private void Form_QuanLyTraHang_Load(object sender, EventArgs e)
        {
        }

        private void btnXacNhanTraHang_Click(object sender, EventArgs e)
        {
            string maDH = txtMaDonHang.Text.Trim();
            string lyDo = txtLyDoTra.Text.Trim();

            if (maDH.Length == 0) { CanhBao("Vui lòng nhập Mã đơn hàng!"); return; }
            DataRow r = DongDangChon();
            if (r == null) { CanhBao("Đơn này chưa có sản phẩm nào để trả."); return; }
            if (r["MaKho"] == DBNull.Value) { CanhBao("Sản phẩm này chưa xuất kho cho đơn nên chưa trả hàng được."); return; }
            int soLuong = (int)numSoLuong.Value;
            if (soLuong <= 0) { CanhBao("Sản phẩm này đã trả hết số lượng đã mua."); return; }
            if (lyDo.Length == 0) { CanhBao("Vui lòng nhập lý do trả (dùng để xếp nhóm lý do và chấm khen/chê)."); return; }
            if (lyDo.Length > 255) { CanhBao("Lý do trả dài quá 255 ký tự."); return; }
            if (!decimal.TryParse(txtSoTienHoan.Text.Trim(), out decimal soTienHoan) || soTienHoan < 0)
            {
                CanhBao("Số tiền hoàn phải là một số lớn hơn hoặc bằng 0!");
                return;
            }
            decimal toiDa = ToiDaHoan(r, soLuong);
            if (soTienHoan > toiDa)
            {
                CanhBao("Số tiền hoàn (" + soTienHoan.ToString("N0") + " đ) lớn hơn số tiền khách thực trả cho " + soLuong + " cái (" + toiDa.ToString("N0") + " đ, đã trừ phần giảm giá của shop).");
                return;
            }

            try
            {
                object tt = AiHelper.LayGiaTri("SELECT TrangThai FROM DonHang WHERE MaDH = @d", AiHelper.P("@d", maDH));
                if (tt == null && AiHelper.LayGiaTri("SELECT 1 FROM DonHang WHERE MaDH = @d", AiHelper.P("@d", maDH)) == null)
                {
                    CanhBao("Mã đơn hàng không tồn tại trên hệ thống!");
                    return;
                }
                if (tt != null && Convert.ToString(tt).ToLower().Contains("hủy")) { CanhBao("Đơn này đã hủy nên không trả hàng được."); return; }
                string ttDon = Convert.ToString(tt);
                if (ttDon != "Đã giao" && ttDon != "Trả hàng/Hoàn tiền")
                {
                    CanhBao("Đơn này đang \"" + ttDon + "\", khách chưa nhận hàng nên chưa trả hàng được. Nếu khách không muốn mua nữa thì hủy đơn ở \"Danh sách đơn hàng\".");
                    return;
                }
                string tinhTrang = Convert.ToString(cboTinhTrang.SelectedItem);

                string maSP = Convert.ToString(r["MaSP"]);
                string maTraHang = AiHelper.LayMaMoi("TH");
                AiHelper.Chay(
                    "INSERT INTO TraHang (MaTraHang, MaDH, MaKho, MaSP, SoLuong, LyDoTra, SoTienHoan, NgayTra, TinhTrangHang) " +
                    "VALUES (@MaTH, @MaDH, @MaKho, @MaSP, @SoLuong, @LyDo, @SoTien, GETDATE(), @TinhTrang)",
                    AiHelper.P("@MaTH", maTraHang), AiHelper.P("@MaDH", maDH), AiHelper.P("@MaKho", Convert.ToString(r["MaKho"])),
                    AiHelper.P("@MaSP", maSP), AiHelper.P("@SoLuong", soLuong), AiHelper.P("@LyDo", lyDo),
                    AiHelper.PDec("@SoTien", soTienHoan, 18, 2), AiHelper.P("@TinhTrang", tinhTrang));

                string ketQua = "";
                try
                {
                    AiHelper.ChamLaiTraHangVaDanhGia();
                    DataTable kq = AiHelper.Doc("SELECT NhomLyDoChinh, CamXucKhachHang FROM TraHang WHERE MaTraHang = @m", AiHelper.P("@m", maTraHang));
                    if (kq.Rows.Count > 0)
                        ketQua = "\nNhóm lý do: " + kq.Rows[0]["NhomLyDoChinh"] + "\nKhen/chê: " + kq.Rows[0]["CamXucKhachHang"];
                }
                catch (Exception ex2) { ketQua = "\n(Chưa xếp nhóm lý do được: " + ex2.Message + ")"; }

                MessageBox.Show("Xác nhận trả hàng & hoàn tiền thành công!\nMã phiếu trả: " + maTraHang +
                                "\nSản phẩm: " + maSP + " x " + soLuong + "\nSố tiền hoàn: " + soTienHoan.ToString("N0") + " VNĐ" +
                                "\nTình trạng hàng: " + tinhTrang + (tinhTrang == "Còn bán được" ? " (đã cộng lại vào kho " + r["MaKho"] + ")" : " (không cộng vào kho)") + ketQua,
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XoaForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc/ghi dữ liệu: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void CanhBao(string noiDung)
        {
            MessageBox.Show(noiDung, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XoaForm()
        {
            GiaoDien.TaiLai(this);
            txtMaDonHang.Clear();
            txtLyDoTra.Clear();
            txtSoTienHoan.Clear();
            txtMaDonHang.Focus();
        }
    }
}
