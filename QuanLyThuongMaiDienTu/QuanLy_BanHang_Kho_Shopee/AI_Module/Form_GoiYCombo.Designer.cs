namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_GoiYCombo
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
            this.lblSupport = new System.Windows.Forms.Label();
            this.numSupport = new System.Windows.Forms.NumericUpDown();
            this.lblConfidence = new System.Windows.Forms.Label();
            this.numConfidence = new System.Windows.Forms.NumericUpDown();
            this.lblLift = new System.Windows.Forms.Label();
            this.numLift = new System.Windows.Forms.NumericUpDown();
            this.btnKhaiPha = new System.Windows.Forms.Button();
            this.lblTongDon = new System.Windows.Forms.Label();
            this.dgvCombo = new System.Windows.Forms.DataGridView();
            this.lblGoiY = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSupport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConfidence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCombo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "GỢI Ý COMBO - MÓN HAY MUA CHUNG";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Tìm từng cặp 2 sản phẩm hay nằm chung một đơn để làm combo. Mỗi lần bấm tìm, danh sách cũ được thay bằng danh sách mới.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblSupport
            // 
            this.lblSupport.AutoSize = false;
            this.lblSupport.Location = new System.Drawing.Point(22, 86);
            this.lblSupport.Size = new System.Drawing.Size(215, 26);
            this.lblSupport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.TabIndex = 2;
            this.lblSupport.Text = "Cặp có ở ít nhất (tỷ lệ đơn):";
            this.lblSupport.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numSupport
            // 
            this.numSupport.DecimalPlaces = 4;
            this.numSupport.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numSupport.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSupport.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSupport.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numSupport.Location = new System.Drawing.Point(240, 86);
            this.numSupport.Size = new System.Drawing.Size(90, 26);
            this.numSupport.Name = "numSupport";
            this.numSupport.TabIndex = 3;
            this.numSupport.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblConfidence
            // 
            this.lblConfidence.AutoSize = false;
            this.lblConfidence.Location = new System.Drawing.Point(350, 86);
            this.lblConfidence.Size = new System.Drawing.Size(220, 26);
            this.lblConfidence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.TabIndex = 4;
            this.lblConfidence.Text = "Mua món chính thì mua kèm từ:";
            this.lblConfidence.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numConfidence
            // 
            this.numConfidence.DecimalPlaces = 2;
            this.numConfidence.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numConfidence.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numConfidence.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numConfidence.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.numConfidence.Location = new System.Drawing.Point(573, 86);
            this.numConfidence.Size = new System.Drawing.Size(80, 26);
            this.numConfidence.Name = "numConfidence";
            this.numConfidence.TabIndex = 5;
            this.numConfidence.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblLift
            // 
            this.lblLift.AutoSize = false;
            this.lblLift.Location = new System.Drawing.Point(673, 86);
            this.lblLift.Size = new System.Drawing.Size(180, 26);
            this.lblLift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLift.Name = "lblLift";
            this.lblLift.TabIndex = 6;
            this.lblLift.Text = "Gấp bình thường từ (lần):";
            this.lblLift.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numLift
            // 
            this.numLift.DecimalPlaces = 2;
            this.numLift.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numLift.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLift.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numLift.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numLift.Location = new System.Drawing.Point(856, 86);
            this.numLift.Size = new System.Drawing.Size(80, 26);
            this.numLift.Name = "numLift";
            this.numLift.TabIndex = 7;
            this.numLift.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnKhaiPha
            // 
            this.btnKhaiPha.Location = new System.Drawing.Point(22, 122);
            this.btnKhaiPha.Size = new System.Drawing.Size(220, 32);
            this.btnKhaiPha.Name = "btnKhaiPha";
            this.btnKhaiPha.TabIndex = 8;
            this.btnKhaiPha.Text = "Tìm món hay mua chung";
            this.btnKhaiPha.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhaiPha.UseVisualStyleBackColor = true;
            this.btnKhaiPha.Click += new System.EventHandler(this.btnKhaiPha_Click);
            // 
            // lblTongDon
            // 
            this.lblTongDon.AutoSize = false;
            this.lblTongDon.Location = new System.Drawing.Point(262, 122);
            this.lblTongDon.Size = new System.Drawing.Size(400, 32);
            this.lblTongDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTongDon.Name = "lblTongDon";
            this.lblTongDon.TabIndex = 9;
            this.lblTongDon.Text = "";
            this.lblTongDon.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDon.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // dgvCombo
            // 
            this.dgvCombo.AllowUserToAddRows = false;
            this.dgvCombo.AllowUserToDeleteRows = false;
            this.dgvCombo.ReadOnly = true;
            this.dgvCombo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCombo.Location = new System.Drawing.Point(22, 166);
            this.dgvCombo.Size = new System.Drawing.Size(1053, 300);
            this.dgvCombo.Name = "dgvCombo";
            this.dgvCombo.TabIndex = 10;
            this.dgvCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvCombo.SelectionChanged += new System.EventHandler(this.dgvCombo_SelectionChanged);
            // 
            // lblGoiY
            // 
            this.lblGoiY.AutoSize = false;
            this.lblGoiY.Location = new System.Drawing.Point(22, 476);
            this.lblGoiY.Size = new System.Drawing.Size(1053, 90);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.TabIndex = 11;
            this.lblGoiY.Text = "";
            this.lblGoiY.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoiY.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblGoiY.ForeColor = System.Drawing.Color.DarkGreen;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 618);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 12;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_GoiYCombo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblSupport);
            this.Controls.Add(this.numSupport);
            this.Controls.Add(this.lblConfidence);
            this.Controls.Add(this.numConfidence);
            this.Controls.Add(this.lblLift);
            this.Controls.Add(this.numLift);
            this.Controls.Add(this.btnKhaiPha);
            this.Controls.Add(this.lblTongDon);
            this.Controls.Add(this.dgvCombo);
            this.Controls.Add(this.lblGoiY);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_GoiYCombo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gợi ý combo (món hay mua chung)";
            this.Load += new System.EventHandler(this.Form_GoiYCombo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSupport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConfidence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCombo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblSupport;
        private System.Windows.Forms.NumericUpDown numSupport;
        private System.Windows.Forms.Label lblConfidence;
        private System.Windows.Forms.NumericUpDown numConfidence;
        private System.Windows.Forms.Label lblLift;
        private System.Windows.Forms.NumericUpDown numLift;
        private System.Windows.Forms.Button btnKhaiPha;
        private System.Windows.Forms.Label lblTongDon;
        private System.Windows.Forms.DataGridView dgvCombo;
        private System.Windows.Forms.Label lblGoiY;
        private System.Windows.Forms.Button btnThoat;
    }
}
