using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_DocCamXucDanhGia : Form
    {
        private const string NGUON_DANH_GIA = "Lời đánh giá sản phẩm";
        private const string NGUON_TRA_HANG = "Lý do trả hàng";
        private const string KHEN = "Khen";
        private const string TRUNG_TINH = "Trung tính";
        private const string CHE = "Chê";
        private ComboBox _cboNguon;
        private ComboBox _cboLoc;

        public Form_DocCamXucDanhGia()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            lblGhiChu.Text = "Máy đọc lời khách theo từ điển: đếm số từ khen và số từ chê, bên nào nhiều hơn thì xếp vào bên đó. Có chữ \"không / chưa / chẳng\" đứng ngay trước thì đảo nghĩa (\"không tốt\" là chê). " +
                             "Lời không có từ khen, từ chê, hoặc khen chê ngang nhau thì xếp vào Trung tính. Dòng nào máy đọc sai thì bấm chọn dòng đó rồi chọn lại ở ô \"Sửa thành\", máy lưu ngay vào cơ sở dữ liệu.";
            lblCSAT.Text = "Mức hài lòng theo sản phẩm (% khách hài lòng = % đánh giá từ 4 sao trở lên, cùng cách tính với Bảng thông báo):";
            btnCapNhatCSAT.Text = "Đọc lại và tính lại % khách hài lòng";
            lblNhan.Text = "Sửa thành:";
            btnGanNhan.Text = "Lưu dòng đang chọn";
            btnLamSach.Text = "Sửa chữ viết tắt rồi đọc lại";
            btnChamLaiTuDong.Text = "Máy đọc lại tất cả (giữ dòng đã sửa tay)";
            btnBoGanTay.Text = "Trả dòng đang chọn về cho máy đọc";

            Label lblXem = new Label { Text = "Xem:", AutoSize = true, Location = new Point(560, 88) };
            _cboNguon = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(600, 84), Size = new Size(230, 26) };
            _cboNguon.Items.AddRange(new object[] { NGUON_DANH_GIA, NGUON_TRA_HANG });
            _cboLoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(845, 84), Size = new Size(230, 26) };
            _cboLoc.Items.AddRange(new object[] { "Tất cả", "Chỉ lời khen", "Chỉ lời trung tính", "Chỉ lời chê" });
            _cboNguon.SelectedIndex = 0;
            _cboLoc.SelectedIndex = 0;
            _cboNguon.SelectionChangeCommitted += (s, e) => TaiTraHang();
            _cboLoc.SelectionChangeCommitted += (s, e) => TaiTraHang();
            Controls.Add(lblXem);
            Controls.Add(_cboNguon);
            Controls.Add(_cboLoc);
            cboCamXuc.SelectionChangeCommitted += (s, e) => GanNhan(true);
        }

        private bool DangXemTraHang
        {
            get { return Convert.ToString(_cboNguon.SelectedItem) == NGUON_TRA_HANG; }
        }

        private string LoaiDoiTuong
        {
            get { return DangXemTraHang ? "Trả hàng" : "Đánh giá"; }
        }

        private string CamXucDangLoc
        {
            get
            {
                switch (_cboLoc.SelectedIndex)
                {
                    case 1: return KHEN;
                    case 2: return TRUNG_TINH;
                    case 3: return CHE;
                    default: return "";
                }
            }
        }

        private void btnChamLaiTuDong_Click(object sender, EventArgs e)
        {
            ChamLaiTuDong();
        }

        private void btnBoGanTay_Click(object sender, EventArgs e)
        {
            BoGanTay();
        }

        private void ChamLaiTuDong()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                AiHelper.ChamLaiTraHangVaDanhGia();
                TaiTraHang();
                TaiCSAT();
                AiHelper.BaoTin("Máy đã đọc lại xong. Dòng có cột \"Đã sửa tay\" được giữ nguyên.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
            finally { Cursor = Cursors.Default; }
        }

        private void BoGanTay()
        {
            if (dgvTraHang.CurrentRow == null) { AiHelper.BaoTin("Hãy bấm chọn một dòng."); return; }
            string ma = Convert.ToString(dgvTraHang.CurrentRow.Cells["Ma"].Value);
            try
            {
                int n = AiHelper.Chay("DELETE FROM PhanTich_CamXuc_DanhGia WHERE LoaiDoiTuong = @loai AND DoiTuongId = @ma AND GanTay = 1",
                                      AiHelper.P("@loai", LoaiDoiTuong), AiHelper.P("@ma", ma));
                if (n == 0) { AiHelper.BaoTin("Dòng " + ma + " chưa sửa tay nên không cần trả lại."); return; }
                ChamLaiTuDong();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Form_DocCamXucDanhGia_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvTraHang);
            AiHelper.DinhDangLuoi(dgvCSAT);
            cboCamXuc.SelectedIndex = 0;
            TaiTraHang();
            TaiCSAT();
        }

        private void TaiTraHang()
        {
            try
            {
                string sql = DangXemTraHang
                    ? "SELECT th.MaTraHang AS Ma, th.NgayTra AS Ngay, th.MaSP, sp.TenSP, th.LyDoTra AS LoiKhach, th.NhomLyDoChinh AS NhomLyDo, " +
                      "       ISNULL(th.CamXucKhachHang, N'Trung tính') AS CamXuc, CAST(ISNULL(pc.GanTay, 0) AS BIT) AS GanTay " +
                      "FROM TraHang th JOIN SanPham sp ON sp.MaSP = th.MaSP " +
                      "LEFT JOIN PhanTich_CamXuc_DanhGia pc ON pc.LoaiDoiTuong = N'Trả hàng' AND pc.DoiTuongId = th.MaTraHang "
                    : "SELECT d.MaDanhGia AS Ma, d.NgayDanhGia AS Ngay, d.MaSP, sp.TenSP, d.SoSao, d.NoiDung AS LoiKhach, " +
                      "       ISNULL(pc.NhomCamXuc, N'Trung tính') AS CamXuc, CAST(ISNULL(pc.GanTay, 0) AS BIT) AS GanTay " +
                      "FROM DanhGiaSanPham d JOIN SanPham sp ON sp.MaSP = d.MaSP " +
                      "LEFT JOIN PhanTich_CamXuc_DanhGia pc ON pc.LoaiDoiTuong = N'Đánh giá' AND pc.DoiTuongId = d.MaDanhGia ";
                DataTable tatCa = AiHelper.Doc(sql + "ORDER BY Ngay DESC, Ma");

                int khen = 0, trungTinh = 0, che = 0;
                foreach (DataRow r in tatCa.Rows)
                {
                    string cx = Convert.ToString(r["CamXuc"]);
                    if (cx == KHEN) khen++;
                    else if (cx == CHE) che++;
                    else trungTinh++;
                }
                int tong = Math.Max(1, tatCa.Rows.Count);
                lblDanhSach.Text = "Khen " + khen + " lời (" + (100.0 * khen / tong).ToString("0") + "%)  ·  Trung tính " + trungTinh +
                                   " lời (" + (100.0 * trungTinh / tong).ToString("0") + "%)  ·  Chê " + che +
                                   " lời (" + (100.0 * che / tong).ToString("0") + "%)";

                string loc = CamXucDangLoc;
                DataView v = tatCa.DefaultView;
                v.RowFilter = loc.Length == 0 ? "" : "CamXuc = '" + loc + "'";
                dgvTraHang.DataSource = v.ToTable();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void TaiCSAT()
        {
            try
            {
                dgvCSAT.DataSource = AiHelper.Doc(
                    "SELECT sp.MaSP, sp.TenSP, dm.TenDanhMuc, " +
                    "       (SELECT COUNT(*) FROM DanhGiaSanPham d WHERE d.MaSP = sp.MaSP) AS SoDanhGia, " +
                    "       sp.DiemCamXuc_CSAT AS CSAT_PhanTram, " +
                    "       (SELECT COUNT(*) FROM TraHang th WHERE th.MaSP = sp.MaSP) AS SoPhieuTra, " +
                    "       (SELECT COUNT(*) FROM TraHang th WHERE th.MaSP = sp.MaSP AND th.CamXucKhachHang = N'Chê') AS SoLyDoTraBiChe " +
                    "FROM SanPham sp LEFT JOIN DanhMuc dm ON dm.MaDanhMuc = sp.MaDanhMuc " +
                    "ORDER BY CASE WHEN sp.DiemCamXuc_CSAT IS NULL THEN 1 ELSE 0 END, sp.DiemCamXuc_CSAT, sp.MaSP");
                AiHelper.DinhDangSo(dgvCSAT, "CSAT_PhanTram", "N1");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvTraHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string nhan = Convert.ToString(dgvTraHang.Rows[e.RowIndex].Cells["CamXuc"].Value);
            int i = cboCamXuc.FindStringExact(nhan);
            if (i >= 0) cboCamXuc.SelectedIndex = i;
        }

        private void btnGanNhan_Click(object sender, EventArgs e)
        {
            GanNhan(false);
        }

        private void ChonLaiDong(string ma)
        {
            foreach (DataGridViewRow r in dgvTraHang.Rows)
            {
                if (Convert.ToString(r.Cells["Ma"].Value) != ma) continue;
                dgvTraHang.CurrentCell = r.Cells["Ma"];
                return;
            }
        }

        private void GanNhan(bool tuDong)
        {
            if (dgvTraHang.CurrentRow == null) { if (!tuDong) AiHelper.BaoTin("Hãy bấm chọn một dòng."); return; }
            string ma = Convert.ToString(dgvTraHang.CurrentRow.Cells["Ma"].Value);
            string noiDung = Convert.ToString(dgvTraHang.CurrentRow.Cells["LoiKhach"].Value);
            string chon = Convert.ToString(cboCamXuc.SelectedItem);
            if (chon != KHEN && chon != TRUNG_TINH && chon != CHE) { if (!tuDong) AiHelper.BaoTin("Hãy chọn Khen, Trung tính hoặc Chê."); return; }
            if (tuDong && chon == Convert.ToString(dgvTraHang.CurrentRow.Cells["CamXuc"].Value)) return;

            string nhan = chon;
            decimal diem = chon == KHEN ? 1m : (chon == CHE ? -1m : 0m);
            try
            {
                AiHelper.Chay(
                    "SET XACT_ABORT ON; BEGIN TRANSACTION; " +
                    (DangXemTraHang ? "UPDATE TraHang SET CamXucKhachHang = @nhan WHERE MaTraHang = @ma; " : "") +
                    "DELETE FROM PhanTich_CamXuc_DanhGia WHERE LoaiDoiTuong = @loai AND DoiTuongId = @ma; " +
                    "INSERT INTO PhanTich_CamXuc_DanhGia (DoiTuongId, LoaiDoiTuong, DiemCamXuc, NhomCamXuc, NoiDungGoc, GanTay) " +
                    "VALUES (@ma, @loai, @diem, @nhan, @nd, 1); " +
                    "COMMIT TRANSACTION;",
                    AiHelper.P("@ma", ma), AiHelper.P("@loai", LoaiDoiTuong), AiHelper.P("@nhan", nhan),
                    AiHelper.PDec("@diem", diem, 6, 4), AiHelper.P("@nd", noiDung));
                TaiTraHang();
                TaiCSAT();
                ChonLaiDong(ma);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLamSach_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable kq = AiHelper.ChayThuTuc("sp_TuDongLamSach_DuLieuGiaoDich");
                AiHelper.BaoTin("Đã làm sạch xong.\nSố bản ghi đã sửa: " + AiHelper.SoDau(kq, "SoBanGhiDaSua") +
                                "\nSố khách có số điện thoại không đưa về được dạng 10 số: " + AiHelper.SoDau(kq, "SoBanGhiLoi"));
                TaiTraHang();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaiTraHang();
            TaiCSAT();
        }

        private void btnCapNhatCSAT_Click(object sender, EventArgs e)
        {
            ChamLaiTuDong();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
