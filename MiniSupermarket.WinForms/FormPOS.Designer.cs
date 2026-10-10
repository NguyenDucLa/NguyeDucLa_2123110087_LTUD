namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
            pnlLeft = new Panel();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblHint = new Label();
            dgvCart = new DataGridView();
            pnlRight = new Panel();
            lblCustomerPhone = new Label();
            txtCustomerPhone = new TextBox();
            lblCustomerName = new Label();
            lblTotalCaption = new Label();
            lblTotalAmount = new Label();
            lblCashCaption = new Label();
            txtCashReceived = new TextBox();
            lblChangeCaption = new Label();
            lblChange = new Label();
            btnCheckout = new Button();
            btnClearCart = new Button();

            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnlRight.SuspendLayout();
            SuspendLayout();

            // 
            // pnlLeft (Giỏ hàng thanh toán)
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(dgvCart);
            pnlLeft.Controls.Add(lblHint);
            pnlLeft.Controls.Add(txtBarcode);
            pnlLeft.Controls.Add(lblBarcode);
            pnlLeft.Location = new Point(10, 10);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(780, 650);
            pnlLeft.TabIndex = 0;

            // lblBarcode
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBarcode.Location = new Point(10, 12);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(70, 19);
            lblBarcode.Text = "Mã vạch:";

            // txtBarcode
            txtBarcode.Font = new Font("Segoe UI", 11F);
            txtBarcode.Location = new Point(90, 10);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Quét mã vạch sản phẩm rồi nhấn Enter...";
            txtBarcode.Size = new Size(420, 27);
            txtBarcode.TabIndex = 0;
            txtBarcode.KeyDown += txtBarcode_KeyDown;

            // lblHint
            lblHint.AutoSize = true;
            lblHint.ForeColor = Color.Gray;
            lblHint.Location = new Point(520, 16);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(200, 15);
            lblHint.Text = "Nhấn Enter để thêm vào giỏ hàng";

            // dgvCart
            dgvCart.AllowUserToAddRows = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Location = new Point(10, 55);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 30;
            dgvCart.Size = new Size(760, 585);
            dgvCart.TabIndex = 1;

            // 
            // pnlRight (Thông tin khách hàng & thanh toán)
            // 
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(btnClearCart);
            pnlRight.Controls.Add(btnCheckout);
            pnlRight.Controls.Add(lblChange);
            pnlRight.Controls.Add(lblChangeCaption);
            pnlRight.Controls.Add(txtCashReceived);
            pnlRight.Controls.Add(lblCashCaption);
            pnlRight.Controls.Add(lblTotalAmount);
            pnlRight.Controls.Add(lblTotalCaption);
            pnlRight.Controls.Add(lblCustomerName);
            pnlRight.Controls.Add(txtCustomerPhone);
            pnlRight.Controls.Add(lblCustomerPhone);
            pnlRight.Location = new Point(800, 10);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(400, 650);
            pnlRight.TabIndex = 1;

            // lblCustomerPhone
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomerPhone.Location = new Point(15, 15);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.Size = new Size(220, 19);
            lblCustomerPhone.Text = "SĐT Khách hàng thân thiết:";

            // txtCustomerPhone
            txtCustomerPhone.Font = new Font("Segoe UI", 10F);
            txtCustomerPhone.Location = new Point(15, 38);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT để tích điểm (bỏ trống nếu vãng lai)";
            txtCustomerPhone.Size = new Size(370, 25);
            txtCustomerPhone.TabIndex = 0;

            // lblCustomerName
            lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblCustomerName.ForeColor = Color.FromArgb(41, 100, 180);
            lblCustomerName.Location = new Point(15, 72);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(370, 25);
            lblCustomerName.Text = "Khách vãng lai";

            // lblTotalCaption
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalCaption.Location = new Point(15, 130);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Size = new Size(120, 20);
            lblTotalCaption.Text = "TỔNG TIỀN:";

            // lblTotalAmount
            lblTotalAmount.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(200, 30, 30);
            lblTotalAmount.Location = new Point(15, 155);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(370, 55);
            lblTotalAmount.Text = "0 đ";

            // lblCashCaption
            lblCashCaption.AutoSize = true;
            lblCashCaption.Location = new Point(15, 225);
            lblCashCaption.Name = "lblCashCaption";
            lblCashCaption.Size = new Size(130, 15);
            lblCashCaption.Text = "Tiền khách đưa:";

            // txtCashReceived
            txtCashReceived.Font = new Font("Segoe UI", 11F);
            txtCashReceived.Location = new Point(15, 248);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.Size = new Size(370, 27);
            txtCashReceived.TabIndex = 1;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;

            // lblChangeCaption
            lblChangeCaption.AutoSize = true;
            lblChangeCaption.Location = new Point(15, 290);
            lblChangeCaption.Name = "lblChangeCaption";
            lblChangeCaption.Size = new Size(130, 15);
            lblChangeCaption.Text = "Tiền thừa trả khách:";

            // lblChange
            lblChange.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblChange.Location = new Point(15, 312);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(370, 35);
            lblChange.Text = "0 đ";

            // btnCheckout
            btnCheckout.BackColor = Color.FromArgb(34, 139, 34);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(15, 400);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(370, 70);
            btnCheckout.TabIndex = 2;
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;

            // btnClearCart
            btnClearCart.BackColor = Color.FromArgb(200, 40, 40);
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(15, 485);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(370, 55);
            btnClearCart.TabIndex = 3;
            btnClearCart.Text = "HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;

            // 
            // FormPOS
            // 
            ClientSize = new Size(1220, 672);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            KeyPreview = true;
            Name = "FormPOS";
            Text = "Quầy bán hàng POS";
            KeyDown += FormPOS_KeyDown;

            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLeft;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblHint;
        private DataGridView dgvCart;

        private Panel pnlRight;
        private Label lblCustomerPhone;
        private TextBox txtCustomerPhone;
        private Label lblCustomerName;
        private Label lblTotalCaption;
        private Label lblTotalAmount;
        private Label lblCashCaption;
        private TextBox txtCashReceived;
        private Label lblChangeCaption;
        private Label lblChange;
        private Button btnCheckout;
        private Button btnClearCart;
    }
}
