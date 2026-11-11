using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_CongNoNhaCungCap : Form
    {
        private const string TAT_CA = "(Tất cả nhà cung cấp)";

        private readonly DataGridView dgvNCC, dgvPhieu;
        private readonly ComboBox cboNCC, cboHinhThuc;
        private readonly CheckBox chkConNo;
        private readonly TextBox txtSoTien;
        private readonly NumericUpDown numSoNgay;
        private readonly Label lblPhieu;
        private string _maPN;
        private decimal _conNo;

        public Form_CongNoNhaCungCap()
        {
            Text = "Công nợ nhà cung cấp";
            Name = "Form_CongNoNhaCungCap";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1080, 720);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "CÔNG NỢ NHÀ CUNG CẤP", AutoSize = true, Location = new Point(20, 10) };
            TextBox txtGiaiThich = new TextBox
            {
                Multiline = true, ReadOnly = true, TabStop = false, Location = new Point(20, 72), Size = new Size(1040, 52),
                Text = "Mỗi phiếu nhập là một khoản phải trả cho nhà cung cấp. Hạn trả = ngày nhập + số ngày được nợ của nhà cung cấp đó (0 ngày là trả ngay khi nhận hàng). " +
                       "Quá hạn mà chưa trả đủ thì hiện \"Quá hạn\". Bấm một phiếu ở bảng dưới, nhập số tiền rồi bấm Trả tiền."
            };
            Label l1 = new Label { Text = "Tổng theo nhà cung cấp (bấm 1 dòng để lọc):", Location = new Point(20, 132), Size = new Size(500, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvNCC = new DataGridView { Location = new Point(20, 156), Size = new Size(1040, 140) };

            Label l2 = new Label { Text = "Nhà cung cấp:", Location = new Point(20, 310), Size = new Size(90, 22) };
            cboNCC = new ComboBox { Location = new Point(112, 307), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            chkConNo = new CheckBox { Text = "Chỉ phiếu còn nợ", Location = new Point(430, 308), Size = new Size(150, 22), Checked = true };
            Label l3 = new Label { Text = "Số ngày được nợ của nhà cung cấp này:", Location = new Point(600, 310), Size = new Size(235, 22) };
            numSoNgay = new NumericUpDown { Location = new Point(838, 307), Width = 70, Minimum = 0, Maximum = 365 };
            Button btnLuuNgay = new Button { Text = "Lưu số ngày", Location = new Point(915, 304), Size = new Size(145, 30) };

            dgvPhieu = new DataGridView { Location = new Point(20, 342), Size = new Size(1040, 270) };
            lblPhieu = new Label { Text = "Chưa chọn phiếu nhập.", Location = new Point(20, 622), Size = new Size(1040, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            Label l4 = new Label { Text = "Số tiền trả (đ):", Location = new Point(20, 656), Size = new Size(95, 22) };
            txtSoTien = new TextBox { Location = new Point(118, 653), Width = 160 };
            Label l5 = new Label { Text = "Trả bằng:", Location = new Point(295, 656), Size = new Size(65, 22) };
            cboHinhThuc = new ComboBox { Location = new Point(362, 653), Width = 190, DropDownStyle = ComboBoxStyle.DropDownList };
            cboHinhThuc.Items.AddRange(new object[] { "Chuyển khoản ngân hàng", "Tiền mặt" });
            cboHinhThuc.SelectedIndex = 0;
            Button btnTra = new Button { Text = "Trả tiền cho phiếu đang chọn", Location = new Point(570, 649), Size = new Size(220, 32) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(940, 649), Size = new Size(120, 32) };

            Controls.AddRange(new Control[] { lblTieuDe, txtGiaiThich, l1, dgvNCC, l2, cboNCC, chkConNo, l3, numSoNgay, btnLuuNgay,
                dgvPhieu, lblPhieu, l4, txtSoTien, l5, cboHinhThuc, btnTra, btnThoat });

            GiaoDien.ThemBangDuoi(this, "Các lần đã trả tiền nhà cung cấp (mới nhất ở trên)", 220,
                () => AiHelper.Doc(
                    "SELECT TOP 300 t.MaTT AS [Mã phiếu chi], t.NgayTra AS [Ngày trả], t.MaPN AS [Phiếu nhập], ncc.TenNCC AS [Nhà cung cấp], " +
                    "       t.SoTien AS [Số tiền], t.HinhThuc AS [Trả bằng], t.NguoiLap AS [Người lập] " +
                    "FROM ThanhToanNCC t JOIN PhieuNhap pn ON pn.MaPN = t.MaPN JOIN NhaCungCap ncc ON ncc.MaNCC = pn.MaNCC ORDER BY t.NgayTra DESC"),
                null);

            cboNCC.SelectionChangeCommitted += (s, e) => { NapSoNgay(); NapPhieu(); };
            chkConNo.CheckedChanged += (s, e) => NapPhieu();
            dgvNCC.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvNCC.Rows.Count) return;
                cboNCC.SelectedValue = Convert.ToString(dgvNCC.Rows[e.RowIndex].Cells["Mã NCC"].Value);
                NapSoNgay();
                NapPhieu();
            };
            dgvPhieu.CellClick += (s, e) => { if (e.RowIndex >= 0 && e.RowIndex < dgvPhieu.Rows.Count) ChonPhieu(dgvPhieu.Rows[e.RowIndex]); };
            btnLuuNgay.Click += (s, e) => LuuSoNgay();
            btnTra.Click += (s, e) => TraTien();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvNCC);
                AiHelper.DinhDangLuoi(dgvPhieu);
                try
                {
                    DataTable ncc = AiHelper.Doc("SELECT CAST('' AS VARCHAR(20)) AS MaNCC, N'" + TAT_CA + "' AS Ten UNION ALL " +
                                                 "SELECT MaNCC, MaNCC + N' - ' + TenNCC FROM NhaCungCap ORDER BY MaNCC");
                    AiHelper.NapCombo(cboNCC, ncc, "MaNCC", "Ten");
                }
                catch (Exception ex) { AiHelper.BaoLoi(ex); }
                NapTong();
                NapSoNgay();
                NapPhieu();
            };

            GiaoDien.GanChoForm(this);
        }

        private string MaNCC()
        {
            return Convert.ToString(cboNCC.SelectedValue ?? "");
        }

        private void NapTong()
        {
            try
            {
                dgvNCC.DataSource = AiHelper.Doc(
                    "SELECT MaNCC AS [Mã NCC], TenNCC AS [Nhà cung cấp], MAX(SoNgayCongNo) AS [Được nợ (ngày)], COUNT(*) AS [Số phiếu nhập], " +
                    "       SUM(TongTien) AS [Tổng tiền nhập], SUM(DaTra) AS [Đã trả], SUM(ConNo) AS [Còn nợ], " +
                    "       SUM(CASE WHEN TinhTrang = N'Quá hạn' THEN ConNo ELSE 0 END) AS [Nợ quá hạn], " +
                    "       MIN(CASE WHEN ConNo > 0 THEN HanTra END) AS [Hạn trả gần nhất] " +
                    "FROM dbo.vw_CongNoNhaCungCap GROUP BY MaNCC, TenNCC ORDER BY MaNCC");
                foreach (string c in new[] { "Tổng tiền nhập", "Đã trả", "Còn nợ", "Nợ quá hạn" }) AiHelper.DinhDangSo(dgvNCC, c, "N0");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NapSoNgay()
        {
            try
            {
                string ma = MaNCC();
                numSoNgay.Enabled = ma.Length > 0;
                if (ma.Length == 0) return;
                object v = AiHelper.LayGiaTri("SELECT SoNgayCongNo FROM NhaCungCap WHERE MaNCC = @m", AiHelper.P("@m", ma));
                numSoNgay.Value = v == null ? 0 : Math.Min(365, Convert.ToInt32(v));
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NapPhieu()
        {
            try
            {
                dgvPhieu.DataSource = AiHelper.Doc(
                    "SELECT MaPN AS [Phiếu nhập], NgayNhap AS [Ngày nhập], TenNCC AS [Nhà cung cấp], MaKho AS [Kho], TongTien AS [Tiền phiếu], " +
                    "       DaTra AS [Đã trả], ConNo AS [Còn nợ], HanTra AS [Hạn trả], LanTraCuoi AS [Lần trả gần nhất], TinhTrang AS [Tình trạng] " +
                    "FROM dbo.vw_CongNoNhaCungCap WHERE (@m = '' OR MaNCC = @m) AND (@c = 0 OR ConNo > 0) " +
                    "ORDER BY CASE TinhTrang WHEN N'Quá hạn' THEN 0 WHEN N'Chưa tới hạn' THEN 1 ELSE 2 END, HanTra",
                    AiHelper.P("@m", MaNCC()), AiHelper.P("@c", chkConNo.Checked ? 1 : 0));
                foreach (string c in new[] { "Tiền phiếu", "Đã trả", "Còn nợ" }) AiHelper.DinhDangSo(dgvPhieu, c, "N0");
                _maPN = null;
                lblPhieu.Text = "Chưa chọn phiếu nhập.";
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void ChonPhieu(DataGridViewRow r)
        {
            _maPN = Convert.ToString(r.Cells["Phiếu nhập"].Value);
            _conNo = Convert.ToDecimal(r.Cells["Còn nợ"].Value);
            lblPhieu.Text = "Phiếu " + _maPN + " - " + r.Cells["Nhà cung cấp"].Value + ": còn nợ " + _conNo.ToString("N0") + " đ, hạn trả " +
                            Convert.ToDateTime(r.Cells["Hạn trả"].Value).ToString("dd/MM/yyyy") + " (" + r.Cells["Tình trạng"].Value + ").";
            txtSoTien.Text = _conNo > 0 ? _conNo.ToString("0") : "";
        }

        private void LuuSoNgay()
        {
            string ma = MaNCC();
            if (ma.Length == 0) { AiHelper.BaoTin("Hãy chọn một nhà cung cấp trước."); return; }
            try
            {
                AiHelper.Chay("UPDATE NhaCungCap SET SoNgayCongNo = @n WHERE MaNCC = @m", AiHelper.P("@n", (int)numSoNgay.Value), AiHelper.P("@m", ma));
                NapTong();
                NapPhieu();
                AiHelper.BaoTin("Đã lưu. Hạn trả của các phiếu nhập của nhà cung cấp này được tính lại theo " + numSoNgay.Value + " ngày.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void TraTien()
        {
            if (_maPN == null) { AiHelper.BaoTin("Hãy bấm chọn một phiếu nhập ở bảng trên."); return; }
            decimal soTien;
            if (!AiHelper.DocSoTien(txtSoTien.Text, out soTien) || soTien <= 0) { AiHelper.BaoTin("Số tiền trả phải là số lớn hơn 0."); return; }
            if (MessageBox.Show("Ghi đã trả " + soTien.ToString("N0") + " đ (" + cboHinhThuc.SelectedItem + ") cho phiếu nhập " + _maPN + "?",
                                "Xác nhận trả tiền", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                string maTT = AiHelper.ChayThuTucLayMa("sp_TraTienNhaCungCap", "@MaTT",
                    AiHelper.P("@MaPN", _maPN), AiHelper.PDec("@SoTien", soTien, 18, 2), AiHelper.P("@HinhThuc", Convert.ToString(cboHinhThuc.SelectedItem)),
                    AiHelper.P("@NguoiLap", Session.HoTen));
                NapTong();
                NapPhieu();
                GiaoDien.TaiLai(this);
                AiHelper.BaoTin("Đã ghi phiếu chi " + maTT + ".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}
