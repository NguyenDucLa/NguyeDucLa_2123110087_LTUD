BUỔI 4: THIẾT KẾ KIẾN TRÚC GIAO DIỆN WINFORMS SHELL (SIDEBAR & MAIN WORKSPACE) VÀ ĐIỀU HƯỚNG PHÂN QUYỀN (ADMIN / CASHIER / WAREHOUSE)
1. MỤC TIÊU VÀ TÌNH HUỐNG THỰC TẾ
Mục tiêu:



Làm chủ kỹ thuật thiết kế giao diện đơn khung hiện đại (Single-Form Architecture) trên Windows Forms (.NET Core/C#) sử dụng cấu trúc Left Sidebar Panel (thanh chức năng) và Main Content Panel (vùng hiển thị nghiệp vụ).

Nắm vững kỹ thuật nhúng động Form con (TopLevel = false, Dock = Fill) vào vùng chứa mà không làm mở cửa sổ mới rườm rà.

Hiện thực hóa cơ chế Role-Based Access Control (RBAC) ngay trên giao diện Client: Ẩn/Hiện (hoặc kích hoạt/vô hiệu hóa) các nút chức năng tương ứng với 3 vai trò: Admin, Cashier (Thu ngân), Warehouse (Thủ kho).

Tình huống bài toán thực tế:



Trong hệ thống siêu thị mini, nhân viên thu ngân chỉ được quyền truy cập màn hình POS bán hàng; thủ kho chỉ được quản lý nhập kho và kiểm kê sản phẩm; trong khi quản trị viên (Admin) được toàn quyền thao tác danh mục, cấu hình và xem báo cáo tài chính.

Yêu cầu đặt ra là xây dựng một màn hình điều khiển trung tâm (FormMainShell) có thể tự động thích ứng với vai trò của người dùng ngay sau khi đăng nhập thành công.

2. BỘ DỮ LIỆU TÀI KHOẢN MẪU PHÂN QUYỀN (15 DÒNG KIỂM THỬ)

STT
Username Google Tài liệu
Mật khẩu ban đầu
Họ và tên nhân viên Google Tài liệu
Vai trò (Role) Google Tài liệu
Phạm vi chức năng cho phép PNG
1
admin01
123456
Nguyễn Quản Trị
Admin
Toàn quyền toàn bộ hệ thống
2
admin02
123456
Trần Giám Đốc
Admin
Toàn quyền toàn bộ hệ thống
3
cashier01
123456
Lê Thu Ngân
Cashier
Màn hình POS, Khách hàng thành viên
4
cashier02
123456
Phạm Bán Hàng
Cashier
Màn hình POS, Khách hàng thành viên
5
cashier03
123456
Hoàng Thu Ngân
Cashier
Màn hình POS, Khách hàng thành viên
6
cashier04
123456
Vũ Thị Quầy
Cashier
Màn hình POS, Khách hàng thành viên
7
cashier05
123456
Đỗ Bán Lẻ
Cashier
Màn hình POS, Khách hàng thành viên
8
ware01
123456
Ngô Quản Kho
Warehouse
Sản phẩm, Nhóm hàng, Nhập xuất kho
9
ware02
123456
Bùi Kiểm Kê
Warehouse
Sản phẩm, Nhóm hàng, Nhập xuất kho
10
ware03
123456
Dương Thủ Kho
Warehouse
Sản phẩm, Nhóm hàng, Nhập xuất kho
11
ware04
123456
Lý Nhập Hàng
Warehouse
Sản phẩm, Nhóm hàng, Nhập xuất kho
12
admin_backup
123456
Đặng Hỗ Trợ
Admin
Toàn quyền toàn bộ hệ thống
13
cashier06
123456
Hồ Ca Chiều
Cashier
Màn hình POS, Khách hàng thành viên
14
ware05
123456
Trương Vận Chuyển
Warehouse
Sản phẩm, Nhóm hàng, Nhập xuất kho
15
supervisor
123456
Mai Giám Sát
Admin
Toàn quyền toàn bộ hệ thống

3. NỘI DUNG THỰC HÀNH TỪNG BƯỚC (STEP-BY-STEP)
┌─────────────────────────────────────────────────────────────────────────────┐
│                             FormMainShell                                   │
├────────────────────┬────────────────────────────────────────────────────────┤
│  panelSidebar      │  panelTopHeader (Breadcrumb & User Info)               │
│  (Width = 230)     ├────────────────────────────────────────────────────────┤
│  - Logo MiniMart   │                                                        │
│  - btnPOS          │                                                        │
│  - btnCategory     │  panelMainContent                                      │
│  - btnProduct      │  (Dock = Fill)                                         │
│  - btnCustomer     │                                                        │
│  - btnReports      │  [Vùng nhúng động các UserControl / Form con]          │
│  - btnUserManage   │                                                        │
│  ───────────────   │                                                        │
│  - panelUserFooter │                                                        │
│    (Tên & Logout)  │                                                        │
└────────────────────┴────────────────────────────────────────────────────────┘


Bước 1: Thiết kế giao diện khung FormMainShell.cs
Mở project MiniSupermarket.WinForms, tạo một Windows Form mới đặt tên là FormMainShell.cs.


Cấu hình thuộc tính của FormMainShell tại cửa sổ Properties:



Size: 1280, 720 (Tối ưu độ phân giải HD chuẩn cho quầy thu ngân).

StartPosition: CenterScreen.

Text: Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini.

Kéo thả các điều khiển phân vùng:



panelSidebar (Thanh điều hướng bên trái):



Dock: Left, Width: 230, BackColor: Color.FromArgb(24, 30, 48) (Xanh đen đậm).

panelTopHeader (Thanh tiêu đề trên):



Kéo một Panel vào khoảng trống còn lại, chỉnh Dock: Top, Height: 60, BackColor: Color.White.

Kéo 1 Label (lblTitle): Font Bold 14pt, Text: BÀN LÀM VIỆC HỆ THỐNG.

Kéo 1 Label (lblUserInfo): Đặt sát bên phải, Font Segoe UI 10pt, Text: Xin chào: ....

panelMainContent (Vùng chứa trung tâm):



Kéo thêm một Panel vào vùng còn lại, chỉnh Dock: Fill, BackColor: Color.FromArgb(244, 245, 247).

Bố trí các nút bấm điều hướng vào panelSidebar:



Đặt 1 Label Logo ở trên cùng: 🛒 MiniMart POS, ForeColor: White, Font: Bold 14pt.

Kéo các nút Button (Chỉnh FlatStyle = Flat, FlatAppearance.BorderSize = 0, ForeColor = White, Height = 45, TextAlign = MiddleLeft):



btnPOS: 🛒 Bán hàng (POS)




btnCategory: 📁 Quản lý Danh mục




btnProduct: 📦 Quản lý Sản phẩm




btnCustomer: 👥 Quản lý Khách hàng




btnReports: 📊 Báo cáo Doanh thu




btnUserManage: 🛡 Quản trị Tài khoản

Ở đáy của panelSidebar, kéo một Panel phụ (Dock: Bottom, Height: 60) chứa nút btnLogout (🚪 Đăng xuất).


Bước 2: Xây dựng Cơ chế nạp Form động vào Main Panel
Mở tệp FormMainShell.cs và khai báo cấu trúc điều hướng form con vào panelMainContent:

C#
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms {
    public partial class FormMainShell : Form {
        // Biến lưu trữ Form con đang được kích hoạt hiển thị
        private Form? _activeForm = null;

        public FormMainShell() {
            InitializeComponent();
        }

        private void FormMainShell_Load(object sender, EventArgs e) {
            // 1. Hiển thị thông tin phiên người dùng đăng nhập
            lblUserInfo.Text = $"Nhân viên: {SessionManager.CurrentUsername} | Vai trò: [{SessionManager.CurrentRole}]";

            // 2. Kích hoạt phân quyền giao diện theo vai trò (Role-Based Access)
            ApplyRolePermissions(SessionManager.CurrentRole);

            // 3. Mở màn hình mặc định tương ứng với vai trò
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        /// <summary>
        /// Hàm nhúng động một Form con vào vùng panelMainContent
        /// </summary>
        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton) {
            // Nếu form con đang mở cùng loại, không cần khởi tạo lại
            if (_activeForm != null) {
                _activeForm.Close();
            }

            HighlightActiveButton(senderButton);

            _activeForm = childForm;
            childForm.TopLevel = false;                          // Bỏ thuộc tính cửa sổ độc lập cấp cao nhất
            childForm.FormBorderStyle = FormBorderStyle.None;     // Bỏ viền và thanh điều khiển Windows
            childForm.Dock = DockStyle.Fill;                     // Tràn toàn bộ vùng panelMainContent
            
            panelMainContent.Controls.Clear();                   // Dọn dẹp màn hình cũ
            panelMainContent.Controls.Add(childForm);            // Thêm form mới vào vùng chứa
            panelMainContent.Tag = childForm;
            
            lblTitle.Text = screenTitle;                        // Đồng bộ tên màn hình trên Header
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// Làm nổi bật nút menu bên Sidebar đang được chọn
        /// </summary>
        private void HighlightActiveButton(Button activeButton) {
            foreach (Control ctrl in panelSidebar.Controls) {
                if (ctrl is Button btn && btn != btnLogout) {
                    btn.BackColor = Color.FromArgb(24, 30, 48); // Màu gốc
                }
            }
            activeButton.BackColor = Color.FromArgb(41, 100, 180); // Màu xanh active
        }
    }
}


Bước 3: Hiện thực hóa Logic phân quyền (Admin / Cashier / Warehouse)
Bổ sung đoạn mã kiểm tra quyền hạn chặt chẽ ngay tại FormMainShell.cs:

C#
       /// <summary>
        /// Phân định quyền truy cập hiển thị theo vai trò người dùng
        /// </summary>
        private void ApplyRolePermissions(string role) {
            switch (role.ToUpper()) {
                case "ADMIN":
                    // Quản trị viên: Có toàn quyền sử dụng tất cả các nút
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    break;

                case "CASHIER":
                    // Thu ngân: Chỉ truy cập màn hình Bán hàng (POS) và Khách hàng
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    btnCategory.Visible = false;
                    btnProduct.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    break;

                case "WAREHOUSE":
                    // Thủ kho: Chỉ quản lý Danh mục nhóm hàng và Sản phẩm tồn kho
                    btnPOS.Visible = false;
                    btnCustomer.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    break;

                default:
                    // Vai trò không xác định: Khóa toàn bộ
                    MessageBox.Show("Tài khoản chưa được cấp quyền hạn hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Điều hướng ngay vào màn hình đúng chuyên môn của từng vai trò
        /// </summary>
        private void OpenDefaultScreenByRole(string role) {
            switch (role.ToUpper()) {
                case "ADMIN":
                case "WAREHOUSE":
                    OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);
                    break;
                case "CASHIER":
                    OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM", btnCustomer);
                    break;
            }
        }

        // ================= CÁC SỰ KIỆN CLICK NÚT TRÊN SIDEBAR =================
        private void btnCategory_Click(object sender, EventArgs e) {
            OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC SẢN PHẨM", btnCategory);
        }

        private void btnCustomer_Click(object sender, EventArgs e) {
            OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG THÂN THIẾT", btnCustomer);
        }

        private void btnPOS_Click(object sender, EventArgs e) {
            // Màn hình POS (sẽ hoàn thiện tại Buổi 5)
            MessageBox.Show("Màn hình Quét mã vạch Barcode POS sẵn sàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogout_Click(object sender, EventArgs e) {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất phiên làm việc?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes) {
                SessionManager.JwtToken = string.Empty;
                SessionManager.CurrentUsername = string.Empty;
                SessionManager.CurrentRole = string.Empty;

                this.Hide();
                new FormLogin().ShowDialog();
                this.Close();
            }
        }

4. ĐÁNH GIÁ KẾT QUẢ ĐẠT ĐƯỢC (CHECKLIST CHO SINH VIÊN)

[ ] Bố cục khung chuẩn: Ứng dụng không mở hàng loạt cửa sổ rời rạc. Toàn bộ thao tác nghiệp vụ chạy trực tiếp trong vùng panelMainContent.


[ ] Phân quyền chính xác theo bảng kiểm thử:



Đăng nhập tài khoản cashier01  Sidebar chỉ hiển thị nút Bán hàng (POS) và Khách hàng.


Đăng nhập tài khoản ware01  Sidebar chỉ hiển thị nút Danh mục và Sản phẩm.


Đăng nhập tài khoản admin01  Hiển thị đầy đủ tất cả các nút chức năng.


[ ] Trải nghiệm người dùng: Nhấp vào nút nào thì nút đó đổi màu nổi bật (HighlightActiveButton), tiêu đề màn hình trên lblTitle đồng bộ đúng tên nghiệp vụ.


5. BÀI TẬP THỰC HÀNH MỞ RỘNG (TỰ THỰC HÀNH CÓ GỢI Ý)

Đề bài: Xây dựng Màn hình Báo cáo Doanh thu Nhanh (FormQuickReport) và kiểm soát phân quyền cấp Action
Sinh viên hãy tạo một Form con tên là FormQuickReport.cs để nhúng vào panelMainContent. Chỉ tài khoản có vai trò Admin mới được phép bấm nút Xem doanh thu ca.

Gợi ý thực hiện:
Thiết kế FormQuickReport.cs: Gồm 1 DateTimePicker chọn ngày xem báo cáo, 1 nút btnRunReport (Xem báo cáo), và 3 thẻ tóm tắt (Kéo 3 Panel màu có chữ to):


Tổng số hóa đơn: lblTotalOrders

Tổng doanh thu: lblTotalRevenue

Mặt hàng bán chạy nhất: lblBestSeller

Bổ sung ràng buộc bảo vệ:



Trong sự kiện btnReports_Click trên FormMainShell, ngoài việc ẩn nút ở Sidebar, hãy viết thêm 1 lớp bảo vệ:



C#
if (SessionManager.CurrentRole != "Admin") {
    MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
    return;
}
OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT", btnReports);


Thử nghiệm: Dùng tài khoản cashier01 và ware01 để kiểm tra tính toàn vẹn của cơ chế phân quyền hai lớp (vừa ẩn giao diện vừa chặn logic).


HƯỚNG DẪN XÂY DỰNG CÁC FORM CHỨC NĂNG ĐỘNG (CHILD FORMS) TRONG ARCHITECTURE SHELL - BUỔI THỰC HÀNH 4
Tài liệu này hướng dẫn chi tiết cách xây dựng các Form con nghiệp vụ để nhúng động (TopLevel = false, Dock = Fill) vào vùng chứa panelMainContent của FormMainShell, tương ứng với cơ chế phân quyền Admin, Cashier (Thu ngân), và Warehouse (Thủ kho).

1. MÔ HÌNH ĐIỀU HƯỚNG DYNAMIC CHILD FORMS
┌──────────────────────────────────────────────────────────────┐
│                    FormMainShell (Parent)                    │
│ ┌─────────────────┬────────────────────────────────────────┐ │
│ │  panelSidebar   │ panelTopHeader (Breadcrumb & User Info)│ │
│ │  (Width = 230)  ├────────────────────────────────────────┤ │
│ │                 │            panelMainContent            │ │
│ │  [btnPOS]       │  ┌──────────────────────────────────┐  │ │
│ │  [btnCategory]  │  │                                  │  │ │
│ │  [btnProduct]   │  │   NHÚNG ĐỘNG CÁC FORM NGHIỆP VỤ  │  │ │
│ │  [btnCustomer]  │  │   (FormPOS / FormProduct / ...)  │  │ │
│ │  [btnReports]   │  │                                  │  │ │
│ │  [btnUserManage]│  └──────────────────────────────────┘  │ │
│ └─────────────────┴────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────────┘


2. CÁC FORM CON NGHIỆP VỤ CẦN TRIỂN KHAI
FORM 1: FormPOS.cs – Màn hình Bán hàng Quầy POS (Dành cho: Cashier, Admin)
2.1. Thiết kế Giao diện (GUI)
Khu vực Trái (65% chiều ngang): Giỏ hàng thanh toán

txtBarcode: Ô quét mã vạch (sự kiện KeyDown bắt phím Enter để tự động thêm sản phẩm vào giỏ).

dgvCart: DataGridView hiển thị giỏ hàng hiện tại gồm các cột: Mã SP, Tên SP, Đơn giá, Số lượng, Thành tiền.

Khu vực Phải (35% chiều ngang): Thông tin Khách hàng & Thanh toán



txtCustomerPhone: Ô nhập SĐT khách hàng thân thiết để tích điểm (gọi API Customer).

lblCustomerName: Hiển thị tên khách hàng và hạng thẻ.

lblTotalAmount: Label hiển thị tổng tiền cần thanh toán (Font to, màu nổi bật).

txtCashReceived: Tiền khách đưa.

lblChange: Tiền thừa trả lại khách.

btnCheckout: Nút THANH TOÁN (F9) (Màu xanh lá đậm).

btnClearCart: Nút HỦY GIỎ HÀNG (Màu đỏ).

2.2. Mã nguồn Code-Behind hoàn chỉnh (FormPOS.cs)
C#
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms {
    public partial class FormPOS : Form {
        private readonly List<CartItemDto> _cart = new();

        public FormPOS() {
            InitializeComponent();
            SetupCartGrid();
        }

        private void SetupCartGrid() {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 80 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn Giá", Width = 110 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 70 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Thành Tiền", Width = 120 });
        }

        // Bắt sự kiện quét mã Barcode
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtBarcode.Text)) {
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
            }
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode) {
            try {
                // Gọi API tra cứu sản phẩm theo Barcode
                var product = await ApiClientService.Client.GetFromJsonAsync<ProductDto>($"products/barcode/{barcode}");
                if (product == null) {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null) {
                    existingItem.Quantity++;
                } else {
                    _cart.Add(new CartItemDto {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            } catch (Exception ex) {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay() {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;

            decimal total = _cart.Sum(x => x.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";
            CalculateChange();
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e) {
            CalculateChange();
        }

        private void CalculateChange() {
            decimal total = _cart.Sum(x => x.TotalPrice);
            if (decimal.TryParse(txtCashReceived.Text, out decimal cashReceived)) {
                decimal change = cashReceived - total;
                lblChange.Text = change >= 0 ? $"{change:N0} đ" : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Black : Color.Red;
            } else {
                lblChange.Text = "0 đ";
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e) {
            if (_cart.Count == 0) {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var orderRequest = new {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);
            if (response.IsSuccessStatusCode) {
                MessageBox.Show("Thanh toán thành công và đã in hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cart.Clear();
                UpdateCartDisplay();
                txtCashReceived.Clear();
                txtCustomerPhone.Clear();
                lblCustomerName.Text = "Khách vãng lai";
            } else {
                MessageBox.Show("Thanh toán thất bại từ máy chủ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class CartItemDto {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class ProductDto {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}


FORM 2: FormProductManagement.cs – Quản lý Sản phẩm & Kho hàng (Dành cho: Warehouse, Admin)
2.1. Thiết kế Giao diện (GUI)
Thanh trên: txtSearchBarcode (tìm theo mã vạch), cboFilterCategory (lọc theo nhóm hàng), nút btnSearch.

Khu vực trung tâm (Trái 68%): dgvProducts hiển thị: ProductId, Mã vạch, Tên sản phẩm, Đơn giá, Tồn kho, Nhóm hàng.

Khu vực chi tiết (Phải 32%): txtId, txtBarcode, txtProductName, nudPrice (NumericUpDown), nudStock (NumericUpDown), cboCategory (ComboBox chọn danh mục), các nút btnLoad, btnAdd, btnUpdate, btnDelete.

2.2. Mã nguồn Code-Behind hoàn chỉnh (FormProductManagement.cs)
C#
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Models;

namespace MiniSupermarket.WinForms {
    public partial class FormProductManagement : Form {
        public FormProductManagement() {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object sender, EventArgs e) {
            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
        }

        private async Task LoadCategoriesToComboAsync() {
            try {
                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories");
                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";
            } catch (Exception ex) {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsAsync() {
            try {
                var products = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>("products");
                dgvProducts.DataSource = products;
            } catch (Exception ex) {
                MessageBox.Show("Lỗi nạp sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                var row = dgvProducts.Rows[e.RowIndex];
                txtId.Text = row.Cells["ProductId"].Value?.ToString();
                txtBarcode.Text = row.Cells["Barcode"].Value?.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();
                nudPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value ?? 0);
                nudStock.Value = Convert.ToInt32(row.Cells["StockQuantity"].Value ?? 0);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e) {
            var newProd = new {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = (int)cboCategory.SelectedValue
            };

            var res = await ApiClientService.Client.PostAsJsonAsync("products", newProd);
            if (res.IsSuccessStatusCode) {
                MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsAsync();
                ClearInputs();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtId.Text)) return;
            int id = int.Parse(txtId.Text);

            var updateProd = new {
                ProductId = id,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = (int)cboCategory.SelectedValue
            };

            var res = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", updateProd);
            if (res.IsSuccessStatusCode) {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtId.Text)) return;
            int id = int.Parse(txtId.Text);

            if (MessageBox.Show($"Xác nhận xóa sản phẩm ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                var res = await ApiClientService.Client.DeleteAsync($"products/{id}");
                if (res.IsSuccessStatusCode) {
                    MessageBox.Show("Đã xóa sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                    ClearInputs();
                }
            }
        }

        private void ClearInputs() {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
        }
    }
}


FORM 3: FormUserManagement.cs – Quản lý Tài khoản & Phân quyền (Dành riêng cho: Admin)
3.1. Thiết kế Giao diện (GUI)
dgvUsers: Hiển thị danh sách nhân viên (Username, Họ tên, Email, Vai trò, Trạng thái hoạt động).


txtUsername, txtFullName, txtPassword, cboRole (gồm 3 mục: Admin, Cashier, Warehouse).


Các nút: btnAddUser, btnResetPassword, btnToggleLock (Khóa/Mở khóa tài khoản).


3.2. Mã nguồn Code-Behind hoàn chỉnh (FormUserManagement.cs)
C#
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms {
    public partial class FormUserManagement : Form {
        public FormUserManagement() {
            InitializeComponent();
            cboRole.Items.AddRange(new string[] { "Admin", "Cashier", "Warehouse" });
            cboRole.SelectedIndex = 1;
        }

        private async void FormUserManagement_Load(object sender, EventArgs e) {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync() {
            try {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users");
                dgvUsers.DataSource = users;
            } catch (Exception ex) {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAddUser_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)) {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier"
            };

            var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
            if (res.IsSuccessStatusCode) {
                MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUsersAsync();
                txtUsername.Clear();
                txtPassword.Clear();
                txtFullName.Clear();
            } else {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    public class UserDto {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}


3. GẮN KẾT CÁC FORM ĐỘNG VÀO FormMainShell.cs
Cập nhật lại toàn bộ các sự kiện bấm nút trên Sidebar của FormMainShell.cs để mở tương ứng các màn hình:
C#
// 1. Quản lý Danh mục (Admin & Warehouse)
private void btnCategory_Click(object sender, EventArgs e) {
    OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);
}

// 2. Bán hàng POS (Admin & Cashier)
private void btnPOS_Click(object sender, EventArgs e) {
    OpenChildForm(new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
}

// 3. Quản lý Sản phẩm (Admin & Warehouse)
private void btnProduct_Click(object sender, EventArgs e) {
    OpenChildForm(new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG", btnProduct);
}

// 4. Quản lý Khách hàng (Admin & Cashier)
private void btnCustomer_Click(object sender, EventArgs e) {
    OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM", btnCustomer);
}

// 5. Báo cáo Doanh thu (Chỉ Admin)
private void btnReports_Click(object sender, EventArgs e) {
    OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT CA BÁN", btnReports);
}

// 6. Quản trị Tài khoản Người dùng (Chỉ Admin)
private void btnUserManage_Click(object sender, EventArgs e) {
    OpenChildForm(new FormUserManagement(), "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN HỆ THỐNG", btnUserManage);
}


4. BẢNG TỔNG KẾT VAI TRÒ VÀ TRÁCH NHIỆM (DÀNH CHO SINH VIÊN THỰC TẬP)
Vai trò (Role)
Form khởi chạy mặc định
Các Form được cấp phép mở PNG
Quyền hạn bị ngăn chặn tuyệt đối PNG
Admin
FormCategoryManagement
FormPOS, FormCategoryManagement, FormProductManagement, FormCustomerManagement, FormQuickReport, FormUserManagement
Không bị chặn (Toàn quyền hệ thống)
Cashier
FormPOS
FormPOS, FormCustomerManagement
Quản lý kho, Sửa giá sản phẩm, Xem báo cáo doanh thu, Quản lý tài khoản
Warehouse
FormProductManagement
FormProductManagement, FormCategoryManagement
Bán hàng POS, Xem doanh thu, Tạo tài khoản, Quản lý khách hàng


