using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_TuDienVietTat : Form
    {
        private int _idDangChon = -1;

        public Form_TuDienVietTat()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
        }

        private void Form_TuDienVietTat_Load(object sender, EventArgs e)
        {
            AiHelper.DinhDangLuoi(dgvTeencode);
            cboLoaiTu.SelectedIndex = 0;
            TaiDuLieu();
        }

        private void TaiDuLieu()
        {
            try
            {
                dgvTeencode.DataSource = AiHelper.Doc("SELECT Id, TuGoc, TuChuanHoa, LoaiTu FROM TuDien_Teencode ORDER BY TuGoc");
                _idDangChon = -1;
                CamXucTuDien.LamMoi();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void dgvTeencode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvTeencode.Rows[e.RowIndex];
            _idDangChon = Convert.ToInt32(r.Cells["Id"].Value);
            txtTuGoc.Text = Convert.ToString(r.Cells["TuGoc"].Value);
            txtTuChuan.Text = Convert.ToString(r.Cells["TuChuanHoa"].Value);
            int i = cboLoaiTu.FindStringExact(Convert.ToString(r.Cells["LoaiTu"].Value));
            if (i >= 0) cboLoaiTu.SelectedIndex = i;
        }

        private bool KiemTraNhap()
        {
            if (txtTuGoc.Text.Trim().Length == 0 || txtTuChuan.Text.Trim().Length == 0)
            {
                AiHelper.BaoTin("Hãy nhập đủ \"Từ gốc\" và \"Viết đúng là\".");
                return false;
            }
            if (txtTuGoc.Text.Trim().Contains(" "))
            {
                AiHelper.BaoTin("\"Từ gốc\" chỉ là 1 từ, không có khoảng trắng (thủ tục thay theo từng từ đứng riêng).");
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhap()) return;
            try
            {
                AiHelper.Chay("INSERT INTO TuDien_Teencode (TuGoc, TuChuanHoa, LoaiTu) VALUES (@g, @c, @l)",
                    AiHelper.P("@g", txtTuGoc.Text.Trim().ToLowerInvariant()), AiHelper.P("@c", txtTuChuan.Text.Trim()), AiHelper.P("@l", cboLoaiTu.SelectedItem));
                txtTuGoc.Clear(); txtTuChuan.Clear();
                TaiDuLieu();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) { AiHelper.BaoTin("Từ gốc này đã có trong từ điển."); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_idDangChon < 0) { AiHelper.BaoTin("Hãy chọn một dòng trong bảng để sửa."); return; }
            if (!KiemTraNhap()) return;
            try
            {
                AiHelper.Chay("UPDATE TuDien_Teencode SET TuGoc = @g, TuChuanHoa = @c, LoaiTu = @l WHERE Id = @id",
                    AiHelper.P("@g", txtTuGoc.Text.Trim().ToLowerInvariant()), AiHelper.P("@c", txtTuChuan.Text.Trim()),
                    AiHelper.P("@l", cboLoaiTu.SelectedItem), AiHelper.P("@id", _idDangChon));
                TaiDuLieu();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) { AiHelper.BaoTin("Từ gốc này đã có ở một dòng khác."); }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_idDangChon < 0) { AiHelper.BaoTin("Hãy chọn một dòng trong bảng để xóa."); return; }
            if (MessageBox.Show("Xóa từ \"" + txtTuGoc.Text + "\" khỏi từ điển?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                AiHelper.Chay("DELETE FROM TuDien_Teencode WHERE Id = @id", AiHelper.P("@id", _idDangChon));
                txtTuGoc.Clear(); txtTuChuan.Clear();
                TaiDuLieu();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuGoc.Clear(); txtTuChuan.Clear();
            TaiDuLieu();
        }

        private void btnThu_Click(object sender, EventArgs e)
        {
            try
            {
                string s = Convert.ToString(AiHelper.LayGiaTri("SELECT dbo.fn_LamSachKyTu(@t)", AiHelper.P("@t", txtThu.Text))) ?? "";
                s = " " + s.ToLowerInvariant() + " ";
                DataTable tu = AiHelper.Doc("SELECT TuGoc, TuChuanHoa FROM TuDien_Teencode ORDER BY LEN(TuGoc) DESC, Id");
                for (int lan = 0; lan < 2; lan++)
                    foreach (DataRow r in tu.Rows)
                        s = s.Replace(" " + Convert.ToString(r["TuGoc"]).ToLowerInvariant() + " ", " " + Convert.ToString(r["TuChuanHoa"]) + " ");
                txtKetQuaThu.Text = s.Trim();
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
