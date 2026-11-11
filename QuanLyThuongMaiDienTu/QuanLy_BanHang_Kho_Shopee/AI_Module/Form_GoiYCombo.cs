using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_GoiYCombo : Form
    {
        public Form_GoiYCombo()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            Load += (s, e) => DatDauCot();
        }

        private const string SQL_DOC_COMBO =
            "SELECT d.SanPhamChinhId, sp1.TenSP AS TenSPChinh, d.SanPhamKemId, sp2.TenSP AS TenSPKem, " +
            "       d.ChiSo_Support, d.ChiSo_Confidence, d.ChiSo_Lift " +
            "FROM DeXuat_ComboKhuyenMai d " +
            "JOIN SanPham sp1 ON sp1.MaSP = d.SanPhamChinhId JOIN SanPham sp2 ON sp2.MaSP = d.SanPhamKemId " +
            "ORDER BY d.ChiSo_Lift DESC, d.ChiSo_Confidence DESC";

        private void Form_GoiYCombo_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvCombo);
            try
            {
                object n = AiHelper.LayGiaTri("SELECT COUNT(DISTINCT MaDH) FROM ChiTietDonHang");
                lblTongDon.Text = "Tổng số đơn có chi tiết: " + Convert.ToInt32(n ?? 0);
                NapBang(AiHelper.Doc(SQL_DOC_COMBO));
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void DatDauCot()
        {
            dgvCombo.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCombo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCombo.ColumnHeadersHeight = 46;
        }

        private void DatCot()
        {
            dgvCombo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DatMotCot("SanPhamChinhId", 85, 80);
            DatMotCot("TenSPChinh", 235, 220);
            DatMotCot("SanPhamKemId", 85, 80);
            DatMotCot("TenSPKem", 235, 220);
            DatMotCot("ChiSo_Support", 130, 120);
            DatMotCot("ChiSo_Confidence", 145, 130);
            DatMotCot("ChiSo_Lift", 134, 120);
        }

        private void DatMotCot(string tenCot, float phan, int rongToiThieu)
        {
            if (!dgvCombo.Columns.Contains(tenCot)) return;
            DataGridViewColumn c = dgvCombo.Columns[tenCot];
            c.FillWeight = phan;
            c.MinimumWidth = rongToiThieu;
        }

        private void NapBang(DataTable dt)
        {
            dgvCombo.DataSource = dt;
            AiHelper.DinhDangSo(dgvCombo, "ChiSo_Support", "0.0000");
            AiHelper.DinhDangSo(dgvCombo, "ChiSo_Confidence", "0.00%");
            AiHelper.DinhDangSo(dgvCombo, "ChiSo_Lift", "0.00");
            DatCot();
            if (dt.Rows.Count == 0)
                lblGoiY.Text = "Chưa có cặp nào. Bấm \"Tìm món hay mua chung\" (đã bấm mà vẫn trống thì hạ 3 mức lọc xuống).";
            else
                HienGoiY();
        }

        private void HienGoiY()
        {
            DataGridViewRow r = dgvCombo.CurrentRow;
            if (r == null || r.IsNewRow) return;
            if (!dgvCombo.Columns.Contains("TenSPChinh") || !dgvCombo.Columns.Contains("ChiSo_Confidence") || !dgvCombo.Columns.Contains("ChiSo_Lift")) return;
            object chinh = r.Cells["TenSPChinh"].Value;
            object kem = r.Cells["TenSPKem"].Value;
            object conf = r.Cells["ChiSo_Confidence"].Value;
            object lift = r.Cells["ChiSo_Lift"].Value;
            if (chinh == null || chinh == DBNull.Value || conf == null || conf == DBNull.Value || lift == null || lift == DBNull.Value) return;
            double tyLe = Convert.ToDouble(conf) * 100;
            double gap = Convert.ToDouble(lift);
            lblGoiY.Text = "Khách mua \"" + chinh + "\" thì nên gợi ý thêm \"" + kem + "\".\n" +
                           "Trong các đơn có sản phẩm chính, " + tyLe.ToString("0.00") + "% có cả sản phẩm kèm; " +
                           "mua cùng nhau gấp " + gap.ToString("0.00") + " lần so với bình thường.";
        }

        private void btnKhaiPha_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable kq = AiHelper.ChayThuTuc("sp_TimMonHayMuaChung",
                    AiHelper.PDec("@SupportToiThieu", numSupport.Value, 9, 6),
                    AiHelper.PDec("@ConfidenceToiThieu", numConfidence.Value, 9, 6),
                    AiHelper.PDec("@LiftToiThieu", numLift.Value, 12, 4));
                NapBang(kq);
                if (kq.Rows.Count == 0)
                    AiHelper.BaoTin("Không có cặp sản phẩm nào đạt cả 3 mức lọc. Hãy hạ 3 mức lọc xuống rồi thử lại.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvCombo_SelectionChanged(object sender, EventArgs e)
        {
            HienGoiY();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
