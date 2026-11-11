namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_DonDangNgo
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
            this.btnChayDiem = new System.Windows.Forms.Button();
            this.lblLoc = new System.Windows.Forms.Label();
            this.cboLoc = new System.Windows.Forms.ComboBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgvDonHang = new System.Windows.Forms.DataGridView();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.btnGanCo = new System.Windows.Forms.Button();
            this.btnBoCo = new System.Windows.Forms.Button();
            this.lblXuLy = new System.Windows.Forms.Label();
            this.dgvXuLy = new System.Windows.Forms.DataGridView();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThaiXuLy = new System.Windows.Forms.ComboBox();
            this.btnCapNhatXuLy = new System.Windows.Forms.Button();
            this.lblTraHang = new System.Windows.Forms.Label();
            this.dgvTraHang = new System.Windows.Forms.DataGridView();
            this.btnCoTraHang = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuLy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraHang)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐƠN ĐÁNG NGỜ, NGHI GIAN LẬN";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1100, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Điểm rủi ro hủy đơn do hệ thống chấm theo quy tắc có sẵn. Gắn cờ gian lận là quyết định của nhân viên và được lưu lại để xử lý.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // btnChayDiem
            // 
            this.btnChayDiem.Location = new System.Drawing.Point(22, 88);
            this.btnChayDiem.Size = new System.Drawing.Size(250, 30);
            this.btnChayDiem.Name = "btnChayDiem";
            this.btnChayDiem.TabIndex = 2;
            this.btnChayDiem.Text = "Chấm điểm rủi ro mọi đơn";
            this.btnChayDiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChayDiem.UseVisualStyleBackColor = true;
            this.btnChayDiem.Click += new System.EventHandler(this.btnChayDiem_Click);
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Location = new System.Drawing.Point(295, 92);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.TabIndex = 3;
            this.lblLoc.Text = "Lọc:";
            this.lblLoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboLoc
            // 
            this.cboLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoc.FormattingEnabled = true;
            this.cboLoc.Items.AddRange(new object[] {
            "Tất cả",
            "Nguy cơ cao",
            "Cần xem",
            "Đã gắn cờ gian lận"});
            this.cboLoc.Location = new System.Drawing.Point(340, 88);
            this.cboLoc.Size = new System.Drawing.Size(230, 26);
            this.cboLoc.Name = "cboLoc";
            this.cboLoc.TabIndex = 4;
            this.cboLoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoc.SelectedIndexChanged += new System.EventHandler(this.cboLoc_SelectedIndexChanged);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(590, 86);
            this.btnLamMoi.Size = new System.Drawing.Size(110, 32);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // dgvDonHang
            // 
            this.dgvDonHang.AllowUserToAddRows = false;
            this.dgvDonHang.AllowUserToDeleteRows = false;
            this.dgvDonHang.ReadOnly = true;
            this.dgvDonHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonHang.Location = new System.Drawing.Point(22, 128);
            this.dgvDonHang.Size = new System.Drawing.Size(1103, 200);
            this.dgvDonHang.Name = "dgvDonHang";
            this.dgvDonHang.TabIndex = 6;
            this.dgvDonHang.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvDonHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDonHang_CellClick);
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(22, 342);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.TabIndex = 7;
            this.lblLyDo.Text = "Lý do nghi ngờ:";
            this.lblLyDo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(150, 338);
            this.txtLyDo.Size = new System.Drawing.Size(590, 26);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.TabIndex = 8;
            this.txtLyDo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyDo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnGanCo
            // 
            this.btnGanCo.Location = new System.Drawing.Point(755, 334);
            this.btnGanCo.Size = new System.Drawing.Size(230, 32);
            this.btnGanCo.Name = "btnGanCo";
            this.btnGanCo.TabIndex = 9;
            this.btnGanCo.Text = "Gắn cờ nghi ngờ gian lận";
            this.btnGanCo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGanCo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnGanCo.UseVisualStyleBackColor = true;
            this.btnGanCo.Click += new System.EventHandler(this.btnGanCo_Click);
            // 
            // btnBoCo
            // 
            this.btnBoCo.Location = new System.Drawing.Point(995, 334);
            this.btnBoCo.Size = new System.Drawing.Size(130, 32);
            this.btnBoCo.Name = "btnBoCo";
            this.btnBoCo.TabIndex = 10;
            this.btnBoCo.Text = "Bỏ cờ";
            this.btnBoCo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoCo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnBoCo.UseVisualStyleBackColor = true;
            this.btnBoCo.Click += new System.EventHandler(this.btnBoCo_Click);
            // 
            // lblXuLy
            // 
            this.lblXuLy.AutoSize = true;
            this.lblXuLy.Location = new System.Drawing.Point(22, 376);
            this.lblXuLy.Name = "lblXuLy";
            this.lblXuLy.TabIndex = 11;
            this.lblXuLy.Text = "Đơn đã gắn cờ (BatThuong_DonHang_GianLan):";
            this.lblXuLy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvXuLy
            // 
            this.dgvXuLy.AllowUserToAddRows = false;
            this.dgvXuLy.AllowUserToDeleteRows = false;
            this.dgvXuLy.ReadOnly = true;
            this.dgvXuLy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXuLy.Location = new System.Drawing.Point(22, 402);
            this.dgvXuLy.Size = new System.Drawing.Size(1103, 130);
            this.dgvXuLy.Name = "dgvXuLy";
            this.dgvXuLy.TabIndex = 12;
            this.dgvXuLy.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvXuLy.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvXuLy_CellClick);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(22, 546);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.TabIndex = 13;
            this.lblTrangThai.Text = "Trạng thái xử lý:";
            this.lblTrangThai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboTrangThaiXuLy
            // 
            this.cboTrangThaiXuLy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiXuLy.FormattingEnabled = true;
            this.cboTrangThaiXuLy.Items.AddRange(new object[] {
            "Chưa xử lý",
            "Đang kiểm tra",
            "Đã xác nhận gian lận",
            "Không gian lận"});
            this.cboTrangThaiXuLy.Location = new System.Drawing.Point(150, 542);
            this.cboTrangThaiXuLy.Size = new System.Drawing.Size(190, 26);
            this.cboTrangThaiXuLy.Name = "cboTrangThaiXuLy";
            this.cboTrangThaiXuLy.TabIndex = 14;
            this.cboTrangThaiXuLy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCapNhatXuLy
            // 
            this.btnCapNhatXuLy.Location = new System.Drawing.Point(355, 540);
            this.btnCapNhatXuLy.Size = new System.Drawing.Size(190, 32);
            this.btnCapNhatXuLy.Name = "btnCapNhatXuLy";
            this.btnCapNhatXuLy.TabIndex = 15;
            this.btnCapNhatXuLy.Text = "Cập nhật xử lý";
            this.btnCapNhatXuLy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatXuLy.UseVisualStyleBackColor = true;
            this.btnCapNhatXuLy.Click += new System.EventHandler(this.btnCapNhatXuLy_Click);
            // 
            // lblTraHang
            // 
            this.lblTraHang.AutoSize = true;
            this.lblTraHang.Location = new System.Drawing.Point(22, 584);
            this.lblTraHang.Name = "lblTraHang";
            this.lblTraHang.TabIndex = 16;
            this.lblTraHang.Text = "Phiếu trả hàng (cột CoGianLanDoiTra):";
            this.lblTraHang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvTraHang
            // 
            this.dgvTraHang.AllowUserToAddRows = false;
            this.dgvTraHang.AllowUserToDeleteRows = false;
            this.dgvTraHang.ReadOnly = true;
            this.dgvTraHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraHang.Location = new System.Drawing.Point(22, 610);
            this.dgvTraHang.Size = new System.Drawing.Size(850, 100);
            this.dgvTraHang.Name = "dgvTraHang";
            this.dgvTraHang.TabIndex = 17;
            this.dgvTraHang.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.dgvTraHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTraHang_CellClick);
            // 
            // btnCoTraHang
            // 
            this.btnCoTraHang.Location = new System.Drawing.Point(885, 610);
            this.btnCoTraHang.Size = new System.Drawing.Size(240, 32);
            this.btnCoTraHang.Name = "btnCoTraHang";
            this.btnCoTraHang.TabIndex = 18;
            this.btnCoTraHang.Text = "Gắn / bỏ cờ đổi trả gian lận";
            this.btnCoTraHang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoTraHang.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnCoTraHang.UseVisualStyleBackColor = true;
            this.btnCoTraHang.Click += new System.EventHandler(this.btnCoTraHang_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1025, 680);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 19;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_DonDangNgo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 730);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.btnChayDiem);
            this.Controls.Add(this.lblLoc);
            this.Controls.Add(this.cboLoc);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.dgvDonHang);
            this.Controls.Add(this.lblLyDo);
            this.Controls.Add(this.txtLyDo);
            this.Controls.Add(this.btnGanCo);
            this.Controls.Add(this.btnBoCo);
            this.Controls.Add(this.lblXuLy);
            this.Controls.Add(this.dgvXuLy);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.cboTrangThaiXuLy);
            this.Controls.Add(this.btnCapNhatXuLy);
            this.Controls.Add(this.lblTraHang);
            this.Controls.Add(this.dgvTraHang);
            this.Controls.Add(this.btnCoTraHang);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_DonDangNgo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đơn đáng ngờ, nghi gian lận";
            this.Load += new System.EventHandler(this.Form_DonDangNgo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuLy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Button btnChayDiem;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.ComboBox cboLoc;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvDonHang;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Button btnGanCo;
        private System.Windows.Forms.Button btnBoCo;
        private System.Windows.Forms.Label lblXuLy;
        private System.Windows.Forms.DataGridView dgvXuLy;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThaiXuLy;
        private System.Windows.Forms.Button btnCapNhatXuLy;
        private System.Windows.Forms.Label lblTraHang;
        private System.Windows.Forms.DataGridView dgvTraHang;
        private System.Windows.Forms.Button btnCoTraHang;
        private System.Windows.Forms.Button btnThoat;
    }
}
