using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private int _selectedUserId = 0;

        // ---- Trạng thái phân trang ----
        private int _page = 1;
        private int _pageSize = 10;
        private int _totalPages = 1;
        private int _totalItems = 0;
        private bool _ready = false;

        public FormUserManagement()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new object[] { "Admin", "Cashier", "Warehouse" });
            cboRole.SelectedIndex = 1;
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserId", DataPropertyName = "UserId", HeaderText = "Mã", Width = 60 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", DataPropertyName = "Username", HeaderText = "Tên đăng nhập", Width = 140 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "FullName", DataPropertyName = "FullName", HeaderText = "Họ và tên", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Role", DataPropertyName = "Role", HeaderText = "Vai trò", Width = 100 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsActive", DataPropertyName = "IsActive", HeaderText = "Hoạt động", Width = 90 });
        }

        private async void FormUserManagement_Load(object sender, EventArgs e)
        {
            cboPageSize.SelectedIndex = 1; // 10 dòng/trang
            await LoadUsersAsync();
            _ready = true;
        }

        // Nạp danh sách tài khoản theo trang (.Skip / .Take phía máy chủ)
        private async Task LoadUsersAsync()
        {
            try
            {
                var paged = await ApiClientService.Client.GetFromJsonAsync<PagedResult<UserDto>>(
                    $"users/paged?page={_page}&pageSize={_pageSize}");

                if (paged == null) return;

                if (paged.TotalPages > 0 && _page > paged.TotalPages)
                {
                    _page = paged.TotalPages;
                    await LoadUsersAsync();
                    return;
                }

                _totalItems = paged.TotalItems;
                _totalPages = Math.Max(1, paged.TotalPages);
                dgvUsers.DataSource = paged.Data;
                UpdatePager();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePager()
        {
            lblPageInfo.Text = $"Trang {_page}/{_totalPages}   ·   {_totalItems} tài khoản";
            btnFirst.Enabled = btnPrev.Enabled = _page > 1;
            btnNext.Enabled = btnLast.Enabled = _page < _totalPages;
        }

        private async Task ReloadCurrentAsync() => await LoadUsersAsync();

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

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvUsers.Rows[e.RowIndex];
                _selectedUserId = Convert.ToInt32(row.Cells["UserId"].Value ?? 0);
                txtUsername.Text = row.Cells["Username"].Value?.ToString();
                txtFullName.Text = row.Cells["FullName"].Value?.ToString();

                var role = row.Cells["Role"].Value?.ToString();
                if (!string.IsNullOrEmpty(role)) cboRole.SelectedItem = role;
            }
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier"
            };

            var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadCurrentAsync();
                txtUsername.Clear();
                txtPassword.Clear();
                txtFullName.Clear();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đặt lại mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới vào ô Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = await ApiClientService.Client.PutAsJsonAsync($"users/{_selectedUserId}/reset-password", new { NewPassword = txtPassword.Text });
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnToggleLock_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần khóa/mở khóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = await ApiClientService.Client.PutAsync($"users/{_selectedUserId}/toggle-lock", null);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Đã thay đổi trạng thái Khóa / Mở khóa tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadCurrentAsync();
            }
            else
            {
                MessageBox.Show("Thao tác thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
