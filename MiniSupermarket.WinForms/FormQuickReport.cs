using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private List<DailyRevenueDto> _daily = new List<DailyRevenueDto>();
        private List<TopProductDto> _topProducts = new List<TopProductDto>();
        private DashboardSummaryDto _summary = new DashboardSummaryDto();

        public FormQuickReport()
        {
            InitializeComponent();
            SetupGrids();
            InitDateRange();
        }

        // ------------------------------------------------------------------
        // Khởi tạo
        // ------------------------------------------------------------------
        private void SetupGrids()
        {
            dgvTopProducts.AutoGenerateColumns = false;
            dgvTopProducts.Columns.Clear();
            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TopRank",
                HeaderText = "#",
                FillWeight = 8
            });
            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TopName",
                DataPropertyName = "ProductName",
                HeaderText = "Sản phẩm",
                FillWeight = 50
            });
            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TopQty",
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TopRevenue",
                DataPropertyName = "Revenue",
                HeaderText = "Doanh thu",
                FillWeight = 30,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvCashiers.AutoGenerateColumns = false;
            dgvCashiers.Columns.Clear();
            dgvCashiers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cashier",
                DataPropertyName = "CashierUsername",
                HeaderText = "Tên đăng nhập thu ngân",
                FillWeight = 40
            });
            dgvCashiers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CashierOrders",
                DataPropertyName = "TotalOrders",
                HeaderText = "Số hóa đơn",
                FillWeight = 25,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvCashiers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CashierRevenue",
                DataPropertyName = "TotalRevenue",
                HeaderText = "Doanh thu (đ)",
                FillWeight = 35,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void InitDateRange()
        {
            dtpTo.Value = DateTime.Today;
            dtpFrom.Value = DateTime.Today.AddDays(-13);
            cboGroupBy.SelectedIndex = 0;   // Theo ngày
        }

        // ------------------------------------------------------------------
        // Sự kiện
        // ------------------------------------------------------------------
        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private void cboGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawChart();   // đổi cách nhóm thì chỉ cần vẽ lại biểu đồ
        }

        // ------------------------------------------------------------------
        // Nạp dữ liệu: 4 API tương ứng 4 khối trên màn hình
        // ------------------------------------------------------------------
        private async Task LoadReportAsync()
        {
            try
            {
                btnRunReport.Enabled = false;

                string from = dtpFrom.Value.ToString("yyyy-MM-dd");
                string to = dtpTo.Value.ToString("yyyy-MM-dd");

                // (#44) 4 chỉ số KPI
                _summary = await ApiClientService.Client
                    .GetFromJsonAsync<DashboardSummaryDto>("reports/dashboard-summary")
                    ?? new DashboardSummaryDto();

                lblVal1.Text = $"{_summary.RevenueToday:N0} đ";
                lblVal2.Text = _summary.OrdersToday.ToString("N0");
                lblVal3.Text = _summary.NewCustomers.ToString("N0");
                lblVal4.Text = _summary.LowStock.ToString("N0");
                lblSub1.Text = $"tổng thu ngày {DateTime.Today:dd/MM/yyyy}";

                // (#45) Doanh thu theo ngày — lấy từ Stored Procedure sp_GetRevenueByDay
                var byDay = await ApiClientService.Client
                    .GetFromJsonAsync<RevenueByDayResult>($"reports/revenue-by-day?from={from}&to={to}");
                _daily = byDay?.Data ?? new List<DailyRevenueDto>();
                DrawChart();

                // (#46) Top 5 sản phẩm bán chạy
                var top = await ApiClientService.Client
                    .GetFromJsonAsync<TopProductsResult>($"reports/top-products?date={to}&top=5");
                _topProducts = top?.Data ?? new List<TopProductDto>();

                dgvTopProducts.Rows.Clear();
                dgvTopProducts.DataSource = null;
                foreach (var item in _topProducts)
                {
                    int index = dgvTopProducts.Rows.Add(
                        (dgvTopProducts.Rows.Count + 1).ToString(),
                        item.ProductName,
                        item.Quantity.ToString("N0"),
                        item.Revenue.ToString("N0"));
                    if (dgvTopProducts.Rows.Count == 1)
                        dgvTopProducts.Rows[index].DefaultCellStyle.Font =
                            new Font(dgvTopProducts.Font, FontStyle.Bold);
                }

                // Bảng doanh thu theo thu ngân của ngày "Đến ngày"
                var cashiers = await ApiClientService.Client
                    .GetFromJsonAsync<DashboardDto>($"reports/dashboard?date={to}&days=1");
                dgvCashiers.DataSource = cashiers?.Cashiers ?? new List<CashierRowDto>();

                lblUpdated.Text = "Cập nhật lúc " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunReport.Enabled = true;
            }
        }

        // ------------------------------------------------------------------
        // (#45) Vẽ biểu đồ cột bằng System.Windows.Forms.DataVisualization
        // ------------------------------------------------------------------
        private void DrawChart()
        {
            var series = chartRevenue.Series["DoanhThu"];
            series.Points.Clear();

            if (_daily.Count == 0)
            {
                chartRevenue.Titles[0].Text = "DOANH THU THEO THỜI GIAN  (chưa có dữ liệu)";
                return;
            }

            string mode = cboGroupBy.SelectedItem?.ToString() ?? "Theo ngày";
            chartRevenue.Titles[0].Text = $"DOANH THU {mode.ToUpper()}  ({dtpFrom.Value:dd/MM/yyyy} - {dtpTo.Value:dd/MM/yyyy})";

            List<(string Label, decimal Revenue)> points;

            if (mode == "Theo tuần")
            {
                points = _daily
                    .GroupBy(d => WeekKey(d.Date))
                    .OrderBy(g => g.Key.SortKey)
                    .Select(g => ($"Tuần {g.Key.Week}", g.Sum(x => x.Revenue)))
                    .ToList();
            }
            else if (mode == "Theo tháng")
            {
                points = _daily
                    .GroupBy(d => d.Date.Substring(0, 7))       // yyyy-MM
                    .OrderBy(g => g.Key)
                    .Select(g => ($"Tháng {g.Key.Substring(5, 2)}/{g.Key.Substring(0, 4)}", g.Sum(x => x.Revenue)))
                    .ToList();
            }
            else
            {
                points = _daily
                    .Select(d => (FormatDay(d.Date), d.Revenue))
                    .ToList();
            }

            foreach (var p in points)
            {
                series.Points.AddXY(p.Label, p.Revenue);
            }

            // Ghi giá trị lên đầu cột khi số cột ít
            series.IsValueShownAsLabel = points.Count <= 12;
            series.LabelFormat = "N0";
        }

        // ------------------------------------------------------------------
        // (#48) Xuất báo cáo doanh thu ra CSV (mở bằng Excel)
        // ------------------------------------------------------------------
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_daily.Count == 0 && _topProducts.Count == 0 && dgvCashiers.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu để xuất. Hãy bấm \"Xem\" trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var dialog = new SaveFileDialog
                {
                    Filter = "Tệp CSV (mở bằng Excel)|*.csv",
                    FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmm}.csv"
                };

                if (dialog.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();

                // ----- Phần 1: chỉ số tổng quan -----
                sb.AppendLine("BÁO CÁO DOANH THU - TIỆM RAU CỦ QUẢ SẠCH MỘC GARDEN");
                sb.AppendLine($"Xuất lúc:,{DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine($"Khoảng thời gian:,{dtpFrom.Value:dd/MM/yyyy} - {dtpTo.Value:dd/MM/yyyy}");
                sb.AppendLine();

                sb.AppendLine("CHỈ SỐ TỔNG QUAN");
                sb.AppendLine("Chỉ số,Giá trị");
                sb.AppendLine($"Doanh thu hôm nay (đ),{_summary.RevenueToday:0}");
                sb.AppendLine($"Tổng hóa đơn hôm nay,{_summary.OrdersToday}");
                sb.AppendLine($"Khách hàng mới hôm nay,{_summary.NewCustomers}");
                sb.AppendLine($"Mặt hàng sắp hết (tồn <= 10),{_summary.LowStock}");
                sb.AppendLine();

                // ----- Phần 2: doanh thu theo ngày -----
                sb.AppendLine("DOANH THU THEO NGÀY");
                sb.AppendLine("Ngày,Số hóa đơn,Doanh thu (đ)");
                foreach (var d in _daily)
                {
                    sb.AppendLine($"{FormatDay(d.Date)},{d.Orders},{d.Revenue:0}");
                }
                sb.AppendLine($"TỔNG CỘNG,,{_daily.Sum(x => x.Revenue):0}");
                sb.AppendLine();

                // ----- Phần 3: top sản phẩm -----
                sb.AppendLine("TOP 5 SẢN PHẨM BÁN CHẠY NHẤT");
                sb.AppendLine("Hạng,Sản phẩm,Số lượng,Doanh thu (đ)");
                for (int i = 0; i < _topProducts.Count; i++)
                {
                    var p = _topProducts[i];
                    sb.AppendLine($"{i + 1},{CsvCell(p.ProductName)},{p.Quantity},{p.Revenue:0}");
                }
                sb.AppendLine();

                // ----- Phần 4: doanh thu theo thu ngân -----
                sb.AppendLine("DOANH THU THEO THU NGÂN");
                sb.AppendLine("Thu ngân,Số hóa đơn,Doanh thu (đ)");
                foreach (DataGridViewRow row in dgvCashiers.Rows)
                {
                    if (row.IsNewRow) continue;
                    sb.AppendLine($"{CsvCell(row.Cells["Cashier"].Value?.ToString())}," +
                                  $"{row.Cells["CashierOrders"].Value}," +
                                  $"{row.Cells["CashierRevenue"].Value}");
                }

                // UTF8Encoding(true) = có BOM -> Excel nhận đúng tiếng Việt
                File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));

                MessageBox.Show($"Đã xuất báo cáo ra tệp:\n{dialog.FileName}", "Xuất thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xuất tệp thất bại: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------------
        // Tiện ích
        // ------------------------------------------------------------------
        private static string FormatDay(string isoDate)
        {
            return DateTime.TryParse(isoDate, out var d) ? d.ToString("dd/MM") : isoDate;
        }

        private static (string SortKey, int Week) WeekKey(string isoDate)
        {
            if (!DateTime.TryParse(isoDate, out var d)) return ("0000-00", 0);

            int week = ISOWeek.GetWeekOfYear(d);
            return ($"{d.Year}-{week:D2}", week);
        }

        private static string CsvCell(string? value)
        {
            value ??= string.Empty;
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }
    }
}
