namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_DocMaVanDonTuAnh
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblDonHang = new System.Windows.Forms.Label();
            this.cboDonHang = new System.Windows.Forms.ComboBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.btnNhanDien = new System.Windows.Forms.Button();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblVanBan = new System.Windows.Forms.Label();
            this.txtVanBan = new System.Windows.Forms.TextBox();
            this.btnTimMa = new System.Windows.Forms.Button();
            this.lblUngVien = new System.Windows.Forms.Label();
            this.lstUngVien = new System.Windows.Forms.ListBox();
            this.lblMaChon = new System.Windows.Forms.Label();
            this.txtMaVanDon = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐỌC MÃ VẬN ĐƠN TỪ ẢNH";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Đọc chữ từ ảnh cần cài thêm thư viện Tesseract (bấm nút đọc ảnh để xem cách cài). Chưa cài vẫn dùng được: chọn ảnh, dán hoặc gõ chữ vào ô, tách mã, lưu.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblDonHang
            // 
            this.lblDonHang.AutoSize = true;
            this.lblDonHang.Location = new System.Drawing.Point(22, 90);
            this.lblDonHang.Name = "lblDonHang";
            this.lblDonHang.TabIndex = 2;
            this.lblDonHang.Text = "Đơn hàng có vận đơn:";
            this.lblDonHang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboDonHang
            // 
            this.cboDonHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonHang.FormattingEnabled = true;
            this.cboDonHang.Location = new System.Drawing.Point(200, 86);
            this.cboDonHang.Size = new System.Drawing.Size(190, 26);
            this.cboDonHang.Name = "cboDonHang";
            this.cboDonHang.TabIndex = 3;
            this.cboDonHang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(410, 84);
            this.btnChonAnh.Size = new System.Drawing.Size(240, 30);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.TabIndex = 4;
            this.btnChonAnh.Text = "Chọn ảnh phiếu giao hàng...";
            this.btnChonAnh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // btnNhanDien
            // 
            this.btnNhanDien.Location = new System.Drawing.Point(660, 84);
            this.btnNhanDien.Size = new System.Drawing.Size(170, 30);
            this.btnNhanDien.Name = "btnNhanDien";
            this.btnNhanDien.TabIndex = 5;
            this.btnNhanDien.Text = "Đọc chữ từ ảnh";
            this.btnNhanDien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhanDien.UseVisualStyleBackColor = true;
            this.btnNhanDien.Click += new System.EventHandler(this.btnNhanDien_Click);
            // 
            // picAnh
            // 
            this.picAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.Location = new System.Drawing.Point(22, 130);
            this.picAnh.Size = new System.Drawing.Size(500, 400);
            this.picAnh.Name = "picAnh";
            this.picAnh.TabIndex = 6;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = false;
            this.lblTrangThai.Location = new System.Drawing.Point(22, 540);
            this.lblTrangThai.Size = new System.Drawing.Size(500, 60);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.TabIndex = 7;
            this.lblTrangThai.Text = "";
            this.lblTrangThai.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.DarkBlue;
            // 
            // lblVanBan
            // 
            this.lblVanBan.AutoSize = true;
            this.lblVanBan.Location = new System.Drawing.Point(540, 130);
            this.lblVanBan.Name = "lblVanBan";
            this.lblVanBan.TabIndex = 8;
            this.lblVanBan.Text = "Văn bản đọc được (dán hoặc sửa tay được):";
            this.lblVanBan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtVanBan
            // 
            this.txtVanBan.Multiline = true;
            this.txtVanBan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVanBan.Location = new System.Drawing.Point(540, 156);
            this.txtVanBan.Size = new System.Drawing.Size(535, 150);
            this.txtVanBan.Name = "txtVanBan";
            this.txtVanBan.TabIndex = 9;
            this.txtVanBan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnTimMa
            // 
            this.btnTimMa.Location = new System.Drawing.Point(540, 314);
            this.btnTimMa.Size = new System.Drawing.Size(220, 30);
            this.btnTimMa.Name = "btnTimMa";
            this.btnTimMa.TabIndex = 10;
            this.btnTimMa.Text = "Tách mã từ văn bản";
            this.btnTimMa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimMa.UseVisualStyleBackColor = true;
            this.btnTimMa.Click += new System.EventHandler(this.btnTimMa_Click);
            // 
            // lblUngVien
            // 
            this.lblUngVien.AutoSize = true;
            this.lblUngVien.Location = new System.Drawing.Point(540, 354);
            this.lblUngVien.Name = "lblUngVien";
            this.lblUngVien.TabIndex = 11;
            this.lblUngVien.Text = "Mã ứng viên (bấm để chọn):";
            this.lblUngVien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lstUngVien
            // 
            this.lstUngVien.FormattingEnabled = true;
            this.lstUngVien.ItemHeight = 19;
            this.lstUngVien.Location = new System.Drawing.Point(540, 380);
            this.lstUngVien.Size = new System.Drawing.Size(535, 100);
            this.lstUngVien.Name = "lstUngVien";
            this.lstUngVien.TabIndex = 12;
            this.lstUngVien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUngVien.SelectedIndexChanged += new System.EventHandler(this.lstUngVien_SelectedIndexChanged);
            // 
            // lblMaChon
            // 
            this.lblMaChon.AutoSize = true;
            this.lblMaChon.Location = new System.Drawing.Point(540, 494);
            this.lblMaChon.Name = "lblMaChon";
            this.lblMaChon.TabIndex = 13;
            this.lblMaChon.Text = "Mã vận đơn Shopee:";
            this.lblMaChon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtMaVanDon
            // 
            this.txtMaVanDon.Location = new System.Drawing.Point(710, 490);
            this.txtMaVanDon.Size = new System.Drawing.Size(365, 26);
            this.txtMaVanDon.Name = "txtMaVanDon";
            this.txtMaVanDon.TabIndex = 14;
            this.txtMaVanDon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(540, 534);
            this.btnLuu.Size = new System.Drawing.Size(220, 36);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.TabIndex = 15;
            this.btnLuu.Text = "Lưu vào vận đơn";
            this.btnLuu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 658);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 16;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_DocMaVanDonTuAnh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblDonHang);
            this.Controls.Add(this.cboDonHang);
            this.Controls.Add(this.btnChonAnh);
            this.Controls.Add(this.btnNhanDien);
            this.Controls.Add(this.picAnh);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblVanBan);
            this.Controls.Add(this.txtVanBan);
            this.Controls.Add(this.btnTimMa);
            this.Controls.Add(this.lblUngVien);
            this.Controls.Add(this.lstUngVien);
            this.Controls.Add(this.lblMaChon);
            this.Controls.Add(this.txtMaVanDon);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_DocMaVanDonTuAnh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đọc mã vận đơn từ ảnh";
            this.Load += new System.EventHandler(this.Form_DocMaVanDonTuAnh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblDonHang;
        private System.Windows.Forms.ComboBox cboDonHang;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Button btnNhanDien;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblVanBan;
        private System.Windows.Forms.TextBox txtVanBan;
        private System.Windows.Forms.Button btnTimMa;
        private System.Windows.Forms.Label lblUngVien;
        private System.Windows.Forms.ListBox lstUngVien;
        private System.Windows.Forms.Label lblMaChon;
        private System.Windows.Forms.TextBox txtMaVanDon;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnThoat;
    }
}
