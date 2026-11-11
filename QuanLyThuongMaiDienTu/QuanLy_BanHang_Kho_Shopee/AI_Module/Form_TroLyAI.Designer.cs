namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_TroLyAI
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
            this.lblCauHoi = new System.Windows.Forms.Label();
            this.txtCauHoi = new System.Windows.Forms.TextBox();
            this.btnHoiAI = new System.Windows.Forms.Button();
            this.btnXoaTrang = new System.Windows.Forms.Button();
            this.lblSQL = new System.Windows.Forms.Label();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.btnChaySQL = new System.Windows.Forms.Button();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.lblLichSu = new System.Windows.Forms.Label();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TRỢ LÝ AI HỎI DỮ LIỆU";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Gõ câu hỏi bằng tiếng Việt, AI viết câu lệnh đọc dữ liệu, bạn xem lại rồi mới bấm Chạy. Chỉ đọc được, không sửa hay xóa dữ liệu.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblCauHoi
            // 
            this.lblCauHoi.AutoSize = true;
            this.lblCauHoi.Location = new System.Drawing.Point(22, 88);
            this.lblCauHoi.Name = "lblCauHoi";
            this.lblCauHoi.TabIndex = 2;
            this.lblCauHoi.Text = "Câu hỏi:";
            this.lblCauHoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtCauHoi
            // 
            this.txtCauHoi.Multiline = true;
            this.txtCauHoi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCauHoi.Location = new System.Drawing.Point(22, 112);
            this.txtCauHoi.Size = new System.Drawing.Size(820, 56);
            this.txtCauHoi.Name = "txtCauHoi";
            this.txtCauHoi.TabIndex = 3;
            this.txtCauHoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCauHoi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnHoiAI
            // 
            this.btnHoiAI.Location = new System.Drawing.Point(855, 112);
            this.btnHoiAI.Size = new System.Drawing.Size(120, 56);
            this.btnHoiAI.Name = "btnHoiAI";
            this.btnHoiAI.TabIndex = 4;
            this.btnHoiAI.Text = "Hỏi AI";
            this.btnHoiAI.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoiAI.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnHoiAI.UseVisualStyleBackColor = true;
            this.btnHoiAI.Click += new System.EventHandler(this.btnHoiAI_Click);
            // 
            // btnXoaTrang
            // 
            this.btnXoaTrang.Location = new System.Drawing.Point(985, 112);
            this.btnXoaTrang.Size = new System.Drawing.Size(90, 56);
            this.btnXoaTrang.Name = "btnXoaTrang";
            this.btnXoaTrang.TabIndex = 5;
            this.btnXoaTrang.Text = "Xóa";
            this.btnXoaTrang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaTrang.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnXoaTrang.UseVisualStyleBackColor = true;
            this.btnXoaTrang.Click += new System.EventHandler(this.btnXoaTrang_Click);
            // 
            // lblSQL
            // 
            this.lblSQL.AutoSize = true;
            this.lblSQL.Location = new System.Drawing.Point(22, 178);
            this.lblSQL.Name = "lblSQL";
            this.lblSQL.TabIndex = 6;
            this.lblSQL.Text = "Câu lệnh đọc dữ liệu (xem lại, sửa được):";
            this.lblSQL.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtSQL
            // 
            this.txtSQL.Multiline = true;
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSQL.Location = new System.Drawing.Point(22, 202);
            this.txtSQL.Size = new System.Drawing.Size(820, 86);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.TabIndex = 7;
            this.txtSQL.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnChaySQL
            // 
            this.btnChaySQL.Location = new System.Drawing.Point(855, 202);
            this.btnChaySQL.Size = new System.Drawing.Size(220, 86);
            this.btnChaySQL.Name = "btnChaySQL";
            this.btnChaySQL.TabIndex = 8;
            this.btnChaySQL.Text = "Chạy câu lệnh";
            this.btnChaySQL.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChaySQL.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnChaySQL.UseVisualStyleBackColor = true;
            this.btnChaySQL.Click += new System.EventHandler(this.btnChaySQL_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = false;
            this.lblTrangThai.Location = new System.Drawing.Point(22, 296);
            this.lblTrangThai.Size = new System.Drawing.Size(1050, 24);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.TabIndex = 9;
            this.lblTrangThai.Text = "";
            this.lblTrangThai.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblTrangThai.ForeColor = System.Drawing.Color.DarkBlue;
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Location = new System.Drawing.Point(22, 322);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.TabIndex = 10;
            this.lblKetQua.Text = "Kết quả:";
            this.lblKetQua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(22, 346);
            this.dgvKetQua.Size = new System.Drawing.Size(1053, 160);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.TabIndex = 11;
            this.dgvKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lblLichSu
            // 
            this.lblLichSu.AutoSize = true;
            this.lblLichSu.Location = new System.Drawing.Point(22, 514);
            this.lblLichSu.Name = "lblLichSu";
            this.lblLichSu.TabIndex = 12;
            this.lblLichSu.Text = "Lịch sử câu hỏi gần đây (bấm vào một dòng để nạp lại):";
            this.lblLichSu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Location = new System.Drawing.Point(22, 538);
            this.dgvLichSu.Size = new System.Drawing.Size(1053, 112);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.TabIndex = 13;
            this.dgvLichSu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvLichSu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSu_CellClick);
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
            // Form_TroLyAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblCauHoi);
            this.Controls.Add(this.txtCauHoi);
            this.Controls.Add(this.btnHoiAI);
            this.Controls.Add(this.btnXoaTrang);
            this.Controls.Add(this.lblSQL);
            this.Controls.Add(this.txtSQL);
            this.Controls.Add(this.btnChaySQL);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.lblLichSu);
            this.Controls.Add(this.dgvLichSu);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_TroLyAI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trợ lý AI hỏi dữ liệu";
            this.Load += new System.EventHandler(this.Form_TroLyAI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblCauHoi;
        private System.Windows.Forms.TextBox txtCauHoi;
        private System.Windows.Forms.Button btnHoiAI;
        private System.Windows.Forms.Button btnXoaTrang;
        private System.Windows.Forms.Label lblSQL;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.Button btnChaySQL;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Label lblLichSu;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.Button btnThoat;
    }
}
