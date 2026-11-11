using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;
using QuanLy_BanHang_Kho_Shopee.Sales_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_BaoCaoDoanhThu : Form
    {
        private const string TAT_CA = "(Tất cả kênh)";

        private const string GIAI_THICH =
            "Cách đọc: Tiền hàng là tiền bán theo giá trên đơn (không tính đơn hủy). Shop không nhận đủ số này: phải trừ giảm giá shop tự bỏ ra, " +
            "phí sàn (chỉ tính khi đơn đã giao, theo bảng \"Phí sàn theo kênh bán\"), tiền hoàn cho khách trả hàng. Còn lại là tiền shop thực nhận. " +
            "Lấy tiền thực nhận trừ giá vốn (giá nhập bình quân của hàng đã bán, hàng khách trả còn bán được thì không tính) ra lãi gộp. " +
            "Lãi gộp chưa trừ lương, mặt bằng, quảng cáo, đóng gói.";

        private const string SQL_CHON =
            "COUNT(*) AS [Số đơn], SUM(DaGiao) AS [Đơn đã giao], SUM(TienHang) AS [Tiền hàng], SUM(GiamGiaCuaShop) AS [Giảm giá của shop], " +
            "SUM(PhiSan) AS [Phí sàn], SUM(TienHoanKhach) AS [Tiền hoàn khách], SUM(TienShopNhan) AS [Shop thực nhận], " +
            "SUM(GiaVonHangBan) AS [Giá vốn], SUM(LaiGop) AS [Lãi gộp], " +
            "CAST(100.0 * SUM(LaiGop) / NULLIF(SUM(TienShopNhan), 0) AS DECIMAL(6,1)) AS [Lãi gộp / thực nhận (%)] ";

        private readonly DateTimePicker dtThang;
        private readonly ComboBox cboKenh;
        private readonly Label lblTongKet;
        private readonly DataGridView dgvKenh, dgvThang;

        public Form_BaoCaoDoanhThu()
        {
            Text = "Báo cáo doanh thu";
            Name = "Form_BaoCaoDoanhThu";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1040, 820);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "BÁO CÁO DOANH THU VÀ TIỀN THỰC NHẬN", AutoSize = true, Location = new Point(20, 10) };
            Label l1 = new Label { Text = "Tháng:", Location = new Point(20, 76), Size = new Size(50, 22) };
            dtThang = new DateTimePicker { Location = new Point(72, 72), Width = 100, Format = DateTimePickerFormat.Custom, CustomFormat = "MM/yyyy", ShowUpDown = true };
            Label l2 = new Label { Text = "Kênh bán:", Location = new Point(190, 76), Size = new Size(70, 22) };
            cboKenh = new ComboBox { Location = new Point(262, 72), Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
            Button btnXem = new Button { Text = "Thống kê", Location = new Point(510, 69), Size = new Size(120, 30) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(900, 69), Size = new Size(120, 30) };

            TextBox txtGiaiThich = new TextBox { Multiline = true, ReadOnly = true, TabStop = false, Location = new Point(20, 108), Size = new Size(1000, 66), Text = GIAI_THICH };
            lblTongKet = new Label { Location = new Point(20, 182), Size = new Size(1000, 172), Font = new Font("Consolas", 10F), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(8) };
            Label l3 = new Label { Text = "Tháng đang chọn, chia theo kênh bán:", Location = new Point(20, 362), Size = new Size(600, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvKenh = new DataGridView { Location = new Point(20, 386), Size = new Size(1000, 120) };
            Label l4 = new Label { Text = "Từng tháng (theo kênh đang chọn) - bấm 1 dòng để xem tháng đó:", Location = new Point(20, 514), Size = new Size(700, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            dgvThang = new DataGridView { Location = new Point(20, 538), Size = new Size(1000, 272) };

            Controls.AddRange(new Control[] { lblTieuDe, l1, dtThang, l2, cboKenh, btnXem, btnThoat, txtGiaiThich, lblTongKet, l3, dgvKenh, l4, dgvThang });

            btnXem.Click += (s, e) => LoadBaoCao();
            btnThoat.Click += (s, e) => Close();
            cboKenh.SelectionChangeCommitted += (s, e) => LoadBaoCao();
            dgvThang.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvThang.Rows.Count) return;
                string th = Convert.ToString(dgvThang.Rows[e.RowIndex].Cells["Tháng"].Value);
                string[] p = th.Split('/');
                int m, y;
                if (p.Length == 2 && int.TryParse(p[0], out m) && int.TryParse(p[1], out y))
                {
                    dtThang.Value = new DateTime(y, m, 1);
                    LoadBaoCao();
                }
            };
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvKenh);
                AiHelper.DinhDangLuoi(dgvThang);
                NapBoLoc();
                LoadBaoCao();
            };

            GiaoDien.GanChoForm(this);
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
                object moiNhat = AiHelper.LayGiaTri("SELECT MAX(NgayDat) FROM DonHang WHERE ISNULL(TrangThai, N'') NOT LIKE N'%hủy%'");
                DateTime ngay = moiNhat == null ? DateTime.Today : Convert.ToDateTime(moiNhat);
                dtThang.Value = new DateTime(ngay.Year, ngay.Month, 1);
            }
            catch (Exception ex) { lblTongKet.Text = "Chưa đọc được kênh bán: " + AiHelper.LoiDeHieu(ex); }
        }

        private string KenhDangChon()
        {
            string k = Convert.ToString(cboKenh.SelectedValue);
            return string.IsNullOrEmpty(k) ? null : k;
        }

        private void LoadBaoCao()
        {
            DateTime tu = new DateTime(dtThang.Value.Year, dtThang.Value.Month, 1);
            DateTime den = tu.AddMonths(1);
            string kenh = KenhDangChon();
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT " + SQL_CHON + "FROM dbo.vw_TienDonHang " +
                    "WHERE NgayDat >= @tu AND NgayDat < @den AND DaHuy = 0 AND (@k IS NULL OR MaDoiTac = @k)",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den), AiHelper.P("@k", kenh));
                object huy = AiHelper.LayGiaTri(
                    "SELECT COUNT(*) FROM dbo.vw_TienDonHang WHERE NgayDat >= @tu AND NgayDat < @den AND DaHuy = 1 AND (@k IS NULL OR MaDoiTac = @k)",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den), AiHelper.P("@k", kenh));
                lblTongKet.Text = TongKet(t.Rows[0], tu, Convert.ToInt32(huy ?? 0));

                dgvKenh.DataSource = AiHelper.Doc(
                    "SELECT KenhBan AS [Kênh bán], " + SQL_CHON + "FROM dbo.vw_TienDonHang " +
                    "WHERE NgayDat >= @tu AND NgayDat < @den AND DaHuy = 0 GROUP BY KenhBan ORDER BY SUM(TienHang) DESC",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den));

                dgvThang.DataSource = AiHelper.Doc(
                    "SELECT RIGHT(CONVERT(CHAR(7), Ngay, 120), 2) + '/' + LEFT(CONVERT(CHAR(7), Ngay, 120), 4) AS [Tháng], " + SQL_CHON +
                    "FROM dbo.vw_TienDonHang WHERE DaHuy = 0 AND (@k IS NULL OR MaDoiTac = @k) " +
                    "GROUP BY CONVERT(CHAR(7), Ngay, 120) ORDER BY CONVERT(CHAR(7), Ngay, 120) DESC",
                    AiHelper.P("@k", kenh));
            }
            catch (Exception ex)
            {
                lblTongKet.Text = "Chưa thống kê được: " + AiHelper.LoiDeHieu(ex);
            }
        }

        private static decimal So(DataRow r, string cot)
        {
            return r[cot] == DBNull.Value ? 0 : Convert.ToDecimal(r[cot]);
        }

        private string TongKet(DataRow r, DateTime thang, int soHuy)
        {
            decimal tienHang = So(r, "Tiền hàng"), giam = So(r, "Giảm giá của shop"), phi = So(r, "Phí sàn"), hoan = So(r, "Tiền hoàn khách");
            decimal nhan = So(r, "Shop thực nhận"), giaVon = So(r, "Giá vốn"), lai = So(r, "Lãi gộp");
            int soDon = Convert.ToInt32(So(r, "Số đơn")), daGiao = Convert.ToInt32(So(r, "Đơn đã giao"));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Tháng " + thang.ToString("MM/yyyy") + " - " + cboKenh.Text + ": " + soDon.ToString("N0") + " đơn không hủy (đã giao " +
                          daGiao.ToString("N0") + "), " + soHuy.ToString("N0") + " đơn hủy.");
            sb.AppendLine(Dong("Tiền hàng", tienHang, ""));
            sb.AppendLine(Dong("Giảm giá của shop", giam, "-"));
            sb.AppendLine(Dong("Phí sàn", phi, "-"));
            sb.AppendLine(Dong("Tiền hoàn cho khách", hoan, "-"));
            sb.AppendLine(Dong("Shop thực nhận", nhan, "="));
            sb.AppendLine(Dong("Giá vốn hàng đã bán", giaVon, "-"));
            sb.Append(Dong("Lãi gộp", lai, "=") + (nhan != 0 ? "   (" + (100m * lai / nhan).ToString("0.0") + "% tiền thực nhận)" : ""));
            if (soDon > daGiao) sb.Append("\nCòn " + (soDon - daGiao) + " đơn chưa giao xong: đã có trong tiền hàng nhưng chưa tính phí sàn, tiền thực nhận và lãi.");
            return sb.ToString();
        }

        private static string Dong(string ten, decimal tien, string dau)
        {
            return (dau.Length == 0 ? "  " : dau + " ") + ten.PadRight(24) + tien.ToString("N0").PadLeft(18) + " đ";
        }
    }
}
