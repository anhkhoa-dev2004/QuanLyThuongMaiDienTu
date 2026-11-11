namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    partial class Form_TrangChuChinh
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.txtThongTinUser = new System.Windows.Forms.TextBox();
            this.lblThongTinUser = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.treeMenuChucNang = new System.Windows.Forms.TreeView();
            this.lblSoLuongMenu = new System.Windows.Forms.Label();
            this.pnlSidebarHeader = new System.Windows.Forms.Panel();
            this.lblSidebarCaption = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblContentDesc = new System.Windows.Forms.Label();
            this.lblContentTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlSidebarHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            this.pnlHeader.Controls.Add(this.btnQuayLai);
            this.pnlHeader.Controls.Add(this.txtThongTinUser);
            this.pnlHeader.Controls.Add(this.lblThongTinUser);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1320, 64);
            this.pnlHeader.TabIndex = 0;
            //
            // btnQuayLai
            //
            this.btnQuayLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuayLai.BackColor = System.Drawing.Color.White;
            this.btnQuayLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuayLai.FlatAppearance.BorderSize = 0;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            this.btnQuayLai.Location = new System.Drawing.Point(1066, 14);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(110, 36);
            this.btnQuayLai.TabIndex = 4;
            this.btnQuayLai.Text = "Đăng xuất";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            //
            // txtThongTinUser
            //
            this.txtThongTinUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThongTinUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            this.txtThongTinUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtThongTinUser.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThongTinUser.ForeColor = System.Drawing.Color.White;
            this.txtThongTinUser.Location = new System.Drawing.Point(770, 24);
            this.txtThongTinUser.Name = "txtThongTinUser";
            this.txtThongTinUser.ReadOnly = true;
            this.txtThongTinUser.Size = new System.Drawing.Size(280, 22);
            this.txtThongTinUser.TabIndex = 3;
            this.txtThongTinUser.TabStop = false;
            this.txtThongTinUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThongTinUser.TextChanged += new System.EventHandler(this.txtThongTinUser_TextChanged);
            //
            // lblThongTinUser
            //
            this.lblThongTinUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThongTinUser.AutoSize = true;
            this.lblThongTinUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.lblThongTinUser.Location = new System.Drawing.Point(690, 26);
            this.lblThongTinUser.Name = "lblThongTinUser";
            this.lblThongTinUser.Size = new System.Drawing.Size(65, 20);
            this.lblThongTinUser.TabIndex = 2;
            this.lblThongTinUser.Text = "Xin chào,";
            this.lblThongTinUser.Click += new System.EventHandler(this.lblThongTinUser_Click);
            //
            // lblTieuDe
            //
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(24, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(520, 31);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ BÁN HÀNG - KHO SHOPEE";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            //
            // pnlSidebar
            //
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(39)))), ((int)(((byte)(56)))));
            this.pnlSidebar.Controls.Add(this.treeMenuChucNang);
            this.pnlSidebar.Controls.Add(this.lblSoLuongMenu);
            this.pnlSidebar.Controls.Add(this.pnlSidebarHeader);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 64);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(420, 656);
            this.pnlSidebar.TabIndex = 1;
            //
            // treeMenuChucNang
            //
            this.treeMenuChucNang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(39)))), ((int)(((byte)(56)))));
            this.treeMenuChucNang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeMenuChucNang.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeMenuChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenuChucNang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeMenuChucNang.ForeColor = System.Drawing.Color.Gainsboro;
            this.treeMenuChucNang.FullRowSelect = true;
            this.treeMenuChucNang.HideSelection = false;
            this.treeMenuChucNang.HotTracking = true;
            this.treeMenuChucNang.Indent = 20;
            this.treeMenuChucNang.ItemHeight = 30;
            this.treeMenuChucNang.Location = new System.Drawing.Point(0, 40);
            this.treeMenuChucNang.Name = "treeMenuChucNang";
            this.treeMenuChucNang.ShowLines = false;
            this.treeMenuChucNang.Size = new System.Drawing.Size(420, 582);
            this.treeMenuChucNang.TabIndex = 2;
            this.treeMenuChucNang.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeMenuChucNang_DrawNode);
            this.treeMenuChucNang.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMenuChucNang_AfterSelect);
            this.treeMenuChucNang.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeMenuChucNang_NodeMouseDoubleClick);
            //
            // lblSoLuongMenu
            //
            this.lblSoLuongMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(46)))));
            this.lblSoLuongMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSoLuongMenu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuongMenu.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSoLuongMenu.Location = new System.Drawing.Point(0, 622);
            this.lblSoLuongMenu.Name = "lblSoLuongMenu";
            this.lblSoLuongMenu.Size = new System.Drawing.Size(420, 34);
            this.lblSoLuongMenu.TabIndex = 1;
            this.lblSoLuongMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSoLuongMenu.Text = "Hiển thị 0/0 chức năng theo phân quyền";
            //
            // pnlSidebarHeader
            //
            this.pnlSidebarHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(46)))));
            this.pnlSidebarHeader.Controls.Add(this.lblSidebarCaption);
            this.pnlSidebarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSidebarHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebarHeader.Name = "pnlSidebarHeader";
            this.pnlSidebarHeader.Size = new System.Drawing.Size(420, 40);
            this.pnlSidebarHeader.TabIndex = 0;
            //
            // lblSidebarCaption
            //
            this.lblSidebarCaption.AutoSize = true;
            this.lblSidebarCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSidebarCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(190)))), ((int)(((byte)(90)))));
            this.lblSidebarCaption.Location = new System.Drawing.Point(16, 11);
            this.lblSidebarCaption.Name = "lblSidebarCaption";
            this.lblSidebarCaption.Size = new System.Drawing.Size(160, 19);
            this.lblSidebarCaption.TabIndex = 0;
            this.lblSidebarCaption.Text = "CHỨC NĂNG HỆ THỐNG";
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.pnlContent.Controls.Add(this.lblContentDesc);
            this.pnlContent.Controls.Add(this.lblContentTitle);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(420, 64);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(900, 656);
            this.pnlContent.TabIndex = 2;
            //
            // lblContentDesc
            //
            this.lblContentDesc.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblContentDesc.Location = new System.Drawing.Point(40, 92);
            this.lblContentDesc.MaximumSize = new System.Drawing.Size(720, 0);
            this.lblContentDesc.Name = "lblContentDesc";
            this.lblContentDesc.Size = new System.Drawing.Size(600, 40);
            this.lblContentDesc.TabIndex = 1;
            this.lblContentDesc.Text = "Chọn một chức năng ở menu bên trái để bắt đầu.";
            this.lblContentDesc.UseMnemonic = false;
            //
            // lblContentTitle
            //
            this.lblContentTitle.AutoSize = true;
            this.lblContentTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblContentTitle.Location = new System.Drawing.Point(40, 40);
            this.lblContentTitle.Name = "lblContentTitle";
            this.lblContentTitle.Size = new System.Drawing.Size(320, 37);
            this.lblContentTitle.TabIndex = 0;
            this.lblContentTitle.Text = "Chào mừng trở lại!";
            this.lblContentTitle.UseMnemonic = false;
            //
            // Form_TrangChuChinh
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1320, 720);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1136, 600);
            this.Name = "Form_TrangChuChinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Shopee";
            this.Load += new System.EventHandler(this.Form_TrangChuChinh_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebarHeader.ResumeLayout(false);
            this.pnlSidebarHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblThongTinUser;
        private System.Windows.Forms.TextBox txtThongTinUser;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlSidebarHeader;
        private System.Windows.Forms.Label lblSidebarCaption;
        private System.Windows.Forms.TreeView treeMenuChucNang;
        private System.Windows.Forms.Label lblSoLuongMenu;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblContentTitle;
        private System.Windows.Forms.Label lblContentDesc;
    }
}
