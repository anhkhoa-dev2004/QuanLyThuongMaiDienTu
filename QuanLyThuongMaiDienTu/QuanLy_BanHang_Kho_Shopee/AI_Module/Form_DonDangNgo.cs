using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_DonDangNgo : Form
    {
        public Form_DonDangNgo()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void Form_DonDangNgo_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvDonHang);
            AiHelper.DinhDangLuoi(dgvXuLy);
            AiHelper.DinhDangLuoi(dgvTraHang);
            cboLoc.SelectedIndex = 0;
            cboTrangThaiXuLy.SelectedIndex = 0;
            TaiTatCa();
        }

        private void TaiTatCa()
        {
            TaiDonHang();
            TaiXuLy();
            TaiTraHang();
        }

        private void TaiDonHang()
        {
            try
            {
                dgvDonHang.DataSource = AiHelper.Doc(
                    "SELECT dh.MaDH, dh.NgayDat, kh.TenKH, kh.SoDT, dh.TongTien, dh.TyLeRuiRoHuyDon, dh.TrangThaiCanhBaoHuy, dh.CoNghiNghiepGianLan " +
                    "FROM DonHang dh LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH " +
                    "WHERE @loc = 0 " +
                    "   OR (@loc = 1 AND dh.TrangThaiCanhBaoHuy = N'Nguy cơ cao') " +
                    "   OR (@loc = 2 AND dh.TrangThaiCanhBaoHuy = N'Cần xem') " +
                    "   OR (@loc = 3 AND dh.CoNghiNghiepGianLan = 1) " +
                    "ORDER BY dh.TyLeRuiRoHuyDon DESC, dh.NgayDat DESC",
                    AiHelper.P("@loc", Math.Max(0, cboLoc.SelectedIndex)));
                AiHelper.DinhDangSo(dgvDonHang, "TongTien", "N0");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void TaiXuLy()
        {
            try
            {
                dgvXuLy.DataSource = AiHelper.Doc(
                    "SELECT Id, DonHangId, KhachHangId, DiemRuiRo, LyDoCanhBao, TrangThaiXuly FROM BatThuong_DonHang_GianLan ORDER BY Id DESC");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void TaiTraHang()
        {
            try
            {
                dgvTraHang.DataSource = AiHelper.Doc(
                    "SELECT MaTraHang, MaDH, MaSP, SoLuong, LyDoTra, CoGianLanDoiTra FROM TraHang ORDER BY NgayTra DESC, MaTraHang");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private string MaDonDangChon()
        {
            return dgvDonHang.CurrentRow == null ? null : Convert.ToString(dgvDonHang.CurrentRow.Cells["MaDH"].Value);
        }

        private void btnChayDiem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable kq = AiHelper.ChayThuTuc("sp_CanhBaoRuiRoHuyDon");
                AiHelper.BaoTin("Đã chấm điểm rủi ro cho " + kq.Rows.Count + " đơn hàng.\n\n" +
                                "Quy tắc điểm (trong thủ tục): SĐT thiếu/sai/số mẫu +40, địa chỉ thiếu/quá ngắn/địa chỉ mẫu +30, tỷ lệ đơn đã hủy trước đó tối đa +30.");
                TaiDonHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoc.SelectedIndex >= 0 && dgvDonHang.IsHandleCreated) TaiDonHang();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaiTatCa();
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnGanCo_Click(object sender, EventArgs e)
        {
            string ma = MaDonDangChon();
            if (ma == null) { AiHelper.BaoTin("Hãy chọn một đơn hàng."); return; }
            string lyDo = txtLyDo.Text.Trim();
            if (lyDo.Length == 0) { AiHelper.BaoTin("Hãy nhập lý do nghi ngờ để lưu lại."); return; }
            try
            {
                AiHelper.Chay(
                    "SET XACT_ABORT ON; BEGIN TRANSACTION; " +
                    "UPDATE DonHang SET CoNghiNghiepGianLan = 1 WHERE MaDH = @d; " +
                    "IF EXISTS (SELECT 1 FROM BatThuong_DonHang_GianLan WHERE DonHangId = @d AND TrangThaiXuly <> N'Không gian lận') " +
                    "    UPDATE BatThuong_DonHang_GianLan SET LyDoCanhBao = @ly WHERE DonHangId = @d AND TrangThaiXuly <> N'Không gian lận' " +
                    "ELSE " +
                    "    INSERT INTO BatThuong_DonHang_GianLan (DonHangId, KhachHangId, DiemRuiRo, LyDoCanhBao) " +
                    "    SELECT MaDH, MaKH, TyLeRuiRoHuyDon, @ly FROM DonHang WHERE MaDH = @d; " +
                    "COMMIT TRANSACTION;",
                    AiHelper.P("@d", ma), AiHelper.P("@ly", lyDo));
                txtLyDo.Clear();
                TaiTatCa();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnBoCo_Click(object sender, EventArgs e)
        {
            string ma = MaDonDangChon();
            if (ma == null) { AiHelper.BaoTin("Hãy chọn một đơn hàng."); return; }
            try
            {
                AiHelper.Chay(
                    "SET XACT_ABORT ON; BEGIN TRANSACTION; " +
                    "UPDATE DonHang SET CoNghiNghiepGianLan = 0 WHERE MaDH = @d; " +
                    "UPDATE BatThuong_DonHang_GianLan SET TrangThaiXuly = N'Không gian lận' WHERE DonHangId = @d AND TrangThaiXuly <> N'Không gian lận'; " +
                    "COMMIT TRANSACTION;",
                    AiHelper.P("@d", ma));
                TaiTatCa();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvXuLy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int i = cboTrangThaiXuLy.FindStringExact(Convert.ToString(dgvXuLy.Rows[e.RowIndex].Cells["TrangThaiXuly"].Value));
            if (i >= 0) cboTrangThaiXuLy.SelectedIndex = i;
        }

        private void btnCapNhatXuLy_Click(object sender, EventArgs e)
        {
            if (dgvXuLy.CurrentRow == null) { AiHelper.BaoTin("Hãy chọn một dòng trong danh sách đơn đã gắn cờ."); return; }
            int id = Convert.ToInt32(dgvXuLy.CurrentRow.Cells["Id"].Value);
            string tt = Convert.ToString(cboTrangThaiXuLy.SelectedItem);
            try
            {
                AiHelper.Chay(
                    "SET XACT_ABORT ON; BEGIN TRANSACTION; " +
                    "UPDATE BatThuong_DonHang_GianLan SET TrangThaiXuly = @tt WHERE Id = @id; " +
                    "UPDATE dh SET CoNghiNghiepGianLan = CASE WHEN @tt = N'Không gian lận' THEN 0 ELSE 1 END " +
                    "FROM DonHang dh JOIN BatThuong_DonHang_GianLan b ON b.DonHangId = dh.MaDH WHERE b.Id = @id; " +
                    "COMMIT TRANSACTION;",
                    AiHelper.P("@tt", tt), AiHelper.P("@id", id));
                TaiTatCa();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvTraHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnCoTraHang_Click(object sender, EventArgs e)
        {
            if (dgvTraHang.CurrentRow == null) { AiHelper.BaoTin("Hãy chọn một phiếu trả hàng."); return; }
            string ma = Convert.ToString(dgvTraHang.CurrentRow.Cells["MaTraHang"].Value);
            try
            {
                AiHelper.Chay("UPDATE TraHang SET CoGianLanDoiTra = CASE WHEN CoGianLanDoiTra = 1 THEN 0 ELSE 1 END WHERE MaTraHang = @m", AiHelper.P("@m", ma));
                TaiTraHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
