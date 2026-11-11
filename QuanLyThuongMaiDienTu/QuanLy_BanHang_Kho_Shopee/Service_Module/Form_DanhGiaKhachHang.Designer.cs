namespace QuanLy_BanHang_Kho_Shopee.Service_Module
{
    partial class Form_DanhGiaKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMaDonHang = new System.Windows.Forms.Label();
            this.txtMaDonHang = new System.Windows.Forms.TextBox();
            this.btnMoDon = new System.Windows.Forms.Button();
            this.lblThongTinDon = new System.Windows.Forms.Label();
            this.grpDanhGia = new System.Windows.Forms.GroupBox();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSoSao = new System.Windows.Forms.Label();
            this.cboSoSao = new System.Windows.Forms.ComboBox();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.lblCamXuc = new System.Windows.Forms.Label();
            this.btnLuuDanhGia = new System.Windows.Forms.Button();
            this.grpKhaoSat = new System.Windows.Forms.GroupBox();
            this.lblLoiKhach = new System.Windows.Forms.Label();
            this.txtLoiKhach = new System.Windows.Forms.TextBox();
            this.lblHaoHung = new System.Windows.Forms.Label();
            this.cboHaoHung = new System.Windows.Forms.ComboBox();
            this.lblKhaoSat = new System.Windows.Forms.Label();
            this.btnLuuKhaoSat = new System.Windows.Forms.Button();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.dgvDanhGia = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhGia)).BeginInit();
            this.grpDanhGia.SuspendLayout();
            this.grpKhaoSat.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(880, 56);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐÁNH GIÁ VÀ KHẢO SÁT KHÁCH HÀNG";
            // 
            // lblMaDonHang
            // 
            this.lblMaDonHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDonHang.Location = new System.Drawing.Point(12, 72);
            this.lblMaDonHang.Name = "lblMaDonHang";
            this.lblMaDonHang.Size = new System.Drawing.Size(90, 22);
            this.lblMaDonHang.TabIndex = 1;
            this.lblMaDonHang.Text = "Mã đơn hàng:";
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDonHang.Location = new System.Drawing.Point(105, 68);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(140, 24);
            this.txtMaDonHang.TabIndex = 2;
            this.txtMaDonHang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaDonHang_KeyDown);
            // 
            // btnMoDon
            // 
            this.btnMoDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoDon.Location = new System.Drawing.Point(255, 66);
            this.btnMoDon.Name = "btnMoDon";
            this.btnMoDon.Size = new System.Drawing.Size(100, 28);
            this.btnMoDon.TabIndex = 3;
            this.btnMoDon.Text = "Mở đơn";
            this.btnMoDon.UseVisualStyleBackColor = true;
            this.btnMoDon.Click += new System.EventHandler(this.btnMoDon_Click);
            // 
            // lblThongTinDon
            // 
            this.lblThongTinDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinDon.Location = new System.Drawing.Point(365, 62);
            this.lblThongTinDon.Name = "lblThongTinDon";
            this.lblThongTinDon.Size = new System.Drawing.Size(505, 40);
            this.lblThongTinDon.TabIndex = 4;
            this.lblThongTinDon.Text = "Nhập mã đơn rồi bấm Enter hoặc \"Mở đơn\". Có thể bấm 1 dòng ở bảng dưới để mở đơn đó.";
            // 
            // grpDanhGia
            // 
            this.grpDanhGia.Controls.Add(this.lblSanPham);
            this.grpDanhGia.Controls.Add(this.cboSanPham);
            this.grpDanhGia.Controls.Add(this.lblSoSao);
            this.grpDanhGia.Controls.Add(this.cboSoSao);
            this.grpDanhGia.Controls.Add(this.lblNoiDung);
            this.grpDanhGia.Controls.Add(this.txtNoiDung);
            this.grpDanhGia.Controls.Add(this.lblCamXuc);
            this.grpDanhGia.Controls.Add(this.btnLuuDanhGia);
            this.grpDanhGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDanhGia.Location = new System.Drawing.Point(12, 108);
            this.grpDanhGia.Name = "grpDanhGia";
            this.grpDanhGia.Size = new System.Drawing.Size(430, 250);
            this.grpDanhGia.TabIndex = 5;
            this.grpDanhGia.Text = "Khách đánh giá sản phẩm";
            // 
            // lblSanPham
            // 
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSanPham.Location = new System.Drawing.Point(10, 28);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(72, 22);
            this.lblSanPham.TabIndex = 6;
            this.lblSanPham.Text = "Sản phẩm:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSanPham.Location = new System.Drawing.Point(85, 24);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(335, 24);
            this.cboSanPham.TabIndex = 7;
            this.cboSanPham.SelectedIndexChanged += new System.EventHandler(this.cboSanPham_SelectedIndexChanged);
            // 
            // lblSoSao
            // 
            this.lblSoSao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoSao.Location = new System.Drawing.Point(10, 62);
            this.lblSoSao.Name = "lblSoSao";
            this.lblSoSao.Size = new System.Drawing.Size(72, 22);
            this.lblSoSao.TabIndex = 8;
            this.lblSoSao.Text = "Số sao:";
            // 
            // cboSoSao
            // 
            this.cboSoSao.Items.AddRange(new object[] {
            "-- chọn --",
            "1 sao",
            "2 sao",
            "3 sao",
            "4 sao",
            "5 sao"});
            this.cboSoSao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoSao.FormattingEnabled = true;
            this.cboSoSao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSoSao.Location = new System.Drawing.Point(85, 58);
            this.cboSoSao.Name = "cboSoSao";
            this.cboSoSao.Size = new System.Drawing.Size(120, 24);
            this.cboSoSao.TabIndex = 9;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoiDung.Location = new System.Drawing.Point(10, 94);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(300, 22);
            this.lblNoiDung.TabIndex = 10;
            this.lblNoiDung.Text = "Lời đánh giá (có thể để trống):";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.MaxLength = 1000;
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoiDung.Location = new System.Drawing.Point(10, 116);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(410, 64);
            this.txtNoiDung.TabIndex = 11;
            this.txtNoiDung.TextChanged += new System.EventHandler(this.txtNoiDung_TextChanged);
            // 
            // lblCamXuc
            // 
            this.lblCamXuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamXuc.Location = new System.Drawing.Point(10, 184);
            this.lblCamXuc.Name = "lblCamXuc";
            this.lblCamXuc.Size = new System.Drawing.Size(410, 22);
            this.lblCamXuc.TabIndex = 12;
            this.lblCamXuc.Text = "";
            // 
            // btnLuuDanhGia
            // 
            this.btnLuuDanhGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuDanhGia.Location = new System.Drawing.Point(10, 208);
            this.btnLuuDanhGia.Name = "btnLuuDanhGia";
            this.btnLuuDanhGia.Size = new System.Drawing.Size(180, 30);
            this.btnLuuDanhGia.TabIndex = 13;
            this.btnLuuDanhGia.Text = "Lưu đánh giá";
            this.btnLuuDanhGia.UseVisualStyleBackColor = true;
            this.btnLuuDanhGia.Click += new System.EventHandler(this.btnLuuDanhGia_Click);
            // 
            // grpKhaoSat
            // 
            this.grpKhaoSat.Controls.Add(this.lblLoiKhach);
            this.grpKhaoSat.Controls.Add(this.txtLoiKhach);
            this.grpKhaoSat.Controls.Add(this.lblHaoHung);
            this.grpKhaoSat.Controls.Add(this.cboHaoHung);
            this.grpKhaoSat.Controls.Add(this.lblKhaoSat);
            this.grpKhaoSat.Controls.Add(this.btnLuuKhaoSat);
            this.grpKhaoSat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpKhaoSat.Location = new System.Drawing.Point(450, 108);
            this.grpKhaoSat.Name = "grpKhaoSat";
            this.grpKhaoSat.Size = new System.Drawing.Size(418, 250);
            this.grpKhaoSat.TabIndex = 14;
            this.grpKhaoSat.Text = "Khảo sát: \"Điều gì khiến bạn mua hôm nay?\"";
            // 
            // lblLoiKhach
            // 
            this.lblLoiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoiKhach.Location = new System.Drawing.Point(10, 28);
            this.lblLoiKhach.Name = "lblLoiKhach";
            this.lblLoiKhach.Size = new System.Drawing.Size(390, 22);
            this.lblLoiKhach.TabIndex = 15;
            this.lblLoiKhach.Text = "Câu khách trả lời (ghi đúng lời khách):";
            // 
            // txtLoiKhach
            // 
            this.txtLoiKhach.Multiline = true;
            this.txtLoiKhach.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLoiKhach.MaxLength = 500;
            this.txtLoiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoiKhach.Location = new System.Drawing.Point(10, 50);
            this.txtLoiKhach.Name = "txtLoiKhach";
            this.txtLoiKhach.Size = new System.Drawing.Size(398, 64);
            this.txtLoiKhach.TabIndex = 16;
            // 
            // lblHaoHung
            // 
            this.lblHaoHung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHaoHung.Location = new System.Drawing.Point(10, 122);
            this.lblHaoHung.Name = "lblHaoHung";
            this.lblHaoHung.Size = new System.Drawing.Size(290, 22);
            this.lblHaoHung.TabIndex = 17;
            this.lblHaoHung.Text = "Mức hào hứng (1 = bình thường, 5 = rất háo hức):";
            // 
            // cboHaoHung
            // 
            this.cboHaoHung.Items.AddRange(new object[] {
            "-- chọn --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "Không trả lời"});
            this.cboHaoHung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHaoHung.FormattingEnabled = true;
            this.cboHaoHung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHaoHung.Location = new System.Drawing.Point(300, 118);
            this.cboHaoHung.Name = "cboHaoHung";
            this.cboHaoHung.Size = new System.Drawing.Size(108, 24);
            this.cboHaoHung.TabIndex = 18;
            // 
            // lblKhaoSat
            // 
            this.lblKhaoSat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhaoSat.Location = new System.Drawing.Point(10, 150);
            this.lblKhaoSat.Name = "lblKhaoSat";
            this.lblKhaoSat.Size = new System.Drawing.Size(398, 54);
            this.lblKhaoSat.TabIndex = 19;
            this.lblKhaoSat.Text = "";
            // 
            // btnLuuKhaoSat
            // 
            this.btnLuuKhaoSat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuKhaoSat.Location = new System.Drawing.Point(10, 208);
            this.btnLuuKhaoSat.Name = "btnLuuKhaoSat";
            this.btnLuuKhaoSat.Size = new System.Drawing.Size(180, 30);
            this.btnLuuKhaoSat.TabIndex = 20;
            this.btnLuuKhaoSat.Text = "Lưu khảo sát";
            this.btnLuuKhaoSat.UseVisualStyleBackColor = true;
            this.btnLuuKhaoSat.Click += new System.EventHandler(this.btnLuuKhaoSat_Click);
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSach.Location = new System.Drawing.Point(12, 366);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(600, 22);
            this.lblDanhSach.TabIndex = 21;
            this.lblDanhSach.Text = "200 đánh giá gần nhất (bấm 1 dòng để mở đơn đó):";
            // 
            // dgvDanhGia
            // 
            this.dgvDanhGia.AllowUserToAddRows = false;
            this.dgvDanhGia.AllowUserToDeleteRows = false;
            this.dgvDanhGia.ReadOnly = true;
            this.dgvDanhGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhGia.Location = new System.Drawing.Point(12, 388);
            this.dgvDanhGia.Name = "dgvDanhGia";
            this.dgvDanhGia.Size = new System.Drawing.Size(856, 206);
            this.dgvDanhGia.TabIndex = 22;
            this.dgvDanhGia.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhGia_CellClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(768, 602);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.TabIndex = 23;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_DanhGiaKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 640);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.dgvDanhGia);
            this.Controls.Add(this.lblDanhSach);
            this.Controls.Add(this.grpKhaoSat);
            this.Controls.Add(this.grpDanhGia);
            this.Controls.Add(this.lblThongTinDon);
            this.Controls.Add(this.btnMoDon);
            this.Controls.Add(this.txtMaDonHang);
            this.Controls.Add(this.lblMaDonHang);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_DanhGiaKhachHang";
            this.Text = "Đánh giá và khảo sát khách hàng";
            this.Load += new System.EventHandler(this.Form_DanhGiaKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhGia)).EndInit();
            this.grpDanhGia.ResumeLayout(false);
            this.grpDanhGia.PerformLayout();
            this.grpKhaoSat.ResumeLayout(false);
            this.grpKhaoSat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMaDonHang;
        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.Button btnMoDon;
        private System.Windows.Forms.Label lblThongTinDon;
        private System.Windows.Forms.GroupBox grpDanhGia;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSoSao;
        private System.Windows.Forms.ComboBox cboSoSao;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Label lblCamXuc;
        private System.Windows.Forms.Button btnLuuDanhGia;
        private System.Windows.Forms.GroupBox grpKhaoSat;
        private System.Windows.Forms.Label lblLoiKhach;
        private System.Windows.Forms.TextBox txtLoiKhach;
        private System.Windows.Forms.Label lblHaoHung;
        private System.Windows.Forms.ComboBox cboHaoHung;
        private System.Windows.Forms.Label lblKhaoSat;
        private System.Windows.Forms.Button btnLuuKhaoSat;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhGia;
        private System.Windows.Forms.Button btnThoat;
    }
}
