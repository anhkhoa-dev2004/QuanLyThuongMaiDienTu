using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_ThuGiaBan : Form
    {
        private double? _ped;

        public Form_ThuGiaBan()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            GiaoDien.TuChayKhiDoi(() => btnTinh_Click(btnTinh, EventArgs.Empty), cboSanPham, dtpTuNgay, dtpDenNgay);
        }

        private void Form_ThuGiaBan_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvTheoThang);
            btnLuuPED.Enabled = false;
            try
            {
                DataTable sp = AiHelper.Doc("SELECT MaSP, MaSP + N' - ' + TenSP AS Hien FROM SanPham ORDER BY MaSP");
                AiHelper.NapCombo(cboSanPham, sp, "MaSP", "Hien");
                DateTime moiNhat = AiHelper.NgayDuLieuMoiNhat();
                dtpDenNgay.Value = moiNhat;
                dtpTuNgay.Value = moiNhat.AddYears(-1);
                if (cboSanPham.SelectedValue != null) btnTinh_Click(btnTinh, EventArgs.Empty);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            _ped = null; btnLuuPED.Enabled = false; lblKetQua.Text = "";
            if (cboSanPham.SelectedValue == null) return;
            string ma = cboSanPham.SelectedValue.ToString();
            try
            {
                DataTable gia = AiHelper.Doc("SELECT GiaBan, DoCoGiang_PED FROM SanPham WHERE MaSP = @sp", AiHelper.P("@sp", ma));
                lblGiaHienTai.Text = "Giá bán hiện tại: " + Convert.ToDecimal(gia.Rows[0]["GiaBan"]).ToString("N0") + " đ" +
                    "   |   Mức nhạy giá đã lưu: " + (gia.Rows[0]["DoCoGiang_PED"] == DBNull.Value ? "chưa có" : Convert.ToDecimal(gia.Rows[0]["DoCoGiang_PED"]).ToString("0.0000"));

                DataTable dt = AiHelper.Doc(
                    "SELECT CONVERT(CHAR(7), dh.NgayDat, 120) AS Thang, " +
                    "       SUM(ct.SoLuong * ct.DonGia) / NULLIF(SUM(ct.SoLuong), 0) AS GiaTrungBinh, SUM(ct.SoLuong) AS SoLuongBan " +
                    "FROM ChiTietDonHang ct JOIN DonHang dh ON dh.MaDH = ct.MaDH " +
                    "WHERE ct.MaSP = @sp AND dh.NgayDat >= @tu AND dh.NgayDat < @den AND ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%' " +
                    "GROUP BY CONVERT(CHAR(7), dh.NgayDat, 120) ORDER BY Thang",
                    AiHelper.P("@sp", ma), AiHelper.P("@tu", dtpTuNgay.Value.Date), AiHelper.P("@den", dtpDenNgay.Value.Date.AddDays(1)));
                dgvTheoThang.DataSource = dt;
                AiHelper.DinhDangSo(dgvTheoThang, "GiaTrungBinh", "N0");

                List<double> lx = new List<double>(), ly = new List<double>();
                foreach (DataRow r in dt.Rows)
                {
                    if (r["GiaTrungBinh"] == DBNull.Value) continue;
                    double p = Convert.ToDouble(r["GiaTrungBinh"]), q = Convert.ToDouble(r["SoLuongBan"]);
                    if (p > 0 && q > 0) { lx.Add(Math.Log(p)); ly.Add(Math.Log(q)); }
                }
                if (lx.Count < 3)
                {
                    lblKetQua.Text = "Chưa đủ dữ liệu: cần ít nhất 3 tháng có bán (hiện có " + lx.Count + ").";
                    return;
                }
                double mx = 0, my = 0; for (int i = 0; i < lx.Count; i++) { mx += lx[i]; my += ly[i]; }
                mx /= lx.Count; my /= lx.Count;
                double sxx = 0, sxy = 0, syy = 0;
                for (int i = 0; i < lx.Count; i++) { sxx += (lx[i] - mx) * (lx[i] - mx); sxy += (lx[i] - mx) * (ly[i] - my); syy += (ly[i] - my) * (ly[i] - my); }
                if (sxx < 1e-12)
                {
                    lblKetQua.Text = "Chưa đủ dữ liệu: giá bán các tháng gần như giống hệt nhau nên chưa biết khách nhạy giá cỡ nào (cần có từ 2 mức giá khác nhau).";
                    return;
                }
                double ped = sxy / sxx;
                double r2 = syy < 1e-12 ? 1 : (sxy * sxy) / (sxx * syy);
                _ped = ped;
                string dienGiai;
                if (ped <= -1) dienGiai = "Khách NHẠY GIÁ: giảm giá 1% thì bán thêm khoảng " + Math.Abs(ped).ToString("0.##") + "%, giảm giá thường làm doanh thu tăng.";
                else if (ped < 0) dienGiai = "Khách ÍT NHẠY GIÁ: giảm giá 1% chỉ bán thêm khoảng " + Math.Abs(ped).ToString("0.##") + "%, tăng giá nhẹ thường làm doanh thu tăng.";
                else dienGiai = "Bất thường: giá tăng mà lượng bán không giảm. Có thể dữ liệu quá ít, hoặc khách mua vì lý do khác (khuyến mãi, mùa vụ, thói quen, thương hiệu) chứ không vì giá.";
                lblKetQua.Text = "Mức nhạy giá = " + ped.ToString("0.00") + "   (độ khớp " + (r2 * 100).ToString("0") + "%, " + lx.Count + " tháng)\n" + dienGiai +
                                 (lx.Count < 6 || r2 < 0.5 ? "\nCẩn thận: ít tháng hoặc độ khớp thấp nên con số này chưa đáng tin." : "");
                btnLuuPED.Enabled = true;
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLuuPED_Click(object sender, EventArgs e)
        {
            if (!_ped.HasValue || cboSanPham.SelectedValue == null) return;
            try
            {
                AiHelper.Chay("UPDATE SanPham SET DoCoGiang_PED = @v WHERE MaSP = @sp",
                    AiHelper.PDec("@v", Math.Round((decimal)_ped.Value, 4), 10, 4), AiHelper.P("@sp", cboSanPham.SelectedValue.ToString()));
                AiHelper.BaoTin("Đã lưu mức nhạy giá vào sản phẩm.");
                btnTinh_Click(sender, e);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
