namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_KhachHayPhanNan
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
            this.btnPhanTich = new System.Windows.Forms.Button();
            this.dgvChuDe = new System.Windows.Forms.DataGridView();
            this.pnlBieuDo = new System.Windows.Forms.Panel();
            this.lblChiTiet = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuDe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "KHÁCH HAY PHÀN NÀN CHUYỆN GÌ";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Gom chủ đề bằng TỪ KHÓA trong lý do trả hàng. Bấm một chủ đề để xem các phiếu trả hàng thuộc chủ đề đó.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // btnPhanTich
            // 
            this.btnPhanTich.Location = new System.Drawing.Point(22, 88);
            this.btnPhanTich.Size = new System.Drawing.Size(220, 32);
            this.btnPhanTich.Name = "btnPhanTich";
            this.btnPhanTich.TabIndex = 2;
            this.btnPhanTich.Text = "Phân tích chủ đề";
            this.btnPhanTich.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanTich.UseVisualStyleBackColor = true;
            this.btnPhanTich.Click += new System.EventHandler(this.btnPhanTich_Click);
            // 
            // dgvChuDe
            // 
            this.dgvChuDe.AllowUserToAddRows = false;
            this.dgvChuDe.AllowUserToDeleteRows = false;
            this.dgvChuDe.ReadOnly = true;
            this.dgvChuDe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChuDe.Location = new System.Drawing.Point(22, 132);
            this.dgvChuDe.Size = new System.Drawing.Size(520, 210);
            this.dgvChuDe.Name = "dgvChuDe";
            this.dgvChuDe.TabIndex = 3;
            this.dgvChuDe.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvChuDe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChuDe_CellClick);
            // 
            // pnlBieuDo
            // 
            this.pnlBieuDo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBieuDo.BackColor = System.Drawing.Color.White;
            this.pnlBieuDo.Location = new System.Drawing.Point(560, 132);
            this.pnlBieuDo.Size = new System.Drawing.Size(515, 210);
            this.pnlBieuDo.Name = "pnlBieuDo";
            this.pnlBieuDo.TabIndex = 4;
            this.pnlBieuDo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlBieuDo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBieuDo_Paint);
            // 
            // lblChiTiet
            // 
            this.lblChiTiet.AutoSize = true;
            this.lblChiTiet.Location = new System.Drawing.Point(22, 354);
            this.lblChiTiet.Name = "lblChiTiet";
            this.lblChiTiet.TabIndex = 5;
            this.lblChiTiet.Text = "Phiếu trả hàng thuộc chủ đề đang chọn:";
            this.lblChiTiet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(22, 378);
            this.dgvChiTiet.Size = new System.Drawing.Size(1053, 250);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.TabIndex = 6;
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 638);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_KhachHayPhanNan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.btnPhanTich);
            this.Controls.Add(this.dgvChuDe);
            this.Controls.Add(this.pnlBieuDo);
            this.Controls.Add(this.lblChiTiet);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_KhachHayPhanNan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khách hay phàn nàn chuyện gì";
            this.Load += new System.EventHandler(this.Form_KhachHayPhanNan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuDe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Button btnPhanTich;
        private System.Windows.Forms.DataGridView dgvChuDe;
        private System.Windows.Forms.Panel pnlBieuDo;
        private System.Windows.Forms.Label lblChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Button btnThoat;
    }
}
