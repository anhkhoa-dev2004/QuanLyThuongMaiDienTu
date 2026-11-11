namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_DocCamXucDanhGia
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
            this.btnChamLaiTuDong = new System.Windows.Forms.Button();
            this.btnBoGanTay = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.dgvTraHang = new System.Windows.Forms.DataGridView();
            this.lblNhan = new System.Windows.Forms.Label();
            this.cboCamXuc = new System.Windows.Forms.ComboBox();
            this.btnGanNhan = new System.Windows.Forms.Button();
            this.btnLamSach = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblCSAT = new System.Windows.Forms.Label();
            this.btnCapNhatCSAT = new System.Windows.Forms.Button();
            this.dgvCSAT = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCSAT)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐỌC CẢM XÚC TRONG ĐÁNH GIÁ";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Máy đọc lời khách và đếm từ khen, từ chê theo từ điển. Dòng nào đọc sai thì chọn dòng đó và sửa tay.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Location = new System.Drawing.Point(22, 88);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.TabIndex = 2;
            this.lblDanhSach.Text = "Phản hồi lấy từ phiếu trả hàng:";
            this.lblDanhSach.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvTraHang
            // 
            this.dgvTraHang.AllowUserToAddRows = false;
            this.dgvTraHang.AllowUserToDeleteRows = false;
            this.dgvTraHang.ReadOnly = true;
            this.dgvTraHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraHang.Location = new System.Drawing.Point(22, 112);
            this.dgvTraHang.Size = new System.Drawing.Size(1053, 210);
            this.dgvTraHang.Name = "dgvTraHang";
            this.dgvTraHang.TabIndex = 3;
            this.dgvTraHang.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvTraHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTraHang_CellClick);
            // 
            // lblNhan
            // 
            this.lblNhan.AutoSize = true;
            this.lblNhan.Location = new System.Drawing.Point(22, 336);
            this.lblNhan.Name = "lblNhan";
            this.lblNhan.TabIndex = 4;
            this.lblNhan.Text = "Nhãn cảm xúc:";
            this.lblNhan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboCamXuc
            // 
            this.cboCamXuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCamXuc.FormattingEnabled = true;
            this.cboCamXuc.Items.AddRange(new object[] {
            "Khen",
            "Trung tính",
            "Chê"});
            this.cboCamXuc.Location = new System.Drawing.Point(150, 332);
            this.cboCamXuc.Size = new System.Drawing.Size(150, 26);
            this.cboCamXuc.Name = "cboCamXuc";
            this.cboCamXuc.TabIndex = 5;
            this.cboCamXuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnGanNhan
            // 
            this.btnGanNhan.Location = new System.Drawing.Point(315, 328);
            this.btnGanNhan.Size = new System.Drawing.Size(250, 32);
            this.btnGanNhan.Name = "btnGanNhan";
            this.btnGanNhan.TabIndex = 6;
            this.btnGanNhan.Text = "Gán nhãn cho dòng đang chọn";
            this.btnGanNhan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGanNhan.UseVisualStyleBackColor = true;
            this.btnGanNhan.Click += new System.EventHandler(this.btnGanNhan_Click);
            // 
            // btnLamSach
            // 
            this.btnLamSach.Location = new System.Drawing.Point(575, 328);
            this.btnLamSach.Size = new System.Drawing.Size(260, 32);
            this.btnLamSach.Name = "btnLamSach";
            this.btnLamSach.TabIndex = 7;
            this.btnLamSach.Text = "Làm sạch văn bản (chạy thủ tục)";
            this.btnLamSach.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamSach.UseVisualStyleBackColor = true;
            this.btnLamSach.Click += new System.EventHandler(this.btnLamSach_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(845, 328);
            this.btnLamMoi.Size = new System.Drawing.Size(110, 32);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 8;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblCSAT
            // 
            this.lblCSAT.AutoSize = true;
            this.lblCSAT.Location = new System.Drawing.Point(22, 376);
            this.lblCSAT.Name = "lblCSAT";
            this.lblCSAT.TabIndex = 9;
            this.lblCSAT.Text = "Điểm hài lòng theo sản phẩm (trung bình các nhãn, thang -1 đến +1):";
            this.lblCSAT.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCapNhatCSAT
            // 
            this.btnCapNhatCSAT.Location = new System.Drawing.Point(22, 402);
            this.btnCapNhatCSAT.Size = new System.Drawing.Size(360, 32);
            this.btnCapNhatCSAT.Name = "btnCapNhatCSAT";
            this.btnCapNhatCSAT.TabIndex = 10;
            this.btnCapNhatCSAT.Text = "Lưu % khách hài lòng vào sản phẩm";
            this.btnCapNhatCSAT.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatCSAT.UseVisualStyleBackColor = true;
            this.btnCapNhatCSAT.Click += new System.EventHandler(this.btnCapNhatCSAT_Click);
            // 
            // dgvCSAT
            // 
            this.dgvCSAT.AllowUserToAddRows = false;
            this.dgvCSAT.AllowUserToDeleteRows = false;
            this.dgvCSAT.ReadOnly = true;
            this.dgvCSAT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCSAT.Location = new System.Drawing.Point(22, 442);
            this.dgvCSAT.Size = new System.Drawing.Size(1053, 196);
            this.dgvCSAT.Name = "dgvCSAT";
            this.dgvCSAT.TabIndex = 11;
            this.dgvCSAT.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 648);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 12;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnChamLaiTuDong
            // 
            this.btnChamLaiTuDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamLaiTuDong.Location = new System.Drawing.Point(22, 648);
            this.btnChamLaiTuDong.Name = "btnChamLaiTuDong";
            this.btnChamLaiTuDong.Size = new System.Drawing.Size(340, 30);
            this.btnChamLaiTuDong.TabIndex = 200;
            this.btnChamLaiTuDong.Text = "Chấm tự động lại tất cả (giữ dòng gán tay)";
            this.btnChamLaiTuDong.UseVisualStyleBackColor = true;
            this.btnChamLaiTuDong.Click += new System.EventHandler(this.btnChamLaiTuDong_Click);
            // 
            // btnBoGanTay
            // 
            this.btnBoGanTay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoGanTay.Location = new System.Drawing.Point(372, 648);
            this.btnBoGanTay.Name = "btnBoGanTay";
            this.btnBoGanTay.Size = new System.Drawing.Size(240, 30);
            this.btnBoGanTay.TabIndex = 201;
            this.btnBoGanTay.Text = "Bỏ gán tay dòng đang chọn";
            this.btnBoGanTay.UseVisualStyleBackColor = true;
            this.btnBoGanTay.Click += new System.EventHandler(this.btnBoGanTay_Click);
            // 
            // Form_DocCamXucDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 690);
            this.Controls.Add(this.btnChamLaiTuDong);
            this.Controls.Add(this.btnBoGanTay);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblDanhSach);
            this.Controls.Add(this.dgvTraHang);
            this.Controls.Add(this.lblNhan);
            this.Controls.Add(this.cboCamXuc);
            this.Controls.Add(this.btnGanNhan);
            this.Controls.Add(this.btnLamSach);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.lblCSAT);
            this.Controls.Add(this.btnCapNhatCSAT);
            this.Controls.Add(this.dgvCSAT);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_DocCamXucDanhGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đọc cảm xúc trong đánh giá";
            this.Load += new System.EventHandler(this.Form_DocCamXucDanhGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCSAT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.DataGridView dgvTraHang;
        private System.Windows.Forms.Label lblNhan;
        private System.Windows.Forms.ComboBox cboCamXuc;
        private System.Windows.Forms.Button btnGanNhan;
        private System.Windows.Forms.Button btnLamSach;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblCSAT;
        private System.Windows.Forms.Button btnCapNhatCSAT;
        private System.Windows.Forms.DataGridView dgvCSAT;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnChamLaiTuDong;
        private System.Windows.Forms.Button btnBoGanTay;
    }
}
