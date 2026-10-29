> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
> **Buổi thực hành:** Buổi 1 - Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)
Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:
* **`MiniSupermarket.API` (Backend):** Dự án ASP.NET Core Web API chịu trách nhiệm xử lý logic nghiệp vụ, quản lý dữ liệu và cung cấp các RESTful API chuẩn hóa.
* **`MiniSupermarket.WinForms` (Frontend Client):** Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, sử dụng `HttpClient` để gọi dữ liệu từ API qua mạng và hiển thị trực quan lên `DataGridView`.

---

## 🛠️ 2. Công nghệ Sử dụng
* **Ngôn ngữ:** C# (.NET 8.0)
* **Backend:** ASP.NET Core Web API, Controllers, In-Memory Data, LINQ
* **Frontend:** Windows Forms (.NET 8.0), `System.Net.Http.Json`
* **Công cụ kiểm thử:** Swagger UI

---

## 📂 3. Cấu trúc Solution
```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/          # Dự án Web API (Backend)
│   ├── Controllers/              # Chứa CategoriesController (CRUD & Search)
│   ├── Models/                   # Chứa lớp thực thể Category.cs
│   └── Program.cs                # Cấu hình dịch vụ và Middleware
│
└── MiniSupermarket.WinForms/     # Dự án Windows Forms (Frontend Client)
    └── FormCategoryManagement.cs # Giao diện quản lý danh mục CRUD

```
---

# 🛒 Buổi 02: Xác thực & Phân quyền hệ thống với JWT (Web API & WinForms)

Dự án mô phỏng ứng dụng **Quản lý Siêu thị Mini (MiniSupermarket)** kết hợp kiến trúc **RESTful Web API (ASP.NET Core)** và giao diện desktop **WinForms (.NET Framework / .NET Core)**.

Buổi 02 tập trung vào việc cài đặt cơ chế **Xác thực (Authentication)** và **Phân quyền (Authorization)** người dùng sử dụng chuỗi mã hóa **JWT Bearer Token**.

---

## 📚 Nội dung thực hành

### 1. Web API Backend (`MiniSupermarket.API`)
- **Tạo AuthController:** Xây dựng endpoint `POST /api/auth/login` kiểm tra tài khoản, mật khẩu và cấp phát JWT Token.
- **Cấu hình JWT Bearer Authentication:** Khai báo cấu hình `Authentication` và `Authorization` trong `Program.cs`.
- **Cấu hình Swagger UI:** Bổ sung nút **Authorize** hỗ trợ truyền JWT Token trực tiếp trên giao diện Swagger để thử nghiệm API.
- **Phân quyền Endpoint:**
  - Áp thuộc tính `[Authorize]` trên `CategoriesController` để chặn tất cả request không có Token hợp lệ.
  - Thêm endpoint `[Authorize(Roles = "Admin")]` (`GET /api/Categories/admin-dashboard`) dành riêng cho Admin.
  - Thêm endpoint `[Authorize(Roles = "Admin,Cashier")]` (`GET /api/Categories/staff-pos`) dành cho cả Admin và Thu ngân.

### 2. Client Windows Forms (`MiniSupermarket.WinForms`)
- **Quản lý Phiên làm việc (`SessionManager`):** Tạo class tĩnh lưu trữ `JwtToken` và `CurrentRole` trong suốt quá trình chạy ứng dụng.
- **Màn hình Đăng nhập (`FormLogin`):** 
  - Cho phép người dùng nhập Username/Password.
  - Gọi API `auth/login`, nhận JWT Token và lưu vào `SessionManager`.
- **Đính kèm Token vào Request (`FormCategoryManagement`):**
  - Tự động thêm Header `Authorization: Bearer <token>` vào tất cả các yêu cầu HTTP (GET, POST, PUT, DELETE, SEARCH).
- **Khởi chạy ứng dụng (`Program.cs`):** Cấu hình `FormLogin` làm màn hình mặc định ban đầu.

---

## 🛠️ Công nghệ sử dụng
* **Backend:** ASP.NET Core Web API 8.0, `Microsoft.AspNetCore.Authentication.JwtBearer`
* **Frontend:** WinForms (.NET 8.0), `HttpClient`, `System.Net.Http.Json`
* **Công cụ thử nghiệm:** Swagger UI

---

## 🚀 Hướng dẫn chạy & Thử nghiệm

### 1. Khởi chạy Web API
1. Chọn dự án `MiniSupermarket.API` làm Startup Project.
2. Nhấn `F5` hoặc `Ctrl + F5` để khởi chạy.
3. Trang **Swagger UI** xuất hiện tại địa chỉ: `https://localhost:7075/swagger`.


### 2. Thử nghiệm trên WinForms
1. Đảm bảo `MiniSupermarket.API` đang chạy.
2. Mở giải pháp (Solution) và khởi chạy dự án `MiniSupermarket.WinForms`.
3. Đăng nhập bằng tài khoản `admin` hoặc `cashier`.
4. Sau khi đăng nhập thành công, hệ thống tự động mở màn hình **Quản lý danh mục hàng hóa (`FormCategoryManagement`)** và tải dữ liệu từ Web API.

---

## 📝 Đóng góp & Tiến độ
- [x] Tạo API đăng nhập & phát hành mã JWT Token
- [x] Áp dụng `[Authorize]` bảo vệ các API trong CategoriesController
- [x] Thiết kế Form Login trên WinForms
- [x] Lưu trữ Token vào Session và gửi đính kèm HTTP Request Header
- [x] Hoàn thiện tài liệu README cho Buổi 02

---

# 🗄️ Buổi 03: Tích hợp SQL Server và Entity Framework Core Code-First

Thay cơ chế lưu tạm **In-Memory** (Buổi 1 & 2) bằng CSDL **SQL Server**, truy cập qua **EF Core Code-First** với truy vấn bất đồng bộ `async/await` + `LINQ` → dữ liệu lưu **vĩnh viễn**.

**Công nghệ:** EF Core 8.0 (`SqlServer`, `Tools`, `Design`) · SQL Server / LocalDB · WinForms + JWT (Buổi 2) · Swagger, SSMS.

## 📚 Nội dung thực hành
1. **Gói NuGet (Backend):** `Microsoft.EntityFrameworkCore.SqlServer`, `.Tools`, `.Design`.
2. **Entities & DbContext:** `Category` (1-N với `Product`, `[JsonIgnore]`), `Product` (FK `CategoryId`), `Data/SupermarketDbContext.cs` (`DbSet<>` + **Data Seeding** trong `OnModelCreating`).
3. **Chuỗi kết nối + DI:** khai báo trong `appsettings.json`, đăng ký trong `Program.cs`:
   ```csharp
   builder.Services.AddDbContext<SupermarketDbContext>(o => o.UseSqlServer(connectionString));
   ```
   ```json
   "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True"
   ```
   *(SQL Express → `Server=.\SQLEXPRESS`; tài khoản `sa` → `User Id=sa;Password=...`)*
4. **Migrations:** `Add-Migration InitialCreateDatabase` → `Update-Database` (tự tạo CSDL + bảng **Categories**, **Products** + 5 dòng seed).
5. **`CategoriesController`:** tiêm `SupermarketDbContext`, CRUD + search bất đồng bộ (`ToListAsync`, `FindAsync`, `SaveChangesAsync`), giữ `[Authorize]`.
6. **WinForms:** `FormCategoryManagement` đọc/ghi trực tiếp xuống SQL Server; tắt/mở lại vẫn còn dữ liệu.

## 🧩 Bài tập mở rộng: Quản lý Khách hàng (Customers)
- **`Customer.cs`:** `CustomerId` (PK identity), `CustomerName` (nvarchar 100), `PhoneNumber` (varchar 15), `Address` (nullable), `RewardPoints` (=0), `MembershipRank` (="Chuẩn").
- **DbContext:** thêm `DbSet<Customer>` + seed 3 khách hàng (Vàng/Bạc/Chuẩn) → `Add-Migration AddCustomersTable` + `Update-Database`.
- **`CustomersController`:** `GET` all · `GET {id}` · `GET search?keyword=` (theo **tên hoặc SĐT**) · `POST` · `PUT {id}` · `DELETE {id}`.
- **`FormCustomerManagement`:** `dgvCustomers` + ô `txtCustomerId/Name/Phone/Address/RewardPoints/MembershipRank` + nút `btnLoad/Add/Update/Delete/Search`; gọi API bằng `HttpClient` + JWT. Mở từ nút **"Quản lý Khách hàng"** ở `FormCategoryManagement`.

## 🚀 Chạy & kiểm thử
1. **API:** `Update-Database` → F5 → Swagger `https://localhost:7075/swagger` (Authorize bằng token từ `POST /api/auth/login`, `admin`/`123456`).
2. **SSMS:** Server `(localdb)\MSSQLLocalDB`, Windows Authentication, **Encrypt = Optional** → xem `MiniSupermarketDb` (`Categories`, `Products`, `Customers`).
3. **WinForms:** chạy client → đăng nhập → CRUD nhóm hàng & khách hàng → tắt/mở lại để kiểm chứng dữ liệu còn nguyên.

## 📝 Đóng góp & Tiến độ (Buổi 03)
- [x] Gói NuGet EF Core + Entities (`Category`, `Product`) + `SupermarketDbContext` (seed)
- [x] Chuỗi kết nối + DI, Migrations tạo CSDL `MiniSupermarketDb`
- [x] `CategoriesController` sang EF Core + `async/await`
- [x] **Bài tập mở rộng:** `Customer`, `CustomersController`, `FormCustomerManagement`
- [x] Kiểm chứng qua Swagger + SSMS + WinForms

---

# 🖥️ Buổi 04: Kiến trúc giao diện WinForms Shell & Điều hướng phân quyền (Admin / Cashier / Warehouse)

Xây dựng **một khung giao diện duy nhất (Single-Form Architecture)**: thanh điều hướng bên trái (`panelSidebar`) + vùng làm việc (`panelMainContent`), **nhúng động** các form nghiệp vụ vào vùng nội dung và **phân quyền giao diện (RBAC)** theo 3 vai trò.

## 🎯 Mục tiêu
- Thiết kế khung **Sidebar + Top Header + Main Content**.
- Nhúng form con bằng `TopLevel = false`, `FormBorderStyle = None`, `Dock = Fill` (không mở cửa sổ rời rạc).
- Ẩn/hiện nút chức năng theo vai trò và chặn logic **2 lớp**.
- Giao diện **co giãn** theo cửa sổ (`Anchor`/`Dock`), không bị che khi kéo nhỏ/phóng to.

## 🧱 Kiến trúc khung
```text
FormMainShell
├── panelSidebar      (Dock = Left, 230px)  ← Logo + 6 nút chức năng + Đăng xuất
├── panelTopHeader    (Dock = Top, 60px)    ← lblTitle + lblUserInfo
└── panelMainContent  (Dock = Fill)         ← nhúng động Form con
```

## 📚 Nội dung thực hành
1. **FormMainShell:** sidebar tối màu `(24,30,48)`, nút `Flat` + `TextAlign=MiddleLeft`; header trắng; vùng nội dung `(244,245,247)`.
2. **Nạp form động:** `OpenChildForm()` đóng form cũ → `TopLevel=false`, `Dock=Fill` → `panelMainContent.Controls.Add()` → đồng bộ `lblTitle`; `HighlightActiveButton()` tô màu nút đang chọn.
3. **RBAC:** `ApplyRolePermissions()` bật/tắt nút theo vai trò; `OpenDefaultScreenByRole()` mở màn hình mặc định.
4. **Tài khoản mẫu (15):** `admin01/02`, `admin_backup`, `supervisor` = **Admin**; `cashier01–06` = **Cashier**; `ware01–05` = **Warehouse** (mật khẩu `123456`). Đăng nhập kiểm tra bảng `Users` trong CSDL.
5. **Form con:** `FormPOS`, `FormProductManagement`, `FormUserManagement`, `FormQuickReport` + tái sử dụng `FormCategoryManagement`, `FormCustomerManagement`.

## 🔐 Phân quyền
| Vai trò | Nút hiển thị | Màn hình mặc định |
|---|---|---|
| **Admin** | Tất cả 6 nút | Danh mục |
| **Cashier** | POS, Khách hàng | POS |
| **Warehouse** | Danh mục, Sản phẩm | Danh mục |

## 🧩 Bài tập mở rộng
- **FormQuickReport:** `DateTimePicker` + `btnRunReport` + 3 thẻ tóm tắt `lblTotalOrders`, `lblTotalRevenue`, `lblBestSeller` (gọi API `reports/quick`).
- **Bảo vệ 2 lớp:** vừa ẩn nút ở Sidebar, vừa kiểm tra `if (SessionManager.CurrentRole != "Admin") return;` trong `btnReports_Click`.

## 🛠️ Kỹ thuật giao diện responsive
- Panel/lưới dùng `Anchor` (`Top|Bottom|Left|Right`), panel chi tiết dính `Right`, nút chức năng dính `Bottom` → co giãn theo cửa sổ, không bị che.
- Vùng nội dung luôn `Dock = Fill` trong `panelMainContent`.

## 📝 Đóng góp & Tiến độ (Buổi 04)
- [x] FormMainShell (Sidebar + Header + Main Content) & nhúng form động
- [x] RBAC Admin/Cashier/Warehouse + highlight nút + đồng bộ tiêu đề
- [x] 15 tài khoản mẫu, đăng nhập theo bảng `Users`
- [x] Các form con: POS, Sản phẩm, Khách hàng, Danh mục, Tài khoản, Báo cáo
- [x] **Bài tập mở rộng:** FormQuickReport + phân quyền 2 lớp
- [x] Giao diện co giãn (Anchor/Dock), không bị che khi thay đổi kích thước

---

# 🗄️ Buổi 05: ADO.NET nâng cao, Stored Procedure & Giao dịch POS Checkout

Truy cập CSDL bằng **ADO.NET thuần** (không qua EF Core) và bảo vệ nghiệp vụ bán hàng bằng **SqlTransaction** theo chuẩn **ACID**.

## 📚 Nội dung chính
1. **ADO.NET cơ bản:** `SqlConnection` → `SqlCommand` (tham số hóa `SqlParameter`, chống SQL Injection) → `SqlDataReader` (Connected) hoặc `DataTable` + `SqlDataAdapter` (Disconnected). Phân biệt `ExecuteScalar()` / `ExecuteNonQuery()` / `ExecuteReader()`.
2. **API tra mã vạch siêu tốc:** `AdoProductController` đọc trực tiếp bằng `SqlDataReader`, tải danh sách bằng `DataTable`.
3. **Stored Procedure:** `sp_GetRevenueByCashier` (GROUP BY + SUM) → API báo cáo doanh thu theo thu ngân.
4. **ACID & SqlTransaction:** `BeginTransaction()` → `Commit()` / `Rollback()`; mọi `SqlCommand` phải gán `command.Transaction`.
5. **POS Checkout đa bảng:** `Orders` → `OrderDetails` → trừ tồn kho `Products` → tích điểm `Customers`, tất cả trong **một transaction**.

## 🔌 Endpoint mới
| Method | Route | Kỹ thuật |
|---|---|---|
| `GET` | `/api/ado-products/barcode/{barcode}` | `SqlDataReader` (404 nếu không có) |
| `GET` | `/api/ado-products/disconnected-cache` | `DataTable` + `SqlDataAdapter` |
| `GET` | `/api/reports/revenue-by-cashier` | `CommandType.StoredProcedure` |
| `POST` | `/api/checkout` | `SqlTransaction` (commit/rollback) |
| `DELETE` | `/api/checkout/cancel/{id}` | Transaction hoàn kho + trừ điểm |

> **Ghi chú:** Script tạo SP nằm ở `Database/sp_GetRevenueByCashier.sql`. Tên cột thực tế của bảng `Orders` là `OrderDate`, `TotalAmount` (khác tài liệu gốc), nên mã ADO.NET đã được viết khớp CSDL.

## 📝 Đóng góp & Tiến độ (Buổi 05)
- [x] ADO.NET thuần + tham số hóa chống SQL Injection
- [x] `AdoProductController` (tra mã vạch, DataTable cache, lọc tồn kho)
- [x] Stored Procedure `sp_GetRevenueByCashier` + API báo cáo doanh thu
- [x] `CheckoutController` bọc `SqlTransaction` + hủy đơn hoàn kho
- [x] Kiểm chứng: checkout commit, rollback khi thiếu kho, hủy đơn khôi phục tồn kho

