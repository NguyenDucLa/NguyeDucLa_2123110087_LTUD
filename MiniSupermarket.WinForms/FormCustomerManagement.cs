using System.Net.Http.Headers; // Đính kèm Authorization Header (JWT)
using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // Địa chỉ Base API (khớp với cổng HTTPS của MiniSupermarket.API)
        private const string ApiBaseUrl = "https://localhost:7075/api/";

        public FormCustomerManagement()
        {
            InitializeComponent();
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
            await LoadDataAsync();
        }

        // Hàm dùng chung: GET danh sách khách hàng đổ lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();
                var customers = await client.GetFromJsonAsync<List<CustomerDto>>("customers");
                dgvCustomers.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quyền truy cập hoặc mất kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Tải lại
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Click một dòng trên bảng -> đưa dữ liệu lên các ô nhập
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                txtCustomerId.Text = row.Cells["CustomerId"].Value?.ToString() ?? string.Empty;
                txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? string.Empty;
                txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? string.Empty;
                txtAddress.Text = row.Cells["Address"]?.Value?.ToString() ?? string.Empty;
                txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString() ?? string.Empty;
                txtMembershipRank.Text = row.Cells["MembershipRank"].Value?.ToString() ?? string.Empty;
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
                await LoadDataAsync();
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
                await LoadDataAsync();
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
                    await LoadDataAsync();
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
                await LoadDataAsync(); // Ô tìm kiếm trống thì tải lại toàn bộ
                return;
            }

            try
            {
                using var client = GetAuthenticatedClient();
                // EscapeDataString để từ khóa tiếng Việt/URL-safe
                var result = await client.GetFromJsonAsync<List<CustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(keyword)}");
                dgvCustomers.DataSource = result;
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
