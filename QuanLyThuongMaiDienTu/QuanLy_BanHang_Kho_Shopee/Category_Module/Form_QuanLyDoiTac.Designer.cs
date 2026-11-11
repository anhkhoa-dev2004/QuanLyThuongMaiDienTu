namespace QuanLy_BanHang_Kho_Shopee.Category_Module
{
    partial class Form_QuanLyDoiTac
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
            this.lblKenh = new System.Windows.Forms.Label();
            this.lblVanChuyen = new System.Windows.Forms.Label();
            this.dgvVanChuyen = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVanChuyen)).BeginInit();
            this.txtTenDoiTac = new System.Windows.Forms.TextBox();
            this.lblTenDoiTac = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtMaDoiTac = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.lblMaDoiTac = new System.Windows.Forms.Label();
            this.dgvDoiTac = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoiTac)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTenDoiTac
            // 
            this.txtTenDoiTac.Location = new System.Drawing.Point(541, 97);
            this.txtTenDoiTac.Name = "txtTenDoiTac";
            this.txtTenDoiTac.Size = new System.Drawing.Size(198, 20);
            this.txtTenDoiTac.TabIndex = 80;
            this.txtTenDoiTac.TextChanged += new System.EventHandler(this.txtTenDoiTac_TextChanged);
            // 
            // lblTenDoiTac
            // 
            this.lblTenDoiTac.AutoSize = true;
            this.lblTenDoiTac.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDoiTac.Location = new System.Drawing.Point(420, 97);
            this.lblTenDoiTac.Name = "lblTenDoiTac";
            this.lblTenDoiTac.Size = new System.Drawing.Size(80, 19);
            this.lblTenDoiTac.TabIndex = 79;
            this.lblTenDoiTac.Text = "Tên đối tác:";
            this.lblTenDoiTac.Click += new System.EventHandler(this.lblTenDoiTac_Click);
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(538, 143);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(198, 20);
            this.txtDiaChi.TabIndex = 78;
            this.txtDiaChi.TextChanged += new System.EventHandler(this.txtDiaChi_TextChanged);
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiaChi.Location = new System.Drawing.Point(420, 143);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(54, 19);
            this.lblDiaChi.TabIndex = 77;
            this.lblDiaChi.Text = "Địa chỉ:";
            this.lblDiaChi.Click += new System.EventHandler(this.lblDiaChi_Click);
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(175, 139);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(198, 20);
            this.txtSoDienThoai.TabIndex = 76;
            this.txtSoDienThoai.TextChanged += new System.EventHandler(this.txtSoDienThoai_TextChanged);
            // 
            // txtMaDoiTac
            // 
            this.txtMaDoiTac.Location = new System.Drawing.Point(175, 99);
            this.txtMaDoiTac.Name = "txtMaDoiTac";
            this.txtMaDoiTac.Size = new System.Drawing.Size(198, 20);
            this.txtMaDoiTac.TabIndex = 75;
            this.txtMaDoiTac.TextChanged += new System.EventHandler(this.txtMaDoiTac_TextChanged);
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDienThoai.Location = new System.Drawing.Point(37, 143);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(73, 19);
            this.lblDienThoai.TabIndex = 74;
            this.lblDienThoai.Text = "Điện thoại:";
            this.lblDienThoai.Click += new System.EventHandler(this.lblDienThoai_Click);
            // 
            // lblMaDoiTac
            // 
            this.lblMaDoiTac.AutoSize = true;
            this.lblMaDoiTac.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDoiTac.Location = new System.Drawing.Point(37, 98);
            this.lblMaDoiTac.Name = "lblMaDoiTac";
            this.lblMaDoiTac.Size = new System.Drawing.Size(78, 19);
            this.lblMaDoiTac.TabIndex = 73;
            this.lblMaDoiTac.Text = "Mã đối tác:";
            this.lblMaDoiTac.Click += new System.EventHandler(this.lblMaDoiTac_Click);
            // 
            // dgvDoiTac
            // 
            this.dgvDoiTac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoiTac.Location = new System.Drawing.Point(23, 267);
            this.dgvDoiTac.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvDoiTac.Name = "dgvDoiTac";
            this.dgvDoiTac.Size = new System.Drawing.Size(754, 162);
            this.dgvDoiTac.TabIndex = 72;
            this.dgvDoiTac.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoiTac_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(618, 222);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 27);
            this.btnThoat.TabIndex = 71;
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
            this.btnXoa.TabIndex = 70;
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
            this.btnSua.TabIndex = 69;
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
            this.btnThem.TabIndex = 68;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(225, 21);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(289, 36);
            this.lblTieuDe.TabIndex = 67;
            this.lblTieuDe.Text = "QUẢN LÝ ĐỐI TÁC";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.lblKenh);
            this.pnlBoSung.Controls.Add(this.lblVanChuyen);
            this.pnlBoSung.Controls.Add(this.dgvVanChuyen);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 200);
            this.pnlBoSung.TabIndex = 200;
            // 
            // lblKenh
            // 
            this.lblKenh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKenh.Location = new System.Drawing.Point(20, 6);
            this.lblKenh.Name = "lblKenh";
            this.lblKenh.Size = new System.Drawing.Size(760, 44);
            this.lblKenh.TabIndex = 201;
            this.lblKenh.Text = "Chọn một đối tác trong bảng để xem kết quả bán hàng của kênh đó.";
            // 
            // lblVanChuyen
            // 
            this.lblVanChuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVanChuyen.Location = new System.Drawing.Point(20, 54);
            this.lblVanChuyen.Name = "lblVanChuyen";
            this.lblVanChuyen.Size = new System.Drawing.Size(760, 22);
            this.lblVanChuyen.TabIndex = 202;
            this.lblVanChuyen.Text = "Đơn vị vận chuyển (lấy từ bảng Vận đơn; điểm = số đơn đã giao / (đã giao + hoàn hoặc thất bại) x 100):";
            // 
            // dgvVanChuyen
            // 
            this.dgvVanChuyen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVanChuyen.Location = new System.Drawing.Point(20, 78);
            this.dgvVanChuyen.Name = "dgvVanChuyen";
            this.dgvVanChuyen.Size = new System.Drawing.Size(760, 116);
            this.dgvVanChuyen.TabIndex = 203;
            // 
            // Form_QuanLyDoiTac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.txtTenDoiTac);
            this.Controls.Add(this.lblTenDoiTac);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.txtMaDoiTac);
            this.Controls.Add(this.lblDienThoai);
            this.Controls.Add(this.lblMaDoiTac);
            this.Controls.Add(this.dgvDoiTac);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_QuanLyDoiTac";
            this.Text = "Quản lý đối tác và chi nhánh";
            this.Load += new System.EventHandler(this.Form_QuanLyDoiTac_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoiTac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVanChuyen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTenDoiTac;
        private System.Windows.Forms.Label lblTenDoiTac;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtMaDoiTac;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.Label lblMaDoiTac;
        private System.Windows.Forms.DataGridView dgvDoiTac;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Label lblKenh;
        private System.Windows.Forms.Label lblVanChuyen;
        private System.Windows.Forms.DataGridView dgvVanChuyen;
    }
}