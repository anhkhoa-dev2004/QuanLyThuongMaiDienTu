using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_KhachHayPhanNan : Form
    {
        private List<string> _nhan = new List<string>();
        private List<double> _giaTri = new List<double>();

        public Form_KhachHayPhanNan()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            AiHelper.BatDoubleBuffer(pnlBieuDo);
        }

        private void Form_KhachHayPhanNan_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvChuDe);
            AiHelper.DinhDangLuoi(dgvChiTiet);
            try
            {
                NapChuDe(AiHelper.Doc("SELECT TenChuDe, TanSuatXuatHien, MucDoUuTien, NgayCapNhat FROM ChuDe_KhieuNai ORDER BY MucDoUuTien"));
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void NapChuDe(DataTable dt)
        {
            dgvChuDe.DataSource = dt;
            _nhan.Clear(); _giaTri.Clear();
            foreach (DataRow r in dt.Rows)
            {
                _nhan.Add(Convert.ToString(r["TenChuDe"]));
                _giaTri.Add(Convert.ToDouble(r["TanSuatXuatHien"]));
            }
            dgvChiTiet.DataSource = null;
            pnlBieuDo.Invalidate();
        }

        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            try
            {
                NapChuDe(AiHelper.ChayThuTuc("sp_PhanTichChuDeKhieuNai"));
                if (_nhan.Count == 0) AiHelper.BaoTin("Chưa có phiếu trả hàng nào để phân tích.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvChuDe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string chuDe = Convert.ToString(dgvChuDe.Rows[e.RowIndex].Cells["TenChuDe"].Value);
            try
            {
                dgvChiTiet.DataSource = AiHelper.Doc(
                    "SELECT th.MaTraHang, th.MaDH, th.MaSP, sp.TenSP, th.SoLuong, th.LyDoTra, th.NoiDung_DaLamSach, th.NgayTra " +
                    "FROM TraHang th JOIN SanPham sp ON sp.MaSP = th.MaSP " +
                    "WHERE th.NhomLyDoChinh = @cd ORDER BY th.NgayTra DESC",
                    AiHelper.P("@cd", chuDe));
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void pnlBieuDo_Paint(object sender, PaintEventArgs e)
        {
            AiHelper.VeCot(e.Graphics, pnlBieuDo.ClientRectangle, _nhan, _giaTri, "Số phiếu trả hàng theo chủ đề");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
