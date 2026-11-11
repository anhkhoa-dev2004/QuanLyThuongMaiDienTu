namespace QuanLy_BanHang_Kho_Shopee.Report_Module
{
    partial class Form_BangThongBao
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
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.tabThongBao = new System.Windows.Forms.TabControl();
            this.tpViecCanLam = new System.Windows.Forms.TabPage();
            this.lblViecCanLam = new System.Windows.Forms.Label();
            this.dgvViecCanLam = new System.Windows.Forms.DataGridView();
            this.tpLyDoMua = new System.Windows.Forms.TabPage();
            this.lblLyDoMua = new System.Windows.Forms.Label();
            this.dgvLyDoMua = new System.Windows.Forms.DataGridView();
            this.tpCamXuc = new System.Windows.Forms.TabPage();
            this.lblCamXucSanPham = new System.Windows.Forms.Label();
            this.dgvCamXucSanPham = new System.Windows.Forms.DataGridView();
            this.lblCamXucDanhMuc = new System.Windows.Forms.Label();
            this.dgvCamXucDanhMuc = new System.Windows.Forms.DataGridView();
            this.tpMuaTheoHung = new System.Windows.Forms.TabPage();
            this.lblMuaHungSanPham = new System.Windows.Forms.Label();
            this.dgvMuaHungSanPham = new System.Windows.Forms.DataGridView();
            this.lblMuaHungCaShop = new System.Windows.Forms.Label();
            this.dgvMuaHungCaShop = new System.Windows.Forms.DataGridView();
            this.lblMuaLai = new System.Windows.Forms.Label();
            this.dgvMuaLai = new System.Windows.Forms.DataGridView();
            this.tpCungCau = new System.Windows.Forms.TabPage();
            this.lblCungCauTong = new System.Windows.Forms.Label();
            this.dgvCungCauTong = new System.Windows.Forms.DataGridView();
            this.lblCungCauSanPham = new System.Windows.Forms.Label();
            this.dgvCungCauSanPham = new System.Windows.Forms.DataGridView();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViecCanLam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLyDoMua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCamXucSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCamXucDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaHungSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaHungCaShop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCungCauTong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCungCauSanPham)).BeginInit();
            this.tabThongBao.SuspendLayout();
            this.tpViecCanLam.SuspendLayout();
            this.tpLyDoMua.SuspendLayout();
            this.tpCamXuc.SuspendLayout();
            this.tpMuaTheoHung.SuspendLayout();
            this.tpCungCau.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(880, 56);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BẢNG THÔNG BÁO: KHÁCH VUI HAY BUỒN, VÌ SAO MUA";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenNgay.Location = new System.Drawing.Point(12, 70);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(95, 22);
            this.lblDenNgay.TabIndex = 1;
            this.lblDenNgay.Text = "Tính đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.ShowCheckBox = true;
            this.dtpDenNgay.Checked = false;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Location = new System.Drawing.Point(108, 66);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(140, 26);
            this.dtpDenNgay.TabIndex = 2;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.Location = new System.Drawing.Point(258, 64);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(190, 30);
            this.btnCapNhat.TabIndex = 3;
            this.btnCapNhat.Text = "Cập nhật số liệu và xem";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.Location = new System.Drawing.Point(458, 62);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(410, 40);
            this.lblTrangThai.TabIndex = 4;
            this.lblTrangThai.Text = "Đang chuẩn bị số liệu...";
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuongDan.Location = new System.Drawing.Point(12, 104);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(856, 36);
            this.lblHuongDan.TabIndex = 5;
            this.lblHuongDan.Text = "Bỏ đánh dấu ô ngày thì tính đến ngày cuối cùng có dữ liệu. Cách tính và các mức (60%, 80%, 90 ngày, 30 lượt) giống hệt file Python và báo cáo Power BI. Mỗi lần cập nhật, mức phải đặt hàng của từng sản phẩm cũng được tính lại.";
            // 
            // tabThongBao
            // 
            this.tabThongBao.Controls.Add(this.tpViecCanLam);
            this.tabThongBao.Controls.Add(this.tpLyDoMua);
            this.tabThongBao.Controls.Add(this.tpCamXuc);
            this.tabThongBao.Controls.Add(this.tpMuaTheoHung);
            this.tabThongBao.Controls.Add(this.tpCungCau);
            this.tabThongBao.Location = new System.Drawing.Point(12, 142);
            this.tabThongBao.Name = "tabThongBao";
            this.tabThongBao.Size = new System.Drawing.Size(856, 452);
            this.tabThongBao.TabIndex = 6;
            // 
            // tpViecCanLam
            // 
            this.tpViecCanLam.Controls.Add(this.lblViecCanLam);
            this.tpViecCanLam.Controls.Add(this.dgvViecCanLam);
            this.tpViecCanLam.AutoScroll = true;
            this.tpViecCanLam.BackColor = System.Drawing.Color.White;
            this.tpViecCanLam.Location = new System.Drawing.Point(4, 22);
            this.tpViecCanLam.Name = "tpViecCanLam";
            this.tpViecCanLam.Size = new System.Drawing.Size(848, 426);
            this.tpViecCanLam.TabIndex = 7;
            this.tpViecCanLam.Text = "Việc cần chú ý";
            this.tpViecCanLam.UseVisualStyleBackColor = true;
            // 
            // lblViecCanLam
            // 
            this.lblViecCanLam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViecCanLam.Location = new System.Drawing.Point(6, 6);
            this.lblViecCanLam.Name = "lblViecCanLam";
            this.lblViecCanLam.Size = new System.Drawing.Size(815, 36);
            this.lblViecCanLam.TabIndex = 8;
            this.lblViecCanLam.Text = "Sản phẩm cần đặt hàng (khẩn cấp xếp trên), sản phẩm khách không hài lòng, lý do mua nhiều nhất, khác biệt có thật giữa ngày sale và ngày thường:";
            // 
            // dgvViecCanLam
            // 
            this.dgvViecCanLam.AllowUserToAddRows = false;
            this.dgvViecCanLam.AllowUserToDeleteRows = false;
            this.dgvViecCanLam.ReadOnly = true;
            this.dgvViecCanLam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViecCanLam.Location = new System.Drawing.Point(6, 44);
            this.dgvViecCanLam.Name = "dgvViecCanLam";
            this.dgvViecCanLam.Size = new System.Drawing.Size(815, 370);
            this.dgvViecCanLam.TabIndex = 9;
            this.dgvViecCanLam.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvViecCanLam.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // tpLyDoMua
            // 
            this.tpLyDoMua.Controls.Add(this.lblLyDoMua);
            this.tpLyDoMua.Controls.Add(this.dgvLyDoMua);
            this.tpLyDoMua.AutoScroll = true;
            this.tpLyDoMua.BackColor = System.Drawing.Color.White;
            this.tpLyDoMua.Location = new System.Drawing.Point(4, 22);
            this.tpLyDoMua.Name = "tpLyDoMua";
            this.tpLyDoMua.Size = new System.Drawing.Size(848, 426);
            this.tpLyDoMua.TabIndex = 10;
            this.tpLyDoMua.Text = "1. Vì sao khách mua";
            this.tpLyDoMua.UseVisualStyleBackColor = true;
            // 
            // lblLyDoMua
            // 
            this.lblLyDoMua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLyDoMua.Location = new System.Drawing.Point(6, 6);
            this.lblLyDoMua.Name = "lblLyDoMua";
            this.lblLyDoMua.Size = new System.Drawing.Size(815, 36);
            this.lblLyDoMua.TabIndex = 11;
            this.lblLyDoMua.Text = "Mỗi đơn khách trả lời 1 câu \"Điều gì khiến bạn mua hôm nay?\". Câu trả lời được xếp vào nhóm có nhiều từ khóa khớp nhất (từ điển lý do mua).";
            // 
            // dgvLyDoMua
            // 
            this.dgvLyDoMua.AllowUserToAddRows = false;
            this.dgvLyDoMua.AllowUserToDeleteRows = false;
            this.dgvLyDoMua.ReadOnly = true;
            this.dgvLyDoMua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLyDoMua.Location = new System.Drawing.Point(6, 44);
            this.dgvLyDoMua.Name = "dgvLyDoMua";
            this.dgvLyDoMua.Size = new System.Drawing.Size(815, 370);
            this.dgvLyDoMua.TabIndex = 12;
            this.dgvLyDoMua.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvLyDoMua.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // tpCamXuc
            // 
            this.tpCamXuc.Controls.Add(this.lblCamXucSanPham);
            this.tpCamXuc.Controls.Add(this.dgvCamXucSanPham);
            this.tpCamXuc.Controls.Add(this.lblCamXucDanhMuc);
            this.tpCamXuc.Controls.Add(this.dgvCamXucDanhMuc);
            this.tpCamXuc.AutoScroll = true;
            this.tpCamXuc.BackColor = System.Drawing.Color.White;
            this.tpCamXuc.Location = new System.Drawing.Point(4, 22);
            this.tpCamXuc.Name = "tpCamXuc";
            this.tpCamXuc.Size = new System.Drawing.Size(848, 426);
            this.tpCamXuc.TabIndex = 13;
            this.tpCamXuc.Text = "2. Khách cảm thấy gì";
            this.tpCamXuc.UseVisualStyleBackColor = true;
            // 
            // lblCamXucSanPham
            // 
            this.lblCamXucSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamXucSanPham.Location = new System.Drawing.Point(6, 6);
            this.lblCamXucSanPham.Name = "lblCamXucSanPham";
            this.lblCamXucSanPham.Size = new System.Drawing.Size(815, 22);
            this.lblCamXucSanPham.TabIndex = 14;
            this.lblCamXucSanPham.Text = "Từng sản phẩm (% khách hài lòng = % đánh giá từ 4 sao trở lên; ĐỎ dưới 60%, VÀNG dưới 80%, XANH từ 80%):";
            // 
            // dgvCamXucSanPham
            // 
            this.dgvCamXucSanPham.AllowUserToAddRows = false;
            this.dgvCamXucSanPham.AllowUserToDeleteRows = false;
            this.dgvCamXucSanPham.ReadOnly = true;
            this.dgvCamXucSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCamXucSanPham.Location = new System.Drawing.Point(6, 30);
            this.dgvCamXucSanPham.Name = "dgvCamXucSanPham";
            this.dgvCamXucSanPham.Size = new System.Drawing.Size(815, 250);
            this.dgvCamXucSanPham.TabIndex = 15;
            this.dgvCamXucSanPham.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvCamXucSanPham.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // lblCamXucDanhMuc
            // 
            this.lblCamXucDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamXucDanhMuc.Location = new System.Drawing.Point(6, 288);
            this.lblCamXucDanhMuc.Name = "lblCamXucDanhMuc";
            this.lblCamXucDanhMuc.Size = new System.Drawing.Size(815, 22);
            this.lblCamXucDanhMuc.TabIndex = 16;
            this.lblCamXucDanhMuc.Text = "Theo danh mục:";
            // 
            // dgvCamXucDanhMuc
            // 
            this.dgvCamXucDanhMuc.AllowUserToAddRows = false;
            this.dgvCamXucDanhMuc.AllowUserToDeleteRows = false;
            this.dgvCamXucDanhMuc.ReadOnly = true;
            this.dgvCamXucDanhMuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCamXucDanhMuc.Location = new System.Drawing.Point(6, 312);
            this.dgvCamXucDanhMuc.Name = "dgvCamXucDanhMuc";
            this.dgvCamXucDanhMuc.Size = new System.Drawing.Size(815, 105);
            this.dgvCamXucDanhMuc.TabIndex = 17;
            this.dgvCamXucDanhMuc.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvCamXucDanhMuc.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // tpMuaTheoHung
            // 
            this.tpMuaTheoHung.Controls.Add(this.lblMuaHungSanPham);
            this.tpMuaTheoHung.Controls.Add(this.dgvMuaHungSanPham);
            this.tpMuaTheoHung.Controls.Add(this.lblMuaHungCaShop);
            this.tpMuaTheoHung.Controls.Add(this.dgvMuaHungCaShop);
            this.tpMuaTheoHung.Controls.Add(this.lblMuaLai);
            this.tpMuaTheoHung.Controls.Add(this.dgvMuaLai);
            this.tpMuaTheoHung.AutoScroll = true;
            this.tpMuaTheoHung.BackColor = System.Drawing.Color.White;
            this.tpMuaTheoHung.Location = new System.Drawing.Point(4, 22);
            this.tpMuaTheoHung.Name = "tpMuaTheoHung";
            this.tpMuaTheoHung.Size = new System.Drawing.Size(848, 426);
            this.tpMuaTheoHung.TabIndex = 18;
            this.tpMuaTheoHung.Text = "3. Mua theo hứng sale";
            this.tpMuaTheoHung.UseVisualStyleBackColor = true;
            // 
            // lblMuaHungSanPham
            // 
            this.lblMuaHungSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuaHungSanPham.Location = new System.Drawing.Point(6, 6);
            this.lblMuaHungSanPham.Name = "lblMuaHungSanPham";
            this.lblMuaHungSanPham.Size = new System.Drawing.Size(815, 22);
            this.lblMuaHungSanPham.TabIndex = 19;
            this.lblMuaHungSanPham.Text = "Từng sản phẩm (ngày sale bán gấp nhiều lần nhất xếp trên):";
            // 
            // dgvMuaHungSanPham
            // 
            this.dgvMuaHungSanPham.AllowUserToAddRows = false;
            this.dgvMuaHungSanPham.AllowUserToDeleteRows = false;
            this.dgvMuaHungSanPham.ReadOnly = true;
            this.dgvMuaHungSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMuaHungSanPham.Location = new System.Drawing.Point(6, 30);
            this.dgvMuaHungSanPham.Name = "dgvMuaHungSanPham";
            this.dgvMuaHungSanPham.Size = new System.Drawing.Size(815, 200);
            this.dgvMuaHungSanPham.TabIndex = 20;
            this.dgvMuaHungSanPham.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvMuaHungSanPham.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // lblMuaHungCaShop
            // 
            this.lblMuaHungCaShop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuaHungCaShop.Location = new System.Drawing.Point(6, 236);
            this.lblMuaHungCaShop.Name = "lblMuaHungCaShop";
            this.lblMuaHungCaShop.Size = new System.Drawing.Size(815, 22);
            this.lblMuaHungCaShop.TabIndex = 21;
            this.lblMuaHungCaShop.Text = "Cả cửa hàng (chỉ ghi \"Khác biệt có thật\" khi chênh lệch lớn hơn mức dao động do ít mẫu):";
            // 
            // dgvMuaHungCaShop
            // 
            this.dgvMuaHungCaShop.AllowUserToAddRows = false;
            this.dgvMuaHungCaShop.AllowUserToDeleteRows = false;
            this.dgvMuaHungCaShop.ReadOnly = true;
            this.dgvMuaHungCaShop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMuaHungCaShop.Location = new System.Drawing.Point(6, 260);
            this.dgvMuaHungCaShop.Name = "dgvMuaHungCaShop";
            this.dgvMuaHungCaShop.Size = new System.Drawing.Size(815, 120);
            this.dgvMuaHungCaShop.TabIndex = 22;
            this.dgvMuaHungCaShop.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvMuaHungCaShop.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // lblMuaLai
            // 
            this.lblMuaLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuaLai.Location = new System.Drawing.Point(6, 386);
            this.lblMuaLai.Name = "lblMuaLai";
            this.lblMuaLai.Size = new System.Drawing.Size(815, 22);
            this.lblMuaLai.TabIndex = 23;
            this.lblMuaLai.Text = "Khách chấm vui / buồn rồi có mua lại trong 60 ngày không:";
            // 
            // dgvMuaLai
            // 
            this.dgvMuaLai.AllowUserToAddRows = false;
            this.dgvMuaLai.AllowUserToDeleteRows = false;
            this.dgvMuaLai.ReadOnly = true;
            this.dgvMuaLai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMuaLai.Location = new System.Drawing.Point(6, 410);
            this.dgvMuaLai.Name = "dgvMuaLai";
            this.dgvMuaLai.Size = new System.Drawing.Size(815, 120);
            this.dgvMuaLai.TabIndex = 24;
            this.dgvMuaLai.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvMuaLai.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // tpCungCau
            // 
            this.tpCungCau.Controls.Add(this.lblCungCauTong);
            this.tpCungCau.Controls.Add(this.dgvCungCauTong);
            this.tpCungCau.Controls.Add(this.lblCungCauSanPham);
            this.tpCungCau.Controls.Add(this.dgvCungCauSanPham);
            this.tpCungCau.AutoScroll = true;
            this.tpCungCau.BackColor = System.Drawing.Color.White;
            this.tpCungCau.Location = new System.Drawing.Point(4, 22);
            this.tpCungCau.Name = "tpCungCau";
            this.tpCungCau.Size = new System.Drawing.Size(848, 426);
            this.tpCungCau.TabIndex = 25;
            this.tpCungCau.Text = "4. Kho hàng & món đang hot";
            this.tpCungCau.UseVisualStyleBackColor = true;
            // 
            // lblCungCauTong
            // 
            this.lblCungCauTong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCungCauTong.Location = new System.Drawing.Point(6, 6);
            this.lblCungCauTong.Name = "lblCungCauTong";
            this.lblCungCauTong.Size = new System.Drawing.Size(815, 22);
            this.lblCungCauTong.TabIndex = 26;
            this.lblCungCauTong.Text = "Số sản phẩm theo tình trạng:";
            // 
            // dgvCungCauTong
            // 
            this.dgvCungCauTong.AllowUserToAddRows = false;
            this.dgvCungCauTong.AllowUserToDeleteRows = false;
            this.dgvCungCauTong.ReadOnly = true;
            this.dgvCungCauTong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCungCauTong.Location = new System.Drawing.Point(6, 30);
            this.dgvCungCauTong.Name = "dgvCungCauTong";
            this.dgvCungCauTong.Size = new System.Drawing.Size(815, 125);
            this.dgvCungCauTong.TabIndex = 27;
            this.dgvCungCauTong.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvCungCauTong.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // lblCungCauSanPham
            // 
            this.lblCungCauSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCungCauSanPham.Location = new System.Drawing.Point(6, 162);
            this.lblCungCauSanPham.Name = "lblCungCauSanPham";
            this.lblCungCauSanPham.Size = new System.Drawing.Size(815, 22);
            this.lblCungCauSanPham.TabIndex = 28;
            this.lblCungCauSanPham.Text = "Từng sản phẩm (ĐANG HOT = 30 ngày gần bán nhanh hơn TB 90 ngày từ 20%; dự đoán 30 ngày tới = tốc độ bán x 30):";
            // 
            // dgvCungCauSanPham
            // 
            this.dgvCungCauSanPham.AllowUserToAddRows = false;
            this.dgvCungCauSanPham.AllowUserToDeleteRows = false;
            this.dgvCungCauSanPham.ReadOnly = true;
            this.dgvCungCauSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCungCauSanPham.Location = new System.Drawing.Point(6, 186);
            this.dgvCungCauSanPham.Name = "dgvCungCauSanPham";
            this.dgvCungCauSanPham.Size = new System.Drawing.Size(815, 232);
            this.dgvCungCauSanPham.TabIndex = 29;
            this.dgvCungCauSanPham.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.LuoiThongBao_DataBindingComplete);
            this.dgvCungCauSanPham.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LuoiThongBao_CellFormatting);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(768, 602);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.TabIndex = 30;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Form_BangThongBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 640);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.tabThongBao);
            this.Controls.Add(this.lblHuongDan);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_BangThongBao";
            this.Text = "Bảng thông báo: khách vui hay buồn, vì sao mua";
            this.Load += new System.EventHandler(this.Form_BangThongBao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViecCanLam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLyDoMua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCamXucSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCamXucDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaHungSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaHungCaShop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuaLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCungCauTong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCungCauSanPham)).EndInit();
            this.tabThongBao.ResumeLayout(false);
            this.tpViecCanLam.ResumeLayout(false);
            this.tpViecCanLam.PerformLayout();
            this.tpLyDoMua.ResumeLayout(false);
            this.tpLyDoMua.PerformLayout();
            this.tpCamXuc.ResumeLayout(false);
            this.tpCamXuc.PerformLayout();
            this.tpMuaTheoHung.ResumeLayout(false);
            this.tpMuaTheoHung.PerformLayout();
            this.tpCungCau.ResumeLayout(false);
            this.tpCungCau.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.TabControl tabThongBao;
        private System.Windows.Forms.TabPage tpViecCanLam;
        private System.Windows.Forms.Label lblViecCanLam;
        private System.Windows.Forms.DataGridView dgvViecCanLam;
        private System.Windows.Forms.TabPage tpLyDoMua;
        private System.Windows.Forms.Label lblLyDoMua;
        private System.Windows.Forms.DataGridView dgvLyDoMua;
        private System.Windows.Forms.TabPage tpCamXuc;
        private System.Windows.Forms.Label lblCamXucSanPham;
        private System.Windows.Forms.DataGridView dgvCamXucSanPham;
        private System.Windows.Forms.Label lblCamXucDanhMuc;
        private System.Windows.Forms.DataGridView dgvCamXucDanhMuc;
        private System.Windows.Forms.TabPage tpMuaTheoHung;
        private System.Windows.Forms.Label lblMuaHungSanPham;
        private System.Windows.Forms.DataGridView dgvMuaHungSanPham;
        private System.Windows.Forms.Label lblMuaHungCaShop;
        private System.Windows.Forms.DataGridView dgvMuaHungCaShop;
        private System.Windows.Forms.Label lblMuaLai;
        private System.Windows.Forms.DataGridView dgvMuaLai;
        private System.Windows.Forms.TabPage tpCungCau;
        private System.Windows.Forms.Label lblCungCauTong;
        private System.Windows.Forms.DataGridView dgvCungCauTong;
        private System.Windows.Forms.Label lblCungCauSanPham;
        private System.Windows.Forms.DataGridView dgvCungCauSanPham;
        private System.Windows.Forms.Button btnThoat;
    }
}
