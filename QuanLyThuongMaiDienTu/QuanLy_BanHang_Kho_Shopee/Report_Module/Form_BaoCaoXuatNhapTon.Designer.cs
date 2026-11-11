namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    partial class Form_BaoCaoXuatNhapTon
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
            this.btnDuBao = new System.Windows.Forms.Button();
            this.cboPhuongPhap = new System.Windows.Forms.ComboBox();
            this.lblDuBao = new System.Windows.Forms.Label();
            this.txtDenNgay = new System.Windows.Forms.TextBox();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.txtTuNgay = new System.Windows.Forms.TextBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dgvBaoCaoXuatNhapTon = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCaoXuatNhapTon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.Location = new System.Drawing.Point(518, 117);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(198, 20);
            this.txtDenNgay.TabIndex = 80;
            this.txtDenNgay.TextChanged += new System.EventHandler(this.txtDenNgay_TextChanged);
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenNgay.Location = new System.Drawing.Point(404, 117);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(85, 21);
            this.lblDenNgay.TabIndex = 79;
            this.lblDenNgay.Text = "Đến ngày:";
            this.lblDenNgay.Click += new System.EventHandler(this.lblDenNgay_Click);
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.Location = new System.Drawing.Point(128, 117);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(198, 20);
            this.txtTuNgay.TabIndex = 76;
            this.txtTuNgay.TextChanged += new System.EventHandler(this.txtTuNgay_TextChanged);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuNgay.Location = new System.Drawing.Point(28, 118);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(76, 21);
            this.lblTuNgay.TabIndex = 75;
            this.lblTuNgay.Text = "Từ ngày:";
            this.lblTuNgay.Click += new System.EventHandler(this.lblTuNgay_Click);
            // 
            // dgvBaoCaoXuatNhapTon
            // 
            this.dgvBaoCaoXuatNhapTon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCaoXuatNhapTon.Location = new System.Drawing.Point(23, 267);
            this.dgvBaoCaoXuatNhapTon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvBaoCaoXuatNhapTon.Name = "dgvBaoCaoXuatNhapTon";
            this.dgvBaoCaoXuatNhapTon.Size = new System.Drawing.Size(754, 162);
            this.dgvBaoCaoXuatNhapTon.TabIndex = 74;
            this.dgvBaoCaoXuatNhapTon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaoCaoXuatNhapTon_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(408, 211);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 73;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemBaoCao.Location = new System.Drawing.Point(214, 211);
            this.btnXemBaoCao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(112, 27);
            this.btnXemBaoCao.TabIndex = 72;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = true;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(187, 21);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(472, 36);
            this.lblTieuDe.TabIndex = 71;
            this.lblTieuDe.Text = "BÁO CÁO XUẤT - NHẬP - TỒN";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.btnDuBao);
            this.pnlBoSung.Controls.Add(this.cboPhuongPhap);
            this.pnlBoSung.Controls.Add(this.lblDuBao);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 130);
            this.pnlBoSung.TabIndex = 200;
            // 
            // btnDuBao
            // 
            this.btnDuBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuBao.Location = new System.Drawing.Point(20, 8);
            this.btnDuBao.Name = "btnDuBao";
            this.btnDuBao.Size = new System.Drawing.Size(340, 30);
            this.btnDuBao.TabIndex = 201;
            this.btnDuBao.Text = "Dự báo xuất 7 ngày tới cho dòng đang chọn";
            this.btnDuBao.UseVisualStyleBackColor = true;
            this.btnDuBao.Click += new System.EventHandler(this.btnDuBao_Click);
            // 
            // cboPhuongPhap
            // 
            this.cboPhuongPhap.Items.AddRange(new object[] {
            "Theo xu hướng các ngày gần đây",
            "Theo xu hướng và thứ trong tuần"});
            this.cboPhuongPhap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongPhap.FormattingEnabled = true;
            this.cboPhuongPhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhuongPhap.Location = new System.Drawing.Point(370, 10);
            this.cboPhuongPhap.Name = "cboPhuongPhap";
            this.cboPhuongPhap.Size = new System.Drawing.Size(300, 24);
            this.cboPhuongPhap.TabIndex = 202;
            // 
            // lblDuBao
            // 
            this.lblDuBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuBao.Location = new System.Drawing.Point(20, 46);
            this.lblDuBao.Name = "lblDuBao";
            this.lblDuBao.Size = new System.Drawing.Size(760, 80);
            this.lblDuBao.TabIndex = 203;
            this.lblDuBao.Text = "Chọn một sản phẩm trong bảng rồi bấm nút. Dùng số lượng xuất mỗi ngày trong 60 ngày tính đến \"Đến ngày\" của báo cáo.";
            // 
            // Form_BaoCaoXuatNhapTon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtDenNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.txtTuNgay);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.dgvBaoCaoXuatNhapTon);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_BaoCaoXuatNhapTon";
            this.Load += new System.EventHandler(this.Form_BaoCaoXuatNhapTon_Load);
            this.Text = "Báo cáo xuất - nhập - tồn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCaoXuatNhapTon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDenNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.TextBox txtTuNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DataGridView dgvBaoCaoXuatNhapTon;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Button btnDuBao;
        private System.Windows.Forms.ComboBox cboPhuongPhap;
        private System.Windows.Forms.Label lblDuBao;
    }
}