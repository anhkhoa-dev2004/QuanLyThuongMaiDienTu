using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public static class DuaVeCungThang
    {
        public static double[] VeKhoangKhongMot(double[] x)
        {
            if (x.Length == 0) return new double[0];
            double min = x.Min(), max = x.Max();
            double[] r = new double[x.Length];
            for (int i = 0; i < x.Length; i++) r[i] = max - min < 1e-12 ? 0 : (x[i] - min) / (max - min);
            return r;
        }

        public static double[] LechSoVoiTrungBinh(double[] x)
        {
            int n = x.Length;
            double[] r = new double[n];
            if (n == 0) return r;
            double tb = x.Average();
            double s2 = 0; foreach (double v in x) s2 += (v - tb) * (v - tb);
            double sd = n > 1 ? Math.Sqrt(s2 / (n - 1)) : 0;
            for (int i = 0; i < n; i++) r[i] = sd < 1e-12 ? 0 : (x[i] - tb) / sd;
            return r;
        }

        private static double[] Cot(double[,] m, int j)
        {
            double[] c = new double[m.GetLength(0)];
            for (int i = 0; i < c.Length; i++) c[i] = m[i, j];
            return c;
        }

        public static double[,] VeKhoangKhongMotCot(double[,] m)
        {
            int n = m.GetLength(0), p = m.GetLength(1);
            double[,] r = new double[n, p];
            for (int j = 0; j < p; j++)
            {
                double[] c = VeKhoangKhongMot(Cot(m, j));
                for (int i = 0; i < n; i++) r[i, j] = c[i];
            }
            return r;
        }

        public static double[,] LechSoVoiTrungBinhCot(double[,] m)
        {
            int n = m.GetLength(0), p = m.GetLength(1);
            double[,] r = new double[n, p];
            for (int j = 0; j < p; j++)
            {
                double[] c = LechSoVoiTrungBinh(Cot(m, j));
                for (int i = 0; i < n; i++) r[i, j] = c[i];
            }
            return r;
        }
    }

    public static class TimGiaTriLa
    {
        public static double PhanVi(double[] daSapXep, double p)
        {
            int n = daSapXep.Length;
            if (n == 1) return daSapXep[0];
            double vt = p * (n - 1);
            int lo = (int)Math.Floor(vt), hi = Math.Min(lo + 1, n - 1);
            return daSapXep[lo] + (vt - lo) * (daSapXep[hi] - daSapXep[lo]);
        }

        public static bool[] DanhDau(double[] x, double k, out double canDuoi, out double canTren)
        {
            canDuoi = 0; canTren = 0;
            if (x.Length < 4) return new bool[x.Length];
            double[] s = (double[])x.Clone();
            Array.Sort(s);
            double q1 = PhanVi(s, 0.25), q3 = PhanVi(s, 0.75), iqr = q3 - q1;
            canDuoi = q1 - k * iqr; canTren = q3 + k * iqr;
            bool[] kq = new bool[x.Length];
            for (int i = 0; i < x.Length; i++) kq[i] = x[i] < canDuoi || x[i] > canTren;
            return kq;
        }
    }

    public static class TinhTheoXuHuong
    {
        public static double[] Hoc(double[,] x, double[] y, double lambda)
        {
            int n = x.GetLength(0), p = x.GetLength(1), m = p + 1;
            double[,] a = new double[m, m];
            double[] b = new double[m];
            double[] z = new double[m];
            for (int i = 0; i < n; i++)
            {
                z[0] = 1;
                for (int j = 0; j < p; j++) z[j + 1] = x[i, j];
                for (int r = 0; r < m; r++)
                {
                    b[r] += z[r] * y[i];
                    for (int c = 0; c < m; c++) a[r, c] += z[r] * z[c];
                }
            }
            for (int j = 1; j < m; j++) a[j, j] += lambda;
            return GiaiHe(a, b);
        }

        public static double DuDoan(double[] he, double[] x)
        {
            double s = he[0];
            for (int j = 0; j < x.Length; j++) s += he[j + 1] * x[j];
            return s;
        }

        public static double[] GiaiHe(double[,] ma, double[] vp)
        {
            int m = vp.Length;
            double[,] a = (double[,])ma.Clone();
            double[] b = (double[])vp.Clone();
            for (int col = 0; col < m; col++)
            {
                int chot = col;
                for (int r = col + 1; r < m; r++) if (Math.Abs(a[r, col]) > Math.Abs(a[chot, col])) chot = r;
                if (Math.Abs(a[chot, col]) < 1e-10) return null;
                if (chot != col)
                {
                    for (int c = 0; c < m; c++) { double t = a[col, c]; a[col, c] = a[chot, c]; a[chot, c] = t; }
                    double tb = b[col]; b[col] = b[chot]; b[chot] = tb;
                }
                for (int r = col + 1; r < m; r++)
                {
                    double f = a[r, col] / a[col, col];
                    for (int c = col; c < m; c++) a[r, c] -= f * a[col, c];
                    b[r] -= f * b[col];
                }
            }
            double[] kq = new double[m];
            for (int r = m - 1; r >= 0; r--)
            {
                double s = b[r];
                for (int c = r + 1; c < m; c++) s -= a[r, c] * kq[c];
                kq[r] = s / a[r, r];
            }
            return kq;
        }
    }

    public static class DuBaoTheoXuHuong
    {
        public static double[] DuBao(double[] chuoi, int soBuoc, out string loi)
        {
            loi = null;
            int n = chuoi.Length;
            if (n < 15) { loi = "Cần ít nhất 15 ngày dữ liệu (hiện có " + n + ")."; return null; }

            double[] d = new double[n - 1];
            for (int i = 1; i < n; i++) d[i - 1] = chuoi[i] - chuoi[i - 1];

            int m = d.Length - 2;
            double[,] x = new double[m, 2];
            double[] y = new double[m];
            for (int t = 2; t < d.Length; t++) { x[t - 2, 0] = d[t - 1]; x[t - 2, 1] = d[t - 2]; y[t - 2] = d[t]; }

            double[] he = TinhTheoXuHuong.Hoc(x, y, 0);
            if (he == null) { loi = "Chuỗi quá đều hoặc quá thưa (nhiều ngày bằng 0) nên không tìm được hệ số."; return null; }

            List<double> dd = new List<double>(d);
            double cuoi = chuoi[n - 1];
            double[] kq = new double[soBuoc];
            for (int s = 0; s < soBuoc; s++)
            {
                int k = dd.Count;
                double tiep = he[0] + he[1] * dd[k - 1] + he[2] * dd[k - 2];
                double moi = Math.Max(0, cuoi + tiep);
                dd.Add(moi - cuoi);
                cuoi = moi;
                kq[s] = moi;
            }
            return kq;
        }
    }

    public static class DuBaoTheoThuTrongTuan
    {
        private static double[] DacTrung(DateTime ngay, int t, int n)
        {
            double[] f = new double[7];
            f[0] = (double)t / n;
            int thu = ((int)ngay.DayOfWeek + 6) % 7;
            if (thu >= 1) f[thu] = 1;
            return f;
        }

        public static double[] DuBao(double[] chuoi, DateTime ngayDau, int soBuoc, double lambda, out string loi)
        {
            loi = null;
            int n = chuoi.Length;
            if (n < 21) { loi = "Cần ít nhất 21 ngày dữ liệu (hiện có " + n + ")."; return null; }
            double[,] x = new double[n, 7];
            for (int i = 0; i < n; i++)
            {
                double[] f = DacTrung(ngayDau.AddDays(i), i, n);
                for (int j = 0; j < 7; j++) x[i, j] = f[j];
            }
            double[] he = TinhTheoXuHuong.Hoc(x, chuoi, lambda);
            if (he == null) { loi = "Không tính được vì số liệu quá ít thay đổi."; return null; }
            double[] kq = new double[soBuoc];
            for (int s = 0; s < soBuoc; s++)
                kq[s] = Math.Max(0, TinhTheoXuHuong.DuDoan(he, DacTrung(ngayDau.AddDays(n + s), n + s, n)));
            return kq;
        }
    }

    public static class SoNhapMoiLan
    {
        public static double Tinh(double nhuCauNam, double chiPhiMoiLanDat, double chiPhiLuuKhoMotDonViNam)
        {
            if (nhuCauNam <= 0 || chiPhiMoiLanDat <= 0 || chiPhiLuuKhoMotDonViNam <= 0) return 0;
            return Math.Sqrt(2 * nhuCauNam * chiPhiMoiLanDat / chiPhiLuuKhoMotDonViNam);
        }
    }

    public static class GomNhomTuDong
    {
        private static double Kc2(double[,] a, int i, double[,] tam, int c, int p)
        {
            double s = 0;
            for (int j = 0; j < p; j++) { double d = a[i, j] - tam[c, j]; s += d * d; }
            return s;
        }

        public static int[] Chay(double[,] du, int k, out double[,] tam)
        {
            int n = du.GetLength(0), p = du.GetLength(1);
            if (k > n) k = n;
            if (k < 1) k = 1;
            Random rnd = new Random(12345);
            tam = new double[k, p];

            int dau = rnd.Next(n);
            for (int j = 0; j < p; j++) tam[0, j] = du[dau, j];
            double[] kc = new double[n];
            for (int c = 1; c < k; c++)
            {
                double tong = 0;
                for (int i = 0; i < n; i++)
                {
                    double tot = double.MaxValue;
                    for (int q = 0; q < c; q++) tot = Math.Min(tot, Kc2(du, i, tam, q, p));
                    kc[i] = tot; tong += tot;
                }
                int chon = n - 1;
                if (tong <= 0) chon = rnd.Next(n);
                else
                {
                    double r = rnd.NextDouble() * tong, acc = 0;
                    for (int i = 0; i < n; i++) { acc += kc[i]; if (acc >= r) { chon = i; break; } }
                }
                for (int j = 0; j < p; j++) tam[c, j] = du[chon, j];
            }

            int[] nhan = new int[n];
            for (int i = 0; i < n; i++) nhan[i] = -1;
            for (int lan = 0; lan < 100; lan++)
            {
                bool doi = false;
                for (int i = 0; i < n; i++)
                {
                    int gan = 0; double tot = double.MaxValue;
                    for (int c = 0; c < k; c++) { double d = Kc2(du, i, tam, c, p); if (d < tot) { tot = d; gan = c; } }
                    if (nhan[i] != gan) { nhan[i] = gan; doi = true; }
                }
                if (!doi) break;
                double[,] moi = new double[k, p];
                int[] dem = new int[k];
                for (int i = 0; i < n; i++) { dem[nhan[i]]++; for (int j = 0; j < p; j++) moi[nhan[i], j] += du[i, j]; }
                for (int c = 0; c < k; c++)
                    if (dem[c] > 0) for (int j = 0; j < p; j++) tam[c, j] = moi[c, j] / dem[c];
            }
            return nhan;
        }
    }

    public static class QuetNhieuChiSo
    {
        private class Nut
        {
            public int Cot = -1;
            public double Cat;
            public Nut Trai, Phai;
            public int KichCo;
        }

        private static double C(int n)
        {
            if (n <= 1) return 0;
            if (n == 2) return 1;
            return 2 * (Math.Log(n - 1) + 0.5772156649) - 2.0 * (n - 1) / n;
        }

        public static double[] Diem(double[,] du, int soCay, int mauCon, int seed)
        {
            int n = du.GetLength(0), p = du.GetLength(1);
            double[] diem = new double[n];
            if (n < 2) return diem;
            int psi = Math.Min(mauCon, n);
            int sauToiDa = (int)Math.Ceiling(Math.Log(psi, 2));
            Random rnd = new Random(seed);
            double[] tong = new double[n];
            for (int c = 0; c < soCay; c++)
            {
                int[] tatCa = new int[n];
                for (int i = 0; i < n; i++) tatCa[i] = i;
                for (int i = 0; i < psi; i++) { int j = i + rnd.Next(n - i); int t = tatCa[i]; tatCa[i] = tatCa[j]; tatCa[j] = t; }
                int[] chon = new int[psi];
                Array.Copy(tatCa, chon, psi);
                Nut goc = Xay(du, chon, 0, sauToiDa, rnd, p);
                for (int i = 0; i < n; i++) tong[i] += DoDai(goc, du, i, 0);
            }
            double cn = C(psi);
            for (int i = 0; i < n; i++) diem[i] = Math.Pow(2, -(tong[i] / soCay) / cn);
            return diem;
        }

        private static Nut Xay(double[,] du, int[] idx, int sau, int sauToiDa, Random rnd, int p)
        {
            Nut nut = new Nut { KichCo = idx.Length };
            if (sau >= sauToiDa || idx.Length <= 1) return nut;
            for (int thu = 0; thu < p * 2; thu++)
            {
                int cot = rnd.Next(p);
                double mn = double.MaxValue, mx = double.MinValue;
                foreach (int i in idx) { mn = Math.Min(mn, du[i, cot]); mx = Math.Max(mx, du[i, cot]); }
                if (mx - mn < 1e-12) continue;
                double cat = mn + rnd.NextDouble() * (mx - mn);
                List<int> tr = new List<int>(), ph = new List<int>();
                foreach (int i in idx) { if (du[i, cot] < cat) tr.Add(i); else ph.Add(i); }
                if (tr.Count == 0 || ph.Count == 0) continue;
                nut.Cot = cot; nut.Cat = cat;
                nut.Trai = Xay(du, tr.ToArray(), sau + 1, sauToiDa, rnd, p);
                nut.Phai = Xay(du, ph.ToArray(), sau + 1, sauToiDa, rnd, p);
                return nut;
            }
            return nut;
        }

        private static double DoDai(Nut nut, double[,] du, int i, int sau)
        {
            if (nut.Cot < 0) return sau + C(nut.KichCo);
            return du[i, nut.Cot] < nut.Cat ? DoDai(nut.Trai, du, i, sau + 1) : DoDai(nut.Phai, du, i, sau + 1);
        }
    }

    public class LuatPhanLoai
    {
        private class Nut
        {
            public int Cot = -1;
            public double Nguong;
            public Nut Trai, Phai;
            public double P1;
            public int N;
        }

        private Nut _goc;

        public static LuatPhanLoai Hoc(double[,] x, int[] y, int doSauToiDa, int laToiThieu)
        {
            LuatPhanLoai cay = new LuatPhanLoai();
            List<int> idx = new List<int>();
            for (int i = 0; i < y.Length; i++) idx.Add(i);
            cay._goc = Xay(x, y, idx, 0, doSauToiDa, laToiThieu);
            return cay;
        }

        private static double Gini(int n1, int n)
        {
            if (n == 0) return 0;
            double p = (double)n1 / n;
            return 2 * p * (1 - p);
        }

        private static Nut Xay(double[,] x, int[] y, List<int> idx, int sau, int sauToiDa, int laToiThieu)
        {
            int n = idx.Count, n1 = 0;
            foreach (int i in idx) if (y[i] == 1) n1++;
            Nut nut = new Nut { N = n, P1 = n == 0 ? 0 : (double)n1 / n };
            if (sau >= sauToiDa || n1 == 0 || n1 == n || n < 2 * laToiThieu) return nut;

            int p = x.GetLength(1);
            double tot = Gini(n1, n), nguongTot = 0;
            int cotTot = -1;
            for (int cot = 0; cot < p; cot++)
            {
                List<int> s = idx.OrderBy(i => x[i, cot]).ToList();
                int l = 0, l1 = 0;
                for (int v = 0; v < n - 1; v++)
                {
                    l++; if (y[s[v]] == 1) l1++;
                    if (x[s[v], cot] == x[s[v + 1], cot]) continue;
                    if (l < laToiThieu || n - l < laToiThieu) continue;
                    double g = (l * Gini(l1, l) + (n - l) * Gini(n1 - l1, n - l)) / n;
                    if (g < tot - 1e-12) { tot = g; cotTot = cot; nguongTot = (x[s[v], cot] + x[s[v + 1], cot]) / 2; }
                }
            }
            if (cotTot < 0) return nut;

            List<int> tr = new List<int>(), ph = new List<int>();
            foreach (int i in idx) { if (x[i, cotTot] <= nguongTot) tr.Add(i); else ph.Add(i); }
            nut.Cot = cotTot; nut.Nguong = nguongTot;
            nut.Trai = Xay(x, y, tr, sau + 1, sauToiDa, laToiThieu);
            nut.Phai = Xay(x, y, ph, sau + 1, sauToiDa, laToiThieu);
            return nut;
        }

        public double XacSuat1(double[] dong)
        {
            Nut nut = _goc;
            while (nut.Cot >= 0) nut = dong[nut.Cot] <= nut.Nguong ? nut.Trai : nut.Phai;
            return nut.P1;
        }

        public string MoTa(string[] tenCot)
        {
            StringBuilder sb = new StringBuilder();
            MoTaNut(_goc, tenCot, 0, sb);
            return sb.ToString();
        }

        private static void MoTaNut(Nut nut, string[] tenCot, int sau, StringBuilder sb)
        {
            string le = new string(' ', sau * 3);
            if (nut.Cot < 0) { sb.AppendLine(le + "-> " + (nut.P1 * 100).ToString("0") + "% là lớp 1 (" + nut.N + " dòng)"); return; }
            sb.AppendLine(le + "nếu " + tenCot[nut.Cot] + " <= " + nut.Nguong.ToString("0.###") + ":");
            MoTaNut(nut.Trai, tenCot, sau + 1, sb);
            sb.AppendLine(le + "ngược lại:");
            MoTaNut(nut.Phai, tenCot, sau + 1, sb);
        }
    }

    public static class SinhThemMau
    {
        private static double Kc(double[] a, double[] b)
        {
            double s = 0;
            for (int j = 0; j < a.Length; j++) s += (a[j] - b[j]) * (a[j] - b[j]);
            return s;
        }

        public static List<double[]> SinhThem(List<double[]> thieuSo, int soCanThem, int k, int seed)
        {
            List<double[]> kq = new List<double[]>();
            int m = thieuSo.Count;
            if (m < 2 || soCanThem <= 0) return kq;
            Random rnd = new Random(seed);
            k = Math.Max(1, Math.Min(k, m - 1));
            for (int s = 0; s < soCanThem; s++)
            {
                int a = rnd.Next(m);
                List<int> gan = Enumerable.Range(0, m).Where(j => j != a).OrderBy(j => Kc(thieuSo[a], thieuSo[j])).Take(k).ToList();
                int b = gan[rnd.Next(gan.Count)];
                double t = rnd.NextDouble();
                double[] moi = new double[thieuSo[a].Length];
                for (int j = 0; j < moi.Length; j++) moi[j] = thieuSo[a][j] + t * (thieuSo[b][j] - thieuSo[a][j]);
                kq.Add(moi);
            }
            return kq;
        }
    }

    public static class DienTheoMonGiong
    {
        public static double? DuDoan(double[,] dacTrung, double?[] giaTri, int dong, int k)
        {
            int n = dacTrung.GetLength(0), p = dacTrung.GetLength(1);
            List<KeyValuePair<double, double>> ds = new List<KeyValuePair<double, double>>();
            for (int j = 0; j < n; j++)
            {
                if (j == dong || !giaTri[j].HasValue) continue;
                double kc = 0;
                for (int c = 0; c < p; c++) { double d = dacTrung[dong, c] - dacTrung[j, c]; kc += d * d; }
                ds.Add(new KeyValuePair<double, double>(kc, giaTri[j].Value));
            }
            if (ds.Count == 0) return null;
            return ds.OrderBy(v => v.Key).Take(k).Average(v => v.Value);
        }
    }

    public static class CamXucTuDien
    {
        private static List<KeyValuePair<string, int>> _tuKhoa;
        private static List<KeyValuePair<string, string>> _vietTat;
        private static readonly string[] PhuDinh = { "không", "chưa", "chẳng" };
        private const string KyTuBo = "!@#$%^&*()_+={}[]|\\<>?~`\":;'";

        private static List<string[]> _tuLyDoMua;
        private static Dictionary<string, string[]> _cachBan;

        public static void LamMoi()
        {
            _tuKhoa = null;
            _vietTat = null;
            _tuLyDoMua = null;
            _cachBan = null;
        }

        public static string CamXucTheoSao(int soSao)
        {
            if (soSao >= 4) return "Vui (4-5 sao)";
            if (soSao == 3) return "Bình thường (3 sao)";
            return "Buồn (1-2 sao)";
        }

        public static bool SaoCaoMaLoiKhongKhen(int soSao, string vanBan, string nhanTheoLoi)
        {
            return soSao >= 4 && !string.IsNullOrWhiteSpace(vanBan) && nhanTheoLoi != "Khen";
        }

        private static bool CoMot(string s, params string[] tu)
        {
            foreach (string x in tu) if (s.IndexOf(x, StringComparison.Ordinal) >= 0) return true;
            return false;
        }

        public static string NhomLyDoTra(string vanBan)
        {
            if (string.IsNullOrWhiteSpace(vanBan)) return "Không nêu lý do";
            string s = ChuanBi(vanBan);
            if (CoMot(s, "lỗi", "hỏng", "vỡ", "trầy", "xước", "móp", "không hoạt động")) return "Hàng lỗi / hư hỏng / trầy xước";
            if (CoMot(s, "khác mô tả", "không đúng mô tả", "không giống", "khác hình", "hàng giả", "nhái")) return "Không đúng mô tả / hàng giả";
            if (CoMot(s, "sai", "thiếu", "nhầm", "không đúng")) return "Giao sai / thiếu hàng";
            if (CoMot(s, "chậm", "trễ", "lâu")) return "Giao hàng chậm";
            if (CoMot(s, "đổi ý", "không cần", "không thích", "không vừa")) return "Đổi ý / không có nhu cầu";
            return "Khác";
        }

        public static string XepLyDoMua(string vanBan, out int soTuKhop)
        {
            if (_tuLyDoMua == null)
            {
                List<string[]> ds = new List<string[]>();
                foreach (DataRow r in AiHelper.Doc("SELECT TuKhoa, NhomLyDo, ThuTuNhom FROM TuDien_LyDoMua").Rows)
                    ds.Add(new[] { Convert.ToString(r["TuKhoa"]).ToLowerInvariant(), Convert.ToString(r["NhomLyDo"]), Convert.ToString(r["ThuTuNhom"]) });
                _tuLyDoMua = ds;
            }
            string s = ChuanBi(vanBan);
            Dictionary<string, int> dem = new Dictionary<string, int>();
            Dictionary<string, int> thuTu = new Dictionary<string, int>();
            foreach (string[] d in _tuLyDoMua)
            {
                if (s.IndexOf(" " + d[0] + " ", StringComparison.Ordinal) < 0) continue;
                int c;
                dem.TryGetValue(d[1], out c);
                dem[d[1]] = c + 1;
                thuTu[d[1]] = Convert.ToInt32(d[2]);
            }
            soTuKhop = 0;
            if (dem.Count == 0) return "Khác / không rõ";
            string nhom = dem.OrderByDescending(x => x.Value).ThenBy(x => thuTu[x.Key]).First().Key;
            soTuKhop = dem[nhom];
            return nhom;
        }

        public static string CachBan(string nhomLyDo, out string kieuLyDo)
        {
            if (_cachBan == null)
            {
                Dictionary<string, string[]> ds = new Dictionary<string, string[]>();
                foreach (DataRow r in AiHelper.Doc("SELECT NhomLyDo, KieuLyDo, CachBanHop FROM CachBan_TheoLyDoMua").Rows)
                    ds[Convert.ToString(r["NhomLyDo"])] = new[] { Convert.ToString(r["KieuLyDo"]), Convert.ToString(r["CachBanHop"]) };
                _cachBan = ds;
            }
            string[] v;
            if (!_cachBan.TryGetValue(nhomLyDo ?? "", out v)) { kieuLyDo = ""; return ""; }
            kieuLyDo = v[0];
            return v[1];
        }

        private static void NapTuDien()
        {
            if (_tuKhoa != null && _vietTat != null) return;
            List<KeyValuePair<string, int>> tk = new List<KeyValuePair<string, int>>();
            foreach (DataRow r in AiHelper.Doc("SELECT TuKhoa, Diem FROM TuDien_CamXuc").Rows)
                tk.Add(new KeyValuePair<string, int>(Convert.ToString(r["TuKhoa"]).ToLowerInvariant(), Convert.ToInt32(r["Diem"])));
            List<KeyValuePair<string, string>> vt = new List<KeyValuePair<string, string>>();
            foreach (DataRow r in AiHelper.Doc("SELECT TuGoc, TuChuanHoa FROM TuDien_Teencode ORDER BY LEN(TuGoc) DESC, Id").Rows)
                vt.Add(new KeyValuePair<string, string>(Convert.ToString(r["TuGoc"]).ToLowerInvariant(), Convert.ToString(r["TuChuanHoa"])));
            _tuKhoa = tk;
            _vietTat = vt;
        }

        public static string ChuanBi(string vanBan)
        {
            NapTuDien();
            string s = (vanBan ?? "").ToLowerInvariant();
            foreach (char c in ",.-/") s = s.Replace(c, ' ');
            foreach (char c in KyTuBo) s = s.Replace(c, ' ');
            s = s.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' ');
            while (s.Contains("  ")) s = s.Replace("  ", " ");
            s = " " + s.Trim() + " ";
            for (int lan = 0; lan < 2; lan++)
            {
                foreach (KeyValuePair<string, string> p in _vietTat)
                    s = s.Replace(" " + p.Key + " ", " " + p.Value + " ");
            }
            return s;
        }

        public static void Cham(string vanBan, out decimal diem, out string nhan)
        {
            string s = ChuanBi(vanBan);
            int khen = 0, che = 0;
            foreach (KeyValuePair<string, int> p in _tuKhoa)
            {
                if (s.IndexOf(" " + p.Key + " ", StringComparison.Ordinal) < 0) continue;
                bool dao = false;
                foreach (string pd in PhuDinh)
                {
                    if (s.IndexOf(" " + pd + " " + p.Key + " ", StringComparison.Ordinal) >= 0) { dao = true; break; }
                }
                int d = dao ? -p.Value : p.Value;
                if (d > 0) khen++; else che++;
            }
            diem = khen + che == 0 ? 0m : Math.Round((decimal)(khen - che) / (khen + che), 4, MidpointRounding.AwayFromZero);
            nhan = khen > che ? "Khen" : (che > khen ? "Chê" : "Trung tính");
        }
    }
}
