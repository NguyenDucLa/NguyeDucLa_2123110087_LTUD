using System.Net.Http.Headers; // Đính kèm Authorization Header (JWT)
using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // Địa chỉ Base API (khớp với cổng HTTPS của MiniSupermarket.API)
        private const string ApiBaseUrl = "https://localhost:7075/api/";

        // ---- Trạng thái phân trang ----
        private int _page = 1;
        private int _pageSize = 10;
        private int _totalPages = 1;
        private int _totalItems = 0;
        private bool _ready = false;
        private List<CustomerDto>? _filteredCache;

        public FormCustomerManagement()
        {
            InitializeComponent();
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerId", DataPropertyName = "CustomerId", HeaderText = "Mã KH", Width = 60 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", DataPropertyName = "CustomerName", HeaderText = "Tên khách hàng", Width = 150 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "PhoneNumber", DataPropertyName = "PhoneNumber", HeaderText = "Số điện thoại", Width = 110 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Address", DataPropertyName = "Address", HeaderText = "Địa chỉ giao hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "RewardPoints", DataPropertyName = "RewardPoints", HeaderText = "Điểm", Width = 60 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "MembershipRank", DataPropertyName = "MembershipRank", HeaderText = "Hạng thẻ", Width = 80 });
        }

        // Hàm hỗ trợ tạo HttpClient và đính kèm JWT Bearer Token tự động
        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            return client;
        }

        // Sự kiện Form vừa bật lên: tự động tải dữ liệu khách hàng
        private async void FormCustomerManagement_Load(object sender, EventArgs e)
        {
            cboPageSize.SelectedIndex = 1; // 10 dòng/trang
            await LoadDataAsync();
            _ready = true;
        }

        // Hàm dùng chung: GET danh sách khách hàng có phân trang
        private async Task LoadDataAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();
                var paged = await client.GetFromJsonAsync<PagedResult<CustomerDto>>(
                    $"customers/paged?page={_page}&pageSize={_pageSize}");

                if (paged == null) return;

                if (paged.TotalPages > 0 && _page > paged.TotalPages)
                {
                    _page = paged.TotalPages;
                    await LoadDataAsync();
                    return;
                }

                _filteredCache = null;
                _totalItems = paged.TotalItems;
                _totalPages = Math.Max(1, paged.TotalPages);
                dgvCustomers.DataSource = paged.Data;
                UpdatePager();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quyền truy cập hoặc mất kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyClientPage()
        {
            var list = _filteredCache ?? new List<CustomerDto>();

            _totalItems = list.Count;
            _totalPages = Math.Max(1, (int)Math.Ceiling(_totalItems / (double)_pageSize));
            if (_page > _totalPages) _page = _totalPages;
            if (_page < 1) _page = 1;

            dgvCustomers.DataSource = list.Skip((_page - 1) * _pageSize).Take(_pageSize).ToList();
            UpdatePager();
        }

        private void UpdatePager()
        {
            lblPageInfo.Text = $"Trang {_page}/{_totalPages}   ·   {_totalItems} khách hàng";
            btnFirst.Enabled = btnPrev.Enabled = _page > 1;
            btnNext.Enabled = btnLast.Enabled = _page < _totalPages;
        }

        private async Task ReloadCurrentAsync()
        {
            if (_filteredCache != null) ApplyClientPage();
            else await LoadDataAsync();
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

        // Nút Tải lại
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            _filteredCache = null;
            _page = 1;
            await LoadDataAsync();
        }

        // Click một dòng trên bảng -> đưa dữ liệu lên các ô nhập
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCustomers.Rows[e.RowIndex].DataBoundItem is CustomerDto c)
            {
                txtCustomerId.Text = c.CustomerId.ToString();
                txtCustomerName.Text = c.CustomerName;
                txtPhoneNumber.Text = c.PhoneNumber;
                txtAddress.Text = c.Address ?? string.Empty;
                txtRewardPoints.Text = c.RewardPoints.ToString();
                txtMembershipRank.Text = c.MembershipRank;
            }
        }

        // Nút THÊM MỚI (CREATE)
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên và Số điện thoại khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCustomer = new
            {
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = ParsePoints(),
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text) ? "Chuẩn" : txtMembershipRank.Text.Trim()
            };

            using var client = GetAuthenticatedClient();
            var response = await client.PostAsJsonAsync("customers", newCustomer);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm mới khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadCurrentAsync();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại! Lỗi: " + response.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút CẬP NHẬT (UPDATE)
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtCustomerId.Text);
            var updateCustomer = new
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = ParsePoints(),
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text) ? "Chuẩn" : txtMembershipRank.Text.Trim()
            };

            using var client = GetAuthenticatedClient();
            var response = await client.PutAsJsonAsync($"customers/{id}", updateCustomer);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadCurrentAsync();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại! Lỗi: " + response.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút XÓA (DELETE)
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtCustomerId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using var client = GetAuthenticatedClient();
                var response = await client.DeleteAsync($"customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ReloadCurrentAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Lỗi: " + response.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Nút TÌM KIẾM (SEARCH theo tên hoặc SĐT)
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                _filteredCache = null;
                _page = 1;
                await LoadDataAsync(); // Ô tìm kiếm trống thì tải lại toàn bộ
                return;
            }

            try
            {
                using var client = GetAuthenticatedClient();
                // EscapeDataString để từ khóa tiếng Việt/URL-safe
                var result = await client.GetFromJsonAsync<List<CustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(keyword)}");

                _filteredCache = result ?? new List<CustomerDto>();
                _page = 1;
                ApplyClientPage();
            }
            catch (Exception)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Đọc điểm tích lũy, mặc định 0 nếu nhập sai
        private int ParsePoints()
        {
            return int.TryParse(txtRewardPoints.Text.Trim(), out int points) ? points : 0;
        }

        // Xóa trắng các ô nhập liệu
        private void ClearInputs()
        {
            txtCustomerId.Text = "";
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtRewardPoints.Text = "";
            txtMembershipRank.Text = "";
        }
    }

    // Lớp DTO hứng dữ liệu JSON trả về từ Server
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = string.Empty;
    }
}
