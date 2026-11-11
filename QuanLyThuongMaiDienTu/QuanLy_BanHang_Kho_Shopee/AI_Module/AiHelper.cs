using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public static class AiHelper
    {
        public static SqlParameter P(string ten, object giaTri)
        {
            return new SqlParameter(ten, giaTri ?? DBNull.Value);
        }

        public static SqlParameter PDec(string ten, decimal giaTri, byte precision, byte scale)
        {
            return new SqlParameter(ten, SqlDbType.Decimal) { Precision = precision, Scale = scale, Value = giaTri };
        }

        public static string LoiDeHieu(Exception ex)
        {
            if (ex == null) return "";
            Exception goc = ex;
            while (goc.InnerException != null && !(goc is SqlException)) goc = goc.InnerException;

            SqlException sq = goc as SqlException;
            if (sq != null)
            {
                switch (sq.Number)
                {
                    case -1: case 2: case 53: case 26: case 40: case 10061:
                        return "Không kết nối được SQL Server. Hãy kiểm tra SQL Server đã bật chưa và tên máy ở dòng Data Source trong file App.config.";
                    case -2:
                        return "Máy tính lâu quá nên đã dừng. Hãy thử lại sau ít giây.";
                    case 4060:
                        return "Chưa có database QuanLyThuongMaiDienTu. Hãy mở file QuanLyThuongMaiDienTu_Database.sql trong SSMS và bấm Execute.";
                    case 18456:
                        return "SQL Server không cho tài khoản Windows này đăng nhập. Hãy kiểm tra quyền đăng nhập trong SSMS.";
                    case 2627: case 2601:
                        return "Mã này đã có rồi. Hãy dùng mã khác.";
                    case 547:
                        return "Không lưu được vì dữ liệu không khớp: mã nhập vào chưa có trong danh sách, số lượng hoặc số tiền không hợp lệ, hoặc dòng này đang được dùng ở chỗ khác nên chưa xóa được.";
                    case 515:
                        return "Còn ô bắt buộc chưa nhập.";
                    case 8152: case 2628:
                        return "Có ô nhập quá dài. Hãy rút gọn lại.";
                    case 208: case 2812: case 207:
                        return "Database đang thiếu bảng hoặc cột mà phần mềm cần. Hãy chạy lại file QuanLyThuongMaiDienTu_Database.sql mới nhất.";
                    case 1205:
                        return "Có người khác đang sửa cùng dữ liệu. Hãy thử lại.";
                }
                if (sq.Number >= 50000) return sq.Message;
                return sq.Message;
            }
            if (goc is System.Net.Http.HttpRequestException)
                return "Không kết nối được máy chủ AI. Hãy kiểm tra mạng Internet rồi thử lại.";
            if (goc is System.Threading.Tasks.TaskCanceledException)
                return "Máy chủ AI trả lời chậm quá nên đã dừng. Hãy thử lại.";
            if (goc is FormatException || goc is InvalidCastException)
                return "Có ô nhập sai kiểu (ví dụ ô số mà gõ chữ). Hãy kiểm tra lại.";
            return goc.Message;
        }

        public static DataTable Doc(string sql, params SqlParameter[] ps)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            using (SqlConnection cn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) da.Fill(dt);
            }
            return dt;
        }

        public static int Chay(string sql, params SqlParameter[] ps)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SqlConnection cn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static object LayGiaTri(string sql, params SqlParameter[] ps)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SqlConnection cn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                cn.Open();
                object o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? null : o;
            }
        }

        public static string ChayThuTucLayMa(string tenThuTuc, string tenThamSoRa, params SqlParameter[] ps)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SqlConnection cn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand(tenThuTuc, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                SqlParameter ra = null;
                if (!string.IsNullOrEmpty(tenThamSoRa))
                {
                    ra = cmd.Parameters.Add(tenThamSoRa, SqlDbType.VarChar, 20);
                    ra.Direction = ParameterDirection.Output;
                }
                cn.Open();
                cmd.ExecuteNonQuery();
                return ra == null ? null : Convert.ToString(ra.Value);
            }
        }

        public static string LayMaMoi(string loai)
        {
            return ChayThuTucLayMa("sp_LayMaMoi", "@Ma", P("@Loai", loai));
        }

        public static bool DocSoTien(string chu, out decimal giaTri)
        {
            string s = (chu ?? "").Trim().Replace(" ", "").Replace(".", "").Replace(",", "").Replace("đ", "");
            return decimal.TryParse(s, System.Globalization.NumberStyles.AllowLeadingSign, System.Globalization.CultureInfo.InvariantCulture, out giaTri);
        }

        public static DateTime NgayDuLieuMoiNhat()
        {
            object o = LayGiaTri("SELECT dbo.fn_NgayDuLieuMoiNhat()");
            return o == null ? DateTime.Today : Convert.ToDateTime(o).Date;
        }

        public static DataTable ChayThuTuc(string tenThuTuc, params SqlParameter[] ps)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            using (SqlConnection cn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand(tenThuTuc, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) da.Fill(dt);
            }
            return dt;
        }

        public static DataSet ChayThuTucNhieuBang(string tenThuTuc, params SqlParameter[] ps)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataSet ds = new DataSet();
            using (SqlConnection cn = DbHelper.TaoKetNoi())
            using (SqlCommand cmd = new SqlCommand(tenThuTuc, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 180;
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) da.Fill(ds);
            }
            return ds;
        }

        public static void ChamLaiTraHangVaDanhGia()
        {
            ChayThuTuc("sp_TuDongLamSach_DuLieuGiaoDich", P("@HienKetQua", false));
            ChayThuTuc("sp_PhanTichChuDeKhieuNai", P("@HienKetQua", false));
            ChayThuTuc("sp_PhanTichCamXuc_DanhGia", P("@HienKetQua", false));
        }

        public static void ChuanBiSoLieuThongBao()
        {
            ChamLaiTraHangVaDanhGia();
            ChayThuTuc("sp_PhanTichLyDoMua", P("@HienKetQua", false));
        }

        public static int SoDau(DataTable dt, string cot)
        {
            if (dt == null || dt.Rows.Count == 0 || !dt.Columns.Contains(cot) || dt.Rows[0][cot] == DBNull.Value) return 0;
            return Convert.ToInt32(dt.Rows[0][cot]);
        }

        public static void DinhDangLuoi(DataGridView g)
        {
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.RowHeadersVisible = false;
            g.BackgroundColor = Color.White;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            g.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            GiaoDien.DinhDangBang(g);
        }

        public static void DinhDangSo(DataGridView g, string tenCot, string dinhDang)
        {
            if (g.Columns.Contains(tenCot)) g.Columns[tenCot].DefaultCellStyle.Format = dinhDang;
        }

        public static void BaoLoi(Exception ex)
        {
            Cursor.Current = Cursors.Default;
            MessageBox.Show(LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void BaoTin(string noiDung)
        {
            MessageBox.Show(noiDung, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void BatDoubleBuffer(Control c)
        {
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(c, true, null);
        }

        public static void NapCombo(ComboBox cbo, DataTable dt, string cotGiaTri, string cotHienThi)
        {
            cbo.DataSource = dt;
            cbo.ValueMember = cotGiaTri;
            cbo.DisplayMember = cotHienThi;
        }

        private static void NenVaTieuDe(Graphics g, Rectangle v, string tieuDe)
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (Font f = new Font("Segoe UI", 10F, FontStyle.Bold))
                g.DrawString(tieuDe ?? "", f, Brushes.Black, 8, 4);
        }

        private static void KhongCoDuLieu(Graphics g, Rectangle v)
        {
            using (Font f = new Font("Segoe UI", 10F, FontStyle.Italic))
            {
                string s = "Chưa có dữ liệu";
                SizeF sz = g.MeasureString(s, f);
                g.DrawString(s, f, Brushes.Gray, (v.Width - sz.Width) / 2, (v.Height - sz.Height) / 2);
            }
        }

        public static void VeCot(Graphics g, Rectangle v, IList<string> nhan, IList<double> giaTri, string tieuDe, IList<Color> mauCot = null)
        {
            NenVaTieuDe(g, v, tieuDe);
            if (nhan == null || giaTri == null || giaTri.Count == 0) { KhongCoDuLieu(g, v); return; }
            int n = giaTri.Count;
            double max = 0; for (int i = 0; i < n; i++) if (giaTri[i] > max) max = giaTri[i];
            if (max <= 0) max = 1;
            int trai = 10, phai = 10, tren = 46;
            float rong = (v.Width - trai - phai) / (float)n;
            float coChu = rong < 45 ? 7.5F : 8.5F;
            using (Font f = new Font("Segoe UI", coChu))
            using (Font fSo = new Font("Segoe UI", 8.5F, FontStyle.Bold))
            using (StringFormat giua = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisWord })
            {
                float caoNhan = 0;
                for (int i = 0; i < n; i++)
                    caoNhan = Math.Max(caoNhan, g.MeasureString(nhan[i] ?? "", f, (int)Math.Max(10, rong - 4), giua).Height);
                int duoi = (int)Math.Min(Math.Max(26, caoNhan + 8), v.Height / 2.5F);
                float cao = v.Height - tren - duoi;
                if (cao < 20) cao = 20;
                g.DrawLine(Pens.Gray, trai, tren + cao, v.Width - phai, tren + cao);
                for (int i = 0; i < n; i++)
                {
                    float h = (float)(giaTri[i] / max) * cao;
                    float x = trai + i * rong + rong * 0.15F;
                    float w = rong * 0.7F;
                    Color mau = mauCot != null && i < mauCot.Count ? mauCot[i] : Color.SteelBlue;
                    using (Brush b = new SolidBrush(mau)) g.FillRectangle(b, x, tren + cao - h, w, h);
                    g.DrawString(giaTri[i].ToString("#,##0.#"), fSo, Brushes.Black, new RectangleF(x - 20, tren + cao - h - 18, w + 40, 18), giua);
                    g.DrawString(nhan[i] ?? "", f, Brushes.Black, new RectangleF(trai + i * rong + 2, tren + cao + 4, rong - 4, duoi - 4), giua);
                }
            }
        }

        public static void VeDuong(Graphics g, Rectangle v, IList<string> nhanX, IList<double> thuc, IList<double> duBao, string tieuDe,
                                   string tenThuc = "Thực tế", string tenDuBao = "Dự báo")
        {
            NenVaTieuDe(g, v, tieuDe);
            int n = thuc == null ? 0 : thuc.Count;
            if (n < 2) { KhongCoDuLieu(g, v); return; }
            double max = 0;
            for (int i = 0; i < n; i++)
            {
                if (!double.IsNaN(thuc[i]) && thuc[i] > max) max = thuc[i];
                if (duBao != null && !double.IsNaN(duBao[i]) && duBao[i] > max) max = duBao[i];
            }
            if (max <= 0) max = 1;
            int trai = 44, phai = 14, tren = 30, duoi = 30;
            float rong = v.Width - trai - phai, cao = v.Height - tren - duoi;
            Func<int, float> X = i => trai + rong * i / (n - 1);
            Func<double, float> Y = val => (float)(tren + cao - (val / max) * cao);
            using (Font f = new Font("Segoe UI", 9F))
            {
                g.DrawLine(Pens.Gray, trai, tren, trai, tren + cao);
                g.DrawLine(Pens.Gray, trai, tren + cao, trai + rong, tren + cao);
                g.DrawString(max.ToString("N0"), f, Brushes.Black, 2, tren - 6);
                g.DrawString("0", f, Brushes.Black, 24, tren + cao - 8);
                int buoc = Math.Max(1, n / 8);
                for (int i = 0; i < n; i += buoc)
                    g.DrawString(nhanX[i], f, Brushes.Black, X(i) - 14, tren + cao + 4);
                VeMotDuong(g, thuc, X, Y, Color.SteelBlue);
                if (duBao != null) VeMotDuong(g, duBao, X, Y, Color.Crimson);
                float x = v.Width - 8;
                if (duBao != null)
                {
                    x -= g.MeasureString(tenDuBao, f).Width;
                    g.DrawString(tenDuBao, f, Brushes.Black, x, 3);
                    x -= 14;
                    g.FillRectangle(Brushes.Crimson, x, 6, 10, 10);
                    x -= 12;
                }
                x -= g.MeasureString(tenThuc, f).Width;
                g.DrawString(tenThuc, f, Brushes.Black, x, 3);
                g.FillRectangle(Brushes.SteelBlue, x - 14, 6, 10, 10);
            }
        }

        private static void VeMotDuong(Graphics g, IList<double> gt, Func<int, float> X, Func<double, float> Y, Color mau)
        {
            using (Pen but = new Pen(mau, 2F))
            using (Brush co = new SolidBrush(mau))
            {
                int truoc = -1;
                for (int i = 0; i < gt.Count; i++)
                {
                    if (double.IsNaN(gt[i])) { truoc = -1; continue; }
                    if (truoc >= 0) g.DrawLine(but, X(truoc), Y(gt[truoc]), X(i), Y(gt[i]));
                    truoc = i;
                }
                if (gt.Count <= 60)
                    for (int i = 0; i < gt.Count; i++)
                        if (!double.IsNaN(gt[i])) g.FillEllipse(co, X(i) - 2.5F, Y(gt[i]) - 2.5F, 5, 5);
            }
        }

        public static void VeDiem(Graphics g, Rectangle v, IList<double> x, IList<double> y, IList<string> nhan, string tieuDe, string tenX, string tenY)
        {
            NenVaTieuDe(g, v, tieuDe);
            if (x == null || x.Count == 0) { KhongCoDuLieu(g, v); return; }
            double minX = double.MaxValue, maxX = double.MinValue, minY = double.MaxValue, maxY = double.MinValue;
            for (int i = 0; i < x.Count; i++)
            {
                minX = Math.Min(minX, x[i]); maxX = Math.Max(maxX, x[i]);
                minY = Math.Min(minY, y[i]); maxY = Math.Max(maxY, y[i]);
            }
            if (maxX - minX < 1e-9) { minX -= 1; maxX += 1; }
            if (maxY - minY < 1e-9) { minY -= 1; maxY += 1; }
            double dx = (maxX - minX) * 0.1, dy = (maxY - minY) * 0.1;
            minX -= dx; maxX += dx; minY -= dy; maxY += dy;
            int trai = 30, phai = 20, tren = 30, duoi = 28;
            float rong = v.Width - trai - phai, cao = v.Height - tren - duoi;
            Func<double, float> X = val => (float)(trai + (val - minX) / (maxX - minX) * rong);
            Func<double, float> Y = val => (float)(tren + cao - (val - minY) / (maxY - minY) * cao);
            using (Font f = new Font("Segoe UI", 9F))
            using (Pen luoi = new Pen(Color.Silver) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                g.DrawRectangle(Pens.Gray, trai, tren, rong, cao);
                if (minX < 0 && maxX > 0) g.DrawLine(luoi, X(0), tren, X(0), tren + cao);
                if (minY < 0 && maxY > 0) g.DrawLine(luoi, trai, Y(0), trai + rong, Y(0));
                g.DrawString(tenX, f, Brushes.Black, trai + rong / 2 - 20, tren + cao + 8);
                g.DrawString(tenY, f, Brushes.Black, 2, tren - 4);
                for (int i = 0; i < x.Count; i++)
                {
                    g.FillEllipse(Brushes.SteelBlue, X(x[i]) - 4, Y(y[i]) - 4, 8, 8);

                    float rongNhan = g.MeasureString(nhan[i], f).Width;
                    float xNhan = X(x[i]) + 5 + rongNhan > trai + rong ? X(x[i]) - 5 - rongNhan : X(x[i]) + 5;
                    g.DrawString(nhan[i], f, Brushes.Black, xNhan, Y(y[i]) - 7);
                }
            }
        }
    }
}
