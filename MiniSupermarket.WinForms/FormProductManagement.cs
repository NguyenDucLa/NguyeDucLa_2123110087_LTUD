using System.Net.Http.Json;
using System.Text;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        // ---- Trạng thái phân trang ----
        private int _page = 1;                     // Trang hiện tại
        private int _pageSize = 10;                // Số dòng mỗi trang
        private int _totalPages = 1;               // Tổng số trang (API trả về)
        private int _totalItems = 0;               // Tổng số sản phẩm
        private bool _ready = false;               // Chặn sự kiện khi form còn đang khởi tạo
        private List<ProductDto>? _filteredCache;  // null = xem toàn bộ (phân trang phía server)

        public FormProductManagement()
        {
            InitializeComponent();
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductId", DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 70 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Barcode", DataPropertyName = "Barcode", HeaderText = "Mã vạch", Width = 120 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductName", DataPropertyName = "ProductName", HeaderText = "Tên sản phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", DataPropertyName = "Price", HeaderText = "Đơn giá", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockQuantity", DataPropertyName = "StockQuantity", HeaderText = "Tồn kho", Width = 80 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "CategoryName", DataPropertyName = "CategoryName", HeaderText = "Nhóm hàng", Width = 160 });

            // Cảnh báo hàng sắp hết: tồn kho <= 10 thì tô nền đỏ nhạt
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;
        }

        // Tiêu chí #43 - Low-Stock Alert
        private Font? _lowStockFont;

        private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count) return;
            if (e.CellStyle == null) return;

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is ProductDto product && product.StockQuantity <= 10)
            {
                _lowStockFont ??= new Font(dgvProducts.Font, FontStyle.Bold);

                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.SelectionBackColor = Color.IndianRed;
                e.CellStyle.Font = _lowStockFont;
            }
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            cboPageSize.SelectedIndex = 1; // 10 dòng/trang (sự kiện bị bỏ qua vì _ready = false)
            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
            _ready = true;
        }

        private async Task LoadCategoriesToComboAsync()
        {
            try
            {
                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories") ?? new List<CategoryDto>();

                // ComboBox chọn danh mục (cho form chi tiết)
                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                // ComboBox lọc (thêm mục "Tất cả")
                var filterList = new List<CategoryDto> { new CategoryDto { CategoryId = 0, CategoryName = "— Tất cả nhóm hàng —" } };
                filterList.AddRange(categories);
                cboFilterCategory.DataSource = filterList;
                cboFilterCategory.DisplayMember = "CategoryName";
                cboFilterCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nạp danh sách sản phẩm theo trang (phân trang phía máy chủ bằng .Skip() / .Take())
        private async Task LoadProductsAsync()
        {
            try
            {
                var paged = await ApiClientService.Client.GetFromJsonAsync<PagedResult<ProductDto>>(
                    $"products/paged?page={_page}&pageSize={_pageSize}");

                if (paged == null) return;

                // Nếu trang hiện tại vượt quá tổng số trang (do vừa xóa bớt dữ liệu) → lùi về trang cuối
                if (paged.TotalPages > 0 && _page > paged.TotalPages)
                {
                    _page = paged.TotalPages;
                    await LoadProductsAsync();
                    return;
                }

                _filteredCache = null;
                _totalItems = paged.TotalItems;
                _totalPages = Math.Max(1, paged.TotalPages);
                dgvProducts.DataSource = paged.Data;
                UpdatePager();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phân trang phía client cho kết quả tìm kiếm / lọc theo nhóm hàng
        private void ApplyClientPage()
        {
            var list = _filteredCache ?? new List<ProductDto>();

            _totalItems = list.Count;
            _totalPages = Math.Max(1, (int)Math.Ceiling(_totalItems / (double)_pageSize));
            if (_page > _totalPages) _page = _totalPages;
            if (_page < 1) _page = 1;

            dgvProducts.DataSource = list.Skip((_page - 1) * _pageSize).Take(_pageSize).ToList();
            UpdatePager();
        }

        // Cập nhật nhãn "Trang x/y - n sản phẩm" và bật/tắt các nút điều hướng
        private void UpdatePager()
        {
            lblPageInfo.Text = $"Trang {_page}/{_totalPages}   ·   {_totalItems} sản phẩm";
            btnFirst.Enabled = btnPrev.Enabled = _page > 1;
            btnNext.Enabled = btnLast.Enabled = _page < _totalPages;
        }

        private async Task ReloadCurrentAsync()
        {
            if (_filteredCache != null) ApplyClientPage();
            else await LoadProductsAsync();
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            _page = 1;
            await ReloadCurrentAsync();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_page > 1) _page--;
            await ReloadCurrentAsync();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_page < _totalPages) _page++;
            await ReloadCurrentAsync();
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            _page = _totalPages;
            await ReloadCurrentAsync();
        }

        private async void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_ready || cboPageSize.SelectedItem is not int size) return;

            _pageSize = size;
            _page = 1;
            await ReloadCurrentAsync();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchBarcode.Text.Trim();
            int categoryId = cboFilterCategory.SelectedValue is int id ? id : 0;

            try
            {
                List<ProductDto>? result;
                if (!string.IsNullOrEmpty(keyword))
                {
                    result = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>($"products/search?keyword={Uri.EscapeDataString(keyword)}");
                }
                else if (categoryId > 0)
                {
                    result = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>($"products/by-category/{categoryId}");
                }
                else
                {
                    // Không có điều kiện lọc → quay về danh sách phân trang phía máy chủ
                    _filteredCache = null;
                    _page = 1;
                    await LoadProductsAsync();
                    return;
                }

                // Có điều kiện lọc → giữ kết quả lại và phân trang trên lưới
                _filteredCache = result ?? new List<ProductDto>();
                _page = 1;
                ApplyClientPage();
            }
            catch (Exception)
            {
                MessageBox.Show("Không tìm thấy sản phẩm phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearchBarcode.Clear();
            if (cboFilterCategory.Items.Count > 0) cboFilterCategory.SelectedIndex = 0;

            _filteredCache = null;
            _page = 1;
            await LoadProductsAsync();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                txtId.Text = row.Cells["ProductId"].Value?.ToString();
                txtBarcode.Text = row.Cells["Barcode"].Value?.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();
                nudPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value ?? 0m);
                nudStock.Value = Convert.ToDecimal(row.Cells["StockQuantity"].Value ?? 0);

                // Lấy CategoryId từ đối tượng được gắn (grid không hiển thị cột CategoryId)
                if (row.DataBoundItem is ProductDto product)
                {
                    cboCategory.SelectedValue = product.CategoryId;
                }
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã vạch và Tên sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newProd = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = cboCategory.SelectedValue is int cid ? cid : 1
            };

            var res = await ApiClientService.Client.PostAsJsonAsync("products", newProd);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadCurrentAsync();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại! Lỗi: " + res.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text)) return;
            int id = int.Parse(txtId.Text);

            var updateProd = new
            {
                ProductId = id,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = cboCategory.SelectedValue is int cid ? cid : 1
            };

            var res = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", updateProd);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadCurrentAsync();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại! Lỗi: " + res.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text)) return;
            int id = int.Parse(txtId.Text);

            if (MessageBox.Show($"Xác nhận xóa sản phẩm ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var res = await ApiClientService.Client.DeleteAsync($"products/{id}");
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã xóa sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ReloadCurrentAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Lỗi: " + res.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
        }

        // ==================================================================
        // Tiêu chí #48 - Xuất danh sách sản phẩm + tồn kho ra tệp CSV
        // (mở được trực tiếp bằng Excel, có BOM UTF-8 để không lỗi tiếng Việt)
        // ==================================================================
        private async void btnExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                btnExportCsv.Enabled = false;

                var all = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>("products")
                          ?? new List<ProductDto>();

                if (all.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var dialog = new SaveFileDialog
                {
                    Filter = "Tệp CSV (mở bằng Excel)|*.csv",
                    FileName = $"DanhSachSanPham_{DateTime.Now:yyyyMMdd_HHmm}.csv"
                };

                if (dialog.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();
                sb.AppendLine("Mã SP,Mã vạch,Tên sản phẩm,Đơn giá (đ),Tồn kho,Nhóm hàng,Cảnh báo");

                foreach (var p in all)
                {
                    sb.AppendLine(string.Join(",",
                        p.ProductId.ToString(),
                        CsvCell(p.Barcode),
                        CsvCell(p.ProductName),
                        p.Price.ToString("0"),
                        p.StockQuantity.ToString(),
                        CsvCell(p.CategoryName),
                        CsvCell(p.StockQuantity <= 10 ? "Sắp hết hàng" : "")));
                }

                // UTF8Encoding(true) = có BOM -> Excel nhận đúng bảng mã tiếng Việt
                File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));

                MessageBox.Show($"Đã xuất {all.Count} sản phẩm ra tệp:\n{dialog.FileName}",
                    "Xuất thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xuất tệp thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExportCsv.Enabled = true;
            }
        }

        // Bọc ô CSV trong dấu nháy kép nếu chứa dấu phẩy / nháy kép
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
