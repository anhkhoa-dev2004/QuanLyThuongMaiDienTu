using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_TroLyAI : Form
    {
        private class CauHoiNhanh
        {
            public string Ten;
            public string[] TuKhoa;
            public string Sql;
            public Func<DataTable, string> TomTat;
        }

        private static readonly HttpClient _http = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
        private static readonly Color MauTot = Color.FromArgb(30, 132, 73);
        private static readonly Color MauLoi = Color.FromArgb(192, 57, 43);
        private static readonly Color MauCho = Color.FromArgb(52, 73, 94);

        private string _moTaCSDL;
        private bool _dangHoi;
        private FlowLayoutPanel _pnlNhanh;
        private CheckBox _chkXemLenh;
        private readonly List<CauHoiNhanh> _dsNhanh;

        public Form_TroLyAI()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            _dsNhanh = TaoCauHoiNhanh();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            lblGhiChu.Text = "Bấm 1 câu hỏi có sẵn là có câu trả lời ngay, không cần cài gì thêm. Muốn hỏi câu khác thì gõ vào ô rồi nhấn Enter. " +
                             "Máy chỉ đọc dữ liệu, không sửa, không xóa.";
            lblCauHoi.Text = "Gõ câu hỏi rồi nhấn Enter (Shift + Enter để xuống dòng):";
            btnHoiAI.Text = "Hỏi";
            lblLichSu.Text = "Câu đã hỏi gần đây (bấm 1 dòng để hỏi lại):";
            lblSQL.Visible = false;
            txtSQL.Visible = false;
            btnChaySQL.Visible = false;
            btnChaySQL.Text = "Chạy lại câu lệnh này";

            _pnlNhanh = new FlowLayoutPanel
            {
                Location = new Point(22, 178),
                Size = new Size(1053, 112),
                AutoScroll = true,
                WrapContents = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            foreach (CauHoiNhanh c in _dsNhanh)
            {
                CauHoiNhanh cau = c;
                Button nut = new Button
                {
                    Text = cau.Ten,
                    AutoSize = true,
                    Height = 32,
                    Margin = new Padding(0, 0, 8, 8),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = GiaoDien.Xanh,
                    Cursor = Cursors.Hand
                };
                nut.FlatAppearance.BorderColor = GiaoDien.Cam;
                nut.Click += (s, e) => HoiCauNhanh(cau);
                _pnlNhanh.Controls.Add(nut);
            }
            Controls.Add(_pnlNhanh);

            _chkXemLenh = new CheckBox
            {
                Text = "Xem câu lệnh máy đã dùng",
                AutoSize = true,
                Location = new Point(860, 322),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _chkXemLenh.CheckedChanged += (s, e) =>
            {
                bool xem = _chkXemLenh.Checked;
                lblSQL.Visible = xem;
                txtSQL.Visible = xem;
                btnChaySQL.Visible = xem;
                _pnlNhanh.Visible = !xem;
            };
            Controls.Add(_chkXemLenh);
            _chkXemLenh.BringToFront();

            txtCauHoi.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter || e.Shift) return;
                e.SuppressKeyPress = true;
                btnHoiAI.PerformClick();
            };
        }

        private static string TenKhach(DataTable t, int i)
        {
            return Convert.ToString(t.Rows[i][1]);
        }

        private static List<CauHoiNhanh> TaoCauHoiNhanh()
        {
            return new List<CauHoiNhanh>
            {
                new CauHoiNhanh
                {
                    Ten = "Cần đặt hàng món nào?",
                    TuKhoa = new[] { "đặt hàng", "nhập hàng", "hết hàng", "sắp hết", "nhập thêm" },
                    Sql = "SELECT TenSP AS [Sản phẩm], TonHienTai AS [Tồn], SoNgayConLai AS [Đủ bán (ngày)], SoNenNhap AS [Nên nhập thêm], ViecNenLam AS [Nên làm gì] " +
                          "FROM vw_DuBaoSanPham WHERE TrangThai IN (N'KHẨN CẤP', N'ĐẶT HÀNG NGAY') ORDER BY SoNgayConLai",
                    TomTat = t => t.Rows.Count == 0
                        ? "Chưa có món nào phải đặt hàng. Kho đang đủ bán."
                        : "Có " + t.Rows.Count + " món cần đặt: " + string.Join(", ", t.Rows.Cast<DataRow>().Select(r => r["Sản phẩm"] + " (" + r["Nên nhập thêm"] + " cái)")) + "."
                },
                new CauHoiNhanh
                {
                    Ten = "Món nào đang hot?",
                    TuKhoa = new[] { "hot", "trend", "bán nhanh", "xu hướng", "đang lên" },
                    Sql = "SELECT TenSP AS [Sản phẩm], XuHuongPT AS [Bán nhanh hơn TB 90 ngày (%)], DuDoan30Ngay AS [Dự đoán bán 30 ngày tới], " +
                          "MonHayMuaChung AS [Hay mua chung với], ViecNenLam AS [Nên làm gì] " +
                          "FROM vw_DuBaoSanPham WHERE Cau = N'ĐANG HOT - bán nhanh lên' ORDER BY XuHuongPT DESC",
                    TomTat = t => t.Rows.Count == 0
                        ? "Chưa có món nào đang hot (30 ngày gần nhất bán nhanh hơn trung bình 90 ngày từ 20%)."
                        : "Có " + t.Rows.Count + " món đang hot: " + string.Join(", ", t.Rows.Cast<DataRow>().Select(r => Convert.ToString(r["Sản phẩm"]))) + ". Nên đẩy quảng cáo và giữ đủ hàng."
                },
                new CauHoiNhanh
                {
                    Ten = "Món nào khách đang chê?",
                    TuKhoa = new[] { "chê", "phàn nàn", "không hài lòng", "buồn", "đánh giá xấu", "hài lòng" },
                    Sql = "SELECT sp.TenSP AS [Sản phẩm], COUNT(*) AS [Số đánh giá], " +
                          "CAST(100.0 * SUM(CASE WHEN d.SoSao >= 4 THEN 1 ELSE 0 END) / COUNT(*) AS DECIMAL(5, 1)) AS [% khách hài lòng], " +
                          "SUM(CASE WHEN pc.NhomCamXuc = N'Chê' THEN 1 ELSE 0 END) AS [Số lời chê] " +
                          "FROM DanhGiaSanPham d JOIN SanPham sp ON sp.MaSP = d.MaSP " +
                          "LEFT JOIN PhanTich_CamXuc_DanhGia pc ON pc.LoaiDoiTuong = N'Đánh giá' AND pc.DoiTuongId = d.MaDanhGia " +
                          "GROUP BY sp.TenSP ORDER BY [% khách hài lòng]",
                    TomTat = t =>
                    {
                        if (t.Rows.Count == 0) return "Chưa có đánh giá nào.";
                        List<string> che = t.Rows.Cast<DataRow>().Where(r => Convert.ToDecimal(r["% khách hài lòng"]) < 60m)
                                            .Select(r => r["Sản phẩm"] + " (" + r["% khách hài lòng"] + "% hài lòng)").ToList();
                        return che.Count == 0
                            ? "Không có món nào dưới 60% khách hài lòng. Thấp nhất là " + t.Rows[0]["Sản phẩm"] + " (" + t.Rows[0]["% khách hài lòng"] + "%)."
                            : "Khách đang chê: " + string.Join(", ", che) + ". Nên đọc lời chê và sửa trước khi chạy quảng cáo.";
                    }
                },
                new CauHoiNhanh
                {
                    Ten = "Doanh thu 6 tháng gần nhất",
                    TuKhoa = new[] { "doanh thu", "bán được bao nhiêu", "thu được", "tháng này" },
                    Sql = "SELECT TOP 6 RIGHT(CONVERT(CHAR(7), Ngay, 120), 2) + '/' + LEFT(CONVERT(CHAR(7), Ngay, 120), 4) AS [Tháng], " +
                          "SUM(SoDon) AS [Số đơn], SUM(TongDoanhThu) AS [Doanh thu] " +
                          "FROM dbo.fn_ThongKeDoanhThu('19000101', '20991231') " +
                          "GROUP BY CONVERT(CHAR(7), Ngay, 120) ORDER BY CONVERT(CHAR(7), Ngay, 120) DESC",
                    TomTat = t =>
                    {
                        if (t.Rows.Count == 0) return "Chưa có đơn nào.";
                        string kq = "Tháng " + t.Rows[0]["Tháng"] + " bán được " + (Convert.ToDecimal(t.Rows[0]["Doanh thu"]) / 1000000m).ToString("N1") +
                                    " triệu từ " + t.Rows[0]["Số đơn"] + " đơn (không tính đơn hủy).";
                        if (t.Rows.Count > 1)
                            kq += " Tháng " + t.Rows[1]["Tháng"] + ": " + (Convert.ToDecimal(t.Rows[1]["Doanh thu"]) / 1000000m).ToString("N1") + " triệu.";
                        return kq;
                    }
                },
                new CauHoiNhanh
                {
                    Ten = "Top 5 món bán chạy 30 ngày qua",
                    TuKhoa = new[] { "bán chạy", "top", "bán nhiều nhất" },
                    Sql = "SELECT TOP 5 sp.TenSP AS [Sản phẩm], SUM(ct.SoLuong) AS [Số cái bán], SUM(ct.SoLuong * ct.DonGia) AS [Doanh thu] " +
                          "FROM ChiTietDonHang ct JOIN DonHang dh ON dh.MaDH = ct.MaDH JOIN SanPham sp ON sp.MaSP = ct.MaSP " +
                          "WHERE ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' AND dh.NgayDat > DATEADD(DAY, -30, (SELECT MAX(NgayDat) FROM DonHang)) " +
                          "GROUP BY sp.TenSP ORDER BY SUM(ct.SoLuong) DESC",
                    TomTat = t => t.Rows.Count == 0
                        ? "30 ngày qua chưa bán được món nào."
                        : "Bán chạy nhất 30 ngày tính đến đơn mới nhất: " + t.Rows[0]["Sản phẩm"] + " (" + t.Rows[0]["Số cái bán"] + " cái)."
                },
                new CauHoiNhanh
                {
                    Ten = "Khách mua vì lý do gì?",
                    TuKhoa = new[] { "lý do", "vì sao", "tại sao", "cảm xúc", "động lực" },
                    Sql = "SELECT p.NhomLyDo AS [Lý do mua], ISNULL(c.KieuLyDo, N'') AS [Kiểu], COUNT(*) AS [Số câu trả lời], " +
                          "CAST(100.0 * COUNT(*) / SUM(COUNT(*)) OVER () AS DECIMAL(5, 1)) AS [Tỷ lệ (%)], ISNULL(c.CachBanHop, N'') AS [Cách bán hợp] " +
                          "FROM PhanTich_LyDoMua p LEFT JOIN CachBan_TheoLyDoMua c ON c.NhomLyDo = p.NhomLyDo " +
                          "GROUP BY p.NhomLyDo, c.KieuLyDo, c.CachBanHop ORDER BY COUNT(*) DESC",
                    TomTat = t =>
                    {
                        if (t.Rows.Count == 0) return "Chưa có câu trả lời khảo sát nào.";
                        decimal camXuc = t.Rows.Cast<DataRow>().Where(r => Convert.ToString(r["Kiểu"]) == "Cảm xúc").Sum(r => Convert.ToDecimal(r["Tỷ lệ (%)"]));
                        return "Nhiều nhất: " + t.Rows[0]["Lý do mua"] + " (" + t.Rows[0]["Tỷ lệ (%)"] + "%). Tổng cộng " + camXuc.ToString("0.#") +
                               "% câu trả lời là mua vì cảm xúc.";
                    }
                },
                new CauHoiNhanh
                {
                    Ten = "Khách ruột cần giữ chân",
                    TuKhoa = new[] { "khách ruột", "khách vip", "khách thân", "khách quen", "giữ chân" },
                    Sql = "SELECT TOP 20 kh.MaKH AS [Mã khách], kh.TenKH AS [Tên khách], kh.SoDT AS [Điện thoại], COUNT(dh.MaDH) AS [Số đơn], " +
                          "SUM(dh.TongTien) AS [Tổng tiền đã mua], MAX(dh.NgayDat) AS [Lần mua cuối] " +
                          "FROM KhachHang kh JOIN DonHang dh ON dh.MaKH = kh.MaKH AND ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' " +
                          "WHERE kh.Nhom_RFM LIKE N'Hạng 5%' GROUP BY kh.MaKH, kh.TenKH, kh.SoDT ORDER BY SUM(dh.TongTien) DESC",
                    TomTat = t => t.Rows.Count == 0
                        ? "Chưa chia nhóm khách. Vào \"Chia nhóm khách hàng\" bấm \"Chia nhóm khách\" rồi hỏi lại."
                        : "Đây là " + t.Rows.Count + " khách ruột chi nhiều nhất, đứng đầu là " + TenKhach(t, 0) + ". Nên gửi quà tri ân, báo hàng mới cho họ trước."
                },
                new CauHoiNhanh
                {
                    Ten = "Khách sắp mất cần gọi lại",
                    TuKhoa = new[] { "sắp mất", "bỏ đi", "lâu không", "gọi lại", "quay lại" },
                    Sql = "SELECT TOP 20 kh.MaKH AS [Mã khách], kh.TenKH AS [Tên khách], kh.SoDT AS [Điện thoại], COUNT(dh.MaDH) AS [Số đơn], " +
                          "MAX(dh.NgayDat) AS [Lần mua cuối] " +
                          "FROM KhachHang kh JOIN DonHang dh ON dh.MaKH = kh.MaKH AND ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' " +
                          "WHERE kh.Nhom_RFM LIKE N'Hạng 1%' GROUP BY kh.MaKH, kh.TenKH, kh.SoDT ORDER BY MAX(dh.NgayDat)",
                    TomTat = t => t.Rows.Count == 0
                        ? "Chưa chia nhóm khách. Vào \"Chia nhóm khách hàng\" bấm \"Chia nhóm khách\" rồi hỏi lại."
                        : "Có " + t.Rows.Count + " khách lâu không quay lại (xếp người lâu nhất lên đầu). Nên gọi hỏi thăm hoặc gửi voucher có hạn ngắn."
                },
                new CauHoiNhanh
                {
                    Ten = "Món nào nên làm combo?",
                    TuKhoa = new[] { "combo", "mua chung", "mua kèm", "bán kèm" },
                    Sql = "SELECT TenSP AS [Món], MonHayMuaChung AS [Hay mua chung với], SoDonMuaChung AS [Số đơn có cả 2 món] " +
                          "FROM vw_DuBaoSanPham WHERE MonHayMuaChung IS NOT NULL ORDER BY SoDonMuaChung DESC",
                    TomTat = t => t.Rows.Count == 0
                        ? "Chưa có đơn nào mua từ 2 món trở lên."
                        : "Cặp hay mua chung nhất: " + t.Rows[0]["Món"] + " + " + t.Rows[0]["Hay mua chung với"] + " (" + t.Rows[0]["Số đơn có cả 2 món"] + " đơn). Nên làm combo cặp này."
                },
                new CauHoiNhanh
                {
                    Ten = "Đơn đáng ngờ chưa xử lý",
                    TuKhoa = new[] { "đáng ngờ", "gian lận", "lừa", "bom hàng", "rủi ro" },
                    Sql = "SELECT TOP 50 b.DonHangId AS [Mã đơn], kh.TenKH AS [Khách], b.DiemRuiRo AS [Điểm rủi ro], b.LyDoCanhBao AS [Lý do] " +
                          "FROM BatThuong_DonHang_GianLan b LEFT JOIN KhachHang kh ON kh.MaKH = b.KhachHangId " +
                          "WHERE b.TrangThaiXuly = N'Chưa xử lý' ORDER BY b.DiemRuiRo DESC",
                    TomTat = t => t.Rows.Count == 0
                        ? "Không có đơn đáng ngờ nào đang chờ xử lý."
                        : "Có " + t.Rows.Count + " đơn đáng ngờ chưa xử lý. Mở \"Đơn đáng ngờ, nghi gian lận\" để kiểm tra trước khi giao."
                }
            };
        }

        private void Form_TroLyAI_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvKetQua);
            AiHelper.DinhDangLuoi(dgvLichSu);
            try { _moTaCSDL = TaoMoTaCSDL(); }
            catch (Exception ex) { BaoTrangThai("Không đọc được danh sách bảng: " + AiHelper.LoiDeHieu(ex), MauLoi); }
            TaiLichSu();
            if (LayApiKey() == null)
                BaoTrangThai("Đang chạy không cần khóa AI: bấm câu hỏi có sẵn, hoặc gõ câu có các chữ như \"doanh thu\", \"đang hot\", \"đặt hàng\", \"khách chê\"... " +
                             "Muốn hỏi tự do mọi câu thì điền GeminiApiKey trong App.config.", MauCho);
            else
                BaoTrangThai("Sẵn sàng. Bấm câu hỏi có sẵn hoặc gõ câu bất kỳ rồi nhấn Enter.", MauTot);
            txtCauHoi.Focus();
        }

        private void BaoTrangThai(string noiDung, Color mau)
        {
            lblTrangThai.Text = noiDung;
            lblTrangThai.ForeColor = mau;
            lblTrangThai.Refresh();
        }

        private static string LayApiKey()
        {
            string k = ConfigurationManager.AppSettings["GeminiApiKey"];
            if (string.IsNullOrWhiteSpace(k)) k = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
            return string.IsNullOrWhiteSpace(k) ? null : k.Trim();
        }

        private static string TaoMoTaCSDL()
        {
            DataTable dt = AiHelper.Doc(
                "SELECT c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE " +
                "FROM INFORMATION_SCHEMA.COLUMNS c " +
                "WHERE c.TABLE_SCHEMA = 'dbo' AND c.TABLE_NAME <> 'NguoiDung' " +
                "ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION");
            StringBuilder sb = new StringBuilder();
            string bang = null;
            foreach (DataRow r in dt.Rows)
            {
                string b = r["TABLE_NAME"].ToString();
                if (b != bang)
                {
                    if (bang != null) sb.AppendLine(")");
                    sb.Append(b).Append("(");
                    bang = b;
                }
                else sb.Append(", ");
                sb.Append(r["COLUMN_NAME"]).Append(" ").Append(r["DATA_TYPE"]);
            }
            if (bang != null) sb.AppendLine(")");
            return sb.ToString();
        }

        private void TaiLichSu()
        {
            try
            {
                dgvLichSu.DataSource = AiHelper.Doc("SELECT TOP 30 Id, ThoiGian, CauHoiText, CauLenhSQL FROM LichSuTruyVan_AI ORDER BY Id DESC");
                if (dgvLichSu.Columns.Contains("Id")) dgvLichSu.Columns["Id"].Visible = false;
                if (dgvLichSu.Columns.Contains("CauLenhSQL")) dgvLichSu.Columns["CauLenhSQL"].Visible = false;
            }
            catch (Exception ex) { BaoTrangThai("Chưa đọc được lịch sử: " + AiHelper.LoiDeHieu(ex), MauLoi); }
        }

        private void dgvLichSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _dangHoi) return;
            DataGridViewRow r = dgvLichSu.Rows[e.RowIndex];
            string cauHoi = Convert.ToString(r.Cells["CauHoiText"].Value);
            string sql = Convert.ToString(r.Cells["CauLenhSQL"].Value);
            txtCauHoi.Text = cauHoi;
            CauHoiNhanh nhanh = _dsNhanh.FirstOrDefault(c => c.Ten == cauHoi);
            if (nhanh != null) { HoiCauNhanh(nhanh); return; }
            if (sql.Length == 0) return;
            DataTable dt = ChayVaHien(sql);
            if (dt != null) BaoTrangThai(TomTatChung(dt), MauTot);
        }

        private void HoiCauNhanh(CauHoiNhanh cau, string cauGoc = null)
        {
            if (cauGoc == null) txtCauHoi.Text = cau.Ten;
            BaoTrangThai("Đang tìm câu trả lời...", MauCho);
            DataTable dt = ChayVaHien(cau.Sql);
            if (dt == null) return;
            string tomTat;
            try { tomTat = cau.TomTat(dt); }
            catch { tomTat = TomTatChung(dt); }
            BaoTrangThai((cauGoc == null ? "" : "Máy hiểu câu hỏi là \"" + cau.Ten + "\". ") + tomTat, MauTot);
            LuuLichSu(cau.Ten, cau.Sql);
        }

        private CauHoiNhanh TimCauGiong(string cauHoi)
        {
            string s = cauHoi.ToLowerInvariant();
            return _dsNhanh.FirstOrDefault(c => c.TuKhoa.Any(k => s.Contains(k)));
        }

        private async void btnHoiAI_Click(object sender, EventArgs e)
        {
            if (_dangHoi) return;
            string cauHoi = txtCauHoi.Text.Trim();
            if (cauHoi.Length == 0) { BaoTrangThai("Hãy gõ câu hỏi hoặc bấm một câu có sẵn.", MauCho); txtCauHoi.Focus(); return; }

            string key = LayApiKey();
            if (key == null || string.IsNullOrEmpty(_moTaCSDL))
            {
                CauHoiNhanh giong = TimCauGiong(cauHoi);
                if (giong != null) { HoiCauNhanh(giong, cauHoi); return; }
                BaoTrangThai("Câu này cần AI mới trả lời được. Hãy bấm một câu có sẵn ở trên, " +
                             "hoặc điền GeminiApiKey trong App.config (lấy khóa miễn phí ở Google AI Studio) để hỏi tự do.", MauLoi);
                return;
            }

            _dangHoi = true;
            btnHoiAI.Enabled = false;
            _pnlNhanh.Enabled = false;
            BaoTrangThai("AI đang đọc câu hỏi... (chỉ gửi câu hỏi và tên bảng, không gửi dữ liệu khách)", MauCho);
            try
            {
                string sql = LamSachCauSQL(await GoiGemini(TaoLoiNhan(cauHoi, null, null), key));
                DataTable dt = ChayAnToan(sql, out string loi);
                if (dt == null)
                {
                    BaoTrangThai("Câu đầu chưa chạy được, AI đang tự sửa lại...", MauCho);
                    sql = LamSachCauSQL(await GoiGemini(TaoLoiNhan(cauHoi, sql, loi), key));
                    dt = ChayAnToan(sql, out loi);
                }
                txtSQL.Text = sql;
                if (dt == null)
                {
                    CauHoiNhanh giong = TimCauGiong(cauHoi);
                    if (giong != null) { HoiCauNhanh(giong, cauHoi); return; }
                    BaoTrangThai("AI chưa trả lời được câu này (" + loi + "). Hãy hỏi cách khác, ngắn gọn hơn, hoặc bấm một câu có sẵn.", MauLoi);
                    return;
                }
                HienKetQua(dt);
                BaoTrangThai(TomTatChung(dt), MauTot);
                LuuLichSu(cauHoi, sql);
            }
            catch (Exception ex)
            {
                CauHoiNhanh giong = TimCauGiong(cauHoi);
                if (giong != null) { HoiCauNhanh(giong, cauHoi); return; }
                BaoTrangThai("Chưa hỏi được AI: " + AiHelper.LoiDeHieu(ex), MauLoi);
            }
            finally
            {
                _dangHoi = false;
                btnHoiAI.Enabled = true;
                _pnlNhanh.Enabled = true;
            }
        }

        private string TaoLoiNhan(string cauHoi, string cauCu, string loiCu)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Bạn viết câu lệnh T-SQL cho SQL Server của một cửa hàng bán đồ công nghệ trên Shopee.");
            sb.AppendLine("Viết ĐÚNG MỘT câu SELECT trả lời câu hỏi. Chỉ trả về câu lệnh, không giải thích, không markdown, không dấu chấm phẩy.");
            sb.AppendLine("Quy tắc:");
            sb.AppendLine("- Chỉ dùng bảng và cột có trong danh sách bên dưới. Không dùng bảng NguoiDung.");
            sb.AppendLine("- Chuỗi tiếng Việt viết dạng N'...'. Đặt tên cột kết quả bằng tiếng Việt có dấu trong ngoặc vuông, ví dụ [Doanh thu].");
            sb.AppendLine("- Lấy tối đa TOP 200 dòng.");
            sb.AppendLine("- Doanh thu = SUM(DonHang.TongTien) của các đơn có TrangThai không chứa chữ 'hủy' (ISNULL(TrangThai, N'') NOT LIKE N'%hủy%').");
            sb.AppendLine("- Khi hỏi 'hôm nay', 'tháng này', '30 ngày qua' thì lấy mốc là (SELECT MAX(NgayDat) FROM DonHang) thay cho GETDATE().");
            sb.AppendLine("- DonHang.TrangThai chỉ có: N'Chờ xác nhận', N'Chờ lấy hàng', N'Đang giao', N'Đã giao', N'Trả hàng/Hoàn tiền', N'Đã hủy'.");
            sb.AppendLine("- DonHang.HinhThucNhan là N'Giao tận nơi' hoặc N'Nhận tại cửa hàng'. Người nhận, SĐT, địa chỉ của từng đơn nằm ở DonHang.TenNguoiNhan, SdtNguoiNhan, DiaChiGiao. " +
                          "Đơn giao không thành công có LyDoHuy bắt đầu bằng N'Giao không thành công'; hàng của đơn đó nằm ở HangHoanVeKho (TrangThai N'Đang hoàn về' hoặc N'Đã nhận về kho').");
            sb.AppendLine("- Tiền shop thực nhận, phí sàn, giá vốn, lãi gộp từng đơn nằm ở view vw_TienDonHang (cột TienHang, GiamGiaCuaShop, PhiSan, " +
                          "TienHoanKhach, TienShopNhan, GiaVonHangBan, LaiGop, KenhBan, DaHuy, DaGiao). Hỏi về lãi hoặc tiền thực nhận thì dùng view này.");
            sb.AppendLine("- Bảng vw_DuBaoSanPham có sẵn cho từng sản phẩm: TonHienTai, Cau (xu hướng bán, 'ĐANG HOT - bán nhanh lên' là món hot), " +
                          "DuDoan30Ngay, SoNgayConLai, SoNenNhap, TrangThai ('KHẨN CẤP', 'ĐẶT HÀNG NGAY', 'TỒN NHIỀU', 'ỔN', 'KHÔNG BÁN'), MonHayMuaChung, ViecNenLam.");
            sb.AppendLine("- Lãi ròng từng tháng (đã trừ chi phí vận hành, hàng mất hỏng, bảo hành) nằm ở view vw_LaiRongTheoThang (cột Thang, TienHang, LaiGop, ChiPhiVanHanh, HangMatHong, ChiPhiBaoHanh, LaiRong). " +
                          "Nợ nhà cung cấp ở vw_CongNoNhaCungCap (ConNo, HanTra, TinhTrang). Tiền sàn chuyển về so với tiền phần mềm tính ở vw_DoiSoatTienSan (TinhTrang, ChenhLech).");
            sb.AppendLine("- KhachHang.Nhom_RFM có dạng 'Hạng 5 - Khách ruột' ... 'Hạng 1 - Khách sắp mất'. PhanTich_LyDoMua.NhomLyDo là lý do khách mua.");
            sb.AppendLine("- Cảm xúc từng đánh giá nằm ở PhanTich_CamXuc_DanhGia (LoaiDoiTuong = N'Đánh giá', DoiTuongId = DanhGiaSanPham.MaDanhGia, NhomCamXuc là N'Khen', N'Trung tính' hoặc N'Chê').");
            if (cauCu != null)
            {
                sb.AppendLine();
                sb.AppendLine("Câu lệnh bạn viết lần trước bị lỗi, hãy viết lại cho đúng.");
                sb.AppendLine("Câu lệnh cũ: " + cauCu);
                sb.AppendLine("Lỗi: " + loiCu);
            }
            sb.AppendLine();
            sb.AppendLine("Danh sách bảng và cột:");
            sb.AppendLine(_moTaCSDL);
            sb.AppendLine("Câu hỏi: " + cauHoi);
            return sb.ToString();
        }

        private static async Task<string> GoiGemini(string prompt, string key)
        {
            string model = ConfigurationManager.AppSettings["GeminiModel"];
            if (string.IsNullOrWhiteSpace(model)) model = "gemini-2.5-flash";
            string url = "https://generativelanguage.googleapis.com/v1beta/models/" + model.Trim() + ":generateContent";

            Dictionary<string, object> phan = new Dictionary<string, object> { { "text", prompt } };
            Dictionary<string, object> noiDung = new Dictionary<string, object> { { "parts", new object[] { phan } } };
            Dictionary<string, object> body = new Dictionary<string, object> { { "contents", new object[] { noiDung } } };
            JavaScriptSerializer js = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url))
            {
                req.Headers.Add("x-goog-api-key", key);
                req.Content = new StringContent(js.Serialize(body), Encoding.UTF8, "application/json");
                using (HttpResponseMessage res = await _http.SendAsync(req))
                {
                    string txt = await res.Content.ReadAsStringAsync();
                    if (!res.IsSuccessStatusCode)
                        throw new Exception("Gemini trả lỗi " + (int)res.StatusCode + ": " + (txt.Length > 300 ? txt.Substring(0, 300) + "..." : txt));

                    IDictionary<string, object> goc = js.DeserializeObject(txt) as IDictionary<string, object>;
                    System.Collections.IList ungVien = goc != null && goc.ContainsKey("candidates") ? goc["candidates"] as System.Collections.IList : null;
                    if (ungVien == null || ungVien.Count == 0) throw new Exception("Gemini không trả về nội dung (có thể bị chặn bởi bộ lọc an toàn).");
                    IDictionary<string, object> uv = ungVien[0] as IDictionary<string, object>;
                    IDictionary<string, object> nd = uv != null && uv.ContainsKey("content") ? uv["content"] as IDictionary<string, object> : null;
                    System.Collections.IList parts = nd != null && nd.ContainsKey("parts") ? nd["parts"] as System.Collections.IList : null;
                    IDictionary<string, object> p0 = parts != null && parts.Count > 0 ? parts[0] as IDictionary<string, object> : null;
                    if (p0 == null || !p0.ContainsKey("text")) throw new Exception("Không đọc được câu trả lời của Gemini.");
                    return Convert.ToString(p0["text"]);
                }
            }
        }

        private static string LamSachCauSQL(string s)
        {
            s = (s ?? "").Trim();
            s = Regex.Replace(s, @"^```[a-zA-Z]*\s*", "");
            s = Regex.Replace(s, @"\s*```$", "");
            s = s.Trim();
            while (s.EndsWith(";")) s = s.Substring(0, s.Length - 1).TrimEnd();
            return s;
        }

        private static string KiemTraChiDocSELECT(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return "Chưa có câu lệnh.";
            if (sql.Contains("--") || sql.Contains("/*")) return "Không cho phép chú thích trong câu lệnh.";

            string khongChuoi = Regex.Replace(sql, @"N?'(?:[^']|'')*'", "''", RegexOptions.IgnoreCase);
            if (khongChuoi.Contains(";")) return "Chỉ được chạy 1 câu lệnh (không dùng dấu chấm phẩy ở giữa).";
            if (!Regex.IsMatch(khongChuoi, @"^\s*(SELECT|WITH)\b", RegexOptions.IgnoreCase)) return "Chỉ cho phép câu bắt đầu bằng SELECT hoặc WITH.";
            if (Regex.IsMatch(khongChuoi,
                @"\b(INSERT|UPDATE|DELETE|DROP|ALTER|CREATE|TRUNCATE|EXEC|EXECUTE|MERGE|GRANT|REVOKE|DENY|INTO|OPENROWSET|OPENQUERY|OPENDATASOURCE|BACKUP|RESTORE|SHUTDOWN|WAITFOR|DBCC|KILL|RECONFIGURE|BULK)\b|\bxp_|\bsp_",
                RegexOptions.IgnoreCase))
                return "Câu lệnh có phần sửa/xóa dữ liệu, không được phép (chỉ được đọc dữ liệu).";
            if (Regex.IsMatch(khongChuoi, @"\bNguoiDung\b", RegexOptions.IgnoreCase)) return "Không được truy vấn bảng NguoiDung (chứa mật khẩu).";
            return null;
        }

        private static DataTable ChayAnToan(string sql, out string loi)
        {
            loi = KiemTraChiDocSELECT(sql);
            if (loi != null) return null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable dt = new DataTable();
                using (SqlConnection cn = DbHelper.TaoKetNoi())
                {
                    cn.Open();
                    using (SqlTransaction tran = cn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, cn, tran))
                            {
                                cmd.CommandTimeout = 30;
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) da.Fill(0, 1000, dt);
                            }
                        }
                        finally { tran.Rollback(); }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                loi = ex is SqlException ? ex.Message : AiHelper.LoiDeHieu(ex);
                return null;
            }
        }

        private DataTable ChayVaHien(string sql)
        {
            string loi;
            DataTable dt = ChayAnToan(sql, out loi);
            txtSQL.Text = sql;
            if (dt == null)
            {
                BaoTrangThai("Chưa lấy được câu trả lời: " + loi, MauLoi);
                return null;
            }
            HienKetQua(dt);
            return dt;
        }

        private void HienKetQua(DataTable dt)
        {
            dgvKetQua.DataSource = dt;
            lblKetQua.Text = "Kết quả: " + dt.Rows.Count + " dòng" + (dt.Rows.Count >= 1000 ? " (chỉ hiện 1000 dòng đầu)" : "");
        }

        private static string TomTatChung(DataTable dt)
        {
            if (dt.Rows.Count == 0) return "Không có dữ liệu nào khớp câu hỏi.";
            if (dt.Rows.Count == 1 && dt.Columns.Count <= 4)
            {
                List<string> phan = new List<string>();
                foreach (DataColumn c in dt.Columns)
                {
                    object v = dt.Rows[0][c];
                    string chu;
                    if (v == null || v == DBNull.Value) chu = "(trống)";
                    else if (v is decimal || v is double || v is float) chu = Convert.ToDecimal(v).ToString("#,##0.##");
                    else if (v is DateTime) chu = ((DateTime)v).ToString("dd/MM/yyyy");
                    else chu = Convert.ToString(v);
                    phan.Add(c.ColumnName + ": " + chu);
                }
                return "Trả lời: " + string.Join("  |  ", phan);
            }
            return "Có " + dt.Rows.Count + " dòng kết quả ở bảng bên dưới.";
        }

        private void LuuLichSu(string cauHoi, string sql)
        {
            try
            {
                AiHelper.Chay("INSERT INTO LichSuTruyVan_AI (CauHoiText, CauLenhSQL, NguoiDungId) VALUES (@h, @s, @u)",
                    AiHelper.P("@h", cauHoi), AiHelper.P("@s", sql), AiHelper.P("@u", Session.MaND));
                TaiLichSu();
            }
            catch { }
        }

        private void btnChaySQL_Click(object sender, EventArgs e)
        {
            string sql = LamSachCauSQL(txtSQL.Text);
            DataTable dt = ChayVaHien(sql);
            if (dt == null) return;
            BaoTrangThai(TomTatChung(dt), MauTot);
            string cauHoi = txtCauHoi.Text.Trim();
            LuuLichSu(cauHoi.Length == 0 ? "(gõ tay câu lệnh)" : cauHoi, sql);
        }

        private void btnXoaTrang_Click(object sender, EventArgs e)
        {
            txtCauHoi.Clear();
            txtSQL.Clear();
            dgvKetQua.DataSource = null;
            lblKetQua.Text = "Kết quả:";
            BaoTrangThai("", MauCho);
            txtCauHoi.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
