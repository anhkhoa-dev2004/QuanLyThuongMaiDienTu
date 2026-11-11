namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    partial class Form_ViTriKho
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
            this.btnGoiYAbc = new System.Windows.Forms.Button();
            this.lblAbc = new System.Windows.Forms.Label();
            this.dgvAbc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbc)).BeginInit();
            this.txtTenViTri = new System.Windows.Forms.TextBox();
            this.lblTenViTri = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtMaViTri = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblMaViTri = new System.Windows.Forms.Label();
            this.dgvViTriKho = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViTriKho)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTenViTri
            // 
            this.txtTenViTri.Location = new System.Drawing.Point(546, 89);
            this.txtTenViTri.Name = "txtTenViTri";
            this.txtTenViTri.Size = new System.Drawing.Size(198, 20);
            this.txtTenViTri.TabIndex = 48;
            this.txtTenViTri.TextChanged += new System.EventHandler(this.txtTenViTri_TextChanged);
            // 
            // lblTenViTri
            // 
            this.lblTenViTri.AutoSize = true;
            this.lblTenViTri.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenViTri.Location = new System.Drawing.Point(450, 88);
            this.lblTenViTri.Name = "lblTenViTri";
            this.lblTenViTri.Size = new System.Drawing.Size(65, 19);
            this.lblTenViTri.TabIndex = 47;
            this.lblTenViTri.Text = "Tên vị trí:";
            this.lblTenViTri.Click += new System.EventHandler(this.lblTenViTri_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(183, 130);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(198, 20);
            this.txtGhiChu.TabIndex = 44;
            this.txtGhiChu.TextChanged += new System.EventHandler(this.txtGhiChu_TextChanged);
            // 
            // txtMaViTri
            // 
            this.txtMaViTri.Location = new System.Drawing.Point(183, 90);
            this.txtMaViTri.Name = "txtMaViTri";
            this.txtMaViTri.Size = new System.Drawing.Size(198, 20);
            this.txtMaViTri.TabIndex = 43;
            this.txtMaViTri.TextChanged += new System.EventHandler(this.txtMaViTri_TextChanged);
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.Location = new System.Drawing.Point(45, 134);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(58, 19);
            this.lblGhiChu.TabIndex = 42;
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.Click += new System.EventHandler(this.lblGhiChu_Click);
            // 
            // lblMaViTri
            // 
            this.lblMaViTri.AutoSize = true;
            this.lblMaViTri.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaViTri.Location = new System.Drawing.Point(45, 89);
            this.lblMaViTri.Name = "lblMaViTri";
            this.lblMaViTri.Size = new System.Drawing.Size(60, 19);
            this.lblMaViTri.TabIndex = 41;
            this.lblMaViTri.Text = "Mã vị trí";
            this.lblMaViTri.Click += new System.EventHandler(this.lblMaViTri_Click);
            // 
            // dgvViTriKho
            // 
            this.dgvViTriKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViTriKho.Location = new System.Drawing.Point(23, 267);
            this.dgvViTriKho.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvViTriKho.Name = "dgvViTriKho";
            this.dgvViTriKho.Size = new System.Drawing.Size(754, 162);
            this.dgvViTriKho.TabIndex = 40;
            this.dgvViTriKho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViTriKho_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(618, 222);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 39;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(263, 222);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(118, 27);
            this.btnXoa.TabIndex = 38;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(424, 222);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(142, 27);
            this.btnSua.TabIndex = 37;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(71, 222);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(144, 27);
            this.btnThem.TabIndex = 36;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(215, 21);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(348, 36);
            this.lblTieuDe.TabIndex = 35;
            this.lblTieuDe.Text = "QUẢN LÝ VỊ TRÍ KHO";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.btnGoiYAbc);
            this.pnlBoSung.Controls.Add(this.lblAbc);
            this.pnlBoSung.Controls.Add(this.dgvAbc);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 230);
            this.pnlBoSung.TabIndex = 200;
            // 
            // btnGoiYAbc
            // 
            this.btnGoiYAbc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoiYAbc.Location = new System.Drawing.Point(20, 8);
            this.btnGoiYAbc.Name = "btnGoiYAbc";
            this.btnGoiYAbc.Size = new System.Drawing.Size(230, 30);
            this.btnGoiYAbc.TabIndex = 201;
            this.btnGoiYAbc.Text = "Gợi ý xếp hàng theo nhóm A / B / C";
            this.btnGoiYAbc.UseVisualStyleBackColor = true;
            this.btnGoiYAbc.Click += new System.EventHandler(this.btnGoiYAbc_Click);
            // 
            // lblAbc
            // 
            this.lblAbc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbc.Location = new System.Drawing.Point(20, 44);
            this.lblAbc.Name = "lblAbc";
            this.lblAbc.Size = new System.Drawing.Size(760, 48);
            this.lblAbc.TabIndex = 202;
            this.lblAbc.Text = "Bấm nút để xếp sản phẩm theo sản lượng bán và chia nhóm A / B / C.";
            // 
            // dgvAbc
            // 
            this.dgvAbc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbc.Location = new System.Drawing.Point(20, 96);
            this.dgvAbc.Name = "dgvAbc";
            this.dgvAbc.Size = new System.Drawing.Size(760, 130);
            this.dgvAbc.TabIndex = 203;
            // 
            // Form_ViTriKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 680);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtTenViTri);
            this.Controls.Add(this.lblTenViTri);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.txtMaViTri);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblMaViTri);
            this.Controls.Add(this.dgvViTriKho);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_ViTriKho";
            this.Text = "Quản lý kho và vị trí kệ ô";
            this.Load += new System.EventHandler(this.Form_ViTriKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViTriKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTenViTri;
        private System.Windows.Forms.Label lblTenViTri;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtMaViTri;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblMaViTri;
        private System.Windows.Forms.DataGridView dgvViTriKho;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Button btnGoiYAbc;
        private System.Windows.Forms.Label lblAbc;
        private System.Windows.Forms.DataGridView dgvAbc;
    }
}