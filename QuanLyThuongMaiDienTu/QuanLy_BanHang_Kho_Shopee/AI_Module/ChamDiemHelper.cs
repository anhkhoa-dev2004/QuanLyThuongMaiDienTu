using System;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public static class ChamDiemHelper
    {
        public class KetQua
        {
            public double[] GiaTriRieng;
            public double[] TyLePhuongSai;
            public double[,] VectorRieng;
            public double[,] Diem;
        }

        public static KetQua Tinh(double[,] x)
        {
            int n = x.GetLength(0), p = x.GetLength(1);
            if (n < 2 || p < 1) throw new ArgumentException("Cần ít nhất 2 dòng và 1 cột.");

            double[,] z = new double[n, p];
            for (int j = 0; j < p; j++)
            {
                double tb = 0; for (int i = 0; i < n; i++) tb += x[i, j]; tb /= n;
                double s2 = 0; for (int i = 0; i < n; i++) s2 += (x[i, j] - tb) * (x[i, j] - tb);
                double sd = Math.Sqrt(s2 / (n - 1));
                for (int i = 0; i < n; i++) z[i, j] = sd < 1e-12 ? 0 : (x[i, j] - tb) / sd;
            }

            double[,] c = new double[p, p];
            for (int a = 0; a < p; a++)
                for (int b = 0; b < p; b++)
                {
                    double t = 0; for (int i = 0; i < n; i++) t += z[i, a] * z[i, b];
                    c[a, b] = t / (n - 1);
                }

            double[] gtr; double[,] vec;
            Jacobi(c, out gtr, out vec);

            int[] idx = new int[p]; for (int i = 0; i < p; i++) idx[i] = i;
            Array.Sort(idx, (a, b) => gtr[b].CompareTo(gtr[a]));

            KetQua kq = new KetQua();
            kq.GiaTriRieng = new double[p];
            kq.VectorRieng = new double[p, p];
            for (int k = 0; k < p; k++)
            {
                kq.GiaTriRieng[k] = Math.Max(0, gtr[idx[k]]);

                int lon = 0; double m = 0;
                for (int r = 0; r < p; r++) if (Math.Abs(vec[r, idx[k]]) > m) { m = Math.Abs(vec[r, idx[k]]); lon = r; }
                double dau = vec[lon, idx[k]] < 0 ? -1 : 1;
                for (int r = 0; r < p; r++) kq.VectorRieng[r, k] = dau * vec[r, idx[k]];
            }

            double tong = 0; for (int k = 0; k < p; k++) tong += kq.GiaTriRieng[k];
            kq.TyLePhuongSai = new double[p];
            for (int k = 0; k < p; k++) kq.TyLePhuongSai[k] = tong <= 0 ? 0 : kq.GiaTriRieng[k] / tong;

            kq.Diem = new double[n, p];
            for (int i = 0; i < n; i++)
                for (int k = 0; k < p; k++)
                {
                    double t = 0; for (int r = 0; r < p; r++) t += z[i, r] * kq.VectorRieng[r, k];
                    kq.Diem[i, k] = t;
                }
            return kq;
        }

        private static void Jacobi(double[,] ma, out double[] gtr, out double[,] vec)
        {
            int p = ma.GetLength(0);
            double[,] a = (double[,])ma.Clone();
            double[,] v = new double[p, p];
            for (int i = 0; i < p; i++) v[i, i] = 1;

            for (int quet = 0; quet < 100; quet++)
            {
                double off = 0;
                for (int i = 0; i < p; i++) for (int j = i + 1; j < p; j++) off += a[i, j] * a[i, j];
                if (off < 1e-22) break;

                for (int pp = 0; pp < p - 1; pp++)
                    for (int q = pp + 1; q < p; q++)
                    {
                        if (Math.Abs(a[pp, q]) < 1e-300) continue;
                        double theta = (a[q, q] - a[pp, pp]) / (2 * a[pp, q]);
                        double t = Math.Sign(theta == 0 ? 1 : theta) / (Math.Abs(theta) + Math.Sqrt(theta * theta + 1));
                        double c = 1 / Math.Sqrt(t * t + 1), s = t * c;
                        for (int k = 0; k < p; k++)
                        {
                            double akp = a[k, pp], akq = a[k, q];
                            a[k, pp] = c * akp - s * akq; a[k, q] = s * akp + c * akq;
                        }
                        for (int k = 0; k < p; k++)
                        {
                            double apk = a[pp, k], aqk = a[q, k];
                            a[pp, k] = c * apk - s * aqk; a[q, k] = s * apk + c * aqk;
                        }
                        for (int k = 0; k < p; k++)
                        {
                            double vkp = v[k, pp], vkq = v[k, q];
                            v[k, pp] = c * vkp - s * vkq; v[k, q] = s * vkp + c * vkq;
                        }
                    }
            }
            gtr = new double[p];
            for (int i = 0; i < p; i++) gtr[i] = a[i, i];
            vec = v;
        }
    }
}
