namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    partial class Form_LichSuGiaoDich
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
            this.lblLoaiGiaoDich = new System.Windows.Forms.Label();
            this.cboLoaiGiaoDich = new System.Windows.Forms.ComboBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.txtTuNgay = new System.Windows.Forms.TextBox();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.txtDenNgay = new System.Windows.Forms.TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtTongSoGiaoDich = new System.Windows.Forms.TextBox();
            this.dgvLichSuGiaoDich = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuGiaoDich)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(230, 20);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(440, 36);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "LỊCH SỬ GIAO DỊCH";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // lblLoaiGiaoDich
            // 
            this.lblLoaiGiaoDich.AutoSize = true;
            this.lblLoaiGiaoDich.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiGiaoDich.Location = new System.Drawing.Point(25, 79);
            this.lblLoaiGiaoDich.Name = "lblLoaiGiaoDich";
            this.lblLoaiGiaoDich.Size = new System.Drawing.Size(69, 19);
            this.lblLoaiGiaoDich.TabIndex = 1;
            this.lblLoaiGiaoDich.Text = "Loại GD:";
            this.lblLoaiGiaoDich.Click += new System.EventHandler(this.lblLoaiGiaoDich_Click);
            // 
            // cboLoaiGiaoDich
            // 
            this.cboLoaiGiaoDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGiaoDich.FormattingEnabled = true;
            this.cboLoaiGiaoDich.Location = new System.Drawing.Point(110, 76);
            this.cboLoaiGiaoDich.Name = "cboLoaiGiaoDich";
            this.cboLoaiGiaoDich.Size = new System.Drawing.Size(160, 21);
            this.cboLoaiGiaoDich.TabIndex = 2;
            this.cboLoaiGiaoDich.SelectedIndexChanged += new System.EventHandler(this.cboLoaiGiaoDich_SelectedIndexChanged);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuNgay.Location = new System.Drawing.Point(300, 79);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(62, 19);
            this.lblTuNgay.TabIndex = 3;
            this.lblTuNgay.Text = "Từ ngày:";
            this.lblTuNgay.Click += new System.EventHandler(this.lblTuNgay_Click);
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.Location = new System.Drawing.Point(380, 76);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(110, 20);
            this.txtTuNgay.TabIndex = 4;
            this.txtTuNgay.TextChanged += new System.EventHandler(this.txtTuNgay_TextChanged);
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenNgay.Location = new System.Drawing.Point(510, 79);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(69, 19);
            this.lblDenNgay.TabIndex = 5;
            this.lblDenNgay.Text = "Đến ngày:";
            this.lblDenNgay.Click += new System.EventHandler(this.lblDenNgay_Click);
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.Location = new System.Drawing.Point(590, 76);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(110, 20);
            this.txtDenNgay.TabIndex = 6;
            this.txtDenNgay.TextChanged += new System.EventHandler(this.txtDenNgay_TextChanged);
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuKhoa.Location = new System.Drawing.Point(25, 119);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(69, 19);
            this.lblTuKhoa.TabIndex = 7;
            this.lblTuKhoa.Text = "Từ khóa:";
            this.lblTuKhoa.Click += new System.EventHandler(this.lblTuKhoa_Click);
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Location = new System.Drawing.Point(110, 116);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(400, 20);
            this.txtTuKhoa.TabIndex = 8;
            this.txtTuKhoa.TextChanged += new System.EventHandler(this.txtTuKhoa_TextChanged);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(530, 113);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 27);
            this.btnTimKiem.TabIndex = 9;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(640, 113);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 27);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtTongSoGiaoDich
            // 
            this.txtTongSoGiaoDich.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongSoGiaoDich.Location = new System.Drawing.Point(750, 116);
            this.txtTongSoGiaoDich.Name = "txtTongSoGiaoDich";
            this.txtTongSoGiaoDich.ReadOnly = true;
            this.txtTongSoGiaoDich.Size = new System.Drawing.Size(125, 22);
            this.txtTongSoGiaoDich.TabIndex = 11;
            this.txtTongSoGiaoDich.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTongSoGiaoDich.TextChanged += new System.EventHandler(this.txtTongSoGiaoDich_TextChanged);
            // 
            // dgvLichSuGiaoDich
            // 
            this.dgvLichSuGiaoDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSuGiaoDich.Location = new System.Drawing.Point(25, 160);
            this.dgvLichSuGiaoDich.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvLichSuGiaoDich.Name = "dgvLichSuGiaoDich";
            this.dgvLichSuGiaoDich.Size = new System.Drawing.Size(850, 360);
            this.dgvLichSuGiaoDich.TabIndex = 12;
            this.dgvLichSuGiaoDich.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSuGiaoDich_CellContentClick);
            // 
            // Form_LichSuGiaoDich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 545);
            this.Controls.Add(this.dgvLichSuGiaoDich);
            this.Controls.Add(this.txtTongSoGiaoDich);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTuKhoa);
            this.Controls.Add(this.lblTuKhoa);
            this.Controls.Add(this.txtDenNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.txtTuNgay);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.cboLoaiGiaoDich);
            this.Controls.Add(this.lblLoaiGiaoDich);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_LichSuGiaoDich";
            this.Text = "Lịch sử giao dịch";
            this.Load += new System.EventHandler(this.Form_LichSuGiaoDich_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuGiaoDich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblLoaiGiaoDich;
        private System.Windows.Forms.ComboBox cboLoaiGiaoDich;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.TextBox txtTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.TextBox txtDenNgay;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox txtTongSoGiaoDich;
        private System.Windows.Forms.DataGridView dgvLichSuGiaoDich;
    }
}
