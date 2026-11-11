namespace QuanLy_BanHang_Kho_Shopee.AI_Module
{
    partial class Form_DonDuLieuTuDong
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
            this.pnlBoSung = new System.Windows.Forms.Panel();
            this.btnDienGiaKnn = new System.Windows.Forms.Button();
            this.btnTimNgoaiLe = new System.Windows.Forms.Button();
            this.lblPhu = new System.Windows.Forms.Label();
            this.dgvNgoaiLe = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNgoaiLe)).BeginInit();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.btnLamSach = new System.Windows.Forms.Button();
            this.btnDienThieu = new System.Windows.Forms.Button();
            this.btnChayCaHai = new System.Windows.Forms.Button();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.lblThieu = new System.Windows.Forms.Label();
            this.dgvThieu = new System.Windows.Forms.DataGridView();
            this.lblLog = new System.Windows.Forms.Label();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "DỌN DỮ LIỆU TỰ ĐỘNG";
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(22, 50);
            this.lblGhiChu.Size = new System.Drawing.Size(1050, 32);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Dọn dữ liệu 2 việc: sửa số điện thoại, địa chỉ, chữ viết tắt trong lý do trả hàng; và điền chỗ còn trống bằng mức trung bình của danh mục.";
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            // 
            // btnLamSach
            // 
            this.btnLamSach.Location = new System.Drawing.Point(22, 88);
            this.btnLamSach.Size = new System.Drawing.Size(330, 34);
            this.btnLamSach.Name = "btnLamSach";
            this.btnLamSach.TabIndex = 2;
            this.btnLamSach.Text = "Sửa chữ viết tắt, số điện thoại, địa chỉ";
            this.btnLamSach.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamSach.UseVisualStyleBackColor = true;
            this.btnLamSach.Click += new System.EventHandler(this.btnLamSach_Click);
            // 
            // btnDienThieu
            // 
            this.btnDienThieu.Location = new System.Drawing.Point(362, 88);
            this.btnDienThieu.Size = new System.Drawing.Size(240, 34);
            this.btnDienThieu.Name = "btnDienThieu";
            this.btnDienThieu.TabIndex = 3;
            this.btnDienThieu.Text = "Điền dữ liệu thiếu";
            this.btnDienThieu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDienThieu.UseVisualStyleBackColor = true;
            this.btnDienThieu.Click += new System.EventHandler(this.btnDienThieu_Click);
            // 
            // btnChayCaHai
            // 
            this.btnChayCaHai.Location = new System.Drawing.Point(612, 88);
            this.btnChayCaHai.Size = new System.Drawing.Size(170, 34);
            this.btnChayCaHai.Name = "btnChayCaHai";
            this.btnChayCaHai.TabIndex = 4;
            this.btnChayCaHai.Text = "Chạy cả hai";
            this.btnChayCaHai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChayCaHai.UseVisualStyleBackColor = true;
            this.btnChayCaHai.Click += new System.EventHandler(this.btnChayCaHai_Click);
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = false;
            this.lblKetQua.Location = new System.Drawing.Point(22, 130);
            this.lblKetQua.Size = new System.Drawing.Size(1053, 26);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.TabIndex = 5;
            this.lblKetQua.Text = "";
            this.lblKetQua.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblKetQua.ForeColor = System.Drawing.Color.DarkGreen;
            // 
            // lblThieu
            // 
            this.lblThieu.AutoSize = true;
            this.lblThieu.Location = new System.Drawing.Point(22, 164);
            this.lblThieu.Name = "lblThieu";
            this.lblThieu.TabIndex = 6;
            this.lblThieu.Text = "Dữ liệu đang thiếu (đếm trực tiếp):";
            this.lblThieu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvThieu
            // 
            this.dgvThieu.AllowUserToAddRows = false;
            this.dgvThieu.AllowUserToDeleteRows = false;
            this.dgvThieu.ReadOnly = true;
            this.dgvThieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThieu.Location = new System.Drawing.Point(22, 190);
            this.dgvThieu.Size = new System.Drawing.Size(500, 230);
            this.dgvThieu.Name = "dgvThieu";
            this.dgvThieu.TabIndex = 7;
            this.dgvThieu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(540, 164);
            this.lblLog.Name = "lblLog";
            this.lblLog.TabIndex = 8;
            this.lblLog.Text = "Nhật ký xử lý (Log_TienXuLyDuLieu):";
            this.lblLog.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.ReadOnly = true;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Location = new System.Drawing.Point(540, 190);
            this.dgvLog.Size = new System.Drawing.Size(535, 230);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.TabIndex = 9;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(975, 438);
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.btnDienGiaKnn);
            this.pnlBoSung.Controls.Add(this.btnTimNgoaiLe);
            this.pnlBoSung.Controls.Add(this.lblPhu);
            this.pnlBoSung.Controls.Add(this.dgvNgoaiLe);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 480);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(1100, 230);
            this.pnlBoSung.TabIndex = 200;
            // 
            // btnDienGiaKnn
            // 
            this.btnDienGiaKnn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDienGiaKnn.Location = new System.Drawing.Point(22, 8);
            this.btnDienGiaKnn.Name = "btnDienGiaKnn";
            this.btnDienGiaKnn.Size = new System.Drawing.Size(270, 30);
            this.btnDienGiaKnn.TabIndex = 201;
            this.btnDienGiaKnn.Text = "Điền giá bán còn trống (theo 3 món giống nhất)";
            this.btnDienGiaKnn.UseVisualStyleBackColor = true;
            this.btnDienGiaKnn.Click += new System.EventHandler(this.btnDienGiaKnn_Click);
            // 
            // btnTimNgoaiLe
            // 
            this.btnTimNgoaiLe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimNgoaiLe.Location = new System.Drawing.Point(302, 8);
            this.btnTimNgoaiLe.Name = "btnTimNgoaiLe";
            this.btnTimNgoaiLe.Size = new System.Drawing.Size(230, 30);
            this.btnTimNgoaiLe.TabIndex = 202;
            this.btnTimNgoaiLe.Text = "Tìm giá / tiền bất thường";
            this.btnTimNgoaiLe.UseVisualStyleBackColor = true;
            this.btnTimNgoaiLe.Click += new System.EventHandler(this.btnTimNgoaiLe_Click);
            // 
            // lblPhu
            // 
            this.lblPhu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhu.Location = new System.Drawing.Point(22, 44);
            this.lblPhu.Name = "lblPhu";
            this.lblPhu.Size = new System.Drawing.Size(1053, 56);
            this.lblPhu.TabIndex = 203;
            this.lblPhu.Text = "Điền giá còn trống: với mỗi sản phẩm chưa có giá, tìm 3 sản phẩm giống nhất (cùng danh mục nếu đủ, so theo ngưỡng an toàn, tổng tồn, tổng đã bán) rồi lấy trung bình giá của 3 sản phẩm đó. Tìm giá / tiền bất thường: xếp các giá trị từ nhỏ tới lớn, lấy khoảng giữa của phần đông, giá trị nào quá xa khoảng đó thì liệt kê để kiểm tra. Không xóa gì, báo cáo vẫn tính các dòng này.";
            // 
            // dgvNgoaiLe
            // 
            this.dgvNgoaiLe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNgoaiLe.Location = new System.Drawing.Point(22, 104);
            this.dgvNgoaiLe.Name = "dgvNgoaiLe";
            this.dgvNgoaiLe.Size = new System.Drawing.Size(1053, 120);
            this.dgvNgoaiLe.TabIndex = 204;
            // 
            // Form_DonDuLieuTuDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 710);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.btnLamSach);
            this.Controls.Add(this.btnDienThieu);
            this.Controls.Add(this.btnChayCaHai);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.lblThieu);
            this.Controls.Add(this.dgvThieu);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.btnThoat);
            this.Name = "Form_DonDuLieuTuDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dọn dữ liệu tự động";
            this.Load += new System.EventHandler(this.Form_DonDuLieuTuDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNgoaiLe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Button btnLamSach;
        private System.Windows.Forms.Button btnDienThieu;
        private System.Windows.Forms.Button btnChayCaHai;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Label lblThieu;
        private System.Windows.Forms.DataGridView dgvThieu;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Button btnDienGiaKnn;
        private System.Windows.Forms.Button btnTimNgoaiLe;
        private System.Windows.Forms.Label lblPhu;
        private System.Windows.Forms.DataGridView dgvNgoaiLe;
    }
}
