using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Service_Module
{
    public partial class Form_DanhGiaKhachHang : Form
    {
        private string _maDH, _maKH;
        private DataTable _ctDon;
        private bool _coKhaoSat;

        public Form_DanhGiaKhachHang()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            AiHelper.DinhDangLuoi(dgvDanhGia);
            cboSoSao.SelectedIndexChanged += (s, e) => HienCamXuc();
            txtLoiKhach.TextChanged += (s, e) => HienLyDoMua();
        }

        private static readonly Color MauVui = Color.FromArgb(30, 132, 73);
        private static readonly Color MauBuon = Color.FromArgb(192, 57, 43);
        private static readonly Color MauThuong = Color.FromArgb(110, 110, 110);

        private void HienCamXuc()
        {
            int sao = cboSoSao.SelectedIndex;
            string loi = txtNoiDung.Text.Trim();
            if (sao < 1 && loi.Length == 0) { lblCamXuc.Text = ""; return; }
            try
            {
                string theoSao = sao >= 1 ? CamXucTuDien.CamXucTheoSao(sao) : "chưa chọn sao";
                string theoLoi = "chưa có lời";
                if (loi.Length > 0)
                {
                    decimal diem;
                    CamXucTuDien.Cham(loi, out diem, out theoLoi);
                }
                bool lech = CamXucTuDien.SaoCaoMaLoiKhongKhen(sao, loi, theoLoi);
                lblCamXuc.Text = "Sao: " + theoSao + " | Lời: " + theoLoi + (lech ? " | Sao cao mà lời không khen" : "");
                bool buon = theoLoi == "Chê" || (sao >= 1 && sao <= 2);
                bool vui = !buon && !lech && (theoLoi == "Khen" || sao >= 4);
                lblCamXuc.ForeColor = buon || lech ? MauBuon : (vui ? MauVui : MauThuong);
            }
            catch (Exception ex) { lblCamXuc.Text = "Chưa đọc được: " + AiHelper.LoiDeHieu(ex); }
        }

        private void HienLyDoMua()
        {
            string loi = txtLoiKhach.Text.Trim();
            if (loi.Length == 0) { lblKhaoSat.Text = _coKhaoSat ? "Đơn này đã có khảo sát." : "Đơn này chưa có khảo sát."; return; }
            try
            {
                int soTu;
                string nhom = CamXucTuDien.XepLyDoMua(loi, out soTu);
                string kieu;
                string cachBan = CamXucTuDien.CachBan(nhom, out kieu);
                lblKhaoSat.Text = "Khách mua vì: " + nhom + (kieu.Length > 0 ? " (" + kieu + ")" : "") + "\nCách bán hợp: " + cachBan;
                lblKhaoSat.ForeColor = kieu == "Cảm xúc" ? MauVui : MauThuong;
            }
            catch (Exception ex) { lblKhaoSat.Text = "Chưa xếp nhóm được: " + AiHelper.LoiDeHieu(ex); }
        }

        private void Form_DanhGiaKhachHang_Load(object sender, EventArgs e)
        {
            DatTrangThaiTrong();
            TaiDanhSach();
        }

        private void DatTrangThaiTrong()
        {
            _maDH = null; _maKH = null; _ctDon = null; _coKhaoSat = false;
            cboSanPham.DataSource = null;
            cboSanPham.Items.Clear();
            cboSoSao.SelectedIndex = 0;
            txtNoiDung.Clear();
            txtLoiKhach.Clear();
            cboHaoHung.SelectedIndex = 0;
            lblKhaoSat.Text = "";
            lblCamXuc.Text = "";
            btnLuuDanhGia.Text = "Lưu đánh giá";
            btnLuuDanhGia.Enabled = false;
            btnLuuKhaoSat.Enabled = false;
        }

        private void TaiDanhSach()
        {
            try
            {
                dgvDanhGia.DataSource = AiHelper.Doc(
                    "SELECT TOP 200 dg.NgayDanhGia AS [Ngày đánh giá], dg.MaDH AS [Mã đơn], dg.MaSP AS [Mã SP], sp.TenSP AS [Sản phẩm], " +
                    "       dg.MaKH AS [Mã khách], dg.SoSao AS [Số sao], dg.NoiDung AS [Lời đánh giá], pc.NhomCamXuc AS [Khen / chê] " +
                    "FROM DanhGiaSanPham dg JOIN SanPham sp ON sp.MaSP = dg.MaSP " +
                    "LEFT JOIN PhanTich_CamXuc_DanhGia pc ON pc.LoaiDoiTuong = N'Đánh giá' AND pc.DoiTuongId = dg.MaDanhGia " +
                    "ORDER BY dg.NgayDanhGia DESC, dg.MaDanhGia DESC");
                AiHelper.DinhDangSo(dgvDanhGia, "Ngày đánh giá", "dd/MM/yyyy HH:mm");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvDanhGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDanhGia.Rows.Count) return;
            string maDH = Convert.ToString(dgvDanhGia.Rows[e.RowIndex].Cells["Mã đơn"].Value);
            string maSP = Convert.ToString(dgvDanhGia.Rows[e.RowIndex].Cells["Mã SP"].Value);
            txtMaDonHang.Text = maDH;
            MoDon();
            ChonLaiSanPham(maSP);
        }

        private void ChonLaiSanPham(string maSP)
        {
            if (_ctDon == null) return;
            for (int i = 0; i < _ctDon.Rows.Count; i++)
                if (Convert.ToString(_ctDon.Rows[i]["MaSP"]) == maSP) { cboSanPham.SelectedIndex = i; return; }
        }

        private void txtMaDonHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            MoDon();
        }

        private void btnMoDon_Click(object sender, EventArgs e)
        {
            MoDon();
        }

        private void MoDon()
        {
            DatTrangThaiTrong();
            string ma = txtMaDonHang.Text.Trim();
            if (ma.Length == 0) { lblThongTinDon.Text = "Hãy nhập mã đơn hàng."; return; }
            try
            {
                DataTable d = AiHelper.Doc(
                    "SELECT dh.MaDH, dh.MaKH, kh.TenKH, dh.NgayDat, dh.TrangThai FROM DonHang dh " +
                    "LEFT JOIN KhachHang kh ON kh.MaKH = dh.MaKH WHERE dh.MaDH = @d", AiHelper.P("@d", ma));
                if (d.Rows.Count == 0) { lblThongTinDon.Text = "Không tìm thấy đơn \"" + ma + "\"."; return; }
                DataRow r = d.Rows[0];
                string tt = Convert.ToString(r["TrangThai"]);
                string kh = r["MaKH"] == DBNull.Value ? "(không có mã khách)" : r["MaKH"] + " - " + r["TenKH"];
                lblThongTinDon.Text = "Khách: " + kh + "  |  Ngày đặt: " + Convert.ToDateTime(r["NgayDat"]).ToString("dd/MM/yyyy HH:mm") + "  |  Trạng thái: " + tt;
                if (tt.ToLower().Contains("hủy")) { lblThongTinDon.Text += "\nĐơn đã hủy nên không ghi đánh giá và khảo sát."; return; }
                if (tt != "Đã giao" && tt != "Trả hàng/Hoàn tiền") { lblThongTinDon.Text += "\nKhách chưa nhận hàng nên chưa đánh giá được (giống Shopee: chỉ đánh giá sau khi đơn đã giao)."; return; }

                _maDH = Convert.ToString(r["MaDH"]);
                _maKH = r["MaKH"] == DBNull.Value ? null : Convert.ToString(r["MaKH"]);

                _ctDon = AiHelper.Doc(
                    "SELECT ct.MaSP, ct.MaSP + N' - ' + sp.TenSP + CASE WHEN dg.MaDanhGia IS NULL THEN N'' ELSE N'  (đã đánh giá)' END AS Hien, " +
                    "       dg.MaDanhGia, dg.SoSao, dg.NoiDung " +
                    "FROM ChiTietDonHang ct JOIN SanPham sp ON sp.MaSP = ct.MaSP " +
                    "LEFT JOIN DanhGiaSanPham dg ON dg.MaDH = ct.MaDH AND dg.MaSP = ct.MaSP " +
                    "WHERE ct.MaDH = @d ORDER BY ct.MaSP", AiHelper.P("@d", _maDH));
                cboSanPham.DisplayMember = "Hien";
                cboSanPham.ValueMember = "MaSP";
                cboSanPham.DataSource = _ctDon;
                btnLuuDanhGia.Enabled = _ctDon.Rows.Count > 0;
                ChonSanPham();

                DataTable ks = AiHelper.Doc(
                    "SELECT k.LoiKhachNoi, k.MucHaoHung, p.NhomLyDo FROM KhaoSat_LyDoMua k " +
                    "LEFT JOIN PhanTich_LyDoMua p ON p.KhaoSatId = k.Id WHERE k.MaDH = @d", AiHelper.P("@d", _maDH));
                _coKhaoSat = ks.Rows.Count > 0;
                if (_coKhaoSat)
                {
                    DataRow k = ks.Rows[0];
                    txtLoiKhach.Text = Convert.ToString(k["LoiKhachNoi"]);
                    cboHaoHung.SelectedIndex = k["MucHaoHung"] == DBNull.Value ? 6 : Convert.ToInt32(k["MucHaoHung"]);
                    lblKhaoSat.Text = "Đơn này đã có khảo sát" + (k["NhomLyDo"] == DBNull.Value ? "" : ", đang xếp vào nhóm: " + k["NhomLyDo"]) + ". Sửa rồi bấm Lưu để cập nhật.";
                }
                else lblKhaoSat.Text = "Đơn này chưa có khảo sát.";
                btnLuuKhaoSat.Enabled = true;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private DataRow DongDangChon()
        {
            if (_ctDon == null || cboSanPham.SelectedIndex < 0 || cboSanPham.SelectedIndex >= _ctDon.Rows.Count) return null;
            return _ctDon.Rows[cboSanPham.SelectedIndex];
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChonSanPham();
        }

        private void ChonSanPham()
        {
            DataRow r = DongDangChon();
            if (r == null) return;
            if (r["MaDanhGia"] == DBNull.Value)
            {
                cboSoSao.SelectedIndex = 0;
                txtNoiDung.Clear();
                btnLuuDanhGia.Text = "Lưu đánh giá";
            }
            else
            {
                cboSoSao.SelectedIndex = Convert.ToInt32(r["SoSao"]);
                txtNoiDung.Text = Convert.ToString(r["NoiDung"]);
                btnLuuDanhGia.Text = "Sửa đánh giá";
            }
        }

        private void txtNoiDung_TextChanged(object sender, EventArgs e)
        {
            HienCamXuc();
        }

        private void btnLuuDanhGia_Click(object sender, EventArgs e)
        {
            if (_maDH == null) { AiHelper.BaoTin("Hãy mở một đơn hàng chưa hủy trước."); return; }
            DataRow r = DongDangChon();
            if (r == null) { AiHelper.BaoTin("Hãy chọn sản phẩm cần đánh giá."); return; }
            if (cboSoSao.SelectedIndex < 1) { AiHelper.BaoTin("Hãy chọn số sao khách chấm (1 đến 5)."); return; }
            int sao = cboSoSao.SelectedIndex;
            string nd = txtNoiDung.Text.Trim();
            string maSP = Convert.ToString(r["MaSP"]);
            string maDG = r["MaDanhGia"] == DBNull.Value ? null : Convert.ToString(r["MaDanhGia"]);
            try
            {
                SqlParameter pNd = new SqlParameter("@nd", SqlDbType.NVarChar, 1000) { Value = nd.Length == 0 ? (object)DBNull.Value : nd };
                if (maDG == null)
                {
                    maDG = AiHelper.LayMaMoi("DG");
                    AiHelper.Chay(
                        "INSERT INTO DanhGiaSanPham (MaDanhGia, MaSP, MaDH, MaKH, SoSao, NoiDung, NgayDanhGia) VALUES (@m, @sp, @dh, @kh, @sao, @nd, GETDATE())",
                        AiHelper.P("@m", maDG), AiHelper.P("@sp", maSP), AiHelper.P("@dh", _maDH), AiHelper.P("@kh", _maKH),
                        AiHelper.P("@sao", sao), pNd);
                }
                else
                {
                    AiHelper.Chay("UPDATE DanhGiaSanPham SET SoSao = @sao, NoiDung = @nd WHERE MaDanhGia = @m",
                                  AiHelper.P("@sao", sao), pNd, AiHelper.P("@m", maDG));
                }

                string nhan = "";
                try
                {
                    AiHelper.ChayThuTuc("sp_PhanTichCamXuc_DanhGia", AiHelper.P("@HienKetQua", false));
                    object n = AiHelper.LayGiaTri("SELECT NhomCamXuc FROM PhanTich_CamXuc_DanhGia WHERE LoaiDoiTuong = N'Đánh giá' AND DoiTuongId = @m", AiHelper.P("@m", maDG));
                    if (n != null) nhan = "\nLời đánh giá được chấm là: " + n;
                }
                catch (Exception ex2) { nhan = "\n(Chưa chấm khen/chê được: " + ex2.Message + ")"; }

                AiHelper.BaoTin("Đã lưu đánh giá " + sao + " sao cho " + maSP + " trong đơn " + _maDH + "." + nhan);
                MoDon();
                ChonLaiSanPham(maSP);
                TaiDanhSach();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLuuKhaoSat_Click(object sender, EventArgs e)
        {
            if (_maDH == null) { AiHelper.BaoTin("Hãy mở một đơn hàng chưa hủy trước."); return; }
            string loi = txtLoiKhach.Text.Trim();
            if (loi.Length == 0) { AiHelper.BaoTin("Hãy ghi câu khách trả lời."); return; }
            if (cboHaoHung.SelectedIndex < 1) { AiHelper.BaoTin("Hãy chọn mức hào hứng (hoặc \"Không trả lời\")."); return; }
            object hh = cboHaoHung.SelectedIndex == 6 ? (object)DBNull.Value : cboHaoHung.SelectedIndex;
            try
            {
                if (_coKhaoSat)
                    AiHelper.Chay("UPDATE KhaoSat_LyDoMua SET LoiKhachNoi = @l, MucHaoHung = @h WHERE MaDH = @d",
                                  AiHelper.P("@l", loi), AiHelper.P("@h", hh), AiHelper.P("@d", _maDH));
                else
                    AiHelper.Chay("INSERT INTO KhaoSat_LyDoMua (MaDH, MaKH, NgayTraLoi, LoiKhachNoi, MucHaoHung) VALUES (@d, @kh, GETDATE(), @l, @h)",
                                  AiHelper.P("@d", _maDH), AiHelper.P("@kh", _maKH), AiHelper.P("@l", loi), AiHelper.P("@h", hh));

                string nhom = "";
                try
                {
                    AiHelper.ChayThuTuc("sp_PhanTichLyDoMua", AiHelper.P("@HienKetQua", false));
                    object n = AiHelper.LayGiaTri("SELECT p.NhomLyDo FROM PhanTich_LyDoMua p JOIN KhaoSat_LyDoMua k ON k.Id = p.KhaoSatId WHERE k.MaDH = @d",
                                                  AiHelper.P("@d", _maDH));
                    if (n != null) nhom = "\nCâu trả lời được xếp vào nhóm: " + n;
                }
                catch (Exception ex2) { nhom = "\n(Chưa xếp nhóm được: " + ex2.Message + ")"; }

                AiHelper.BaoTin("Đã lưu khảo sát cho đơn " + _maDH + "." + nhom);
                MoDon();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
