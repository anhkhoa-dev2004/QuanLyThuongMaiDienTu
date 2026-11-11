using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_LapDonHang : Form
    {
        private readonly TextBox txtSdt, txtTenKhach, txtDiaChi, txtGiamGia, txtPhiShip;
        private readonly Label lblKhach, lblConBan, lblTien, lblRuiRo;
        private readonly ComboBox cboKenh, cboThanhToan, cboSanPham;
        private readonly NumericUpDown numSoLuong;
        private readonly CheckBox chkTaiQuay;
        private readonly DataGridView dgvGio;

        private readonly DataTable _gio = new DataTable();
        private DataTable _sanPham;
        private string _maKH;
        private bool _khachDen;
        private MoHinhHuyDon.KetQua _moHinh;

        public Form_LapDonHang()
        {
            Text = "Lập đơn hàng mới";
            Name = "Form_LapDonHang";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(980, 710);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "LẬP ĐƠN HÀNG MỚI", AutoSize = true, Location = new Point(20, 10) };

            Label l1 = NhanO("Số điện thoại:", 20, 74, 105);
            txtSdt = new TextBox { Location = new Point(130, 72), Width = 160 };
            Label l2 = NhanO("Tên khách:", 310, 74, 75);
            txtTenKhach = new TextBox { Location = new Point(390, 72), Width = 220 };
            lblKhach = new Label { Location = new Point(625, 66), Size = new Size(340, 36), ForeColor = Color.FromArgb(52, 73, 94) };

            Label l3 = NhanO("Địa chỉ giao:", 20, 106, 105);
            txtDiaChi = new TextBox { Location = new Point(130, 104), Width = 480 };
            chkTaiQuay = new CheckBox { Text = "Khách nhận tại cửa hàng (bán tại quầy)", Location = new Point(625, 104), Size = new Size(340, 24), Enabled = false };

            Label l4 = NhanO("Kênh bán:", 20, 140, 105);
            cboKenh = new ComboBox { Location = new Point(130, 138), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l5 = NhanO("Khách trả bằng:", 370, 140, 105);
            cboThanhToan = new ComboBox { Location = new Point(480, 138), Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            cboThanhToan.Items.AddRange(DonHangShopee.CACH_TRA_TIEN);
            cboThanhToan.SelectedIndex = 0;

            Label l6 = NhanO("Sản phẩm:", 20, 182, 105);
            cboSanPham = new ComboBox { Location = new Point(130, 180), Width = 520, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l7 = NhanO("Số lượng:", 665, 182, 70);
            numSoLuong = new NumericUpDown { Location = new Point(740, 180), Width = 70, Minimum = 1, Maximum = 100000, Value = 1 };
            Button btnThem = new Button { Text = "Thêm vào đơn", Location = new Point(822, 177), Size = new Size(140, 30) };
            lblConBan = new Label { Location = new Point(130, 210), Size = new Size(830, 22), ForeColor = Color.FromArgb(110, 110, 110) };

            dgvGio = new DataGridView { Location = new Point(20, 238), Size = new Size(942, 196) };
            Button btnBo = new Button { Text = "Bỏ món đang chọn", Location = new Point(20, 442), Size = new Size(170, 30) };

            Label l8 = NhanO("Giảm giá của shop (đ):", 20, 488, 160);
            txtGiamGia = new TextBox { Location = new Point(185, 486), Width = 140, Text = "0" };
            Label l9 = NhanO("Phí ship khách trả (đ):", 350, 488, 160);
            txtPhiShip = new TextBox { Location = new Point(515, 486), Width = 140, Text = "0" };

            lblTien = new Label { Location = new Point(20, 520), Size = new Size(942, 78), Font = new Font("Segoe UI", 9.5F) };
            lblRuiRo = new Label { Location = new Point(20, 602), Size = new Size(942, 56), ForeColor = Color.FromArgb(120, 70, 20) };

            Button btnChot = new Button { Text = "Chốt đơn", Location = new Point(620, 666), Size = new Size(170, 34) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(800, 666), Size = new Size(162, 34) };

            Controls.AddRange(new Control[]
            {
                lblTieuDe, l1, txtSdt, l2, txtTenKhach, lblKhach, l3, txtDiaChi, chkTaiQuay, l4, cboKenh, l5, cboThanhToan,
                l6, cboSanPham, l7, numSoLuong, btnThem, lblConBan, dgvGio, btnBo, l8, txtGiamGia, l9, txtPhiShip,
                lblTien, lblRuiRo, btnChot, btnThoat
            });

            _gio.Columns.Add("Mã SP", typeof(string));
            _gio.Columns.Add("Tên sản phẩm", typeof(string));
            _gio.Columns.Add("Số lượng", typeof(int));
            _gio.Columns.Add("Đơn giá", typeof(decimal));
            _gio.Columns.Add("Thành tiền", typeof(decimal));

            txtSdt.Leave += (s, e) => TimKhachTheoSdt();
            txtSdt.TextChanged += (s, e) =>
            {
                if (new string(txtSdt.Text.Where(char.IsDigit).ToArray()).Length >= 10) TimKhachTheoSdt();
                else { _maKH = null; _khachDen = false; lblKhach.Text = ""; }
                CapNhatRuiRo();
            };
            txtDiaChi.TextChanged += (s, e) => CapNhatRuiRo();
            cboKenh.SelectionChangeCommitted += (s, e) => { CapNhatTheoKenh(); CapNhatTien(); };
            chkTaiQuay.CheckedChanged += (s, e) => { txtDiaChi.Enabled = !chkTaiQuay.Checked; CapNhatRuiRo(); };
            cboThanhToan.SelectedIndexChanged += (s, e) => CapNhatRuiRo();
            cboSanPham.SelectedIndexChanged += (s, e) => HienThongTinSanPham();
            txtGiamGia.TextChanged += (s, e) => CapNhatTien();
            txtPhiShip.TextChanged += (s, e) => CapNhatTien();
            btnThem.Click += (s, e) => ThemVaoGio();
            btnBo.Click += (s, e) => BoMonDangChon();
            btnChot.Click += (s, e) => ChotDon();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvGio);
                dgvGio.DataSource = _gio;
                NapTatCa();
            };

            GiaoDien.GanChoForm(this);
        }

        private Label NhanO(string chu, int x, int y, int rong)
        {
            return new Label { Text = chu, Location = new Point(x, y), Size = new Size(rong, 22) };
        }

        private void NapTatCa()
        {
            try
            {
                object kenhCu = cboKenh.SelectedValue;
                AiHelper.NapCombo(cboKenh, DonHangShopee.LayKenhBan(), "MaDoiTac", "TenDoiTac");
                if (kenhCu != null) cboKenh.SelectedValue = kenhCu;
                CapNhatTheoKenh();

                _sanPham = AiHelper.Doc(
                    "SELECT sp.MaSP, sp.TenSP, ISNULL(sp.GiaVon, 0) AS GiaVon, " +
                    "       ISNULL(km.Gia, ISNULL(sp.GiaBan, 0)) AS Gia, CASE WHEN km.Gia IS NULL THEN 0 ELSE 1 END AS DangGiam, " +
                    "       ISNULL(ton.Ton, 0) - ISNULL(giu.Giu, 0) AS CoTheBan " +
                    "FROM SanPham sp " +
                    "OUTER APPLY (SELECT MIN(k.GiaApDung) AS Gia FROM LichSuGiaKhuyenMai k WHERE k.MaSP = sp.MaSP AND CAST(GETDATE() AS DATE) BETWEEN k.TuNgay AND k.DenNgay) km " +
                    "OUTER APPLY (SELECT SUM(t.SoLuongTon) AS Ton FROM TonKho t WHERE t.MaSP = sp.MaSP) ton " +
                    "OUTER APPLY (SELECT SUM(ct.SoLuong) AS Giu FROM ChiTietDonHang ct JOIN DonHang dh ON dh.MaDH = ct.MaDH " +
                    "             WHERE ct.MaSP = sp.MaSP AND dh.TrangThai = N'Chờ xác nhận') giu " +
                    "WHERE sp.DangKinhDoanh = 1 " +
                    "ORDER BY sp.TenSP");
                _sanPham.Columns.Add("Hien", typeof(string));
                foreach (DataRow r in _sanPham.Rows)
                    r["Hien"] = r["MaSP"] + " - " + r["TenSP"] + "  |  " + Convert.ToDecimal(r["Gia"]).ToString("N0") + " đ" +
                                (Convert.ToInt32(r["DangGiam"]) == 1 ? " (đang giảm giá)" : "") +
                                "  |  còn bán được " + Math.Max(0, Convert.ToInt32(r["CoTheBan"]));
                AiHelper.NapCombo(cboSanPham, _sanPham, "MaSP", "Hien");
                HienThongTinSanPham();
                CapNhatTien();
                CapNhatRuiRo();
            }
            catch (Exception ex)
            {
                lblConBan.Text = "Chưa đọc được danh sách sản phẩm: " + AiHelper.LoiDeHieu(ex);
            }
        }

        private bool KenhLaCuaHang()
        {
            DataRowView v = cboKenh.SelectedItem as DataRowView;
            return v != null && Convert.ToString(v["LoaiKenh"]) == "Cửa hàng";
        }

        private void CapNhatTheoKenh()
        {
            bool cuaHang = KenhLaCuaHang();
            chkTaiQuay.Enabled = cuaHang;
            if (!cuaHang) chkTaiQuay.Checked = false;
        }

        private string LoiCachTraTien()
        {
            string cach = Convert.ToString(cboThanhToan.SelectedItem);
            DataRowView v = cboKenh.SelectedItem as DataRowView;
            string tenKenh = v == null ? "" : Convert.ToString(v["TenDoiTac"]);
            if (cach == "Tiền mặt tại cửa hàng" && !chkTaiQuay.Checked)
                return "\"Tiền mặt tại cửa hàng\" chỉ dùng khi khách nhận hàng tại quầy. Giao tận nơi mà thu tiền mặt thì chọn COD.";
            if (cach == "Ví ShopeePay" && tenKenh.IndexOf("Shopee", StringComparison.OrdinalIgnoreCase) < 0)
                return "Ví ShopeePay chỉ có khi khách đặt trên Shopee.";
            if (_khachDen && cach.Contains("COD"))
                return "Khách này nằm trong danh sách đen nên không nhận COD. Hãy để khách trả trước (chuyển khoản, thẻ, ví).";
            return null;
        }

        private DataRow SanPhamDangChon()
        {
            if (_sanPham == null || cboSanPham.SelectedIndex < 0 || cboSanPham.SelectedIndex >= _sanPham.Rows.Count) return null;
            return _sanPham.Rows[cboSanPham.SelectedIndex];
        }

        private int DaCoTrongGio(string maSP)
        {
            foreach (DataRow r in _gio.Rows)
                if (Convert.ToString(r["Mã SP"]) == maSP) return Convert.ToInt32(r["Số lượng"]);
            return 0;
        }

        private void HienThongTinSanPham()
        {
            DataRow r = SanPhamDangChon();
            if (r == null) { lblConBan.Text = ""; return; }
            int coThe = Math.Max(0, Convert.ToInt32(r["CoTheBan"]));
            int trongGio = DaCoTrongGio(Convert.ToString(r["MaSP"]));
            lblConBan.Text = "Giá bán hôm nay: " + Convert.ToDecimal(r["Gia"]).ToString("N0") + " đ" +
                             (Convert.ToInt32(r["DangGiam"]) == 1 ? " (giá đợt giảm giá)" : "") +
                             ".  Còn bán được: " + coThe + " cái (tồn các kho trừ hàng đang giữ cho đơn chờ xác nhận)" +
                             (trongGio > 0 ? ".  Đơn này đã có " + trongGio + " cái." : ".");
        }

        private void ThemVaoGio()
        {
            DataRow r = SanPhamDangChon();
            if (r == null) { BaoNhe("Hãy chọn sản phẩm."); return; }
            string maSP = Convert.ToString(r["MaSP"]);
            int them = (int)numSoLuong.Value;
            int coThe = Math.Max(0, Convert.ToInt32(r["CoTheBan"]));
            int moi = DaCoTrongGio(maSP) + them;
            if (moi > coThe)
            {
                BaoNhe("\"" + r["TenSP"] + "\" chỉ còn bán được " + coThe + " cái, không đủ " + moi + " cái.");
                return;
            }
            decimal gia = Convert.ToDecimal(r["Gia"]);
            if (gia <= 0) { BaoNhe("Sản phẩm này chưa có giá bán. Hãy nhập giá ở màn Sản phẩm trước."); return; }
            decimal giaVon = Convert.ToDecimal(r["GiaVon"]);
            if (giaVon > 0 && gia < giaVon && DaCoTrongGio(maSP) == 0 &&
                MessageBox.Show("\"" + r["TenSP"] + "\" đang bán " + gia.ToString("N0") + " đ, thấp hơn giá vốn " + giaVon.ToString("N0") +
                                " đ.\nMỗi cái lỗ " + (giaVon - gia).ToString("N0") + " đ (chưa tính phí sàn). Vẫn thêm vào đơn?",
                                "Bán dưới giá vốn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            DataRow dong = _gio.Rows.Cast<DataRow>().FirstOrDefault(x => Convert.ToString(x["Mã SP"]) == maSP);
            if (dong == null)
            {
                dong = _gio.NewRow();
                dong["Mã SP"] = maSP;
                dong["Tên sản phẩm"] = r["TenSP"];
                _gio.Rows.Add(dong);
            }
            dong["Số lượng"] = moi;
            dong["Đơn giá"] = gia;
            dong["Thành tiền"] = gia * moi;
            numSoLuong.Value = 1;
            HienThongTinSanPham();
            CapNhatTien();
            CapNhatRuiRo();
        }

        private void BoMonDangChon()
        {
            if (dgvGio.CurrentRow == null || dgvGio.CurrentRow.Index < 0 || dgvGio.CurrentRow.Index >= _gio.Rows.Count) return;
            _gio.Rows.RemoveAt(dgvGio.CurrentRow.Index);
            HienThongTinSanPham();
            CapNhatTien();
            CapNhatRuiRo();
        }

        private decimal TienHang()
        {
            decimal tong = 0;
            foreach (DataRow r in _gio.Rows) tong += Convert.ToDecimal(r["Thành tiền"]);
            return tong;
        }

        private static bool DocSoTien(string s, out decimal so)
        {
            string chi = (s ?? "").Replace(".", "").Replace(",", "").Replace(" ", "").Trim();
            if (chi.Length == 0) { so = 0; return true; }
            return decimal.TryParse(chi, NumberStyles.Integer, CultureInfo.InvariantCulture, out so) && so >= 0;
        }

        private void CapNhatTien()
        {
            decimal tienHang = TienHang(), giam, ship;
            bool giamDung = DocSoTien(txtGiamGia.Text, out giam), shipDung = DocSoTien(txtPhiShip.Text, out ship);
            StringBuilder sb = new StringBuilder();
            sb.Append("Tiền hàng: " + tienHang.ToString("N0") + " đ   |   Giảm giá của shop: " + (giamDung ? giam.ToString("N0") + " đ" : "ô nhập sai") +
                      "   |   Phí ship khách trả: " + (shipDung ? ship.ToString("N0") + " đ" : "ô nhập sai"));
            if (!giamDung || !shipDung) { lblTien.Text = sb + "\nHai ô giảm giá và phí ship chỉ nhập số (ví dụ 20000)."; return; }
            if (giam > tienHang) sb.Append("\nGiảm giá đang lớn hơn tiền hàng.");
            sb.Append("\nKhách phải trả: " + (tienHang - giam + ship).ToString("N0") + " đ (phí ship khách trả đi thẳng cho đơn vị vận chuyển, không phải tiền của shop).");
            try
            {
                string chiTiet;
                Dictionary<string, decimal> tienTheoMaSP = new Dictionary<string, decimal>();
                foreach (DataRow r in _gio.Rows)
                {
                    string ma = Convert.ToString(r["Mã SP"]);
                    decimal tt;
                    tienTheoMaSP.TryGetValue(ma, out tt);
                    tienTheoMaSP[ma] = tt + Convert.ToDecimal(r["Thành tiền"]);
                }
                decimal phi = DonHangShopee.UocTinhPhiSan(Convert.ToString(cboKenh.SelectedValue), tienTheoMaSP, giam, ship, DateTime.Now, out chiTiet);
                sb.Append("\nPhí sàn ước tính khi giao xong: " + phi.ToString("N0") + " đ (" + chiTiet + ").  Shop ước nhận: " +
                          (tienHang - giam - phi).ToString("N0") + " đ.");
            }
            catch (Exception ex) { sb.Append("\nChưa tính được phí sàn: " + AiHelper.LoiDeHieu(ex)); }
            lblTien.Text = sb.ToString();
        }

        private void TimKhachTheoSdt()
        {
            string sdt = txtSdt.Text.Trim();
            if (sdt.Length == 0) return;
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT TOP 1 MaKH, TenKH, DiaChi, ISNULL(DanhSachDen_Blacklist, 0) AS Den FROM KhachHang " +
                    "WHERE dbo.fn_ChuanHoaSoDienThoai(SoDT) = dbo.fn_ChuanHoaSoDienThoai(@s) ORDER BY MaKH",
                    AiHelper.P("@s", sdt));
                if (t.Rows.Count == 0)
                {
                    _maKH = null;
                    _khachDen = false;
                    lblKhach.Text = "Khách mới. Nhập tên và địa chỉ, khách sẽ được lưu khi chốt đơn.";
                    return;
                }
                DataRow r = t.Rows[0];
                _maKH = Convert.ToString(r["MaKH"]);
                _khachDen = Convert.ToInt32(r["Den"]) == 1;
                txtTenKhach.Text = Convert.ToString(r["TenKH"]);
                if (txtDiaChi.Text.Trim().Length == 0) txtDiaChi.Text = Convert.ToString(r["DiaChi"]);
                lblKhach.Text = "Khách cũ: " + _maKH + (_khachDen ? " - ĐANG NẰM TRONG DANH SÁCH ĐEN" : "");
                lblKhach.ForeColor = _khachDen ? Color.FromArgb(192, 57, 43) : Color.FromArgb(30, 132, 73);
            }
            catch (Exception ex) { lblKhach.Text = "Chưa tìm được khách: " + AiHelper.LoiDeHieu(ex); }
        }

        private static bool SdtLoi(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return true;
            string d = new string(s.Where(char.IsDigit).ToArray());
            if (d.Length == 11 && d.StartsWith("84")) d = "0" + d.Substring(2);
            return !(d.Length == 10 && d[0] == '0');
        }

        private static bool DiaChiLoi(string s)
        {
            return string.IsNullOrWhiteSpace(s) || s.Trim().Length < 10;
        }

        private void CapNhatRuiRo()
        {
            if (lblRuiRo == null) return;
            if (txtSdt.Text.Trim().Length == 0 && _gio.Rows.Count == 0) { lblRuiRo.Text = "Nhập SĐT khách và thêm hàng để xem kiểm tra rủi ro hủy đơn."; return; }
            try
            {
                bool sdtLoi = SdtLoi(txtSdt.Text), dcLoi = !chkTaiQuay.Checked && DiaChiLoi(txtDiaChi.Text);
                StringBuilder sb = new StringBuilder();
                if (_khachDen) sb.Append("Khách nằm trong danh sách đen. ");
                if (sdtLoi) sb.Append("SĐT chưa đúng (cần 10 số). ");
                if (dcLoi) sb.Append("Địa chỉ quá ngắn. ");
                if (_moHinh == null) _moHinh = MoHinhHuyDon.Hoc();
                if (_moHinh.DaHoc)
                {
                    double? p = MoHinhHuyDon.DuDoan(_moHinh, new double[]
                    {
                        sdtLoi ? 1 : 0, dcLoi ? 1 : 0, (double)TienHang(),
                        (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) ? 1 : 0, DateTime.Now.Hour,
                        Convert.ToString(cboThanhToan.SelectedItem).Contains("COD") ? 1 : 0
                    });
                    if (p.HasValue) sb.Append("Đơn này giống khoảng " + (p.Value * 100).ToString("0") + "% các đơn từng bị hủy. " + _moHinh.ThongBao);
                }
                else sb.Append(_moHinh.ThongBao);
                lblRuiRo.Text = "Kiểm tra rủi ro hủy đơn: " + sb;
            }
            catch (Exception ex) { lblRuiRo.Text = "Chưa kiểm tra được rủi ro: " + AiHelper.LoiDeHieu(ex); }
        }

        private void ChotDon()
        {
            if (_gio.Rows.Count == 0) { BaoNhe("Đơn chưa có món nào. Hãy chọn sản phẩm rồi bấm \"Thêm vào đơn\"."); return; }
            if (cboKenh.SelectedValue == null) { BaoNhe("Hãy chọn kênh bán."); return; }
            if (string.IsNullOrEmpty(_maKH))
            {
                if (txtTenKhach.Text.Trim().Length == 0) { BaoNhe("Hãy nhập tên khách."); return; }
                if (SdtLoi(txtSdt.Text)) { BaoNhe("SĐT khách chưa đúng (cần 10 số, bắt đầu bằng số 0)."); return; }
            }
            if (!chkTaiQuay.Checked && DiaChiLoi(txtDiaChi.Text)) { BaoNhe("Địa chỉ giao hàng quá ngắn, hãy ghi đủ số nhà, đường, quận/huyện, tỉnh/thành."); return; }
            string loiTraTien = LoiCachTraTien();
            if (loiTraTien != null) { BaoNhe(loiTraTien); return; }
            decimal giam, ship;
            if (!DocSoTien(txtGiamGia.Text, out giam) || !DocSoTien(txtPhiShip.Text, out ship)) { BaoNhe("Ô giảm giá và phí ship chỉ nhập số, không âm."); return; }
            if (giam > TienHang()) { BaoNhe("Giảm giá của shop lớn hơn tiền hàng."); return; }
            if (_khachDen && MessageBox.Show("Khách này đang nằm trong danh sách đen. Vẫn chốt đơn?", "Cảnh báo khách hàng",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            DataTable ds = new DataTable();
            ds.Columns.Add("MaSP", typeof(string));
            ds.Columns.Add("SoLuong", typeof(int));
            foreach (DataRow r in _gio.Rows) ds.Rows.Add(Convert.ToString(r["Mã SP"]), Convert.ToInt32(r["Số lượng"]));

            try
            {
                string maDH;
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                using (SqlCommand cmd = new SqlCommand("sp_TaoDonHang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaKH", SqlDbType.VarChar, 20).Value = string.IsNullOrEmpty(_maKH) ? (object)DBNull.Value : _maKH;
                    cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 255).Value = txtTenKhach.Text.Trim();
                    cmd.Parameters.Add("@SoDT", SqlDbType.VarChar, 20).Value = txtSdt.Text.Trim();
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = chkTaiQuay.Checked ? "" : txtDiaChi.Text.Trim();
                    cmd.Parameters.Add("@HinhThucNhan", SqlDbType.NVarChar, 30).Value = chkTaiQuay.Checked ? "Nhận tại cửa hàng" : "Giao tận nơi";
                    cmd.Parameters.Add("@MaDoiTac", SqlDbType.VarChar, 20).Value = Convert.ToString(cboKenh.SelectedValue);
                    cmd.Parameters.Add("@PhuongThucThanhToan", SqlDbType.NVarChar, 50).Value = Convert.ToString(cboThanhToan.SelectedItem);
                    cmd.Parameters.Add(AiHelper.PDec("@GiamGiaCuaShop", giam, 18, 2));
                    cmd.Parameters.Add(AiHelper.PDec("@PhiVanChuyenKhachTra", ship, 18, 2));
                    SqlParameter pHang = cmd.Parameters.Add("@DsHang", SqlDbType.Structured);
                    pHang.TypeName = "dbo.DanhSachHangMua";
                    pHang.Value = ds;
                    SqlParameter pMa = cmd.Parameters.Add("@MaDH", SqlDbType.VarChar, 20);
                    pMa.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    maDH = Convert.ToString(pMa.Value);
                }

                MessageBox.Show("Đã tạo đơn " + maDH + " ở trạng thái \"Chờ xác nhận\".\n" +
                                "Hàng trong đơn đã được giữ lại, không bán trùng cho đơn khác.\n" +
                                "Bước tiếp theo: vào \"Danh sách đơn hàng\" bấm \"Xác nhận & xuất kho\"." +
                                (chkTaiQuay.Checked ? "\nKhách nhận tại quầy: xuất kho xong đơn sẽ chuyển thẳng sang \"Đã giao\"." : TinhRuiRoSauKhiChot(maDH)),
                                "Đã chốt đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa tạo được đơn: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NapTatCa();
            }
        }

        private string TinhRuiRoSauKhiChot(string maDH)
        {
            try
            {
                DataTable t = AiHelper.ChayThuTuc("sp_CanhBaoRuiRoHuyDon", AiHelper.P("@MaDH", maDH));
                if (t.Rows.Count == 0) return "";
                decimal diem = Convert.ToDecimal(t.Rows[0]["TyLeRuiRoHuyDon"]);
                return "\n\nĐiểm rủi ro hủy đơn: " + diem.ToString("0") + "/100 (" + t.Rows[0]["TrangThaiCanhBaoHuy"] + ")." +
                       (diem >= 30 ? "\nNên gọi xác nhận SĐT và địa chỉ với khách trước khi đóng gói." : "");
            }
            catch { return ""; }
        }

        private static void BaoNhe(string noiDung)
        {
            MessageBox.Show(noiDung, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
