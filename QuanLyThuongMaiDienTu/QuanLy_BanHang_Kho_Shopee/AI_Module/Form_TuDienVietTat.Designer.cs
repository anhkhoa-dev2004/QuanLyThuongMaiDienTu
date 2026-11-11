namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_TuDienVietTat
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
            this.dgvTeencode = new System.Windows.Forms.DataGridView();
            this.lblTuGoc = new System.Windows.Forms.Label();
            this.txtTuGoc = new System.Windows.Forms.TextBox();
            this.lblTuChuan = new System.Windows.Forms.Label();
            this.txtTuChuan = new System.Windows.Forms.TextBox();
            this.lblLoaiTu = new System.Windows.Forms.Label();
            this.cboLoaiTu = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblThu = new System.Windows.Forms.Label();
            this.txtThu = new System.Windows.Forms.TextBox();
            this.btnThu = new System.Windows.Forms.Button();
            this.lblKQ = new System.Windows.Forms.Label();
            this.txtKetQuaThu = new System.Windows.Forms.TextBox();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeencode)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TỪ ĐIỂN CHỮ VIẾT TẮT";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Từ điển chữ viết tắt của khách (ko, dc, sp...). Từ điển này dùng chung khi sửa chữ, đọc lời khen/chê và xếp nhóm lý do mua.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // dgvTeencode
            // 
            this.dgvTeencode.AllowUserToAddRows = false;
            this.dgvTeencode.AllowUserToDeleteRows = false;
            this.dgvTeencode.ReadOnly = true;
            this.dgvTeencode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTeencode.Location = new System.Drawing.Point(22, 90);
            this.dgvTeencode.Size = new System.Drawing.Size(560, 500);
            this.dgvTeencode.Name = "dgvTeencode";
            this.dgvTeencode.TabIndex = 2;
            this.dgvTeencode.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom));
            this.dgvTeencode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTeencode_CellClick);
            // 
            // lblTuGoc
            // 
            this.lblTuGoc.AutoSize = true;
            this.lblTuGoc.Location = new System.Drawing.Point(610, 92);
            this.lblTuGoc.Name = "lblTuGoc";
            this.lblTuGoc.TabIndex = 3;
            this.lblTuGoc.Text = "Từ gốc:";
            this.lblTuGoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtTuGoc
            // 
            this.txtTuGoc.Location = new System.Drawing.Point(610, 118);
            this.txtTuGoc.Size = new System.Drawing.Size(200, 26);
            this.txtTuGoc.Name = "txtTuGoc";
            this.txtTuGoc.TabIndex = 4;
            this.txtTuGoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblTuChuan
            // 
            this.lblTuChuan.AutoSize = true;
            this.lblTuChuan.Location = new System.Drawing.Point(830, 92);
            this.lblTuChuan.Name = "lblTuChuan";
            this.lblTuChuan.TabIndex = 5;
            this.lblTuChuan.Text = "Viết đúng là:";
            this.lblTuChuan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtTuChuan
            // 
            this.txtTuChuan.Location = new System.Drawing.Point(830, 118);
            this.txtTuChuan.Size = new System.Drawing.Size(245, 26);
            this.txtTuChuan.Name = "txtTuChuan";
            this.txtTuChuan.TabIndex = 6;
            this.txtTuChuan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblLoaiTu
            // 
            this.lblLoaiTu.AutoSize = true;
            this.lblLoaiTu.Location = new System.Drawing.Point(610, 154);
            this.lblLoaiTu.Name = "lblLoaiTu";
            this.lblLoaiTu.TabIndex = 7;
            this.lblLoaiTu.Text = "Loại từ:";
            this.lblLoaiTu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboLoaiTu
            // 
            this.cboLoaiTu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTu.FormattingEnabled = true;
            this.cboLoaiTu.Items.AddRange(new object[] {
            "Teencode",
            "Viết tắt"});
            this.cboLoaiTu.Location = new System.Drawing.Point(610, 180);
            this.cboLoaiTu.Size = new System.Drawing.Size(200, 26);
            this.cboLoaiTu.Name = "cboLoaiTu";
            this.cboLoaiTu.TabIndex = 8;
            this.cboLoaiTu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(610, 222);
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 9;
            this.btnThem.Text = "Thêm";
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(720, 222);
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 10;
            this.btnSua.Text = "Sửa";
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(830, 222);
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 11;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(940, 222);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 12;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblThu
            // 
            this.lblThu.AutoSize = true;
            this.lblThu.Location = new System.Drawing.Point(610, 280);
            this.lblThu.Name = "lblThu";
            this.lblThu.TabIndex = 13;
            this.lblThu.Text = "Thử làm sạch một câu:";
            this.lblThu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtThu
            // 
            this.txtThu.Multiline = true;
            this.txtThu.Location = new System.Drawing.Point(610, 306);
            this.txtThu.Size = new System.Drawing.Size(465, 70);
            this.txtThu.Name = "txtThu";
            this.txtThu.TabIndex = 14;
            this.txtThu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnThu
            // 
            this.btnThu.Location = new System.Drawing.Point(610, 384);
            this.btnThu.Size = new System.Drawing.Size(170, 32);
            this.btnThu.Name = "btnThu";
            this.btnThu.TabIndex = 15;
            this.btnThu.Text = "Làm sạch thử";
            this.btnThu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThu.UseVisualStyleBackColor = true;
            this.btnThu.Click += new System.EventHandler(this.btnThu_Click);
            // 
            // lblKQ
            // 
            this.lblKQ.AutoSize = true;
            this.lblKQ.Location = new System.Drawing.Point(610, 426);
            this.lblKQ.Name = "lblKQ";
            this.lblKQ.TabIndex = 16;
            this.lblKQ.Text = "Kết quả:";
            this.lblKQ.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtKetQuaThu
            // 
            this.txtKetQuaThu.Multiline = true;
            this.txtKetQuaThu.ReadOnly = true;
            this.txtKetQuaThu.Location = new System.Drawing.Point(610, 452);
            this.txtKetQuaThu.Size = new System.Drawing.Size(465, 70);
            this.txtKetQuaThu.Name = "txtKetQuaThu";
            this.txtKetQuaThu.TabIndex = 17;
            this.txtKetQuaThu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 608);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 18;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_TuDienVietTat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.dgvTeencode);
            this.Controls.Add(this.lblTuGoc);
            this.Controls.Add(this.txtTuGoc);
            this.Controls.Add(this.lblTuChuan);
            this.Controls.Add(this.txtTuChuan);
            this.Controls.Add(this.lblLoaiTu);
            this.Controls.Add(this.cboLoaiTu);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.lblThu);
            this.Controls.Add(this.txtThu);
            this.Controls.Add(this.btnThu);
            this.Controls.Add(this.lblKQ);
            this.Controls.Add(this.txtKetQuaThu);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_TuDienVietTat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Từ điển chữ viết tắt";
            this.Load += new System.EventHandler(this.Form_TuDienVietTat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeencode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.DataGridView dgvTeencode;
        private System.Windows.Forms.Label lblTuGoc;
        private System.Windows.Forms.TextBox txtTuGoc;
        private System.Windows.Forms.Label lblTuChuan;
        private System.Windows.Forms.TextBox txtTuChuan;
        private System.Windows.Forms.Label lblLoaiTu;
        private System.Windows.Forms.ComboBox cboLoaiTu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblThu;
        private System.Windows.Forms.TextBox txtThu;
        private System.Windows.Forms.Button btnThu;
        private System.Windows.Forms.Label lblKQ;
        private System.Windows.Forms.TextBox txtKetQuaThu;
        private System.Windows.Forms.Button btnThoat;
    }
}
