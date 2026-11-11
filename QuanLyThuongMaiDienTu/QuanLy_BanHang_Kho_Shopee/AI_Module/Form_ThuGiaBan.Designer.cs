namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_ThuGiaBan
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
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTinh = new System.Windows.Forms.Button();
            this.lblGiaHienTai = new System.Windows.Forms.Label();
            this.dgvTheoThang = new System.Windows.Forms.DataGridView();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.btnLuuPED = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheoThang)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "THỬ GIÁ BÁN - GIẢM GIÁ THÌ BÁN THÊM ĐƯỢC BAO NHIÊU";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Hồi quy ln(số lượng bán) theo ln(giá bán) từng tháng. Cần ít nhất 3 tháng có bán và có từ 2 mức giá khác nhau; giá bán lấy theo giá thực tế trong chi tiết đơn hàng.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Location = new System.Drawing.Point(22, 92);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.TabIndex = 2;
            this.lblSanPham.Text = "Sản phẩm:";
            this.lblSanPham.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(110, 88);
            this.cboSanPham.Size = new System.Drawing.Size(320, 26);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.TabIndex = 3;
            this.cboSanPham.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(450, 92);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.TabIndex = 4;
            this.lblTuNgay.Text = "Từ ngày:";
            this.lblTuNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(525, 88);
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 26);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.TabIndex = 5;
            this.dtpTuNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(665, 92);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.TabIndex = 6;
            this.lblDenNgay.Text = "Đến ngày:";
            this.lblDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(745, 88);
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 26);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.TabIndex = 7;
            this.dtpDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnTinh
            // 
            this.btnTinh.Location = new System.Drawing.Point(885, 86);
            this.btnTinh.Size = new System.Drawing.Size(190, 30);
            this.btnTinh.Name = "btnTinh";
            this.btnTinh.TabIndex = 8;
            this.btnTinh.Text = "Tính mức nhạy giá";
            this.btnTinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinh.UseVisualStyleBackColor = true;
            this.btnTinh.Click += new System.EventHandler(this.btnTinh_Click);
            // 
            // lblGiaHienTai
            // 
            this.lblGiaHienTai.AutoSize = false;
            this.lblGiaHienTai.Location = new System.Drawing.Point(22, 128);
            this.lblGiaHienTai.Size = new System.Drawing.Size(1053, 26);
            this.lblGiaHienTai.Name = "lblGiaHienTai";
            this.lblGiaHienTai.TabIndex = 9;
            this.lblGiaHienTai.Text = "";
            this.lblGiaHienTai.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblGiaHienTai.ForeColor = System.Drawing.Color.DarkBlue;
            // 
            // dgvTheoThang
            // 
            this.dgvTheoThang.AllowUserToAddRows = false;
            this.dgvTheoThang.AllowUserToDeleteRows = false;
            this.dgvTheoThang.ReadOnly = true;
            this.dgvTheoThang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTheoThang.Location = new System.Drawing.Point(22, 160);
            this.dgvTheoThang.Size = new System.Drawing.Size(1053, 200);
            this.dgvTheoThang.Name = "dgvTheoThang";
            this.dgvTheoThang.TabIndex = 10;
            this.dgvTheoThang.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = false;
            this.lblKetQua.Location = new System.Drawing.Point(22, 372);
            this.lblKetQua.Size = new System.Drawing.Size(1053, 120);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.TabIndex = 11;
            this.lblKetQua.Text = "";
            this.lblKetQua.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblKetQua.ForeColor = System.Drawing.Color.DarkGreen;
            // 
            // btnLuuPED
            // 
            this.btnLuuPED.Location = new System.Drawing.Point(22, 502);
            this.btnLuuPED.Size = new System.Drawing.Size(260, 34);
            this.btnLuuPED.Name = "btnLuuPED";
            this.btnLuuPED.TabIndex = 12;
            this.btnLuuPED.Text = "Lưu mức nhạy giá vào sản phẩm";
            this.btnLuuPED.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuPED.UseVisualStyleBackColor = true;
            this.btnLuuPED.Click += new System.EventHandler(this.btnLuuPED_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 648);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 13;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_ThuGiaBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 690);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblSanPham);
            this.Controls.Add(this.cboSanPham);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.btnTinh);
            this.Controls.Add(this.lblGiaHienTai);
            this.Controls.Add(this.dgvTheoThang);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.btnLuuPED);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_ThuGiaBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thử giá bán";
            this.Load += new System.EventHandler(this.Form_ThuGiaBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheoThang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnTinh;
        private System.Windows.Forms.Label lblGiaHienTai;
        private System.Windows.Forms.DataGridView dgvTheoThang;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Button btnLuuPED;
        private System.Windows.Forms.Button btnThoat;
    }
}
