using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_ChiaNhomKhachHang : Form
    {
        private List<string> _nhan = new List<string>();
        private List<double> _giaTri = new List<double>();
        private string _tieuDeBieuDo = "Số khách trong từng hạng (đã lưu)";

        public Form_ChiaNhomKhachHang()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            AiHelper.BatDoubleBuffer(pnlBieuDo);
            Load += (s, e) => DatDauCot();
            lblGhiChu.Text = "Khách được chia 5 nhóm, mỗi nhóm số người bằng nhau, xét 3 điều: bao lâu chưa quay lại, mua mấy đơn, chi bao nhiêu tiền (đơn hủy không tính). Hạng 5 - Khách ruột là tốt nhất, Hạng 1 - Khách sắp mất là yếu nhất. Nút \"Máy tự gom nhóm\" chỉ để xem, không đổi nhóm đã lưu.";
        }

        private void btnGomNhom_Click(object sender, EventArgs e)
        {
            ChayKMeans();
        }

        private void ChayKMeans()
        {
            int k = (int)numSoNhom.Value;
            try
            {
                DataTable dt = AiHelper.Doc(
                    "SELECT kh.MaKH, kh.TenKH, DATEDIFF(DAY, MAX(dh.NgayDat), @ngay) AS R, COUNT(*) AS F, SUM(ISNULL(dh.TongTien, 0)) AS M " +
                    "FROM DonHang dh JOIN KhachHang kh ON kh.MaKH = dh.MaKH WHERE ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' GROUP BY kh.MaKH, kh.TenKH",
                    AiHelper.P("@ngay", dtpNgayTinh.Value.Date));
                int n = dt.Rows.Count;
                if (n < k * 2) { AiHelper.BaoTin("Chỉ có " + n + " khách có đơn hợp lệ, ít quá để chia " + k + " nhóm (cần ít nhất " + (k * 2) + ")."); return; }

                double[,] x = new double[n, 3];
                for (int i = 0; i < n; i++)
                {
                    x[i, 0] = Convert.ToDouble(dt.Rows[i]["R"]);
                    x[i, 1] = Convert.ToDouble(dt.Rows[i]["F"]);
                    x[i, 2] = Convert.ToDouble(dt.Rows[i]["M"]);
                }
                double[,] tam;
                int[] nhan = GomNhomTuDong.Chay(DuaVeCungThang.LechSoVoiTrungBinhCot(x), k, out tam);

                double[] tongM = new double[k]; int[] dem = new int[k];
                for (int i = 0; i < n; i++) { tongM[nhan[i]] += x[i, 2]; dem[nhan[i]]++; }
                List<int> thuTu = Enumerable.Range(0, k).Where(c => dem[c] > 0).OrderByDescending(c => tongM[c] / dem[c]).ToList();
                Dictionary<int, string> ten = new Dictionary<int, string>();
                for (int i = 0; i < thuTu.Count; i++) ten[thuTu[i]] = "Nhóm " + (i + 1);

                DataTable kq = new DataTable();
                kq.Columns.Add("MaKH", typeof(string)); kq.Columns.Add("TenKH", typeof(string));
                kq.Columns.Add("R_NgayTuLanMuaCuoi", typeof(int)); kq.Columns.Add("F_SoDon", typeof(int)); kq.Columns.Add("M_TongTien", typeof(decimal));
                kq.Columns.Add("NhomKMeans", typeof(string));
                for (int i = 0; i < n; i++)
                    kq.Rows.Add(dt.Rows[i]["MaKH"], dt.Rows[i]["TenKH"], (int)x[i, 0], (int)x[i, 1], (decimal)x[i, 2], ten[nhan[i]]);
                kq.DefaultView.Sort = "NhomKMeans, M_TongTien DESC";
                dgvRFM.DataSource = kq.DefaultView.ToTable();
                DatCot();

                _nhan.Clear(); _giaTri.Clear();
                foreach (int c in thuTu) { _nhan.Add(ten[c]); _giaTri.Add(dem[c]); }
                _tieuDeBieuDo = "Số khách mỗi nhóm (máy tự gom, không lưu)";
                pnlBieuDo.Invalidate();

                StringBuilder sb = new StringBuilder("Máy tự gom " + n + " khách có đơn thành " + k + " nhóm (chỉ để xem, không lưu). Số trung bình của 1 khách trong nhóm:\n");
                foreach (int c in thuTu)
                {
                    double r = 0, fq = 0;
                    for (int i = 0; i < n; i++) if (nhan[i] == c) { r += x[i, 0]; fq += x[i, 1]; }
                    sb.AppendLine(ten[c] + ": " + dem[c] + " khách - " + (r / dem[c]).ToString("0") + " ngày chưa mua - " + (fq / dem[c]).ToString("0.0") + " đơn - " + (tongM[c] / dem[c]).ToString("N0") + " đ");
                }
                lblGoiY.Text = sb.ToString();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Form_ChiaNhomKhachHang_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvRFM);
            try
            {
                DateTime moiNhat = AiHelper.NgayDuLieuMoiNhat();
                dtpNgayTinh.Value = moiNhat;
                dtpNgayTinh.Tag = moiNhat;
                NapBang(AiHelper.Doc("SELECT MaKH, TenKH, Nhom_RFM FROM KhachHang ORDER BY Nhom_RFM DESC, MaKH"));
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NapBang(DataTable dt)
        {
            dgvRFM.DataSource = dt;
            DatCot();
            SortedDictionary<string, int> dem = new SortedDictionary<string, int>();
            foreach (DataRow r in dt.Rows)
            {
                string h = r["Nhom_RFM"] == DBNull.Value ? "Chưa xếp hạng" : Convert.ToString(r["Nhom_RFM"]);
                int c; dem.TryGetValue(h, out c); dem[h] = c + 1;
            }
            _nhan.Clear(); _giaTri.Clear();
            foreach (KeyValuePair<string, int> kv in dem) { _nhan.Add(kv.Key); _giaTri.Add(kv.Value); }
            _tieuDeBieuDo = "Số khách trong từng hạng (đã lưu)";
            pnlBieuDo.Invalidate();
            lblGoiY.Text = "";
            HienGoiY();
        }

        private void DatDauCot()
        {
            dgvRFM.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRFM.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRFM.ColumnHeadersHeight = 46;
        }

        private void DatCot()
        {
            dgvRFM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DatMotCot("MaKH", 70, 60);
            DatMotCot("TenKH", 150, 130);
            DatMotCot("Nhom_RFM", 200, 150);
            DatMotCot("R_NgayTuLanMuaCuoi", 90, 75);
            DatMotCot("F_SoDon", 60, 55);
            DatMotCot("M_TongTien", 105, 95);
            DatMotCot("NhomKMeans", 85, 75);
        }

        private void DatMotCot(string tenCot, float phan, int rongToiThieu)
        {
            if (!dgvRFM.Columns.Contains(tenCot)) return;
            DataGridViewColumn c = dgvRFM.Columns[tenCot];
            c.FillWeight = phan;
            c.MinimumWidth = rongToiThieu;
        }

        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            try
            {
                NapBang(AiHelper.ChayThuTuc("sp_ChiaNhomKhachHang", AiHelper.P("@NgayTinh", dtpNgayTinh.Value.Date)));
                DateTime? moiNhat = dtpNgayTinh.Tag as DateTime?;
                if (moiNhat.HasValue && dtpNgayTinh.Value.Date < moiNhat.Value)
                    AiHelper.BaoTin("Lưu ý: ngày tính (" + dtpNgayTinh.Value.ToString("dd/MM/yyyy") + ") sớm hơn đơn mới nhất (" + moiNhat.Value.ToString("dd/MM/yyyy") + "), số ngày chưa quay lại của một số khách sẽ bị âm.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvRFM_SelectionChanged(object sender, EventArgs e)
        {
            HienGoiY();
        }

        private void HienGoiY()
        {
            if (dgvRFM.CurrentRow == null || !dgvRFM.Columns.Contains("Nhom_RFM")) return;
            string h = Convert.ToString(dgvRFM.CurrentRow.Cells["Nhom_RFM"].Value);
            string goiY;
            switch (h)
            {
                case "Hạng 5 - Khách ruột": goiY = "Khách tốt nhất (mua gần đây, mua nhiều, chi nhiều): giữ chân bằng ưu đãi tri ân, mời dùng thử hàng mới."; break;
                case "Hạng 4 - Khách thân": goiY = "Khách khá: khuyến khích mua thêm bằng combo hoặc ưu đãi theo mốc chi tiêu."; break;
                case "Hạng 3 - Khách thường": goiY = "Khách trung bình: nhắc mua lại, gợi ý sản phẩm hay được mua cùng."; break;
                case "Hạng 2 - Khách ít ghé": goiY = "Khách ít hoạt động: gửi voucher có hạn dùng để kéo quay lại."; break;
                case "Hạng 1 - Khách sắp mất": goiY = "Khách yếu nhất hoặc lâu không mua: làm chiến dịch quay lại, hoặc giảm chi phí chăm sóc."; break;
                default: goiY = "Chưa có đơn hợp lệ nên chưa xếp hạng."; break;
            }
            lblGoiY.Text = "Gợi ý chung cho " + (h.Length == 0 ? "khách này" : h) + " (gợi ý theo kinh nghiệm bán hàng, không phải số liệu tính ra):\n" + goiY;
        }

        private void pnlBieuDo_Paint(object sender, PaintEventArgs e)
        {
            AiHelper.VeCot(e.Graphics, pnlBieuDo.ClientRectangle, _nhan, _giaTri, _tieuDeBieuDo);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
