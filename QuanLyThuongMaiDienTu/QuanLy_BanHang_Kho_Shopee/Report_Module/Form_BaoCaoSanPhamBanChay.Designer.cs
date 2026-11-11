namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    partial class Form_BaoCaoSanPhamBanChay
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
            this.lblLocAbc = new System.Windows.Forms.Label();
            this.cboAbc = new System.Windows.Forms.ComboBox();
            this.btnMuaKem = new System.Windows.Forms.Button();
            this.lblGhi = new System.Windows.Forms.Label();
            this.dgvMuaKem = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaKem)).BeginInit();
            this.txtChonTop = new System.Windows.Forms.TextBox();
            this.lblChonTop = new System.Windows.Forms.Label();
            this.dgvBaoCaoSanPhamBanChay = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXemDanhSach = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCaoSanPhamBanChay)).BeginInit();
            this.SuspendLayout();
            // 
            // txtChonTop
            // 
            this.txtChonTop.Location = new System.Drawing.Point(223, 118);
            this.txtChonTop.Name = "txtChonTop";
            this.txtChonTop.Size = new System.Drawing.Size(198, 20);
            this.txtChonTop.TabIndex = 86;
            this.txtChonTop.TextChanged += new System.EventHandler(this.txtChonTop_TextChanged);
            // 
            // lblChonTop
            // 
            this.lblChonTop.AutoSize = true;
            this.lblChonTop.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonTop.Location = new System.Drawing.Point(105, 118);
            this.lblChonTop.Name = "lblChonTop";
            this.lblChonTop.Size = new System.Drawing.Size(91, 21);
            this.lblChonTop.TabIndex = 85;
            this.lblChonTop.Text = "Chọn Top:";
            this.lblChonTop.Click += new System.EventHandler(this.lblChonTop_Click);
            // 
            // dgvBaoCaoSanPhamBanChay
            // 
            this.dgvBaoCaoSanPhamBanChay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCaoSanPhamBanChay.Location = new System.Drawing.Point(23, 267);
            this.dgvBaoCaoSanPhamBanChay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvBaoCaoSanPhamBanChay.Name = "dgvBaoCaoSanPhamBanChay";
            this.dgvBaoCaoSanPhamBanChay.Size = new System.Drawing.Size(754, 162);
            this.dgvBaoCaoSanPhamBanChay.TabIndex = 84;
            this.dgvBaoCaoSanPhamBanChay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaoCaoSanPhamBanChay_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(408, 211);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 83;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXemDanhSach
            // 
            this.btnXemDanhSach.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemDanhSach.Location = new System.Drawing.Point(214, 211);
            this.btnXemDanhSach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXemDanhSach.Name = "btnXemDanhSach";
            this.btnXemDanhSach.Size = new System.Drawing.Size(112, 27);
            this.btnXemDanhSach.TabIndex = 82;
            this.btnXemDanhSach.Text = "Xem danh sách";
            this.btnXemDanhSach.UseVisualStyleBackColor = true;
            this.btnXemDanhSach.Click += new System.EventHandler(this.btnXemDanhSach_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(122, 23);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(591, 36);
            this.lblTieuDe.TabIndex = 81;
            this.lblTieuDe.Text = "SẢN PHẨM BÁN CHẠY 12 THÁNG GẦN NHẤT";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.lblLocAbc);
            this.pnlBoSung.Controls.Add(this.cboAbc);
            this.pnlBoSung.Controls.Add(this.btnMuaKem);
            this.pnlBoSung.Controls.Add(this.lblGhi);
            this.pnlBoSung.Controls.Add(this.dgvMuaKem);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 200);
            this.pnlBoSung.TabIndex = 200;
            // 
            // lblLocAbc
            // 
            this.lblLocAbc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocAbc.Location = new System.Drawing.Point(20, 12);
            this.lblLocAbc.Name = "lblLocAbc";
            this.lblLocAbc.Size = new System.Drawing.Size(150, 24);
            this.lblLocAbc.TabIndex = 201;
            this.lblLocAbc.Text = "Lọc theo nhóm A / B / C:";
            // 
            // cboAbc
            // 
            this.cboAbc.Items.AddRange(new object[] {
            "Tất cả",
            "A",
            "B",
            "C"});
            this.cboAbc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAbc.FormattingEnabled = true;
            this.cboAbc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAbc.Location = new System.Drawing.Point(175, 9);
            this.cboAbc.Name = "cboAbc";
            this.cboAbc.Size = new System.Drawing.Size(110, 24);
            this.cboAbc.TabIndex = 202;
            // 
            // btnMuaKem
            // 
            this.btnMuaKem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMuaKem.Location = new System.Drawing.Point(305, 7);
            this.btnMuaKem.Name = "btnMuaKem";
            this.btnMuaKem.Size = new System.Drawing.Size(260, 30);
            this.btnMuaKem.TabIndex = 203;
            this.btnMuaKem.Text = "Tìm sản phẩm hay được mua kèm";
            this.btnMuaKem.UseVisualStyleBackColor = true;
            this.btnMuaKem.Click += new System.EventHandler(this.btnMuaKem_Click);
            // 
            // lblGhi
            // 
            this.lblGhi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhi.Location = new System.Drawing.Point(20, 44);
            this.lblGhi.Name = "lblGhi";
            this.lblGhi.Size = new System.Drawing.Size(760, 40);
            this.lblGhi.TabIndex = 204;
            this.lblGhi.Text = "";
            // 
            // dgvMuaKem
            // 
            this.dgvMuaKem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMuaKem.Location = new System.Drawing.Point(20, 88);
            this.dgvMuaKem.Name = "dgvMuaKem";
            this.dgvMuaKem.Size = new System.Drawing.Size(760, 105);
            this.dgvMuaKem.TabIndex = 205;
            // 
            // Form_BaoCaoSanPhamBanChay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtChonTop);
            this.Controls.Add(this.lblChonTop);
            this.Controls.Add(this.dgvBaoCaoSanPhamBanChay);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXemDanhSach);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_BaoCaoSanPhamBanChay";
            this.Load += new System.EventHandler(this.Form_BaoCaoSanPhamBanChay_Load);
            this.Text = "Sản phẩm bán chạy";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCaoSanPhamBanChay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaKem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtChonTop;
        private System.Windows.Forms.Label lblChonTop;
        private System.Windows.Forms.DataGridView dgvBaoCaoSanPhamBanChay;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXemDanhSach;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Label lblLocAbc;
        private System.Windows.Forms.ComboBox cboAbc;
        private System.Windows.Forms.Button btnMuaKem;
        private System.Windows.Forms.Label lblGhi;
        private System.Windows.Forms.DataGridView dgvMuaKem;
    }
}