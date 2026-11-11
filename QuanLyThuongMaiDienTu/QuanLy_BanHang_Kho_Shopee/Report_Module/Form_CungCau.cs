using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class Form_CungCau : Form
    {
        private class KhungVe : Panel
        {
            public KhungVe()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
                BorderStyle = BorderStyle.FixedSingle;
                BackColor = Color.White;
            }
        }

        private const string GIAI_THICH =
            "Cung = hàng mình đang có để bán (tồn kho + hàng mới nhập). Cầu = số cái khách đặt mua (đơn hủy không tính).\r\n" +
            "• Cung > Cầu: hàng nhiều, khách ít → dễ bị ép giá, tiền nằm chết trong kho → dừng nhập, xả hàng sớm.\r\n" +
            "• Cung < Cầu: hàng ít, khách nhiều → bán được giá, chưa cần giảm → nhập thêm vừa đủ, đừng ôm quá nhiều.\r\n" +
            "• Cung = Cầu: cân bằng → lời đủ là được, nhập đều tay theo tốc độ bán.\r\n" +
            "Món theo trend đi 3 bước: Đầu sóng (khách đổ vào) → Giữa sóng (bán đều) → Cuối sóng (khách bớt). Kiếm tiền ở đầu sóng, chốt lời ở giữa sóng, dừng trước cuối sóng.\r\n" +
            "Thu tiền về = bán hết hàng trong kho mất bao nhiêu ngày. Ngày sale khách đông nhưng vẫn có thể lỗ vì bán rẻ, nên máy so lãi mỗi cái ngày sale với ngày thường.";

        private readonly Label lblTongQuan, lblThang;
        private readonly DataGridView dgvSanPham, dgvThang;
        private readonly KhungVe pnlCungCau, pnlGia;

        private readonly List<string> _thang = new List<string>();
        private readonly List<double> _cau = new List<double>(), _cung = new List<double>(), _gia = new List<double>();
        private string _tenSP = "";

        public Form_CungCau()
        {
            Text = "Cung - Cầu: hàng nhiều hay khách nhiều";
            Name = "Form_CungCau";
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1100, 1010);

            Label lblTieuDe = new Label { Name = "lblTieuDe", Text = "CUNG - CẦU: HÀNG NHIỀU HAY KHÁCH NHIỀU", AutoSize = true, Location = new Point(20, 10) };
            TextBox txtGiaiThich = new TextBox
            {
                Multiline = true, ReadOnly = true, TabStop = false,
                Location = new Point(22, 70), Size = new Size(1066, 112),
                Text = GIAI_THICH
            };
            lblTongQuan = new Label { Location = new Point(22, 190), Size = new Size(940, 22), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            Button btnTaiLai = new Button { Text = "Tải lại", Location = new Point(988, 186), Size = new Size(100, 30) };
            dgvSanPham = new DataGridView { Location = new Point(22, 220), Size = new Size(1066, 270) };
            lblThang = new Label { Location = new Point(22, 498), Size = new Size(1066, 22), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            pnlCungCau = new KhungVe { Location = new Point(22, 524), Size = new Size(640, 250) };
            pnlGia = new KhungVe { Location = new Point(670, 524), Size = new Size(418, 250) };
            dgvThang = new DataGridView { Location = new Point(22, 782), Size = new Size(1066, 180) };
            Button btnThoat = new Button { Text = "Thoát", Location = new Point(988, 970), Size = new Size(100, 30) };

            Controls.AddRange(new Control[] { lblTieuDe, txtGiaiThich, lblTongQuan, btnTaiLai, dgvSanPham, lblThang, pnlCungCau, pnlGia, dgvThang, btnThoat });

            pnlCungCau.Paint += (s, e) => AiHelper.VeDuong(e.Graphics, pnlCungCau.ClientRectangle, _thang, _cau, _cung,
                "Cầu và Cung từng tháng: " + _tenSP, "Cầu (khách đặt)", "Cung (hàng có để bán)");
            pnlGia.Paint += (s, e) => AiHelper.VeCot(e.Graphics, pnlGia.ClientRectangle, _thang, _gia, "Giá thực bán trung bình (nghìn đồng)");

            btnTaiLai.Click += (s, e) => LoadDanhSach();
            btnThoat.Click += (s, e) => Close();
            dgvSanPham.SelectionChanged += (s, e) => NapTheoThang();
            Load += (s, e) =>
            {
                AiHelper.DinhDangLuoi(dgvSanPham);
                AiHelper.DinhDangLuoi(dgvThang);
                LoadDanhSach();
            };

            GiaoDien.GanChoForm(this);
        }

        private void LoadDanhSach()
        {
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT MaSP AS [Mã SP], TenSP AS [Sản phẩm], CungCau AS [Cung - Cầu], GiaiDoan AS [Đang ở sóng nào], " +
                    "       TonHienTai AS [Tồn], DuDoan30Ngay AS [Dự đoán bán 30 ngày tới], " +
                    "       CAST(GiaThucBan30 AS DECIMAL(18, 0)) AS [Giá thực bán 30 ngày], GiaDoiPT AS [Giá đổi so với 2 tháng trước (%)], GiaXuHuong AS [Giá], " +
                    "       ThuTienVe AS [Thu tiền về], TienNamTrongKho AS [Tiền nằm trong kho], " +
                    "       CAST(LaiMoiCaiNgayThuong AS DECIMAL(18, 0)) AS [Lãi mỗi cái ngày thường], CAST(LaiMoiCaiNgaySale AS DECIMAL(18, 0)) AS [Lãi mỗi cái ngày sale], " +
                    "       NenLamGi AS [Nên làm gì] " +
                    "FROM vw_CungCau " +
                    "ORDER BY CASE WHEN CungCau LIKE N'Cung < Cầu%' THEN 0 WHEN CungCau LIKE N'Cung > Cầu%' THEN 1 ELSE 2 END, TienNamTrongKho DESC");
                string maDangChon = dgvSanPham.CurrentRow == null ? null : Convert.ToString(dgvSanPham.CurrentRow.Cells["Mã SP"].Value);
                dgvSanPham.DataSource = dt;
                if (dgvSanPham.Columns.Contains("Nên làm gì")) dgvSanPham.Columns["Nên làm gì"].MinimumWidth = 380;

                decimal tien = dt.Rows.Cast<DataRow>().Sum(r => r["Tiền nằm trong kho"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Tiền nằm trong kho"]));
                Func<string, int> dem = dau => dt.Rows.Cast<DataRow>().Count(r => Convert.ToString(r["Cung - Cầu"]).StartsWith(dau));
                int dauSong = dt.Rows.Cast<DataRow>().Count(r => Convert.ToString(r["Đang ở sóng nào"]).StartsWith("Đầu sóng"));
                int cuoiSong = dt.Rows.Cast<DataRow>().Count(r => Convert.ToString(r["Đang ở sóng nào"]).StartsWith("Cuối sóng"));
                lblTongQuan.Text = "Tiền đang nằm trong kho: " + tien.ToString("N0") + " đ   |   Cung < Cầu: " + dem("Cung < Cầu") + " món   |   Cung = Cầu: " +
                                   dem("Cung = Cầu") + " món   |   Cung > Cầu: " + dem("Cung > Cầu") + " món   |   Đầu sóng: " + dauSong + "   |   Cuối sóng: " + cuoiSong;

                if (maDangChon != null)
                    foreach (DataGridViewRow r in dgvSanPham.Rows)
                        if (Convert.ToString(r.Cells["Mã SP"].Value) == maDangChon) { dgvSanPham.CurrentCell = r.Cells["Mã SP"]; break; }
                NapTheoThang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NapTheoThang()
        {
            if (dgvSanPham.CurrentRow == null || !dgvSanPham.Columns.Contains("Mã SP")) return;
            string ma = Convert.ToString(dgvSanPham.CurrentRow.Cells["Mã SP"].Value);
            _tenSP = Convert.ToString(dgvSanPham.CurrentRow.Cells["Sản phẩm"].Value);
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT RIGHT(CONVERT(CHAR(7), DauThang, 120), 2) + '/' + LEFT(CONVERT(CHAR(7), DauThang, 120), 4) AS [Tháng], " +
                    "       Cau AS [Cầu: khách đặt (cái)], TonDauThang AS [Tồn đầu tháng], NhapTrongThang AS [Nhập trong tháng], " +
                    "       Cung AS [Cung: hàng có để bán (cái)], DuBanMayThang AS [Đủ bán mấy tháng], " +
                    "       CAST(GiaThucBanTB AS DECIMAL(18, 0)) AS [Giá thực bán TB], CungCau AS [Hàng mỏng hay dư] " +
                    "FROM dbo.fn_CungCauTheoThang(@ma) ORDER BY DauThang",
                    AiHelper.P("@ma", ma));
                dgvThang.DataSource = dt;

                _thang.Clear(); _cau.Clear(); _cung.Clear(); _gia.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    string th = Convert.ToString(r["Tháng"]);
                    _thang.Add("T" + th.Substring(0, 2) + "/" + th.Substring(5, 2));
                    _cau.Add(Convert.ToDouble(r["Cầu: khách đặt (cái)"]));
                    _cung.Add(Convert.ToDouble(r["Cung: hàng có để bán (cái)"]));
                    _gia.Add(r["Giá thực bán TB"] == DBNull.Value ? 0 : Math.Round(Convert.ToDouble(r["Giá thực bán TB"]) / 1000.0, 0));
                }

                int mong = dt.Rows.Cast<DataRow>().Count(r => Convert.ToString(r["Hàng mỏng hay dư"]).StartsWith("Hàng mỏng"));
                int du = dt.Rows.Cast<DataRow>().Count(r => Convert.ToString(r["Hàng mỏng hay dư"]).StartsWith("Hàng dư"));
                lblThang.Text = "Từng tháng của " + _tenSP + ": " + dt.Rows.Count + " tháng, trong đó " + mong + " tháng hàng mỏng, " + du +
                                " tháng hàng dư. Đường xanh cao hơn đường đỏ là khách đặt nhiều hơn hàng có sẵn.";
            }
            catch (Exception ex) { lblThang.Text = "Chưa đọc được số liệu theo tháng: " + AiHelper.LoiDeHieu(ex); }
            pnlCungCau.Invalidate();
            pnlGia.Invalidate();
        }
    }
}
