namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    partial class Form_DanhSachPhieuNhap
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
            this.lblNgayDatHang = new System.Windows.Forms.Label();
            this.dtpNgayDatHang = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChuNgayDat = new System.Windows.Forms.Label();
            this.txtMaNhanVien = new System.Windows.Forms.TextBox();
            this.lblMaNhanVien = new System.Windows.Forms.Label();
            this.txtMaDoiTac = new System.Windows.Forms.TextBox();
            this.txtMaPhieuNhap = new System.Windows.Forms.TextBox();
            this.lblMaDoiTac = new System.Windows.Forms.Label();
            this.lblMaPhieuNhap = new System.Windows.Forms.Label();
            this.dgvDanhSachPhieuNhap = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtNgayNhap = new System.Windows.Forms.TextBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.Location = new System.Drawing.Point(546, 84);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.Size = new System.Drawing.Size(198, 20);
            this.txtMaNhanVien.TabIndex = 60;
            this.txtMaNhanVien.TextChanged += new System.EventHandler(this.txtMaNhanVien_TextChanged);
            // 
            // lblMaNhanVien
            // 
            this.lblMaNhanVien.AutoSize = true;
            this.lblMaNhanVien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNhanVien.Location = new System.Drawing.Point(450, 83);
            this.lblMaNhanVien.Name = "lblMaNhanVien";
            this.lblMaNhanVien.Size = new System.Drawing.Size(93, 19);
            this.lblMaNhanVien.TabIndex = 59;
            this.lblMaNhanVien.Text = "Mã nhân viên:";
            this.lblMaNhanVien.Click += new System.EventHandler(this.lblMaNhanVien_Click);
            // 
            // txtMaDoiTac
            // 
            this.txtMaDoiTac.Location = new System.Drawing.Point(183, 125);
            this.txtMaDoiTac.Name = "txtMaDoiTac";
            this.txtMaDoiTac.Size = new System.Drawing.Size(198, 20);
            this.txtMaDoiTac.TabIndex = 58;
            this.txtMaDoiTac.TextChanged += new System.EventHandler(this.txtMaDoiTac_TextChanged);
            // 
            // txtMaPhieuNhap
            // 
            this.txtMaPhieuNhap.Location = new System.Drawing.Point(183, 85);
            this.txtMaPhieuNhap.Name = "txtMaPhieuNhap";
            this.txtMaPhieuNhap.Size = new System.Drawing.Size(198, 20);
            this.txtMaPhieuNhap.TabIndex = 57;
            this.txtMaPhieuNhap.TextChanged += new System.EventHandler(this.txtMaPhieuNhap_TextChanged);
            // 
            // lblMaDoiTac
            // 
            this.lblMaDoiTac.AutoSize = true;
            this.lblMaDoiTac.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDoiTac.Location = new System.Drawing.Point(45, 129);
            this.lblMaDoiTac.Name = "lblMaDoiTac";
            this.lblMaDoiTac.Size = new System.Drawing.Size(78, 19);
            this.lblMaDoiTac.TabIndex = 56;
            this.lblMaDoiTac.Text = "Mã đối tác:";
            this.lblMaDoiTac.Click += new System.EventHandler(this.lblMaDoiTac_Click);
            // 
            // lblMaPhieuNhap
            // 
            this.lblMaPhieuNhap.AutoSize = true;
            this.lblMaPhieuNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaPhieuNhap.Location = new System.Drawing.Point(45, 84);
            this.lblMaPhieuNhap.Name = "lblMaPhieuNhap";
            this.lblMaPhieuNhap.Size = new System.Drawing.Size(102, 19);
            this.lblMaPhieuNhap.TabIndex = 55;
            this.lblMaPhieuNhap.Text = "Mã phiếu nhập:";
            this.lblMaPhieuNhap.Click += new System.EventHandler(this.lblMaPhieuNhap_Click);
            // 
            // dgvDanhSachPhieuNhap
            // 
            this.dgvDanhSachPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachPhieuNhap.Location = new System.Drawing.Point(23, 267);
            this.dgvDanhSachPhieuNhap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvDanhSachPhieuNhap.Name = "dgvDanhSachPhieuNhap";
            this.dgvDanhSachPhieuNhap.Size = new System.Drawing.Size(754, 162);
            this.dgvDanhSachPhieuNhap.TabIndex = 54;
            this.dgvDanhSachPhieuNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachPhieuNhap_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(618, 222);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 53;
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
            this.btnXoa.TabIndex = 52;
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
            this.btnSua.TabIndex = 51;
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
            this.btnThem.TabIndex = 50;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(215, 21);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(413, 36);
            this.lblTieuDe.TabIndex = 49;
            this.lblTieuDe.Text = "DANH SÁCH PHIẾU NHẬP";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(183, 169);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(198, 20);
            this.txtTongTien.TabIndex = 64;
            this.txtTongTien.TextChanged += new System.EventHandler(this.txtTongTien_TextChanged);
            // 
            // txtNgayNhap
            // 
            this.txtNgayNhap.Location = new System.Drawing.Point(546, 130);
            this.txtNgayNhap.Name = "txtNgayNhap";
            this.txtNgayNhap.Size = new System.Drawing.Size(198, 20);
            this.txtNgayNhap.TabIndex = 63;
            this.txtNgayNhap.TextChanged += new System.EventHandler(this.txtNgayNhap_TextChanged);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.Location = new System.Drawing.Point(45, 173);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(68, 19);
            this.lblTongTien.TabIndex = 62;
            this.lblTongTien.Text = "Tổng tiền:";
            this.lblTongTien.Click += new System.EventHandler(this.lblTongTien_Click);
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayNhap.Location = new System.Drawing.Point(450, 130);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(78, 19);
            this.lblNgayNhap.TabIndex = 61;
            this.lblNgayNhap.Text = "Ngày nhập:";
            this.lblNgayNhap.Click += new System.EventHandler(this.lblNgayNhap_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.lblNgayDatHang);
            this.pnlBoSung.Controls.Add(this.dtpNgayDatHang);
            this.pnlBoSung.Controls.Add(this.lblGhiChuNgayDat);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 48);
            this.pnlBoSung.TabIndex = 200;
            // 
            // lblNgayDatHang
            // 
            this.lblNgayDatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayDatHang.Location = new System.Drawing.Point(20, 14);
            this.lblNgayDatHang.Name = "lblNgayDatHang";
            this.lblNgayDatHang.Size = new System.Drawing.Size(200, 24);
            this.lblNgayDatHang.TabIndex = 201;
            this.lblNgayDatHang.Text = "Ngày đã đặt hàng với NCC:";
            // 
            // dtpNgayDatHang
            // 
            this.dtpNgayDatHang.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayDatHang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDatHang.ShowCheckBox = true;
            this.dtpNgayDatHang.Checked = false;
            this.dtpNgayDatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayDatHang.Location = new System.Drawing.Point(220, 10);
            this.dtpNgayDatHang.Name = "dtpNgayDatHang";
            this.dtpNgayDatHang.Size = new System.Drawing.Size(150, 26);
            this.dtpNgayDatHang.TabIndex = 202;
            // 
            // lblGhiChuNgayDat
            // 
            this.lblGhiChuNgayDat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChuNgayDat.Location = new System.Drawing.Point(380, 8);
            this.lblGhiChuNgayDat.Name = "lblGhiChuNgayDat";
            this.lblGhiChuNgayDat.Size = new System.Drawing.Size(400, 36);
            this.lblGhiChuNgayDat.TabIndex = 203;
            this.lblGhiChuNgayDat.Text = "Đánh dấu vào ô nếu biết ngày đặt hàng. Tổng tiền tự tính từ chi tiết phiếu nhập, không sửa tay.";
            // 
            // Form_DanhSachPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 498);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.txtNgayNhap);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblNgayNhap);
            this.Controls.Add(this.txtMaNhanVien);
            this.Controls.Add(this.lblMaNhanVien);
            this.Controls.Add(this.txtMaDoiTac);
            this.Controls.Add(this.txtMaPhieuNhap);
            this.Controls.Add(this.lblMaDoiTac);
            this.Controls.Add(this.lblMaPhieuNhap);
            this.Controls.Add(this.dgvDanhSachPhieuNhap);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_DanhSachPhieuNhap";
            this.Text = "Danh sách phiếu nhập kho";
            this.Load += new System.EventHandler(this.Form_DanhSachPhieuNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaNhanVien;
        private System.Windows.Forms.Label lblMaNhanVien;
        private System.Windows.Forms.TextBox txtMaDoiTac;
        private System.Windows.Forms.TextBox txtMaPhieuNhap;
        private System.Windows.Forms.Label lblMaDoiTac;
        private System.Windows.Forms.Label lblMaPhieuNhap;
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtNgayNhap;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Label lblNgayDatHang;
        private System.Windows.Forms.DateTimePicker dtpNgayDatHang;
        private System.Windows.Forms.Label lblGhiChuNgayDat;
    }
}