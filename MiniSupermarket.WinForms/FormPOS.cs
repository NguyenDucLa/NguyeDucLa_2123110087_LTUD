using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new();

        // Thông tin khách hàng đang chọn (phục vụ đổi điểm thưởng)
        private CustomerDto? _customer;
        private int _availablePoints = 0;

        // Tổng tiền hàng / tiền được giảm / số tiền phải trả
        private decimal CartTotal => _cart.Sum(x => x.TotalPrice);
        private decimal RedeemDiscount => Math.Min(nudRedeemPoints.Value * 1000m, CartTotal);   // 1 điểm = 1.000đ
        private decimal PayableTotal => CartTotal - RedeemDiscount;

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 80 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn Giá", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 70 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Thành Tiền", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
        }

        // Bắt sự kiện quét mã Barcode (nhấn Enter để thêm sản phẩm vào giỏ)
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
            }
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                // Gọi API tra cứu sản phẩm theo Barcode
                var product = await ApiClientService.Client.GetFromJsonAsync<ProductDto>($"products/barcode/{barcode}");
                if (product == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            }
            catch (Exception)
            {
                MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.ToList();

            lblTotalAmount.Text = $"{PayableTotal:N0} đ";
            lblDiscount.Text = RedeemDiscount > 0
                ? $"Giảm từ điểm: -{RedeemDiscount:N0} đ   (tổng hàng {CartTotal:N0} đ)"
                : $"Giảm từ điểm: 0 đ   (tổng hàng {CartTotal:N0} đ)";

            CalculateChange();
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        // Đổi số điểm muốn dùng -> tính lại số tiền phải trả
        private void nudRedeemPoints_ValueChanged(object sender, EventArgs e)
        {
            UpdateCartDisplay();
        }

        private void btnUseMaxPoints_Click(object sender, EventArgs e)
        {
            // Số điểm tối đa có thể dùng = min(điểm khả dung, tổng tiền / 1.000)
            decimal maxByMoney = Math.Floor(CartTotal / 1000m);
            decimal maxPoints = Math.Min(_availablePoints, maxByMoney);

            if (maxPoints <= 0)
            {
                MessageBox.Show("Không có điểm khả dụng hoặc giỏ hàng chưa có sản phẩm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            nudRedeemPoints.Value = maxPoints;
        }

        // Tra cứu khách hàng theo SĐT để biết điểm thưởng
        private async void txtCustomerPhone_Leave(object sender, EventArgs e)
        {
            await LookupCustomerAsync();
        }

        private async Task LookupCustomerAsync()
        {
            string phone = txtCustomerPhone.Text.Trim();
            _customer = null;
            _availablePoints = 0;
            nudRedeemPoints.Value = 0;

            if (phone.Length < 3)
            {
                lblCustomerName.Text = "Khách vãng lai";
                lblRedeem.Text = "Điểm thưởng khả dụng: 0 điểm";
                nudRedeemPoints.Maximum = 0;
                UpdateCartDisplay();
                return;
            }

            try
            {
                var list = await ApiClientService.Client
                    .GetFromJsonAsync<List<CustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(phone)}");

                _customer = list?.FirstOrDefault(c => c.PhoneNumber == phone);

                if (_customer == null)
                {
                    lblCustomerName.Text = "Không tìm thấy khách hàng này";
                    lblRedeem.Text = "Điểm thưởng khả dụng: 0 điểm";
                    nudRedeemPoints.Maximum = 0;
                }
                else
                {
                    _availablePoints = _customer.RewardPoints;
                    lblCustomerName.Text = $"{_customer.CustomerName}  ·  {_customer.MembershipRank}";
                    lblRedeem.Text = $"Điểm thưởng khả dụng: {_availablePoints:N0} điểm";
                    nudRedeemPoints.Maximum = Math.Max(0, _availablePoints);
                }
            }
            catch
            {
                lblCustomerName.Text = "Không kết nối được máy chủ";
            }

            UpdateCartDisplay();
        }

        private void CalculateChange()
        {
            decimal total = PayableTotal;
            if (decimal.TryParse(txtCashReceived.Text, out decimal cashReceived))
            {
                decimal change = cashReceived - total;
                lblChange.Text = change >= 0 ? $"{change:N0} đ" : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Black : Color.Red;
            }
            else
            {
                lblChange.Text = "0 đ";
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                CustomerId = _customer?.CustomerId,
                RedeemPoints = (int)nudRedeemPoints.Value,
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            try
            {
                btnCheckout.Enabled = false;
                var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CheckoutResultDto>();

                    string msg = "Thanh toán thành công!\n\n";
                    msg += $"Mã hóa đơn: {result?.OrderCode}\n";
                    msg += $"Tổng tiền hàng: {result?.TotalAmount:N0} đ\n";
                    if ((result?.Discount ?? 0) > 0)
                        msg += $"Giảm từ {result?.RedeemPoints:N0} điểm: -{result?.Discount:N0} đ\n";
                    msg += $"Khách phải trả: {result?.FinalAmount:N0} đ";

                    if (!string.IsNullOrEmpty(result?.CustomerName))
                    {
                        msg += $"\n\nKhách hàng: {result!.CustomerName}";
                        msg += $"\nĐiểm tích lũy còn lại: {result.RewardPoints:N0} điểm";
                    }

                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _cart.Clear();
                    txtCashReceived.Clear();
                    txtCustomerPhone.Clear();
                    lblCustomerName.Text = "Khách vãng lai";
                    lblRedeem.Text = "Điểm thưởng khả dụng: 0 điểm";
                    _customer = null;
                    _availablePoints = 0;
                    nudRedeemPoints.Maximum = 0;
                    nudRedeemPoints.Value = 0;
                    UpdateCartDisplay();
                }
                else
                {
                    var err = await response.Content.ReadFromJsonAsync<ApiMessageDto>();
                    MessageBox.Show(err?.Message ?? "Thanh toán thất bại từ máy chủ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckout.Enabled = true;
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            _cart.Clear();
            txtCashReceived.Clear();
            nudRedeemPoints.Value = 0;
            UpdateCartDisplay();
        }

        // Phím tắt: F9 = Thanh toán
        private void FormPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                btnCheckout_Click(sender, e);
            }
        }
    }
}
