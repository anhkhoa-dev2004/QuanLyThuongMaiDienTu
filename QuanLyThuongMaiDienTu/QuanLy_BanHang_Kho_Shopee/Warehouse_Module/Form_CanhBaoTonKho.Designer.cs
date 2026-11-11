namespace QuanLy_BanHang_Kho_Shopee.Warehouse_Module
{
    partial class Form_CanhBaoTonKho
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
            this.btnTinhLaiROP = new System.Windows.Forms.Button();
            this.lblSapXep = new System.Windows.Forms.Label();
            this.lblGoiY = new System.Windows.Forms.Label();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.dgvCanhBaoTonKho = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnBaoNhapHang = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBaoTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = true;
            this.lblThongBao.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Location = new System.Drawing.Point(28, 98);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(421, 27);
            this.lblThongBao.TabIndex = 56;
            this.lblThongBao.Text = "Tất cả sản phẩm (tự tính lại theo đơn và phiếu xuất mới nhất, bấm F5 để tải lại):";
            this.lblThongBao.Click += new System.EventHandler(this.lblThongBao_Click);
            // 
            // dgvCanhBaoTonKho
            // 
            this.dgvCanhBaoTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanhBaoTonKho.Location = new System.Drawing.Point(33, 139);
            this.dgvCanhBaoTonKho.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvCanhBaoTonKho.Name = "dgvCanhBaoTonKho";
            this.dgvCanhBaoTonKho.Size = new System.Drawing.Size(754, 226);
            this.dgvCanhBaoTonKho.TabIndex = 54;
            this.dgvCanhBaoTonKho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCanhBaoTonKho_CellContentClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(434, 395);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(184, 27);
            this.btnThoat.TabIndex = 53;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnBaoNhapHang
            // 
            this.btnBaoNhapHang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoNhapHang.Location = new System.Drawing.Point(167, 395);
            this.btnBaoNhapHang.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBaoNhapHang.Name = "btnBaoNhapHang";
            this.btnBaoNhapHang.Size = new System.Drawing.Size(186, 27);
            this.btnBaoNhapHang.TabIndex = 51;
            this.btnBaoNhapHang.Text = "Báo nhập hàng";
            this.btnBaoNhapHang.UseVisualStyleBackColor = true;
            this.btnBaoNhapHang.Click += new System.EventHandler(this.btnBaoNhapHang_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(83, 27);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(664, 36);
            this.lblTieuDe.TabIndex = 49;
            this.lblTieuDe.Text = "CẢNH BÁO HẾT HÀNG / TỒN NHIỀU";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // pnlBoSung
            // 
            this.pnlBoSung.Controls.Add(this.btnTinhLaiROP);
            this.pnlBoSung.Controls.Add(this.lblSapXep);
            this.pnlBoSung.Controls.Add(this.lblGoiY);
            this.pnlBoSung.BackColor = System.Drawing.Color.White;
            this.pnlBoSung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoSung.Location = new System.Drawing.Point(0, 450);
            this.pnlBoSung.Name = "pnlBoSung";
            this.pnlBoSung.Size = new System.Drawing.Size(800, 110);
            this.pnlBoSung.TabIndex = 200;
            // 
            // btnTinhLaiROP
            // 
            this.btnTinhLaiROP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinhLaiROP.Location = new System.Drawing.Point(20, 8);
            this.btnTinhLaiROP.Name = "btnTinhLaiROP";
            this.btnTinhLaiROP.Size = new System.Drawing.Size(420, 30);
            this.btnTinhLaiROP.TabIndex = 201;
            this.btnTinhLaiROP.Text = "Tính lại và tải lại";
            this.btnTinhLaiROP.UseVisualStyleBackColor = true;
            this.btnTinhLaiROP.Click += new System.EventHandler(this.btnTinhLaiROP_Click);
            // 
            // lblSapXep
            // 
            this.lblSapXep.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSapXep.Location = new System.Drawing.Point(450, 12);
            this.lblSapXep.Name = "lblSapXep";
            this.lblSapXep.Size = new System.Drawing.Size(330, 24);
            this.lblSapXep.TabIndex = 202;
            this.lblSapXep.Text = "Món cần đặt hàng gấp xếp trên cùng. Bấm 1 dòng để xem nên làm gì.";
            // 
            // lblGoiY
            // 
            this.lblGoiY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoiY.Location = new System.Drawing.Point(20, 46);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.Size = new System.Drawing.Size(760, 60);
            this.lblGoiY.TabIndex = 203;
            this.lblGoiY.Text = "";
            // 
            // Form_CanhBaoTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.pnlBoSung);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.dgvCanhBaoTonKho);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnBaoNhapHang);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_CanhBaoTonKho";
            this.Text = "Cảnh báo hết hàng / tồn nhiều";
            this.Load += new System.EventHandler(this.Form_CanhBaoTonKho_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBaoTonKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.DataGridView dgvCanhBaoTonKho;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnBaoNhapHang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBoSung;
        private System.Windows.Forms.Button btnTinhLaiROP;
        private System.Windows.Forms.Label lblSapXep;
        private System.Windows.Forms.Label lblGoiY;
    }
}