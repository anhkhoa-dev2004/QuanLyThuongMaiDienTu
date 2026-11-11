namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    partial class Form_DangNhap
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
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnDangKyTk = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblTenTK = new System.Windows.Forms.Label();
            this.lblMk = new System.Windows.Forms.Label();
            this.txtTenTK = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnQuenMatKhau = new System.Windows.Forms.Button();
            this.btnTroGiup = new System.Windows.Forms.Button();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(264, 32);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(384, 36);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐĂNG NHẬP HỆ THỐNG";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.Location = new System.Drawing.Point(657, 98);
            this.btnDangNhap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(134, 27);
            this.btnDangNhap.TabIndex = 1;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnDangKyTk
            // 
            this.btnDangKyTk.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKyTk.Location = new System.Drawing.Point(657, 167);
            this.btnDangKyTk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDangKyTk.Name = "btnDangKyTk";
            this.btnDangKyTk.Size = new System.Drawing.Size(134, 27);
            this.btnDangKyTk.TabIndex = 2;
            this.btnDangKyTk.Text = "Đăng ký";
            this.btnDangKyTk.UseVisualStyleBackColor = true;
            this.btnDangKyTk.Click += new System.EventHandler(this.btnDangKyTk_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(605, 330);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(146, 27);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // lblTenTK
            // 
            this.lblTenTK.AutoSize = true;
            this.lblTenTK.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenTK.Location = new System.Drawing.Point(151, 139);
            this.lblTenTK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenTK.Name = "lblTenTK";
            this.lblTenTK.Size = new System.Drawing.Size(125, 21);
            this.lblTenTK.TabIndex = 4;
            this.lblTenTK.Text = "Tên đăng nhập:";
            // 
            // lblMk
            // 
            this.lblMk.AutoSize = true;
            this.lblMk.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMk.Location = new System.Drawing.Point(151, 210);
            this.lblMk.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMk.Name = "lblMk";
            this.lblMk.Size = new System.Drawing.Size(83, 21);
            this.lblMk.TabIndex = 5;
            this.lblMk.Text = "Mật khẩu:";
            this.lblMk.Click += new System.EventHandler(this.lblMk_Click);
            // 
            // txtTenTK
            // 
            this.txtTenTK.Location = new System.Drawing.Point(316, 133);
            this.txtTenTK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTenTK.Name = "txtTenTK";
            this.txtTenTK.Size = new System.Drawing.Size(230, 21);
            this.txtTenTK.TabIndex = 6;
            this.txtTenTK.TextChanged += new System.EventHandler(this.txtTenTK_TextChanged);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(316, 202);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(230, 21);
            this.txtMatKhau.TabIndex = 7;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txtMatKhau_TextChanged);
            // 
            // btnQuenMatKhau
            // 
            this.btnQuenMatKhau.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuenMatKhau.Location = new System.Drawing.Point(386, 330);
            this.btnQuenMatKhau.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuenMatKhau.Name = "btnQuenMatKhau";
            this.btnQuenMatKhau.Size = new System.Drawing.Size(150, 27);
            this.btnQuenMatKhau.TabIndex = 8;
            this.btnQuenMatKhau.Text = "Quên mật khẩu";
            this.btnQuenMatKhau.UseVisualStyleBackColor = true;
            this.btnQuenMatKhau.Click += new System.EventHandler(this.btnQuenMatKhau_Click);
            // 
            // btnTroGiup
            // 
            this.btnTroGiup.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTroGiup.Location = new System.Drawing.Point(657, 237);
            this.btnTroGiup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTroGiup.Name = "btnTroGiup";
            this.btnTroGiup.Size = new System.Drawing.Size(134, 27);
            this.btnTroGiup.TabIndex = 9;
            this.btnTroGiup.Text = "Trợ giúp";
            this.btnTroGiup.UseVisualStyleBackColor = true;
            this.btnTroGiup.Click += new System.EventHandler(this.btnTroGiup_Click);
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMatKhau.Location = new System.Drawing.Point(178, 330);
            this.btnDoiMatKhau.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(135, 27);
            this.btnDoiMatKhau.TabIndex = 10;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = true;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // Form_DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 520);
            this.Controls.Add(this.btnDoiMatKhau);
            this.Controls.Add(this.btnTroGiup);
            this.Controls.Add(this.btnQuenMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTenTK);
            this.Controls.Add(this.lblMk);
            this.Controls.Add(this.lblTenTK);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangKyTk);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form_DangNhap";
            this.Text = "Đăng nhập hệ thống";
            this.Load += new System.EventHandler(this.Form_DangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnDangKyTk;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblTenTK;
        private System.Windows.Forms.Label lblMk;
        private System.Windows.Forms.TextBox txtTenTK;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnQuenMatKhau;
        private System.Windows.Forms.Button btnTroGiup;
        private System.Windows.Forms.Button btnDoiMatKhau;
    }
}