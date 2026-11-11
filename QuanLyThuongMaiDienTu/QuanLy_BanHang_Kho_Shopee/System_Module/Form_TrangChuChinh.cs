using QuanLy_BanHang_Kho_Shopee.AI_Module;
using QuanLy_BanHang_Kho_Shopee.Category_Module;
using QuanLy_BanHang_Kho_Shopee.Report_Module;
using QuanLy_BanHang_Kho_Shopee.Sales_Module;
using QuanLy_BanHang_Kho_Shopee.Service_Module;
using QuanLy_BanHang_Kho_Shopee.Warehouse_Module;
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

namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    public partial class Form_TrangChuChinh : Form
    {
        private readonly Font _fontNhom;
        private readonly Font _fontMuc;
        private ImageList _imageListMenu;

        public Form_TrangChuChinh()
        {
            InitializeComponent();
            GiaoDien.CoDinhCuaSo(this);

            _fontNhom = new Font(treeMenuChucNang.Font, FontStyle.Bold);
            _fontMuc = treeMenuChucNang.Font;

            KhoiTaoBoIconMenu();
            treeMenuChucNang.NodeMouseClick += treeMenuChucNang_NodeMouseClick;
            TaoNutMenu();
        }

        private const int RONG_MENU = 420;
        private Button _btnMenu;

        private void TaoNutMenu()
        {
            _btnMenu = new Button
            {
                Text = "☰ Ẩn menu",
                Size = new Size(118, 36),
                Location = new Point(560, 14),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            _btnMenu.FlatAppearance.BorderColor = Color.White;
            _btnMenu.Click += (s, e) => HienMenu(pnlSidebar.Width == 0);
            pnlHeader.Controls.Add(_btnMenu);
            _btnMenu.BringToFront();
        }

        private void HienMenu(bool hien)
        {
            pnlSidebar.Width = hien ? RONG_MENU : 0;
            _btnMenu.Text = hien ? "☰ Ẩn menu" : "☰ Hiện menu";
        }

        private void Form_TrangChuChinh_Load(object sender, EventArgs e)
        {
            if (!Session.DaDangNhap || string.IsNullOrEmpty(Session.MaND))
            {
                MessageBox.Show("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại!", "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                using (SqlConnection conn = DbHelper.TaoKetNoi())
                {
                    conn.Open();
                    string query = "INSERT INTO NhatKyHeThong (MaND, ThaoTac, ThoiGian) VALUES (@MaND, N'Đăng nhập trang chủ hệ thống', GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaND", Session.MaND);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi nhật ký hệ thống: " + AiHelper.LoiDeHieu(ex), "Có trục trặc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtThongTinUser.Text = $"{Session.HoTen} - {Session.TenQuyen}";
            LoadMenuTreeView();
            NapDashboard();
        }

        public void LamMoiMenuTheoQuyen()
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(LamMoiMenuTheoQuyen));
                return;
            }

            txtThongTinUser.Text = $"{Session.HoTen} - {Session.TenQuyen}";
            LoadMenuTreeView();
            NapDashboard();
        }

        private class ChucNang
        {
            public string Nhom;
            public string Ten;
            public Func<bool> Quyen;
            public Func<Form> Tao;
        }

        private static bool LaQuanTri()
        {
            return Session.QuyenKho && Session.QuyenBanHang && Session.QuyenBaoCao;
        }

        private static bool DungDuocTroLyAI()
        {
            return Session.QuyenBaoCao || Session.QuyenBanHang || Session.QuyenKho;
        }

        private static ChucNang CN(string nhom, string ten, Func<bool> quyen, Func<Form> tao)
        {
            return new ChucNang { Nhom = nhom, Ten = ten, Quyen = quyen, Tao = tao };
        }

        private const string NHOM_BAN = "1. Bán hàng";
        private const string NHOM_KHACH = "2. Khách hàng";
        private const string NHOM_KHO = "3. Kho hàng";
        private const string NHOM_BAO_CAO = "4. Báo cáo & hiểu khách";
        private const string NHOM_CONG_CU = "5. Công cụ";
        private const string NHOM_TAI_KHOAN = "6. Tài khoản";

        private const string CN_BANG_THONG_BAO = "Bảng thông báo: khách vui hay buồn, vì sao mua";
        private const string CN_TRO_LY_AI = "Trợ lý AI hỏi dữ liệu";

        private static readonly List<ChucNang> _dsChucNang = new List<ChucNang>
        {
            CN(NHOM_BAN, "Lập đơn hàng mới", () => Session.QuyenBanHang, () => new Form_LapDonHang()),
            CN(NHOM_BAN, "Danh sách đơn hàng", () => Session.QuyenBanHang, () => new Form_DanhSachDonHang()),
            CN(NHOM_BAN, "Hóa đơn & hóa đơn điện tử", () => Session.QuyenBanHang, () => new Form_HoaDon()),
            CN(NHOM_BAN, "Vận đơn & giao hàng", () => Session.QuyenBanHang, () => new Form_VanDon()),
            CN(NHOM_BAN, "Đọc mã vận đơn từ ảnh", () => Session.QuyenBanHang, () => new Form_DocMaVanDonTuAnh()),
            CN(NHOM_BAN, "Đợt sale", () => Session.QuyenBanHang, () => new Form_DotSale()),
            CN(NHOM_BAN, "Gợi ý combo (món hay mua chung)", () => Session.QuyenBanHang, () => new Form_GoiYCombo()),
            CN(NHOM_BAN, "Đơn đáng ngờ, nghi gian lận", () => Session.QuyenBanHang, () => new Form_DonDangNgo()),
            CN(NHOM_BAN, "Phí sàn theo kênh bán", LaQuanTri, () => new Form_PhiSan()),
            CN(NHOM_BAN, "Đối soát tiền sàn chuyển về", LaQuanTri, () => new Form_DoiSoatTienSan()),

            CN(NHOM_KHACH, "Danh sách khách hàng", () => Session.QuyenBanHang, () => new Form_QuanLyKhachHang()),
            CN(NHOM_KHACH, "Chia nhóm khách hàng", () => Session.QuyenBanHang, () => new Form_ChiaNhomKhachHang()),
            CN(NHOM_KHACH, "Đánh giá & khảo sát \"vì sao mua\"", () => Session.QuyenBanHang, () => new Form_DanhGiaKhachHang()),
            CN(NHOM_KHACH, "Trả hàng & hoàn tiền", () => Session.QuyenBanHang, () => new Form_QuanLyTraHang()),
            CN(NHOM_KHACH, "Bảo hành", () => Session.QuyenBanHang, () => new Form_QuanLyBaoHanh()),
            CN(NHOM_KHACH, "Đối tác & chi nhánh", () => Session.QuyenBanHang, () => new Form_QuanLyDoiTac()),

            CN(NHOM_KHO, "Sản phẩm", () => Session.QuyenKho, () => new Form_SanPham()),
            CN(NHOM_KHO, "Danh mục sản phẩm", () => Session.QuyenKho, () => new Form_DanhMuc()),
            CN(NHOM_KHO, "Nhà cung cấp", () => Session.QuyenKho, () => new Form_NhaCungCap()),
            CN(NHOM_KHO, "Lập phiếu nhập kho", () => Session.QuyenKho, () => new Form_LapPhieuNhap()),
            CN(NHOM_KHO, "Danh sách phiếu nhập", () => Session.QuyenKho, () => new Form_DanhSachPhieuNhap()),
            CN(NHOM_KHO, "Lập phiếu xuất kho", () => Session.QuyenKho, () => new Form_LapPhieuXuat()),
            CN(NHOM_KHO, "Danh sách phiếu xuất", () => Session.QuyenKho, () => new Form_DanhSachPhieuXuat()),
            CN(NHOM_KHO, "Kho & vị trí kệ", () => Session.QuyenKho, () => new Form_ViTriKho()),
            CN(NHOM_KHO, "Tra cứu tồn kho", () => Session.QuyenKho, () => new Form_TraCuuTonKho()),
            CN(NHOM_KHO, "Cảnh báo hết hàng / tồn nhiều", () => Session.QuyenKho, () => new Form_CanhBaoTonKho()),
            CN(NHOM_KHO, "Dự báo bán và lúc nào đặt hàng", () => Session.QuyenKho, () => new Form_DuBaoBanHang()),
            CN(NHOM_KHO, "Cảnh báo kho xuất bất thường", () => Session.QuyenKho, () => new Form_KhoXuatBatThuong()),
            CN(NHOM_KHO, "Kiểm kê và chuyển kho", () => Session.QuyenKho, () => new Form_KiemKeKho()),
            CN(NHOM_KHO, "Công nợ nhà cung cấp", LaQuanTri, () => new Form_CongNoNhaCungCap()),

            CN(NHOM_BAO_CAO, CN_BANG_THONG_BAO, () => Session.QuyenBaoCao, () => new Form_BangThongBao()),
            CN(NHOM_BAO_CAO, "Phân tích bán hàng và cảm xúc khách", () => Session.QuyenBaoCao, () => new Form_PhanTichBanHang()),
            CN(NHOM_BAO_CAO, "Cung - Cầu: hàng nhiều hay khách nhiều", () => Session.QuyenBaoCao || Session.QuyenKho, () => new Form_CungCau()),
            CN(NHOM_BAO_CAO, "Báo cáo doanh thu", () => Session.QuyenBaoCao, () => new Form_BaoCaoDoanhThu()),
            CN(NHOM_BAO_CAO, "Chi phí vận hành và lãi ròng", LaQuanTri, () => new Form_ChiPhiVanHanh()),
            CN(NHOM_BAO_CAO, "Sản phẩm bán chạy", () => Session.QuyenBaoCao, () => new Form_BaoCaoSanPhamBanChay()),
            CN(NHOM_BAO_CAO, "Báo cáo xuất - nhập - tồn", () => Session.QuyenBaoCao, () => new Form_BaoCaoXuatNhapTon()),
            CN(NHOM_BAO_CAO, "Đọc cảm xúc trong đánh giá", () => Session.QuyenBaoCao, () => new Form_DocCamXucDanhGia()),
            CN(NHOM_BAO_CAO, "Khách hay phàn nàn chuyện gì", () => Session.QuyenBaoCao, () => new Form_KhachHayPhanNan()),
            CN(NHOM_BAO_CAO, "Thử giá bán", () => Session.QuyenBaoCao, () => new Form_ThuGiaBan()),
            CN(NHOM_BAO_CAO, "Chấm điểm tổng hợp sản phẩm", () => Session.QuyenBaoCao, () => new Form_ChamDiemSanPham()),
            CN(NHOM_BAO_CAO, "Lịch sử giao dịch", () => Session.QuyenBaoCao || Session.QuyenKho || Session.QuyenBanHang, () => new Form_LichSuGiaoDich()),

            CN(NHOM_CONG_CU, CN_TRO_LY_AI, DungDuocTroLyAI, () => new Form_TroLyAI()),
            CN(NHOM_CONG_CU, "Dọn dữ liệu tự động", LaQuanTri, () => new Form_DonDuLieuTuDong()),
            CN(NHOM_CONG_CU, "Từ điển chữ viết tắt", LaQuanTri, () => new Form_TuDienVietTat()),

            CN(NHOM_TAI_KHOAN, "Phân quyền người dùng", LaQuanTri, () => new Form_PhanQuyen()),
            CN(NHOM_TAI_KHOAN, "Tạo tài khoản nhân viên", LaQuanTri, () => new Form_DangKy()),
            CN(NHOM_TAI_KHOAN, "Đổi mật khẩu", () => true, () => new Form_QuenMatKhau()),
        };

        private bool CoQuyenChucNang(string tenChucNang)
        {
            ChucNang cn = _dsChucNang.FirstOrDefault(c => c.Ten == tenChucNang);
            return cn == null || cn.Quyen();
        }

        private void LocMenuTheoPhanQuyen(TreeNode node)
        {
            for (int i = node.Nodes.Count - 1; i >= 0; i--)
            {
                TreeNode child = node.Nodes[i];

                if (child.Nodes.Count > 0)
                {
                    LocMenuTheoPhanQuyen(child);
                    if (child.Nodes.Count == 0)
                    {
                        node.Nodes.RemoveAt(i);
                    }
                }
                else if (!CoQuyenChucNang(child.Text))
                {
                    node.Nodes.RemoveAt(i);
                }
            }
        }

        private int DemSoChucNangLa(TreeNode node)
        {
            int tong = 0;
            foreach (TreeNode con in node.Nodes)
            {
                tong += con.Nodes.Count == 0 ? 1 : DemSoChucNangLa(con);
            }
            return tong;
        }

        private void GanIconChoCay(TreeNode node)
        {
            if (node.Level == 0)
            {
                node.ImageKey = node.SelectedImageKey = "root";
            }
            else if (node.Nodes.Count > 0)
            {
                node.ImageKey = node.SelectedImageKey = "group";
            }
            else
            {
                node.ImageKey = node.SelectedImageKey = "leaf";
            }

            bool laGocHoacNhom = node.Level == 0 || node.Nodes.Count > 0;
            node.NodeFont = laGocHoacNhom ? _fontNhom : _fontMuc;

            foreach (TreeNode con in node.Nodes)
            {
                GanIconChoCay(con);
            }
        }

        private void LoadMenuTreeView()
        {
            treeMenuChucNang.Nodes.Clear();

            TreeNode rootNode = new TreeNode("Cửa hàng của tôi");
            foreach (string nhom in _dsChucNang.Select(c => c.Nhom).Distinct())
            {
                TreeNode nodeNhom = new TreeNode(nhom);
                foreach (ChucNang c in _dsChucNang.Where(x => x.Nhom == nhom)) nodeNhom.Nodes.Add(c.Ten);
                rootNode.Nodes.Add(nodeNhom);
            }

            GanIconChoCay(rootNode);
            int tongSoChucNang = DemSoChucNangLa(rootNode);
            LocMenuTheoPhanQuyen(rootNode);
            int soChucNangHienThi = DemSoChucNangLa(rootNode);

            treeMenuChucNang.Nodes.Add(rootNode);
            treeMenuChucNang.ExpandAll();

            lblSoLuongMenu.Text = $"Bạn dùng được {soChucNangHienThi}/{tongSoChucNang} chức năng";
            CapNhatNoiDungChao(soChucNangHienThi, tongSoChucNang);
        }

        private void CapNhatNoiDungChao(int soChucNangHienThi, int tongSoChucNang)
        {
            lblContentTitle.Text = "Chào mừng trở lại!";
            lblContentDesc.Text = $"Tài khoản {Session.HoTen} ({Session.TenQuyen}) đang có quyền sử dụng {soChucNangHienThi}/{tongSoChucNang} chức năng trong hệ thống. Hãy chọn một mục ở menu bên trái để bắt đầu.";
        }

        private void CapNhatTieuDeNoiDung(string tieuDe, string moTa)
        {
            lblContentTitle.Text = tieuDe;
            lblContentDesc.Text = moTa;
        }

        private bool _dangMoForm;
        private DateTime _luc_DongForm = DateTime.MinValue;

        private void XuLyChuyenForm(string nodeText)
        {
            if (_dangMoForm) return;
            if ((DateTime.Now - _luc_DongForm).TotalMilliseconds < 600) return;
            _dangMoForm = true;
            try { XuLyChuyenFormGoc(nodeText); }
            finally { _dangMoForm = false; _luc_DongForm = DateTime.Now; }
        }

        private void XuLyChuyenFormGoc(string nodeText)
        {
            ChucNang cn = _dsChucNang.FirstOrDefault(c => c.Ten == nodeText);
            if (cn == null) return;
            if (!cn.Quyen())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền dùng chức năng này!", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DangMoTrongKhung(nodeText)) return;
            MoTrongKhung(cn.Tao(), nodeText);
        }

        private void treeMenuChucNang_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            if (e.Node.Nodes.Count == 0)
            {
                CapNhatTieuDeNoiDung(e.Node.Text, "Chức năng đang hiện trong khung bên phải.");
                XuLyChuyenForm(e.Node.Text);
                NapDashboard();
            }
            else
            {
                int soMuc = DemSoChucNangLa(e.Node);
                CapNhatTieuDeNoiDung(e.Node.Text, $"Nhóm chức năng gồm {soMuc} mục mà tài khoản của bạn có quyền sử dụng. Nhấp một mục để mở.");
            }
        }

        private void treeMenuChucNang_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 0)
            {
                CapNhatTieuDeNoiDung(e.Node.Text, "Chức năng đang hiện trong khung bên phải.");
                XuLyChuyenForm(e.Node.Text);
                NapDashboard();
            }
        }

        private void treeMenuChucNang_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            bool isRoot = e.Node.Level == 0;
            bool isGroup = e.Node.Level == 1;
            bool isSelected = (e.State & TreeNodeStates.Selected) != 0;

            Color mauNen = isSelected
                ? Color.FromArgb(238, 77, 45)
                : treeMenuChucNang.BackColor;

            Color mauChu = isSelected
                ? Color.White
                : isRoot
                    ? Color.White
                    : isGroup
                        ? Color.FromArgb(255, 200, 130)
                        : Color.Gainsboro;

            Font fontVe = isGroup || isRoot ? _fontNhom : _fontMuc;

            using (SolidBrush brushNen = new SolidBrush(mauNen))
            {
                e.Graphics.FillRectangle(brushNen, new Rectangle(0, e.Bounds.Top, treeMenuChucNang.ClientSize.Width, e.Bounds.Height));
            }

            TextRenderer.DrawText(e.Graphics, e.Node.Text, fontVe, e.Bounds, mauChu, mauNen,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPrefix | TextFormatFlags.NoClipping);
        }

        private void KhoiTaoBoIconMenu()
        {
            _imageListMenu = new ImageList();
            _imageListMenu.ImageSize = new Size(18, 18);
            _imageListMenu.ColorDepth = ColorDepth.Depth32Bit;

            _imageListMenu.Images.Add("root", VeIconGoc());
            _imageListMenu.Images.Add("group", VeIconNhom());
            _imageListMenu.Images.Add("leaf", VeIconMuc());

            treeMenuChucNang.ImageList = _imageListMenu;
        }

        private Bitmap VeIconGoc()
        {
            Bitmap bmp = new Bitmap(18, 18);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (SolidBrush ngoai = new SolidBrush(Color.FromArgb(238, 77, 45)))
                    g.FillEllipse(ngoai, 1, 1, 16, 16);
                using (SolidBrush trong = new SolidBrush(Color.White))
                    g.FillEllipse(trong, 6, 6, 6, 6);
            }
            return bmp;
        }

        private Bitmap VeIconNhom()
        {
            Bitmap bmp = new Bitmap(18, 18);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (SolidBrush tab = new SolidBrush(Color.FromArgb(255, 190, 90)))
                    g.FillRectangle(tab, 1, 3, 7, 3);
                using (SolidBrush than = new SolidBrush(Color.FromArgb(255, 170, 60)))
                    g.FillRectangle(than, 1, 5, 16, 11);
            }
            return bmp;
        }

        private Bitmap VeIconMuc()
        {
            Bitmap bmp = new Bitmap(18, 18);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (SolidBrush cham = new SolidBrush(Color.FromArgb(120, 190, 255)))
                    g.FillEllipse(cham, 6, 6, 6, 6);
            }
            return bmp;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất để quay lại màn hình đăng nhập không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Session.Clear();
                this.Close();
                foreach (Form f in Application.OpenForms)
                {
                    if (f is Form_DangNhap)
                    {
                        f.Show();
                        break;
                    }
                }
            }
        }

        private void lblTieuDe_Click(object sender, EventArgs e) { }
        private void txtThongTinUser_TextChanged(object sender, EventArgs e) { }
        private void lblThongTinUser_Click(object sender, EventArgs e) { }

        private Form _formDangNhung;
        private string _tenFormDangNhung = "";

        private bool DangMoTrongKhung(string tenChucNang)
        {
            return _formDangNhung != null && !_formDangNhung.IsDisposed && _tenFormDangNhung == tenChucNang;
        }

        private void MoTrongKhung(Form frm, string tenChucNang)
        {
            GoFormDangNhung();

            Size khach = frm.ClientSize;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ClientSize = khach;
            frm.Dock = DockStyle.None;

            lblContentTitle.Visible = false;
            lblContentDesc.Visible = false;
            if (_pnlDashboard != null) _pnlDashboard.Visible = false;

            if (khach.Width > pnlContent.ClientSize.Width && pnlSidebar.Width > 0) HienMenu(false);
            pnlContent.AutoScroll = true;
            pnlContent.AutoScrollPosition = new Point(0, 0);
            frm.Location = new Point(0, 0);

            _formDangNhung = frm;
            _tenFormDangNhung = tenChucNang;
            frm.FormClosed += FormDangNhung_FormClosed;
            pnlContent.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            BeginInvoke(new Action(() => pnlContent.AutoScrollPosition = new Point(0, 0)));
        }

        private void GoFormDangNhung()
        {
            if (_formDangNhung == null) return;
            Form cu = _formDangNhung;
            _formDangNhung = null;
            _tenFormDangNhung = "";
            cu.FormClosed -= FormDangNhung_FormClosed;
            if (!cu.IsDisposed) cu.Close();
        }

        private void FormDangNhung_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ReferenceEquals(sender, _formDangNhung)) return;
            string ten = _tenFormDangNhung;
            _formDangNhung = null;
            _tenFormDangNhung = "";
            if (this.IsDisposed || this.Disposing) return;

            lblContentTitle.Visible = true;
            lblContentDesc.Visible = true;
            HienMenu(true);
            CapNhatTieuDeNoiDung(ten, "Chức năng đã đóng. Bấm lại mục trong menu để mở.");
            NapDashboard();
        }

        private void treeMenuChucNang_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Node == null || e.Node.Nodes.Count > 0) return;
            XuLyChuyenForm(e.Node.Text);
        }

        private Panel _pnlDashboard;

        private void NapDashboard()
        {
            if (_formDangNhung != null && !_formDangNhung.IsDisposed) return;

            if (_pnlDashboard != null)
            {
                pnlContent.Controls.Remove(_pnlDashboard);
                _pnlDashboard.Dispose();
                _pnlDashboard = null;
            }
            if (!Session.DaDangNhap) return;

            List<string[]> the = new List<string[]>();
            if (Session.QuyenBanHang || Session.QuyenBaoCao) ThemTheBanHang(the);
            if (Session.QuyenBanHang) ThemThe(the, () =>
            {
                object n = AiHelper.LayGiaTri("SELECT COUNT(*) FROM BatThuong_DonHang_GianLan WHERE TrangThaiXuly = N'Chưa xử lý'");
                return new[] { "Đơn đáng ngờ chưa xử lý", Convert.ToString(n ?? 0), "Mở \"Đơn đáng ngờ, nghi gian lận\"" };
            });
            if (Session.QuyenKho)
            {
                ThemThe(the, () =>
                {
                    object d = AiHelper.LayGiaTri("SELECT COUNT(*) FROM vw_DuBaoSanPham WHERE TrangThai IN (N'KHẨN CẤP', N'ĐẶT HÀNG NGAY')");
                    return new[] { "Sản phẩm phải đặt hàng", Convert.ToString(d ?? 0), "Mở \"Cảnh báo hết hàng / tồn nhiều\"" };
                });
                ThemThe(the, () =>
                {
                    object h = AiHelper.LayGiaTri("SELECT COUNT(*) FROM vw_DuBaoSanPham WHERE Cau = N'ĐANG HOT - bán nhanh lên'");
                    return new[] { "Món đang hot", Convert.ToString(h ?? 0), "30 ngày gần bán nhanh hơn từ 20%" };
                });
                ThemThe(the, () =>
                {
                    object b = AiHelper.LayGiaTri("SELECT COUNT(*) FROM Log_BatThuongKho");
                    return new[] { "Ngày kho xuất bất thường đã lưu", Convert.ToString(b ?? 0), "Mở \"Cảnh báo kho xuất bất thường\"" };
                });
            }
            if (Session.QuyenBaoCao)
            {
                ThemThe(the, () =>
                {
                    object d = AiHelper.LayGiaTri(
                        "SELECT COUNT(*) FROM (SELECT MaSP FROM DanhGiaSanPham GROUP BY MaSP " +
                        "HAVING 100.0 * SUM(CASE WHEN SoSao >= 4 THEN 1 ELSE 0 END) / COUNT(*) < 60) x");
                    return new[] { "Sản phẩm khách đang chê", Convert.ToString(d ?? 0), "Dưới 60% đánh giá 4-5 sao" };
                });
                ThemThe(the, () =>
                {
                    DataTable t = AiHelper.Doc(
                        "SELECT COUNT(*) AS Tong, SUM(CASE WHEN NhomLyDo NOT IN (N'Nhu cầu thực dụng', N'Khác / không rõ') THEN 1 ELSE 0 END) AS CamXuc " +
                        "FROM PhanTich_LyDoMua");
                    int tong = Convert.ToInt32(t.Rows[0]["Tong"]);
                    if (tong == 0) return new[] { "Khách mua vì cảm xúc", "Chưa có", "Chưa có câu khảo sát nào" };
                    int camXuc = Convert.ToInt32(t.Rows[0]["CamXuc"]);
                    return new[] { "Khách mua vì cảm xúc", (100.0 * camXuc / tong).ToString("0") + "%", camXuc + "/" + tong + " câu \"vì sao mua\"" };
                });
            }
            bool quanTri = LaQuanTri();
            if (quanTri) ThemThe(the, () =>
            {
                object h = AiHelper.LayGiaTri("SELECT COUNT(*) FROM LichSuTruyVan_AI WHERE CAST(ThoiGian AS DATE) = CAST(GETDATE() AS DATE)");
                return new[] { "Câu hỏi gửi Trợ lý AI hôm nay", Convert.ToString(h ?? 0), "Câu hỏi có sẵn dùng ngay, không cần khóa" };
            });
            if (the.Count == 0) return;

            int soHang = (the.Count + 3) / 4;
            bool coNut = DungDuocTroLyAI();
            _pnlDashboard = new Panel { Left = 40, Top = 200, Width = 840, Height = soHang * 110 + (coNut ? 50 : 0), BackColor = Color.Transparent };
            for (int i = 0; i < the.Count; i++)
            {
                Panel o = new Panel { Left = (i % 4) * 210, Top = (i / 4) * 110, Width = 200, Height = 100, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
                ThanhPhu.Nhan(o, 8, 6, 182, 36, the[i][0], false);
                Label so = ThanhPhu.Nhan(o, 8, 42, 182, 30, the[i][1], true);
                so.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                so.ForeColor = Color.FromArgb(238, 77, 45);
                Label gc = ThanhPhu.Nhan(o, 8, 76, 182, 20, the[i][2], false);
                gc.Font = new Font("Segoe UI", 7.5F);
                gc.ForeColor = Color.Gray;
                _pnlDashboard.Controls.Add(o);
            }
            int yNut = soHang * 110 + 6, xNut = 0;
            if (Session.QuyenBaoCao)
            {
                ThanhPhu.Nut(_pnlDashboard, "Mở Bảng thông báo", xNut, yNut, 170, (s, e) =>
                {
                    MoTrongKhung(new Form_BangThongBao(), CN_BANG_THONG_BAO);
                });
                xNut += 180;
            }
            if (DungDuocTroLyAI())
            {
                ThanhPhu.Nut(_pnlDashboard, "Mở Trợ lý AI", xNut, yNut, 150, (s, e) =>
                {
                    MoTrongKhung(new Form_TroLyAI(), CN_TRO_LY_AI);
                });
            }
            pnlContent.Controls.Add(_pnlDashboard);
        }

        private static void ThemThe(List<string[]> the, Func<string[]> lay)
        {
            try { the.Add(lay()); }
            catch { }
        }

        private static void ThemTheBanHang(List<string[]> the)
        {
            try
            {
                object moiNhat = AiHelper.LayGiaTri("SELECT MAX(NgayDat) FROM DonHang");
                if (moiNhat == null || moiNhat == DBNull.Value) return;
                DateTime ngay = Convert.ToDateTime(moiNhat);
                DateTime tu = new DateTime(ngay.Year, ngay.Month, 1);
                DateTime den = tu.AddMonths(1).AddDays(-1);
                DataTable t = AiHelper.Doc(
                    "SELECT ISNULL(SUM(TongDoanhThu), 0) AS DoanhThu, ISNULL(SUM(SoDon), 0) AS SoDon, " +
                    "       ISNULL(SUM(TienShopNhan), 0) AS ThucNhan, ISNULL(SUM(LaiGop), 0) AS LaiGop FROM dbo.fn_ThongKeDoanhThu(@tu, @den)",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den));
                object huy = AiHelper.LayGiaTri(
                    "SELECT COUNT(*) FROM DonHang WHERE NgayDat >= @tu AND NgayDat < DATEADD(DAY, 1, @den) AND ISNULL(TrangThai, N'') LIKE N'%hủy%'",
                    AiHelper.P("@tu", tu), AiHelper.P("@den", den));
                DataTable dem = AiHelper.Doc(
                    "SELECT SUM(CASE WHEN TrangThai = N'Chờ xác nhận' THEN 1 ELSE 0 END) AS ChoXacNhan, " +
                    "       SUM(CASE WHEN TrangThai = N'Chờ lấy hàng' THEN 1 ELSE 0 END) AS ChoLayHang, " +
                    "       SUM(CASE WHEN TrangThai = N'Đang giao' THEN 1 ELSE 0 END) AS DangGiao FROM DonHang");
                decimal doanhThu = Convert.ToDecimal(t.Rows[0]["DoanhThu"]);
                decimal thucNhan = Convert.ToDecimal(t.Rows[0]["ThucNhan"]);
                decimal lai = Convert.ToDecimal(t.Rows[0]["LaiGop"]);
                int soDon = Convert.ToInt32(t.Rows[0]["SoDon"]);
                string thang = tu.ToString("MM/yyyy");
                the.Add(new[] { "Doanh thu tháng " + thang, (doanhThu / 1000000m).ToString("N1") + " triệu", "Tiền hàng, không tính đơn hủy" });
                the.Add(new[] { "Shop thực nhận tháng " + thang, (thucNhan / 1000000m).ToString("N1") + " triệu", "Lãi gộp " + (lai / 1000000m).ToString("N1") + " triệu" });
                the.Add(new[] { "Đơn tháng " + thang, soDon.ToString("N0"), "Đơn hủy trong tháng: " + Convert.ToString(huy ?? 0) });
                the.Add(new[] { "Đơn chờ xác nhận", Convert.ToString(dem.Rows[0]["ChoXacNhan"] == DBNull.Value ? 0 : dem.Rows[0]["ChoXacNhan"]), "Mở \"Danh sách đơn hàng\"" });
                the.Add(new[] { "Đơn chờ lấy / đang giao",
                                Convert.ToString(dem.Rows[0]["ChoLayHang"] == DBNull.Value ? 0 : dem.Rows[0]["ChoLayHang"]) + " / " +
                                Convert.ToString(dem.Rows[0]["DangGiao"] == DBNull.Value ? 0 : dem.Rows[0]["DangGiao"]),
                                "Mở \"Vận đơn & giao hàng\"" });
            }
            catch { }
        }
    }
}
