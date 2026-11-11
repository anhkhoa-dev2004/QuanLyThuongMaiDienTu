namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_ChiaNhomKhachHang
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
            this.btnGomNhom = new System.Windows.Forms.Button();
            this.lblSoNhom = new System.Windows.Forms.Label();
            this.numSoNhom = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNhom)).BeginInit();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblNgayTinh = new System.Windows.Forms.Label();
            this.dtpNgayTinh = new System.Windows.Forms.DateTimePicker();
            this.btnPhanTich = new System.Windows.Forms.Button();
            this.dgvRFM = new System.Windows.Forms.DataGridView();
            this.pnlBieuDo = new System.Windows.Forms.Panel();
            this.lblGoiY = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRFM)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CHIA NHÓM KHÁCH HÀNG";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Khách được chia 5 nhóm bằng nhau theo 3 điều: mua gần đây không, mua mấy lần, chi bao nhiêu. Đơn đã hủy không tính.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblNgayTinh
            // 
            this.lblNgayTinh.AutoSize = true;
            this.lblNgayTinh.Location = new System.Drawing.Point(22, 90);
            this.lblNgayTinh.Name = "lblNgayTinh";
            this.lblNgayTinh.TabIndex = 2;
            this.lblNgayTinh.Text = "Tính đến ngày:";
            this.lblNgayTinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpNgayTinh
            // 
            this.dtpNgayTinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTinh.Location = new System.Drawing.Point(150, 86);
            this.dtpNgayTinh.Size = new System.Drawing.Size(130, 26);
            this.dtpNgayTinh.Name = "dtpNgayTinh";
            this.dtpNgayTinh.TabIndex = 3;
            this.dtpNgayTinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPhanTich
            // 
            this.btnPhanTich.Location = new System.Drawing.Point(300, 84);
            this.btnPhanTich.Size = new System.Drawing.Size(190, 30);
            this.btnPhanTich.Name = "btnPhanTich";
            this.btnPhanTich.TabIndex = 4;
            this.btnPhanTich.Text = "Chia nhóm khách";
            this.btnPhanTich.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanTich.UseVisualStyleBackColor = true;
            this.btnPhanTich.Click += new System.EventHandler(this.btnPhanTich_Click);
            // 
            // dgvRFM
            // 
            this.dgvRFM.AllowUserToAddRows = false;
            this.dgvRFM.AllowUserToDeleteRows = false;
            this.dgvRFM.ReadOnly = true;
            this.dgvRFM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRFM.Location = new System.Drawing.Point(22, 132);
            this.dgvRFM.Size = new System.Drawing.Size(640, 430);
            this.dgvRFM.Name = "dgvRFM";
            this.dgvRFM.TabIndex = 5;
            this.dgvRFM.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom));
            this.dgvRFM.SelectionChanged += new System.EventHandler(this.dgvRFM_SelectionChanged);
            // 
            // pnlBieuDo
            // 
            this.pnlBieuDo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBieuDo.BackColor = System.Drawing.Color.White;
            this.pnlBieuDo.Location = new System.Drawing.Point(680, 132);
            this.pnlBieuDo.Size = new System.Drawing.Size(395, 250);
            this.pnlBieuDo.Name = "pnlBieuDo";
            this.pnlBieuDo.TabIndex = 6;
            this.pnlBieuDo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlBieuDo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBieuDo_Paint);
            // 
            // lblGoiY
            // 
            this.lblGoiY.AutoSize = false;
            this.lblGoiY.Location = new System.Drawing.Point(680, 392);
            this.lblGoiY.Size = new System.Drawing.Size(395, 220);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.TabIndex = 7;
            this.lblGoiY.Text = "";
            this.lblGoiY.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoiY.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblGoiY.ForeColor = System.Drawing.Color.DarkGreen;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 618);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnGomNhom
            // 
            this.btnGomNhom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGomNhom.Location = new System.Drawing.Point(500, 84);
            this.btnGomNhom.Name = "btnGomNhom";
            this.btnGomNhom.Size = new System.Drawing.Size(220, 30);
            this.btnGomNhom.TabIndex = 200;
            this.btnGomNhom.Text = "Máy tự gom nhóm (chỉ để xem)";
            this.btnGomNhom.UseVisualStyleBackColor = true;
            this.btnGomNhom.Click += new System.EventHandler(this.btnGomNhom_Click);
            // 
            // lblSoNhom
            // 
            this.lblSoNhom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoNhom.Location = new System.Drawing.Point(734, 90);
            this.lblSoNhom.Name = "lblSoNhom";
            this.lblSoNhom.Size = new System.Drawing.Size(70, 24);
            this.lblSoNhom.TabIndex = 201;
            this.lblSoNhom.Text = "Số nhóm:";
            // 
            // numSoNhom
            // 
            this.numSoNhom.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numSoNhom.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSoNhom.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numSoNhom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSoNhom.Location = new System.Drawing.Point(808, 86);
            this.numSoNhom.Name = "numSoNhom";
            this.numSoNhom.Size = new System.Drawing.Size(60, 24);
            this.numSoNhom.TabIndex = 202;
            // 
            // Form_ChiaNhomKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.btnGomNhom);
            this.Controls.Add(this.lblSoNhom);
            this.Controls.Add(this.numSoNhom);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblNgayTinh);
            this.Controls.Add(this.dtpNgayTinh);
            this.Controls.Add(this.btnPhanTich);
            this.Controls.Add(this.dgvRFM);
            this.Controls.Add(this.pnlBieuDo);
            this.Controls.Add(this.lblGoiY);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_ChiaNhomKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chia nhóm khách hàng";
            this.Load += new System.EventHandler(this.Form_ChiaNhomKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRFM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNhom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblNgayTinh;
        private System.Windows.Forms.DateTimePicker dtpNgayTinh;
        private System.Windows.Forms.Button btnPhanTich;
        private System.Windows.Forms.DataGridView dgvRFM;
        private System.Windows.Forms.Panel pnlBieuDo;
        private System.Windows.Forms.Label lblGoiY;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnGomNhom;
        private System.Windows.Forms.Label lblSoNhom;
        private System.Windows.Forms.NumericUpDown numSoNhom;
    }
}
