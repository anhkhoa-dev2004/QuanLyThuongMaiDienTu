namespace QuanLy_BanHang_Kho_Shopee.Service_Module
{
    partial class Form_QuanLyTraHang
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
            this.lblSanPhamTra = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSoLuongTra = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblConTra = new System.Windows.Forms.Label();
            this.lblCamXuc = new System.Windows.Forms.Label();
            this.lblRuiRo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.txtSoTienHoan = new System.Windows.Forms.TextBox();
            this.lblSoTienHoan = new System.Windows.Forms.Label();
            this.txtLyDoTra = new System.Windows.Forms.TextBox();
            this.lblLyDoTra = new System.Windows.Forms.Label();
            this.txtMaDonHang = new System.Windows.Forms.TextBox();
            this.lblMaDonHang = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXacNhanTraHang = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSoTienHoan
            // 
            this.txtSoTienHoan.Location = new System.Drawing.Point(347, 272);
            this.txtSoTienHoan.Name = "txtSoTienHoan";
            this.txtSoTienHoan.Size = new System.Drawing.Size(198, 20);
            this.txtSoTienHoan.TabIndex = 140;
            // 
            // lblSoTienHoan
            // 
            this.lblSoTienHoan.AutoSize = true;
            this.lblSoTienHoan.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoTienHoan.Location = new System.Drawing.Point(190, 272);
            this.lblSoTienHoan.Name = "lblSoTienHoan";
            this.lblSoTienHoan.Size = new System.Drawing.Size(107, 21);
            this.lblSoTienHoan.TabIndex = 139;
            this.lblSoTienHoan.Text = "Số tiền hoàn:";
            // 
            // txtLyDoTra
            // 
            this.txtLyDoTra.Location = new System.Drawing.Point(347, 208);
            this.txtLyDoTra.Name = "txtLyDoTra";
            this.txtLyDoTra.Size = new System.Drawing.Size(198, 20);
            this.txtLyDoTra.TabIndex = 138;
            this.txtLyDoTra.TextChanged += new System.EventHandler(this.txtLyDoTra_TextChanged);
            // 
            // lblLyDoTra
            // 
            this.lblLyDoTra.AutoSize = true;
            this.lblLyDoTra.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLyDoTra.Location = new System.Drawing.Point(190, 208);
            this.lblLyDoTra.Name = "lblLyDoTra";
            this.lblLyDoTra.Size = new System.Drawing.Size(83, 21);
            this.lblLyDoTra.TabIndex = 137;
            this.lblLyDoTra.Text = "Lý do trả:";
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Location = new System.Drawing.Point(347, 142);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(198, 20);
            this.txtMaDonHang.TabIndex = 136;
            this.txtMaDonHang.TextChanged += new System.EventHandler(this.txtMaDonHang_TextChanged);
            // 
            // lblMaDonHang
            // 
            this.lblMaDonHang.AutoSize = true;
            this.lblMaDonHang.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDonHang.Location = new System.Drawing.Point(190, 142);
            this.lblMaDonHang.Name = "lblMaDonHang";
            this.lblMaDonHang.Size = new System.Drawing.Size(112, 21);
            this.lblMaDonHang.TabIndex = 135;
            this.lblMaDonHang.Text = "Mã đơn hàng:";
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(441, 378);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(159, 27);
            this.btnThoat.TabIndex = 134;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXacNhanTraHang
            // 
            this.btnXacNhanTraHang.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhanTraHang.Location = new System.Drawing.Point(145, 378);
            this.btnXacNhanTraHang.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXacNhanTraHang.Name = "btnXacNhanTraHang";
            this.btnXacNhanTraHang.Size = new System.Drawing.Size(171, 27);
            this.btnXacNhanTraHang.TabIndex = 133;
            this.btnXacNhanTraHang.Text = "Xác nhận trả hàng";
            this.btnXacNhanTraHang.UseVisualStyleBackColor = true;
            this.btnXacNhanTraHang.Click += new System.EventHandler(this.btnXacNhanTraHang_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(139, 46);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(578, 36);
            this.lblTieuDe.TabIndex = 132;
            this.lblTieuDe.Text = "QUẢN LÝ TRẢ HÀNG VÀ HOÀN TIỀN";
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.lblSanPhamTra);
            this.pnlBoSung.Controls.Add(this.cboSanPham);
            this.pnlBoSung.Controls.Add(this.lblSoLuongTra);
            this.pnlBoSung.Controls.Add(this.numSoLuong);
            this.pnlBoSung.Controls.Add(this.lblConTra);
            this.pnlBoSung.Controls.Add(this.lblCamXuc);
            this.pnlBoSung.Controls.Add(this.lblRuiRo);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 190);
            this.pnlBoSung.TabIndex = 200;
            // 
            // lblSanPhamTra
            // 
            this.lblSanPhamTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSanPhamTra.Location = new System.Drawing.Point(20, 12);
            this.lblSanPhamTra.Name = "lblSanPhamTra";
            this.lblSanPhamTra.Size = new System.Drawing.Size(110, 24);
            this.lblSanPhamTra.TabIndex = 201;
            this.lblSanPhamTra.Text = "Sản phẩm trả:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSanPham.Location = new System.Drawing.Point(130, 8);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(420, 24);
            this.cboSanPham.TabIndex = 202;
            this.cboSanPham.SelectedIndexChanged += new System.EventHandler(this.cboSanPham_SelectedIndexChanged);
            // 
            // lblSoLuongTra
            // 
            this.lblSoLuongTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuongTra.Location = new System.Drawing.Point(565, 12);
            this.lblSoLuongTra.Name = "lblSoLuongTra";
            this.lblSoLuongTra.Size = new System.Drawing.Size(70, 24);
            this.lblSoLuongTra.TabIndex = 203;
            this.lblSoLuongTra.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSoLuong.Location = new System.Drawing.Point(640, 8);
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 24);
            this.numSoLuong.TabIndex = 204;
            this.numSoLuong.ValueChanged += new System.EventHandler(this.numSoLuong_ValueChanged);
            // 
            // lblConTra
            // 
            this.lblConTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConTra.Location = new System.Drawing.Point(130, 38);
            this.lblConTra.Name = "lblConTra";
            this.lblConTra.Size = new System.Drawing.Size(650, 22);
            this.lblConTra.TabIndex = 205;
            this.lblConTra.Text = "Nhập mã đơn hàng để xem các sản phẩm trong đơn.";
            // 
            // lblCamXuc
            // 
            this.lblCamXuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamXuc.Location = new System.Drawing.Point(20, 64);
            this.lblCamXuc.Name = "lblCamXuc";
            this.lblCamXuc.Size = new System.Drawing.Size(760, 26);
            this.lblCamXuc.TabIndex = 206;
            this.lblCamXuc.Text = "";
            // 
            // lblRuiRo
            // 
            this.lblRuiRo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuiRo.Location = new System.Drawing.Point(20, 94);
            this.lblRuiRo.Name = "lblRuiRo";
            this.lblRuiRo.Size = new System.Drawing.Size(760, 90);
            this.lblRuiRo.TabIndex = 207;
            this.lblRuiRo.Text = "";
            // 
            // Form_QuanLyTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 640);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtSoTienHoan);
            this.Controls.Add(this.lblSoTienHoan);
            this.Controls.Add(this.txtLyDoTra);
            this.Controls.Add(this.lblLyDoTra);
            this.Controls.Add(this.txtMaDonHang);
            this.Controls.Add(this.lblMaDonHang);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXacNhanTraHang);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_QuanLyTraHang";
            this.Text = "Quản lý trả hàng và hoàn tiền";
            this.Load += new System.EventHandler(this.Form_QuanLyTraHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSoTienHoan;
        private System.Windows.Forms.Label lblSoTienHoan;
        private System.Windows.Forms.TextBox txtLyDoTra;
        private System.Windows.Forms.Label lblLyDoTra;
        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.Label lblMaDonHang;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXacNhanTraHang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Label lblSanPhamTra;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSoLuongTra;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label lblConTra;
        private System.Windows.Forms.Label lblCamXuc;
        private System.Windows.Forms.Label lblRuiRo;
    }
}