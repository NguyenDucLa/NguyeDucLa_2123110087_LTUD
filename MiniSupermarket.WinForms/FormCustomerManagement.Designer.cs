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

            pnlPager = new Panel();
            lblPageInfo = new Label();
            btnFirst = new Button();
            btnPrev = new Button();
            btnNext = new Button();
            btnLast = new Button();
            lblPageSize = new Label();
            cboPageSize = new ComboBox();

            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpInfo.SuspendLayout();
            pnlPager.SuspendLayout();
            SuspendLayout();

            // 
            // grpSearch (Khung Tìm kiếm)
            // 
            grpSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Location = new Point(12, 12);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(786, 60);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm (theo Tên hoặc Số điện thoại)";

            // txtKeyword
            txtKeyword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.Location = new Point(12, 22);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên hoặc SĐT...";
            txtKeyword.Size = new Size(500, 27);
            txtKeyword.TabIndex = 0;

            // btnSearch
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(536, 21);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(115, 30);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // btnLoad
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoad.Location = new Point(659, 21);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(115, 30);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // grpList (Khung Danh sách Khách hàng)
            // 
            grpList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpList.Controls.Add(dgvCustomers);
            grpList.Controls.Add(pnlPager);
            grpList.Location = new Point(12, 80);
            grpList.Name = "grpList";
            grpList.Size = new Size(786, 588);
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
            dgvCustomers.Size = new Size(780, 562);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // 
            // pnlPager (Thanh phân trang - .Skip()/.Take() phía máy chủ)
            // 
            pnlPager.BackColor = Color.White;
            pnlPager.Controls.Add(cboPageSize);
            pnlPager.Controls.Add(lblPageSize);
            pnlPager.Controls.Add(btnLast);
            pnlPager.Controls.Add(btnNext);
            pnlPager.Controls.Add(btnPrev);
            pnlPager.Controls.Add(btnFirst);
            pnlPager.Controls.Add(lblPageInfo);
            pnlPager.Dock = DockStyle.Bottom;
            pnlPager.Location = new Point(3, 542);
            pnlPager.Name = "pnlPager";
            pnlPager.Size = new Size(780, 43);

            // lblPageInfo
            lblPageInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPageInfo.AutoSize = true;
            lblPageInfo.Location = new Point(12, 15);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(160, 15);
            lblPageInfo.Text = "Trang 1/1  ·  0 khách hàng";

            // btnFirst
            btnFirst.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFirst.Font = new Font("Segoe UI", 11F);
            btnFirst.Location = new Point(210, 7);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(36, 28);
            btnFirst.TabIndex = 1;
            btnFirst.Text = "«";
            btnFirst.UseVisualStyleBackColor = true;
            btnFirst.Click += btnFirst_Click;

            // btnPrev
            btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrev.Font = new Font("Segoe UI", 11F);
            btnPrev.Location = new Point(250, 7);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(36, 28);
            btnPrev.TabIndex = 2;
            btnPrev.Text = "‹";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;

            // btnNext
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNext.Font = new Font("Segoe UI", 11F);
            btnNext.Location = new Point(290, 7);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(36, 28);
            btnNext.TabIndex = 3;
            btnNext.Text = "›";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;

            // btnLast
            btnLast.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLast.Font = new Font("Segoe UI", 11F);
            btnLast.Location = new Point(330, 7);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(36, 28);
            btnLast.TabIndex = 4;
            btnLast.Text = "»";
            btnLast.UseVisualStyleBackColor = true;
            btnLast.Click += btnLast_Click;

            // lblPageSize
            lblPageSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPageSize.AutoSize = true;
            lblPageSize.Location = new Point(385, 15);
            lblPageSize.Name = "lblPageSize";
            lblPageSize.Size = new Size(90, 15);
            lblPageSize.Text = "Số dòng / trang:";

            // cboPageSize
            cboPageSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPageSize.Items.AddRange(new object[] { 5, 10, 20, 50 });
            cboPageSize.Location = new Point(485, 11);
            cboPageSize.Name = "cboPageSize";
            cboPageSize.Size = new Size(70, 23);
            cboPageSize.TabIndex = 5;
            cboPageSize.SelectedIndexChanged += cboPageSize_SelectedIndexChanged;

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
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpInfo.Location = new Point(808, 12);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(330, 656);
            grpInfo.TabIndex = 2;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin Khách hàng";

            // lblId & txtCustomerId
            lblId.AutoSize = true;
            lblId.Location = new Point(10, 28);
            lblId.Name = "lblId";
            lblId.Size = new Size(120, 20);
            lblId.Text = "Mã khách hàng";

            txtCustomerId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerId.Location = new Point(10, 50);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(310, 27);
            txtCustomerId.TabIndex = 1;

            // lblName & txtCustomerName
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 85);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 20);
            lblName.Text = "Tên khách hàng";

            txtCustomerName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerName.Location = new Point(10, 107);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(310, 27);
            txtCustomerName.TabIndex = 3;

            // lblPhone & txtPhoneNumber
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(10, 142);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(120, 20);
            lblPhone.Text = "Số điện thoại";

            txtPhoneNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPhoneNumber.Location = new Point(10, 164);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(310, 27);
            txtPhoneNumber.TabIndex = 5;

            // lblAddress & txtAddress
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(10, 192);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(130, 20);
            lblAddress.Text = "Địa chỉ giao hàng";

            txtAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAddress.Location = new Point(10, 212);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(310, 150);
            txtAddress.TabIndex = 7;

            // lblPoints & txtRewardPoints
            lblPoints.AutoSize = true;
            lblPoints.Location = new Point(10, 380);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(110, 20);
            lblPoints.Text = "Điểm tích lũy";
            txtRewardPoints.Location = new Point(10, 400);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(140, 27);
            txtRewardPoints.TabIndex = 8;

            // lblRank & txtMembershipRank
            lblRank.AutoSize = true;
            lblRank.Location = new Point(160, 380);
            lblRank.Name = "lblRank";
            lblRank.Size = new Size(110, 20);
            lblRank.Text = "Hạng thẻ";
            txtMembershipRank.Location = new Point(160, 400);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(160, 27);
            txtMembershipRank.TabIndex = 9;

            // 
            // Nút Thêm mới, Cập nhật, Xóa
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(10, 612);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 34);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(114, 612);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 34);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(218, 612);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 34);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // FormCustomerManagement
            // 
            ClientSize = new Size(1150, 680);
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
            pnlPager.ResumeLayout(false);
            pnlPager.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        private GroupBox grpList;
        private DataGridView dgvCustomers;

        private Panel pnlPager;
        private Label lblPageInfo;
        private Button btnFirst;
        private Button btnPrev;
        private Button btnNext;
        private Button btnLast;
        private Label lblPageSize;
        private ComboBox cboPageSize;

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
