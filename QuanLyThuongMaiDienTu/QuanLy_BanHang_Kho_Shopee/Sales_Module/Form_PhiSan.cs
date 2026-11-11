using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_PhiSan : Form
    {
        private const string GIAI_THICH =
            "Sàn thương mại điện tử giữ lại một phần tiền mỗi đơn trước khi chuyển cho shop. Bảng này ghi các mức phí đó để phần mềm tính \"tiền shop thực nhận\" và \"lãi gộp\". " +
            "Phí chỉ tính cho đơn đã giao xong. Mức phí đổi theo ngành hàng và theo thời gian, nên mỗi dòng có ngày bắt đầu, ngày kết thúc.\r\n" +
            "Lấy số đúng của shop mình: Kênh Người Bán Shopee > Tài chính > Doanh thu > tải \"Báo cáo thu nhập\" (có từng khoản phí của từng đơn).\r\n" +
            "Cách tính: \"Phần trăm tiền hàng\" = (tiền hàng - giảm giá của shop) x %;  \"Phần trăm tiền khách trả\" = (tiền hàng - giảm giá của shop + phí ship khách trả) x %;  \"Số tiền mỗi đơn\" = cố định mỗi đơn.\r\n" +
            "Phí cố định của Shopee khác nhau theo ngành hàng: chọn Danh mục thì dòng phí đó chỉ tính trên tiền các món thuộc danh mục ấy (chỉ dùng với \"Phần trăm tiền hàng\").";

        private readonly DataGridView dgvPhi;
        private readonly ComboBox cboKenh, cboLoaiPhi, cboCachTinh, cboDanhMuc;
        private readonly TextBox txtGiaTri, txtNguon;
        private readonly DateTimePicker dtTu, dtDen;
        private readonly CheckBox chkCoDen;
        private int? _idDangChon;

        public Form_PhiSan()
        {
            Text = "Phí sàn theo kênh bán";
            Name = "Form_PhiSan";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1000, 700);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "PHÍ SÀN THEO KÊNH BÁN", AutoSize = true, Location = new Point(20, 10) };
            TextBox txtGiaiThich = new TextBox { Multiline = true, ReadOnly = true, TabStop = false, Location = new Point(20, 72), Size = new Size(960, 96), Text = GIAI_THICH };
            dgvPhi = new DataGridView { Location = new Point(20, 176), Size = new Size(960, 280) };

            Label l1 = Nhan("Kênh bán:", 20, 470, 90);
            cboKenh = new ComboBox { Location = new Point(115, 467), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l2 = Nhan("Loại phí:", 375, 470, 70);
            cboLoaiPhi = new ComboBox { Location = new Point(450, 467), Width = 220, DropDownStyle = ComboBoxStyle.DropDown };
            cboLoaiPhi.Items.AddRange(new object[] { "Phí xử lý giao dịch", "Phí cố định", "Phí hạ tầng", "Phí dịch vụ", "Thuế sàn khấu trừ" });
            Label l3 = Nhan("Cách tính:", 690, 470, 70);
            cboCachTinh = new ComboBox { Location = new Point(765, 467), Width = 215, DropDownStyle = ComboBoxStyle.DropDownList };
            cboCachTinh.Items.AddRange(new object[] { "Phần trăm tiền hàng", "Phần trăm tiền khách trả", "Số tiền mỗi đơn" });
            cboCachTinh.SelectedIndex = 0;

            Label l4 = Nhan("Giá trị (% hoặc đồng):", 20, 506, 150);
            txtGiaTri = new TextBox { Location = new Point(175, 503), Width = 120 };
            Label l5 = Nhan("Áp từ ngày:", 315, 506, 80);
            dtTu = new DateTimePicker { Location = new Point(398, 503), Width = 110, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };
            chkCoDen = new CheckBox { Text = "Đến ngày:", Location = new Point(525, 504), Size = new Size(85, 22) };
            dtDen = new DateTimePicker { Location = new Point(612, 503), Width = 110, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Enabled = false };
            Label l7 = Nhan("Danh mục:", 735, 506, 70);
            cboDanhMuc = new ComboBox { Location = new Point(808, 503), Width = 172, DropDownStyle = ComboBoxStyle.DropDownList };

            Label l6 = Nhan("Lấy số từ đâu:", 20, 542, 100);
            txtNguon = new TextBox { Location = new Point(125, 539), Width = 855 };

            Button btnThem = new Button { Text = "Thêm dòng mới", Location = new Point(20, 584), Size = new Size(150, 32) };
            Button btnSua = new Button { Text = "Lưu sửa dòng đang chọn", Location = new Point(180, 584), Size = new Size(190, 32) };
            Button btnXoa = new Button { Text = "Xóa dòng đang chọn", Location = new Point(380, 584), Size = new Size(160, 32) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(860, 584), Size = new Size(120, 32) };

            Controls.AddRange(new Control[]
            {
                lblTieuDe, txtGiaiThich, dgvPhi, l1, cboKenh, l2, cboLoaiPhi, l3, cboCachTinh, l4, txtGiaTri, l5, dtTu, chkCoDen, dtDen,
                l7, cboDanhMuc, l6, txtNguon, btnThem, btnSua, btnXoa, btnThoat
            });

            chkCoDen.CheckedChanged += (s, e) => dtDen.Enabled = chkCoDen.Checked;
            dgvPhi.CellClick += (s, e) => { if (e.RowIndex >= 0 && e.RowIndex < dgvPhi.Rows.Count) ChonDong(dgvPhi.Rows[e.RowIndex]); };
            btnThem.Click += (s, e) => Luu(true);
            btnSua.Click += (s, e) => Luu(false);
            btnXoa.Click += (s, e) => Xoa();
            btnThoat.Click += (s, e) => Close();
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvPhi);
                try
                {
                    AiHelper.NapCombo(cboKenh, DonHangShopee.LayKenhBan(), "MaDoiTac", "TenDoiTac");
                    DataTable dm = AiHelper.Doc("SELECT CAST('' AS VARCHAR(20)) AS MaDanhMuc, N'(Cả đơn)' AS Hien UNION ALL " +
                                                "SELECT MaDanhMuc, MaDanhMuc + N' - ' + TenDanhMuc FROM DanhMuc ORDER BY MaDanhMuc");
                    AiHelper.NapCombo(cboDanhMuc, dm, "MaDanhMuc", "Hien");
                }
                catch (Exception ex) { AiHelper.BaoLoi(ex); }
                LoadDanhSach();
            };

            GiaoDien.GanChoForm(this);
        }

        private static Label Nhan(string chu, int x, int y, int rong)
        {
            return new Label { Text = chu, Location = new Point(x, y), Size = new Size(rong, 22) };
        }

        private void LoadDanhSach()
        {
            try
            {
                dgvPhi.DataSource = AiHelper.Doc(
                    "SELECT p.Id AS [Số dòng], p.MaDoiTac AS [Mã kênh], dt.TenDoiTac AS [Kênh bán], p.LoaiPhi AS [Loại phí], " +
                    "       ISNULL(p.MaDanhMuc, '') AS [Mã danh mục], ISNULL(dm.TenDanhMuc, N'(Cả đơn)') AS [Danh mục], p.CachTinh AS [Cách tính], " +
                    "       p.GiaTri AS [Giá trị], p.TuNgay AS [Từ ngày], p.DenNgay AS [Đến ngày], p.NguonThongTin AS [Lấy số từ đâu] " +
                    "FROM PhiSan p JOIN DoiTac dt ON dt.MaDoiTac = p.MaDoiTac LEFT JOIN DanhMuc dm ON dm.MaDanhMuc = p.MaDanhMuc " +
                    "ORDER BY p.MaDoiTac, p.LoaiPhi, p.MaDanhMuc, p.TuNgay");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void ChonDong(DataGridViewRow r)
        {
            _idDangChon = Convert.ToInt32(r.Cells["Số dòng"].Value);
            cboKenh.SelectedValue = Convert.ToString(r.Cells["Mã kênh"].Value);
            cboLoaiPhi.Text = Convert.ToString(r.Cells["Loại phí"].Value);
            cboCachTinh.SelectedItem = Convert.ToString(r.Cells["Cách tính"].Value);
            cboDanhMuc.SelectedValue = Convert.ToString(r.Cells["Mã danh mục"].Value);
            txtGiaTri.Text = Convert.ToDecimal(r.Cells["Giá trị"].Value).ToString("0.####", CultureInfo.InvariantCulture);
            dtTu.Value = Convert.ToDateTime(r.Cells["Từ ngày"].Value);
            object den = r.Cells["Đến ngày"].Value;
            chkCoDen.Checked = den != null && den != DBNull.Value;
            if (chkCoDen.Checked) dtDen.Value = Convert.ToDateTime(den);
            txtNguon.Text = Convert.ToString(r.Cells["Lấy số từ đâu"].Value);
        }

        private void Luu(bool themMoi)
        {
            if (!themMoi && !_idDangChon.HasValue) { AiHelper.BaoTin("Hãy bấm chọn một dòng trong bảng trước."); return; }
            if (cboKenh.SelectedValue == null) { AiHelper.BaoTin("Hãy chọn kênh bán."); return; }
            string loai = cboLoaiPhi.Text.Trim();
            if (loai.Length == 0) { AiHelper.BaoTin("Hãy nhập loại phí."); return; }
            decimal giaTri;
            if (!decimal.TryParse(txtGiaTri.Text.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out giaTri) || giaTri < 0)
            {
                AiHelper.BaoTin("Giá trị phải là số không âm. Phần trăm thì ghi như 6 hoặc 4.91; số tiền thì ghi như 3000.");
                return;
            }
            string cach = Convert.ToString(cboCachTinh.SelectedItem);
            if (cach != "Số tiền mỗi đơn" && giaTri > 100) { AiHelper.BaoTin("Phần trăm không được lớn hơn 100."); return; }
            string maDM = Convert.ToString(cboDanhMuc.SelectedValue);
            if (maDM.Length > 0 && cach != "Phần trăm tiền hàng")
            {
                AiHelper.BaoTin("Dòng phí theo danh mục chỉ dùng cách tính \"Phần trăm tiền hàng\". Muốn áp cho cả đơn thì chọn Danh mục là (Cả đơn).");
                return;
            }
            if (chkCoDen.Checked && dtDen.Value.Date < dtTu.Value.Date) { AiHelper.BaoTin("Ngày kết thúc phải sau ngày bắt đầu."); return; }
            if (txtNguon.Text.Trim().Length == 0) { AiHelper.BaoTin("Hãy ghi lấy số này từ đâu (ví dụ: Báo cáo thu nhập Shopee tháng 09/2026) để sau này kiểm tra lại."); return; }

            try
            {
                List<SqlParameter> ps = new List<SqlParameter>
                {
                    AiHelper.P("@k", Convert.ToString(cboKenh.SelectedValue)), AiHelper.P("@l", loai), AiHelper.P("@c", cach),
                    AiHelper.PDec("@g", giaTri, 18, 4), AiHelper.P("@tu", dtTu.Value.Date),
                    AiHelper.P("@den", chkCoDen.Checked ? (object)dtDen.Value.Date : null), AiHelper.P("@n", txtNguon.Text.Trim()),
                    AiHelper.P("@dm", maDM.Length > 0 ? (object)maDM : null)
                };
                if (themMoi)
                {
                    AiHelper.Chay("INSERT INTO PhiSan (MaDoiTac, MaDanhMuc, LoaiPhi, CachTinh, GiaTri, TuNgay, DenNgay, NguonThongTin) VALUES (@k, @dm, @l, @c, @g, @tu, @den, @n)",
                                  ps.ToArray());
                }
                else
                {
                    ps.Add(AiHelper.P("@id", _idDangChon.Value));
                    AiHelper.Chay("UPDATE PhiSan SET MaDoiTac = @k, MaDanhMuc = @dm, LoaiPhi = @l, CachTinh = @c, GiaTri = @g, TuNgay = @tu, DenNgay = @den, NguonThongTin = @n WHERE Id = @id",
                                  ps.ToArray());
                }
                LoadDanhSach();
                AiHelper.BaoTin(themMoi ? "Đã thêm mức phí. Báo cáo doanh thu và tiền thực nhận sẽ tính theo mức mới." : "Đã lưu thay đổi.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Xoa()
        {
            if (!_idDangChon.HasValue) { AiHelper.BaoTin("Hãy bấm chọn một dòng trong bảng trước."); return; }
            if (MessageBox.Show("Xóa dòng phí số " + _idDangChon.Value + "? Các đơn cũ sẽ được tính lại không có khoản phí này.", "Xác nhận",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                AiHelper.Chay("DELETE FROM PhiSan WHERE Id = @id", AiHelper.P("@id", _idDangChon.Value));
                _idDangChon = null;
                LoadDanhSach();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }
    }
}
