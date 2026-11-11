using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    public partial class Form_DotSale : Form
    {
        private string _maDangChon;

        public Form_DotSale()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
            AiHelper.DinhDangLuoi(dgvDotSale);
        }

        private void Form_DotSale_Load(object sender, EventArgs e)
        {
            LamMoi();
            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            try
            {
                dgvDotSale.DataSource = AiHelper.Doc(
                    "SELECT ds.MaDot AS [Mã đợt], ds.TenDot AS [Tên đợt], ds.NgayBatDau AS [Bắt đầu], ds.NgayKetThuc AS [Kết thúc], " +
                    "       ds.NgayChinh AS [Ngày chính], ds.MucGiamToiDa_PhanTram AS [Giảm tối đa %], " +
                    "       DATEDIFF(DAY, ds.NgayBatDau, ds.NgayKetThuc) + 1 AS [Số ngày], " +
                    "       (SELECT COUNT(*) FROM DonHang dh WHERE CAST(dh.NgayDat AS DATE) BETWEEN ds.NgayBatDau AND ds.NgayKetThuc " +
                    "          AND ISNULL(dh.TrangThai, N'') NOT LIKE N'%hủy%') AS [Số đơn không hủy] " +
                    "FROM DotSale ds ORDER BY ds.NgayBatDau DESC");
                foreach (string c in new[] { "Bắt đầu", "Kết thúc", "Ngày chính" }) AiHelper.DinhDangSo(dgvDotSale, c, "dd/MM/yyyy");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvDotSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDotSale.Rows.Count) return;
            DataGridViewRow r = dgvDotSale.Rows[e.RowIndex];
            _maDangChon = Convert.ToString(r.Cells["Mã đợt"].Value);
            txtMaDot.Text = _maDangChon;
            txtMaDot.ReadOnly = true;
            txtTenDot.Text = Convert.ToString(r.Cells["Tên đợt"].Value);
            dtpBatDau.Value = Convert.ToDateTime(r.Cells["Bắt đầu"].Value);
            dtpKetThuc.Value = Convert.ToDateTime(r.Cells["Kết thúc"].Value);
            object nc = r.Cells["Ngày chính"].Value;
            if (nc == null || nc == DBNull.Value) dtpNgayChinh.Checked = false;
            else { dtpNgayChinh.Value = Convert.ToDateTime(nc); dtpNgayChinh.Checked = true; }
            object g = r.Cells["Giảm tối đa %"].Value;
            numGiamToiDa.Value = g == null || g == DBNull.Value ? 0 : Math.Min(100m, Math.Max(0m, Convert.ToDecimal(g)));
        }

        private void LamMoi()
        {
            _maDangChon = null;
            txtMaDot.ReadOnly = false;
            txtMaDot.Clear();
            txtTenDot.Clear();
            dtpBatDau.Value = DateTime.Today;
            dtpKetThuc.Value = DateTime.Today;
            dtpNgayChinh.Checked = false;
            numGiamToiDa.Value = 0;
            txtMaDot.Focus();
        }

        private bool KiemTra(string ma, string maBoQua)
        {
            if (ma.Length == 0) { AiHelper.BaoTin("Hãy nhập mã đợt."); return false; }
            if (txtTenDot.Text.Trim().Length == 0) { AiHelper.BaoTin("Hãy nhập tên đợt."); return false; }
            DateTime bd = dtpBatDau.Value.Date, kt = dtpKetThuc.Value.Date;
            if (kt < bd) { AiHelper.BaoTin("Ngày kết thúc không được trước ngày bắt đầu."); return false; }
            if (dtpNgayChinh.Checked && (dtpNgayChinh.Value.Date < bd || dtpNgayChinh.Value.Date > kt))
            {
                AiHelper.BaoTin("Ngày chính phải nằm trong khoảng Bắt đầu - Kết thúc.");
                return false;
            }
            DataTable trung = AiHelper.Doc(
                "SELECT TOP 1 MaDot, TenDot, NgayBatDau, NgayKetThuc FROM DotSale " +
                "WHERE (@bo IS NULL OR MaDot <> @bo) AND NgayBatDau <= @kt AND @bd <= NgayKetThuc ORDER BY NgayBatDau",
                AiHelper.P("@bo", maBoQua), Ngay("@bd", bd), Ngay("@kt", kt));
            if (trung.Rows.Count > 0)
            {
                DataRow r = trung.Rows[0];
                AiHelper.BaoTin("Trùng ngày với đợt " + r["MaDot"] + " - " + r["TenDot"] + " (" +
                                Convert.ToDateTime(r["NgayBatDau"]).ToString("dd/MM/yyyy") + " đến " + Convert.ToDateTime(r["NgayKetThuc"]).ToString("dd/MM/yyyy") + ").");
                return false;
            }
            return true;
        }

        private static SqlParameter Ngay(string ten, DateTime giaTri)
        {
            return new SqlParameter(ten, SqlDbType.Date) { Value = giaTri.Date };
        }

        private SqlParameter[] ThamSo(string ma)
        {
            return new[]
            {
                AiHelper.P("@ma", ma),
                AiHelper.P("@ten", txtTenDot.Text.Trim()),
                new SqlParameter("@chinh", SqlDbType.Date) { Value = dtpNgayChinh.Checked ? (object)dtpNgayChinh.Value.Date : DBNull.Value },
                Ngay("@bd", dtpBatDau.Value),
                Ngay("@kt", dtpKetThuc.Value),
                AiHelper.PDec("@giam", numGiamToiDa.Value, 5, 2)
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMaDot.Text.Trim();
            try
            {
                if (!KiemTra(ma, null)) return;
                if (AiHelper.LayGiaTri("SELECT 1 FROM DotSale WHERE MaDot = @m", AiHelper.P("@m", ma)) != null)
                {
                    AiHelper.BaoTin("Mã đợt " + ma + " đã có. Chọn dòng đó để sửa, hoặc đặt mã khác.");
                    return;
                }
                AiHelper.Chay("INSERT INTO DotSale (MaDot, TenDot, NgayChinh, NgayBatDau, NgayKetThuc, MucGiamToiDa_PhanTram) " +
                              "VALUES (@ma, @ten, @chinh, @bd, @kt, @giam)", ThamSo(ma));
                AiHelper.BaoTin("Đã thêm đợt sale " + ma + ".");
                TaiDanhSach();
                LamMoi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_maDangChon == null) { AiHelper.BaoTin("Hãy chọn 1 đợt ở bảng bên dưới trước khi sửa."); return; }
            try
            {
                if (!KiemTra(_maDangChon, _maDangChon)) return;
                AiHelper.Chay("UPDATE DotSale SET TenDot = @ten, NgayChinh = @chinh, NgayBatDau = @bd, NgayKetThuc = @kt, MucGiamToiDa_PhanTram = @giam " +
                              "WHERE MaDot = @ma", ThamSo(_maDangChon));
                AiHelper.BaoTin("Đã sửa đợt sale " + _maDangChon + ".");
                TaiDanhSach();
                LamMoi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_maDangChon == null) { AiHelper.BaoTin("Hãy chọn 1 đợt ở bảng bên dưới trước khi xóa."); return; }
            if (MessageBox.Show("Xóa đợt sale " + _maDangChon + "? Các đơn trong khoảng ngày này sẽ được tính là ngày thường.", "Xác nhận xóa",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                AiHelper.Chay("DELETE FROM DotSale WHERE MaDot = @m", AiHelper.P("@m", _maDangChon));
                TaiDanhSach();
                LamMoi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
