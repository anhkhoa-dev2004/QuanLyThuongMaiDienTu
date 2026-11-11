using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    public partial class Form_PhanQuyen : Form
    {
        private DataTable dtPhanQuyen;

        private const string SQL_DANH_SACH = @"
            SELECT nd.MaND, nd.TenTK, nd.HoTen,
                   ISNULL(pq.TenQuyen, N'Chưa phân quyền') AS TenQuyen,
                   ISNULL(pq.QuyenKho, 0) AS QuyenKho,
                   ISNULL(pq.QuyenBanHang, 0) AS QuyenBanHang,
                   ISNULL(pq.QuyenBaoCao, 0) AS QuyenBaoCao,
                   CASE WHEN nd.TrangThai = N'Đã khóa' THEN N'Đã khóa'
                        WHEN nd.KhoaDen > GETDATE() THEN N'Tạm khóa đến ' + FORMAT(nd.KhoaDen, 'HH:mm dd/MM')
                        WHEN nd.SoLanSaiMatKhau > 0 THEN N'Đang dùng (sai mật khẩu ' + CAST(nd.SoLanSaiMatKhau AS NVARCHAR(5)) + N' lần)'
                        ELSE N'Đang dùng' END AS TinhTrangTK
            FROM NguoiDung nd
            LEFT JOIN PhanQuyen pq ON nd.MaND = pq.MaND";

        public Form_PhanQuyen()
        {
            InitializeComponent();
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + 46);
            Button btnKhoa = new Button { Text = "Khóa tài khoản", Location = new Point(103, 525), Size = new Size(144, 30) };
            Button btnMo = new Button { Text = "Mở khóa", Location = new Point(257, 525), Size = new Size(120, 30) };
            Button btnDatLai = new Button { Text = "Đặt lại mật khẩu", Location = new Point(387, 525), Size = new Size(150, 30) };
            Button btnTao = new Button { Text = "Tạo tài khoản nhân viên", Location = new Point(547, 525), Size = new Size(205, 30) };
            btnKhoa.Click += (s, e) => KhoaMo(true);
            btnMo.Click += (s, e) => KhoaMo(false);
            btnDatLai.Click += (s, e) => DatLaiMatKhau();
            btnTao.Click += (s, e) =>
            {
                using (Form_DangKy f = new Form_DangKy()) f.ShowDialog(this);
                LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
            };
            Button btnLuuPhanQuyen = new Button { Text = "Lưu phân quyền", Location = new Point(762, 525), Size = new Size(150, 30) };
            btnLuuPhanQuyen.Click += (s, e) => LuuPhanQuyen();
            Controls.AddRange(new Control[] { btnKhoa, btnMo, btnDatLai, btnTao, btnLuuPhanQuyen });
            GiaoDien.GanChoForm(this);
        }

        private void LuuPhanQuyen()
        {
            if (dtPhanQuyen == null || dtPhanQuyen.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có danh sách nhân viên để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvPhanQuyen.EndEdit();

            DataRow[] cacDongDaDoi = dtPhanQuyen.Select(null, null, DataViewRowState.ModifiedCurrent);
            if (cacDongDaDoi.Length == 0)
            {
                MessageBox.Show("Chưa có quyền nào được thay đổi.\nHãy tích hoặc bỏ tích ở cột Quyền Kho, Quyền Bán hàng, Quyền Báo cáo rồi bấm Lưu phân quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow row in cacDongDaDoi)
            {
                if (row["MaND"].ToString() == Session.MaND && !LaDayDuQuyen(row))
                {
                    if (MessageBox.Show(
                        "Bạn đang bỏ bớt quyền của CHÍNH tài khoản đang đăng nhập.\n" +
                        "Sau khi lưu, bạn có thể không còn thấy được menu Phân quyền người dùng nữa.\n" +
                        "Bạn có chắc chắn muốn tiếp tục?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                    break;
                }
            }

            List<DataRow> thuTuLuu = cacDongDaDoi.Where(r => r["MaND"].ToString() != Session.MaND).ToList();
            thuTuLuu.AddRange(cacDongDaDoi.Where(r => r["MaND"].ToString() == Session.MaND));

            List<string> daLuu = new List<string>();
            string dangLuu = null;
            bool quyenMinhDoi = false;

            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    foreach (DataRow row in thuTuLuu)
                    {
                        dangLuu = row["MaND"].ToString();
                        bool quyenKho = Convert.ToBoolean(row["QuyenKho"]);
                        bool quyenBanHang = Convert.ToBoolean(row["QuyenBanHang"]);
                        bool quyenBaoCao = Convert.ToBoolean(row["QuyenBaoCao"]);

                        using (SqlCommand cmd = new SqlCommand("sp_CapNhatPhanQuyen", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaNDQuanLy", Session.MaND);
                            cmd.Parameters.AddWithValue("@MaND", dangLuu);
                            cmd.Parameters.AddWithValue("@QuyenKho", quyenKho);
                            cmd.Parameters.AddWithValue("@QuyenBanHang", quyenBanHang);
                            cmd.Parameters.AddWithValue("@QuyenBaoCao", quyenBaoCao);
                            cmd.Parameters.AddWithValue("@TenQuyen", DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }

                        daLuu.Add(dangLuu);

                        if (dangLuu == Session.MaND)
                        {
                            Session.QuyenKho = quyenKho;
                            Session.QuyenBanHang = quyenBanHang;
                            Session.QuyenBaoCao = quyenBaoCao;
                            quyenMinhDoi = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string noiDung = dangLuu == null ? "Chưa lưu được phân quyền." : "Chưa lưu được quyền của tài khoản " + dangLuu + ".";
                if (daLuu.Count > 0) noiDung += "\nĐã lưu xong cho: " + string.Join(", ", daLuu) + ".";
                MessageBox.Show(noiDung + "\n" + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringBuilder thongBao = new StringBuilder();
            thongBao.AppendLine("Đã lưu phân quyền cho " + thuTuLuu.Count + " tài khoản:");
            foreach (DataRow row in thuTuLuu)
            {
                thongBao.AppendLine("- " + row["MaND"] + " (" + row["HoTen"] + "): Quyền Kho " + CoHayKhong(row["QuyenKho"]) +
                    ", Quyền Bán hàng " + CoHayKhong(row["QuyenBanHang"]) + ", Quyền Báo cáo " + CoHayKhong(row["QuyenBaoCao"]));
            }

            if (quyenMinhDoi)
            {
                thongBao.AppendLine();
                thongBao.Append("Quyền của chính bạn đã đổi, menu đã được cập nhật theo quyền mới.");
                foreach (Form f in Application.OpenForms)
                {
                    Form_TrangChuChinh trangChu = f as Form_TrangChuChinh;
                    if (trangChu != null)
                    {
                        trangChu.LamMoiMenuTheoQuyen();
                        break;
                    }
                }
            }

            LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
            AiHelper.BaoTin(thongBao.ToString().TrimEnd());
        }

        private static bool LaDayDuQuyen(DataRow row)
        {
            return Convert.ToBoolean(row["QuyenKho"]) && Convert.ToBoolean(row["QuyenBanHang"]) && Convert.ToBoolean(row["QuyenBaoCao"]);
        }

        private static string CoHayKhong(object giaTri)
        {
            return Convert.ToBoolean(giaTri) ? "Có" : "Không";
        }

        private string MaNDDangChon()
        {
            if (dgvPhanQuyen.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản trong bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return Convert.ToString(dgvPhanQuyen.CurrentRow.Cells["MaND"].Value);
        }

        private void KhoaMo(bool khoa)
        {
            string maND = MaNDDangChon();
            if (maND == null) return;
            string hoi = khoa ? "Khóa tài khoản " + maND + "? Người này sẽ không đăng nhập được nữa (dùng khi nhân viên nghỉ việc)."
                              : "Mở khóa tài khoản " + maND + "? Số lần nhập sai mật khẩu cũng được xóa.";
            if (MessageBox.Show(hoi, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                AiHelper.ChayThuTucLayMa("sp_KhoaMoTaiKhoan", null, AiHelper.P("@MaNDQuanLy", Session.MaND), AiHelper.P("@MaND", maND), AiHelper.P("@Khoa", khoa));
                LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void DatLaiMatKhau()
        {
            string maND = MaNDDangChon();
            if (maND == null) return;
            string mk = Sales_Module.DonHangShopee.HoiLyDo("Đặt lại mật khẩu", "Nhập mật khẩu tạm cho " + maND + " (ít nhất 6 ký tự). Báo cho nhân viên đăng nhập rồi tự đổi mật khẩu.", null);
            if (mk == null) return;
            try
            {
                AiHelper.ChayThuTucLayMa("sp_DatLaiMatKhau", null, AiHelper.P("@MaNDQuanLy", Session.MaND), AiHelper.P("@MaND", maND), AiHelper.P("@MatKhauMoi", mk));
                LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
                AiHelper.BaoTin("Đã đặt lại mật khẩu cho " + maND + " và mở khóa nếu đang bị tạm khóa.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void Form_PhanQuyen_Load(object sender, EventArgs e)
        {
            CauHinhLuoi();
            LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
        }

        private void CauHinhLuoi()
        {
            dgvPhanQuyen.AutoGenerateColumns = false;
            dgvPhanQuyen.Columns.Clear();
            dgvPhanQuyen.AllowUserToAddRows = false;
            dgvPhanQuyen.AllowUserToDeleteRows = false;
            dgvPhanQuyen.RowHeadersVisible = false;
            dgvPhanQuyen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhanQuyen.MultiSelect = false;
            dgvPhanQuyen.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvPhanQuyen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanQuyen.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaND",
                DataPropertyName = "MaND",
                HeaderText = "Mã NV",
                ReadOnly = true,
                FillWeight = 60
            });
            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenTK",
                DataPropertyName = "TenTK",
                HeaderText = "Tài khoản",
                ReadOnly = true,
                FillWeight = 80
            });
            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                DataPropertyName = "HoTen",
                HeaderText = "Họ tên",
                ReadOnly = true,
                FillWeight = 110
            });
            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenQuyen",
                DataPropertyName = "TenQuyen",
                HeaderText = "Tên vai trò",
                ReadOnly = true,
                FillWeight = 150
            });
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "QuyenKho",
                DataPropertyName = "QuyenKho",
                HeaderText = "Quyền Kho",
                FillWeight = 90
            });
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "QuyenBanHang",
                DataPropertyName = "QuyenBanHang",
                HeaderText = "Quyền Bán hàng",
                FillWeight = 105
            });
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "QuyenBaoCao",
                DataPropertyName = "QuyenBaoCao",
                HeaderText = "Quyền Báo cáo",
                FillWeight = 105
            });

            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TinhTrangTK",
                DataPropertyName = "TinhTrangTK",
                HeaderText = "Tình trạng tài khoản",
                ReadOnly = true,
                FillWeight = 120
            });

            dgvPhanQuyen.CellContentClick -= dgvPhanQuyen_CellContentClick;
            dgvPhanQuyen.CellContentClick += dgvPhanQuyen_CellContentClick;
            dgvPhanQuyen.CurrentCellDirtyStateChanged -= dgvPhanQuyen_CurrentCellDirtyStateChanged;
            dgvPhanQuyen.CurrentCellDirtyStateChanged += dgvPhanQuyen_CurrentCellDirtyStateChanged;
        }

        private void LoadDanhSachPhanQuyen(string query, SqlParameter thamSo)
        {
            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (thamSo != null) cmd.Parameters.Add(thamSo);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtPhanQuyen = new DataTable();
                            da.Fill(dtPhanQuyen);
                        }
                    }
                }

                dgvPhanQuyen.DataSource = dtPhanQuyen;

                dtPhanQuyen.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phân quyền: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void lblTimKiem_Click(object sender, EventArgs e)
        {
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            string query = SQL_DANH_SACH + " WHERE nd.MaND LIKE @Key OR nd.TenTK LIKE @Key OR nd.HoTen LIKE @Key";
            LoadDanhSachPhanQuyen(query, new SqlParameter("@Key", "%" + keyword + "%"));
        }

        private void dgvPhanQuyen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvPhanQuyen.Columns[e.ColumnIndex] as DataGridViewCheckBoxColumn != null)
            {
                if (dgvPhanQuyen.IsCurrentCellDirty)
                {
                    dgvPhanQuyen.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dgvPhanQuyen_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPhanQuyen.IsCurrentCellDirty)
            {
                dgvPhanQuyen.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
        }

        private void btnXoaQuyen_Click(object sender, EventArgs e)
        {
            if (dgvPhanQuyen.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần thao tác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có muốn reset quyền của tài khoản này về mặc định (chỉ còn Quyền Bán hàng) không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            string maND = dgvPhanQuyen.CurrentRow.Cells["MaND"].Value.ToString();

            if (maND == Session.MaND)
            {
                MessageBox.Show("Không thể tự reset quyền của chính tài khoản đang đăng nhập tại đây, hãy nhờ một quản trị viên khác thực hiện!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CapNhatPhanQuyen", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNDQuanLy", Session.MaND);
                        cmd.Parameters.AddWithValue("@MaND", maND);
                        cmd.Parameters.AddWithValue("@QuyenKho", false);
                        cmd.Parameters.AddWithValue("@QuyenBanHang", true);
                        cmd.Parameters.AddWithValue("@QuyenBaoCao", false);
                        cmd.Parameters.AddWithValue("@TenQuyen", DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã reset quyền về mặc định thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachPhanQuyen(SQL_DANH_SACH, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thao tác: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (dtPhanQuyen != null && dtPhanQuyen.GetChanges() != null)
            {
                DialogResult dr = MessageBox.Show("Bạn có thay đổi chưa lưu. Thoát mà không lưu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }
            this.Close();
        }
    }
}
