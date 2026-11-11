namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_KhoXuatBatThuong
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
            this.btnQuetNhieuChiSo = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblSoNgay = new System.Windows.Forms.Label();
            this.numSoNgay = new System.Windows.Forms.NumericUpDown();
            this.lblNguong = new System.Windows.Forms.Label();
            this.numNguongZ = new System.Windows.Forms.NumericUpDown();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnQuet = new System.Windows.Forms.Button();
            this.btnLuuLog = new System.Windows.Forms.Button();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.lblLog = new System.Windows.Forms.Label();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNguongZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CẢNH BÁO KHO XUẤT BẤT THƯỜNG";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "So số lượng xuất mỗi ngày của từng sản phẩm với ngày bình thường; ngày nào xuất nhiều hoặc ít khác thường thì báo và lưu vào nhật ký được. Nút quét nhiều chỉ số xét cùng lúc số lượng xuất, mức so với trung bình và số phiếu trong ngày; kết quả chỉ để xem, không lưu.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblSoNgay
            // 
            this.lblSoNgay.AutoSize = true;
            this.lblSoNgay.Location = new System.Drawing.Point(22, 90);
            this.lblSoNgay.Name = "lblSoNgay";
            this.lblSoNgay.TabIndex = 2;
            this.lblSoNgay.Text = "Số ngày xem xét:";
            this.lblSoNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numSoNgay
            // 
            this.numSoNgay.DecimalPlaces = 0;
            this.numSoNgay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numSoNgay.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numSoNgay.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numSoNgay.Location = new System.Drawing.Point(160, 86);
            this.numSoNgay.Size = new System.Drawing.Size(70, 26);
            this.numSoNgay.Name = "numSoNgay";
            this.numSoNgay.TabIndex = 3;
            this.numSoNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblNguong
            // 
            this.lblNguong.AutoSize = true;
            this.lblNguong.Location = new System.Drawing.Point(250, 90);
            this.lblNguong.Name = "lblNguong";
            this.lblNguong.TabIndex = 4;
            this.lblNguong.Text = "Ngưỡng |Z| từ:";
            this.lblNguong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numNguongZ
            // 
            this.numNguongZ.DecimalPlaces = 1;
            this.numNguongZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numNguongZ.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNguongZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNguongZ.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.numNguongZ.Location = new System.Drawing.Point(370, 86);
            this.numNguongZ.Size = new System.Drawing.Size(70, 26);
            this.numNguongZ.Name = "numNguongZ";
            this.numNguongZ.TabIndex = 5;
            this.numNguongZ.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(460, 90);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.TabIndex = 6;
            this.lblDenNgay.Text = "Đến ngày:";
            this.lblDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(545, 86);
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 26);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.TabIndex = 7;
            this.dtpDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnQuet
            // 
            this.btnQuet.Location = new System.Drawing.Point(695, 84);
            this.btnQuet.Size = new System.Drawing.Size(170, 30);
            this.btnQuet.Name = "btnQuet";
            this.btnQuet.TabIndex = 8;
            this.btnQuet.Text = "Quét bất thường";
            this.btnQuet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuet.UseVisualStyleBackColor = true;
            this.btnQuet.Click += new System.EventHandler(this.btnQuet_Click);
            // 
            // btnLuuLog
            // 
            this.btnLuuLog.Location = new System.Drawing.Point(875, 84);
            this.btnLuuLog.Size = new System.Drawing.Size(200, 30);
            this.btnLuuLog.Name = "btnLuuLog";
            this.btnLuuLog.TabIndex = 9;
            this.btnLuuLog.Text = "Lưu vào nhật ký";
            this.btnLuuLog.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuLog.UseVisualStyleBackColor = true;
            this.btnLuuLog.Click += new System.EventHandler(this.btnLuuLog_Click);
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Location = new System.Drawing.Point(22, 128);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.TabIndex = 10;
            this.lblKetQua.Text = "Ngày xuất bất thường tìm thấy:";
            this.lblKetQua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(22, 152);
            this.dgvKetQua.Size = new System.Drawing.Size(1053, 220);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.TabIndex = 11;
            this.dgvKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(22, 384);
            this.lblLog.Name = "lblLog";
            this.lblLog.TabIndex = 12;
            this.lblLog.Text = "Nhật ký bất thường đã lưu (Log_BatThuongKho):";
            this.lblLog.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.ReadOnly = true;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Location = new System.Drawing.Point(22, 408);
            this.dgvLog.Size = new System.Drawing.Size(1053, 240);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.TabIndex = 13;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 658);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 14;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnQuetNhieuChiSo
            // 
            this.btnQuetNhieuChiSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuetNhieuChiSo.Location = new System.Drawing.Point(695, 118);
            this.btnQuetNhieuChiSo.Name = "btnQuetNhieuChiSo";
            this.btnQuetNhieuChiSo.Size = new System.Drawing.Size(380, 30);
            this.btnQuetNhieuChiSo.TabIndex = 200;
            this.btnQuetNhieuChiSo.Text = "Quét thêm theo nhiều chỉ số cùng lúc (chỉ hiển thị, không lưu)";
            this.btnQuetNhieuChiSo.UseVisualStyleBackColor = true;
            this.btnQuetNhieuChiSo.Click += new System.EventHandler(this.btnQuetNhieuChiSo_Click);
            // 
            // Form_KhoXuatBatThuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.btnQuetNhieuChiSo);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblSoNgay);
            this.Controls.Add(this.numSoNgay);
            this.Controls.Add(this.lblNguong);
            this.Controls.Add(this.numNguongZ);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.btnQuet);
            this.Controls.Add(this.btnLuuLog);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_KhoXuatBatThuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cảnh báo kho xuất bất thường";
            this.Load += new System.EventHandler(this.Form_KhoXuatBatThuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNguongZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblSoNgay;
        private System.Windows.Forms.NumericUpDown numSoNgay;
        private System.Windows.Forms.Label lblNguong;
        private System.Windows.Forms.NumericUpDown numNguongZ;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnQuet;
        private System.Windows.Forms.Button btnLuuLog;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnQuetNhieuChiSo;
    }
}
