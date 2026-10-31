# TÀI LIỆU ĐỐI CHIẾU TIÊU CHÍ 36 → 48 VỚI CODE

> **Dự án:** Hệ thống Quản lý Bán lẻ & Tồn kho — Tiệm Rau củ quả sạch **Mộc Garden**
> **Solution:** `MiniSupermarket.API.sln` — 2 project: `MiniSupermarket.API` (ASP.NET Core 8) + `MiniSupermarket.WinForms` (WinForms .NET 8)
> **Ngày lập:** 31/10/2026
>
> Ghi chú:
> - Số dòng được ghi theo bản code hiện tại, có thể lệch vài dòng nếu file bị sửa tiếp. **Tên hàm/class là căn cứ chính xác** để tra.
> - Trong một số tiêu chí, mô tả của đề dùng tên cột khác với CSDL thực tế (ví dụ `CreatedDate`), tài liệu ghi rõ phần **điều chỉnh tương đương**.

---

## BẢNG TỔNG QUAN

| # | Tiêu chí | File chính | Thành phần then chốt |
|---|---|---|---|
| 36 | Shell phân vùng cố định | `FormMainShell.Designer.cs`, `FormMainShell.cs` | `panelSidebar` / `panelTopHeader` / `panelMainContent`, `OpenChildForm()` |
| 37 | Đổi màn hình không giật lag | `FormMainShell.cs` | `OpenChildForm()`, `HighlightActiveButton()` |
| 38 | Đăng nhập lưu Token | `FormLogin.cs`, `SessionManager.cs` | `SessionManager.JwtToken` (static) |
| 39 | Ẩn/hiện nút theo quyền | `FormMainShell.cs` | `ApplyRolePermissions()`, `OpenDefaultScreenByRole()` |
| 40 | HttpClient tập trung | `ApiClientService.cs` | `ApiClientService.Client`, `AuthenticationHeaderValue("Bearer", …)` |
| 41 | DataGridView danh mục sản phẩm | `FormProductManagement.cs/.Designer.cs` | `SetupGrid()`, `LoadProductsAsync()`, `btnAdd/Update/Delete_Click` |
| 42 | POS tính tiền + thông báo lỗi | `FormPOS.cs` | `AddProductToCartByBarcodeAsync()`, `CalculateChange()`, `btnCheckout_Click()` |
| 43 | Low-Stock Alert | `FormProductManagement.cs` | `dgvProducts_CellFormatting()` |
| 44 | KPI Summary Cards | `ReportsController.DashboardSummary()`, `FormQuickReport.cs` | ADO.NET `ExecuteScalar` × 4 |
| 45 | Bar Chart doanh thu | `Database/sp_GetRevenueByDay.sql`, `ReportsController.RevenueByDay()`, `FormQuickReport.DrawChart()` | `Chart` + `SeriesChartType.Column` |
| 46 | Top 5 sản phẩm bán chạy | `ReportsController.TopProducts()`, `FormQuickReport.LoadReportAsync()` | LINQ `GroupBy` → `OrderByDescending` → `Take(5)` |
| 47 | Đổi điểm thưởng trừ tiền | `OrdersController.Checkout()`, `FormPOS.cs` | `RedeemPoints`, `BeginTransactionAsync()` |
| 48 | Xuất Excel/CSV | `FormProductManagement.btnExportCsv_Click()`, `FormQuickReport.btnExport_Click()` | `SaveFileDialog` + `UTF8Encoding(true)` |

---

## 36. Giao diện Shell phân vùng cố định

**Yêu cầu:** Menu bên trái – Tiêu đề bên trên – Vùng làm việc ở giữa; form con **không** bật ra ngoài cửa sổ.

### Vị trí code

| File | Vị trí | Nội dung |
|---|---|---|
| `MiniSupermarket.WinForms/FormMainShell.Designer.cs` | khai báo 3 panel | `panelSidebar` (`Dock = Left`, rộng 230) · `panelTopHeader` (`Dock = Top`, cao 60) · `panelMainContent` (`Dock = Fill`) |
| `MiniSupermarket.WinForms/FormMainShell.cs` | hàm `OpenChildForm()` (≈ dòng 33) | nhúng form con vào `panelMainContent` |

```csharp
// FormMainShell.cs — OpenChildForm()
if (_activeForm != null) _activeForm.Close();
HighlightActiveButton(senderButton);

_activeForm = childForm;
childForm.TopLevel = false;                       // không phải cửa sổ độc lập
childForm.FormBorderStyle = FormBorderStyle.None; // bỏ viền/thanh tiêu đề
childForm.Dock = DockStyle.Fill;                  // tràn vùng nội dung

panelMainContent.Controls.Clear();
panelMainContent.Controls.Add(childForm);
panelMainContent.Tag = childForm;
lblTitle.Text = screenTitle;
childForm.BringToFront();
childForm.Show();
```

**Kiểm chứng:** đăng nhập → cửa sổ chia 3 vùng rõ ràng; bấm menu, form con chỉ hiện trong vùng giữa, không có thanh tiêu đề riêng, không kéo ra ngoài được.

---

## 37. Đổi màn hình không bị giật lag

**Yêu cầu:** Bấm menu thì form con nạp mượt vào panel trung tâm (`TopLevel = false`, `Dock = Fill`).

### Vị trí code — `MiniSupermarket.WinForms/FormMainShell.cs`

| Hàm | (≈ dòng) | Vai trò |
|---|---|---|
| `OpenChildForm(Form, string, Button?)` | 33 | đóng form cũ → nạp form mới bằng `Controls.Clear()` + `Controls.Add()` (không tạo cửa sổ mới) |
| `HighlightActiveButton(Button?)` | 60 | tô màu nút đang chọn, trả các nút khác về màu gốc |

Cơ chế chống giật:
1. Mỗi lúc **chỉ tồn tại 1 form con** (`_activeForm.Close()` trước khi thêm form mới) → không chồng chất control.
2. Không dùng `form.ShowDialog()` hay tạo `Form` top-level (tránh cấp phát handle cửa sổ mới cho mỗi lần bấm).
3. `Controls.Clear()` giải phóng control cũ ngay, `BringToFront()` đưa form mới lên trước khi vẽ.
4. `FormProductManagement`, `FormQuickReport`… đều bật `AutoGenerateColumns = false` để lưới không phải suy luận cột lại mỗi lần nạp.

**Kiểm chứng:** bấm qua lại 6 menu liên tục, không thấy form “nháy trắng” hay chồng form.

---

## 38. Màn hình Đăng nhập lưu Token

**Yêu cầu:** Nhập tài khoản → gọi API đăng nhập → lưu Token vào **biến tĩnh** để các form sau dùng.

### Vị trí code

| File | Vị trí | Nội dung |
|---|---|---|
| `MiniSupermarket.WinForms/SessionManager.cs` | `public static class SessionManager` | `JwtToken`, `CurrentRole`, `CurrentUsername`, `CurrentFullName` (đều là **static property**) |
| `MiniSupermarket.WinForms/FormLogin.cs` | ≈ dòng 33–48 | `PostAsJsonAsync("auth/login", …)` → gán 4 giá trị vào `SessionManager` → mở `FormMainShell` |

```csharp
// FormLogin.cs
var response = await _client.PostAsJsonAsync("auth/login", loginData);
// ...
SessionManager.JwtToken        = doc.RootElement.GetProperty("token").GetString()   ?? string.Empty;
SessionManager.CurrentRole     = doc.RootElement.GetProperty("role").GetString()    ?? string.Empty;
SessionManager.CurrentUsername = doc.RootElement.TryGetProperty("username", out var un) ? (un.GetString() ?? username) : username;
SessionManager.CurrentFullName = doc.RootElement.TryGetProperty("fullName", out var fn) ? (fn.GetString() ?? username) : username;

var shell = new FormMainShell();
```

Phía server: `MiniSupermarket.API/Controllers/AuthController.cs` → `POST api/auth/login` so khớp mật khẩu **đã băm PBKDF2** (`PasswordHelper.Verify`) rồi phát JWT (HS256, hết hạn 2 giờ).

**Kiểm chứng:** sau khi đăng nhập, mọi request đều có header `Authorization: Bearer …` (xem mục 40) → gọi `GET api/users` trả 200; nếu xoá token (đăng xuất) thì trả 401.

---

## 39. Ẩn/Hiện nút bấm theo quyền

**Yêu cầu:** Thu ngân chỉ thấy nút **Bán hàng POS**; Quản lý (Admin) thấy đầy đủ nút quản trị.

### Vị trí code — `MiniSupermarket.WinForms/FormMainShell.cs`

| Hàm | (≈ dòng) | Nội dung |
|---|---|---|
| `ApplyRolePermissions(string role)` | 79 | bật/tắt `Visible` cho 6 nút sidebar theo vai trò |
| `OpenDefaultScreenByRole(string role)` | 124 | mở màn hình mặc định: Admin → Danh mục, Cashier → POS, Warehouse → Danh mục |

Ma trận phân quyền đang cài đặt:

| Vai trò | Nút hiển thị | Màn hình mặc định |
|---|---|---|
| **Admin** | tất cả 6 nút | Quản lý Danh mục |
| **Cashier** | POS, Khách hàng | POS |
| **Warehouse** | Danh mục, Sản phẩm | Quản lý Danh mục |

Bảo vệ 2 lớp: ngoài việc ẩn nút ở client, API vẫn chặn bằng `[Authorize(Roles = "Admin")]` (`UsersController`, `ReportsController`) và `[Authorize(Roles = "Admin,Warehouse")]` (`ProductsController` POST/PUT/DELETE) → gọi thẳng API vẫn bị **403**.

Điểm vào: `FormMainShell` constructor (≈ dòng 24–27) gọi
```csharp
ApplyRolePermissions(SessionManager.CurrentRole);
OpenDefaultScreenByRole(SessionManager.CurrentRole);
```

**Kiểm chứng:** đăng nhập `cashier01/123456` → sidebar chỉ còn 2 nút; đăng nhập `admin01` → đủ 6 nút.

---

## 40. Gọi API tập trung qua HttpClient

**Yêu cầu:** Class dùng chung, tự đính kèm Bearer Token vào Request Header.

### Vị trí code — `MiniSupermarket.WinForms/ApiClientService.cs` (toàn bộ file)

```csharp
public static class ApiClientService
{
    private const string ApiBaseUrl = "https://localhost:7075/api/";

    private static readonly HttpClient _client = new HttpClient      // 1 instance dùng chung
    {
        BaseAddress = new Uri(ApiBaseUrl)
    };

    public static HttpClient Client
    {
        get
        {
            _client.DefaultRequestHeaders.Authorization =
                string.IsNullOrEmpty(SessionManager.JwtToken)
                    ? null
                    : new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            return _client;
        }
    }
}
```

Sử dụng (mọi form): `ApiClientService.Client.GetFromJsonAsync<...>(...)`, `PostAsJsonAsync(...)`, `PutAsJsonAsync(...)`, `DeleteAsync(...)`.

> Hai form `FormCategoryManagement`, `FormCustomerManagement` dùng `GetAuthenticatedClient()` riêng (tạo `HttpClient` có gắn Bearer) — cách này viết từ Buổi 2, vẫn đúng yêu cầu nhưng **không dùng chung instance**. Nếu giảng viên yêu cầu tuyệt đối 1 class dùng chung, có thể đổi 2 form này sang `ApiClientService.Client`.

---

## 41. Bảng danh mục sản phẩm DataGridView

**Yêu cầu:** Lưới DataGridView tải đủ danh sách từ Web API; nút Thêm/Sửa/Xóa hoạt động.

### Vị trí code — `MiniSupermarket.WinForms/FormProductManagement.cs`

| Hàm | (≈ dòng) | Nội dung |
|---|---|---|
| `SetupGrid()` | 12 | khai báo 6 cột: `ProductId`, `Barcode`, `ProductName`, `Price`, `StockQuantity`, `CategoryName` |
| `LoadProductsAsync()` | ≈ 46 | `GET api/products/paged?page=&pageSize=` → `PagedResult<ProductDto>` |
| `dgvProducts_CellClick()` | ≈ 90 | đổ dữ liệu dòng được chọn lên các ô nhập |
| `btnAdd_Click()` | ≈ 120 | `POST api/products` → **201** |
| `btnUpdate_Click()` | ≈ 150 | `PUT api/products/{id}` → **204** |
| `btnDelete_Click()` | ≈ 175 | `DELETE api/products/{id}` → **204**, chặn xoá nếu SP đã có trong hoá đơn (**400**) |
| `btnSearch_Click()` | ≈ 105 | `GET api/products/search?keyword=` hoặc `by-category/{id}` |

Phía server: `MiniSupermarket.API/Controllers/ProductsController.cs`
- `GetAll()`, `GetPaged()`, `GetById()`, `Search()`, `GetByBarcode()`, `GetByCategory()`, `Create()`, `Update()`, `Delete()`
- Đọc dữ liệu có `AsNoTracking()`, có `Include(p => p.Category)` để lấy tên nhóm hàng.

**Kiểm chứng:** mở màn hình Sản phẩm → 60 sản phẩm chia 6 trang; thêm/sửa/xoá đều báo kết quả và lưới tự nạp lại đúng trang.

---

## 42. Quầy bán hàng tính tiền (POS) + thông báo lỗi thân thiện

**Yêu cầu:** Quét mã ra tên hàng, **tự cộng dồn số lượng**, tự tính tổng tiền và tiền thối; mất mạng/nhập sai thì hiện hộp thoại tiếng Việt, **không bị văng**.

### Vị trí code — `MiniSupermarket.WinForms/FormPOS.cs`

| Hàm | (≈ dòng) | Nội dung |
|---|---|---|
| `txtBarcode_KeyDown()` | 36 | nhấn **Enter** trong ô mã vạch → gọi thêm vào giỏ |
| `AddProductToCartByBarcodeAsync()` | 46 | `GET api/products/barcode/{barcode}`; nếu SP đã có trong giỏ thì **`existingItem.Quantity++`** (cộng dồn), ngược lại thêm dòng mới |
| `UpdateCartDisplay()` | 85 | gán `DataSource`, tính tổng tiền, gọi `CalculateChange()` |
| `CalculateChange()` | 173 | `tiền thối = tiền khách đưa − số phải trả`; âm → hiện **"Chưa đủ tiền!"** màu đỏ |
| `btnCheckout_Click()` | ≈ 190 | `POST api/orders/checkout`; đọc lỗi trả về và hiện nguyên văn tiếng Việt |
| `FormPOS_KeyDown()` | ≈ 265 | phím tắt **F9** = Thanh toán |

Chống crash:
- Mọi lời gọi API nằm trong `try/catch`, nhánh `catch` hiện `MessageBox` tiếng Việt ("Không tìm thấy sản phẩm có mã vạch này!", "Lỗi kết nối máy chủ: …").
- Trước khi gửi, kiểm tra giỏ rỗng; server trả lỗi (ví dụ không đủ tồn kho) thì hiện `err.Message`.
- `btnCheckout.Enabled = false` trong lúc chờ để tránh bấm 2 lần tạo 2 hoá đơn.

**Kiểm chứng:** quét `8938000000001` hai lần → dòng giỏ có SL = 2; nhập tiền khách đưa nhỏ hơn tổng → hiện "Chưa đủ tiền!"; tắt API rồi bấm Thanh toán → hiện hộp thoại lỗi, app vẫn chạy.

---

## 43. Huy hiệu & Cảnh báo Hàng sắp hết kho (Low-Stock Alert)

**Yêu cầu:** Sản phẩm có `StockQuantity <= 10` được tô nền đỏ/cam bằng sự kiện `CellFormatting`, gán `e.CellStyle.BackColor = Color.LightCoral`.

### Vị trí code — `MiniSupermarket.WinForms/FormProductManagement.cs`

| (≈ dòng) | Nội dung |
|---|---|
| 34 | đăng ký sự kiện trong `SetupGrid()`: `dgvProducts.CellFormatting += dgvProducts_CellFormatting;` |
| 40 | hàm xử lý `dgvProducts_CellFormatting()` |

```csharp
// Tiêu chí #43 - Low-Stock Alert
private Font? _lowStockFont;

private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
{
    if (e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count) return;
    if (e.CellStyle == null) return;

    if (dgvProducts.Rows[e.RowIndex].DataBoundItem is ProductDto product && product.StockQuantity <= 10)
    {
        _lowStockFont ??= new Font(dgvProducts.Font, FontStyle.Bold);

        e.CellStyle.BackColor          = Color.LightCoral;   // nền đỏ nhạt
        e.CellStyle.SelectionBackColor = Color.IndianRed;
        e.CellStyle.Font               = _lowStockFont;      // chữ đậm
    }
}
```

Dữ liệu để thấy hiệu ứng: seed cố ý để 4 sản phẩm tồn thấp (migration `SeedLowStockProducts`):

| SP | Tên | Tồn |
|---|---|---|
| 50 | Ngũ cốc granola 500g | 6 |
| 54 | Hạt mắc ca 250g | 8 |
| 58 | Giỏ quà trái cây cao cấp | 5 |
| 60 | Combo nấu lẩu rau củ 4 người | 9 |

**Kiểm chứng:** màn hình Sản phẩm → bấm `»` sang trang 5–6 → các dòng tồn ≤ 10 nền đỏ, chữ đậm.

---

## 44. Dashboard Thẻ chỉ số tổng quan (KPI Summary Cards)

**Yêu cầu:** 4 thẻ số liệu lớn trên đỉnh màn hình: Doanh thu hôm nay · Tổng số hoá đơn · Số khách hàng mới · Số mặt hàng sắp hết (tồn ≤ 10). API: `GET api/reports/dashboard-summary` dùng **ADO.NET `ExecuteScalar`**.

### Phía API — `MiniSupermarket.API/Controllers/ReportsController.cs`

| Vị trí | Nội dung |
|---|---|
| `[HttpGet("dashboard-summary")]`<br>`DashboardSummary()` ≈ dòng 230 | 4 lệnh `ExecuteScalarAsync()` trên **4 `SqlCommand` riêng** |

```csharp
// 1. Doanh thu hôm nay
"SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)"
// 2. Tổng số hoá đơn đã xuất hôm nay
"SELECT COUNT(*) FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)"
// 3. Số khách hàng mới đăng ký hôm nay
"SELECT COUNT(*) FROM Customers WHERE CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)"
// 4. Số mặt hàng sắp hết (tồn <= 10)
"SELECT COUNT(*) FROM Products WHERE StockQuantity <= 10"
```

Trả về: `{ success, generatedAt, revenueToday, ordersToday, newCustomers, lowStock }`.

### Cột `CreatedDate` (bổ sung so với đề)

Đề yêu cầu đếm “khách hàng **mới** đăng ký”, nhưng bảng `Customers` ban đầu chưa có cột ngày:

| File | Thay đổi |
|---|---|
| `Models/Customer.cs` | thêm `public DateTime CreatedDate { get; set; }` (`datetime2`) |
| `Controllers/CustomersController.cs` – `Create()` | gán `CreatedDate = DateTime.Now` |
| `Migrations/*_AddCustomerCreatedDate.cs` | migration thêm cột |
| `Data/DbInitializer.cs` – `BackfillCustomerCreatedDatesAsync()` (≈ dòng 104) | rải ngày đăng ký cho 30 khách mẫu trong ~20 ngày gần nhất để thẻ KPI có số liệu |

### Phía WinForms — `MiniSupermarket.WinForms/FormQuickReport.cs`

| Vị trí | Nội dung |
|---|---|
| `LoadReportAsync()` ≈ dòng 130 | `GetFromJsonAsync<DashboardSummaryDto>("reports/dashboard-summary")` |
| dòng ≈ 136–140 | gán `lblVal1..lblVal4` |

| Thẻ | Label | Dữ liệu |
|---|---|---|
| DOANH THU HÔM NAY | `lblVal1` | `RevenueToday` (VNĐ) |
| TỔNG HÓA ĐƠN | `lblVal2` | `OrdersToday` |
| KHÁCH HÀNG MỚI | `lblVal3` | `NewCustomers` |
| MẶT HÀNG SẮP HẾT | `lblVal4` | `LowStock` (tồn ≤ 10) |

Thiết kế 4 thẻ: `FormQuickReport.Designer.cs` — `pnlCard1 … pnlCard4`, mỗi thẻ có `bar1..bar4` (vạch màu trái), `lblCap*` (tiêu đề), `lblVal*` (số lớn 19pt), `lblSub*` (chú thích).

**Kiểm chứng:** mở Báo cáo → 4 thẻ hiện `1.290.000 đ / 7 / 2 / 4`; tạo thêm 1 khách hàng mới rồi bấm Xem → thẻ “Khách hàng mới” tăng lên 3.

---

## 45. Biểu đồ Cột Doanh thu theo ngày / tuần / tháng (Bar Chart)

**Yêu cầu:** Chọn **Từ ngày → Đến ngày**; API dùng **Stored Procedure** gom nhóm theo ngày; WinForms vẽ bằng control **`System.Windows.Forms.DataVisualization.Charting.Chart`** với `Series ChartType = Column`.

### 1) Stored Procedure — `Database/sp_GetRevenueByDay.sql`

```sql
CREATE PROCEDURE dbo.sp_GetRevenueByDay
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  CAST(o.OrderDate AS DATE)     AS OrderDay,
            COUNT(o.OrderId)              AS TotalOrders,
            ISNULL(SUM(o.TotalAmount), 0) AS TotalRevenue
    FROM    Orders AS o
    WHERE   CAST(o.OrderDate AS DATE) >= @FromDate
      AND   CAST(o.OrderDate AS DATE) <= @ToDate
    GROUP BY CAST(o.OrderDate AS DATE)
    ORDER BY OrderDay;
END
```

> **Điều chỉnh tương đương:** đề ghi `GROUP BY CAST(CreatedDate AS DATE)`; CSDL của đề tài dùng cột `Orders.OrderDate` (thời điểm lập hoá đơn) nên SP gom nhóm theo `OrderDate`.

### 2) API — `MiniSupermarket.API/Controllers/ReportsController.cs`

| Vị trí | Nội dung |
|---|---|
| `[HttpGet("revenue-by-day")]`<br>`RevenueByDay(from, to)` ≈ dòng 302 | `CommandType = CommandType.StoredProcedure`, `AddWithValue("@FromDate"/"@ToDate")`, `SqlDataReader` + `ReadAsync()` |

Trả về `{ success, fromDate, toDate, totalRevenue, data: [{ date, orders, revenue }] }`.
(`DailyRevenueDto` — `DTOs/Dtos.cs`)

### 3) WinForms — `MiniSupermarket.WinForms/FormQuickReport.cs` + `.Designer.cs`

| Vị trí | Nội dung |
|---|---|
| Designer – khai báo `chartRevenue = new Chart()` | `ChartArea("main")`, `Title`, `Series("DoanhThu")` với `ChartType = SeriesChartType.Column`, `Color = (79,70,229)` |
| Designer | `dtpFrom` (**Từ ngày**), `dtpTo` (**Đến ngày**), `btnRunReport` (**Xem**), `cboGroupBy` (**Nhóm theo: Ngày/Tuần/Tháng**) |
| `FormQuickReport.InitDateRange()` | mặc định `Đến ngày = hôm nay`, `Từ ngày = hôm nay − 13` |
| `LoadReportAsync()` ≈ dòng 141–145 | gọi `reports/revenue-by-day?from=&to=` → `_daily` → `DrawChart()` |
| `DrawChart()` ≈ dòng 187 | `series.Points.Clear()` → `Points.AddXY(nhãn, doanh thu)`; tiêu đề đổi theo chế độ |
| `cboGroupBy_SelectedIndexChanged()` ≈ dòng 113 | đổi Ngày/Tuần/Tháng → chỉ vẽ lại biểu đồ, không gọi API |

Cách gom nhóm khi chọn Tuần / Tháng (`DrawChart()`):

```csharp
if (mode == "Theo tuần")
    points = _daily.GroupBy(d => WeekKey(d.Date))            // ISOWeek.GetWeekOfYear
                   .OrderBy(g => g.Key.SortKey)
                   .Select(g => ($"Tuần {g.Key.Week}", g.Sum(x => x.Revenue)))
                   .ToList();
else if (mode == "Theo tháng")
    points = _daily.GroupBy(d => d.Date.Substring(0, 7))     // yyyy-MM
                   .OrderBy(g => g.Key)
                   .Select(g => ($"Tháng {g.Key.Substring(5,2)}/{g.Key.Substring(0,4)}", g.Sum(x => x.Revenue)))
                   .ToList();
```

### 4) Gói NuGet cần thiết (project WinForms)

| Gói | Vì sao |
|---|---|
| `System.Windows.Forms.DataVisualization` (1.0.0-prerelease.20110.1) | cung cấp control `Chart` cho .NET 8 |
| `System.Data.SqlClient` (4.9.1) | assembly mà control `Chart` cần để khởi tạo (thiếu → lỗi "Could not load file or assembly") |

**Kiểm chứng:** Báo cáo → biểu đồ cột 14 ngày; đổi “Nhóm theo” sang Tuần → cột gộp thành “Tuần 43, 44…”. Kiểm tra API trả 14 phần tử, tổng khớp `grandTotal`.

---

## 46. Thống kê Top 5 Sản phẩm Bán chạy nhất (Best-Sellers)

**Yêu cầu:** `LINQ GroupBy(od => od.ProductId)` → `OrderByDescending(Sum Quantity)` → `Take(5)`.

### 1) API — `MiniSupermarket.API/Controllers/ReportsController.cs`

| Vị trí | Nội dung |
|---|---|
| `[HttpGet("top-products")]`<br>`TopProducts(date, top = 5)` ≈ dòng 367 | tham số `top` mặc định **5**, chặn `1..20` |

```csharp
var grouped = await _context.OrderDetails
    .AsNoTracking()
    .Where(d => d.Order!.OrderDate >= day && d.Order.OrderDate < nextDay)
    .GroupBy(d => d.ProductId)                       // ← GroupBy
    .Select(g => new
    {
        ProductId = g.Key,
        Quantity  = g.Sum(x => x.Quantity),
        Revenue   = g.Sum(x => x.LineTotal)
    })
    .OrderByDescending(x => x.Quantity)              // ← giảm dần theo số lượng
    .Take(top)                                       // ← Take(5)
    .ToListAsync();

// lấy tên sản phẩm (tránh join phức tạp trong GroupBy)
var names = await _context.Products.AsNoTracking()
    .Where(p => ids.Contains(p.ProductId))
    .ToDictionaryAsync(p => p.ProductId, p => p.ProductName);
```

Trả về `{ success, date, top, data: [{ productId, productName, quantity, revenue }] }`.

### 2) WinForms — `MiniSupermarket.WinForms/FormQuickReport.cs`

| Vị trí | Nội dung |
|---|---|
| `LoadReportAsync()` ≈ dòng 149 | `GetFromJsonAsync<TopProductsResult>($"reports/top-products?date={to}&top=5")` |
| dòng ≈ 152–164 | đổ vào `dgvTopProducts` theo thứ hạng 1..5, dòng hạng 1 **in đậm** |
| Designer | `lblTopTitle` = "🏆 TOP 5 SẢN PHẨM BÁN CHẠY NHẤT"; 4 cột: `#`, Sản phẩm, SL, Doanh thu |

> Nghiệp vụ “giúp thủ kho chủ động nhập hàng” được nối với tiêu chí **43**: danh sách Top 5 cho biết mặt hàng bán nhanh, kết hợp cột tồn ≤ 10 (tô đỏ) ở màn hình Sản phẩm để quyết định nhập thêm.

**Kiểm chứng:** API trả đúng 5 phần tử, `quantity` giảm dần; bảng trên form hiển thị 5 dòng có hạng 1–5.

---

## 47. Đổi Điểm Thưởng Trừ Tiền Hoá Đơn (Loyalty Points Redemption)

**Yêu cầu:** Khách có `RewardPoints`; thu ngân chọn dùng điểm (ví dụ "50 điểm trừ 50.000 đ"); cập nhật **trừ điểm** và **trừ tiền `FinalAmount`** trong **cùng một transaction**.

### 1) API — `MiniSupermarket.API/Controllers/OrdersController.cs`

| Vị trí | Nội dung |
|---|---|
| `CheckoutRequest` ≈ dòng 178 | thêm `int? CustomerId` và `public int RedeemPoints { get; set; }` |
| `Checkout()` ≈ dòng 71–175 | toàn bộ nằm trong `using var transaction = await _context.Database.BeginTransactionAsync();` |

Luồng xử lý:

```csharp
// 1) Kiểm tra điểm
int redeemPoints = Math.Max(0, request.RedeemPoints);
if (redeemPoints > 0)
{
    if (customer == null)                 return BadRequest(new { message = "Chưa xác định được khách hàng nên không thể dùng điểm thưởng!" });
    if (customer.RewardPoints < redeemPoints)
        return BadRequest(new { message = $"Khách hàng chỉ có {customer.RewardPoints} điểm, không đủ {redeemPoints} điểm để trừ!" });
}

// 2) Trong transaction
decimal discount   = redeemPoints * 1000m;          // 1 điểm = 1.000 đ
if (discount > total) discount = total;             // không cho âm tiền
decimal finalAmount = total - discount;

order.TotalAmount = finalAmount;                    // ← FinalAmount lưu vào hoá đơn
_context.Orders.Add(order);

// 3) Cập nhật điểm TRONG CÙNG transaction
int earnedPoints = (int)(finalAmount / 1000);       // 1 điểm / 1.000 đ
customer.RewardPoints = customer.RewardPoints - redeemPoints + earnedPoints;

await _context.SaveChangesAsync();
await transaction.CommitAsync();
// catch → await transaction.RollbackAsync()  → trả 400
```

Response: `{ success, orderId, orderCode, totalAmount, discount, finalAmount, redeemPoints, earnedPoints, customerName, rewardPoints }`.

> Bản ADO.NET thuần (`CheckoutController.ProcessCheckout`, `SqlTransaction`) phục vụ **tiêu chí 35**; bản dùng cho POS là `OrdersController.Checkout` (EF Core + `BeginTransactionAsync`). Cả hai đều đảm bảo “lưu hoá đơn + trừ kho + trừ/cộng điểm” trong 1 transaction.

### 2) WinForms — `MiniSupermarket.WinForms/FormPOS.cs` + `.Designer.cs`

| Vị trí | Nội dung |
|---|---|
| Designer ≈ dòng 208–227 | `lblRedeem` (Điểm khả dụng), `nudRedeemPoints` (số điểm), `btnUseMaxPoints` ("Dùng tối đa"), `lblDiscount` (Giảm từ điểm) |
| Designer dòng 140 | `txtCustomerPhone.Leave += txtCustomerPhone_Leave;` |
| `txtCustomerPhone_Leave()` / `LookupCustomerAsync()` ≈ dòng 123–170 | `GET api/customers/search?keyword={sđt}` → hiện tên + hạng thẻ + điểm khả dụng, đặt `nudRedeemPoints.Maximum = điểm khả dụng` |
| `RedeemDiscount` (dòng 15) | `Math.Min(nudRedeemPoints.Value * 1000m, CartTotal)` |
| `nudRedeemPoints_ValueChanged()` (dòng 101) | đổi số điểm → `UpdateCartDisplay()` tính lại số phải trả |
| `btnUseMaxPoints_Click()` (dòng 106) | chọn số điểm tối đa dùng được = `min(điểm có, tổng tiền / 1.000)` |
| `btnCheckout_Click()` (≈ 190–240) | gửi `RedeemPoints`, `CustomerId`; hiện hoá đơn chi tiết (tổng tiền, giảm giá, phải trả, điểm còn lại) |

**Kiểm chứng (đã chạy thực tế):** khách `0901122334` có 253 điểm; mua 2 × 15.000 = 30.000 đ, dùng 10 điểm:
```
totalAmount = 30.000 | discount = 10.000 | finalAmount = 20.000
điểm sau = 253 − 10 + 20 = 263
```
Nhập 999.999 điểm → API trả **400** "Khách hàng chỉ có 263 điểm…".

---

## 48. Xuất Báo cáo Danh mục & Hoá đơn ra Excel/CSV

**Yêu cầu:** Nút "Xuất Excel" tải danh sách mặt hàng kèm tồn kho, hoặc danh sách doanh thu, ra `.xlsx`/`.csv`. Cho phép **xuất CSV trực tiếp phía WinForms**.

### 1) Danh mục sản phẩm — `MiniSupermarket.WinForms/FormProductManagement.cs`

| Vị trí | Nội dung |
|---|---|
| Designer dòng 373 | `btnExportCsv.Click += btnExportCsv_Click;` — nút "📤 Xuất Excel (CSV)" trên thanh phân trang |
| `btnExportCsv_Click()` ≈ dòng 334 | `GET api/products` (lấy **toàn bộ** 60 SP) → `SaveFileDialog` → ghi CSV |
| `CsvCell()` | bọc ô trong `"…"` nếu chứa dấu phẩy / nháy kép / xuống dòng |

Cột xuất: `Mã SP, Mã vạch, Tên sản phẩm, Đơn giá (đ), Tồn kho, Nhóm hàng, Cảnh báo`
(cột **Cảnh báo** = `"Sắp hết hàng"` khi `StockQuantity <= 10` — nối tiếp tiêu chí 43).

### 2) Báo cáo doanh thu — `MiniSupermarket.WinForms/FormQuickReport.cs`

| Vị trí | Nội dung |
|---|---|
| Designer dòng 174 | `btnExport.Click += btnExport_Click;` — nút "📤 Xuất Excel (CSV)" |
| `btnExport_Click()` ≈ dòng 239 | `SaveFileDialog` → ghi **4 phần** vào 1 file CSV |

Nội dung file xuất:
1. **CHỈ SỐ TỔNG QUAN** — doanh thu hôm nay, số hoá đơn, khách mới, mặt hàng sắp hết
2. **DOANH THU THEO NGÀY** — Ngày, Số hoá đơn, Doanh thu + dòng TỔNG CỘNG
3. **TOP 5 SẢN PHẨM BÁN CHẠY NHẤT** — Hạng, Sản phẩm, Số lượng, Doanh thu
4. **DOANH THU THEO THU NGÂN** — Thu ngân, Số hoá đơn, Doanh thu

### 3) Chi tiết kỹ thuật đáng chú ý

```csharp
// UTF8Encoding(true) = ghi kèm BOM → Excel mở CSV nhận đúng bảng mã tiếng Việt
File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));
```

- Dùng `SaveFileDialog` với filter `Tệp CSV (mở bằng Excel)|*.csv`, tên file tự sinh theo ngày giờ (`DanhSachSanPham_yyyyMMdd_HHmm.csv`, `BaoCaoDoanhThu_yyyyMMdd_HHmm.csv`).
- Toàn bộ trong `try/catch` → lỗi ghi file hiện MessageBox tiếng Việt.
- **Không dùng ClosedXML/EPPlus** (tránh thêm phụ thuộc nặng); đề cho phép “xuất chuỗi CSV trực tiếp phía WinForms”.
- File CSV mở trực tiếp bằng Excel, giữ đúng dấu tiếng Việt (Nguyễn, Đỗ, Hồ…).

**Kiểm chứng:** bấm nút → chọn nơi lưu → mở bằng Excel thấy đủ 60 dòng sản phẩm với cột tồn kho; file báo cáo có 4 khối số liệu.

---

## PHỤ LỤC A — Các file/thành phần được thêm cho tiêu chí 36–48

| Loại | Đường dẫn | Dùng cho |
|---|---|---|
| Controller (mới) | `MiniSupermarket.API/Controllers/ReportsController.cs` → `DashboardSummary`, `RevenueByDay`, `TopProducts` | 44, 45, 46 |
| Stored Procedure | `Database/sp_GetRevenueByDay.sql` | 45 |
| Data | `MiniSupermarket.API/Data/DbInitializer.cs` → `SeedDemoOrdersAsync`, `BackfillCustomerCreatedDatesAsync` | dữ liệu cho 44, 45 |
| DTO | `MiniSupermarket.API/DTOs/Dtos.cs` → `TopProductDto`, `DailyRevenueDto`, `DashboardDto` | 44, 45, 46 |
| DTO (client) | `MiniSupermarket.WinForms/Dtos.cs` → `DashboardSummaryDto`, `RevenueByDayResult`, `TopProductsResult`, `CheckoutResultDto`, `ApiMessageDto` | 44, 45, 46, 47 |
| Form (viết lại) | `MiniSupermarket.WinForms/FormQuickReport.cs` + `.Designer.cs` | 44, 45, 46, 48 |
| Form (bổ sung) | `MiniSupermarket.WinForms/FormPOS.cs` + `.Designer.cs` | 47 |
| Form (bổ sung) | `MiniSupermarket.WinForms/FormProductManagement.cs` + `.Designer.cs` | 43, 48 |

## PHỤ LỤC B — Migration & gói NuGet

**Migration (theo thứ tự áp dụng):**

| Migration | Nội dung |
|---|---|
| `HashUserPasswords` | chuyển mật khẩu sang chuỗi băm PBKDF2 |
| `ExpandMocGardenData` | 12 nhóm hàng, 60 sản phẩm, 30 khách hàng |
| `AddCustomerCreatedDate` | thêm cột `Customers.CreatedDate` (phục vụ KPI — tiêu chí 44) |
| `SeedLowStockProducts` | hạ tồn kho 4 sản phẩm ≤ 10 (phục vụ tiêu chí 43, 44) |

**Gói NuGet thêm vào project WinForms:**

| Gói | Dùng cho |
|---|---|
| `System.Windows.Forms.DataVisualization` 1.0.0-prerelease.20110.1 | control `Chart` — tiêu chí 45 |
| `System.Data.SqlClient` 4.9.1 | assembly mà `Chart` yêu cầu khi khởi tạo — tiêu chí 45 |

## PHỤ LỤC C — Bảng API dùng cho tiêu chí 36–48

| # | Method & Route | Quyền |
|---|---|---|
| 44 | `GET api/reports/dashboard-summary` | Admin |
| 45 | `GET api/reports/revenue-by-day?from=&to=` | Admin |
| 46 | `GET api/reports/top-products?date=&top=5` | Admin |
| 47 | `POST api/orders/checkout` (body có `redeemPoints`) | Đã đăng nhập |
| 48 | `GET api/products` (client dùng để xuất CSV) | Đã đăng nhập |
| 38 | `POST api/auth/login` | Công khai |
| 39 | `GET api/users`, `GET api/reports/*` (kiểm tra 403) | Admin |

## PHỤ LỤC D — Kết quả kiểm thử tự động (script PowerShell)

```
Bộ 1 (tiêu chí 11–35 + mở rộng)      : 64 PASS / 0 FAIL
Bộ 2 (tiêu chí 44, 45, 46, 47)        : 13 PASS / 0 FAIL
Build toàn solution                   : 0 Warning / 0 Error
```

Các điểm đã kiểm chứng thực tế trên giao diện:
- Sản phẩm: tồn ≤ 10 tô nền `LightCoral`, chữ đậm (trang 5–6).
- Báo cáo: 4 thẻ KPI hiển thị số liệu thật; biểu đồ cột 14 ngày; Top 5 đúng thứ hạng.
- POS: nhập SĐT → hiện điểm; "Dùng tối đa"; hoá đơn ghi rõ giảm giá và điểm còn lại.
