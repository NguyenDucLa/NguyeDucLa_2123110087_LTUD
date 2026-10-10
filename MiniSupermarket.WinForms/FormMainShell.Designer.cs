namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
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
            panelMainContent = new Panel();
            panelTopHeader = new Panel();
            lblTitle = new Label();
            lblUserInfo = new Label();
            panelSidebar = new Panel();
            panelUserFooter = new Panel();
            lblFooterUser = new Label();
            btnLogout = new Button();
            btnUserManage = new Button();
            btnReports = new Button();
            btnCustomer = new Button();
            btnProduct = new Button();
            btnCategory = new Button();
            btnPOS = new Button();
            lblLogo = new Label();

            panelMainContent.SuspendLayout();
            panelTopHeader.SuspendLayout();
            panelSidebar.SuspendLayout();
            panelUserFooter.SuspendLayout();
            SuspendLayout();

            // 
            // panelMainContent (Vùng chứa trung tâm nhúng form động)
            // 
            panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(230, 60);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1050, 660);
            panelMainContent.TabIndex = 2;

            // 
            // panelTopHeader (Thanh tiêu đề trên)
            // 
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(230, 0);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1050, 60);
            panelTopHeader.TabIndex = 1;

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(320, 25);
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";

            // lblUserInfo
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.Location = new Point(700, 18);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(330, 24);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Xin chào: ...";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;

            // 
            // panelSidebar (Thanh điều hướng bên trái)
            // 
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);
            panelSidebar.Controls.Add(btnUserManage);
            panelSidebar.Controls.Add(btnReports);
            panelSidebar.Controls.Add(btnCustomer);
            panelSidebar.Controls.Add(btnProduct);
            panelSidebar.Controls.Add(btnCategory);
            panelSidebar.Controls.Add(btnPOS);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(230, 720);
            panelSidebar.TabIndex = 0;

            // lblLogo
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(18, 20);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(190, 25);
            lblLogo.Text = "🛒 MiniMart POS";

            // btnPOS
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.ForeColor = Color.White;
            btnPOS.Location = new Point(0, 70);
            btnPOS.Name = "btnPOS";
            btnPOS.Padding = new Padding(18, 0, 0, 0);
            btnPOS.Size = new Size(230, 45);
            btnPOS.TabIndex = 0;
            btnPOS.Text = "🛒 Bán hàng (POS)";
            btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnPOS.UseVisualStyleBackColor = false;
            btnPOS.Click += btnPOS_Click;

            // btnCategory
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(0, 115);
            btnCategory.Name = "btnCategory";
            btnCategory.Padding = new Padding(18, 0, 0, 0);
            btnCategory.Size = new Size(230, 45);
            btnCategory.TabIndex = 1;
            btnCategory.Text = "📁 Quản lý Danh mục";
            btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            btnCategory.UseVisualStyleBackColor = false;
            btnCategory.Click += btnCategory_Click;

            // btnProduct
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(0, 160);
            btnProduct.Name = "btnProduct";
            btnProduct.Padding = new Padding(18, 0, 0, 0);
            btnProduct.Size = new Size(230, 45);
            btnProduct.TabIndex = 2;
            btnProduct.Text = "📦 Quản lý Sản phẩm";
            btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            btnProduct.UseVisualStyleBackColor = false;
            btnProduct.Click += btnProduct_Click;

            // btnCustomer
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(0, 205);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Padding = new Padding(18, 0, 0, 0);
            btnCustomer.Size = new Size(230, 45);
            btnCustomer.TabIndex = 3;
            btnCustomer.Text = "👥 Quản lý Khách hàng";
            btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomer_Click;

            // btnReports
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 250);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(18, 0, 0, 0);
            btnReports.Size = new Size(230, 45);
            btnReports.TabIndex = 4;
            btnReports.Text = "📊 Báo cáo Doanh thu";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;

            // btnUserManage
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.ForeColor = Color.White;
            btnUserManage.Location = new Point(0, 295);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Padding = new Padding(18, 0, 0, 0);
            btnUserManage.Size = new Size(230, 45);
            btnUserManage.TabIndex = 5;
            btnUserManage.Text = "🛡 Quản trị Tài khoản";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.UseVisualStyleBackColor = false;
            btnUserManage.Click += btnUserManage_Click;

            // 
            // panelUserFooter (Tên đăng nhập & Đăng xuất)
            // 
            panelUserFooter.BackColor = Color.FromArgb(18, 22, 36);
            panelUserFooter.Controls.Add(btnLogout);
            panelUserFooter.Controls.Add(lblFooterUser);
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Location = new Point(0, 650);
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Size = new Size(230, 70);

            // lblFooterUser
            lblFooterUser.AutoSize = true;
            lblFooterUser.ForeColor = Color.FromArgb(180, 190, 210);
            lblFooterUser.Location = new Point(14, 10);
            lblFooterUser.Name = "lblFooterUser";
            lblFooterUser.Size = new Size(80, 15);
            lblFooterUser.Text = "👤 ...";

            // btnLogout
            btnLogout.BackColor = Color.FromArgb(60, 70, 95);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(14, 34);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(202, 30);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪 Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;

            // 
            // FormMainShell
            // 
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1024, 640);
            Name = "FormMainShell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini";
            Load += FormMainShell_Load;

            panelMainContent.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            panelTopHeader.PerformLayout();
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            panelUserFooter.ResumeLayout(false);
            panelUserFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMainContent;
        private Panel panelTopHeader;
        private Label lblTitle;
        private Label lblUserInfo;

        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnReports;
        private Button btnUserManage;

        private Panel panelUserFooter;
        private Label lblFooterUser;
        private Button btnLogout;
    }
}
