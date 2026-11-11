using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    public partial class Form_DocMaVanDonTuAnh : Form
    {
        private string _duongDanAnh;

        public Form_DocMaVanDonTuAnh()
        {
            InitializeComponent();
            GiaoDien.GanChoForm(this);
#if USE_TESSERACT
            lblGhiChu.Text = "Máy đọc chữ trên ảnh phiếu giao hàng bằng Tesseract (thư viện mã nguồn mở). Ảnh càng rõ đọc càng đúng; chữ đọc được hiện ở ô bên phải, sửa tay được trước khi tách mã và lưu.";
#endif
        }

        private void Form_DocMaVanDonTuAnh_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dh = AiHelper.Doc("SELECT MaDH FROM VanDon ORDER BY MaDH");
                AiHelper.NapCombo(cboDonHang, dh, "MaDH", "MaDH");
                if (dh.Rows.Count == 0) lblTrangThai.Text = "Chưa có vận đơn nào. Hãy tạo vận đơn ở form Vận đơn trước.";
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp", Title = "Chọn ảnh phiếu giao hàng" })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    using (Image tam = Image.FromStream(fs))
                    {
                        if (picAnh.Image != null) picAnh.Image.Dispose();
                        picAnh.Image = new Bitmap(tam);
                    }
                    _duongDanAnh = ofd.FileName;
                    lblTrangThai.Text = "Đã chọn: " + Path.GetFileName(ofd.FileName);
                }
                catch (Exception ex) { AiHelper.BaoLoi(ex); }
            }
        }

        private void btnNhanDien_Click(object sender, EventArgs e)
        {
            if (_duongDanAnh == null) { AiHelper.BaoTin("Hãy chọn ảnh trước."); return; }
#if USE_TESSERACT
            string thuMuc = Path.Combine(Application.StartupPath, "tessdata");
            if (!File.Exists(Path.Combine(thuMuc, "eng.traineddata")))
            {
                AiHelper.BaoTin("Không thấy file dữ liệu chữ tại:\n" + Path.Combine(thuMuc, "eng.traineddata") + "\n\n" +
                                "Tải file eng.traineddata ở github.com/tesseract-ocr/tessdata, bỏ vào thư mục \"tessdata\" của project rồi build lại.\n" +
                                "Trong lúc chưa có file, bạn có thể gõ/dán văn bản vào ô bên phải rồi bấm \"Tách mã từ văn bản\".");
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                using (Tesseract.TesseractEngine engine = new Tesseract.TesseractEngine(thuMuc, "eng", Tesseract.EngineMode.Default))
                using (Tesseract.Pix img = Tesseract.Pix.LoadFromFile(_duongDanAnh))
                using (Tesseract.Page page = engine.Process(img))
                {
                    txtVanBan.Text = page.GetText();
                }
                btnTimMa_Click(sender, e);
            }
            catch (DllNotFoundException)
            {
                AiHelper.BaoTin("Máy này thiếu file chạy của Tesseract.\n\n" +
                                "1) Kiểm tra cạnh file .exe có 2 thư mục \"x86\" và \"x64\" (build lại project sẽ tự có).\n" +
                                "2) Cài \"Microsoft Visual C++ Redistributable\" bản x86 và x64 của Microsoft rồi mở lại chương trình.");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
            finally { Cursor = Cursors.Default; }
#else
            AiHelper.BaoTin("Chưa bật chức năng đọc chữ từ ảnh. Cách bật:\n\n" +
                            "1) Visual Studio: Tools > NuGet Package Manager > cài gói \"Tesseract\" cho project.\n" +
                            "2) Tải file eng.traineddata (kho tessdata chính thức của Tesseract) bỏ vào thư mục \"tessdata\" cạnh file .exe.\n" +
                            "3) Project > Properties > Build > Conditional compilation symbols: thêm USE_TESSERACT.\n\n" +
                            "Trong lúc chưa bật, bạn có thể gõ/dán văn bản vào ô bên phải rồi bấm \"Tách mã từ văn bản\".");
#endif
        }

        private void btnTimMa_Click(object sender, EventArgs e)
        {
            lstUngVien.Items.Clear();
            string s = (txtVanBan.Text ?? "").ToUpperInvariant();
            HashSet<string> thay = new HashSet<string>();

            foreach (Match m in Regex.Matches(s, @"\b[A-Z][A-Z0-9]{9,19}\b"))
                if (Regex.IsMatch(m.Value, @"\d") && thay.Add(m.Value)) lstUngVien.Items.Add(m.Value);

            foreach (Match m in Regex.Matches(s, @"\b\d{10,15}\b"))
                if (thay.Add(m.Value)) lstUngVien.Items.Add(m.Value);

            if (lstUngVien.Items.Count == 1) lstUngVien.SelectedIndex = 0;
            lblTrangThai.Text = lstUngVien.Items.Count == 0 ? "Không tìm thấy mã nào trong văn bản." : "Tìm thấy " + lstUngVien.Items.Count + " mã ứng viên.";
        }

        private void lstUngVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUngVien.SelectedItem != null) txtMaVanDon.Text = lstUngVien.SelectedItem.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboDonHang.SelectedValue == null) { AiHelper.BaoTin("Hãy chọn đơn hàng có vận đơn."); return; }
            string ma = txtMaVanDon.Text.Trim();
            if (ma.Length == 0) { AiHelper.BaoTin("Hãy nhập hoặc chọn mã vận đơn."); return; }
            string maDH = cboDonHang.SelectedValue.ToString();
            try
            {
                string tenAnh = null;
                if (_duongDanAnh != null)
                {
                    string thuMuc = Path.Combine(Application.StartupPath, "Assets", "AnhVanDon");
                    Directory.CreateDirectory(thuMuc);
                    tenAnh = maDH + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(_duongDanAnh);
                    File.Copy(_duongDanAnh, Path.Combine(thuMuc, tenAnh), true);
                }
                int n = AiHelper.Chay("UPDATE VanDon SET MaVanDonShopee = @ma, AnhChupVanDon = ISNULL(@anh, AnhChupVanDon) WHERE MaDH = @dh",
                    AiHelper.P("@ma", ma), AiHelper.P("@anh", tenAnh), AiHelper.P("@dh", maDH));
                AiHelper.BaoTin(n > 0 ? "Đã lưu vào vận đơn của đơn " + maDH + "." : "Không tìm thấy vận đơn của đơn " + maDH + ".");
            }
            catch (Exception ex) { AiHelper.BaoLoi(ex); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
