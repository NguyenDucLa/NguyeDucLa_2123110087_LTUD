BUỔI 3: TÍCH HỢP SQL SERVER VÀ ENTITY FRAMEWORK CORE CODE-FIRST
1. MỤC TIÊU VÀ BỐI CẢNH THỰC TẾ
Mục tiêu:


Thay thế hoàn toàn cơ chế lưu tạm trên RAM (In-Memory) ở Buổi 1 và Buổi 2 bằng cơ sở dữ liệu quan hệ Microsoft SQL Server.

Làm chủ kỹ thuật Entity Framework Core (EF Core) Code-First: Viết lớp C# trước rồi tự động sinh bảng trong CSDL.

Nắm vững các lệnh EF Core Migrations và truy vấn dữ liệu bất đồng bộ bằng LINQ (async/await).

Bối cảnh thực tế đồ án: Khi nhân viên thu ngân thêm hoặc sửa nhóm hàng trên phần mềm WinForms, dữ liệu phải được lưu trữ vĩnh viễn trong CSDL MiniSupermarketDb của SQL Server, đảm bảo tắt máy hoặc khởi động lại API dữ liệu vẫn còn nguyên vẹn.

2. BƯỚC 1: CÀI ĐẶT CÁC GÓI NUGET PACKAGE CHO BACKEND (MiniSupermarket.API)
Nhấp chuột phải vào project MiniSupermarket.API --> chọn Manage NuGet Packages... --> chuyển sang thẻ Browse và cài đặt lần lượt 3 gói sau:

Microsoft.EntityFrameworkCore.SqlServer: Trình điều khiển kết nối EF Core với hệ quản trị SQL Server.

Microsoft.EntityFrameworkCore.Tools: Bộ công cụ dòng lệnh để chạy các lệnh Migration (Add-Migration, Update-Database).

Microsoft.EntityFrameworkCore.Design: Hỗ trợ môi trường thiết kế và sinh mã cấu trúc cơ sở dữ liệu.

3. BƯỚC 2: ĐỊNH NGHĨA CÁC THỰC THỂ (ENTITIES) VÀ DB CONTEXT
3.1. Cập nhật Model Category.cs và tạo thêm Product.cs
Mở thư mục Models trong project MiniSupermarket.API:

File Category.cs (Bổ sung Data Annotations và quan hệ 1-Nhiều với Sản phẩm):

C#
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models {
    [Table("Categories")] // Đặt tên bảng rõ ràng trong SQL Server
    public class Category {
        [Key] // Khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng (Identity 1,1)
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        // Quan hệ 1 - N: Một danh mục có nhiều sản phẩm
        [JsonIgnore] // Tránh lỗi lặp vòng vô tận khi serialize JSON
        public virtual ICollection<Product>? Products { get; set; }
    }
}


File Product.cs (Tạo mới trong thư mục Models để phục vụ mở rộng siêu thị):

C#
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models {
    [Table("Products")]
    public class Product {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã vạch sản phẩm không được trống")]
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Giá bán

        public int StockQuantity { get; set; } // Số lượng tồn kho

        // Khóa ngoại liên kết tới bảng Categories
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}


3.2. Tạo lớp ngữ cảnh dữ liệu SupermarketDbContext.cs
Tạo thư mục mới tên là Data trong project MiniSupermarket.API, nhấp chuột phải chọn Add --> Class, đặt tên là SupermarketDbContext.cs:

C#
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data {
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    public class SupermarketDbContext : DbContext {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        // Khai báo các bảng dữ liệu ánh xạ từ Model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Nạp sẵn 5 danh mục ban đầu vào SQL Server ngay khi tạo bảng
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo" },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt, nước khoáng, trà" },
                new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, phô mai" },
                new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, phở khô, cháo gói" },
                new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn", Description = "Nước mắm, hạt nêm, dầu thực vật" }
            );
        }
    }
}


4. BƯỚC 3: CẤU HÌNH CHUỖI KẾT NỐI VÀ DEPENDENCY INJECTION
4.1. Khai báo chuỗi kết nối trong appsettings.json
Mở file appsettings.json của MiniSupermarket.API và thêm mục ConnectionStrings:

JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TeoNguyenMiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Secret": "SupermarketSecretKeyDoAnMonHoc2026SecureString!!"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}


Lưu ý: sinh viên thực hành trên PC của phòng máy của trường, kết nối bằng User Id=sa có pasword, thì phải:
-- Câu lệnh đặt đè mật khẩu mới cho tài khoản sa cao cấp trong SSMS, băng cách tạo New Query và thực thi
ALTER LOGIN sa WITH PASSWORD = '123456';
 
  Và trong VS Studio 2022 sử dụng chuỗi kết nối như sau:
-- chuoi ket noi neu ket noi bang user id = sa


"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TenSV-CMS_DB;User Id=sa;Password=123456;MultipleActiveResultSets=true;TrustServerCertificate=True"
}



(Nếu dùng SQL Express thì đổi Server thành Server=.\\SQLEXPRESS;).

4.2. Đăng ký DbContext vào Program.cs
Thêm khai báo dịch vụ Entity Framework vào container trước khi ứng dụng được dựng (builder.Build()):

C#
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

// Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbContext sử dụng SQL Server qua cơ chế Dependency Injection (DI)
builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));


5. BƯỚC 4: THỰC THI MIGRATIONS ĐỂ TẠO DATABASE THỰC TẾ
Mở cửa sổ Package Manager Console trong Visual Studio (Tools --> NuGet Package Manager --> Package Manager Console), đảm bảo ô Default project đang trỏ vào MiniSupermarket.API và gõ lần lượt 2 lệnh:

Tạo mã Migration:
Add-Migration InitialCreateDatabase
(Hệ thống sẽ quét các Model và sinh ra mã tạo bảng trong thư mục Migrations).
Áp dụng vào SQL Server:
Update-Database
(EF Core sẽ kết nối vào SQL Server, tự động tạo CSDL MiniSupermarketDb cùng 2 bảng Categories, Products và nạp sẵn 5 dòng dữ liệu mẫu).
Ghi chú
Xóa cache gói NuGet (nếu vấn đề liên quan đến gói): Mở cửa sổ Terminal hay Power Shell, gõ lệnh
dotnet nuget locals all --clear

6. BƯỚC 5: TÁI CẤU TRÚC CategoriesController.cs SỬ DỤNG EF CORE VÀ ASYNC/AWAIT
Thay thế toàn bộ danh sách tĩnh In-Memory cũ bằng việc tiêm phụ thuộc SupermarketDbContext và truy vấn dữ liệu trực tiếp từ SQL Server:

C#
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CategoriesController(SupermarketDbContext context) {
            _context = context;
        }

        // 1. READ: Lấy toàn bộ danh mục từ SQL Server
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var list = await _context.Categories.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        // 2. READ: Lấy chi tiết 1 danh mục theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) {
                return NotFound(new { message = "Không tìm thấy nhóm hàng trong CSDL!" });
            }
            return Ok(category);
        }

        // 3. SEARCH: Tìm kiếm qua Query String trên SQL Server
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword) {
            if (string.IsNullOrWhiteSpace(keyword)) {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }
            // EF Core dịch biểu thức LINQ thành câu lệnh SQL LIKE tương ứng
            var result = await _context.Categories
                .Where(c => c.CategoryName.Contains(keyword))
                .ToListAsync();
            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng vào Database
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category newCat) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(newCat);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào SQL Server

            return CreatedAtAction(nameof(GetById), new { id = newCat.CategoryId }, newCat);
        }

        // 5. UPDATE: Cập nhật nhóm hàng vào Database
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updateCat) {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }

            cat.CategoryName = updateCat.CategoryName;
            cat.Description = updateCat.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Xóa nhóm hàng khỏi Database
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });
            }

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


7. BƯỚC 6: CHẠY KIỂM CHỨNG VÀ KẾT NỐI TOÀN DIỆN VỚI WINFORMS
7.1. Kiểm thử Backend trên Swagger UI
Nhấn F5 chạy MiniSupermarket.API.

Mở Swagger, chạy thử GET /api/categories: Dữ liệu trả về sẽ lấy trực tiếp từ SQL Server.

Thử POST /api/categories để tạo nhóm hàng mới: CategoryId sẽ do SQL Server tự tăng (Identity).

Mở phần mềm SQL Server Management Studio (SSMS), mở bảng dbo.Categories để đối soát xem dòng mới đã được ghi trực tiếp vào đĩa cứng hay chưa.

7.2. Kiểm thử trên WinForms Client (MiniSupermarket.WinForms)
Khởi chạy MiniSupermarket.WinForms.

Nhấn nút Thêm mới, Cập nhật hoặc Xóa nhóm hàng trên màn hình FormCategoryManagement.

Tắt cả ứng dụng WinForms lẫn Backend API, sau đó mở lại: Dữ liệu vừa thao tác vẫn còn nguyên vẹn (khác hoàn toàn với Buổi 1 khi tắt API là mất sạch dữ liệu trong RAM).

8. KẾT LUẬN VÀ ĐÁNH GIÁ BUỔI THỰC HÀNH 3
8.1. Tổng kết kiến thức và kỹ năng đạt được
Làm chủ ORM hiện đại: Sinh viên hiểu rõ vai trò của Entity Framework Core như một cầu nối thông minh giữa đối tượng C# và cơ sở dữ liệu quan hệ SQL Server.

Thành thạo Migrations: Nắm vững quy trình quản lý phiên bản cơ sở dữ liệu bằng code (Add-Migration, Update-Database), không còn phụ thuộc vào việc viết lệnh CREATE TABLE thủ công.

Tối ưu hóa hiệu năng: Áp dụng thuần thục từ khóa async/await kết hợp với các hàm bất đồng bộ của EF Core (ToListAsync, FindAsync, SaveChangesAsync) giúp hệ thống xử lý mượt mà, không bị nghẽn luồng truy cập.

8.2. Định hướng cho Buổi học tiếp theo (Buổi 4)
Trọng tâm Buổi 4: Phân hệ Sản phẩm & Nghiệp vụ Quản lý Kho Hàng (Products Management).

Nội dung chính:



Xây dựng đầy đủ CRUD cho ProductsController liên kết khóa ngoại với danh mục Category.

Thiết kế màn hình quản lý sản phẩm trên WinForms Client: Đổ danh sách nhóm hàng vào ComboBox, tra cứu sản phẩm theo mã vạch Barcode và lọc sản phẩm theo nhóm hàng.

Tối ưu truy vấn bảng quan hệ bằng kỹ thuật Eager Loading (.Include(...)) trong Entity Framework Core.


Bài tập Thực hành Mở rộng cho Buổi 3 được thiết kế hoàn toàn chuẩn xác, bám sát mô hình 6 bảng cốt lõi đã được thống nhất trong cơ sở dữ liệu mẫu (Users, Categories, Products, Customers, Orders, OrderDetails).

Bài tập này dành cho sinh viên tự thực hành có hướng dẫn và gợi ý từng bước để củng cố kỹ năng với bảng Customers (Quản lý Khách hàng thân thiết).

BÀI TẬP THỰC HÀNH MỞ RỘNG BUỔI 3: XÂY DỰNG PHÂN HỆ QUẢN LÝ KHÁCH HÀNG (CUSTOMERS) VỚI EF CORE
1. MỤC TIÊU VÀ Ý NGHĨA NGHIỆP VỤ
Mục tiêu: Giúp sinh viên tự luyện tập toàn bộ quy trình tích hợp CSDL SQL Server với EF Core Code-First dựa trên bảng Customers nằm trong sơ đồ chuẩn 6 bảng của đồ án.

Ý nghĩa nghiệp vụ: Trong hệ thống siêu thị mini, phân hệ Khách hàng chịu trách nhiệm lưu trữ thông tin thành viên, số điện thoại, hạng thẻ và điểm tích lũy để phục vụ cho các chương trình hậu mãi và thanh toán hóa đơn tại quầy POS.

2. HƯỚNG DẪN VÀ GỢI Ý CÁC BƯỚC THỰC HÀNH CHO SINH VIÊN
Bước 1: Định nghĩa Model thực thể Customer.cs
Gợi ý thao tác: Nhấp chuột phải vào thư mục Models trong project MiniSupermarket.API --> Chọn Add --> Class --> Đặt tên file là Customer.cs.

Cấu trúc trường dữ liệu cần có (Dựa theo chuẩn 6 bảng):



CustomerId (INT, Khóa chính, Tự tăng IDENTITY(1,1)).

CustomerName (NVARCHAR(100), Bắt buộc).

PhoneNumber (VARCHAR(15), Bắt buộc, định danh liên hệ).

Address (NVARCHAR(200), Có thể để trống).

RewardPoints (INT, Giá trị mặc định là 0).

MembershipRank (NVARCHAR(50), Giá trị mặc định là 'Chuẩn').

Bước 2: Cập nhật SupermarketDbContext.cs và Data Seeding
Gợi ý thao tác:



Mở file SupermarketDbContext.cs trong thư mục Data, bổ sung khai báo tập hợp DbSet<Customer> Customers { get; set; }.
Tại phương thức OnModelCreating, thêm dữ liệu mồi (Data Seeding) để nạp sẵn 3 khách hàng mẫu vào cơ sở dữ liệu khi chạy Migration:



Khách hàng 1: CustomerName = "Nguyễn Văn A", PhoneNumber = "0901122334", MembershipRank = "Vàng", RewardPoints = 150.

Khách hàng 2: CustomerName = "Trần Thị B", PhoneNumber = "0918877665", MembershipRank = "Bạc", RewardPoints = 50.

Khách hàng 3: CustomerName = "Lê Văn C", PhoneNumber = "0983344556", MembershipRank = "Chuẩn", RewardPoints = 10.

Bước 3: Thực hiện EF Core Migrations
Gợi ý lệnh thực thi: Mở cửa sổ Package Manager Console trong Visual Studio và chạy tuần tự các lệnh sau:



PowerShell
Add-Migration AddCustomersTable
Update-Database



Kiểm chứng: Mở SQL Server Management Studio (SSMS) để kiểm tra xem bảng Customers đã xuất hiện trong CSDL MiniSupermarketDb cùng 3 dòng dữ liệu mẫu hay chưa.

Bước 4: Xây dựng CustomersController.cs (Backend API)
Gợi ý thao tác: Tạo một Empty API Controller đặt tên là CustomersController.cs trong thư mục Controllers.

Các Endpoint cần hoàn thành (Sử dụng Async/Await và LINQ):



GET /api/customers: Lấy toàn bộ danh sách khách hàng.

GET /api/customers/{id}: Lấy chi tiết thông tin khách hàng theo mã định danh.

GET /api/customers/search?keyword=...: Tìm kiếm khách hàng theo tên hoặc số điện thoại.

POST /api/customers: Thêm mới khách hàng thành viên.

HttpPut /api/customers/{id}: Cập nhật thông tin và hạng thẻ của khách hàng.

DELETE /api/customers/{id}: Xóa tài khoản khách hàng khỏi hệ thống.

Bước 5: Xây dựng Giao diện WinForms Client (FormCustomerManagement)
Gợi ý thiết kế giao diện:



Tạo một Windows Form mới đặt tên là FormCustomerManagement.cs.

Đặt một bảng DataGridView (dgvCustomers) để hiển thị danh sách.

Các ô nhập liệu tương ứng: txtCustomerId, txtCustomerName, txtPhoneNumber, txtAddress, txtRewardPoints, txtMembershipRank.

Các nút chức năng: btnLoad, btnAdd, btnUpdate, btnDelete, btnSearch.

Gợi ý viết code gọi API: Sử dụng HttpClient kết hợp các hàm GetFromJsonAsync, PostAsJsonAsync, PutAsJsonAsync, DeleteAsync trỏ đến đường dẫn cơ sở của API (https://localhost:7123/api/customers) tương tự các bài thực hành trước.


3. TIÊU CHÍ NGHIỆM THU BÀI TẬP (CHECKLIST CHO SINH VIÊN)
[ ] CSDL SQL Server: Bảng Customers hiển thị chính xác các trường dữ liệu theo mô hình 6 bảng chuẩn.

[ ] Backend Web API: Kiểm thử thành công các phương thức CRUD và tìm kiếm trên giao diện Swagger UI.

[ ] Frontend WinForms: Thao tác thêm, sửa, xóa, tìm kiếm thông tin khách hàng thành công trên phần mềm ứng dụng.

[ ] Tính bền vững: Dữ liệu thay đổi trên giao diện WinForms được ghi nhận trực tiếp và lưu trữ vĩnh viễn trên SQL Server.
