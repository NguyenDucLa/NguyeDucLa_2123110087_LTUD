namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
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
            dgvUsers = new DataGridView();
            grpInfo = new Panel();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            btnAddUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpInfo.SuspendLayout();
            SuspendLayout();

            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.Location = new Point(10, 10);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 30;
            dgvUsers.Size = new Size(700, 505);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;

            // 
            // grpInfo (Thông tin tài khoản)
            // 
            grpInfo.BackColor = Color.White;
            grpInfo.Controls.Add(btnToggleLock);
            grpInfo.Controls.Add(btnResetPassword);
            grpInfo.Controls.Add(btnAddUser);
            grpInfo.Controls.Add(cboRole);
            grpInfo.Controls.Add(lblRole);
            grpInfo.Controls.Add(txtPassword);
            grpInfo.Controls.Add(lblPassword);
            grpInfo.Controls.Add(txtFullName);
            grpInfo.Controls.Add(lblFullName);
            grpInfo.Controls.Add(txtUsername);
            grpInfo.Controls.Add(lblUsername);
            grpInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpInfo.Location = new Point(720, 10);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(310, 505);
            grpInfo.TabIndex = 1;

            // lblUsername & txtUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(15, 20);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(90, 15);
            lblUsername.Text = "Tên đăng nhập";
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Location = new Point(15, 40);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 23);
            txtUsername.TabIndex = 0;

            // lblFullName & txtFullName
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(15, 80);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(65, 15);
            lblFullName.Text = "Họ và tên";
            txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFullName.Location = new Point(15, 100);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(280, 23);
            txtFullName.TabIndex = 1;

            // lblPassword & txtPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(15, 140);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.Text = "Mật khẩu";
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(15, 160);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(280, 23);
            txtPassword.TabIndex = 2;

            // lblRole & cboRole
            lblRole.AutoSize = true;
            lblRole.Location = new Point(15, 200);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(45, 15);
            lblRole.Text = "Vai trò";
            cboRole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Location = new Point(15, 220);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(280, 23);
            cboRole.TabIndex = 3;

            // btnAddUser
            btnAddUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAddUser.Location = new Point(15, 270);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(280, 40);
            btnAddUser.TabIndex = 4;
            btnAddUser.Text = "➕ Tạo tài khoản";
            btnAddUser.UseVisualStyleBackColor = true;
            btnAddUser.Click += btnAddUser_Click;

            // btnResetPassword
            btnResetPassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnResetPassword.Location = new Point(15, 320);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(280, 40);
            btnResetPassword.TabIndex = 5;
            btnResetPassword.Text = "🔑 Đặt lại mật khẩu";
            btnResetPassword.UseVisualStyleBackColor = true;
            btnResetPassword.Click += btnResetPassword_Click;

            // btnToggleLock
            btnToggleLock.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnToggleLock.Location = new Point(15, 370);
            btnToggleLock.Name = "btnToggleLock";
            btnToggleLock.Size = new Size(280, 40);
            btnToggleLock.TabIndex = 6;
            btnToggleLock.Text = "🔒 Khóa / Mở khóa tài khoản";
            btnToggleLock.UseVisualStyleBackColor = true;
            btnToggleLock.Click += btnToggleLock_Click;

            // 
            // FormUserManagement
            // 
            ClientSize = new Size(1045, 527);
            Controls.Add(grpInfo);
            Controls.Add(dgvUsers);
            Name = "FormUserManagement";
            Text = "Quản trị Tài khoản & Phân quyền";
            Load += FormUserManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsers;
        private Panel grpInfo;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cboRole;
        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnToggleLock;
    }
}
