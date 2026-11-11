using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    public partial class Form_HoaDon : Form
    {
        private const string BAN_HANG = "Hóa đơn bán hàng";
        private const string DIEU_CHINH = "Điều chỉnh giảm";

        private readonly TextBox txtChiTiet;
        private readonly Button btnDieuChinh;
        private string _maDHHienTai;
        private string _maHDBan;
        private string _trangThaiDon;

        public Form_HoaDon()
        {
            InitializeComponent();
            lblMaHoaDon.Text = "Mã đơn / mã HĐ:";
            lblTraCuuXml.Text = "Mã tra cứu:";
            btnInHoaDon.Text = "Phát hành / xem hóa đơn";
            txtKhachHang.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTraCuuXml.ReadOnly = true;
            txtChiTiet = new TextBox
            {
                Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, WordWrap = true,
                Location = new Point(560, 110), Size = new Size(228, 240), Font = new Font("Consolas", 8F)
            };
            btnDieuChinh = new Button { Text = "Lập hóa đơn điều chỉnh giảm (khách trả hàng)", Location = new Point(90, 404), Size = new Size(390, 27), Enabled = false };
            btnDieuChinh.Click += (s, e) => LapHoaDonDieuChinh();
            Controls.Add(txtChiTiet);
            Controls.Add(btnDieuChinh);
            GiaoDien.GanChoForm(this);
            GiaoDien.ThemBangDuoi(this, "Đơn đã giao tới khách và hóa đơn của đơn (đơn chưa có hóa đơn xếp trên cùng)", 260,
                () => AiHelper.Doc(
                    "SELECT TOP 300 dh.MaDH AS [Mã đơn], dh.NgayGiao AS [Ngày giao], ISNULL(dh.TenNguoiNhan, kh.TenKH) AS [Người mua], " +
                    "       dh.TongTien - dh.GiamGiaCuaShop AS [Tiền hàng sau giảm], dh.TrangThai AS [Trạng thái], ISNULL(hb.MaHD, N'Chưa lập') AS [Hóa đơn bán], " +
                    "       CASE WHEN tr.Hoan IS NULL THEN N'' WHEN ISNULL(dc.Tien, 0) >= tr.Hoan THEN N'Đã lập đủ' " +
                    "            ELSE N'Cần lập ' + FORMAT(tr.Hoan - ISNULL(dc.Tien, 0), 'N0', 'vi-VN') + N' đ' END AS [Điều chỉnh do trả hàng] " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH " +
                    "OUTER APPLY (SELECT h.MaHD FROM HoaDon h WHERE h.MaDH = dh.MaDH AND h.LoaiHoaDon = N'Hóa đơn bán hàng') hb " +
                    "OUTER APPLY (SELECT SUM(t.SoTienHoan) AS Hoan FROM TraHang t WHERE t.MaDH = dh.MaDH) tr " +
                    "OUTER APPLY (SELECT SUM(h.TongTien) AS Tien FROM HoaDon h WHERE h.MaDH = dh.MaDH AND h.LoaiHoaDon = N'Điều chỉnh giảm') dc " +
                    "WHERE dh.TrangThai IN (N'Đã giao', N'Trả hàng/Hoàn tiền') " +
                    "ORDER BY CASE WHEN hb.MaHD IS NULL THEN 0 WHEN tr.Hoan > ISNULL(dc.Tien, 0) THEN 1 ELSE 2 END, dh.NgayGiao DESC"),
                r => txtMaHoaDon.Text = Convert.ToString(r.Cells["Mã đơn"].Value));
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            txtMaHoaDon.Text = "";
            LoadThongTinHoaDon();
        }

        private void XoaManHinh(string thongBao)
        {
            txtKhachHang.Text = thongBao;
            txtTongTien.Text = "0 VNĐ";
            txtTraCuuXml.Text = "";
            txtChiTiet.Text = "";
            _maDHHienTai = null;
            _maHDBan = null;
            _trangThaiDon = null;
            btnDieuChinh.Enabled = false;
        }

        private void LoadThongTinHoaDon()
        {
            string ma = txtMaHoaDon.Text.Trim();
            if (ma.Length == 0) { XoaManHinh(""); return; }
            try
            {
                object maDH = AiHelper.LayGiaTri(
                    "SELECT COALESCE((SELECT MaDH FROM HoaDon WHERE MaHD = @c), (SELECT MaDH FROM DonHang WHERE MaDH = @c))", AiHelper.P("@c", ma));
                if (maDH == null) { XoaManHinh("Không tìm thấy"); return; }
                _maDHHienTai = Convert.ToString(maDH);

                DataTable don = AiHelper.Doc(
                    "SELECT dh.TrangThai, ISNULL(dh.TenNguoiNhan, kh.TenKH) AS Ten, dh.TongTien - dh.GiamGiaCuaShop AS TienHang, " +
                    "       ISNULL((SELECT SUM(SoTienHoan) FROM TraHang t WHERE t.MaDH = dh.MaDH), 0) AS DaHoan " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH WHERE dh.MaDH = @d", AiHelper.P("@d", _maDHHienTai));
                DataRow d = don.Rows[0];
                _trangThaiDon = Convert.ToString(d["TrangThai"]);
                txtKhachHang.Text = Convert.ToString(d["Ten"]);

                DataTable hd = AiHelper.Doc(
                    "SELECT h.MaHD, h.LoaiHoaDon, h.TongTien, q.MaHDDT FROM HoaDon h LEFT JOIN QuanLyHDDT q ON q.MaHD = h.MaHD " +
                    "WHERE h.MaDH = @d ORDER BY h.NgayLap", AiHelper.P("@d", _maDHHienTai));
                _maHDBan = null;
                decimal ban = 0, dieuChinh = 0;
                string maTraCuu = "";
                foreach (DataRow r in hd.Rows)
                {
                    if (Convert.ToString(r["LoaiHoaDon"]) == BAN_HANG)
                    {
                        _maHDBan = Convert.ToString(r["MaHD"]);
                        ban = Convert.ToDecimal(r["TongTien"]);
                        maTraCuu = Convert.ToString(r["MaHDDT"]);
                    }
                    else dieuChinh += Convert.ToDecimal(r["TongTien"]);
                }

                bool daGiao = _trangThaiDon == DonHangShopee.DA_GIAO || _trangThaiDon == DonHangShopee.TRA_HANG;
                if (_maHDBan == null)
                {
                    txtTongTien.Text = Convert.ToDecimal(d["TienHang"]).ToString("N0") + " VNĐ (chưa lập)";
                    txtTraCuuXml.Text = daGiao ? "Chưa lập hóa đơn" : "Đơn đang \"" + _trangThaiDon + "\", chưa lập được";
                    txtChiTiet.Text = "";
                }
                else
                {
                    txtTongTien.Text = (ban - dieuChinh).ToString("N0") + " VNĐ" + (dieuChinh > 0 ? " (sau điều chỉnh)" : "");
                    txtTraCuuXml.Text = maTraCuu;
                    txtChiTiet.Text = TaoNoiDung(_maDHHienTai);
                }
                btnDieuChinh.Enabled = _maHDBan != null && Convert.ToDecimal(d["DaHoan"]) > dieuChinh;
            }
            catch (Exception ex)
            {
                XoaManHinh("Không tìm thấy");
                MessageBox.Show("Lỗi khi tải thông tin hóa đơn: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string TaoNoiDung(string maDH)
        {
            StringBuilder sb = new StringBuilder();
            DataTable ch = AiHelper.Doc("SELECT TenCuaHang, MaSoThue, DiaChi, SoDT FROM ThongTinCuaHang WHERE Id = 1");
            if (ch.Rows.Count > 0)
            {
                DataRow c = ch.Rows[0];
                sb.AppendLine("Người bán: " + c["TenCuaHang"]);
                sb.AppendLine("MST: " + c["MaSoThue"] + "  |  ĐT: " + c["SoDT"]);
                sb.AppendLine("Địa chỉ: " + c["DiaChi"]);
            }
            DataTable hd = AiHelper.Doc(
                "SELECT h.MaHD, h.LoaiHoaDon, h.MaHDGoc, h.NgayLap, h.TongTien, h.GiamGia, h.ThueSuat, h.TienChuaThue, h.TienThue, " +
                "       h.TenNguoiMua, h.DiaChiNguoiMua, h.MaSoThueNguoiMua, q.MaHDDT, q.MaXacThuc, q.ChuKySo " +
                "FROM HoaDon h LEFT JOIN QuanLyHDDT q ON q.MaHD = h.MaHD WHERE h.MaDH = @d ORDER BY h.NgayLap", AiHelper.P("@d", maDH));
            foreach (DataRow h in hd.Rows)
            {
                string loai = Convert.ToString(h["LoaiHoaDon"]);
                sb.AppendLine("----------------------------------------");
                sb.AppendLine((loai == BAN_HANG ? "HÓA ĐƠN BÁN HÀNG " : "HÓA ĐƠN ĐIỀU CHỈNH GIẢM ") + h["MaHD"] +
                              (loai == DIEU_CHINH ? " (điều chỉnh cho " + h["MaHDGoc"] + ", do khách trả hàng)" : ""));
                sb.AppendLine("Đơn hàng: " + maDH + "  |  Ngày lập: " + Convert.ToDateTime(h["NgayLap"]).ToString("dd/MM/yyyy HH:mm"));
                sb.AppendLine("Người mua: " + h["TenNguoiMua"] + (h["MaSoThueNguoiMua"] == DBNull.Value ? "" : "  |  MST: " + h["MaSoThueNguoiMua"]));
                sb.AppendLine("Địa chỉ: " + h["DiaChiNguoiMua"]);
                DataTable ct = AiHelper.Doc(
                    "SELECT c.MaSP, sp.TenSP, c.SoLuong, c.DonGia, ISNULL(c.ThanhTien, c.SoLuong * c.DonGia) AS ThanhTien " +
                    "FROM ChiTietHoaDon c JOIN SanPham sp ON sp.MaSP = c.MaSP WHERE c.MaHD = @h ORDER BY c.MaSP", AiHelper.P("@h", h["MaHD"]));
                foreach (DataRow c in ct.Rows)
                    sb.AppendLine("  " + c["TenSP"] + ": " + c["SoLuong"] + " x " + Convert.ToDecimal(c["DonGia"]).ToString("N0") +
                                  " = " + Convert.ToDecimal(c["ThanhTien"]).ToString("N0") + " đ");
                if (Convert.ToDecimal(h["GiamGia"]) > 0)
                    sb.AppendLine("  Giảm giá của shop: -" + Convert.ToDecimal(h["GiamGia"]).ToString("N0") + " đ");
                sb.AppendLine((loai == BAN_HANG ? "Tổng thanh toán" : "Số tiền giảm") + " (đã gồm thuế): " + Convert.ToDecimal(h["TongTien"]).ToString("N0") + " đ");
                sb.AppendLine("  Tiền chưa thuế: " + Convert.ToDecimal(h["TienChuaThue"]).ToString("N0") + " đ");
                sb.AppendLine("  Thuế GTGT " + Convert.ToDecimal(h["ThueSuat"]).ToString("0.##") + "%: " + Convert.ToDecimal(h["TienThue"]).ToString("N0") + " đ");
                if (h["MaHDDT"] != DBNull.Value)
                {
                    sb.AppendLine("Mã tra cứu: " + h["MaHDDT"] + "  |  Mã xác thực: " + h["MaXacThuc"]);
                    sb.AppendLine(Convert.ToString(h["ChuKySo"]));
                }
            }
            return sb.ToString();
        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {
            LoadThongTinHoaDon();
        }

        private void ThucHienInHoaDon()
        {
            if (string.IsNullOrEmpty(_maDHHienTai))
            {
                MessageBox.Show("Hãy nhập mã đơn hoặc mã hóa đơn, hoặc bấm chọn một đơn ở bảng bên dưới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_maHDBan == null)
            {
                if (_trangThaiDon != DonHangShopee.DA_GIAO && _trangThaiDon != DonHangShopee.TRA_HANG)
                {
                    MessageBox.Show("Đơn " + _maDHHienTai + " đang \"" + _trangThaiDon + "\". Chỉ lập hóa đơn cho đơn đã giao tới khách.", "Chưa lập được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Phát hành hóa đơn cho đơn " + _maDHHienTai + "?\nHóa đơn đã phát hành thì không sửa được, chỉ lập được hóa đơn điều chỉnh.",
                                    "Phát hành hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                string mst = DonHangShopee.HoiLyDo("Mã số thuế người mua", "Khách là công ty cần xuất hóa đơn thì nhập mã số thuế. Khách cá nhân thì để trống và bấm Thôi.", null);
                try
                {
                    using (SqlConnection conn = DbHelper.TaoKetNoi())
                    using (SqlCommand cmd = new SqlCommand("sp_PhatHanhHoaDon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MaDH", SqlDbType.VarChar, 20).Value = _maDHHienTai;
                        cmd.Parameters.Add("@MaSoThueNguoiMua", SqlDbType.VarChar, 20).Value = string.IsNullOrEmpty(mst) ? (object)DBNull.Value : mst;
                        cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chưa phát hành được hóa đơn: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                LoadThongTinHoaDon();
                GiaoDien.TaiLai(this);
            }
            MessageBox.Show(TaoNoiDung(_maDHHienTai), "Hóa đơn của đơn " + _maDHHienTai, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LapHoaDonDieuChinh()
        {
            if (string.IsNullOrEmpty(_maDHHienTai)) return;
            if (MessageBox.Show("Lập hóa đơn điều chỉnh giảm cho phần tiền đã hoàn của đơn " + _maDHHienTai + "?", "Hóa đơn điều chỉnh",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                using (SqlCommand cmd = new SqlCommand("sp_LapHoaDonDieuChinh", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaDH", SqlDbType.VarChar, 20).Value = _maDHHienTai;
                    cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadThongTinHoaDon();
                GiaoDien.TaiLai(this);
                MessageBox.Show(TaoNoiDung(_maDHHienTai), "Hóa đơn của đơn " + _maDHHienTai, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa lập được hóa đơn điều chỉnh: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void btnInHoaDon_Click(object sender, EventArgs e)
        {
            ThucHienInHoaDon();
        }

        private void btnInHoaDon_Click_1(object sender, EventArgs e)
        {
            ThucHienInHoaDon();
        }

        private void ThucHienXuatPdf()
        {
            if (string.IsNullOrEmpty(_maDHHienTai) || _maHDBan == null)
            {
                MessageBox.Show("Đơn này chưa có hóa đơn. Hãy bấm \"Phát hành / xem hóa đơn\" trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMayInPdf = null;
            foreach (string tenMayIn in PrinterSettings.InstalledPrinters)
            {
                if (tenMayIn.IndexOf("PDF", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    tenMayInPdf = tenMayIn;
                    if (tenMayIn.Equals("Microsoft Print to PDF", StringComparison.OrdinalIgnoreCase)) break;
                }
            }
            if (tenMayInPdf == null)
            {
                MessageBox.Show(
                    "Không tìm thấy máy in PDF nào trên máy tính!\n\n" +
                    "Vui lòng bật tính năng \"Microsoft Print to PDF\" có sẵn của Windows " +
                    "(Cài đặt > Ứng dụng > Tùy chọn bổ sung) rồi thử lại.",
                    "Thiếu máy in PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string noiDung = TaoNoiDung(_maDHHienTai);
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.FileName = "HoaDon_" + _maHDBan + ".pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (PrintDocument pd = new PrintDocument())
                    {
                        pd.PrinterSettings.PrinterName = tenMayInPdf;
                        pd.PrinterSettings.PrintToFile = true;
                        pd.PrinterSettings.PrintFileName = sfd.FileName;
                        pd.DocumentName = "HoaDon_" + _maHDBan;
                        pd.PrintPage += (s, e) =>
                        {
                            using (Font fTieuDe = new Font("Times New Roman", 16, FontStyle.Bold))
                            using (Font fNoiDung = new Font("Times New Roman", 11, FontStyle.Regular))
                            {
                                RectangleF vung = e.MarginBounds;
                                e.Graphics.DrawString("HÓA ĐƠN GIÁ TRỊ GIA TĂNG (BẢN MÔ PHỎNG)", fTieuDe, Brushes.Black, vung.Left, vung.Top);
                                e.Graphics.DrawString(noiDung, fNoiDung, Brushes.Black,
                                    new RectangleF(vung.Left, vung.Top + 40, vung.Width, vung.Height - 40));
                            }
                        };
                        pd.Print();
                    }
                    MessageBox.Show("Xuất hóa đơn PDF thành công!\nĐường dẫn: " + sfd.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXuatPdf_Click(object sender, EventArgs e)
        {
            ThucHienXuatPdf();
        }

        public void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void txtKhachHang_TextChanged(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
        private void txtTraCuuXml_TextChanged(object sender, EventArgs e) { }
    }
}
