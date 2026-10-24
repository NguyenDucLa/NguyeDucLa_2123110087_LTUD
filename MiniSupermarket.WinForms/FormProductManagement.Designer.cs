namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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
            pnlTop = new Panel();
            lblSearch = new Label();
            txtSearchBarcode = new TextBox();
            lblFilter = new Label();
            cboFilterCategory = new ComboBox();
            btnSearch = new Button();
            dgvProducts = new DataGridView();
            grpInfo = new Panel();
            lblId = new Label();
            txtId = new TextBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblName = new Label();
            txtProductName = new TextBox();
            lblPrice = new Label();
            nudPrice = new NumericUpDown();
            lblStock = new Label();
            nudStock = new NumericUpDown();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            SuspendLayout();

            // 
            // pnlTop (Thanh tìm kiếm & lọc)
            // 
            pnlTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(cboFilterCategory);
            pnlTop.Controls.Add(lblFilter);
            pnlTop.Controls.Add(txtSearchBarcode);
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Location = new Point(10, 10);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1180, 55);
            pnlTop.TabIndex = 0;

            // lblSearch
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(115, 15);
            lblSearch.Text = "Tìm theo mã vạch/tên:";

            // txtSearchBarcode
            txtSearchBarcode.Location = new Point(135, 14);
            txtSearchBarcode.Name = "txtSearchBarcode";
            txtSearchBarcode.PlaceholderText = "Nhập mã vạch hoặc tên sản phẩm...";
            txtSearchBarcode.Size = new Size(300, 23);
            txtSearchBarcode.TabIndex = 0;

            // lblFilter
            lblFilter.AutoSize = true;
            lblFilter.Location = new Point(455, 18);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(60, 15);
            lblFilter.Text = "Nhóm hàng:";

            // cboFilterCategory
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.Location = new Point(545, 14);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(300, 23);
            cboFilterCategory.TabIndex = 1;

            // btnSearch
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(985, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(185, 30);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm / Lọc";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(10, 75);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 30;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.Size = new Size(880, 613);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellClick += dgvProducts_CellClick;

            // 
            // grpInfo (Chi tiết sản phẩm)
            // 
            grpInfo.BackColor = Color.White;
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Controls.Add(btnLoad);
            grpInfo.Controls.Add(cboCategory);
            grpInfo.Controls.Add(lblCategory);
            grpInfo.Controls.Add(nudStock);
            grpInfo.Controls.Add(lblStock);
            grpInfo.Controls.Add(nudPrice);
            grpInfo.Controls.Add(lblPrice);
            grpInfo.Controls.Add(txtProductName);
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(txtBarcode);
            grpInfo.Controls.Add(lblBarcode);
            grpInfo.Controls.Add(txtId);
            grpInfo.Controls.Add(lblId);
            grpInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpInfo.Location = new Point(900, 75);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(290, 613);
            grpInfo.TabIndex = 2;

            // lblId & txtId
            lblId.AutoSize = true;
            lblId.Location = new Point(12, 15);
            lblId.Name = "lblId";
            lblId.Size = new Size(70, 15);
            lblId.Text = "Mã sản phẩm";
            txtId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtId.Location = new Point(12, 33);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(265, 23);
            txtId.TabIndex = 0;

            // lblBarcode & txtBarcode
            lblBarcode.AutoSize = true;
            lblBarcode.Location = new Point(12, 68);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(55, 15);
            lblBarcode.Text = "Mã vạch";
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Location = new Point(12, 86);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(265, 23);
            txtBarcode.TabIndex = 1;

            // lblName & txtProductName
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 121);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 15);
            lblName.Text = "Tên sản phẩm";
            txtProductName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.Location = new Point(12, 139);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(265, 23);
            txtProductName.TabIndex = 2;

            // lblPrice & nudPrice
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(12, 174);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(50, 15);
            lblPrice.Text = "Đơn giá";
            nudPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nudPrice.Location = new Point(12, 192);
            nudPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(265, 23);
            nudPrice.TabIndex = 3;
            nudPrice.ThousandsSeparator = true;

            // lblStock & nudStock
            lblStock.AutoSize = true;
            lblStock.Location = new Point(12, 227);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(55, 15);
            lblStock.Text = "Tồn kho";
            nudStock.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nudStock.Location = new Point(12, 245);
            nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(265, 23);
            nudStock.TabIndex = 4;

            // lblCategory & cboCategory
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(12, 280);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(65, 15);
            lblCategory.Text = "Nhóm hàng";
            cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Location = new Point(12, 298);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(265, 23);
            cboCategory.TabIndex = 5;

            // btnLoad
            btnLoad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLoad.Location = new Point(12, 505);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(128, 32);
            btnLoad.TabIndex = 6;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // btnAdd
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(149, 505);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(128, 32);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            // btnUpdate
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(12, 545);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(128, 32);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(149, 545);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(128, 32);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // FormProductManagement
            // 
            ClientSize = new Size(1200, 700);
            Controls.Add(grpInfo);
            Controls.Add(dgvProducts);
            Controls.Add(pnlTop);
            Name = "FormProductManagement";
            Text = "Quản lý Sản phẩm & Kho hàng";
            Load += FormProductManagement_Load;

            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblSearch;
        private TextBox txtSearchBarcode;
        private Label lblFilter;
        private ComboBox cboFilterCategory;
        private Button btnSearch;

        private DataGridView dgvProducts;

        private Panel grpInfo;
        private Label lblId;
        private TextBox txtId;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblName;
        private TextBox txtProductName;
        private Label lblPrice;
        private NumericUpDown nudPrice;
        private Label lblStock;
        private NumericUpDown nudStock;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Button btnLoad;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}
