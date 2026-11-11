using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee
{
    public static class GiaoDien
    {
        public static readonly Color Cam = Color.FromArgb(238, 77, 45);
        public static readonly Color Xanh = Color.FromArgb(30, 39, 56);
        public static readonly Color Nen = Color.FromArgb(245, 246, 250);
        public static readonly Color Chu = Color.FromArgb(51, 51, 51);
        public static readonly Color ChonNhat = Color.FromArgb(255, 232, 224);

        private static readonly ConditionalWeakTable<DataGridView, object> _bangDaGan = new ConditionalWeakTable<DataGridView, object>();

        private static readonly Dictionary<string, Color> MAU_NHAN = new Dictionary<string, Color>
        {
            { "Trung tính", Color.FromArgb(233, 236, 239) },
            { "Khen", Color.FromArgb(214, 239, 220) }, { "Chê", Color.FromArgb(248, 215, 218) },
            { "Vui (4-5 sao)", Color.FromArgb(214, 239, 220) }, { "Bình thường (3 sao)", Color.FromArgb(233, 236, 239) }, { "Buồn (1-2 sao)", Color.FromArgb(248, 215, 218) },
            { "ĐỎ", Color.FromArgb(248, 215, 218) }, { "VÀNG", Color.FromArgb(255, 243, 205) }, { "XANH", Color.FromArgb(214, 239, 220) },
            { "KHẨN CẤP", Color.FromArgb(248, 215, 218) }, { "ĐẶT HÀNG NGAY", Color.FromArgb(255, 243, 205) },
            { "TỒN NHIỀU", Color.FromArgb(217, 232, 247) }, { "ỔN", Color.FromArgb(214, 239, 220) }, { "KHÔNG BÁN", Color.FromArgb(233, 236, 239) },
            { "ĐANG HOT - bán nhanh lên", Color.FromArgb(255, 222, 200) }, { "ĐANG NGUỘI - bán chậm lại", Color.FromArgb(217, 232, 247) }
        };

        private static readonly string[] HAM_TAI_LAI = { "LoadData", "LoadDanhSach", "LoadBaoCao", "LoadLichSu", "LoadChiTiet", "NapTatCa", "TaiTatCa", "TaiDanhSach", "TaiDuLieu", "TaiLichSu", "TaiLog" };

        private static readonly string[] NUT_TAI_LAI = { "tải lại", "tải lại dữ liệu", "làm mới", "xem danh sách", "xem báo cáo", "thống kê", "cập nhật số liệu và xem" };

        private static readonly Dictionary<string, string> TEN_COT = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "MaSP", "Mã SP" }, { "TenSP", "Tên sản phẩm" }, { "MaKH", "Mã khách" }, { "TenKH", "Tên khách" },
            { "MaDH", "Mã đơn" }, { "NgayDat", "Ngày đặt" }, { "TongTien", "Tổng tiền" }, { "TrangThai", "Trạng thái" },
            { "SoLuong", "Số lượng" }, { "DonGia", "Đơn giá" }, { "GiaBan", "Giá bán" }, { "MaDanhMuc", "Mã danh mục" },
            { "TenDanhMuc", "Danh mục" }, { "MaKho", "Mã kho" }, { "TenKho", "Kho" }, { "MaNCC", "Mã NCC" }, { "TenNCC", "Nhà cung cấp" },
            { "Ngay", "Ngày" }, { "Thang", "Tháng" }, { "ThoiGian", "Thời gian" }, { "SoDon", "Số đơn" },
            { "Nhom_RFM", "Nhóm khách" }, { "SoNgayTuLanMuaCuoi", "Số ngày chưa quay lại" }, { "TongTienMua", "Tổng tiền đã mua" },
            { "DiemR", "Điểm mua gần đây" }, { "DiemF", "Điểm số lần mua" }, { "DiemM", "Điểm chi tiêu" },
            { "R_NgayTuLanMuaCuoi", "Số ngày chưa quay lại" }, { "F_SoDon", "Số đơn" }, { "M_TongTien", "Tổng tiền" },
            { "NhomKMeans", "Nhóm (máy tự gom)" },
            { "SanPhamChinhId", "Mã món chính" }, { "TenSPChinh", "Món chính" }, { "SanPhamKemId", "Mã món kèm" }, { "TenSPKem", "Món kèm" },
            { "ChiSo_Support", "Tỷ lệ đơn có cả 2 món" }, { "ChiSo_Confidence", "Mua món chính thì mua kèm" }, { "ChiSo_Lift", "Gấp mấy lần bình thường" },
            { "CSAT_PhanTram", "% khách hài lòng" }, { "DiemCamXuc_CSAT", "% khách hài lòng" },
            { "DiemDatHangLai_ROP", "Mức phải đặt hàng" }, { "ROP", "Mức phải đặt hàng" }, { "TonKhoAnToan", "Tồn dự phòng" },
            { "SoNgayConLai_DuKien", "Đủ bán (ngày)" }, { "DoCoGiang_PED", "Mức nhạy giá" },
            { "LechSoVoiTrungBinh", "Mức lệch" }, { "ChiSo_ZScore", "Mức lệch" }, { "DiemBatThuong", "Điểm bất thường" },
            { "SoLuongXuat", "Số lượng xuất" }, { "SoPhieuXuat", "Số phiếu xuất" }, { "TrungBinhNgay", "TB mỗi ngày" },
            { "DoLechChuan", "Dao động thường ngày" }, { "MucDo", "Mức độ" }, { "SoLuongLech", "Lệch (cái)" },
            { "SanPhamId", "Mã SP" }, { "MucDoCaiBao", "Mức độ" },
            { "PC1", "Trục ngang" }, { "PC2", "Trục dọc" }, { "ChiSo", "Chỉ số" },
            { "GiaTrungBinh", "Giá bán TB" }, { "SoLuongBan", "Số lượng bán" },
            { "TuGoc", "Từ gốc" }, { "TuChuanHoa", "Viết đúng là" }, { "LoaiTu", "Loại từ" },
            { "CauHoiText", "Câu hỏi" }, { "CauLenhSQL", "Câu lệnh" },
            { "DoiTuongId", "Mã" }, { "LoaiDoiTuong", "Loại" }, { "NoiDungGoc", "Lời khách" }, { "NhomCamXuc", "Cảm xúc" },
            { "DiemCamXuc", "Điểm cảm xúc" }, { "GanTay", "Đã sửa tay" },
            { "TenChuDe", "Chủ đề" }, { "TanSuatXuatHien", "Số lần gặp" }, { "MucDoUuTien", "Mức ưu tiên" }, { "NgayCapNhat", "Ngày cập nhật" },
            { "DonHangId", "Mã đơn" }, { "KhachHangId", "Mã khách" }, { "DiemRuiRo", "Điểm rủi ro" }, { "LyDoCanhBao", "Lý do cảnh báo" },
            { "TrangThaiXuly", "Tình trạng xử lý" }, { "TyLeRuiRoHuyDon", "Rủi ro hủy (điểm)" }, { "TrangThaiCanhBaoHuy", "Cảnh báo hủy" },
            { "TenTienTrinh", "Việc đã chạy" }, { "SoBanGhiDaSua", "Số dòng đã sửa" }, { "SoBanGhiLoi", "Số dòng lỗi" }, { "ThoiGianChay", "Lúc chạy" },
            { "SoDanhGia", "Số đánh giá" }, { "SoPhieuTra", "Số lần trả" }, { "SoLyDoTraBiChe", "Lần trả có lời chê" },
            { "MaDanhGia", "Mã đánh giá" }, { "SoSao", "Số sao" }, { "NoiDung", "Nội dung" }, { "NgayDanhGia", "Ngày đánh giá" },
            { "MaTraHang", "Mã trả hàng" }, { "LyDoTra", "Lý do trả" }, { "SoTienHoan", "Tiền hoàn" }, { "NgayTra", "Ngày trả" },
            { "CamXucKhachHang", "Giọng khách" }, { "NhomLyDoChinh", "Nhóm lý do" }, { "NoiDung_DaLamSach", "Lời khách (đã sửa chữ)" },
            { "MaDot", "Mã đợt" }, { "TenDot", "Tên đợt" }, { "NgayChinh", "Ngày chính" }, { "NgayBatDau", "Bắt đầu" }, { "NgayKetThuc", "Kết thúc" },
            { "MucGiamToiDa_PhanTram", "Giảm tối đa (%)" }, { "LoiKhachNoi", "Khách nói" }, { "MucHaoHung", "Mức hào hứng (1-5)" },
            { "NhomLyDo", "Nhóm lý do" }, { "Ma", "Mã" }, { "LoiKhach", "Lời khách" }, { "CamXuc", "Cảm xúc" }, { "SoTuKhop", "Số từ khớp" }, { "GiaiThich", "Giải thích" }
        };

        public static void GanChoForm(Form f)
        {
            Size khach = f.ClientSize;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ClientSize = khach;
            GanPhimTaiLai(f);
            f.Load += (s, e) => TrangTri(f);
        }

        public static void CoDinhCuaSo(Form f)
        {
            Size khach = f.ClientSize;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.MaximizeBox = false;
            f.ClientSize = khach;
            f.Load += (s, e) =>
            {
                Rectangle vung = Screen.FromControl(f).WorkingArea;
                if (f.Height <= vung.Height && f.Width <= vung.Width) return;
                f.Size = new Size(Math.Min(f.Width, vung.Width), Math.Min(f.Height, vung.Height));
                f.Location = new Point(vung.Left + (vung.Width - f.Width) / 2, vung.Top + (vung.Height - f.Height) / 2);
            };
        }

        private static void TrangTri(Form f)
        {
            f.SuspendLayout();
            try
            {
                f.BackColor = Nen;
                f.ForeColor = Chu;
                BatDoubleBuffer(f);
                foreach (Control c in TatCa(f)) TrangTriMot(c);
                TaoThanhTieuDe(f);
                VuaManHinh(f);
            }
            catch
            {
            }
            finally
            {
                f.ResumeLayout(true);
            }
        }

        private static List<Control> TatCa(Control goc)
        {
            List<Control> kq = new List<Control>();
            foreach (Control c in goc.Controls)
            {
                kq.Add(c);
                kq.AddRange(TatCa(c));
            }
            return kq;
        }

        public static Font Doi(Font f)
        {
            if (f == null || f.FontFamily.Name != "Times New Roman") return null;
            float s = (float)(Math.Round(f.Size * 0.85 * 2) / 2.0);
            if (s < 8f) s = 8f;
            return new Font("Segoe UI", s, f.Style);
        }

        private static void TrangTriMot(Control c)
        {
            Font moi = Doi(c.Font);
            if (moi != null) c.Font = moi;

            if (c is DataGridView) DinhDangBang((DataGridView)c);
            else if (c is TextBox)
            {
                TextBox t = (TextBox)c;
                t.BorderStyle = BorderStyle.FixedSingle;
                t.BackColor = t.ReadOnly ? Color.FromArgb(236, 238, 243) : Color.White;
                t.ForeColor = Chu;
            }
            else if (c is NumericUpDown)
            {
                c.BackColor = Color.White;
            }
            else if (c is Label)
            {
                if (c.ForeColor.ToArgb() == SystemColors.ControlText.ToArgb()) c.ForeColor = Chu;
            }
            else if (c is GroupBox)
            {
                c.ForeColor = Xanh;
            }
            else if (c is CheckBox || c is RadioButton)
            {
                if (c.ForeColor.ToArgb() == SystemColors.ControlText.ToArgb()) c.ForeColor = Chu;
            }
        }

        public static void DinhDangBang(DataGridView g)
        {
            Font co = g.DefaultCellStyle.Font ?? g.Font;
            Font moi = Doi(co);
            if (moi != null) co = moi;

            g.EnableHeadersVisualStyles = false;
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.FixedSingle;
            g.GridColor = Color.FromArgb(232, 234, 240);
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.RowHeadersVisible = false;
            g.AllowUserToResizeRows = false;

            DataGridViewCellStyle dau = g.ColumnHeadersDefaultCellStyle;
            dau.BackColor = Xanh;
            dau.ForeColor = Color.White;
            dau.SelectionBackColor = Xanh;
            dau.SelectionForeColor = Color.White;
            dau.Font = new Font(co, FontStyle.Bold);
            dau.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dau.Padding = new Padding(6, 0, 4, 0);
            dau.WrapMode = DataGridViewTriState.False;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;

            DataGridViewCellStyle o = g.DefaultCellStyle;
            o.Font = co;
            o.BackColor = Color.White;
            o.ForeColor = Chu;
            o.SelectionBackColor = ChonNhat;
            o.SelectionForeColor = Chu;
            o.Padding = new Padding(4, 0, 4, 0);
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 253);
            g.AlternatingRowsDefaultCellStyle.ForeColor = Chu;
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = ChonNhat;
            g.AlternatingRowsDefaultCellStyle.SelectionForeColor = Chu;

            if (!g.Columns.OfType<DataGridViewImageColumn>().Any())
            {
                g.RowTemplate.Height = 28;
                foreach (DataGridViewRow r in g.Rows) r.Height = 28;
            }
            BatDoubleBuffer(g);
            GanTienIchBang(g);
        }

        private static void GanTienIchBang(DataGridView g)
        {
            object daCo;
            if (_bangDaGan.TryGetValue(g, out daCo)) return;
            _bangDaGan.Add(g, new object());
            g.DataBindingComplete += (s, e) => LamDepCot(g);
            g.CellFormatting += (s, e) =>
            {
                string chu = e.Value as string;
                Color mau;
                if (chu != null && MAU_NHAN.TryGetValue(chu, out mau))
                {
                    e.CellStyle.BackColor = mau;
                    e.CellStyle.SelectionBackColor = ControlPaint.Dark(mau, 0.05f);
                }
            };
            if (g.ContextMenuStrip == null) g.ContextMenuStrip = TaoMenuBang(g);
            LamDepCot(g);
        }

        public static void LamDepCot(DataGridView g)
        {
            foreach (DataGridViewColumn c in g.Columns)
            {
                string goc = string.IsNullOrEmpty(c.DataPropertyName) ? c.Name : c.DataPropertyName;
                string ten;
                if (c.HeaderText == goc && TEN_COT.TryGetValue(goc, out ten)) c.HeaderText = ten;
                if (!string.IsNullOrEmpty(c.DefaultCellStyle.Format) || c.ValueType == null) continue;
                Type t = c.ValueType;
                if (t == typeof(DateTime))
                    c.DefaultCellStyle.Format = CoGio(g, c.Index) ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
                else if (t == typeof(decimal) || t == typeof(double) || t == typeof(float))
                    c.DefaultCellStyle.Format = LaCotTien(goc) ? "N0" : "#,##0.##";
            }
        }

        private static bool CoGio(DataGridView g, int cot)
        {
            int dem = 0;
            foreach (DataGridViewRow r in g.Rows)
            {
                if (r.IsNewRow) continue;
                if (++dem > 500) break;
                object v = r.Cells[cot].Value;
                if (v is DateTime && ((DateTime)v).TimeOfDay != TimeSpan.Zero) return true;
            }
            return false;
        }

        private static bool LaCotTien(string ten)
        {
            string t = ten.ToLowerInvariant();
            if (t.Contains("%") || t.Contains("phantram") || t.EndsWith("pt")) return false;
            return (t.Contains("gia") && !t.Contains("giao")) || t.Contains("tien") || t.Contains("doanhthu") || t.StartsWith("m_")
                   || t.Contains("tiền") || t.Contains("giá") || t.Contains("doanh thu");
        }

        private static ContextMenuStrip TaoMenuBang(DataGridView g)
        {
            ContextMenuStrip m = new ContextMenuStrip();
            ToolStripMenuItem dem = new ToolStripMenuItem { Enabled = false };
            ToolStripMenuItem taiLai = new ToolStripMenuItem("Tải lại dữ liệu (F5)");
            ToolStripMenuItem xuat = new ToolStripMenuItem("Xuất bảng này ra Excel (.csv)...");
            ToolStripMenuItem chep = new ToolStripMenuItem("Chép dòng đang chọn");
            m.Items.Add(dem);
            m.Items.Add(new ToolStripSeparator());
            m.Items.Add(taiLai);
            m.Items.Add(xuat);
            m.Items.Add(chep);
            m.Opening += (s, e) =>
            {
                int soDong = g.Rows.Count - (g.AllowUserToAddRows ? 1 : 0);
                dem.Text = "Bảng đang có " + Math.Max(soDong, 0) + " dòng";
                taiLai.Enabled = TimCachTaiLai(g.FindForm()) != null;
                xuat.Enabled = soDong > 0;
                chep.Enabled = g.CurrentRow != null && !g.CurrentRow.IsNewRow;
            };
            taiLai.Click += (s, e) => TaiLai(g.FindForm());
            xuat.Click += (s, e) => XuatCsv(g);
            chep.Click += (s, e) => ChepDong(g);
            return m;
        }

        private static void GanPhimTaiLai(Form f)
        {
            f.KeyPreview = true;
            f.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.F5) return;
                e.Handled = true;
                TaiLai(f);
            };
        }

        public static void TaiLai(Form f)
        {
            Action a = TimCachTaiLai(f);
            if (a == null) return;
            try { a(); }
            catch (Exception ex) { MessageBox.Show("Chưa tải lại được: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private static Action TimCachTaiLai(Form f)
        {
            if (f == null) return null;
            List<Action> viec = new List<Action>();

            MethodInfo ham = null;
            MethodInfo[] dsHam = f.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (string dau in HAM_TAI_LAI)
            {
                ham = dsHam.FirstOrDefault(m => m.Name.StartsWith(dau) && m.ReturnType == typeof(void) && m.GetParameters().Length == 0);
                if (ham != null) break;
            }
            if (ham != null)
            {
                MethodInfo h = ham;
                viec.Add(() => h.Invoke(f, null));
            }
            else
            {
                foreach (Control c in TatCa(f))
                {
                    Button b = c as Button;
                    if (b == null || !b.Visible || !b.Enabled || b.Text == null) continue;
                    if (!NUT_TAI_LAI.Contains(b.Text.Trim().ToLowerInvariant())) continue;
                    viec.Add(b.PerformClick);
                    break;
                }
            }
            foreach (Control c in TatCa(f))
            {
                Action a = c.Tag as Action;
                if (c is Panel && a != null) viec.Add(a);
            }
            if (viec.Count == 0) return null;
            return () => { foreach (Action a in viec) a(); };
        }

        public static void XuatCsv(DataGridView g)
        {
            Form f = g.FindForm();
            string ten = (f != null && !string.IsNullOrWhiteSpace(f.Text) ? f.Text : "Bang") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
            foreach (char k in Path.GetInvalidFileNameChars()) ten = ten.Replace(k, '_');
            using (SaveFileDialog d = new SaveFileDialog())
            {
                d.Filter = "File mở bằng Excel (*.csv)|*.csv";
                d.FileName = ten.Replace(' ', '_') + ".csv";
                if (d.ShowDialog() != DialogResult.OK) return;
                try
                {
                    List<DataGridViewColumn> cot = g.Columns.Cast<DataGridViewColumn>()
                        .Where(c => c.Visible && !(c is DataGridViewImageColumn)).OrderBy(c => c.DisplayIndex).ToList();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Join(",", cot.Select(c => OCsv(c.HeaderText))));
                    int soDong = 0;
                    foreach (DataGridViewRow r in g.Rows)
                    {
                        if (r.IsNewRow) continue;
                        sb.AppendLine(string.Join(",", cot.Select(c => OCsv(GiaTriXuat(r.Cells[c.Index].Value)))));
                        soDong++;
                    }
                    File.WriteAllText(d.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Đã lưu " + soDong + " dòng vào:\n" + d.FileName, "Xuất file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không lưu được file (file có thể đang mở trong Excel): " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static string GiaTriXuat(object v)
        {
            if (v == null || v == DBNull.Value) return "";
            if (v is DateTime) return ((DateTime)v).ToString("yyyy-MM-dd HH:mm:ss");
            IFormattable so = v as IFormattable;
            if (so != null) return so.ToString(null, CultureInfo.InvariantCulture);
            return v.ToString();
        }

        private static string OCsv(string s)
        {
            s = s ?? "";
            if (s.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0) s = "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        private static void ChepDong(DataGridView g)
        {
            if (g.CurrentRow == null) return;
            List<DataGridViewColumn> cot = g.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible && !(c is DataGridViewImageColumn)).OrderBy(c => c.DisplayIndex).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn c in cot)
                sb.AppendLine(c.HeaderText + ": " + Convert.ToString(g.CurrentRow.Cells[c.Index].FormattedValue));
            Clipboard.SetText(sb.ToString());
        }

        public static void TuChayKhiDoi(Action viec, params Control[] ds)
        {
            foreach (Control c in ds)
            {
                Control o = c;
                if (o is ComboBox) ((ComboBox)o).SelectionChangeCommitted += (s, e) => viec();
                else if (o is NumericUpDown) ((NumericUpDown)o).ValueChanged += (s, e) => { if (o.Focused) viec(); };
                else if (o is DateTimePicker) ((DateTimePicker)o).CloseUp += (s, e) => viec();
                else if (o is CheckBox) ((CheckBox)o).CheckedChanged += (s, e) => { if (o.Focused) viec(); };
            }
        }

        public static DataGridView ThemBangDuoi(Form f, string tieuDe, int cao, Func<DataTable> nap, Action<DataGridViewRow> chonDong)
        {
            Panel p = ThanhPhu.ThemDay(f, cao);
            int rong = f.ClientSize.Width;
            Label nhan = ThanhPhu.Nhan(p, 12, 8, rong - 160, 22, tieuDe, true);
            Button nut = ThanhPhu.Nut(p, "Tải lại", rong - 132, 4, 120, null);
            DataGridView g = ThanhPhu.Luoi(p, 12, 34, rong - 24, cao - 42);
            g.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            nut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Action napLai = () =>
            {
                try
                {
                    g.DataSource = nap();
                    nhan.Text = tieuDe + "  (" + g.Rows.Count + " dòng, bấm 1 dòng để chọn)";
                }
                catch (Exception ex)
                {
                    nhan.Text = tieuDe + " - chưa đọc được dữ liệu: " + AiHelper.LoiDeHieu(ex);
                }
            };
            p.Tag = napLai;
            nut.Click += (s, e) => napLai();
            f.Load += (s, e) => napLai();
            if (chonDong != null)
                g.CellClick += (s, e) => { if (e.RowIndex >= 0 && e.RowIndex < g.Rows.Count) chonDong(g.Rows[e.RowIndex]); };
            return g;
        }

        private static void TaoThanhTieuDe(Form f)
        {
            Label tieu = f.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTieuDe");
            if (tieu == null) return;

            int ranhGioi = int.MaxValue;
            foreach (Control c in f.Controls)
            {
                if (c == tieu || !c.Visible || c.Dock != DockStyle.None) continue;
                ranhGioi = Math.Min(ranhGioi, c.Top);
            }
            int cao = ranhGioi == int.MaxValue ? 60 : Math.Min(60, ranhGioi - 6);
            if (cao < 40)
            {
                tieu.ForeColor = Xanh;
                return;
            }

            tieu.AutoSize = false;
            tieu.Dock = DockStyle.None;
            tieu.SetBounds(0, 0, f.ClientSize.Width, cao);
            tieu.Padding = new Padding(22, 0, 0, 0);
            tieu.TextAlign = ContentAlignment.MiddleLeft;
            tieu.BackColor = Xanh;
            tieu.ForeColor = Color.White;
            tieu.Font = new Font("Segoe UI", cao >= 56 ? 17f : (cao >= 48 ? 15f : 13f), FontStyle.Bold);
            tieu.Paint += (s, e) =>
            {
                using (SolidBrush b = new SolidBrush(Cam))
                    e.Graphics.FillRectangle(b, 0, tieu.Height - 3, tieu.Width, 3);
            };
            tieu.SendToBack();
        }

        private static void VuaManHinh(Form f)
        {
            if (!f.TopLevel) return;
            Rectangle vung = Screen.FromControl(f).WorkingArea;
            if (f.Height <= vung.Height && f.Width <= vung.Width) return;

            int gocCao = f.ClientSize.Height, gocRong = f.ClientSize.Width;
            foreach (Control c in f.Controls.Cast<Control>().ToList())
            {
                if (c.Dock != DockStyle.Bottom) continue;
                int cao = c.Height;
                c.Dock = DockStyle.None;
                c.SetBounds(0, gocCao - cao, gocRong, cao);
            }
            f.AutoScroll = true;
            f.AutoScrollMinSize = new Size(0, gocCao);
            f.Size = new Size(Math.Min(f.Width, vung.Width), Math.Min(f.Height, vung.Height));
            f.Location = new Point(vung.Left + (vung.Width - f.Width) / 2, vung.Top + (vung.Height - f.Height) / 2);
        }

        public static void BatDoubleBuffer(Control c)
        {
            PropertyInfo p = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (p != null) p.SetValue(c, true, null);
        }
    }
}
