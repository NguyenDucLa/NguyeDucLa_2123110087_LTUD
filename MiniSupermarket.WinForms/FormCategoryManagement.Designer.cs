namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            dgvCategories = new DataGridView();

            grpInfo = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblName = new Label();
            txtCategoryName = new TextBox();
            lblDesc = new Label();
            txtDescription = new TextBox();

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
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
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
            grpSearch.Size = new Size(776, 60);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm";

            // txtKeyword
            txtKeyword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.Location = new Point(12, 22);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập từ khóa...";
            txtKeyword.Size = new Size(490, 27);
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
            // grpList (Khung Danh sách Nhóm hàng)
            // 
            grpList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpList.Controls.Add(dgvCategories);
            grpList.Controls.Add(pnlPager);
            grpList.Location = new Point(12, 80);
            grpList.Name = "grpList";
            grpList.Size = new Size(776, 548);
            grpList.TabIndex = 1;
            grpList.TabStop = false;
            grpList.Text = "Danh sách Nhóm hàng";

            // dgvCategories
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.Location = new Point(3, 23);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.RowHeadersWidth = 30;
            dgvCategories.Size = new Size(770, 522);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;

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
            pnlPager.Location = new Point(3, 502);
            pnlPager.Name = "pnlPager";
            pnlPager.Size = new Size(770, 43);

            // lblPageInfo
            lblPageInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPageInfo.AutoSize = true;
            lblPageInfo.Location = new Point(12, 15);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(160, 15);
            lblPageInfo.Text = "Trang 1/1  ·  0 nhóm hàng";

            // btnFirst
            btnFirst.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFirst.Font = new Font("Segoe UI", 11F);
            btnFirst.Location = new Point(200, 7);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(36, 28);
            btnFirst.TabIndex = 1;
            btnFirst.Text = "«";
            btnFirst.UseVisualStyleBackColor = true;
            btnFirst.Click += btnFirst_Click;

            // btnPrev
            btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrev.Font = new Font("Segoe UI", 11F);
            btnPrev.Location = new Point(240, 7);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(36, 28);
            btnPrev.TabIndex = 2;
            btnPrev.Text = "‹";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;

            // btnNext
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNext.Font = new Font("Segoe UI", 11F);
            btnNext.Location = new Point(280, 7);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(36, 28);
            btnNext.TabIndex = 3;
            btnNext.Text = "›";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;

            // btnLast
            btnLast.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLast.Font = new Font("Segoe UI", 11F);
            btnLast.Location = new Point(320, 7);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(36, 28);
            btnLast.TabIndex = 4;
            btnLast.Text = "»";
            btnLast.UseVisualStyleBackColor = true;
            btnLast.Click += btnLast_Click;

            // lblPageSize
            lblPageSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPageSize.AutoSize = true;
            lblPageSize.Location = new Point(375, 15);
            lblPageSize.Name = "lblPageSize";
            lblPageSize.Size = new Size(90, 15);
            lblPageSize.Text = "Số dòng / trang:";

            // cboPageSize
            cboPageSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPageSize.Items.AddRange(new object[] { 5, 10, 20, 50 });
            cboPageSize.Location = new Point(475, 11);
            cboPageSize.Name = "cboPageSize";
            cboPageSize.Size = new Size(70, 23);
            cboPageSize.TabIndex = 5;
            cboPageSize.SelectedIndexChanged += cboPageSize_SelectedIndexChanged;

            // 
            // grpInfo (Khung Thông tin Nhóm hàng)
            // 
            grpInfo.Controls.Add(lblId);
            grpInfo.Controls.Add(txtId);
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(txtCategoryName);
            grpInfo.Controls.Add(lblDesc);
            grpInfo.Controls.Add(txtDescription);
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpInfo.Location = new Point(798, 12);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(250, 616);
            grpInfo.TabIndex = 2;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin Nhóm hàng";

            // lblId & txtId
            lblId.AutoSize = true;
            lblId.Location = new Point(10, 30);
            lblId.Name = "lblId";
            lblId.Size = new Size(51, 20);
            lblId.Text = "Mã ID";

            txtId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtId.Location = new Point(10, 53);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(230, 27);
            txtId.TabIndex = 1;

            // lblName & txtCategoryName
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 95);
            lblName.Name = "lblName";
            lblName.Size = new Size(230, 20);
            lblName.Text = "Tên Nhóm hàng (Ví dụ: Bánh kẹo)";

            txtCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCategoryName.Location = new Point(10, 118);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(230, 27);
            txtCategoryName.TabIndex = 3;

            // lblDesc & txtDescription
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(10, 160);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(200, 20);
            lblDesc.Text = "Mô Tả (Mô tả chi tiết...)";

            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(10, 183);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(230, 340);
            txtDescription.TabIndex = 5;

            // 
            // Nút Thêm mới, Cập nhật, Xóa
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(10, 570);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(72, 32);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(88, 570);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(72, 32);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(166, 570);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(74, 32);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // FormCategoryManagement
            // 
            ClientSize = new Size(1060, 640);
            Controls.Add(grpInfo);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormCategoryManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Danh mục Nhóm hàng - FormCategoryManagement";
            Load += FormCategoryManagement_Load;

            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
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
        private DataGridView dgvCategories;

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
        private TextBox txtId;
        private Label lblName;
        private TextBox txtCategoryName;
        private Label lblDesc;
        private TextBox txtDescription;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}