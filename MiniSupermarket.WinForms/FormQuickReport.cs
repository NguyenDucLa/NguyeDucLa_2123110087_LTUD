using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async Task LoadReportAsync()
        {
            try
            {
                string date = dtpDate.Value.ToString("yyyy-MM-dd");
                var report = await ApiClientService.Client.GetFromJsonAsync<QuickReportDto>($"reports/quick?date={date}");

                if (report != null)
                {
                    lblTotalOrders.Text = report.TotalOrders.ToString();
                    lblTotalRevenue.Text = $"{report.TotalRevenue:N0} đ";
                    lblBestSeller.Text = $"{report.BestSellerName}\n({report.BestSellerQuantity} sản phẩm)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
