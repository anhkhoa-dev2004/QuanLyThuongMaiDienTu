using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.AI_Module;

namespace QuanLy_BanHang_Kho_Shopee.Category_Module
{
    public partial class Form_SanPham : Form
    {
        private string ThuMucAnh => Application.StartupPath;

        private const string COL_MASP = "Mã SP";
        private const string COL_TENSP = "Tên Sản Phẩm";
        private const string COL_TENDANHMUC = "Danh Mục";
        private const string COL_MADANHMUC = "MaDanhMucRaw";
        private const string COL_BARCODE = "Barcode";
        private const string COL_NCC = "Nhà NCC";
        private const string COL_TONKHO = "Tồn Kho";
        private const string COL_NGUONGANTOAN = "Ngưỡng An Toàn";
        private const string COL_GIABAN = "Giá Bán";
        private const string COL_SKUSHOPEE = "Mã SKU Shopee";
        private const string COL_HINHANH = "HinhAnh";
        private const string COL_ANH = "ColAnhThuNho";

        private const string COL_TINHTRANG = "Tình Trạng";
        private const string COL_BAOHANH = "Bảo Hành (tháng)";

        private readonly Dictionary<string, Image> _anhNho = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private readonly CheckBox chkNgungBan;
        private readonly NumericUpDown numBaoHanh;

        public Form_SanPham()
        {
            InitializeComponent();
            Label lblBaoHanh = new Label { Text = "Bảo hành (tháng):", Location = new Point(318, 258), Size = new Size(112, 22) };
            numBaoHanh = new NumericUpDown { Location = new Point(432, 255), Width = 68, Minimum = 0, Maximum = 120, Value = 12 };
            chkNgungBan = new CheckBox { Text = "Ngừng bán", Location = new Point(500, 378), Size = new Size(115, 24) };
            Controls.AddRange(new Control[] { lblBaoHanh, numBaoHanh, chkNgungBan });
            GiaoDien.GanChoForm(this);
            GiaoDien.ThemBangDuoi(this, "Lịch sử đổi giá bán (mới nhất ở trên)", 200,
                () => AiHelper.Doc(
                    "SELECT TOP 300 ls.NgayDoi AS [Ngày đổi], ls.MaSP AS [Mã SP], sp.TenSP AS [Sản phẩm], ls.GiaBanCu AS [Giá cũ], ls.GiaBanMoi AS [Giá mới], " +
                    "       ls.GiaVonLucDoi AS [Giá vốn lúc đổi], " +
                    "       CAST(100.0 * (ls.GiaBanMoi - ls.GiaVonLucDoi) / NULLIF(ls.GiaBanMoi, 0) AS DECIMAL(6,1)) AS [Lãi trên giá mới (%)], ls.MaND AS [Người đổi] " +
                    "FROM LichSuGiaBan ls JOIN SanPham sp ON sp.MaSP = ls.MaSP ORDER BY ls.NgayDoi DESC, ls.Id DESC"),
                null);
        }

        private void KhoiTaoChucNang()
        {
            dgvSanPham.SelectionChanged += (s, e) => CapNhatThongSoAi();
            dgvSanPham.CellFormatting += dgvSanPham_CellFormatting;
            dgvSanPham.DataBindingComplete += (s, e) => NapCotAnhThuNho();
        }

        private void btnMoToiUuGia_Click(object sender, EventArgs e)
        {
            using (Form_ThuGiaBan frm = new Form_ThuGiaBan()) frm.ShowDialog();
            CapNhatThongSoAi();
        }

        private void CapNhatThongSoAi()
        {
            if (lblThongSoAi == null) return;
            if (dgvSanPham.CurrentRow == null || !dgvSanPham.Columns.Contains(COL_MASP) || dgvSanPham.CurrentRow.Cells[COL_MASP].Value == null)
            {
                lblThongSoAi.Text = "";
                return;
            }
            try
            {
                DataTable t = AiHelper.Doc(
                    "SELECT sp.DoCoGiang_PED, " +
                    "       (SELECT CAST(100.0 * SUM(CASE WHEN d.SoSao >= 4 THEN 1 ELSE 0 END) / NULLIF(COUNT(*), 0) AS DECIMAL(5, 1)) " +
                    "        FROM DanhGiaSanPham d WHERE d.MaSP = sp.MaSP) AS DiemCamXuc_CSAT, " +
                    "       v.ROP AS DiemDatHangLai_ROP, v.TonAnToan AS TonKhoAnToan, v.SoNgayConLai AS SoNgayConLai_DuKien " +
                    "FROM SanPham sp LEFT JOIN vw_DuBaoSanPham v ON v.MaSP = sp.MaSP WHERE sp.MaSP = @m",
                    AiHelper.P("@m", Convert.ToString(dgvSanPham.CurrentRow.Cells[COL_MASP].Value)));
                if (t.Rows.Count == 0) { lblThongSoAi.Text = ""; return; }
                DataRow r = t.Rows[0];

                string ped = "chưa tính";
                if (r["DoCoGiang_PED"] != DBNull.Value)
                {
                    decimal p = Convert.ToDecimal(r["DoCoGiang_PED"]);
                    ped = p.ToString("0.00") + (p <= -1 ? " (khách nhạy giá)" : (p < 0 ? " (khách ít nhạy giá)" : " (số liệu chưa đáng tin)"));
                }
                string csat = r["DiemCamXuc_CSAT"] == DBNull.Value ? "chưa có" : Convert.ToDecimal(r["DiemCamXuc_CSAT"]).ToString("0.0") + "% đánh giá từ 4 sao trở lên";
                string rop = r["DiemDatHangLai_ROP"] == DBNull.Value ? "chưa tính" : r["DiemDatHangLai_ROP"] + " (tồn dự phòng " + r["TonKhoAnToan"] + ", còn bán ~" + (r["SoNgayConLai_DuKien"] == DBNull.Value ? "?" : r["SoNgayConLai_DuKien"]) + " ngày)";
                lblThongSoAi.Text = "Mức nhạy giá: " + ped + "\nKhách hài lòng: " + csat + "\nMức phải đặt hàng: " + rop;
            }
            catch (Exception ex) { lblThongSoAi.Text = "Chưa đọc được chỉ số phân tích: " + AiHelper.LoiDeHieu(ex); }
        }

        private void Form_SanPham_Load(object sender, EventArgs e)
        {
            KhoiTaoChucNang();
            LoadDanhMuc();
            LoadDataSanPham();
        }

        private void LoadDanhMuc()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMuc ORDER BY TenDanhMuc";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboDanhMuc.DataSource = dt;
                    cboDanhMuc.DisplayMember = "TenDanhMuc";
                    cboDanhMuc.ValueMember = "MaDanhMuc";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải Danh mục: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadDataSanPham()
        {
            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            sp.MaSP AS [Mã SP],
                            sp.TenSP AS [Tên Sản Phẩm],
                            ISNULL(dm.TenDanhMuc, N'(Chưa phân loại)') AS [Danh Mục],
                            sp.MaDanhMuc AS [MaDanhMucRaw],
                            ISNULL(sp.Barcode, '') AS [Barcode],
                            ISNULL(ncc.TenNCC, N'(Chưa có)') AS [Nhà NCC],
                            ISNULL(tk.TongTon, 0) AS [Tồn Kho],
                            sp.NguongAnToan AS [Ngưỡng An Toàn],
                            sp.GiaBan AS [Giá Bán],
                            ISNULL(sp.MaSKUShopee, '') AS [Mã SKU Shopee],
                            CASE WHEN sp.DangKinhDoanh = 1 THEN N'Đang bán' ELSE N'Ngừng bán' END AS [Tình Trạng],
                            sp.SoThangBaoHanh AS [Bảo Hành (tháng)],
                            v.Cau AS [Xu Hướng Bán],
                            v.DuDoan30Ngay AS [Dự Đoán Bán 30 Ngày Tới],
                            v.SoNgayConLai AS [Đủ Bán (Ngày)],
                            v.ViecNenLam AS [Nên Làm Gì],
                            ISNULL(sp.HinhAnh, '') AS [HinhAnh]
                        FROM SanPham sp WITH (NOLOCK)
                        LEFT JOIN vw_DuBaoSanPham v ON v.MaSP = sp.MaSP
                        LEFT JOIN DanhMuc dm WITH (NOLOCK) ON sp.MaDanhMuc = dm.MaDanhMuc
                        LEFT JOIN vw_SanPham_NCCGanNhat ncc ON sp.MaSP = ncc.MaSP
                        LEFT JOIN (
                            SELECT MaSP, SUM(SoLuongTon) AS TongTon
                            FROM TonKho WITH (NOLOCK)
                            GROUP BY MaSP
                        ) tk ON sp.MaSP = tk.MaSP
                        ORDER BY sp.MaSP";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvSanPham.DataSource = dt;
                    dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dgvSanPham.Columns["Giá Bán"] != null)
                        dgvSanPham.Columns["Giá Bán"].DefaultCellStyle.Format = "N0";

                    if (dgvSanPham.Columns["MaDanhMucRaw"] != null)
                        dgvSanPham.Columns["MaDanhMucRaw"].Visible = false;
                    if (dgvSanPham.Columns["HinhAnh"] != null)
                        dgvSanPham.Columns["HinhAnh"].Visible = false;
                    if (dgvSanPham.Columns["Nên Làm Gì"] != null)
                        dgvSanPham.Columns["Nên Làm Gì"].FillWeight = 300;

                    NapCotAnhThuNho();

                    if (dgvSanPham.Rows.Count > 0)
                    {
                        dgvSanPham.ClearSelection();
                        dgvSanPham.Rows[0].Selected = true;
                        dgvSanPham.CurrentCell = dgvSanPham.Rows[0].Cells[COL_TENSP];
                        HienThiDong(dgvSanPham.Rows[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu Sản phẩm: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private string TimFileAnh(string tenFile)
        {
            if (string.IsNullOrWhiteSpace(tenFile)) return null;
            string[] thuMuc =
            {
                Application.StartupPath,
                Path.Combine(Application.StartupPath, "Assets", "AnhSanPham"),
                Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "Assets", "AnhSanPham"))
            };
            foreach (string tm in thuMuc)
            {
                string duongDan = Path.Combine(tm, tenFile.Trim());
                if (File.Exists(duongDan)) return duongDan;
            }
            return null;
        }

        private static Image DocAnh(string duongDan)
        {
            byte[] bytes = File.ReadAllBytes(duongDan);
            using (MemoryStream ms = new MemoryStream(bytes))
            using (Image goc = Image.FromStream(ms))
            {
                return new Bitmap(goc);
            }
        }

        private Image LayAnhNho(string tenFile)
        {
            if (string.IsNullOrWhiteSpace(tenFile)) return null;
            Image anh;
            if (_anhNho.TryGetValue(tenFile, out anh)) return anh;
            anh = null;
            string duongDan = TimFileAnh(tenFile);
            if (duongDan != null)
            {
                try
                {
                    using (Image goc = DocAnh(duongDan))
                        anh = new Bitmap(goc, new Size(64, 64));
                }
                catch
                {
                    anh = null;
                }
            }
            _anhNho[tenFile] = anh;
            return anh;
        }

        private void NapCotAnhThuNho()
        {
            if (!dgvSanPham.Columns.Contains(COL_ANH))
            {
                DataGridViewImageColumn colAnh = new DataGridViewImageColumn();
                colAnh.Name = COL_ANH;
                colAnh.HeaderText = "Ảnh";
                colAnh.ImageLayout = DataGridViewImageCellLayout.Zoom;
                colAnh.Width = 70;
                colAnh.DefaultCellStyle.NullValue = null;
                dgvSanPham.Columns.Add(colAnh);
            }
            dgvSanPham.Columns[COL_ANH].DisplayIndex = 0;
            _anhNho.Clear();
            dgvSanPham.RowTemplate.Height = 60;
            foreach (DataGridViewRow row in dgvSanPham.Rows) row.Height = 60;
            dgvSanPham.Invalidate();
        }

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvSanPham.Columns[e.ColumnIndex].Name != COL_ANH || !dgvSanPham.Columns.Contains(COL_HINHANH)) return;
            e.Value = LayAnhNho(Convert.ToString(dgvSanPham.Rows[e.RowIndex].Cells[COL_HINHANH].Value));
            e.FormattingApplied = true;
        }

        private void HienThiDong(DataGridViewRow row)
        {
            txtMaSanPham.Text = row.Cells[COL_MASP].Value?.ToString();
            txtTenSanPham.Text = row.Cells[COL_TENSP].Value?.ToString();
            txtBarcode.Text = row.Cells[COL_BARCODE].Value?.ToString();
            txtMaNCC.Text = row.Cells[COL_NCC].Value?.ToString();
            txtSoLuong.Text = row.Cells[COL_TONKHO].Value?.ToString();
            txtNguongAnToan.Text = row.Cells[COL_NGUONGANTOAN].Value?.ToString();
            txtGiaBan.Text = row.Cells[COL_GIABAN].Value?.ToString();
            txtMaSKUShopee.Text = row.Cells[COL_SKUSHOPEE].Value?.ToString();
            chkNgungBan.Checked = Convert.ToString(row.Cells[COL_TINHTRANG].Value) == "Ngừng bán";
            object bh = row.Cells[COL_BAOHANH].Value;
            numBaoHanh.Value = bh == null || bh == DBNull.Value ? 12 : Math.Max(0, Math.Min(120, Convert.ToInt32(bh)));

            string maDanhMuc = row.Cells[COL_MADANHMUC].Value?.ToString();
            if (!string.IsNullOrEmpty(maDanhMuc))
                cboDanhMuc.SelectedValue = maDanhMuc;

            _tenFileAnhDangChon = null;
            NapAnhLenPictureBox(row.Cells[COL_HINHANH].Value?.ToString());
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            HienThiDong(dgvSanPham.Rows[e.RowIndex]);
        }

        private void dgvSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            dgvSanPham_CellClick(sender, e);

            string tenFile = dgvSanPham.Rows[e.RowIndex].Cells[COL_HINHANH].Value?.ToString();
            string duongDan = TimFileAnh(tenFile);

            if (duongDan == null)
            {
                MessageBox.Show("Sản phẩm này chưa có ảnh. Bấm \"Chọn ảnh...\" để gắn ảnh cho sản phẩm.",
                    "Chưa có ảnh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenSp = dgvSanPham.Rows[e.RowIndex].Cells[COL_TENSP].Value?.ToString();
            using (Form popup = new Form())
            using (PictureBox pb = new PictureBox())
            {
                popup.Text = tenSp;
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ClientSize = new Size(500, 500);
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                pb.Dock = DockStyle.Fill;
                pb.SizeMode = PictureBoxSizeMode.Zoom;

                pb.Image = DocAnh(duongDan);
                popup.Controls.Add(pb);

                popup.ShowDialog(this);
            }
        }

        private void NapAnhLenPictureBox(string tenFile)
        {
            pbAnhSanPham.Image?.Dispose();
            pbAnhSanPham.Image = null;
            string duongDan = TimFileAnh(tenFile);
            if (duongDan == null) return;
            try { pbAnhSanPham.Image = DocAnh(duongDan); }
            catch { pbAnhSanPham.Image = null; }
        }

        private string _tenFileAnhDangChon = null;

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSanPham.Text.Trim();
            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Nhập Mã SP trước khi chọn ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    string phanMoRong = Path.GetExtension(ofd.FileName);
                    string tenFileMoi = maSP + phanMoRong;
                    string duongDanDich = Path.Combine(ThuMucAnh, tenFileMoi);

                    File.Copy(ofd.FileName, duongDanDich, true);
                    _tenFileAnhDangChon = tenFileMoi;
                    NapAnhLenPictureBox(tenFileMoi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi gắn ảnh: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSanPham.Text.Trim();
            string tenSP = txtTenSanPham.Text.Trim();
            string barcode = txtBarcode.Text.Trim();
            string maSKUShopee = txtMaSKUShopee.Text.Trim();

            if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã SP và Tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboDanhMuc.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out decimal giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nguongAnToan = 10;
            int.TryParse(txtNguongAnToan.Text.Trim(), out nguongAnToan);

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaSP", maSP);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show($"Mã sản phẩm [{maSP}] đã tồn tại!", "Mã đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO SanPham (MaSP, MaDanhMuc, Barcode, TenSP, GiaBan, NguongAnToan, HinhAnh, MaSKUShopee, DangKinhDoanh, SoThangBaoHanh)
                        VALUES (@MaSP, @MaDanhMuc, @Barcode, @TenSP, @GiaBan, @NguongAnToan, @HinhAnh, @MaSKUShopee, @DangBan, @BaoHanh)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaSP", maSP);
                        cmdInsert.Parameters.AddWithValue("@MaDanhMuc", cboDanhMuc.SelectedValue);
                        cmdInsert.Parameters.AddWithValue("@Barcode", string.IsNullOrEmpty(barcode) ? (object)DBNull.Value : barcode);
                        cmdInsert.Parameters.AddWithValue("@TenSP", tenSP);
                        cmdInsert.Parameters.AddWithValue("@GiaBan", giaBan);
                        cmdInsert.Parameters.AddWithValue("@NguongAnToan", nguongAnToan);
                        cmdInsert.Parameters.AddWithValue("@HinhAnh", string.IsNullOrEmpty(_tenFileAnhDangChon) ? (object)DBNull.Value : _tenFileAnhDangChon);
                        cmdInsert.Parameters.AddWithValue("@MaSKUShopee", string.IsNullOrEmpty(maSKUShopee) ? (object)DBNull.Value : maSKUShopee);
                        cmdInsert.Parameters.AddWithValue("@DangBan", !chkNgungBan.Checked);
                        cmdInsert.Parameters.AddWithValue("@BaoHanh", (int)numBaoHanh.Value);
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm Sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataSanPham();
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm Sản phẩm: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSanPham.Text.Trim();
            string tenSP = txtTenSanPham.Text.Trim();
            string barcode = txtBarcode.Text.Trim();
            string maSKUShopee = txtMaSKUShopee.Text.Trim();

            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn Sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboDanhMuc.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out decimal giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nguongAnToan;
            int.TryParse(txtNguongAnToan.Text.Trim(), out nguongAnToan);

            try
            {
                object gv = AiHelper.LayGiaTri("SELECT GiaVon FROM SanPham WHERE MaSP = @m", AiHelper.P("@m", maSP));
                decimal giaVon = gv == null ? 0 : Convert.ToDecimal(gv);
                if (giaVon > 0 && giaBan < giaVon &&
                    MessageBox.Show("Giá bán mới " + giaBan.ToString("N0") + " đ thấp hơn giá vốn " + giaVon.ToString("N0") + " đ (giá nhập bình quân).\n" +
                                    "Mỗi cái bán ra sẽ lỗ " + (giaVon - giaBan).ToString("N0") + " đ, chưa tính phí sàn. Vẫn lưu?",
                                    "Giá bán thấp hơn giá vốn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            }
            catch (Exception ex) { MessageBox.Show("Chưa kiểm tra được giá vốn: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();

                    string updateQuery = @"
                        UPDATE SanPham
                        SET TenSP = @TenSP,
                            MaDanhMuc = @MaDanhMuc,
                            Barcode = @Barcode,
                            GiaBan = @GiaBan,
                            NguongAnToan = @NguongAnToan,
                            HinhAnh = COALESCE(@HinhAnh, HinhAnh),
                            MaSKUShopee = @MaSKUShopee,
                            DangKinhDoanh = @DangBan,
                            SoThangBaoHanh = @BaoHanh
                        WHERE MaSP = @MaSP";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaSP", maSP);
                        cmdUpdate.Parameters.AddWithValue("@TenSP", tenSP);
                        cmdUpdate.Parameters.AddWithValue("@MaDanhMuc", cboDanhMuc.SelectedValue);
                        cmdUpdate.Parameters.AddWithValue("@Barcode", string.IsNullOrEmpty(barcode) ? (object)DBNull.Value : barcode);
                        cmdUpdate.Parameters.AddWithValue("@GiaBan", giaBan);
                        cmdUpdate.Parameters.AddWithValue("@NguongAnToan", nguongAnToan);
                        cmdUpdate.Parameters.AddWithValue("@HinhAnh", string.IsNullOrEmpty(_tenFileAnhDangChon) ? (object)DBNull.Value : _tenFileAnhDangChon);
                        cmdUpdate.Parameters.AddWithValue("@MaSKUShopee", string.IsNullOrEmpty(maSKUShopee) ? (object)DBNull.Value : maSKUShopee);
                        cmdUpdate.Parameters.AddWithValue("@DangBan", !chkNgungBan.Checked);
                        cmdUpdate.Parameters.AddWithValue("@BaoHanh", (int)numBaoHanh.Value);
                        int affected = cmdUpdate.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin Sản phẩm thành công!" +
                                            (chkNgungBan.Checked ? "\nSản phẩm đang để \"Ngừng bán\": màn Lập đơn hàng sẽ không hiện món này nữa." : ""),
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataSanPham();
                            GiaoDien.TaiLai(this);
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã SP để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa Sản phẩm: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSanPham.Text.Trim();

            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn Sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Sản phẩm [{maSP}] không?\n" +
                "Sản phẩm đã từng bán hoặc nhập kho thì không xóa được; khi đó hãy tick \"Ngừng bán\" rồi bấm Sửa.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = DbHelper.TaoKetNoi())
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MaSP", maSP);
                        int affected = cmdDelete.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Xóa Sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataSanPham();
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã SP để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Sản phẩm (Có thể sản phẩm đã có trong hóa đơn/đơn hàng): " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetFields()
        {
            txtMaSanPham.Text = "";
            txtTenSanPham.Text = "";
            txtBarcode.Text = "";
            txtMaNCC.Text = "";
            txtSoLuong.Text = "";
            txtNguongAnToan.Text = "10";
            txtGiaBan.Text = "";
            txtMaSKUShopee.Text = "";
            chkNgungBan.Checked = false;
            numBaoHanh.Value = 12;
            if (cboDanhMuc.Items.Count > 0) cboDanhMuc.SelectedIndex = -1;
            _tenFileAnhDangChon = null;
            pbAnhSanPham.Image?.Dispose();
            pbAnhSanPham.Image = null;
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void lblMaSanPham_Click(object sender, EventArgs e) { }
        private void txtMaSanPham_TextChanged(object sender, EventArgs e) { }
        private void lblTenSanPham_Click(object sender, EventArgs e) { }
        private void txtTenSanPham_TextChanged(object sender, EventArgs e) { }
        private void lblMaDanhMuc_Click(object sender, EventArgs e) { }
        private void lblMaNCC_Click(object sender, EventArgs e) { }
        private void txtMaNCC_TextChanged(object sender, EventArgs e) { }
        private void lblSoLuong_Click(object sender, EventArgs e) { }
        private void txtSoLuong_TextChanged(object sender, EventArgs e) { }
        private void lblGiaBan_Click(object sender, EventArgs e) { }
        private void txtGiaBan_TextChanged(object sender, EventArgs e) { }
    }
}
