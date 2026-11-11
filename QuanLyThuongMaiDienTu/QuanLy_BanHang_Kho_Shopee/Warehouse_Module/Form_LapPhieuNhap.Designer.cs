namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    partial class Form_LapPhieuNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBoSung = new System.Windows.Forms.Panel();
            this.lblGoiY = new System.Windows.Forms.Label();
            this.btnDienGoiY = new System.Windows.Forms.Button();
            this.lblCauHinhEoq = new System.Windows.Forms.Label();
            this.lblNgayDatHang = new System.Windows.Forms.Label();
            this.dtpNgayDatHang = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChuNgayDat = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtMaSanPham = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblMaSanPham = new System.Windows.Forms.Label();
            this.txtNhaCungCap = new System.Windows.Forms.TextBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.txtChonKho = new System.Windows.Forms.TextBox();
            this.txtMaPhieuNhap = new System.Windows.Forms.TextBox();
            this.lblChonKho = new System.Windows.Forms.Label();
            this.lblMaPhieuNhap = new System.Windows.Forms.Label();
            this.dgvChiTietPhieuNhap = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.lblGia = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPhieuNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(183, 169);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(198, 20);
            this.txtSoLuong.TabIndex = 80;
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Location = new System.Drawing.Point(546, 130);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.Size = new System.Drawing.Size(198, 20);
            this.txtMaSanPham.TabIndex = 79;
            this.txtMaSanPham.TextChanged += new System.EventHandler(this.txtMaSanPham_TextChanged);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(45, 173);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(67, 19);
            this.lblSoLuong.TabIndex = 78;
            this.lblSoLuong.Text = "Số lượng:";
            this.lblSoLuong.Click += new System.EventHandler(this.lblSoLuong_Click);
            // 
            // lblMaSanPham
            // 
            this.lblMaSanPham.AutoSize = true;
            this.lblMaSanPham.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaSanPham.Location = new System.Drawing.Point(450, 130);
            this.lblMaSanPham.Name = "lblMaSanPham";
            this.lblMaSanPham.Size = new System.Drawing.Size(55, 19);
            this.lblMaSanPham.TabIndex = 77;
            this.lblMaSanPham.Text = "Mã SP:";
            this.lblMaSanPham.Click += new System.EventHandler(this.lblMaSanPham_Click);
            // 
            // txtNhaCungCap
            // 
            this.txtNhaCungCap.Location = new System.Drawing.Point(546, 84);
            this.txtNhaCungCap.Name = "txtNhaCungCap";
            this.txtNhaCungCap.Size = new System.Drawing.Size(198, 20);
            this.txtNhaCungCap.TabIndex = 76;
            this.txtNhaCungCap.TextChanged += new System.EventHandler(this.txtNhaCungCap_TextChanged);
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhaCungCap.Location = new System.Drawing.Point(450, 83);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(64, 19);
            this.lblNhaCungCap.TabIndex = 75;
            this.lblNhaCungCap.Text = "Nhà CC:";
            this.lblNhaCungCap.Click += new System.EventHandler(this.lblNhaCungCap_Click);
            // 
            // txtChonKho
            // 
            this.txtChonKho.Location = new System.Drawing.Point(183, 125);
            this.txtChonKho.Name = "txtChonKho";
            this.txtChonKho.Size = new System.Drawing.Size(198, 20);
            this.txtChonKho.TabIndex = 74;
            this.txtChonKho.TextChanged += new System.EventHandler(this.txtChonKho_TextChanged);
            // 
            // txtMaPhieuNhap
            // 
            this.txtMaPhieuNhap.Location = new System.Drawing.Point(183, 85);
            this.txtMaPhieuNhap.Name = "txtMaPhieuNhap";
            this.txtMaPhieuNhap.Size = new System.Drawing.Size(198, 20);
            this.txtMaPhieuNhap.TabIndex = 73;
            this.txtMaPhieuNhap.TextChanged += new System.EventHandler(this.txtMaPhieuNhap_TextChanged);
            // 
            // lblChonKho
            // 
            this.lblChonKho.AutoSize = true;
            this.lblChonKho.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonKho.Location = new System.Drawing.Point(45, 129);
            this.lblChonKho.Name = "lblChonKho";
            this.lblChonKho.Size = new System.Drawing.Size(72, 19);
            this.lblChonKho.TabIndex = 72;
            this.lblChonKho.Text = "Chọn kho:";
            this.lblChonKho.Click += new System.EventHandler(this.lblChonKho_Click);
            // 
            // lblMaPhieuNhap
            // 
            this.lblMaPhieuNhap.AutoSize = true;
            this.lblMaPhieuNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaPhieuNhap.Location = new System.Drawing.Point(45, 84);
            this.lblMaPhieuNhap.Name = "lblMaPhieuNhap";
            this.lblMaPhieuNhap.Size = new System.Drawing.Size(102, 19);
            this.lblMaPhieuNhap.TabIndex = 71;
            this.lblMaPhieuNhap.Text = "Mã phiếu nhập:";
            this.lblMaPhieuNhap.Click += new System.EventHandler(this.lblMaPhieuNhap_Click);
            // 
            // dgvChiTietPhieuNhap
            // 
            this.dgvChiTietPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietPhieuNhap.Location = new System.Drawing.Point(23, 267);
            this.dgvChiTietPhieuNhap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvChiTietPhieuNhap.Name = "dgvChiTietPhieuNhap";
            this.dgvChiTietPhieuNhap.Size = new System.Drawing.Size(754, 162);
            this.dgvChiTietPhieuNhap.TabIndex = 70;
            this.dgvChiTietPhieuNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietPhieuNhap_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(618, 222);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 69;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(263, 222);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(118, 27);
            this.btnXoa.TabIndex = 68;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(424, 222);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(142, 27);
            this.btnSua.TabIndex = 67;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(71, 222);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(144, 27);
            this.btnThem.TabIndex = 66;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(160, 21);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(514, 36);
            this.lblTieuDe.TabIndex = 65;
            this.lblTieuDe.Text = "LẬP PHIẾU NHẬP KHO CHI TIẾT";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // txtGia
            // 
            this.txtGia.Location = new System.Drawing.Point(546, 174);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(198, 20);
            this.txtGia.TabIndex = 82;
            this.txtGia.TextChanged += new System.EventHandler(this.txtGia_TextChanged);
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGia.Location = new System.Drawing.Point(450, 174);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(33, 19);
            this.lblGia.TabIndex = 81;
            this.lblGia.Text = "Giá:";
            this.lblGia.Click += new System.EventHandler(this.lblGia_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.lblGoiY);
            this.pnlBoSung.Controls.Add(this.btnDienGoiY);
            this.pnlBoSung.Controls.Add(this.lblCauHinhEoq);
            this.pnlBoSung.Controls.Add(this.lblNgayDatHang);
            this.pnlBoSung.Controls.Add(this.dtpNgayDatHang);
            this.pnlBoSung.Controls.Add(this.lblGhiChuNgayDat);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 140);
            this.pnlBoSung.TabIndex = 200;
            // 
            // lblGoiY
            // 
            this.lblGoiY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoiY.Location = new System.Drawing.Point(20, 6);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.Size = new System.Drawing.Size(760, 46);
            this.lblGoiY.TabIndex = 201;
            this.lblGoiY.Text = "Nhập mã sản phẩm để xem gợi ý số lượng nhập.";
            // 
            // btnDienGoiY
            // 
            this.btnDienGoiY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDienGoiY.Location = new System.Drawing.Point(20, 58);
            this.btnDienGoiY.Name = "btnDienGoiY";
            this.btnDienGoiY.Size = new System.Drawing.Size(200, 30);
            this.btnDienGoiY.TabIndex = 202;
            this.btnDienGoiY.Text = "Điền số lượng gợi ý";
            this.btnDienGoiY.UseVisualStyleBackColor = true;
            this.btnDienGoiY.Click += new System.EventHandler(this.btnDienGoiY_Click);
            // 
            // lblCauHinhEoq
            // 
            this.lblCauHinhEoq.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCauHinhEoq.Location = new System.Drawing.Point(235, 62);
            this.lblCauHinhEoq.Name = "lblCauHinhEoq";
            this.lblCauHinhEoq.Size = new System.Drawing.Size(545, 34);
            this.lblCauHinhEoq.TabIndex = 203;
            this.lblCauHinhEoq.Text = "";
            // 
            // lblNgayDatHang
            // 
            this.lblNgayDatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayDatHang.Location = new System.Drawing.Point(20, 106);
            this.lblNgayDatHang.Name = "lblNgayDatHang";
            this.lblNgayDatHang.Size = new System.Drawing.Size(200, 24);
            this.lblNgayDatHang.TabIndex = 204;
            this.lblNgayDatHang.Text = "Ngày đã đặt hàng với NCC:";
            // 
            // dtpNgayDatHang
            // 
            this.dtpNgayDatHang.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayDatHang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDatHang.ShowCheckBox = true;
            this.dtpNgayDatHang.Checked = false;
            this.dtpNgayDatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayDatHang.Location = new System.Drawing.Point(220, 102);
            this.dtpNgayDatHang.Name = "dtpNgayDatHang";
            this.dtpNgayDatHang.Size = new System.Drawing.Size(150, 26);
            this.dtpNgayDatHang.TabIndex = 205;
            // 
            // lblGhiChuNgayDat
            // 
            this.lblGhiChuNgayDat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChuNgayDat.Location = new System.Drawing.Point(380, 100);
            this.lblGhiChuNgayDat.Name = "lblGhiChuNgayDat";
            this.lblGhiChuNgayDat.Size = new System.Drawing.Size(400, 36);
            this.lblGhiChuNgayDat.TabIndex = 206;
            this.lblGhiChuNgayDat.Text = "Đánh dấu vào ô nếu biết ngày đặt hàng. Để trống thì phiếu này không dùng để tính số ngày hàng về. Chỉ lưu khi tạo phiếu mới.";
            // 
            // Form_LapPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 590);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtGia);
            this.Controls.Add(this.lblGia);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.txtMaSanPham);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.lblMaSanPham);
            this.Controls.Add(this.txtNhaCungCap);
            this.Controls.Add(this.lblNhaCungCap);
            this.Controls.Add(this.txtChonKho);
            this.Controls.Add(this.txtMaPhieuNhap);
            this.Controls.Add(this.lblChonKho);
            this.Controls.Add(this.lblMaPhieuNhap);
            this.Controls.Add(this.dgvChiTietPhieuNhap);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_LapPhieuNhap";
            this.Text = "Lập phiếu nhập kho chi tiết";
            this.Load += new System.EventHandler(this.Form_LapPhieuNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPhieuNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtMaSanPham;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblMaSanPham;
        private System.Windows.Forms.TextBox txtNhaCungCap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.TextBox txtChonKho;
        private System.Windows.Forms.TextBox txtMaPhieuNhap;
        private System.Windows.Forms.Label lblChonKho;
        private System.Windows.Forms.Label lblMaPhieuNhap;
        private System.Windows.Forms.DataGridView dgvChiTietPhieuNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Label lblGoiY;
        private System.Windows.Forms.Button btnDienGoiY;
        private System.Windows.Forms.Label lblCauHinhEoq;
        private System.Windows.Forms.Label lblNgayDatHang;
        private System.Windows.Forms.DateTimePicker dtpNgayDatHang;
        private System.Windows.Forms.Label lblGhiChuNgayDat;
    }
}