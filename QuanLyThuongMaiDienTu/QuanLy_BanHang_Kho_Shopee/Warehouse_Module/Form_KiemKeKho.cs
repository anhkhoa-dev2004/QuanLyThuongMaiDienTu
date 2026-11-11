using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_KiemKeKho : Form
    {
        private readonly ComboBox cboKho, cboPhieu, cboTuKho, cboDenKho, cboSanPham;
        private readonly TextBox txtNguoiKiem;
        private readonly NumericUpDown numSoLuong;
        private readonly DataGridView dgvDem;
        private readonly Label lblPhieu, lblTonDi;
        private DataTable _dem;

        public Form_KiemKeKho()
        {
            Text = "Kiểm kê và chuyển kho";
            Name = "Form_KiemKeKho";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1040, 700);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "KIỂM KÊ KHO VÀ CHUYỂN HÀNG GIỮA CÁC KHO", AutoSize = true, Location = new Point(20, 10) };
            TextBox txtGiaiThich = new TextBox
            {
                Multiline = true, ReadOnly = true, TabStop = false, Location = new Point(20, 72), Size = new Size(1000, 52),
                Text = "Kiểm kê: lập phiếu (phần mềm ghi sẵn số tồn trên sổ), đi đếm từng món rồi nhập số đếm được vào cột \"Đếm thực tế\". Món nào lệch thì ghi lý do. " +
                       "Bấm \"Chốt kiểm kê\" thì tồn kho được sửa theo số đếm, phần thiếu được tính là hàng mất, hỏng trong màn Chi phí vận hành và lãi ròng. Nên kiểm kê sau giờ đóng gói, lúc kho không xuất hàng."
            };
            Label l1 = new Label { Text = "Kho:", Location = new Point(20, 138), Size = new Size(35, 22) };
            cboKho = new ComboBox { Location = new Point(58, 135), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l2 = new Label { Text = "Người đếm:", Location = new Point(272, 138), Size = new Size(70, 22) };
            txtNguoiKiem = new TextBox { Location = new Point(345, 135), Width = 180, Text = Session.HoTen ?? "" };
            Button btnLap = new Button { Text = "Lập phiếu kiểm kê mới", Location = new Point(540, 131), Size = new Size(180, 30) };
            Label l3 = new Label { Text = "Phiếu:", Location = new Point(20, 174), Size = new Size(45, 22) };
            cboPhieu = new ComboBox { Location = new Point(68, 171), Width = 520, DropDownStyle = ComboBoxStyle.DropDownList };
            lblPhieu = new Label { Location = new Point(600, 174), Size = new Size(420, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvDem = new DataGridView { Location = new Point(20, 204), Size = new Size(1000, 270) };
            Button btnLuuDem = new Button { Text = "Lưu số đếm", Location = new Point(20, 482), Size = new Size(140, 32) };
            Button btnChot = new Button { Text = "Chốt kiểm kê (sửa tồn kho)", Location = new Point(170, 482), Size = new Size(210, 32) };

            Label l4 = new Label { Text = "Chuyển hàng giữa các kho:", Location = new Point(20, 534), Size = new Size(300, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            Label l5 = new Label { Text = "Từ kho:", Location = new Point(20, 566), Size = new Size(50, 22) };
            cboTuKho = new ComboBox { Location = new Point(72, 563), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l6 = new Label { Text = "Đến kho:", Location = new Point(255, 566), Size = new Size(58, 22) };
            cboDenKho = new ComboBox { Location = new Point(315, 563), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l7 = new Label { Text = "Sản phẩm:", Location = new Point(500, 566), Size = new Size(65, 22) };
            cboSanPham = new ComboBox { Location = new Point(568, 563), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l8 = new Label { Text = "Số lượng:", Location = new Point(880, 566), Size = new Size(60, 22) };
            numSoLuong = new NumericUpDown { Location = new Point(942, 563), Width = 78, Minimum = 1, Maximum = 100000, Value = 1 };
            lblTonDi = new Label { Location = new Point(20, 598), Size = new Size(700, 22) };
            Button btnChuyen = new Button { Text = "Chuyển kho", Location = new Point(730, 594), Size = new Size(150, 32) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(900, 594), Size = new Size(120, 32) };

            Controls.AddRange(new Control[] { lblTieuDe, txtGiaiThich, l1, cboKho, l2, txtNguoiKiem, btnLap, l3, cboPhieu, lblPhieu, dgvDem, btnLuuDem, btnChot,
                l4, l5, cboTuKho, l6, cboDenKho, l7, cboSanPham, l8, numSoLuong, lblTonDi, btnChuyen, btnThoat });

            DataGridView dgvLichSu = GiaoDien.ThemBangDuoi(this, "Lịch sử kiểm kê lệch sổ và chuyển kho (mới nhất ở trên)", 200,
                () => AiHelper.Doc(
                    "SELECT TOP 300 ls.ThoiGianGD AS [Lúc], ls.LoaiGiaoDich AS [Việc], ls.MaThamChieu AS [Mã phiếu], ls.MaKho AS [Kho], " +
                    "       sp.TenSP AS [Sản phẩm], ls.SoLuong AS [Số lượng (+ thêm, - bớt)], ls.SoTien AS [Giá trị theo giá vốn], ls.MoTa AS [Ghi chú] " +
                    "FROM LichSuGiaoDich ls LEFT JOIN SanPham sp ON sp.MaSP = ls.MaSP " +
                    "WHERE ls.LoaiGiaoDich IN (N'Kiểm kê', N'Chuyển kho') ORDER BY ls.ThoiGianGD DESC, ls.MaLichSu DESC"),
                null);
            Control pnlLichSu = dgvLichSu.Parent;
            pnlLichSu.Dock = DockStyle.None;
            pnlLichSu.SetBounds(0, 640, 1040, 200);
            ClientSize = new Size(1040, 840);

            cboPhieu.SelectionChangeCommitted += (s, e) => NapDem();
            cboTuKho.SelectionChangeCommitted += (s, e) => HienTonDi();
            cboSanPham.SelectionChangeCommitted += (s, e) => HienTonDi();
            btnLap.Click += (s, e) => LapPhieu();
            btnLuuDem.Click += (s, e) => LuuDem(true);
            btnChot.Click += (s, e) => Chot();
            btnChuyen.Click += (s, e) => ChuyenKho();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) =>
            {
                if (!TopLevel) ClientSize = new Size(1040, 840);
                AiHelper.DinhDangLuoi(dgvDem);
                dgvDem.ReadOnly = false;
                try
                {
                    AiHelper.NapCombo(cboKho, AiHelper.Doc("SELECT MaKho, MaKho + N' - ' + TenKho AS Ten FROM Kho ORDER BY MaKho"), "MaKho", "Ten");
                    AiHelper.NapCombo(cboTuKho, AiHelper.Doc("SELECT MaKho, MaKho + N' - ' + TenKho AS Ten FROM Kho ORDER BY MaKho"), "MaKho", "Ten");
                    AiHelper.NapCombo(cboDenKho, AiHelper.Doc("SELECT MaKho, MaKho + N' - ' + TenKho AS Ten FROM Kho ORDER BY MaKho"), "MaKho", "Ten");
                    if (cboDenKho.Items.Count > 1) cboDenKho.SelectedIndex = 1;
                    AiHelper.NapCombo(cboSanPham, AiHelper.Doc("SELECT MaSP, MaSP + N' - ' + TenSP AS Ten FROM SanPham ORDER BY MaSP"), "MaSP", "Ten");
                }
                catch (Exception ex) { AiHelper.BaoLoi(ex); }
                NapPhieu(null);
                HienTonDi();
            };

            GiaoDien.GanChoForm(this);
        }

        private string MaPhieu()
        {
            return Convert.ToString(cboPhieu.SelectedValue ?? "");
        }

        private void NapPhieu(string chon)
        {
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT MaKK, MaKK + N' - ' + MaKho + N' - ' + FORMAT(NgayKiemKe, 'dd/MM/yyyy HH:mm') + N' - ' + TrangThai + ISNULL(N' - ' + NguoiKiem, N'') AS Ten " +
                    "FROM PhieuKiemKe ORDER BY NgayKiemKe DESC");
                AiHelper.NapCombo(cboPhieu, t, "MaKK", "Ten");
                if (chon != null) cboPhieu.SelectedValue = chon;
                NapDem();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NapDem()
        {
            string ma = MaPhieu();
            if (ma.Length == 0) { dgvDem.DataSource = null; lblPhieu.Text = "Chưa có phiếu kiểm kê."; return; }
            try
            {
                _dem = AiHelper.Doc(
                    "SELECT ct.MaSP AS [Mã SP], sp.TenSP AS [Sản phẩm], ct.SoSach AS [Tồn trên sổ], ct.ThucTe AS [Đếm thực tế], " +
                    "       ct.ChenhLech AS [Lệch], ct.LyDoLech AS [Lý do lệch], ISNULL(sp.GiaVon, 0) * ISNULL(ct.ChenhLech, 0) AS [Giá trị lệch theo giá vốn] " +
                    "FROM ChiTietKiemKe ct JOIN SanPham sp ON sp.MaSP = ct.MaSP WHERE ct.MaKK = @m ORDER BY ct.MaSP", AiHelper.P("@m", ma));
                dgvDem.DataSource = _dem;
                bool dangKiem = Convert.ToString(AiHelper.LayGiaTri("SELECT TrangThai FROM PhieuKiemKe WHERE MaKK = @m", AiHelper.P("@m", ma))) == "Đang kiểm";
                foreach (DataGridViewColumn c in dgvDem.Columns)
                    c.ReadOnly = !dangKiem || (c.Name != "Đếm thực tế" && c.Name != "Lý do lệch");
                AiHelper.DinhDangSo(dgvDem, "Giá trị lệch theo giá vốn", "N0");
                int lech = 0;
                foreach (DataRow r in _dem.Rows) if (r["Lệch"] != DBNull.Value && Convert.ToInt32(r["Lệch"]) != 0) lech++;
                lblPhieu.Text = dangKiem ? "Đang kiểm: nhập số đếm rồi bấm Lưu, đếm xong hết thì Chốt." : "Đã chốt: " + lech + " món lệch sổ.";
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void LapPhieu()
        {
            string kho = Convert.ToString(cboKho.SelectedValue ?? "");
            if (kho.Length == 0) { AiHelper.BaoTin("Hãy chọn kho cần kiểm."); return; }
            if (txtNguoiKiem.Text.Trim().Length == 0) { AiHelper.BaoTin("Hãy ghi tên người đếm."); return; }
            try
            {
                string ma = AiHelper.ChayThuTucLayMa("sp_LapPhieuKiemKe", "@MaKK", AiHelper.P("@MaKho", kho), AiHelper.P("@NguoiKiem", txtNguoiKiem.Text.Trim()));
                NapPhieu(ma);
                AiHelper.BaoTin("Đã lập phiếu " + ma + ". Phần mềm đã ghi sẵn số tồn trên sổ của từng món. Đếm xong món nào thì nhập vào cột \"Đếm thực tế\".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private bool LuuDem(bool baoXong)
        {
            string ma = MaPhieu();
            if (ma.Length == 0 || _dem == null) return false;
            dgvDem.EndEdit();
            try
            {
                foreach (DataRow r in _dem.Rows)
                {
                    object thucTe = r["Đếm thực tế"];
                    if (thucTe != DBNull.Value && Convert.ToInt32(thucTe) < 0) { AiHelper.BaoTin("Số đếm của " + r["Sản phẩm"] + " không được âm."); return false; }
                    string lyDo = Convert.ToString(r["Lý do lệch"]).Trim();
                    AiHelper.Chay("UPDATE ChiTietKiemKe SET ThucTe = @t, LyDoLech = @l WHERE MaKK = @m AND MaSP = @s AND EXISTS " +
                                  "(SELECT 1 FROM PhieuKiemKe WHERE MaKK = @m AND TrangThai = N'Đang kiểm')",
                                  AiHelper.P("@t", thucTe == DBNull.Value ? null : (object)Convert.ToInt32(thucTe)),
                                  AiHelper.P("@l", lyDo.Length == 0 ? null : lyDo), AiHelper.P("@m", ma), AiHelper.P("@s", Convert.ToString(r["Mã SP"])));
                }
                NapDem();
                if (baoXong) AiHelper.BaoTin("Đã lưu số đếm.");
                return true;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); return false; }
        }

        private void Chot()
        {
            string ma = MaPhieu();
            if (ma.Length == 0) { AiHelper.BaoTin("Chưa chọn phiếu kiểm kê."); return; }
            if (!LuuDem(false)) return;
            if (MessageBox.Show("Chốt phiếu " + ma + "? Tồn kho sẽ được sửa theo số đếm thực tế và không sửa lại phiếu được nữa.",
                                "Chốt kiểm kê", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                AiHelper.ChayThuTucLayMa("sp_ChotKiemKe", null, AiHelper.P("@MaKK", ma));
                NapPhieu(ma);
                GiaoDien.TaiLai(this);
                AiHelper.BaoTin("Đã chốt kiểm kê, tồn kho đã sửa theo số đếm.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void HienTonDi()
        {
            try
            {
                object ton = AiHelper.LayGiaTri("SELECT SoLuongTon FROM TonKho WHERE MaKho = @k AND MaSP = @s",
                                                AiHelper.P("@k", Convert.ToString(cboTuKho.SelectedValue ?? "")), AiHelper.P("@s", Convert.ToString(cboSanPham.SelectedValue ?? "")));
                lblTonDi.Text = "Kho đi đang có: " + (ton == null ? 0 : Convert.ToInt32(ton)) + " cái.";
            }
            catch (Exception ex) { lblTonDi.Text = "Chưa đọc được tồn kho: " + AiHelper.LoiDeHieu(ex); }
        }

        private void ChuyenKho()
        {
            string tu = Convert.ToString(cboTuKho.SelectedValue ?? ""), den = Convert.ToString(cboDenKho.SelectedValue ?? ""), sp = Convert.ToString(cboSanPham.SelectedValue ?? "");
            if (tu.Length == 0 || den.Length == 0 || sp.Length == 0) { AiHelper.BaoTin("Hãy chọn kho đi, kho đến và sản phẩm."); return; }
            if (tu == den) { AiHelper.BaoTin("Kho đi và kho đến phải khác nhau."); return; }
            if (MessageBox.Show("Chuyển " + numSoLuong.Value + " cái " + cboSanPham.Text + " từ " + tu + " sang " + den + "?", "Chuyển kho",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                string ma = AiHelper.ChayThuTucLayMa("sp_ChuyenKho", "@MaCK", AiHelper.P("@TuKho", tu), AiHelper.P("@DenKho", den), AiHelper.P("@MaSP", sp),
                                                     AiHelper.P("@SoLuong", (int)numSoLuong.Value), AiHelper.P("@NguoiLap", Session.HoTen));
                HienTonDi();
                GiaoDien.TaiLai(this);
                AiHelper.BaoTin("Đã lập phiếu chuyển kho " + ma + ".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}
