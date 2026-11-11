namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    partial class Form_LapPhieuXuat
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
            this.dgvChiTietPhieuXuat = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXacNhanXuatKho = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtMaKhoXuat = new System.Windows.Forms.TextBox();
            this.lblMaKhoXuat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPhieuXuat)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Location = new System.Drawing.Point(178, 94);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(198, 20);
            this.txtMaDonHang.TabIndex = 96;
            this.txtMaDonHang.TextChanged += new System.EventHandler(this.txtMaDonHang_TextChanged);
            // 
            // lblMaDonHang
            // 
            this.lblMaDonHang.AutoSize = true;
            this.lblMaDonHang.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDonHang.Location = new System.Drawing.Point(21, 94);
            this.lblMaDonHang.Name = "lblMaDonHang";
            this.lblMaDonHang.Size = new System.Drawing.Size(112, 21);
            this.lblMaDonHang.TabIndex = 95;
            this.lblMaDonHang.Text = "Mã đơn hàng:";
            this.lblMaDonHang.Click += new System.EventHandler(this.lblMaDonHang_Click);
            // 
            // dgvChiTietPhieuXuat
            // 
            this.dgvChiTietPhieuXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietPhieuXuat.Location = new System.Drawing.Point(25, 265);
            this.dgvChiTietPhieuXuat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvChiTietPhieuXuat.Name = "dgvChiTietPhieuXuat";
            this.dgvChiTietPhieuXuat.Size = new System.Drawing.Size(754, 162);
            this.dgvChiTietPhieuXuat.TabIndex = 94;
            this.dgvChiTietPhieuXuat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietPhieuXuat_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(432, 198);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(159, 27);
            this.btnThoat.TabIndex = 93;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXacNhanXuatKho
            // 
            this.btnXacNhanXuatKho.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhanXuatKho.Location = new System.Drawing.Point(194, 198);
            this.btnXacNhanXuatKho.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXacNhanXuatKho.Name = "btnXacNhanXuatKho";
            this.btnXacNhanXuatKho.Size = new System.Drawing.Size(171, 27);
            this.btnXacNhanXuatKho.TabIndex = 92;
            this.btnXacNhanXuatKho.Text = "Xác nhận xuất kho";
            this.btnXacNhanXuatKho.UseVisualStyleBackColor = true;
            this.btnXacNhanXuatKho.Click += new System.EventHandler(this.btnXacNhanXuatKho_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(172, 23);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(514, 36);
            this.lblTieuDe.TabIndex = 91;
            this.lblTieuDe.Text = "LẬP PHIẾU XUẤT KHO CHI TIẾT";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // txtMaKhoXuat
            // 
            this.txtMaKhoXuat.Location = new System.Drawing.Point(178, 144);
            this.txtMaKhoXuat.Name = "txtMaKhoXuat";
            this.txtMaKhoXuat.Size = new System.Drawing.Size(198, 20);
            this.txtMaKhoXuat.TabIndex = 98;
            this.txtMaKhoXuat.TextChanged += new System.EventHandler(this.txtMaKhoXuat_TextChanged);
            // 
            // lblMaKhoXuat
            // 
            this.lblMaKhoXuat.AutoSize = true;
            this.lblMaKhoXuat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKhoXuat.Location = new System.Drawing.Point(21, 144);
            this.lblMaKhoXuat.Name = "lblMaKhoXuat";
            this.lblMaKhoXuat.Size = new System.Drawing.Size(107, 21);
            this.lblMaKhoXuat.TabIndex = 97;
            this.lblMaKhoXuat.Text = "Mã kho xuất:";
            this.lblMaKhoXuat.Click += new System.EventHandler(this.lblMaKhoXuat_Click);
            // 
            // Form_LapPhieuXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMaKhoXuat);
            this.Controls.Add(this.lblMaKhoXuat);
            this.Controls.Add(this.txtMaDonHang);
            this.Controls.Add(this.lblMaDonHang);
            this.Controls.Add(this.dgvChiTietPhieuXuat);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXacNhanXuatKho);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_LapPhieuXuat";
            this.Text = "Lập phiếu xuất kho";
            this.Load += new System.EventHandler(this.Form_LapPhieuXuat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPhieuXuat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.Label lblMaDonHang;
        private System.Windows.Forms.DataGridView dgvChiTietPhieuXuat;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXacNhanXuatKho;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtMaKhoXuat;
        private System.Windows.Forms.Label lblMaKhoXuat;
    }
}