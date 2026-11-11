using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    public partial class Form_BangThongBao : Form
    {
        private static readonly Dictionary<string, Color> MauTrangThai = new Dictionary<string, Color>
        {
            { "ĐỎ", Color.FromArgb(248, 215, 218) }, { "VÀNG", Color.FromArgb(255, 243, 205) }, { "XANH", Color.FromArgb(214, 239, 220) },
            { "KHẨN CẤP", Color.FromArgb(248, 215, 218) }, { "ĐẶT HÀNG NGAY", Color.FromArgb(255, 243, 205) },
            { "TỒN NHIỀU", Color.FromArgb(217, 232, 247) }, { "ỔN", Color.FromArgb(214, 239, 220) },
            { "KHÔNG BÁN", Color.FromArgb(233, 236, 239) }, { "CHƯA CÓ ĐÁNH GIÁ", Color.FromArgb(233, 236, 239) }
        };

        private static readonly Dictionary<string, string> TenCot = new Dictionary<string, string>
        {
            { "MaSP", "Mã" }, { "TenSP", "Sản phẩm" }, { "TenNCC", "Nhà cung cấp" }, { "TonHienTai", "Tồn" },
            { "BanTB_Ngay", "Bán TB/ngày" }, { "ThayDoi30Ngay_PhanTram", "Thay đổi 30 ngày %" }, { "HangVe_Ngay", "Hàng về (ngày)" },
            { "TonAnToan", "Tồn dự phòng" }, { "ROP", "Mức phải đặt hàng" }, { "DuBan_Ngay", "Đủ bán (ngày)" },
            { "TrangThai", "Tình trạng" }, { "ThongBao", "Thông báo" }, { "SoSanPham", "Số sản phẩm" },
            { "SoDanhGia", "Số đánh giá" }, { "SaoTB", "Sao TB" }, { "CSAT", "% khách hài lòng" }, { "TyLeChuChe", "Lời chê %" },
            { "TyLeTra", "Trả hàng %" }, { "LyDoTraNhieuNhat", "Lý do trả nhiều nhất" }, { "TenDanhMuc", "Danh mục" },
            { "BanSale", "Bán ngày sale (cái)" }, { "Lift", "Gấp (lần)" }, { "TraSalePT", "Trả ngày sale %" },
            { "TraThuongPT", "Trả ngày thường %" }, { "KieuMua", "Kiểu mua" }, { "CauHoi", "Câu hỏi" }, { "SoDo", "Số đo" },
            { "KetLuan", "Kết luận" }, { "Nhom", "Nhóm" }, { "SoLuotDanhGia", "Số lượt đánh giá" },
            { "SoLuotMuaLai", "Số lượt mua lại" }, { "TyLeMuaLai", "Mua lại %" }, { "NhomLyDo", "Nhóm lý do" },
            { "SoCau", "Số câu" }, { "TyLe", "Tỷ lệ %" }, { "HaoHung", "Hào hứng TB (1-5)" }, { "GiaTriDon", "Giá trị đơn TB (đ)" },
            { "TraHangPT", "Đơn có trả hàng %" }, { "TraHangPT_NhomKhac", "Các nhóm còn lại %" },
            { "Loai", "Việc" }, { "NoiDung", "Nội dung" }, { "XuHuongBan", "Xu hướng bán" },
            { "DuDoan30Ngay", "Dự đoán bán 30 ngày tới (cái)" }, { "SoNenNhap", "Nên nhập thêm (cái)" },
            { "MonHayMuaChung", "Hay mua chung với" }, { "ViecNenLam", "Nên làm gì" }
        };

        private static readonly HashSet<string> CotDai = new HashSet<string> { "ThongBao", "KetLuan", "CauHoi", "NoiDung", "ViecNenLam" };
        private readonly Dictionary<DataGridView, HashSet<string>> _cotAn = new Dictionary<DataGridView, HashSet<string>>();

        public Form_BangThongBao()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            foreach (DataGridView g in new[] { dgvViecCanLam, dgvLyDoMua, dgvCamXucSanPham, dgvCamXucDanhMuc, dgvMuaHungSanPham,
                                               dgvMuaHungCaShop, dgvMuaLai, dgvCungCauTong, dgvCungCauSanPham })
            {
                AiHelper.DinhDangLuoi(g);
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            _cotAn[dgvLyDoMua] = new HashSet<string> { "ThongBao" };
            _cotAn[dgvCamXucSanPham] = new HashSet<string> { "MaDanhMuc", "SLXuat", "SLTra", "CSAT_DanhMuc" };
            _cotAn[dgvCamXucDanhMuc] = new HashSet<string> { "MaDanhMuc" };
            _cotAn[dgvMuaHungSanPham] = new HashSet<string> { "BanThuong", "TraSale", "TraThuong", "BanTB_Sale", "BanTB_Thuong" };
            _cotAn[dgvMuaHungCaShop] = new HashSet<string> { "Thu" };
            _cotAn[dgvCungCauSanPham] = new HashSet<string> { "HangVe_LechChuan" };
        }

        private void Form_BangThongBao_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(NapTatCa));
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            NapTatCa();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static DataSet Chay(string tenThuTuc, List<string> loi, params SqlParameter[] ps)
        {
            try { return AiHelper.ChayThuTucNhieuBang(tenThuTuc, ps); }
            catch (Exception ex) { loi.Add(tenThuTuc + ": " + AiHelper.LoiDeHieu(ex)); return null; }
        }

        private static DataTable Bang(DataSet ds, int i)
        {
            return ds != null && ds.Tables.Count > i ? ds.Tables[i] : null;
        }

        private void NapTatCa()
        {
            Cursor = Cursors.WaitCursor;
            btnCapNhat.Enabled = false;
            lblTrangThai.Text = "Đang tính, vui lòng chờ...";
            lblTrangThai.Refresh();
            List<string> loi = new List<string>();
            try
            {
                object denNgay = dtpDenNgay.Checked ? (object)dtpDenNgay.Value.Date : null;
                try { AiHelper.ChuanBiSoLieuThongBao(); }
                catch (Exception ex) { loi.Add("Chuẩn bị số liệu: " + AiHelper.LoiDeHieu(ex)); }

                DataSet lyDo = Chay("sp_BangThongBao_LyDoMua", loi);
                DataSet camXuc = Chay("sp_BangThongBao_CamXuc", loi);
                DataSet muaHung = Chay("sp_BangThongBao_MuaTheoCamXuc", loi, AiHelper.P("@DenNgay", denNgay));
                DataSet cungCau = Chay("sp_BangThongBao_KhoHang", loi, AiHelper.P("@DenNgay", denNgay));

                dgvLyDoMua.DataSource = Bang(lyDo, 0);
                dgvCamXucSanPham.DataSource = Bang(camXuc, 0);
                dgvCamXucDanhMuc.DataSource = Bang(camXuc, 1);
                dgvMuaHungSanPham.DataSource = Bang(muaHung, 0);
                dgvMuaHungCaShop.DataSource = Bang(muaHung, 1);
                dgvMuaLai.DataSource = Bang(muaHung, 2);
                dgvCungCauTong.DataSource = Bang(cungCau, 0);
                dgvCungCauSanPham.DataSource = Bang(cungCau, 1);
                dgvViecCanLam.DataSource = LapViecCanChuY(lyDo, camXuc, muaHung, cungCau);

                int soDo = DemTrangThai(Bang(camXuc, 0), "ĐỎ");
                int soDat = DemTrangThai(Bang(cungCau, 1), "KHẨN CẤP") + DemTrangThai(Bang(cungCau, 1), "ĐẶT HÀNG NGAY");
                lblTrangThai.Text = "Cập nhật lúc " + DateTime.Now.ToString("HH:mm:ss") + ". Sản phẩm khách không hài lòng (ĐỎ): " + soDo +
                                    ". Sản phẩm cần đặt hàng: " + soDat + "." + (loi.Count > 0 ? " Có " + loi.Count + " phần chưa tính được." : "");
                if (loi.Count > 0) AiHelper.BaoLoi(new Exception("Một số phần chưa tính được:\n\n" + string.Join("\n\n", loi)));
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
            finally
            {
                btnCapNhat.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private static int DemTrangThai(DataTable t, string tt)
        {
            if (t == null || !t.Columns.Contains("TrangThai")) return 0;
            int n = 0;
            foreach (DataRow r in t.Rows) if (Convert.ToString(r["TrangThai"]) == tt) n++;
            return n;
        }

        private static DataTable LapViecCanChuY(DataSet lyDo, DataSet camXuc, DataSet muaHung, DataSet cungCau)
        {
            DataTable viec = new DataTable();
            viec.Columns.Add("Loai", typeof(string));
            viec.Columns.Add("NoiDung", typeof(string));

            DataTable cc = Bang(cungCau, 1);
            if (cc != null && cc.Columns.Contains("TrangThai"))
                foreach (DataRow r in cc.Rows)
                {
                    string tt = Convert.ToString(r["TrangThai"]);
                    if (tt == "KHẨN CẤP" || tt == "ĐẶT HÀNG NGAY") viec.Rows.Add("Đặt hàng", r["TenSP"] + ". " + r["ViecNenLam"]);
                }
            if (cc != null && cc.Columns.Contains("XuHuongBan"))
                foreach (DataRow r in cc.Rows)
                {
                    string tt = Convert.ToString(r["TrangThai"]);
                    if (Convert.ToString(r["XuHuongBan"]) == "ĐANG HOT - bán nhanh lên" && tt != "KHẨN CẤP" && tt != "ĐẶT HÀNG NGAY")
                        viec.Rows.Add("Đang hot", r["TenSP"] + ". " + r["ViecNenLam"]);
                }
            DataTable cx = Bang(camXuc, 0);
            if (cx != null && cx.Columns.Contains("TrangThai"))
                foreach (DataRow r in cx.Rows)
                    if (Convert.ToString(r["TrangThai"]) == "ĐỎ") viec.Rows.Add("Khách không hài lòng", r["TenSP"] + ". " + r["ThongBao"]);
            DataTable ld = Bang(lyDo, 0);
            if (ld != null && ld.Rows.Count > 0)
                viec.Rows.Add("Khách mua nhiều nhất vì", ld.Rows[0]["NhomLyDo"] + " (" + Convert.ToDouble(ld.Rows[0]["TyLe"]).ToString("N1") + "% câu trả lời khảo sát).");
            DataTable toan = Bang(muaHung, 1);
            if (toan != null && toan.Columns.Contains("KetLuan"))
                foreach (DataRow r in toan.Rows)
                {
                    string kl = Convert.ToString(r["KetLuan"]);
                    if (kl.StartsWith("Khác biệt có thật")) viec.Rows.Add(Convert.ToString(r["CauHoi"]), r["SoDo"] + ". " + kl);
                }
            if (viec.Rows.Count == 0) viec.Rows.Add("", "Chưa có việc gấp.");
            return viec;
        }

        private void LuoiThongBao_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView g = (DataGridView)sender;
            HashSet<string> an;
            _cotAn.TryGetValue(g, out an);
            foreach (DataGridViewColumn c in g.Columns)
            {
                if (an != null && an.Contains(c.Name)) { c.Visible = false; continue; }
                string ten;
                if (TenCot.TryGetValue(c.Name, out ten)) c.HeaderText = ten;
                Type t = c.ValueType;
                if (t == typeof(double) || t == typeof(decimal) || t == typeof(float))
                    c.DefaultCellStyle.Format = c.Name == "GiaTriDon" ? "N0" : "N1";
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                if (CotDai.Contains(c.Name))
                {
                    c.Width = 340;
                    c.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
                else
                {
                    int rong = TextRenderer.MeasureText(c.HeaderText, g.ColumnHeadersDefaultCellStyle.Font ?? g.Font).Width + 20;
                    int toiDa = c.Name == "TenSP" || c.Name == "NhomLyDo" || c.Name == "LyDoTraNhieuNhat" ? 190 : 140;
                    c.Width = Math.Max(60, Math.Min(toiDa, rong));
                }
            }
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void LuoiThongBao_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView g = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null) return;
            if (g.Columns[e.ColumnIndex].Name != "TrangThai") return;
            Color mau;
            if (MauTrangThai.TryGetValue(Convert.ToString(e.Value), out mau))
            {
                e.CellStyle.BackColor = mau;
                e.CellStyle.SelectionBackColor = mau;
            }
        }
    }
}
