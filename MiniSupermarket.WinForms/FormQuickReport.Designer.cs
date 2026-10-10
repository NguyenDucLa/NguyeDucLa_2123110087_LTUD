namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
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
            lblPickDate = new Label();
            dtpDate = new DateTimePicker();
            btnRunReport = new Button();
            pnlOrders = new Panel();
            lblCaptionOrders = new Label();
            lblTotalOrders = new Label();
            pnlRevenue = new Panel();
            lblCaptionRevenue = new Label();
            lblTotalRevenue = new Label();
            pnlBest = new Panel();
            lblCaptionBest = new Label();
            lblBestSeller = new Label();

            pnlOrders.SuspendLayout();
            pnlRevenue.SuspendLayout();
            pnlBest.SuspendLayout();
            SuspendLayout();

            // lblPickDate
            lblPickDate.AutoSize = true;
            lblPickDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPickDate.Location = new Point(15, 20);
            lblPickDate.Name = "lblPickDate";
            lblPickDate.Size = new Size(150, 19);
            lblPickDate.Text = "Chọn ngày xem báo cáo:";

            // dtpDate
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(185, 18);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 23);
            dtpDate.TabIndex = 0;

            // btnRunReport
            btnRunReport.BackColor = Color.FromArgb(41, 100, 180);
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(400, 16);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(180, 30);
            btnRunReport.TabIndex = 1;
            btnRunReport.Text = "📊 Xem báo cáo";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;

            // 
            // pnlOrders (Tổng số hóa đơn)
            // 
            pnlOrders.BackColor = Color.FromArgb(220, 235, 252);
            pnlOrders.Controls.Add(lblTotalOrders);
            pnlOrders.Controls.Add(lblCaptionOrders);
            pnlOrders.Location = new Point(15, 70);
            pnlOrders.Name = "pnlOrders";
            pnlOrders.Size = new Size(280, 170);
            pnlOrders.TabIndex = 2;

            lblCaptionOrders.AutoSize = true;
            lblCaptionOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCaptionOrders.ForeColor = Color.FromArgb(60, 80, 120);
            lblCaptionOrders.Location = new Point(15, 15);
            lblCaptionOrders.Name = "lblCaptionOrders";
            lblCaptionOrders.Size = new Size(160, 19);
            lblCaptionOrders.Text = "TỔNG SỐ HÓA ĐƠN";

            lblTotalOrders.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.FromArgb(30, 80, 160);
            lblTotalOrders.Location = new Point(15, 70);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(250, 60);
            lblTotalOrders.Text = "0";

            // 
            // pnlRevenue (Tổng doanh thu)
            // 
            pnlRevenue.BackColor = Color.FromArgb(222, 245, 226);
            pnlRevenue.Controls.Add(lblTotalRevenue);
            pnlRevenue.Controls.Add(lblCaptionRevenue);
            pnlRevenue.Location = new Point(315, 70);
            pnlRevenue.Name = "pnlRevenue";
            pnlRevenue.Size = new Size(280, 170);
            pnlRevenue.TabIndex = 3;

            lblCaptionRevenue.AutoSize = true;
            lblCaptionRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCaptionRevenue.ForeColor = Color.FromArgb(60, 110, 70);
            lblCaptionRevenue.Location = new Point(15, 15);
            lblCaptionRevenue.Name = "lblCaptionRevenue";
            lblCaptionRevenue.Size = new Size(160, 19);
            lblCaptionRevenue.Text = "TỔNG DOANH THU";

            lblTotalRevenue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.FromArgb(30, 130, 60);
            lblTotalRevenue.Location = new Point(15, 70);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(250, 60);
            lblTotalRevenue.Text = "0 đ";

            // 
            // pnlBest (Mặt hàng bán chạy nhất)
            // 
            pnlBest.BackColor = Color.FromArgb(252, 240, 218);
            pnlBest.Controls.Add(lblBestSeller);
            pnlBest.Controls.Add(lblCaptionBest);
            pnlBest.Location = new Point(615, 70);
            pnlBest.Name = "pnlBest";
            pnlBest.Size = new Size(280, 170);
            pnlBest.TabIndex = 4;

            lblCaptionBest.AutoSize = true;
            lblCaptionBest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCaptionBest.ForeColor = Color.FromArgb(140, 100, 40);
            lblCaptionBest.Location = new Point(15, 15);
            lblCaptionBest.Name = "lblCaptionBest";
            lblCaptionBest.Size = new Size(210, 19);
            lblCaptionBest.Text = "MẶT HÀNG BÁN CHẠY NHẤT";

            lblBestSeller.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.FromArgb(170, 110, 20);
            lblBestSeller.Location = new Point(15, 60);
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Size = new Size(250, 90);
            lblBestSeller.Text = "Chưa có dữ liệu";

            // 
            // FormQuickReport
            // 
            ClientSize = new Size(910, 260);
            Controls.Add(pnlBest);
            Controls.Add(pnlRevenue);
            Controls.Add(pnlOrders);
            Controls.Add(btnRunReport);
            Controls.Add(dtpDate);
            Controls.Add(lblPickDate);
            Name = "FormQuickReport";
            Text = "Báo cáo doanh thu nhanh";
            Load += FormQuickReport_Load;

            pnlOrders.ResumeLayout(false);
            pnlOrders.PerformLayout();
            pnlRevenue.ResumeLayout(false);
            pnlRevenue.PerformLayout();
            pnlBest.ResumeLayout(false);
            pnlBest.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPickDate;
        private DateTimePicker dtpDate;
        private Button btnRunReport;
        private Panel pnlOrders;
        private Label lblCaptionOrders;
        private Label lblTotalOrders;
        private Panel pnlRevenue;
        private Label lblCaptionRevenue;
        private Label lblTotalRevenue;
        private Panel pnlBest;
        private Label lblCaptionBest;
        private Label lblBestSeller;
    }
}
