namespace QuanLy_BanHang_Kho_Shopee.Category_Module
{
    partial class Form_SanPham
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
            this.lblThongSoAi = new System.Windows.Forms.Label();
            this.btnMoToiUuGia = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMaSanPham = new System.Windows.Forms.Label();
            this.txtMaSanPham = new System.Windows.Forms.TextBox();
            this.lblTenSanPham = new System.Windows.Forms.Label();
            this.txtTenSanPham = new System.Windows.Forms.TextBox();
            this.lblMaDanhMuc = new System.Windows.Forms.Label();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblNguongAnToan = new System.Windows.Forms.Label();
            this.txtNguongAnToan = new System.Windows.Forms.TextBox();
            this.lblMaNCC = new System.Windows.Forms.Label();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.lblMaSKUShopee = new System.Windows.Forms.Label();
            this.txtMaSKUShopee = new System.Windows.Forms.TextBox();
            this.pbAnhSanPham = new System.Windows.Forms.PictureBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnhSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(28, 18);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(320, 35);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ SẢN PHẨM";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // lblMaSanPham
            // 
            this.lblMaSanPham.AutoSize = true;
            this.lblMaSanPham.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblMaSanPham.Location = new System.Drawing.Point(28, 78);
            this.lblMaSanPham.Name = "lblMaSanPham";
            this.lblMaSanPham.Size = new System.Drawing.Size(52, 17);
            this.lblMaSanPham.TabIndex = 1;
            this.lblMaSanPham.Text = "Mã SP:";
            this.lblMaSanPham.Click += new System.EventHandler(this.lblMaSanPham_Click);
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtMaSanPham.Location = new System.Drawing.Point(160, 75);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.Size = new System.Drawing.Size(190, 24);
            this.txtMaSanPham.TabIndex = 2;
            this.txtMaSanPham.TextChanged += new System.EventHandler(this.txtMaSanPham_TextChanged);
            // 
            // lblTenSanPham
            // 
            this.lblTenSanPham.AutoSize = true;
            this.lblTenSanPham.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblTenSanPham.Location = new System.Drawing.Point(28, 114);
            this.lblTenSanPham.Name = "lblTenSanPham";
            this.lblTenSanPham.Size = new System.Drawing.Size(55, 17);
            this.lblTenSanPham.TabIndex = 3;
            this.lblTenSanPham.Text = "Tên SP:";
            this.lblTenSanPham.Click += new System.EventHandler(this.lblTenSanPham_Click);
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtTenSanPham.Location = new System.Drawing.Point(160, 111);
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Size = new System.Drawing.Size(340, 24);
            this.txtTenSanPham.TabIndex = 4;
            this.txtTenSanPham.TextChanged += new System.EventHandler(this.txtTenSanPham_TextChanged);
            // 
            // lblMaDanhMuc
            // 
            this.lblMaDanhMuc.AutoSize = true;
            this.lblMaDanhMuc.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblMaDanhMuc.Location = new System.Drawing.Point(28, 150);
            this.lblMaDanhMuc.Name = "lblMaDanhMuc";
            this.lblMaDanhMuc.Size = new System.Drawing.Size(72, 17);
            this.lblMaDanhMuc.TabIndex = 5;
            this.lblMaDanhMuc.Text = "Danh mục:";
            this.lblMaDanhMuc.Click += new System.EventHandler(this.lblMaDanhMuc_Click);
            // 
            // cboDanhMuc
            // 
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.cboDanhMuc.FormattingEnabled = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(160, 147);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(340, 25);
            this.cboDanhMuc.TabIndex = 6;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblBarcode.Location = new System.Drawing.Point(28, 186);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(61, 17);
            this.lblBarcode.TabIndex = 7;
            this.lblBarcode.Text = "Barcode:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(160, 183);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(340, 24);
            this.txtBarcode.TabIndex = 8;
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblGiaBan.Location = new System.Drawing.Point(28, 222);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(56, 17);
            this.lblGiaBan.TabIndex = 9;
            this.lblGiaBan.Text = "Giá bán:";
            this.lblGiaBan.Click += new System.EventHandler(this.lblGiaBan_Click);
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtGiaBan.Location = new System.Drawing.Point(160, 219);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(150, 24);
            this.txtGiaBan.TabIndex = 10;
            this.txtGiaBan.TextChanged += new System.EventHandler(this.txtGiaBan_TextChanged);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblSoLuong.Location = new System.Drawing.Point(330, 222);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(111, 17);
            this.lblSoLuong.TabIndex = 11;
            this.lblSoLuong.Text = "Tồn kho (all kho):";
            this.lblSoLuong.Click += new System.EventHandler(this.lblSoLuong_Click);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.SystemColors.Control;
            this.txtSoLuong.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtSoLuong.Location = new System.Drawing.Point(443, 219);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.ReadOnly = true;
            this.txtSoLuong.Size = new System.Drawing.Size(57, 24);
            this.txtSoLuong.TabIndex = 12;
            this.txtSoLuong.TabStop = false;
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);
            // 
            // lblNguongAnToan
            // 
            this.lblNguongAnToan.AutoSize = true;
            this.lblNguongAnToan.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblNguongAnToan.Location = new System.Drawing.Point(28, 258);
            this.lblNguongAnToan.Name = "lblNguongAnToan";
            this.lblNguongAnToan.Size = new System.Drawing.Size(106, 17);
            this.lblNguongAnToan.TabIndex = 13;
            this.lblNguongAnToan.Text = "Ngưỡng an toàn:";
            // 
            // txtNguongAnToan
            // 
            this.txtNguongAnToan.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtNguongAnToan.Location = new System.Drawing.Point(160, 255);
            this.txtNguongAnToan.Name = "txtNguongAnToan";
            this.txtNguongAnToan.Size = new System.Drawing.Size(150, 24);
            this.txtNguongAnToan.TabIndex = 14;
            this.txtNguongAnToan.Text = "10";
            // 
            // lblMaNCC
            // 
            this.lblMaNCC.AutoSize = true;
            this.lblMaNCC.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblMaNCC.Location = new System.Drawing.Point(28, 294);
            this.lblMaNCC.Name = "lblMaNCC";
            this.lblMaNCC.Size = new System.Drawing.Size(128, 17);
            this.lblMaNCC.TabIndex = 15;
            this.lblMaNCC.Text = "NCC nhập gần nhất:";
            this.lblMaNCC.Click += new System.EventHandler(this.lblMaNCC_Click);
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.BackColor = System.Drawing.SystemColors.Control;
            this.txtMaNCC.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtMaNCC.Location = new System.Drawing.Point(160, 291);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.ReadOnly = true;
            this.txtMaNCC.Size = new System.Drawing.Size(340, 24);
            this.txtMaNCC.TabIndex = 16;
            this.txtMaNCC.TabStop = false;
            this.txtMaNCC.TextChanged += new System.EventHandler(this.txtMaNCC_TextChanged);
            // 
            // lblMaSKUShopee
            // 
            this.lblMaSKUShopee.AutoSize = true;
            this.lblMaSKUShopee.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblMaSKUShopee.Location = new System.Drawing.Point(28, 330);
            this.lblMaSKUShopee.Name = "lblMaSKUShopee";
            this.lblMaSKUShopee.Size = new System.Drawing.Size(112, 17);
            this.lblMaSKUShopee.TabIndex = 17;
            this.lblMaSKUShopee.Text = "Mã SKU Shopee:";
            // 
            // txtMaSKUShopee
            // 
            this.txtMaSKUShopee.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.txtMaSKUShopee.Location = new System.Drawing.Point(160, 327);
            this.txtMaSKUShopee.Name = "txtMaSKUShopee";
            this.txtMaSKUShopee.Size = new System.Drawing.Size(340, 24);
            this.txtMaSKUShopee.TabIndex = 18;
            // 
            // pbAnhSanPham
            // 
            this.pbAnhSanPham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAnhSanPham.Location = new System.Drawing.Point(560, 75);
            this.pbAnhSanPham.Name = "pbAnhSanPham";
            this.pbAnhSanPham.Size = new System.Drawing.Size(200, 200);
            this.pbAnhSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAnhSanPham.TabIndex = 19;
            this.pbAnhSanPham.TabStop = false;
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnThem.Location = new System.Drawing.Point(28, 372);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 34);
            this.btnThem.TabIndex = 22;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnSua.Location = new System.Drawing.Point(188, 372);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(140, 34);
            this.btnSua.TabIndex = 23;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnXoa.Location = new System.Drawing.Point(348, 372);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(140, 34);
            this.btnXoa.TabIndex = 24;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.btnThoat.Location = new System.Drawing.Point(620, 372);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(140, 34);
            this.btnThoat.TabIndex = 25;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Location = new System.Drawing.Point(28, 424);
            this.dgvSanPham.MultiSelect = false;
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(696, 210);
            this.dgvSanPham.TabIndex = 26;
            this.dgvSanPham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPham_CellClick);
            this.dgvSanPham.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPham_CellDoubleClick);
            // 
            // lblThongSoAi
            // 
            this.lblThongSoAi.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongSoAi.Location = new System.Drawing.Point(512, 281);
            this.lblThongSoAi.Name = "lblThongSoAi";
            this.lblThongSoAi.Size = new System.Drawing.Size(236, 58);
            this.lblThongSoAi.TabIndex = 200;
            this.lblThongSoAi.Text = "";
            // 
            // btnMoToiUuGia
            // 
            this.btnMoToiUuGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoToiUuGia.Location = new System.Drawing.Point(512, 341);
            this.btnMoToiUuGia.Name = "btnMoToiUuGia";
            this.btnMoToiUuGia.Size = new System.Drawing.Size(190, 28);
            this.btnMoToiUuGia.TabIndex = 201;
            this.btnMoToiUuGia.Text = "Mở Thử giá bán";
            this.btnMoToiUuGia.UseVisualStyleBackColor = true;
            this.btnMoToiUuGia.Click += new System.EventHandler(this.btnMoToiUuGia_Click);
            // 
            // Form_SanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 660);
            this.Controls.Add(this.lblThongSoAi);
            this.Controls.Add(this.btnMoToiUuGia);
            this.Controls.Add(this.dgvSanPham);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.pbAnhSanPham);
            this.Controls.Add(this.txtMaSKUShopee);
            this.Controls.Add(this.lblMaSKUShopee);
            this.Controls.Add(this.txtMaNCC);
            this.Controls.Add(this.lblMaNCC);
            this.Controls.Add(this.txtNguongAnToan);
            this.Controls.Add(this.lblNguongAnToan);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.txtGiaBan);
            this.Controls.Add(this.lblGiaBan);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.cboDanhMuc);
            this.Controls.Add(this.lblMaDanhMuc);
            this.Controls.Add(this.txtTenSanPham);
            this.Controls.Add(this.lblTenSanPham);
            this.Controls.Add(this.txtMaSanPham);
            this.Controls.Add(this.lblMaSanPham);
            this.Controls.Add(this.lblTieuDe);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "Form_SanPham";
            this.Text = "Quản lý sản phẩm";
            this.Load += new System.EventHandler(this.Form_SanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAnhSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMaSanPham;
        private System.Windows.Forms.TextBox txtMaSanPham;
        private System.Windows.Forms.Label lblTenSanPham;
        private System.Windows.Forms.TextBox txtTenSanPham;
        private System.Windows.Forms.Label lblMaDanhMuc;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblNguongAnToan;
        private System.Windows.Forms.TextBox txtNguongAnToan;
        private System.Windows.Forms.Label lblMaNCC;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.Label lblMaSKUShopee;
        private System.Windows.Forms.TextBox txtMaSKUShopee;
        private System.Windows.Forms.PictureBox pbAnhSanPham;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Label lblThongSoAi;
        private System.Windows.Forms.Button btnMoToiUuGia;
    }
}
