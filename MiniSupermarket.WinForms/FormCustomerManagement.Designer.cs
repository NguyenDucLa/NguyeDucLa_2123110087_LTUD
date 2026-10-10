namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
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
            grpSearch = new GroupBox();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();

            grpList = new GroupBox();
            dgvCustomers = new DataGridView();

            grpInfo = new GroupBox();
            lblId = new Label();
            txtCustomerId = new TextBox();
            lblName = new Label();
            txtCustomerName = new TextBox();
            lblPhone = new Label();
            txtPhoneNumber = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPoints = new Label();
            txtRewardPoints = new TextBox();
            lblRank = new Label();
            txtMembershipRank = new TextBox();

            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpInfo.SuspendLayout();
            SuspendLayout();

            // 
            // grpSearch (Khung Tìm kiếm)
            // 
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Location = new Point(12, 12);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(600, 60);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm (theo Tên hoặc Số điện thoại)";

            // txtKeyword
            txtKeyword.Location = new Point(12, 22);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên hoặc SĐT...";
            txtKeyword.Size = new Size(350, 27);
            txtKeyword.TabIndex = 0;

            // btnSearch
            btnSearch.Location = new Point(370, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(105, 30);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // btnLoad
            btnLoad.Location = new Point(483, 20);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(105, 30);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // grpList (Khung Danh sách Khách hàng)
            // 
            grpList.Controls.Add(dgvCustomers);
            grpList.Location = new Point(12, 78);
            grpList.Name = "grpList";
            grpList.Size = new Size(600, 434);
            grpList.TabIndex = 1;
            grpList.TabStop = false;
            grpList.Text = "Danh sách Khách hàng";

            // dgvCustomers
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(3, 23);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowHeadersWidth = 30;
            dgvCustomers.Size = new Size(594, 408);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // 
            // grpInfo (Khung Thông tin Khách hàng)
            // 
            grpInfo.Controls.Add(lblId);
            grpInfo.Controls.Add(txtCustomerId);
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(txtCustomerName);
            grpInfo.Controls.Add(lblPhone);
            grpInfo.Controls.Add(txtPhoneNumber);
            grpInfo.Controls.Add(lblAddress);
            grpInfo.Controls.Add(txtAddress);
            grpInfo.Controls.Add(lblPoints);
            grpInfo.Controls.Add(txtRewardPoints);
            grpInfo.Controls.Add(lblRank);
            grpInfo.Controls.Add(txtMembershipRank);
            grpInfo.Location = new Point(620, 12);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(340, 500);
            grpInfo.TabIndex = 2;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin Khách hàng";

            // lblId & txtCustomerId
            lblId.AutoSize = true;
            lblId.Location = new Point(10, 28);
            lblId.Name = "lblId";
            lblId.Size = new Size(120, 20);
            lblId.Text = "Mã khách hàng";

            txtCustomerId.Location = new Point(10, 50);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(320, 27);
            txtCustomerId.TabIndex = 1;

            // lblName & txtCustomerName
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 85);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 20);
            lblName.Text = "Tên khách hàng";

            txtCustomerName.Location = new Point(10, 107);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(320, 27);
            txtCustomerName.TabIndex = 3;

            // lblPhone & txtPhoneNumber
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(10, 142);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(120, 20);
            lblPhone.Text = "Số điện thoại";

            txtPhoneNumber.Location = new Point(10, 164);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(320, 27);
            txtPhoneNumber.TabIndex = 5;

            // lblAddress & txtAddress
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(10, 192);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(130, 20);
            lblAddress.Text = "Địa chỉ giao hàng";

            txtAddress.Location = new Point(10, 212);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(320, 90);
            txtAddress.TabIndex = 7;

            // lblPoints & txtRewardPoints
            lblPoints.AutoSize = true;
            lblPoints.Location = new Point(10, 320);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(110, 20);
            lblPoints.Text = "Điểm tích lũy";
            txtRewardPoints.Location = new Point(10, 340);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(140, 27);
            txtRewardPoints.TabIndex = 8;

            // lblRank & txtMembershipRank
            lblRank.AutoSize = true;
            lblRank.Location = new Point(160, 320);
            lblRank.Name = "lblRank";
            lblRank.Size = new Size(110, 20);
            lblRank.Text = "Hạng thẻ";
            txtMembershipRank.Location = new Point(160, 340);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(170, 27);
            txtMembershipRank.TabIndex = 9;

            // 
            // Nút Thêm mới, Cập nhật, Xóa
            // 
            btnAdd.Location = new Point(10, 400);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(102, 34);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Location = new Point(118, 400);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(102, 34);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Location = new Point(226, 400);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(104, 34);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // FormCustomerManagement
            // 
            ClientSize = new Size(972, 524);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(grpInfo);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng - Tiệm Rau củ quả sạch Mộc Garden";
            Load += FormCustomerManagement_Load;

            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        private GroupBox grpList;
        private DataGridView dgvCustomers;

        private GroupBox grpInfo;
        private Label lblId;
        private TextBox txtCustomerId;
        private Label lblName;
        private TextBox txtCustomerName;
        private Label lblPhone;
        private TextBox txtPhoneNumber;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblPoints;
        private TextBox txtRewardPoints;
        private Label lblRank;
        private TextBox txtMembershipRank;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}
