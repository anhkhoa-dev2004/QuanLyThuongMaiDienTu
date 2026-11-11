namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_ChamDiemSanPham
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
            this.lblKpi1 = new System.Windows.Forms.Label();
            this.lblKpi2 = new System.Windows.Forms.Label();
            this.lblKpi3 = new System.Windows.Forms.Label();
            this.lblKpi4 = new System.Windows.Forms.Label();
            this.lblKpi5 = new System.Windows.Forms.Label();
            this.lblKpi6 = new System.Windows.Forms.Label();
            this.btnTinh = new System.Windows.Forms.Button();
            this.pnlBieuDo = new System.Windows.Forms.Panel();
            this.dgvDiem = new System.Windows.Forms.DataGridView();
            this.lblPhuongSai = new System.Windows.Forms.Label();
            this.dgvTai = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTai)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CHẤM ĐIỂM TỔNG HỢP SẢN PHẨM";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1100, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Gộp 6 con số của mỗi sản phẩm (giá bán, số bán, doanh thu, tồn kho, số nhập, số trả) thành 2 trục để xem sản phẩm nào giống nhau, sản phẩm nào khác biệt.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblKpi1
            // 
            this.lblKpi1.AutoSize = false;
            this.lblKpi1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpi1.Location = new System.Drawing.Point(22, 88);
            this.lblKpi1.Size = new System.Drawing.Size(176, 58);
            this.lblKpi1.Name = "lblKpi1";
            this.lblKpi1.TabIndex = 2;
            this.lblKpi1.Text = "Tổng doanh thu";
            this.lblKpi1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpi1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // lblKpi2
            // 
            this.lblKpi2.AutoSize = false;
            this.lblKpi2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpi2.Location = new System.Drawing.Point(205, 88);
            this.lblKpi2.Size = new System.Drawing.Size(176, 58);
            this.lblKpi2.Name = "lblKpi2";
            this.lblKpi2.TabIndex = 3;
            this.lblKpi2.Text = "Số đơn hàng";
            this.lblKpi2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpi2.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // lblKpi3
            // 
            this.lblKpi3.AutoSize = false;
            this.lblKpi3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpi3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpi3.Location = new System.Drawing.Point(388, 88);
            this.lblKpi3.Size = new System.Drawing.Size(176, 58);
            this.lblKpi3.Name = "lblKpi3";
            this.lblKpi3.TabIndex = 4;
            this.lblKpi3.Text = "Số khách hàng";
            this.lblKpi3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpi3.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // lblKpi4
            // 
            this.lblKpi4.AutoSize = false;
            this.lblKpi4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpi4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpi4.Location = new System.Drawing.Point(571, 88);
            this.lblKpi4.Size = new System.Drawing.Size(176, 58);
            this.lblKpi4.Name = "lblKpi4";
            this.lblKpi4.TabIndex = 5;
            this.lblKpi4.Text = "Tổng tồn kho";
            this.lblKpi4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpi4.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // lblKpi5
            // 
            this.lblKpi5.AutoSize = false;
            this.lblKpi5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpi5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpi5.Location = new System.Drawing.Point(754, 88);
            this.lblKpi5.Size = new System.Drawing.Size(176, 58);
            this.lblKpi5.Name = "lblKpi5";
            this.lblKpi5.TabIndex = 6;
            this.lblKpi5.Text = "SP dưới ngưỡng";
            this.lblKpi5.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpi5.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // lblKpi6
            // 
            this.lblKpi6.AutoSize = false;
            this.lblKpi6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpi6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpi6.Location = new System.Drawing.Point(937, 88);
            this.lblKpi6.Size = new System.Drawing.Size(176, 58);
            this.lblKpi6.Name = "lblKpi6";
            this.lblKpi6.TabIndex = 7;
            this.lblKpi6.Text = "Tỷ lệ trả hàng";
            this.lblKpi6.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpi6.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // btnTinh
            // 
            this.btnTinh.Location = new System.Drawing.Point(22, 156);
            this.btnTinh.Size = new System.Drawing.Size(170, 32);
            this.btnTinh.Name = "btnTinh";
            this.btnTinh.TabIndex = 8;
            this.btnTinh.Text = "Chấm điểm";
            this.btnTinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinh.UseVisualStyleBackColor = true;
            this.btnTinh.Click += new System.EventHandler(this.btnTinh_Click);
            // 
            // pnlBieuDo
            // 
            this.pnlBieuDo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBieuDo.BackColor = System.Drawing.Color.White;
            this.pnlBieuDo.Location = new System.Drawing.Point(22, 196);
            this.pnlBieuDo.Size = new System.Drawing.Size(560, 300);
            this.pnlBieuDo.Name = "pnlBieuDo";
            this.pnlBieuDo.TabIndex = 9;
            this.pnlBieuDo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.pnlBieuDo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBieuDo_Paint);
            // 
            // dgvDiem
            // 
            this.dgvDiem.AllowUserToAddRows = false;
            this.dgvDiem.AllowUserToDeleteRows = false;
            this.dgvDiem.ReadOnly = true;
            this.dgvDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiem.Location = new System.Drawing.Point(600, 196);
            this.dgvDiem.Size = new System.Drawing.Size(525, 300);
            this.dgvDiem.Name = "dgvDiem";
            this.dgvDiem.TabIndex = 10;
            this.dgvDiem.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lblPhuongSai
            // 
            this.lblPhuongSai.AutoSize = false;
            this.lblPhuongSai.Location = new System.Drawing.Point(22, 506);
            this.lblPhuongSai.Size = new System.Drawing.Size(1103, 26);
            this.lblPhuongSai.Name = "lblPhuongSai";
            this.lblPhuongSai.TabIndex = 11;
            this.lblPhuongSai.Text = "";
            this.lblPhuongSai.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhuongSai.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblPhuongSai.ForeColor = System.Drawing.Color.DarkBlue;
            // 
            // dgvTai
            // 
            this.dgvTai.AllowUserToAddRows = false;
            this.dgvTai.AllowUserToDeleteRows = false;
            this.dgvTai.ReadOnly = true;
            this.dgvTai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTai.Location = new System.Drawing.Point(22, 538);
            this.dgvTai.Size = new System.Drawing.Size(1103, 130);
            this.dgvTai.Name = "dgvTai";
            this.dgvTai.TabIndex = 12;
            this.dgvTai.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1025, 688);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 13;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_ChamDiemSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 730);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblKpi1);
            this.Controls.Add(this.lblKpi2);
            this.Controls.Add(this.lblKpi3);
            this.Controls.Add(this.lblKpi4);
            this.Controls.Add(this.lblKpi5);
            this.Controls.Add(this.lblKpi6);
            this.Controls.Add(this.btnTinh);
            this.Controls.Add(this.pnlBieuDo);
            this.Controls.Add(this.dgvDiem);
            this.Controls.Add(this.lblPhuongSai);
            this.Controls.Add(this.dgvTai);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_ChamDiemSanPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chấm điểm tổng hợp sản phẩm";
            this.Load += new System.EventHandler(this.Form_ChamDiemSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblKpi1;
        private System.Windows.Forms.Label lblKpi2;
        private System.Windows.Forms.Label lblKpi3;
        private System.Windows.Forms.Label lblKpi4;
        private System.Windows.Forms.Label lblKpi5;
        private System.Windows.Forms.Label lblKpi6;
        private System.Windows.Forms.Button btnTinh;
        private System.Windows.Forms.Panel pnlBieuDo;
        private System.Windows.Forms.DataGridView dgvDiem;
        private System.Windows.Forms.Label lblPhuongSai;
        private System.Windows.Forms.DataGridView dgvTai;
        private System.Windows.Forms.Button btnThoat;
    }
}
