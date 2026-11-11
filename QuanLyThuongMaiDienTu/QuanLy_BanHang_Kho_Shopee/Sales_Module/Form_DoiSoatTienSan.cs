using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_DoiSoatTienSan : Form
    {
        private const string TAT_CA = "(Tất cả)";

        private readonly ComboBox cboKenh, cboTinhTrang;
        private readonly Label lblTongKet;
        private readonly DataGridView dgvDon;
        private readonly TextBox txtMaDH, txtMaDot, txtSoTien, txtGhiChu;
        private readonly DateTimePicker dtNgayChuyen;

        public Form_DoiSoatTienSan()
        {
            Text = "Đối soát tiền sàn chuyển về";
            Name = "Form_DoiSoatTienSan";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1080, 700);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "ĐỐI SOÁT TIỀN SÀN CHUYỂN VỀ", AutoSize = true, Location = new Point(20, 10) };
            TextBox txtGiaiThich = new TextBox
            {
                Multiline = true, ReadOnly = true, TabStop = false, Location = new Point(20, 72), Size = new Size(1040, 66),
                Text = "Đơn bán trên sàn: khách trả tiền cho sàn, sàn trừ phí rồi mới chuyển cho shop. Màn này so số tiền phần mềm tính shop được nhận " +
                       "(tiền hàng - giảm giá của shop - phí sàn - tiền hoàn) với số tiền sàn thật sự chuyển về.\r\n" +
                       "Lấy số tiền sàn chuyển ở: Kênh Người Bán Shopee > Tài chính > Doanh thu (báo cáo thu nhập), TikTok Shop Seller Center, mục Tài chính. " +
                       "Đơn giao xong quá 14 ngày mà sàn chưa chuyển, hoặc sàn trả thiếu, thì mở yêu cầu hỗ trợ với sàn."
            };
            Label l1 = new Label { Text = "Kênh:", Location = new Point(20, 150), Size = new Size(45, 22) };
            cboKenh = new ComboBox { Location = new Point(66, 147), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l2 = new Label { Text = "Tình trạng:", Location = new Point(285, 150), Size = new Size(70, 22) };
            cboTinhTrang = new ComboBox { Location = new Point(357, 147), Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTinhTrang.Items.AddRange(new object[] { TAT_CA, "Cần xem (lệch hoặc quá hạn)", "Khớp", "Sàn trả thiếu", "Sàn trả dư",
                                                       "Sàn chưa chuyển (chưa tới hạn)", "Quá 14 ngày sàn chưa chuyển tiền" });
            cboTinhTrang.SelectedIndex = 1;
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(940, 144), Size = new Size(120, 30) };
            lblTongKet = new Label { Location = new Point(20, 182), Size = new Size(1040, 64), Font = new Font("Consolas", 9.5F), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            dgvDon = new DataGridView { Location = new Point(20, 254), Size = new Size(1040, 300) };

            Label l3 = new Label { Text = "Ghi tiền sàn chuyển cho 1 đơn (bấm 1 dòng để chọn):", Location = new Point(20, 564), Size = new Size(600, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            Label l4 = new Label { Text = "Mã đơn:", Location = new Point(20, 596), Size = new Size(55, 22) };
            txtMaDH = new TextBox { Location = new Point(78, 593), Width = 110 };
            Label l5 = new Label { Text = "Mã đợt chuyển:", Location = new Point(200, 596), Size = new Size(95, 22) };
            txtMaDot = new TextBox { Location = new Point(298, 593), Width = 150 };
            Label l6 = new Label { Text = "Ngày chuyển:", Location = new Point(460, 596), Size = new Size(85, 22) };
            dtNgayChuyen = new DateTimePicker { Location = new Point(548, 593), Width = 150, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };
            Label l7 = new Label { Text = "Số tiền sàn trả (đ):", Location = new Point(712, 596), Size = new Size(118, 22) };
            txtSoTien = new TextBox { Location = new Point(832, 593), Width = 228 };
            Label l8 = new Label { Text = "Ghi chú:", Location = new Point(20, 630), Size = new Size(55, 22) };
            txtGhiChu = new TextBox { Location = new Point(78, 627), Width = 700 };
            Button btnLuu = new Button { Text = "Lưu tiền sàn chuyển", Location = new Point(790, 622), Size = new Size(270, 32) };

            Controls.AddRange(new Control[] { lblTieuDe, txtGiaiThich, l1, cboKenh, l2, cboTinhTrang, btnThoat, lblTongKet, dgvDon,
                l3, l4, txtMaDH, l5, txtMaDot, l6, dtNgayChuyen, l7, txtSoTien, l8, txtGhiChu, btnLuu });

            GiaoDien.ThemBangDuoi(this, "Các đợt sàn chuyển tiền (mới nhất ở trên)", 200,
                () => AiHelper.Doc(
                    "SELECT TOP 200 c.MaDot AS [Mã đợt], dt.TenDoiTac AS [Kênh], MIN(c.NgayChuyen) AS [Ngày chuyển], COUNT(*) AS [Số đơn], " +
                    "       SUM(c.SoTienSanTra) AS [Tiền sàn chuyển] " +
                    "FROM TienSanChuyenVe c JOIN DonHang dh ON dh.MaDH = c.MaDH JOIN DoiTac dt ON dt.MaDoiTac = dh.MaDoiTac " +
                    "GROUP BY c.MaDot, dt.TenDoiTac ORDER BY MIN(c.NgayChuyen) DESC"),
                null);

            cboKenh.SelectionChangeCommitted += (s, e) => NapDon();
            cboTinhTrang.SelectionChangeCommitted += (s, e) => NapDon();
            dgvDon.CellClick += (s, e) => { if (e.RowIndex >= 0 && e.RowIndex < dgvDon.Rows.Count) ChonDon(dgvDon.Rows[e.RowIndex]); };
            btnLuu.Click += (s, e) => Luu();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvDon);
                try
                {
                    DataTable k = AiHelper.Doc("SELECT CAST('' AS VARCHAR(20)) AS MaDoiTac, N'" + TAT_CA + "' AS Ten UNION ALL " +
                                               "SELECT MaDoiTac, TenDoiTac FROM DoiTac WHERE LoaiKenh = N'Sàn thương mại điện tử' ORDER BY MaDoiTac");
                    AiHelper.NapCombo(cboKenh, k, "MaDoiTac", "Ten");
                }
                catch (Exception ex) { AiHelper.BaoLoi(ex); }
                NapDon();
            };

            GiaoDien.GanChoForm(this);
        }

        private void NapDon()
        {
            try
            {
                string kenh = Convert.ToString(cboKenh.SelectedValue ?? "");
                string tt = Convert.ToString(cboTinhTrang.SelectedItem);
                DataTable tong = AiHelper.Doc(
                    "SELECT TinhTrang, COUNT(*) AS SoDon, SUM(ShopTinhDuocNhan) AS TinhDuoc, SUM(ISNULL(SoTienSanTra, 0)) AS SanTra, SUM(ISNULL(ChenhLech, 0)) AS Lech " +
                    "FROM dbo.vw_DoiSoatTienSan WHERE (@k = '' OR MaDoiTac = @k) GROUP BY TinhTrang ORDER BY TinhTrang", AiHelper.P("@k", kenh));
                StringBuilder sb = new StringBuilder();
                foreach (DataRow r in tong.Rows)
                    sb.AppendLine(Convert.ToString(r["TinhTrang"]).PadRight(34) + Convert.ToInt32(r["SoDon"]).ToString().PadLeft(6) + " đơn | phần mềm tính " +
                                  Convert.ToDecimal(r["TinhDuoc"]).ToString("N0").PadLeft(15) + " đ | sàn đã trả " + Convert.ToDecimal(r["SanTra"]).ToString("N0").PadLeft(15) + " đ" +
                                  (Convert.ToDecimal(r["Lech"]) != 0 ? " | lệch " + Convert.ToDecimal(r["Lech"]).ToString("N0") + " đ" : ""));
                lblTongKet.Text = sb.ToString();

                string dk = tt == TAT_CA ? "1 = 1"
                          : tt.StartsWith("Cần xem") ? "TinhTrang IN (N'Sàn trả thiếu', N'Sàn trả dư', N'Quá 14 ngày sàn chưa chuyển tiền')"
                          : "TinhTrang = @t";
                dgvDon.DataSource = AiHelper.Doc(
                    "SELECT MaDH AS [Mã đơn], KenhBan AS [Kênh], NgayGiao AS [Ngày giao], NgayTraCuoi AS [Ngày khách trả hàng], " +
                    "       ShopTinhDuocNhan AS [Phần mềm tính được nhận], SoTienSanTra AS [Sàn đã trả], ChenhLech AS [Chênh lệch], " +
                    "       NgayChuyen AS [Ngày sàn chuyển], MaDot AS [Mã đợt], TinhTrang AS [Tình trạng], GhiChu AS [Ghi chú] " +
                    "FROM dbo.vw_DoiSoatTienSan WHERE (@k = '' OR MaDoiTac = @k) AND " + dk + " ORDER BY NgayGiao DESC",
                    AiHelper.P("@k", kenh), AiHelper.P("@t", tt));
                foreach (string c in new[] { "Phần mềm tính được nhận", "Sàn đã trả", "Chênh lệch" }) AiHelper.DinhDangSo(dgvDon, c, "N0");
            }
            catch (Exception ex) { lblTongKet.Text = "Chưa đọc được dữ liệu: " + AiHelper.LoiDeHieu(ex); }
        }

        private void ChonDon(DataGridViewRow r)
        {
            txtMaDH.Text = Convert.ToString(r.Cells["Mã đơn"].Value);
            object tra = r.Cells["Sàn đã trả"].Value;
            object tinh = r.Cells["Phần mềm tính được nhận"].Value;
            txtSoTien.Text = Convert.ToDecimal(tra == null || tra == DBNull.Value ? tinh : tra).ToString("0");
            txtMaDot.Text = Convert.ToString(r.Cells["Mã đợt"].Value);
            object ngay = r.Cells["Ngày sàn chuyển"].Value;
            dtNgayChuyen.Value = ngay == null || ngay == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ngay);
            txtGhiChu.Text = Convert.ToString(r.Cells["Ghi chú"].Value);
        }

        private void Luu()
        {
            string maDH = txtMaDH.Text.Trim();
            if (maDH.Length == 0) { AiHelper.BaoTin("Hãy chọn một đơn ở bảng trên hoặc nhập mã đơn."); return; }
            decimal soTien;
            if (!AiHelper.DocSoTien(txtSoTien.Text, out soTien)) { AiHelper.BaoTin("Số tiền sàn trả phải là số (sàn trừ tiền thì ghi số âm, ví dụ -5000)."); return; }
            try
            {
                AiHelper.ChayThuTucLayMa("sp_GhiTienSanChuyenVe", null,
                    AiHelper.P("@MaDH", maDH), AiHelper.P("@MaDot", txtMaDot.Text.Trim()), AiHelper.P("@NgayChuyen", dtNgayChuyen.Value),
                    AiHelper.PDec("@SoTienSanTra", soTien, 18, 2), AiHelper.P("@GhiChu", txtGhiChu.Text.Trim().Length == 0 ? null : txtGhiChu.Text.Trim()));
                NapDon();
                GiaoDien.TaiLai(this);
                AiHelper.BaoTin("Đã lưu tiền sàn chuyển cho đơn " + maDH + ".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}
