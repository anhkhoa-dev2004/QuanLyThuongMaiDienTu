using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_ChiPhiVanHanh : Form
    {
        private const string GIAI_THICH =
            "Lãi sau phí sàn (màn Báo cáo doanh thu) mới chỉ trừ giá vốn, phí sàn, giảm giá và tiền hoàn. Shop còn phải trả tiền thuê, lương, bảo hiểm, điện nước, đóng gói, quảng cáo, phí ship. " +
            "Lãi ròng = lãi sau phí sàn - chi phí vận hành - hàng mất, hỏng (xuất kho không gắn đơn, xuất dư so với đơn, thiếu khi kiểm kê, tính theo giá nhập bình quân) - chi phí bảo hành shop chịu.\r\n" +
            "Số chi phí có sẵn trong dữ liệu mẫu là số MÔ PHỎNG (cách tính ghi ở cột \"Cách tính\"). Dùng thật thì sửa lại theo hóa đơn, bảng lương của shop.";

        private static readonly string[] NHOM = { "Mặt bằng", "Kho bãi", "Nhân viên", "Điện nước, internet", "Đóng gói", "Quảng cáo", "Vận chuyển", "Khác" };

        private readonly DataGridView dgvThang, dgvChiPhi;
        private readonly Label lblTongKet, lblChiPhiThang;
        private readonly DateTimePicker dtThang;
        private readonly ComboBox cboNhom;
        private readonly TextBox txtTen, txtSoTien, txtCachTinh;
        private int? _maCP;

        public Form_ChiPhiVanHanh()
        {
            Text = "Chi phí vận hành và lãi ròng";
            Name = "Form_ChiPhiVanHanh";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1100, 800);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "CHI PHÍ VẬN HÀNH VÀ LÃI RÒNG THEO THÁNG", AutoSize = true, Location = new Point(20, 10) };
            TextBox txtGiaiThich = new TextBox { Multiline = true, ReadOnly = true, TabStop = false, Location = new Point(20, 72), Size = new Size(1060, 70), Text = GIAI_THICH };
            lblTongKet = new Label { Location = new Point(20, 150), Size = new Size(1060, 44), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            Label l1 = new Label { Text = "Từng tháng (bấm 1 dòng để xem chi phí của tháng đó):", Location = new Point(20, 198), Size = new Size(600, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvThang = new DataGridView { Location = new Point(20, 222), Size = new Size(1060, 250) };
            lblChiPhiThang = new Label { Text = "Chi phí của tháng:", Location = new Point(20, 480), Size = new Size(700, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvChiPhi = new DataGridView { Location = new Point(20, 504), Size = new Size(1060, 170) };

            Label l2 = new Label { Text = "Tháng:", Location = new Point(20, 688), Size = new Size(50, 22) };
            dtThang = new DateTimePicker { Location = new Point(72, 685), Width = 90, Format = DateTimePickerFormat.Custom, CustomFormat = "MM/yyyy", ShowUpDown = true };
            Label l3 = new Label { Text = "Nhóm:", Location = new Point(175, 688), Size = new Size(45, 22) };
            cboNhom = new ComboBox { Location = new Point(222, 685), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboNhom.Items.AddRange(NHOM);
            cboNhom.SelectedIndex = 0;
            Label l4 = new Label { Text = "Tên khoản chi:", Location = new Point(385, 688), Size = new Size(90, 22) };
            txtTen = new TextBox { Location = new Point(478, 685), Width = 330 };
            Label l5 = new Label { Text = "Số tiền (đ):", Location = new Point(820, 688), Size = new Size(75, 22) };
            txtSoTien = new TextBox { Location = new Point(898, 685), Width = 182 };
            Label l6 = new Label { Text = "Cách tính:", Location = new Point(20, 722), Size = new Size(70, 22) };
            txtCachTinh = new TextBox { Location = new Point(92, 719), Width = 988 };

            Button btnThem = new Button { Text = "Thêm khoản chi", Location = new Point(20, 756), Size = new Size(140, 32) };
            Button btnSua = new Button { Text = "Lưu sửa dòng đang chọn", Location = new Point(170, 756), Size = new Size(180, 32) };
            Button btnXoa = new Button { Text = "Xóa dòng đang chọn", Location = new Point(360, 756), Size = new Size(150, 32) };
            Button btnChep = new Button { Text = "Chép chi phí cố định sang tháng sau", Location = new Point(520, 756), Size = new Size(250, 32) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(960, 756), Size = new Size(120, 32) };

            Controls.AddRange(new Control[] { lblTieuDe, txtGiaiThich, lblTongKet, l1, dgvThang, lblChiPhiThang, dgvChiPhi,
                l2, dtThang, l3, cboNhom, l4, txtTen, l5, txtSoTien, l6, txtCachTinh, btnThem, btnSua, btnXoa, btnChep, btnThoat });

            dgvThang.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvThang.Rows.Count) return;
                dtThang.Value = Convert.ToDateTime(dgvThang.Rows[e.RowIndex].Cells["Ngày đầu tháng"].Value);
                NapChiPhi();
            };
            dgvChiPhi.CellClick += (s, e) => { if (e.RowIndex >= 0 && e.RowIndex < dgvChiPhi.Rows.Count) ChonDong(dgvChiPhi.Rows[e.RowIndex]); };
            dtThang.ValueChanged += (s, e) => NapChiPhi();
            btnThem.Click += (s, e) => Luu(true);
            btnSua.Click += (s, e) => Luu(false);
            btnXoa.Click += (s, e) => Xoa();
            btnChep.Click += (s, e) => ChepSangThangSau();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvThang);
                AiHelper.DinhDangLuoi(dgvChiPhi);
                try { dtThang.Value = new DateTime(AiHelper.NgayDuLieuMoiNhat().Year, AiHelper.NgayDuLieuMoiNhat().Month, 1); }
                catch { dtThang.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); }
                NapThang();
                NapChiPhi();
            };

            GiaoDien.GanChoForm(this);
        }

        private DateTime ThangDangChon()
        {
            return new DateTime(dtThang.Value.Year, dtThang.Value.Month, 1);
        }

        private void NapThang()
        {
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT FORMAT(Thang, 'MM/yyyy') AS [Tháng], TienHang AS [Tiền hàng], PhiSan AS [Phí sàn], TienShopNhan AS [Shop thực nhận], " +
                    "       GiaVonHangBan AS [Giá vốn], LaiGop AS [Lãi sau phí sàn], ChiPhiVanHanh AS [Chi phí vận hành], HangMatHong AS [Hàng mất, hỏng], " +
                    "       ChiPhiBaoHanh AS [Bảo hành shop chịu], LaiRong AS [Lãi ròng], " +
                    "       CAST(100.0 * LaiRong / NULLIF(TienHang, 0) AS DECIMAL(6,1)) AS [Lãi ròng / tiền hàng (%)], Thang AS [Ngày đầu tháng] " +
                    "FROM dbo.vw_LaiRongTheoThang ORDER BY Thang");
                dgvThang.DataSource = t;
                foreach (string c in new[] { "Tiền hàng", "Phí sàn", "Shop thực nhận", "Giá vốn", "Lãi sau phí sàn", "Chi phí vận hành", "Hàng mất, hỏng", "Bảo hành shop chịu", "Lãi ròng" })
                    AiHelper.DinhDangSo(dgvThang, c, "N0");
                if (dgvThang.Columns.Contains("Ngày đầu tháng")) dgvThang.Columns["Ngày đầu tháng"].Visible = false;

                decimal laiGop = 0, chiPhi = 0, mat = 0, bh = 0, rong = 0, tienHang = 0;
                int thangLai = 0;
                foreach (DataRow r in t.Rows)
                {
                    tienHang += Convert.ToDecimal(r["Tiền hàng"]);
                    laiGop += Convert.ToDecimal(r["Lãi sau phí sàn"]);
                    chiPhi += Convert.ToDecimal(r["Chi phí vận hành"]);
                    mat += Convert.ToDecimal(r["Hàng mất, hỏng"]);
                    bh += Convert.ToDecimal(r["Bảo hành shop chịu"]);
                    rong += Convert.ToDecimal(r["Lãi ròng"]);
                    if (Convert.ToDecimal(r["Lãi ròng"]) > 0) thangLai++;
                }
                lblTongKet.Text = "Cả kỳ: lãi sau phí sàn " + laiGop.ToString("N0") + " đ - chi phí vận hành " + chiPhi.ToString("N0") + " đ - hàng mất, hỏng " + mat.ToString("N0") +
                                  " đ - bảo hành " + bh.ToString("N0") + " đ = LÃI RÒNG " + rong.ToString("N0") + " đ" +
                                  (tienHang > 0 ? " (" + (100 * rong / tienHang).ToString("0.0") + "% tiền hàng)" : "") +
                                  ".\r\nSố tháng có lãi ròng dương: " + thangLai + " / " + t.Rows.Count + ".";
                lblTongKet.ForeColor = rong < 0 ? Color.FromArgb(192, 0, 0) : Color.FromArgb(0, 110, 0);
            }
            catch (Exception ex) { lblTongKet.Text = "Chưa đọc được số liệu: " + AiHelper.LoiDeHieu(ex); }
        }

        private void NapChiPhi()
        {
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT MaCP AS [Số dòng], NhomChiPhi AS [Nhóm], TenChiPhi AS [Khoản chi], SoTien AS [Số tiền], CachTinh AS [Cách tính] " +
                    "FROM ChiPhiVanHanh WHERE Thang = @t ORDER BY NhomChiPhi, TenChiPhi", AiHelper.P("@t", ThangDangChon()));
                dgvChiPhi.DataSource = t;
                AiHelper.DinhDangSo(dgvChiPhi, "Số tiền", "N0");
                decimal tong = 0;
                foreach (DataRow r in t.Rows) tong += Convert.ToDecimal(r["Số tiền"]);
                lblChiPhiThang.Text = "Chi phí tháng " + ThangDangChon().ToString("MM/yyyy") + ": " + t.Rows.Count + " khoản, tổng " + tong.ToString("N0") + " đ";
            }
            catch (Exception ex) { lblChiPhiThang.Text = "Chưa đọc được chi phí: " + AiHelper.LoiDeHieu(ex); }
        }

        private void ChonDong(DataGridViewRow r)
        {
            _maCP = Convert.ToInt32(r.Cells["Số dòng"].Value);
            cboNhom.SelectedItem = Convert.ToString(r.Cells["Nhóm"].Value);
            txtTen.Text = Convert.ToString(r.Cells["Khoản chi"].Value);
            txtSoTien.Text = Convert.ToDecimal(r.Cells["Số tiền"].Value).ToString("0");
            txtCachTinh.Text = Convert.ToString(r.Cells["Cách tính"].Value);
        }

        private void Luu(bool themMoi)
        {
            if (!themMoi && !_maCP.HasValue) { AiHelper.BaoTin("Hãy bấm chọn một khoản chi trong bảng dưới trước."); return; }
            string ten = txtTen.Text.Trim();
            if (ten.Length == 0) { AiHelper.BaoTin("Hãy ghi tên khoản chi (ví dụ: Tiền điện tháng 10)."); return; }
            decimal soTien;
            if (!AiHelper.DocSoTien(txtSoTien.Text, out soTien) || soTien < 0) { AiHelper.BaoTin("Số tiền phải là số không âm, ví dụ 15000000."); return; }
            try
            {
                if (themMoi)
                {
                    object co = AiHelper.LayGiaTri("SELECT COUNT(*) FROM ChiPhiVanHanh WHERE Thang = @t AND TenChiPhi = @n",
                                                   AiHelper.P("@t", ThangDangChon()), AiHelper.P("@n", ten));
                    if (Convert.ToInt32(co) > 0) { AiHelper.BaoTin("Tháng này đã có khoản \"" + ten + "\". Hãy chọn dòng đó rồi bấm Lưu sửa."); return; }
                    AiHelper.Chay("INSERT INTO ChiPhiVanHanh (Thang, NhomChiPhi, TenChiPhi, SoTien, CachTinh) VALUES (@t, @g, @n, @s, @c)",
                                  AiHelper.P("@t", ThangDangChon()), AiHelper.P("@g", Convert.ToString(cboNhom.SelectedItem)), AiHelper.P("@n", ten),
                                  AiHelper.PDec("@s", soTien, 18, 2), AiHelper.P("@c", txtCachTinh.Text.Trim().Length == 0 ? null : txtCachTinh.Text.Trim()));
                }
                else
                {
                    AiHelper.Chay("UPDATE ChiPhiVanHanh SET NhomChiPhi = @g, TenChiPhi = @n, SoTien = @s, CachTinh = @c WHERE MaCP = @id",
                                  AiHelper.P("@g", Convert.ToString(cboNhom.SelectedItem)), AiHelper.P("@n", ten), AiHelper.PDec("@s", soTien, 18, 2),
                                  AiHelper.P("@c", txtCachTinh.Text.Trim().Length == 0 ? null : txtCachTinh.Text.Trim()), AiHelper.P("@id", _maCP.Value));
                }
                NapThang();
                NapChiPhi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Xoa()
        {
            if (!_maCP.HasValue) { AiHelper.BaoTin("Hãy bấm chọn một khoản chi trong bảng dưới trước."); return; }
            if (MessageBox.Show("Xóa khoản chi \"" + txtTen.Text + "\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                AiHelper.Chay("DELETE FROM ChiPhiVanHanh WHERE MaCP = @id", AiHelper.P("@id", _maCP.Value));
                _maCP = null;
                NapThang();
                NapChiPhi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void ChepSangThangSau()
        {
            DateTime tu = ThangDangChon(), den = tu.AddMonths(1);
            if (MessageBox.Show("Chép các khoản Mặt bằng, Kho bãi, Nhân viên (trừ thưởng), Điện nước của tháng " + tu.ToString("MM/yyyy") +
                                " sang tháng " + den.ToString("MM/yyyy") + "?\nKhoản nào tháng sau đã có thì giữ nguyên. Chép xong nhớ sửa số tiền nếu tháng sau khác.",
                                "Chép chi phí cố định", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                int n = AiHelper.Chay(
                    "INSERT INTO ChiPhiVanHanh (Thang, NhomChiPhi, TenChiPhi, SoTien, CachTinh) " +
                    "SELECT @den, c.NhomChiPhi, c.TenChiPhi, c.SoTien, N'Chép từ tháng ' + FORMAT(@tu, 'MM/yyyy') " +
                    "FROM ChiPhiVanHanh c WHERE c.Thang = @tu AND c.NhomChiPhi IN (N'Mặt bằng', N'Kho bãi', N'Nhân viên', N'Điện nước, internet') " +
                    "  AND c.TenChiPhi NOT LIKE N'Thưởng%' " +
                    "  AND NOT EXISTS (SELECT 1 FROM ChiPhiVanHanh x WHERE x.Thang = @den AND x.TenChiPhi = c.TenChiPhi)",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den));
                dtThang.Value = den;
                NapThang();
                NapChiPhi();
                AiHelper.BaoTin("Đã chép " + n + " khoản chi sang tháng " + den.ToString("MM/yyyy") + ".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}
