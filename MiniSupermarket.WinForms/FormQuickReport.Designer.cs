using System.Windows.Forms.DataVisualization.Charting;

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
            ChartArea chartArea = new ChartArea("main");
            Title chartTitle = new Title();
            Series seriesRevenue = new Series("DoanhThu");

            pnlHeader = new Panel();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            btnRunReport = new Button();
            lblGroup = new Label();
            cboGroupBy = new ComboBox();
            btnExport = new Button();
            lblUpdated = new Label();

            pnlKpi = new Panel();
            pnlCard1 = new Panel();
            bar1 = new Panel();
            lblCap1 = new Label();
            lblVal1 = new Label();
            lblSub1 = new Label();
            pnlCard2 = new Panel();
            bar2 = new Panel();
            lblCap2 = new Label();
            lblVal2 = new Label();
            lblSub2 = new Label();
            pnlCard3 = new Panel();
            bar3 = new Panel();
            lblCap3 = new Label();
            lblVal3 = new Label();
            lblSub3 = new Label();
            pnlCard4 = new Panel();
            bar4 = new Panel();
            lblCap4 = new Label();
            lblVal4 = new Label();
            lblSub4 = new Label();

            pnlMiddle = new Panel();
            chartRevenue = new Chart();
            pnlTopList = new Panel();
            lblTopTitle = new Label();
            dgvTopProducts = new DataGridView();

            pnlBottom = new Panel();
            pnlCashierCard = new Panel();
            lblCashierTitle = new Label();
            dgvCashiers = new DataGridView();

            pnlHeader.SuspendLayout();
            pnlKpi.SuspendLayout();
            pnlCard1.SuspendLayout();
            pnlCard2.SuspendLayout();
            pnlCard3.SuspendLayout();
            pnlCard4.SuspendLayout();
            pnlMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartRevenue).BeginInit();
            pnlTopList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
            pnlBottom.SuspendLayout();
            pnlCashierCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCashiers).BeginInit();
            SuspendLayout();

            // 
            // pnlHeader (bộ lọc thời gian)
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblUpdated);
            pnlHeader.Controls.Add(btnExport);
            pnlHeader.Controls.Add(cboGroupBy);
            pnlHeader.Controls.Add(lblGroup);
            pnlHeader.Controls.Add(btnRunReport);
            pnlHeader.Controls.Add(dtpTo);
            pnlHeader.Controls.Add(lblTo);
            pnlHeader.Controls.Add(dtpFrom);
            pnlHeader.Controls.Add(lblFrom);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1200, 58);
            pnlHeader.TabIndex = 0;

            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI", 9F);
            lblFrom.ForeColor = Color.FromArgb(71, 85, 105);
            lblFrom.Location = new Point(18, 20);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(52, 15);
            lblFrom.Text = "Từ ngày:";

            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(74, 17);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(120, 23);
            dtpFrom.TabIndex = 1;

            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI", 9F);
            lblTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblTo.Location = new Point(206, 20);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(60, 15);
            lblTo.Text = "Đến ngày:";

            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(270, 17);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(120, 23);
            dtpTo.TabIndex = 2;

            btnRunReport.BackColor = Color.FromArgb(79, 70, 229);
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(404, 15);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(112, 28);
            btnRunReport.TabIndex = 3;
            btnRunReport.Text = "🔄  Xem";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;

            lblGroup.AutoSize = true;
            lblGroup.Font = new Font("Segoe UI", 9F);
            lblGroup.ForeColor = Color.FromArgb(71, 85, 105);
            lblGroup.Location = new Point(532, 20);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(65, 15);
            lblGroup.Text = "Nhóm theo:";

            cboGroupBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGroupBy.Items.AddRange(new object[] { "Theo ngày", "Theo tuần", "Theo tháng" });
            cboGroupBy.Location = new Point(606, 17);
            cboGroupBy.Name = "cboGroupBy";
            cboGroupBy.Size = new Size(130, 23);
            cboGroupBy.TabIndex = 4;
            cboGroupBy.SelectedIndexChanged += cboGroupBy_SelectedIndexChanged;

            btnExport.BackColor = Color.FromArgb(16, 185, 129);
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(752, 15);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(160, 28);
            btnExport.TabIndex = 5;
            btnExport.Text = "📤  Xuất Excel (CSV)";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;

            lblUpdated.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUpdated.Font = new Font("Segoe UI", 8.25F);
            lblUpdated.ForeColor = Color.FromArgb(148, 163, 184);
            lblUpdated.Location = new Point(930, 20);
            lblUpdated.Name = "lblUpdated";
            lblUpdated.Size = new Size(250, 18);
            lblUpdated.TabIndex = 6;
            lblUpdated.Text = "Chưa tải dữ liệu";
            lblUpdated.TextAlign = ContentAlignment.MiddleRight;

            // 
            // pnlKpi
            // 
            pnlKpi.BackColor = Color.FromArgb(241, 245, 249);
            pnlKpi.Controls.Add(pnlCard1);
            pnlKpi.Controls.Add(pnlCard2);
            pnlKpi.Controls.Add(pnlCard3);
            pnlKpi.Controls.Add(pnlCard4);
            pnlKpi.Dock = DockStyle.Top;
            pnlKpi.Location = new Point(0, 58);
            pnlKpi.Name = "pnlKpi";
            pnlKpi.Size = new Size(1200, 120);
            pnlKpi.TabIndex = 1;

            // pnlCard1 - Doanh thu hôm nay
            pnlCard1.BackColor = Color.White;
            pnlCard1.Controls.Add(lblSub1);
            pnlCard1.Controls.Add(lblVal1);
            pnlCard1.Controls.Add(lblCap1);
            pnlCard1.Controls.Add(bar1);
            pnlCard1.Location = new Point(18, 14);
            pnlCard1.Name = "pnlCard1";
            pnlCard1.Size = new Size(282, 92);
            pnlCard1.TabIndex = 0;

            bar1.BackColor = Color.FromArgb(16, 185, 129);
            bar1.Dock = DockStyle.Left;
            bar1.Location = new Point(0, 0);
            bar1.Name = "bar1";
            bar1.Size = new Size(6, 92);

            lblCap1.AutoSize = true;
            lblCap1.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblCap1.ForeColor = Color.FromArgb(100, 116, 139);
            lblCap1.Location = new Point(20, 14);
            lblCap1.Name = "lblCap1";
            lblCap1.Size = new Size(130, 13);
            lblCap1.Text = "DOANH THU HÔM NAY";

            lblVal1.AutoSize = true;
            lblVal1.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblVal1.ForeColor = Color.FromArgb(4, 120, 87);
            lblVal1.Location = new Point(18, 34);
            lblVal1.Name = "lblVal1";
            lblVal1.Size = new Size(45, 36);
            lblVal1.Text = "0 đ";

            lblSub1.AutoSize = true;
            lblSub1.Font = new Font("Segoe UI", 8.25F);
            lblSub1.ForeColor = Color.FromArgb(148, 163, 184);
            lblSub1.Location = new Point(20, 72);
            lblSub1.Name = "lblSub1";
            lblSub1.Size = new Size(120, 13);
            lblSub1.Text = "tổng thu trong ngày";

            // pnlCard2 - Tổng hóa đơn
            pnlCard2.BackColor = Color.White;
            pnlCard2.Controls.Add(lblSub2);
            pnlCard2.Controls.Add(lblVal2);
            pnlCard2.Controls.Add(lblCap2);
            pnlCard2.Controls.Add(bar2);
            pnlCard2.Location = new Point(312, 14);
            pnlCard2.Name = "pnlCard2";
            pnlCard2.Size = new Size(282, 92);
            pnlCard2.TabIndex = 1;

            bar2.BackColor = Color.FromArgb(79, 70, 229);
            bar2.Dock = DockStyle.Left;
            bar2.Location = new Point(0, 0);
            bar2.Name = "bar2";
            bar2.Size = new Size(6, 92);

            lblCap2.AutoSize = true;
            lblCap2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblCap2.ForeColor = Color.FromArgb(100, 116, 139);
            lblCap2.Location = new Point(20, 14);
            lblCap2.Name = "lblCap2";
            lblCap2.Size = new Size(110, 13);
            lblCap2.Text = "TỔNG HÓA ĐƠN";

            lblVal2.AutoSize = true;
            lblVal2.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblVal2.ForeColor = Color.FromArgb(67, 56, 202);
            lblVal2.Location = new Point(18, 34);
            lblVal2.Name = "lblVal2";
            lblVal2.Size = new Size(29, 36);
            lblVal2.Text = "0";

            lblSub2.AutoSize = true;
            lblSub2.Font = new Font("Segoe UI", 8.25F);
            lblSub2.ForeColor = Color.FromArgb(148, 163, 184);
            lblSub2.Location = new Point(20, 72);
            lblSub2.Name = "lblSub2";
            lblSub2.Size = new Size(110, 13);
            lblSub2.Text = "hóa đơn đã xuất hôm nay";

            // pnlCard3 - Khách hàng mới
            pnlCard3.BackColor = Color.White;
            pnlCard3.Controls.Add(lblSub3);
            pnlCard3.Controls.Add(lblVal3);
            pnlCard3.Controls.Add(lblCap3);
            pnlCard3.Controls.Add(bar3);
            pnlCard3.Location = new Point(606, 14);
            pnlCard3.Name = "pnlCard3";
            pnlCard3.Size = new Size(282, 92);
            pnlCard3.TabIndex = 2;

            bar3.BackColor = Color.FromArgb(245, 158, 11);
            bar3.Dock = DockStyle.Left;
            bar3.Location = new Point(0, 0);
            bar3.Name = "bar3";
            bar3.Size = new Size(6, 92);

            lblCap3.AutoSize = true;
            lblCap3.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblCap3.ForeColor = Color.FromArgb(100, 116, 139);
            lblCap3.Location = new Point(20, 14);
            lblCap3.Name = "lblCap3";
            lblCap3.Size = new Size(125, 13);
            lblCap3.Text = "KHÁCH HÀNG MỚI";

            lblVal3.AutoSize = true;
            lblVal3.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblVal3.ForeColor = Color.FromArgb(180, 83, 9);
            lblVal3.Location = new Point(18, 34);
            lblVal3.Name = "lblVal3";
            lblVal3.Size = new Size(29, 36);
            lblVal3.Text = "0";

            lblSub3.AutoSize = true;
            lblSub3.Font = new Font("Segoe UI", 8.25F);
            lblSub3.ForeColor = Color.FromArgb(148, 163, 184);
            lblSub3.Location = new Point(20, 72);
            lblSub3.Name = "lblSub3";
            lblSub3.Size = new Size(120, 13);
            lblSub3.Text = "đăng ký hôm nay";

            // pnlCard4 - Hàng sắp hết
            pnlCard4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCard4.BackColor = Color.White;
            pnlCard4.Controls.Add(lblSub4);
            pnlCard4.Controls.Add(lblVal4);
            pnlCard4.Controls.Add(lblCap4);
            pnlCard4.Controls.Add(bar4);
            pnlCard4.Location = new Point(900, 14);
            pnlCard4.Name = "pnlCard4";
            pnlCard4.Size = new Size(282, 92);
            pnlCard4.TabIndex = 3;

            bar4.BackColor = Color.FromArgb(239, 68, 68);
            bar4.Dock = DockStyle.Left;
            bar4.Location = new Point(0, 0);
            bar4.Name = "bar4";
            bar4.Size = new Size(6, 92);

            lblCap4.AutoSize = true;
            lblCap4.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblCap4.ForeColor = Color.FromArgb(100, 116, 139);
            lblCap4.Location = new Point(20, 14);
            lblCap4.Name = "lblCap4";
            lblCap4.Size = new Size(140, 13);
            lblCap4.Text = "MẶT HÀNG SẮP HẾT";

            lblVal4.AutoSize = true;
            lblVal4.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblVal4.ForeColor = Color.FromArgb(185, 28, 28);
            lblVal4.Location = new Point(18, 34);
            lblVal4.Name = "lblVal4";
            lblVal4.Size = new Size(29, 36);
            lblVal4.Text = "0";

            lblSub4.AutoEllipsis = true;
            lblSub4.AutoSize = false;
            lblSub4.Font = new Font("Segoe UI", 8.25F);
            lblSub4.ForeColor = Color.FromArgb(148, 163, 184);
            lblSub4.Location = new Point(20, 72);
            lblSub4.Name = "lblSub4";
            lblSub4.Size = new Size(246, 15);
            lblSub4.Text = "tồn kho ≤ 10, cần nhập thêm";

            // 
            // pnlMiddle (biểu đồ + top 5)
            // 
            pnlMiddle.BackColor = Color.FromArgb(241, 245, 249);
            pnlMiddle.Controls.Add(chartRevenue);
            pnlMiddle.Controls.Add(pnlTopList);
            pnlMiddle.Dock = DockStyle.Top;
            pnlMiddle.Location = new Point(0, 178);
            pnlMiddle.Name = "pnlMiddle";
            pnlMiddle.Padding = new Padding(18, 0, 18, 0);
            pnlMiddle.Size = new Size(1200, 340);
            pnlMiddle.TabIndex = 2;

            // chartRevenue - System.Windows.Forms.DataVisualization.Charting.Chart
            chartArea.BackColor = Color.White;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.LineColor = Color.FromArgb(203, 213, 225);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            chartArea.AxisY.LabelStyle.Format = "N0";
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            chartArea.Name = "main";

            chartTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chartTitle.ForeColor = Color.FromArgb(51, 65, 85);
            chartTitle.Name = "chartTitle";
            chartTitle.Text = "DOANH THU THEO THỜI GIAN";

            seriesRevenue.ChartType = SeriesChartType.Column;
            seriesRevenue.Color = Color.FromArgb(79, 70, 229);
            seriesRevenue.Name = "DoanhThu";
            seriesRevenue.XValueType = ChartValueType.String;

            chartRevenue.BackColor = Color.White;
            chartRevenue.ChartAreas.Add(chartArea);
            chartRevenue.Dock = DockStyle.Fill;
            chartRevenue.Location = new Point(18, 0);
            chartRevenue.Name = "chartRevenue";
            chartRevenue.Palette = ChartColorPalette.None;
            chartRevenue.Series.Add(seriesRevenue);
            chartRevenue.Size = new Size(784, 340);
            chartRevenue.TabIndex = 0;
            chartRevenue.Titles.Add(chartTitle);

            // pnlTopList
            pnlTopList.BackColor = Color.White;
            pnlTopList.Controls.Add(dgvTopProducts);
            pnlTopList.Controls.Add(lblTopTitle);
            pnlTopList.Dock = DockStyle.Right;
            pnlTopList.Location = new Point(802, 0);
            pnlTopList.Name = "pnlTopList";
            pnlTopList.Size = new Size(380, 340);
            pnlTopList.TabIndex = 1;

            lblTopTitle.AutoSize = true;
            lblTopTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTopTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblTopTitle.Location = new Point(14, 12);
            lblTopTitle.Name = "lblTopTitle";
            lblTopTitle.Size = new Size(260, 19);
            lblTopTitle.Text = "🏆  TOP 5 SẢN PHẨM BÁN CHẠY NHẤT";

            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.AllowUserToResizeRows = false;
            dgvTopProducts.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(250, 250, 252) };
            dgvTopProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.BackgroundColor = Color.White;
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTopProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTopProducts.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(71, 85, 105)
            };
            dgvTopProducts.ColumnHeadersHeight = 34;
            dgvTopProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTopProducts.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 65, 85),
                SelectionBackColor = Color.FromArgb(238, 242, 255),
                SelectionForeColor = Color.FromArgb(30, 27, 75)
            };
            dgvTopProducts.EnableHeadersVisualStyles = false;
            dgvTopProducts.GridColor = Color.FromArgb(241, 245, 249);
            dgvTopProducts.Location = new Point(12, 40);
            dgvTopProducts.MultiSelect = false;
            dgvTopProducts.Name = "dgvTopProducts";
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.RowTemplate.Height = 34;
            dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopProducts.Size = new Size(356, 288);
            dgvTopProducts.TabIndex = 1;

            // 
            // pnlBottom (doanh thu theo thu ngân)
            // 
            pnlBottom.BackColor = Color.FromArgb(241, 245, 249);
            pnlBottom.Controls.Add(pnlCashierCard);
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.Location = new Point(0, 518);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(18, 12, 18, 14);
            pnlBottom.Size = new Size(1200, 222);
            pnlBottom.TabIndex = 3;

            pnlCashierCard.BackColor = Color.White;
            pnlCashierCard.Controls.Add(dgvCashiers);
            pnlCashierCard.Controls.Add(lblCashierTitle);
            pnlCashierCard.Dock = DockStyle.Fill;
            pnlCashierCard.Location = new Point(18, 12);
            pnlCashierCard.Name = "pnlCashierCard";
            pnlCashierCard.Size = new Size(1164, 196);

            lblCashierTitle.AutoSize = true;
            lblCashierTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCashierTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblCashierTitle.Location = new Point(14, 10);
            lblCashierTitle.Name = "lblCashierTitle";
            lblCashierTitle.Size = new Size(300, 19);
            lblCashierTitle.Text = "👤  DOANH THU THEO THU NGÂN (NGÀY ĐẾN)";

            dgvCashiers.AllowUserToAddRows = false;
            dgvCashiers.AllowUserToResizeRows = false;
            dgvCashiers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(250, 250, 252) };
            dgvCashiers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCashiers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCashiers.BackgroundColor = Color.White;
            dgvCashiers.BorderStyle = BorderStyle.None;
            dgvCashiers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCashiers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCashiers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(71, 85, 105)
            };
            dgvCashiers.ColumnHeadersHeight = 34;
            dgvCashiers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCashiers.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 65, 85),
                SelectionBackColor = Color.FromArgb(238, 242, 255),
                SelectionForeColor = Color.FromArgb(30, 27, 75)
            };
            dgvCashiers.EnableHeadersVisualStyles = false;
            dgvCashiers.GridColor = Color.FromArgb(241, 245, 249);
            dgvCashiers.Location = new Point(14, 38);
            dgvCashiers.MultiSelect = false;
            dgvCashiers.Name = "dgvCashiers";
            dgvCashiers.ReadOnly = true;
            dgvCashiers.RowHeadersVisible = false;
            dgvCashiers.RowTemplate.Height = 30;
            dgvCashiers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCashiers.Size = new Size(1134, 146);
            dgvCashiers.TabIndex = 1;

            // 
            // FormQuickReport
            // 
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 740);
            Controls.Add(pnlBottom);
            Controls.Add(pnlMiddle);
            Controls.Add(pnlKpi);
            Controls.Add(pnlHeader);
            Name = "FormQuickReport";
            Text = "Báo cáo doanh thu và hiệu suất bán hàng";
            Load += FormQuickReport_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlKpi.ResumeLayout(false);
            pnlCard1.ResumeLayout(false);
            pnlCard1.PerformLayout();
            pnlCard2.ResumeLayout(false);
            pnlCard2.PerformLayout();
            pnlCard3.ResumeLayout(false);
            pnlCard3.PerformLayout();
            pnlCard4.ResumeLayout(false);
            pnlCard4.PerformLayout();
            pnlMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartRevenue).EndInit();
            pnlTopList.ResumeLayout(false);
            pnlTopList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
            pnlBottom.ResumeLayout(false);
            pnlCashierCard.ResumeLayout(false);
            pnlCashierCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCashiers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Button btnRunReport;
        private Label lblGroup;
        private ComboBox cboGroupBy;
        private Button btnExport;
        private Label lblUpdated;

        private Panel pnlKpi;
        private Panel pnlCard1;
        private Panel bar1;
        private Label lblCap1;
        private Label lblVal1;
        private Label lblSub1;
        private Panel pnlCard2;
        private Panel bar2;
        private Label lblCap2;
        private Label lblVal2;
        private Label lblSub2;
        private Panel pnlCard3;
        private Panel bar3;
        private Label lblCap3;
        private Label lblVal3;
        private Label lblSub3;
        private Panel pnlCard4;
        private Panel bar4;
        private Label lblCap4;
        private Label lblVal4;
        private Label lblSub4;

        private Panel pnlMiddle;
        private Chart chartRevenue;
        private Panel pnlTopList;
        private Label lblTopTitle;
        private DataGridView dgvTopProducts;

        private Panel pnlBottom;
        private Panel pnlCashierCard;
        private Label lblCashierTitle;
        private DataGridView dgvCashiers;
    }
}
