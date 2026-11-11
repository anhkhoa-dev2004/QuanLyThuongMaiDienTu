namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    partial class Form_QuenMatKhau
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
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtTenTK = new System.Windows.Forms.TextBox();
            this.lblMk = new System.Windows.Forms.Label();
            this.lblTenTK = new System.Windows.Forms.Label();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.btnDoiMk = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtSoDT = new System.Windows.Forms.TextBox();
            this.lblSoDT = new System.Windows.Forms.Label();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(308, 224);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(198, 20);
            this.txtMatKhau.TabIndex = 31;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txtMatKhau_TextChanged);
            // 
            // txtTenTK
            // 
            this.txtTenTK.Location = new System.Drawing.Point(308, 118);
            this.txtTenTK.Name = "txtTenTK";
            this.txtTenTK.Size = new System.Drawing.Size(198, 20);
            this.txtTenTK.TabIndex = 30;
            this.txtTenTK.TextChanged += new System.EventHandler(this.txtTenTK_TextChanged);
            // 
            // lblMk
            // 
            this.lblMk.AutoSize = true;
            this.lblMk.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMk.Location = new System.Drawing.Point(148, 225);
            this.lblMk.Name = "lblMk";
            this.lblMk.Size = new System.Drawing.Size(93, 19);
            this.lblMk.TabIndex = 29;
            this.lblMk.Text = "Mật khẩu mới";
            this.lblMk.Click += new System.EventHandler(this.lblMk_Click);
            // 
            // lblTenTK
            // 
            this.lblTenTK.AutoSize = true;
            this.lblTenTK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenTK.Location = new System.Drawing.Point(148, 116);
            this.lblTenTK.Name = "lblTenTK";
            this.lblTenTK.Size = new System.Drawing.Size(98, 19);
            this.lblTenTK.TabIndex = 28;
            this.lblTenTK.Text = "Tên đăng nhập";
            this.lblTenTK.Click += new System.EventHandler(this.lblTenTK_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.Location = new System.Drawing.Point(381, 349);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(125, 36);
            this.btnQuayLai.TabIndex = 27;
            this.btnQuayLai.Text = "Quay lại";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // btnDoiMk
            // 
            this.btnDoiMk.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMk.Location = new System.Drawing.Point(186, 349);
            this.btnDoiMk.Name = "btnDoiMk";
            this.btnDoiMk.Size = new System.Drawing.Size(115, 36);
            this.btnDoiMk.TabIndex = 26;
            this.btnDoiMk.Text = "Đổi mật khẩu";
            this.btnDoiMk.UseVisualStyleBackColor = true;
            this.btnDoiMk.Click += new System.EventHandler(this.btnDoiMk_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(225, 31);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(294, 36);
            this.lblTieuDe.TabIndex = 25;
            this.lblTieuDe.Text = "QUÊN MẬT KHẨU";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // txtSoDT
            // 
            this.txtSoDT.Location = new System.Drawing.Point(308, 172);
            this.txtSoDT.Name = "txtSoDT";
            this.txtSoDT.Size = new System.Drawing.Size(198, 20);
            this.txtSoDT.TabIndex = 39;
            this.txtSoDT.TextChanged += new System.EventHandler(this.txtSoDT_TextChanged);
            // 
            // lblSoDT
            // 
            this.lblSoDT.AutoSize = true;
            this.lblSoDT.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoDT.Location = new System.Drawing.Point(148, 172);
            this.lblSoDT.Name = "lblSoDT";
            this.lblSoDT.Size = new System.Drawing.Size(91, 19);
            this.lblSoDT.TabIndex = 38;
            this.lblSoDT.Text = "Số điện thoại:";
            this.lblSoDT.Click += new System.EventHandler(this.lblSoDT_Click);
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = true;
            this.lblThongBao.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Location = new System.Drawing.Point(148, 278);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(77, 19);
            this.lblThongBao.TabIndex = 40;
            this.lblThongBao.Text = "Thông báo:";
            this.lblThongBao.Click += new System.EventHandler(this.lblThongBao_Click);
            // 
            // Form_QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.txtSoDT);
            this.Controls.Add(this.lblSoDT);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTenTK);
            this.Controls.Add(this.lblMk);
            this.Controls.Add(this.lblTenTK);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnDoiMk);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "Form_QuenMatKhau";
            this.Text = "Quên mật khẩu";
            this.Load += new System.EventHandler(this.Form_QuenMatKhau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtTenTK;
        private System.Windows.Forms.Label lblMk;
        private System.Windows.Forms.Label lblTenTK;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Button btnDoiMk;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtSoDT;
        private System.Windows.Forms.Label lblSoDT;
        private System.Windows.Forms.Label lblThongBao;
    }
}