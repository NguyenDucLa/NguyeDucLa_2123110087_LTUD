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

### 2. Dữ liệu tài khoản thử nghiệm
| Username | Password | Role | Quyền hạn |
| :--- | :--- | :--- | :--- |
| **`admin`** | `123456` | `Admin` | Toàn quyền truy cập tất cả API & tính năng |
| **`cashier`** | `123456` | `Cashier` | Truy cập màn hình POS, danh mục hàng hóa |

### 3. Thử nghiệm trên WinForms
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

