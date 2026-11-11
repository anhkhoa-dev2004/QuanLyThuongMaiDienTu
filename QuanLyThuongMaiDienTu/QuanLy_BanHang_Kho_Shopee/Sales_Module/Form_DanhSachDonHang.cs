using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_DanhSachDonHang : Form
    {
        private const string TAT_CA = "(Tất cả)";

        private readonly DateTimePicker dtTu, dtDen;
        private readonly ComboBox cboTrangThai, cboKenh, cboRuiRo, cboKho;
        private readonly TextBox txtTim;
        private readonly Label lblDem, lblChiTiet;
        private readonly DataGridView dgvDon, dgvChiTiet;
        private readonly Button btnXuatKho, btnHuy, btnNhanHoan;

        private bool _dangNap;

        public Form_DanhSachDonHang()
        {
            Text = "Danh sách đơn hàng";
            Name = "Form_DanhSachDonHang";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1100, 800);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "DANH SÁCH ĐƠN HÀNG", AutoSize = true, Location = new Point(20, 10) };

            Label l1 = Nhan("Từ ngày:", 20, 74, 60);
            dtTu = new DateTimePicker { Location = new Point(82, 71), Width = 110, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };
            Label l2 = Nhan("Đến ngày:", 204, 74, 65);
            dtDen = new DateTimePicker { Location = new Point(272, 71), Width = 110, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };
            Label l3 = Nhan("Trạng thái:", 396, 74, 72);
            cboTrangThai = new ComboBox { Location = new Point(470, 71), Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.Add(TAT_CA);
            cboTrangThai.Items.AddRange(DonHangShopee.TAT_CA_TRANG_THAI);
            cboTrangThai.SelectedIndex = 0;
            Label l4 = Nhan("Kênh:", 644, 74, 40);
            cboKenh = new ComboBox { Location = new Point(686, 71), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            Button btnXem = new Button { Text = "Xem danh sách", Location = new Point(960, 68), Size = new Size(120, 30) };

            Label l5 = Nhan("Tìm mã đơn / tên / SĐT:", 20, 110, 160);
            txtTim = new TextBox { Location = new Point(182, 107), Width = 200 };
            Label l6 = Nhan("Lọc theo rủi ro:", 396, 110, 100);
            cboRuiRo = new ComboBox { Location = new Point(498, 107), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            cboRuiRo.Items.AddRange(new object[] { TAT_CA, "Nguy cơ hủy cao", "Cần xem", "Nghi gian lận" });
            cboRuiRo.SelectedIndex = 0;
            Button btnChamRuiRo = new Button { Text = "Chấm điểm rủi ro hủy đơn", Location = new Point(686, 104), Size = new Size(200, 30) };

            lblDem = new Label { Location = new Point(20, 140), Size = new Size(1060, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvDon = new DataGridView { Location = new Point(20, 166), Size = new Size(1060, 300) };

            Label lblLuong = new Label { Location = new Point(20, 472), Size = new Size(1060, 36), ForeColor = Color.FromArgb(110, 110, 110), Text = DonHangShopee.GIAI_THICH_LUONG_DON };

            Button btnTao = new Button { Text = "Tạo đơn hàng", Location = new Point(20, 512), Size = new Size(140, 32) };
            Label l7 = Nhan("Xuất từ kho:", 180, 518, 80);
            cboKho = new ComboBox { Location = new Point(262, 515), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            btnXuatKho = new Button { Text = "Xác nhận & xuất kho", Location = new Point(492, 512), Size = new Size(170, 32) };
            btnHuy = new Button { Text = "Hủy đơn", Location = new Point(672, 512), Size = new Size(120, 32) };
            btnNhanHoan = new Button { Text = "Nhận hàng hoàn về kho", Location = new Point(800, 512), Size = new Size(152, 32), Enabled = false };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(960, 512), Size = new Size(120, 32) };

            lblChiTiet = new Label { Location = new Point(20, 552), Size = new Size(1060, 62) };
            dgvChiTiet = new DataGridView { Location = new Point(20, 618), Size = new Size(1060, 170) };

            Controls.AddRange(new Control[]
            {
                lblTieuDe, l1, dtTu, l2, dtDen, l3, cboTrangThai, l4, cboKenh, btnXem, l5, txtTim, l6, cboRuiRo, btnChamRuiRo,
                lblDem, dgvDon, lblLuong, btnTao, l7, cboKho, btnXuatKho, btnHuy, btnNhanHoan, btnThoat, lblChiTiet, dgvChiTiet
            });

            btnXem.Click += (s, e) => LoadDanhSachDonHang();
            txtTim.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; LoadDanhSachDonHang(); } };
            cboTrangThai.SelectionChangeCommitted += (s, e) => LoadDanhSachDonHang();
            cboKenh.SelectionChangeCommitted += (s, e) => LoadDanhSachDonHang();
            dtTu.CloseUp += (s, e) => LoadDanhSachDonHang();
            dtDen.CloseUp += (s, e) => LoadDanhSachDonHang();
            cboRuiRo.SelectionChangeCommitted += (s, e) => ApDungLocRuiRo();
            btnChamRuiRo.Click += (s, e) =>
            {
                try { AiHelper.ChayThuTuc("sp_CanhBaoRuiRoHuyDon"); LoadDanhSachDonHang(); }
                catch (Exception ex) { AiHelper.BaoLoi(ex); }
            };
            dgvDon.SelectionChanged += (s, e) => { if (!_dangNap) HienChiTiet(); };
            btnTao.Click += (s, e) =>
            {
                using (Form_LapDonHang f = new Form_LapDonHang()) f.ShowDialog();
                LoadDanhSachDonHang();
            };
            btnXuatKho.Click += (s, e) => XacNhanXuatKho();
            btnHuy.Click += (s, e) => HuyDonDangChon();
            btnNhanHoan.Click += (s, e) => NhanHangHoan();
            btnThoat.Click += (s, e) => Close();

            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvDon);
                AiHelper.DinhDangLuoi(dgvChiTiet);
                NapBoLoc();
                LoadDanhSachDonHang();
            };

            GiaoDien.GanChoForm(this);
        }

        private static Label Nhan(string chu, int x, int y, int rong)
        {
            return new Label { Text = chu, Location = new Point(x, y), Size = new Size(rong, 22) };
        }

        private void NapBoLoc()
        {
            try
            {
                DataTable kenh = DonHangShopee.LayKenhBan();
                DataRow tatCa = kenh.NewRow();
                tatCa["MaDoiTac"] = "";
                tatCa["TenDoiTac"] = TAT_CA;
                kenh.Rows.InsertAt(tatCa, 0);
                AiHelper.NapCombo(cboKenh, kenh, "MaDoiTac", "TenDoiTac");
                AiHelper.NapCombo(cboKho, DonHangShopee.LayKho(), "MaKho", "Hien");

                object moiNhat = AiHelper.LayGiaTri("SELECT MAX(NgayDat) FROM DonHang");
                DateTime den = moiNhat == null ? DateTime.Today : Convert.ToDateTime(moiNhat).Date;
                if (den < DateTime.Today) den = DateTime.Today;
                dtDen.Value = den;
                dtTu.Value = (moiNhat == null ? DateTime.Today : Convert.ToDateTime(moiNhat).Date).AddDays(-30);
            }
            catch (Exception ex) { lblDem.Text = "Chưa đọc được kênh bán / kho: " + AiHelper.LoiDeHieu(ex); }
        }

        private void LoadDanhSachDonHang()
        {
            string tt = cboTrangThai.SelectedIndex <= 0 ? null : Convert.ToString(cboTrangThai.SelectedItem);
            string kenh = cboKenh.SelectedValue == null || Convert.ToString(cboKenh.SelectedValue) == "" ? null : Convert.ToString(cboKenh.SelectedValue);
            string tim = txtTim.Text.Trim().Length == 0 ? null : "%" + txtTim.Text.Trim() + "%";
            _dangNap = true;
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT v.MaDH AS [Mã đơn], v.NgayDat AS [Ngày đặt], ISNULL(kh.TenKH, N'Khách vãng lai') AS [Khách hàng], " +
                    "       v.KenhBan AS [Kênh bán], v.TrangThai AS [Trạng thái], dh.HinhThucNhan AS [Nhận hàng], " +
                    "       ISNULL((SELECT TOP 1 CASE WHEN h.TrangThai = N'Đang hoàn về' THEN N'Đang hoàn về' ELSE N'Đã về kho' END FROM HangHoanVeKho h " +
                    "               WHERE h.MaDH = dh.MaDH ORDER BY CASE WHEN h.TrangThai = N'Đang hoàn về' THEN 0 ELSE 1 END), N'') AS [Hàng hoàn], " +
                    "       dh.TongTien AS [Tiền hàng], " +
                    "       dh.GiamGiaCuaShop AS [Giảm giá của shop], v.PhiSan AS [Phí sàn], v.TienHoanKhach AS [Tiền hoàn khách], " +
                    "       v.TienShopNhan AS [Shop thực nhận], v.LaiGop AS [Lãi gộp], ISNULL(dh.PhuongThucThanhToan, N'') AS [Khách trả bằng], " +
                    "       dh.TyLeRuiRoHuyDon AS [Điểm rủi ro hủy], ISNULL(dh.TrangThaiCanhBaoHuy, N'') AS [Cảnh báo hủy], " +
                    "       CASE WHEN dh.CoNghiNghiepGianLan = 1 THEN N'Có' ELSE N'' END AS [Nghi gian lận], ISNULL(dh.LyDoHuy, N'') AS [Lý do hủy] " +
                    "FROM dbo.vw_TienDonHang v " +
                    "JOIN DonHang dh ON dh.MaDH = v.MaDH " +
                    "LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH " +
                    "WHERE v.NgayDat >= @tu AND v.NgayDat < DATEADD(DAY, 1, @den) " +
                    "  AND (@tt IS NULL OR v.TrangThai = @tt) AND (@k IS NULL OR v.MaDoiTac = @k) " +
                    "  AND (@tim IS NULL OR v.MaDH LIKE @tim OR kh.TenKH LIKE @tim OR kh.SoDT LIKE @tim OR dh.SdtNguoiNhan LIKE @tim OR dh.TenNguoiNhan LIKE @tim) " +
                    "ORDER BY v.NgayDat DESC",
                    AiHelper.P("@tu", dtTu.Value.Date), AiHelper.P("@den", dtDen.Value.Date),
                    AiHelper.P("@tt", tt), AiHelper.P("@k", kenh), AiHelper.P("@tim", tim));
                dgvDon.DataSource = dt;
                ApDungLocRuiRo();
                DemTheoTrangThai(dt);
            }
            catch (Exception ex)
            {
                lblDem.Text = "Chưa tải được danh sách đơn: " + AiHelper.LoiDeHieu(ex);
            }
            finally { _dangNap = false; }
            HienChiTiet();
        }

        private void DemTheoTrangThai(DataTable dt)
        {
            StringBuilder sb = new StringBuilder("Có " + dt.Rows.Count + " đơn:");
            foreach (string tt in DonHangShopee.TAT_CA_TRANG_THAI)
            {
                int n = 0;
                foreach (DataRow r in dt.Rows) if (Convert.ToString(r["Trạng thái"]) == tt) n++;
                if (n > 0) sb.Append("  " + tt + " " + n + "  |");
            }
            lblDem.Text = sb.ToString().TrimEnd('|', ' ');
        }

        private void ApDungLocRuiRo()
        {
            DataTable dt = dgvDon.DataSource as DataTable;
            if (dt == null) return;
            string loc = "";
            if (cboRuiRo.SelectedIndex == 1) loc = "[Cảnh báo hủy] = 'Nguy cơ cao'";
            else if (cboRuiRo.SelectedIndex == 2) loc = "[Cảnh báo hủy] = 'Cần xem'";
            else if (cboRuiRo.SelectedIndex == 3) loc = "[Nghi gian lận] = 'Có'";
            dt.DefaultView.RowFilter = loc;

            foreach (DataGridViewRow dong in dgvDon.Rows)
            {
                string gianLan = Convert.ToString(dong.Cells["Nghi gian lận"].Value);
                string canhBao = Convert.ToString(dong.Cells["Cảnh báo hủy"].Value);
                if (gianLan == "Có") dong.DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 205);
                else if (canhBao == "Nguy cơ cao") dong.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 178);
                else if (canhBao == "Cần xem") dong.DefaultCellStyle.BackColor = Color.FromArgb(255, 249, 196);
            }
        }

        private string MaDonDangChon()
        {
            if (dgvDon.CurrentRow == null || dgvDon.CurrentRow.Index < 0) return null;
            return Convert.ToString(dgvDon.CurrentRow.Cells["Mã đơn"].Value);
        }

        private string TrangThaiDangChon()
        {
            if (dgvDon.CurrentRow == null || dgvDon.CurrentRow.Index < 0) return null;
            return Convert.ToString(dgvDon.CurrentRow.Cells["Trạng thái"].Value);
        }

        private void HienChiTiet()
        {
            string ma = MaDonDangChon();
            string tt = TrangThaiDangChon();
            btnXuatKho.Enabled = tt == DonHangShopee.CHO_XAC_NHAN;
            btnHuy.Enabled = tt == DonHangShopee.CHO_XAC_NHAN || tt == DonHangShopee.CHO_LAY_HANG;
            btnNhanHoan.Enabled = dgvDon.CurrentRow != null && dgvDon.CurrentRow.Index >= 0 &&
                                  Convert.ToString(dgvDon.CurrentRow.Cells["Hàng hoàn"].Value) == "Đang hoàn về";
            if (string.IsNullOrEmpty(ma)) { lblChiTiet.Text = "Bấm vào một đơn để xem chi tiết."; dgvChiTiet.DataSource = null; return; }
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT dh.NgayDat, dh.NgayXacNhan, dh.NgayGiao, dh.NgayHuy, dh.LyDoHuy, dh.HinhThucNhan, ISNULL(dh.PhuongThucThanhToan, N'') AS PhuongThucThanhToan, " +
                    "       ISNULL(dh.TenNguoiNhan, kh.TenKH) AS TenNguoiNhan, ISNULL(dh.SdtNguoiNhan, kh.SoDT) AS SoDT, ISNULL(dh.DiaChiGiao, kh.DiaChi) AS DiaChi, " +
                    "       (SELECT TOP 1 px.MaPX + N' (kho ' + px.MaKho + N')' FROM PhieuXuat px WHERE px.MaDH = dh.MaDH ORDER BY px.NgayXuat DESC) AS PhieuXuat, " +
                    "       (SELECT TOP 1 vd.DonViVC + N' - ' + ISNULL(vd.MaTracking, N'') + N' - ' + ISNULL(vd.TrangThai, N'') FROM VanDon vd WHERE vd.MaDH = dh.MaDH ORDER BY vd.NgayTao DESC) AS VanDon, " +
                    "       v.KhachThanhToan, v.PhiXuLyGiaoDich, v.PhiCoDinh, v.PhiKhac " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH JOIN dbo.vw_TienDonHang v ON v.MaDH = dh.MaDH WHERE dh.MaDH = @d",
                    AiHelper.P("@d", ma));
                if (t.Rows.Count > 0)
                {
                    DataRow r = t.Rows[0];
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Đơn " + ma + ":  đặt " + Ngay(r["NgayDat"]) + "  →  xác nhận " + Ngay(r["NgayXacNhan"]) +
                              "  →  giao xong " + Ngay(r["NgayGiao"]) + (r["NgayHuy"] == DBNull.Value ? "" : "  |  HỦY " + Ngay(r["NgayHuy"]) + ": " + r["LyDoHuy"]));
                    sb.Append("\nNgười nhận: " + r["TenNguoiNhan"] + " - " + r["SoDT"] + " - " + r["DiaChi"] + " (" + r["HinhThucNhan"] + ", trả bằng " + r["PhuongThucThanhToan"] + ")" +
                              "  |  Phiếu xuất: " + (r["PhieuXuat"] == DBNull.Value ? "chưa có" : r["PhieuXuat"]) +
                              "  |  Vận đơn: " + (r["VanDon"] == DBNull.Value ? "chưa có" : r["VanDon"]));
                    sb.Append("\nKhách phải trả: " + So(r["KhachThanhToan"]) + " đ  |  Phí xử lý giao dịch: " + So(r["PhiXuLyGiaoDich"]) +
                              " đ  |  Phí cố định: " + So(r["PhiCoDinh"]) + " đ  |  Phí khác: " + So(r["PhiKhac"]) + " đ (phí sàn chỉ tính khi đơn đã giao)");
                    lblChiTiet.Text = sb.ToString();
                }
                dgvChiTiet.DataSource = AiHelper.Doc(
                    "SELECT ct.MaSP AS [Mã SP], sp.TenSP AS [Tên sản phẩm], ct.SoLuong AS [Số lượng], ct.DonGia AS [Đơn giá], " +
                    "       ct.ThanhTien AS [Thành tiền], ct.GiaVon AS [Giá vốn 1 cái], " +
                    "       ISNULL((SELECT SUM(th.SoLuong) FROM TraHang th WHERE th.MaDH = ct.MaDH AND th.MaSP = ct.MaSP), 0) AS [Khách đã trả] " +
                    "FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP WHERE ct.MaDH = @d ORDER BY ct.MaSP",
                    AiHelper.P("@d", ma));
            }
            catch (Exception ex) { lblChiTiet.Text = "Chưa đọc được chi tiết đơn: " + AiHelper.LoiDeHieu(ex); }
        }

        private static string Ngay(object v)
        {
            return v == null || v == DBNull.Value ? "(chưa)" : Convert.ToDateTime(v).ToString("dd/MM/yyyy HH:mm");
        }

        private static string So(object v)
        {
            return v == null || v == DBNull.Value ? "0" : Convert.ToDecimal(v).ToString("N0");
        }

        private void XacNhanXuatKho()
        {
            string ma = MaDonDangChon();
            if (string.IsNullOrEmpty(ma)) return;
            if (cboKho.SelectedValue == null) { AiHelper.BaoTin("Hãy chọn kho xuất."); return; }
            string kho = Convert.ToString(cboKho.SelectedValue);
            if (MessageBox.Show("Xác nhận đơn " + ma + " và xuất hàng từ kho " + kho + "?", "Xác nhận đơn",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                string maPX = DonHangShopee.XacNhanVaXuatKho(ma, kho);
                AiHelper.BaoTin(DonHangShopee.ThongBaoSauXuatKho(ma, maPX));
                LoadDanhSachDonHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void HuyDonDangChon()
        {
            string ma = MaDonDangChon();
            if (string.IsNullOrEmpty(ma)) return;
            string lyDo = DonHangShopee.HoiLyDo("Hủy đơn " + ma, "Vì sao hủy đơn? Chọn hoặc gõ lý do. Hàng đã xuất kho (chưa giao cho đơn vị vận chuyển) sẽ vào lại kho cũ.",
                new[] { "Khách đổi ý, không mua nữa", "Khách đặt trùng đơn", "Không liên lạc được khách", "Sai địa chỉ hoặc số điện thoại", "Shop hết hàng" });
            if (lyDo == null) return;
            try
            {
                DonHangShopee.HuyDon(ma, lyDo);
                AiHelper.BaoTin("Đã hủy đơn " + ma + ".");
                LoadDanhSachDonHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NhanHangHoan()
        {
            string ma = MaDonDangChon();
            if (string.IsNullOrEmpty(ma)) return;
            if (MessageBox.Show("Đã nhận đủ hàng của đơn " + ma + " về kho?\nBấm Có thì số hàng này được cộng lại vào tồn kho.", "Nhận hàng hoàn",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                DonHangShopee.NhanHangHoanVeKho(ma);
                AiHelper.BaoTin("Đã cộng hàng của đơn " + ma + " vào lại tồn kho.");
                LoadDanhSachDonHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}
