namespace QuanLy_BanHang_Kho_Shopee.Sales_Module
{
    partial class Form_DotSale
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.lblMaDot = new System.Windows.Forms.Label();
            this.txtMaDot = new System.Windows.Forms.TextBox();
            this.lblTenDot = new System.Windows.Forms.Label();
            this.txtTenDot = new System.Windows.Forms.TextBox();
            this.lblGiamToiDa = new System.Windows.Forms.Label();
            this.numGiamToiDa = new System.Windows.Forms.NumericUpDown();
            this.lblBatDau = new System.Windows.Forms.Label();
            this.dtpBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblKetThuc = new System.Windows.Forms.Label();
            this.dtpKetThuc = new System.Windows.Forms.DateTimePicker();
            this.lblNgayChinh = new System.Windows.Forms.Label();
            this.dtpNgayChinh = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChuNgayChinh = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgvDotSale = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamToiDa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDotSale)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(880, 56);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ ĐỢT SALE";
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuongDan.Location = new System.Drawing.Point(12, 64);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(856, 38);
            this.lblHuongDan.TabIndex = 1;
            this.lblHuongDan.Text = "Đơn đặt trong khoảng Bắt đầu - Kết thúc được tính là đơn ngày sale ở Bảng thông báo (phần Mua theo hứng sale). Hai đợt không được trùng ngày nhau. Sửa đợt sale xong, bấm \"Cập nhật số liệu và xem\" ở Bảng thông báo để thấy số mới.";
            // 
            // lblMaDot
            // 
            this.lblMaDot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDot.Location = new System.Drawing.Point(12, 112);
            this.lblMaDot.Name = "lblMaDot";
            this.lblMaDot.Size = new System.Drawing.Size(60, 22);
            this.lblMaDot.TabIndex = 2;
            this.lblMaDot.Text = "Mã đợt:";
            // 
            // txtMaDot
            // 
            this.txtMaDot.MaxLength = 20;
            this.txtMaDot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDot.Location = new System.Drawing.Point(75, 108);
            this.txtMaDot.Name = "txtMaDot";
            this.txtMaDot.Size = new System.Drawing.Size(110, 24);
            this.txtMaDot.TabIndex = 3;
            // 
            // lblTenDot
            // 
            this.lblTenDot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDot.Location = new System.Drawing.Point(200, 112);
            this.lblTenDot.Name = "lblTenDot";
            this.lblTenDot.Size = new System.Drawing.Size(60, 22);
            this.lblTenDot.TabIndex = 4;
            this.lblTenDot.Text = "Tên đợt:";
            // 
            // txtTenDot
            // 
            this.txtTenDot.MaxLength = 100;
            this.txtTenDot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDot.Location = new System.Drawing.Point(262, 108);
            this.txtTenDot.Name = "txtTenDot";
            this.txtTenDot.Size = new System.Drawing.Size(220, 24);
            this.txtTenDot.TabIndex = 5;
            // 
            // lblGiamToiDa
            // 
            this.lblGiamToiDa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiamToiDa.Location = new System.Drawing.Point(500, 112);
            this.lblGiamToiDa.Name = "lblGiamToiDa";
            this.lblGiamToiDa.Size = new System.Drawing.Size(95, 22);
            this.lblGiamToiDa.TabIndex = 6;
            this.lblGiamToiDa.Text = "Giảm tối đa %:";
            // 
            // numGiamToiDa
            // 
            this.numGiamToiDa.DecimalPlaces = 2;
            this.numGiamToiDa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numGiamToiDa.Location = new System.Drawing.Point(598, 108);
            this.numGiamToiDa.Name = "numGiamToiDa";
            this.numGiamToiDa.Size = new System.Drawing.Size(80, 24);
            this.numGiamToiDa.TabIndex = 7;
            // 
            // lblBatDau
            // 
            this.lblBatDau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatDau.Location = new System.Drawing.Point(12, 148);
            this.lblBatDau.Name = "lblBatDau";
            this.lblBatDau.Size = new System.Drawing.Size(60, 22);
            this.lblBatDau.TabIndex = 8;
            this.lblBatDau.Text = "Bắt đầu:";
            // 
            // dtpBatDau
            // 
            this.dtpBatDau.CustomFormat = "dd/MM/yyyy";
            this.dtpBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBatDau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBatDau.Location = new System.Drawing.Point(75, 144);
            this.dtpBatDau.Name = "dtpBatDau";
            this.dtpBatDau.Size = new System.Drawing.Size(120, 24);
            this.dtpBatDau.TabIndex = 9;
            // 
            // lblKetThuc
            // 
            this.lblKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetThuc.Location = new System.Drawing.Point(210, 148);
            this.lblKetThuc.Name = "lblKetThuc";
            this.lblKetThuc.Size = new System.Drawing.Size(60, 22);
            this.lblKetThuc.TabIndex = 10;
            this.lblKetThuc.Text = "Kết thúc:";
            // 
            // dtpKetThuc
            // 
            this.dtpKetThuc.CustomFormat = "dd/MM/yyyy";
            this.dtpKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpKetThuc.Location = new System.Drawing.Point(272, 144);
            this.dtpKetThuc.Name = "dtpKetThuc";
            this.dtpKetThuc.Size = new System.Drawing.Size(120, 24);
            this.dtpKetThuc.TabIndex = 11;
            // 
            // lblNgayChinh
            // 
            this.lblNgayChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayChinh.Location = new System.Drawing.Point(410, 148);
            this.lblNgayChinh.Name = "lblNgayChinh";
            this.lblNgayChinh.Size = new System.Drawing.Size(75, 22);
            this.lblNgayChinh.TabIndex = 12;
            this.lblNgayChinh.Text = "Ngày chính:";
            // 
            // dtpNgayChinh
            // 
            this.dtpNgayChinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayChinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayChinh.ShowCheckBox = true;
            this.dtpNgayChinh.Checked = false;
            this.dtpNgayChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayChinh.Location = new System.Drawing.Point(488, 144);
            this.dtpNgayChinh.Name = "dtpNgayChinh";
            this.dtpNgayChinh.Size = new System.Drawing.Size(130, 24);
            this.dtpNgayChinh.TabIndex = 13;
            // 
            // lblGhiChuNgayChinh
            // 
            this.lblGhiChuNgayChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChuNgayChinh.Location = new System.Drawing.Point(625, 148);
            this.lblGhiChuNgayChinh.Name = "lblGhiChuNgayChinh";
            this.lblGhiChuNgayChinh.Size = new System.Drawing.Size(245, 22);
            this.lblGhiChuNgayChinh.TabIndex = 14;
            this.lblGhiChuNgayChinh.Text = "(đánh dấu nếu đợt có 1 ngày chính, ví dụ 11.11)";
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(12, 182);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 30);
            this.btnThem.TabIndex = 15;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(122, 182);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 30);
            this.btnSua.TabIndex = 16;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(232, 182);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 30);
            this.btnXoa.TabIndex = 17;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.Location = new System.Drawing.Point(342, 182);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 18;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // dgvDotSale
            // 
            this.dgvDotSale.AllowUserToAddRows = false;
            this.dgvDotSale.AllowUserToDeleteRows = false;
            this.dgvDotSale.ReadOnly = true;
            this.dgvDotSale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDotSale.Location = new System.Drawing.Point(12, 222);
            this.dgvDotSale.Name = "dgvDotSale";
            this.dgvDotSale.Size = new System.Drawing.Size(856, 372);
            this.dgvDotSale.TabIndex = 19;
            this.dgvDotSale.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDotSale_CellClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(768, 602);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.TabIndex = 20;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_DotSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 640);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.dgvDotSale);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblGhiChuNgayChinh);
            this.Controls.Add(this.dtpNgayChinh);
            this.Controls.Add(this.lblNgayChinh);
            this.Controls.Add(this.dtpKetThuc);
            this.Controls.Add(this.lblKetThuc);
            this.Controls.Add(this.dtpBatDau);
            this.Controls.Add(this.lblBatDau);
            this.Controls.Add(this.numGiamToiDa);
            this.Controls.Add(this.lblGiamToiDa);
            this.Controls.Add(this.txtTenDot);
            this.Controls.Add(this.lblTenDot);
            this.Controls.Add(this.txtMaDot);
            this.Controls.Add(this.lblMaDot);
            this.Controls.Add(this.lblHuongDan);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_DotSale";
            this.Text = "Quản lý đợt sale";
            this.Load += new System.EventHandler(this.Form_DotSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numGiamToiDa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDotSale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.Label lblMaDot;
        private System.Windows.Forms.TextBox txtMaDot;
        private System.Windows.Forms.Label lblTenDot;
        private System.Windows.Forms.TextBox txtTenDot;
        private System.Windows.Forms.Label lblGiamToiDa;
        private System.Windows.Forms.NumericUpDown numGiamToiDa;
        private System.Windows.Forms.Label lblBatDau;
        private System.Windows.Forms.DateTimePicker dtpBatDau;
        private System.Windows.Forms.Label lblKetThuc;
        private System.Windows.Forms.DateTimePicker dtpKetThuc;
        private System.Windows.Forms.Label lblNgayChinh;
        private System.Windows.Forms.DateTimePicker dtpNgayChinh;
        private System.Windows.Forms.Label lblGhiChuNgayChinh;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvDotSale;
        private System.Windows.Forms.Button btnThoat;
    }
}
