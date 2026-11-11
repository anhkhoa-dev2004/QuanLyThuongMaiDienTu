namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    partial class Form_HoaDon
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
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtMaHoaDon = new System.Windows.Forms.TextBox();
            this.lblMaHoaDon = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnXuatPdf = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtTraCuuXml = new System.Windows.Forms.TextBox();
            this.lblTraCuuXml = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Location = new System.Drawing.Point(348, 169);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new System.Drawing.Size(198, 20);
            this.txtKhachHang.TabIndex = 105;
            this.txtKhachHang.TextChanged += new System.EventHandler(this.txtKhachHang_TextChanged);
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhachHang.Location = new System.Drawing.Point(191, 169);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(102, 21);
            this.lblKhachHang.TabIndex = 104;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // txtMaHoaDon
            // 
            this.txtMaHoaDon.Location = new System.Drawing.Point(348, 119);
            this.txtMaHoaDon.Name = "txtMaHoaDon";
            this.txtMaHoaDon.Size = new System.Drawing.Size(198, 20);
            this.txtMaHoaDon.TabIndex = 103;
            this.txtMaHoaDon.TextChanged += new System.EventHandler(this.txtMaHoaDon_TextChanged);
            // 
            // lblMaHoaDon
            // 
            this.lblMaHoaDon.AutoSize = true;
            this.lblMaHoaDon.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaHoaDon.Location = new System.Drawing.Point(191, 119);
            this.lblMaHoaDon.Name = "lblMaHoaDon";
            this.lblMaHoaDon.Size = new System.Drawing.Size(69, 21);
            this.lblMaHoaDon.TabIndex = 102;
            this.lblMaHoaDon.Text = "Mã HD:";
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(560, 364);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(120, 27);
            this.btnThoat.TabIndex = 101;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click_1);
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInHoaDon.Location = new System.Drawing.Point(90, 364);
            this.btnInHoaDon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(155, 27);
            this.btnInHoaDon.TabIndex = 100;
            this.btnInHoaDon.Text = "In hóa đơn";
            this.btnInHoaDon.UseVisualStyleBackColor = true;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click_1);
            // 
            // btnXuatPdf
            // 
            this.btnXuatPdf.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatPdf.Location = new System.Drawing.Point(300, 364);
            this.btnXuatPdf.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXuatPdf.Name = "btnXuatPdf";
            this.btnXuatPdf.Size = new System.Drawing.Size(180, 27);
            this.btnXuatPdf.TabIndex = 110;
            this.btnXuatPdf.Text = "Xuất PDF";
            this.btnXuatPdf.UseVisualStyleBackColor = true;
            this.btnXuatPdf.Click += new System.EventHandler(this.btnXuatPdf_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(139, 35);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(470, 36);
            this.lblTieuDe.TabIndex = 99;
            this.lblTieuDe.Text = "HÓA ĐƠN & HÓA ĐƠN ĐIỆN TỬ";
            // 
            // txtTraCuuXml
            // 
            this.txtTraCuuXml.Location = new System.Drawing.Point(348, 275);
            this.txtTraCuuXml.Name = "txtTraCuuXml";
            this.txtTraCuuXml.Size = new System.Drawing.Size(198, 20);
            this.txtTraCuuXml.TabIndex = 109;
            this.txtTraCuuXml.TextChanged += new System.EventHandler(this.txtTraCuuXml_TextChanged);
            // 
            // lblTraCuuXml
            // 
            this.lblTraCuuXml.AutoSize = true;
            this.lblTraCuuXml.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTraCuuXml.Location = new System.Drawing.Point(191, 275);
            this.lblTraCuuXml.Name = "lblTraCuuXml";
            this.lblTraCuuXml.Size = new System.Drawing.Size(117, 21);
            this.lblTraCuuXml.TabIndex = 108;
            this.lblTraCuuXml.Text = "Tra cứu XML:";
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(348, 225);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(198, 20);
            this.txtTongTien.TabIndex = 107;
            this.txtTongTien.TextChanged += new System.EventHandler(this.txtTongTien_TextChanged);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.Location = new System.Drawing.Point(191, 225);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(85, 21);
            this.lblTongTien.TabIndex = 106;
            this.lblTongTien.Text = "Tổng tiền:";
            // 
            // Form_HoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTraCuuXml);
            this.Controls.Add(this.lblTraCuuXml);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.txtKhachHang);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.txtMaHoaDon);
            this.Controls.Add(this.lblMaHoaDon);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXuatPdf);
            this.Controls.Add(this.btnInHoaDon);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_HoaDon";
            this.Text = "Hóa đơn điện tử";
            this.Load += new System.EventHandler(this.Form_HoaDon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.TextBox txtMaHoaDon;
        private System.Windows.Forms.Label lblMaHoaDon;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnXuatPdf;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtTraCuuXml;
        private System.Windows.Forms.Label lblTraCuuXml;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label lblTongTien;
    }
}