namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    partial class Form_TraCuuTonKho
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
            this.txtMaTenSanPham = new System.Windows.Forms.TextBox();
            this.lblMaTenSanPham = new System.Windows.Forms.Label();
            this.txtChonKho = new System.Windows.Forms.TextBox();
            this.lblChonKho = new System.Windows.Forms.Label();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaTenSanPham
            // 
            this.txtMaTenSanPham.Location = new System.Drawing.Point(163, 142);
            this.txtMaTenSanPham.Name = "txtMaTenSanPham";
            this.txtMaTenSanPham.Size = new System.Drawing.Size(198, 20);
            this.txtMaTenSanPham.TabIndex = 60;
            this.txtMaTenSanPham.TextChanged += new System.EventHandler(this.txtMaTenSanPham_TextChanged);
            // 
            // lblMaTenSanPham
            // 
            this.lblMaTenSanPham.AutoSize = true;
            this.lblMaTenSanPham.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaTenSanPham.Location = new System.Drawing.Point(33, 141);
            this.lblMaTenSanPham.Name = "lblMaTenSanPham";
            this.lblMaTenSanPham.Size = new System.Drawing.Size(82, 19);
            this.lblMaTenSanPham.TabIndex = 59;
            this.lblMaTenSanPham.Text = "Mã/Tên SP:";
            this.lblMaTenSanPham.Click += new System.EventHandler(this.lblMaTenSanPham_Click);
            // 
            // txtChonKho
            // 
            this.txtChonKho.Location = new System.Drawing.Point(163, 97);
            this.txtChonKho.Name = "txtChonKho";
            this.txtChonKho.Size = new System.Drawing.Size(198, 20);
            this.txtChonKho.TabIndex = 57;
            this.txtChonKho.TextChanged += new System.EventHandler(this.txtChonKho_TextChanged);
            // 
            // lblChonKho
            // 
            this.lblChonKho.AutoSize = true;
            this.lblChonKho.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonKho.Location = new System.Drawing.Point(33, 96);
            this.lblChonKho.Name = "lblChonKho";
            this.lblChonKho.Size = new System.Drawing.Size(72, 19);
            this.lblChonKho.TabIndex = 55;
            this.lblChonKho.Text = "Chọn kho:";
            this.lblChonKho.Click += new System.EventHandler(this.lblChonKho_Click);
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Location = new System.Drawing.Point(23, 267);
            this.dgvTonKho.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.Size = new System.Drawing.Size(754, 162);
            this.dgvTonKho.TabIndex = 54;
            this.dgvTonKho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTonKho_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(440, 211);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 53;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(214, 211);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(112, 27);
            this.btnTimKiem.TabIndex = 51;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(215, 21);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(319, 36);
            this.lblTieuDe.TabIndex = 49;
            this.lblTieuDe.Text = "TRA CỨU TỒN KHO";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // Form_TraCuuTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMaTenSanPham);
            this.Controls.Add(this.lblMaTenSanPham);
            this.Controls.Add(this.txtChonKho);
            this.Controls.Add(this.lblChonKho);
            this.Controls.Add(this.dgvTonKho);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_TraCuuTonKho";
            this.Text = "Tra cứu tồn kho";
            this.Load += new System.EventHandler(this.Form_TraCuuTonKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaTenSanPham;
        private System.Windows.Forms.Label lblMaTenSanPham;
        private System.Windows.Forms.TextBox txtChonKho;
        private System.Windows.Forms.Label lblChonKho;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblTieuDe;
    }
}