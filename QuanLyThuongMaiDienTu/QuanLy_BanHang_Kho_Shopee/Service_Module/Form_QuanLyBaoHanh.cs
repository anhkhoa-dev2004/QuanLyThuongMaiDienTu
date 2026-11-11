using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Service_Module
{
    public partial class Form_QuanLyBaoHanh : Form
    {
        private readonly Label lblHanBaoHanh, lblPhieuChon;
        private readonly ComboBox cboKetQua;
        private readonly TextBox txtCachXuLy, txtChiPhi;
        private string _maBH;

        public Form_QuanLyBaoHanh()
        {
            InitializeComponent();
            lblNgayHenTra.Text = "Hẹn trả sau (ngày):";

            lblHanBaoHanh = new Label { Location = new Point(201, 322), Size = new Size(340, 50), ForeColor = Color.FromArgb(30, 39, 56) };
            Label lKetQua = new Label { Text = "TRẢ KẾT QUẢ BẢO HÀNH", Location = new Point(560, 104), Size = new Size(230, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            lblPhieuChon = new Label { Text = "Bấm 1 phiếu ở bảng dưới.", Location = new Point(560, 128), Size = new Size(230, 40) };
            Label l1 = new Label { Text = "Kết quả:", Location = new Point(560, 172), Size = new Size(230, 20) };
            cboKetQua = new ComboBox { Location = new Point(560, 193), Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
            cboKetQua.Items.AddRange(new object[] { "Đã sửa xong, trả khách", "Hãng đổi máy mới, trả khách", "Từ chối bảo hành", "Chuyển sang trả hàng" });
            cboKetQua.SelectedIndex = 0;
            Label l2 = new Label { Text = "Đã xử lý thế nào:", Location = new Point(560, 222), Size = new Size(230, 20) };
            txtCachXuLy = new TextBox { Location = new Point(560, 243), Width = 230, Height = 52, Multiline = true };
            Label l3 = new Label { Text = "Chi phí shop chịu (đ):", Location = new Point(560, 300), Size = new Size(230, 20) };
            txtChiPhi = new TextBox { Location = new Point(560, 321), Width = 230, Text = "0" };
            Button btnTraKetQua = new Button { Text = "Lưu kết quả, trả khách", Location = new Point(560, 352), Size = new Size(230, 30) };
            btnTraKetQua.Click += (s, e) => TraKetQua();
            Controls.AddRange(new Control[] { lblHanBaoHanh, lKetQua, lblPhieuChon, l1, cboKetQua, l2, txtCachXuLy, l3, txtChiPhi, btnTraKetQua });
            txtMaDonHang.Leave += (s, e) => HienHanBaoHanh();
            txtMaSanPham.Leave += (s, e) => HienHanBaoHanh();

            GiaoDien.GanChoForm(this);
            GiaoDien.ThemBangDuoi(this, "Phiếu bảo hành (phiếu đang xử lý xếp trên cùng)", 250,
                () => AiHelper.Doc(
                    "SELECT TOP 300 bh.MaBaoHanh AS [Mã bảo hành], bh.MaDH AS [Mã đơn], bh.MaSP AS [Mã SP], sp.TenSP AS [Sản phẩm], " +
                    "       bh.LyDoHong AS [Lỗi khách báo], bh.NgayNhan AS [Ngày nhận], bh.NgayHenTra AS [Hẹn trả], bh.TrangThai AS [Tình trạng], " +
                    "       bh.CachXuLy AS [Cách xử lý], bh.NgayTraKhach AS [Ngày trả khách], bh.ChiPhiShopChiu AS [Shop chịu (đ)], " +
                    "       CASE WHEN bh.TrangThai = N'Đang xử lý' AND bh.NgayHenTra < GETDATE() THEN N'Trễ hẹn ' + CAST(DATEDIFF(DAY, bh.NgayHenTra, GETDATE()) AS NVARCHAR(10)) + N' ngày' " +
                    "            WHEN bh.TrangThai = N'Đang xử lý' THEN N'Chưa tới hẹn' ELSE N'' END AS [Hẹn trả khách] " +
                    "FROM BaoHanh bh LEFT JOIN SanPham sp ON sp.MaSP = bh.MaSP " +
                    "ORDER BY CASE WHEN bh.TrangThai = N'Đang xử lý' THEN 0 ELSE 1 END, bh.NgayNhan DESC"),
                r =>
                {
                    txtMaDonHang.Text = Convert.ToString(r.Cells["Mã đơn"].Value);
                    txtMaSanPham.Text = Convert.ToString(r.Cells["Mã SP"].Value);
                    _maBH = Convert.ToString(r.Cells["Mã bảo hành"].Value);
                    string tt = Convert.ToString(r.Cells["Tình trạng"].Value);
                    lblPhieuChon.Text = "Phiếu " + _maBH + ": " + tt + (tt == "Đang xử lý" ? "" : " (đã có kết quả)");
                    HienHanBaoHanh();
                });
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void lblMaSanPham_Click(object sender, EventArgs e)
        {
        }

        private void txtMaSanPham_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblMaDonHang_Click(object sender, EventArgs e)
        {
        }

        private void Form_QuanLyBaoHanh_Load(object sender, EventArgs e)
        {
            txtNgayHenTra.Text = "14";
        }

        private void txtMaDonHang_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblLyDoHong_Click(object sender, EventArgs e)
        {
        }

        private void txtLyDoHong_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblNgayHenTra_Click(object sender, EventArgs e)
        {
        }

        private void txtNgayHenTra_TextChanged(object sender, EventArgs e)
        {
        }

        private void HienHanBaoHanh()
        {
            string maSP = txtMaSanPham.Text.Trim(), maDH = txtMaDonHang.Text.Trim();
            if (maSP.Length == 0 || maDH.Length == 0) { lblHanBaoHanh.Text = ""; return; }
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT dh.NgayGiao, dh.TrangThai, sp.SoThangBaoHanh, sp.TenSP FROM DonHang dh " +
                    "JOIN ChiTietDonHang ct ON ct.MaDH = dh.MaDH JOIN SanPham sp ON sp.MaSP = ct.MaSP WHERE dh.MaDH = @d AND ct.MaSP = @s",
                    AiHelper.P("@d", maDH), AiHelper.P("@s", maSP));
                if (t.Rows.Count == 0) { lblHanBaoHanh.Text = "Đơn này không có sản phẩm " + maSP + "."; return; }
                DataRow r = t.Rows[0];
                if (r["NgayGiao"] == DBNull.Value) { lblHanBaoHanh.Text = "Đơn đang \"" + r["TrangThai"] + "\", chưa giao tới khách."; return; }
                DateTime giao = Convert.ToDateTime(r["NgayGiao"]);
                int thang = Convert.ToInt32(r["SoThangBaoHanh"]);
                DateTime het = giao.AddMonths(thang);
                lblHanBaoHanh.Text = r["TenSP"] + ": giao " + giao.ToString("dd/MM/yyyy") + ", bảo hành " + thang + " tháng, hết hạn " + het.ToString("dd/MM/yyyy") +
                                     (het < DateTime.Now ? " - ĐÃ HẾT HẠN." : " - còn " + (het - DateTime.Now).Days + " ngày.");
            }
            catch (Exception ex) { lblHanBaoHanh.Text = "Chưa xem được hạn bảo hành: " + AiHelper.LoiDeHieu(ex); }
        }

        private void btnNhanBaoHanh_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSanPham.Text.Trim();
            string maDH = txtMaDonHang.Text.Trim();
            string lyDo = txtLyDoHong.Text.Trim();

            if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(maDH))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã sản phẩm và Mã đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int soNgayHen;
            if (!int.TryParse(txtNgayHenTra.Text.Trim(), out soNgayHen) || soNgayHen < 1 || soNgayHen > 90)
            {
                MessageBox.Show("Số ngày hẹn trả phải từ 1 đến 90.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maBaoHanh = AiHelper.ChayThuTucLayMa("sp_NhanBaoHanh", "@MaBH",
                    AiHelper.P("@MaDH", maDH), AiHelper.P("@MaSP", maSP), AiHelper.P("@LyDoHong", lyDo), AiHelper.P("@SoNgayHen", soNgayHen));
                MessageBox.Show("Đã nhận bảo hành.\nMã phiếu: " + maBaoHanh + "\nHẹn trả khách: " + DateTime.Now.AddDays(soNgayHen).ToString("dd/MM/yyyy"),
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XoaForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa nhận bảo hành được: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TraKetQua()
        {
            if (string.IsNullOrEmpty(_maBH)) { AiHelper.BaoTin("Hãy bấm chọn một phiếu bảo hành ở bảng dưới."); return; }
            decimal chiPhi;
            if (!AiHelper.DocSoTien(txtChiPhi.Text, out chiPhi) || chiPhi < 0) { AiHelper.BaoTin("Chi phí shop chịu phải là số không âm (ví dụ tiền gửi hàng đi hãng)."); return; }
            if (MessageBox.Show("Lưu kết quả \"" + cboKetQua.SelectedItem + "\" cho phiếu " + _maBH + "?", "Trả kết quả bảo hành",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                AiHelper.ChayThuTucLayMa("sp_TraKetQuaBaoHanh", null, AiHelper.P("@MaBH", _maBH), AiHelper.P("@KetQua", Convert.ToString(cboKetQua.SelectedItem)),
                                         AiHelper.P("@CachXuLy", txtCachXuLy.Text.Trim()), AiHelper.PDec("@ChiPhiShopChiu", chiPhi, 18, 2));
                AiHelper.BaoTin("Đã lưu kết quả bảo hành phiếu " + _maBH + ".");
                _maBH = null;
                lblPhieuChon.Text = "Bấm 1 phiếu ở bảng dưới.";
                txtCachXuLy.Clear();
                txtChiPhi.Text = "0";
                GiaoDien.TaiLai(this);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XoaForm()
        {
            GiaoDien.TaiLai(this);
            txtMaSanPham.Clear();
            txtMaDonHang.Clear();
            txtLyDoHong.Clear();
            txtNgayHenTra.Text = "14";
            lblHanBaoHanh.Text = "";
            txtMaSanPham.Focus();
        }
    }
}
