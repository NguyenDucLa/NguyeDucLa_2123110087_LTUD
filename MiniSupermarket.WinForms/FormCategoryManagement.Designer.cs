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

            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
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
            grpSearch.Size = new Size(500, 60);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm";

            // txtKeyword
            txtKeyword.Location = new Point(12, 22);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập từ khóa...";
            txtKeyword.Size = new Size(290, 27);
            txtKeyword.TabIndex = 0;

            // btnSearch
            btnSearch.Location = new Point(310, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 30);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // btnLoad
            btnLoad.Location = new Point(402, 20);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(85, 30);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // grpList (Khung Danh sách Nhóm hàng)
            // 
            grpList.Controls.Add(dgvCategories);
            grpList.Location = new Point(12, 78);
            grpList.Name = "grpList";
            grpList.Size = new Size(500, 320);
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
            dgvCategories.Size = new Size(494, 294);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;

            // 
            // grpInfo (Khung Thông tin Nhóm hàng)
            // 
            grpInfo.Controls.Add(lblId);
            grpInfo.Controls.Add(txtId);
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(txtCategoryName);
            grpInfo.Controls.Add(lblDesc);
            grpInfo.Controls.Add(txtDescription);
            grpInfo.Location = new Point(520, 12);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(250, 330);
            grpInfo.TabIndex = 2;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin Nhóm hàng";

            // lblId & txtId
            lblId.AutoSize = true;
            lblId.Location = new Point(10, 30);
            lblId.Name = "lblId";
            lblId.Size = new Size(51, 20);
            lblId.Text = "Mã ID";

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

            txtDescription.Location = new Point(10, 183);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(230, 130);
            txtDescription.TabIndex = 5;

            // 
            // Nút Thêm mới, Cập nhật, Xóa
            // 
            btnAdd.Location = new Point(520, 355);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(78, 32);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Location = new Point(604, 355);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(78, 32);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Location = new Point(688, 355);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(82, 32);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // FormCategoryManagement
            // 
            ClientSize = new Size(782, 408);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
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
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        private GroupBox grpList;
        private DataGridView dgvCategories;

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