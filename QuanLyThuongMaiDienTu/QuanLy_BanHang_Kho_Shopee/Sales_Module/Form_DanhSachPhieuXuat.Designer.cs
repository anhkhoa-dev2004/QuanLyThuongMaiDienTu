namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    partial class Form_DanhSachPhieuXuat
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
            this.txtMaDonHang = new System.Windows.Forms.TextBox();
            this.lblMaDonHang = new System.Windows.Forms.Label();
            this.dgvDanhSachPhieuXuat = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLapPhieuXuat = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuXuat)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Location = new System.Drawing.Point(178, 120);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(198, 20);
            this.txtMaDonHang.TabIndex = 90;
            this.txtMaDonHang.TextChanged += new System.EventHandler(this.txtMaDonHang_TextChanged);
            // 
            // lblMaDonHang
            // 
            this.lblMaDonHang.AutoSize = true;
            this.lblMaDonHang.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDonHang.Location = new System.Drawing.Point(21, 120);
            this.lblMaDonHang.Name = "lblMaDonHang";
            this.lblMaDonHang.Size = new System.Drawing.Size(142, 21);
            this.lblMaDonHang.TabIndex = 89;
            this.lblMaDonHang.Text = "Lọc theo Mã ĐH:";
            this.lblMaDonHang.Click += new System.EventHandler(this.lblMaDonHang_Click);
            // 
            // dgvDanhSachPhieuXuat
            // 
            this.dgvDanhSachPhieuXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachPhieuXuat.Location = new System.Drawing.Point(25, 267);
            this.dgvDanhSachPhieuXuat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvDanhSachPhieuXuat.Name = "dgvDanhSachPhieuXuat";
            this.dgvDanhSachPhieuXuat.Size = new System.Drawing.Size(754, 162);
            this.dgvDanhSachPhieuXuat.TabIndex = 88;
            this.dgvDanhSachPhieuXuat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachPhieuXuat_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(432, 200);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(159, 27);
            this.btnThoat.TabIndex = 87;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLapPhieuXuat
            // 
            this.btnLapPhieuXuat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLapPhieuXuat.Location = new System.Drawing.Point(194, 200);
            this.btnLapPhieuXuat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLapPhieuXuat.Name = "btnLapPhieuXuat";
            this.btnLapPhieuXuat.Size = new System.Drawing.Size(159, 27);
            this.btnLapPhieuXuat.TabIndex = 86;
            this.btnLapPhieuXuat.Text = "Lập phiếu xuất";
            this.btnLapPhieuXuat.UseVisualStyleBackColor = true;
            this.btnLapPhieuXuat.Click += new System.EventHandler(this.btnLapPhieuXuat_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(172, 25);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(412, 36);
            this.lblTieuDe.TabIndex = 85;
            this.lblTieuDe.Text = "DANH SÁCH PHIẾU XUẤT";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // Form_DanhSachPhieuXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMaDonHang);
            this.Controls.Add(this.lblMaDonHang);
            this.Controls.Add(this.dgvDanhSachPhieuXuat);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLapPhieuXuat);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_DanhSachPhieuXuat";
            this.Text = "Danh sách phiếu xuất kho";
            this.Load += new System.EventHandler(this.Form_DanhSachPhieuXuat_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuXuat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.Label lblMaDonHang;
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuXuat;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLapPhieuXuat;
        private System.Windows.Forms.Label lblTieuDe;
    }
}