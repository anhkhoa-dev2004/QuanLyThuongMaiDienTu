namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_DuBaoBanHang
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
            this.lblCachDuBao = new System.Windows.Forms.Label();
            this.cboCachDuBao = new System.Windows.Forms.ComboBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblLichSu = new System.Windows.Forms.Label();
            this.numLichSu = new System.Windows.Forms.NumericUpDown();
            this.lblDuBao = new System.Windows.Forms.Label();
            this.numDuBao = new System.Windows.Forms.NumericUpDown();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTB = new System.Windows.Forms.Label();
            this.numTB = new System.Windows.Forms.NumericUpDown();
            this.btnXem = new System.Windows.Forms.Button();
            this.lblLeadTime = new System.Windows.Forms.Label();
            this.numLeadTime = new System.Windows.Forms.NumericUpDown();
            this.lblZ = new System.Windows.Forms.Label();
            this.numZ = new System.Windows.Forms.NumericUpDown();
            this.btnTinhROP = new System.Windows.Forms.Button();
            this.lblThongSo = new System.Windows.Forms.Label();
            this.pnlBieuDo = new System.Windows.Forms.Panel();
            this.dgvDuBao = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuBao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeadTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuBao)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "DỰ BÁO BÁN VÀ LÚC NÀO ĐẶT HÀNG";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Dự báo mỗi ngày bán được bao nhiêu và tính mức phải đặt hàng cho từng sản phẩm.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Location = new System.Drawing.Point(22, 90);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.TabIndex = 2;
            this.lblSanPham.Text = "Sản phẩm:";
            this.lblSanPham.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(110, 86);
            this.cboSanPham.Size = new System.Drawing.Size(300, 26);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.TabIndex = 3;
            this.cboSanPham.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(425, 90);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.TabIndex = 4;
            this.lblLoai.Text = "Loại:";
            this.lblLoai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Items.AddRange(new object[] {
            "Xuất",
            "Nhập"});
            this.cboLoai.Location = new System.Drawing.Point(480, 86);
            this.cboLoai.Size = new System.Drawing.Size(110, 26);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.TabIndex = 5;
            this.cboLoai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblLichSu
            // 
            this.lblLichSu.AutoSize = true;
            this.lblLichSu.Location = new System.Drawing.Point(605, 90);
            this.lblLichSu.Name = "lblLichSu";
            this.lblLichSu.TabIndex = 6;
            this.lblLichSu.Text = "Số ngày lịch sử:";
            this.lblLichSu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numLichSu
            // 
            this.numLichSu.DecimalPlaces = 0;
            this.numLichSu.Maximum = new decimal(new int[] {
            730,
            0,
            0,
            0});
            this.numLichSu.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numLichSu.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numLichSu.Location = new System.Drawing.Point(740, 86);
            this.numLichSu.Size = new System.Drawing.Size(70, 26);
            this.numLichSu.Name = "numLichSu";
            this.numLichSu.TabIndex = 7;
            this.numLichSu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblDuBao
            // 
            this.lblDuBao.AutoSize = true;
            this.lblDuBao.Location = new System.Drawing.Point(830, 90);
            this.lblDuBao.Name = "lblDuBao";
            this.lblDuBao.TabIndex = 8;
            this.lblDuBao.Text = "Số ngày dự báo:";
            this.lblDuBao.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numDuBao
            // 
            this.numDuBao.DecimalPlaces = 0;
            this.numDuBao.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numDuBao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDuBao.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numDuBao.Location = new System.Drawing.Point(965, 86);
            this.numDuBao.Size = new System.Drawing.Size(70, 26);
            this.numDuBao.Name = "numDuBao";
            this.numDuBao.TabIndex = 9;
            this.numDuBao.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(22, 130);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.TabIndex = 10;
            this.lblDenNgay.Text = "Đến ngày:";
            this.lblDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(110, 126);
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 26);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.TabIndex = 11;
            this.dtpDenNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblTB
            // 
            this.lblTB.AutoSize = true;
            this.lblTB.Location = new System.Drawing.Point(260, 130);
            this.lblTB.Name = "lblTB";
            this.lblTB.TabIndex = 12;
            this.lblTB.Text = "Dự báo = trung bình mấy ngày gần nhất:";
            this.lblTB.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numTB
            // 
            this.numTB.DecimalPlaces = 0;
            this.numTB.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numTB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTB.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numTB.Location = new System.Drawing.Point(545, 126);
            this.numTB.Size = new System.Drawing.Size(60, 26);
            this.numTB.Name = "numTB";
            this.numTB.TabIndex = 13;
            this.numTB.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(630, 124);
            this.btnXem.Size = new System.Drawing.Size(160, 30);
            this.btnXem.Name = "btnXem";
            this.btnXem.TabIndex = 14;
            this.btnXem.Text = "Xem và dự báo";
            this.btnXem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // lblLeadTime
            // 
            this.lblLeadTime.AutoSize = true;
            this.lblLeadTime.Location = new System.Drawing.Point(22, 172);
            this.lblLeadTime.Name = "lblLeadTime";
            this.lblLeadTime.TabIndex = 15;
            this.lblLeadTime.Text = "Số ngày hàng về:";
            this.lblLeadTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numLeadTime
            // 
            this.numLeadTime.DecimalPlaces = 0;
            this.numLeadTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numLeadTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLeadTime.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numLeadTime.Location = new System.Drawing.Point(160, 168);
            this.numLeadTime.Size = new System.Drawing.Size(60, 26);
            this.numLeadTime.Name = "numLeadTime";
            this.numLeadTime.TabIndex = 16;
            this.numLeadTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Location = new System.Drawing.Point(240, 172);
            this.lblZ.Name = "lblZ";
            this.lblZ.TabIndex = 17;
            this.lblZ.Text = "Hệ số Z:";
            this.lblZ.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numZ
            // 
            this.numZ.DecimalPlaces = 2;
            this.numZ.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numZ.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numZ.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numZ.Value = new decimal(new int[] {
            165,
            0,
            0,
            131072});
            this.numZ.Location = new System.Drawing.Point(310, 168);
            this.numZ.Size = new System.Drawing.Size(80, 26);
            this.numZ.Name = "numZ";
            this.numZ.TabIndex = 18;
            this.numZ.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnTinhROP
            // 
            this.btnTinhROP.Location = new System.Drawing.Point(410, 166);
            this.btnTinhROP.Size = new System.Drawing.Size(270, 30);
            this.btnTinhROP.Name = "btnTinhROP";
            this.btnTinhROP.TabIndex = 19;
            this.btnTinhROP.Text = "Tính mức phải đặt hàng cho mọi sản phẩm";
            this.btnTinhROP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinhROP.UseVisualStyleBackColor = true;
            this.btnTinhROP.Click += new System.EventHandler(this.btnTinhROP_Click);
            // 
            // lblThongSo
            // 
            this.lblThongSo.AutoSize = false;
            this.lblThongSo.Location = new System.Drawing.Point(22, 208);
            this.lblThongSo.Size = new System.Drawing.Size(1053, 26);
            this.lblThongSo.Name = "lblThongSo";
            this.lblThongSo.TabIndex = 20;
            this.lblThongSo.Text = "";
            this.lblThongSo.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongSo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblThongSo.ForeColor = System.Drawing.Color.DarkBlue;
            // 
            // pnlBieuDo
            // 
            this.pnlBieuDo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBieuDo.BackColor = System.Drawing.Color.White;
            this.pnlBieuDo.Location = new System.Drawing.Point(22, 240);
            this.pnlBieuDo.Size = new System.Drawing.Size(1053, 240);
            this.pnlBieuDo.Name = "pnlBieuDo";
            this.pnlBieuDo.TabIndex = 21;
            this.pnlBieuDo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlBieuDo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBieuDo_Paint);
            // 
            // dgvDuBao
            // 
            this.dgvDuBao.AllowUserToAddRows = false;
            this.dgvDuBao.AllowUserToDeleteRows = false;
            this.dgvDuBao.ReadOnly = true;
            this.dgvDuBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuBao.Location = new System.Drawing.Point(22, 488);
            this.dgvDuBao.Size = new System.Drawing.Size(1053, 150);
            this.dgvDuBao.Name = "dgvDuBao";
            this.dgvDuBao.TabIndex = 22;
            this.dgvDuBao.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 656);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 23;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // lblCachDuBao
            // 
            this.lblCachDuBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCachDuBao.Location = new System.Drawing.Point(694, 172);
            this.lblCachDuBao.Name = "lblCachDuBao";
            this.lblCachDuBao.Size = new System.Drawing.Size(95, 24);
            this.lblCachDuBao.TabIndex = 200;
            this.lblCachDuBao.Text = "Cách dự báo:";
            // 
            // cboCachDuBao
            // 
            this.cboCachDuBao.Items.AddRange(new object[] {
            "Trung bình N ngày gần nhất",
            "Theo xu hướng các ngày gần đây",
            "Theo xu hướng và thứ trong tuần"});
            this.cboCachDuBao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCachDuBao.FormattingEnabled = true;
            this.cboCachDuBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCachDuBao.Location = new System.Drawing.Point(793, 168);
            this.cboCachDuBao.Name = "cboCachDuBao";
            this.cboCachDuBao.Size = new System.Drawing.Size(275, 24);
            this.cboCachDuBao.TabIndex = 201;
            // 
            // Form_DuBaoBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.lblCachDuBao);
            this.Controls.Add(this.cboCachDuBao);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblSanPham);
            this.Controls.Add(this.cboSanPham);
            this.Controls.Add(this.lblLoai);
            this.Controls.Add(this.cboLoai);
            this.Controls.Add(this.lblLichSu);
            this.Controls.Add(this.numLichSu);
            this.Controls.Add(this.lblDuBao);
            this.Controls.Add(this.numDuBao);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.lblTB);
            this.Controls.Add(this.numTB);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.lblLeadTime);
            this.Controls.Add(this.numLeadTime);
            this.Controls.Add(this.lblZ);
            this.Controls.Add(this.numZ);
            this.Controls.Add(this.btnTinhROP);
            this.Controls.Add(this.lblThongSo);
            this.Controls.Add(this.pnlBieuDo);
            this.Controls.Add(this.dgvDuBao);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_DuBaoBanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dự báo bán và lúc nào đặt hàng";
            this.Load += new System.EventHandler(this.Form_DuBaoBanHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuBao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeadTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuBao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblLichSu;
        private System.Windows.Forms.NumericUpDown numLichSu;
        private System.Windows.Forms.Label lblDuBao;
        private System.Windows.Forms.NumericUpDown numDuBao;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTB;
        private System.Windows.Forms.NumericUpDown numTB;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Label lblLeadTime;
        private System.Windows.Forms.NumericUpDown numLeadTime;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.NumericUpDown numZ;
        private System.Windows.Forms.Button btnTinhROP;
        private System.Windows.Forms.Label lblThongSo;
        private System.Windows.Forms.Panel pnlBieuDo;
        private System.Windows.Forms.DataGridView dgvDuBao;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblCachDuBao;
        private System.Windows.Forms.ComboBox cboCachDuBao;
    }
}
