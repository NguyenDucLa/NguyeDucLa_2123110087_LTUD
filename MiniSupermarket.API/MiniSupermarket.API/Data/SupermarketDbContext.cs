using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với CSDL SQL Server
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        // Khai báo các bảng dữ liệu sẽ được ánh xạ vào CSDL
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ tránh lỗi đa đường Cascade trên SQL Server
            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Order).WithMany(o => o.OrderDetails)
                .HasForeignKey(d => d.OrderId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer).WithMany()
                .HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.SetNull);

            // 5 danh mục sản phẩm của Tiệm Rau củ quả sạch Mộc Garden
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Rau ăn lá", Description = "Rau muống, cải ngọt, xà lách, cải bó xôi..." },
                new Category { CategoryId = 2, CategoryName = "Củ - Quả", Description = "Cà rốt, khoai tây, cà chua, hành tây, bí đỏ..." },
                new Category { CategoryId = 3, CategoryName = "Trái cây tươi", Description = "Táo, cam, chuối, xoài, dưa hấu..." },
                new Category { CategoryId = 4, CategoryName = "Nấm & Rau mầm", Description = "Nấm kim châm, nấm đùi gà, rau mầm, giá đỗ" },
                new Category { CategoryId = 5, CategoryName = "Nông sản khô & Gia vị", Description = "Gạo ST25, hạt điều, các loại hạt và gia vị" }
            );

            // Khách hàng thân thiết (địa chỉ đầy đủ nằm gọn trong MỘT cột Address)
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Thị Lan", PhoneNumber = "0901122334", Address = "12 Nguyễn Trãi, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh", RewardPoints = 150, MembershipRank = "Vàng" },
                new Customer { CustomerId = 2, CustomerName = "Trần Văn Hùng", PhoneNumber = "0918877665", Address = "45 Lê Lợi, Phường 4, Quận Gò Vấp, TP. Hồ Chí Minh", RewardPoints = 50, MembershipRank = "Bạc" },
                new Customer { CustomerId = 3, CustomerName = "Lê Thu Hà", PhoneNumber = "0983344556", Address = "78 Trần Hưng Đạo, Phường Cửa Nam, Quận Hoàn Kiếm, TP. Hà Nội", RewardPoints = 10, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 4, CustomerName = "Phạm Minh Quân", PhoneNumber = "0905556677", Address = "23 Nguyễn Văn Cừ, Phường Bồ Đề, Quận Long Biên, TP. Hà Nội", RewardPoints = 80, MembershipRank = "Bạc" },
                new Customer { CustomerId = 5, CustomerName = "Đỗ Thị Mai", PhoneNumber = "0938777888", Address = "150 Điện Biên Phủ, Phường Chính Gián, Quận Thanh Khê, TP. Đà Nẵng", RewardPoints = 220, MembershipRank = "Vàng" }
            );

            // Nạp sẵn 15 tài khoản nhân viên mẫu (3 vai trò: Admin / Cashier / Warehouse)
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin01", Password = "123456", FullName = "Nguyễn Quản Trị", Role = "Admin", IsActive = true },
                new User { UserId = 2, Username = "admin02", Password = "123456", FullName = "Trần Giám Đốc", Role = "Admin", IsActive = true },
                new User { UserId = 3, Username = "cashier01", Password = "123456", FullName = "Lê Thu Ngân", Role = "Cashier", IsActive = true },
                new User { UserId = 4, Username = "cashier02", Password = "123456", FullName = "Phạm Bán Hàng", Role = "Cashier", IsActive = true },
                new User { UserId = 5, Username = "cashier03", Password = "123456", FullName = "Hoàng Thu Ngân", Role = "Cashier", IsActive = true },
                new User { UserId = 6, Username = "cashier04", Password = "123456", FullName = "Vũ Thị Quầy", Role = "Cashier", IsActive = true },
                new User { UserId = 7, Username = "cashier05", Password = "123456", FullName = "Đỗ Bán Lẻ", Role = "Cashier", IsActive = true },
                new User { UserId = 8, Username = "ware01", Password = "123456", FullName = "Ngô Quản Kho", Role = "Warehouse", IsActive = true },
                new User { UserId = 9, Username = "ware02", Password = "123456", FullName = "Bùi Kiểm Kê", Role = "Warehouse", IsActive = true },
                new User { UserId = 10, Username = "ware03", Password = "123456", FullName = "Dương Thủ Kho", Role = "Warehouse", IsActive = true },
                new User { UserId = 11, Username = "ware04", Password = "123456", FullName = "Lý Nhập Hàng", Role = "Warehouse", IsActive = true },
                new User { UserId = 12, Username = "admin_backup", Password = "123456", FullName = "Đặng Hỗ Trợ", Role = "Admin", IsActive = true },
                new User { UserId = 13, Username = "cashier06", Password = "123456", FullName = "Hồ Ca Chiều", Role = "Cashier", IsActive = true },
                new User { UserId = 14, Username = "ware05", Password = "123456", FullName = "Trương Vận Chuyển", Role = "Warehouse", IsActive = true },
                new User { UserId = 15, Username = "supervisor", Password = "123456", FullName = "Mai Giám Sát", Role = "Admin", IsActive = true }
            );

            // 20 sản phẩm rau - củ - quả tươi sạch của Tiệm Mộc Garden
            modelBuilder.Entity<Product>().HasData(
                // --- Nhóm 1: Rau ăn lá ---
                new Product { ProductId = 1, Barcode = "8938000000001", ProductName = "Rau muống hữu cơ 500g", Price = 15000, StockQuantity = 120, CategoryId = 1 },
                new Product { ProductId = 2, Barcode = "8938000000002", ProductName = "Cải ngọt VietGAP 500g", Price = 18000, StockQuantity = 100, CategoryId = 1 },
                new Product { ProductId = 3, Barcode = "8938000000003", ProductName = "Xà lách Romaine 300g", Price = 25000, StockQuantity = 80, CategoryId = 1 },
                new Product { ProductId = 4, Barcode = "8938000000004", ProductName = "Rau cải bó xôi 400g", Price = 22000, StockQuantity = 90, CategoryId = 1 },
                // --- Nhóm 2: Củ - Quả ---
                new Product { ProductId = 5, Barcode = "8938000000005", ProductName = "Cà rốt Đà Lạt 500g", Price = 20000, StockQuantity = 150, CategoryId = 2 },
                new Product { ProductId = 6, Barcode = "8938000000006", ProductName = "Khoai tây Đà Lạt 1kg", Price = 35000, StockQuantity = 130, CategoryId = 2 },
                new Product { ProductId = 7, Barcode = "8938000000007", ProductName = "Cà chua bi 500g", Price = 30000, StockQuantity = 110, CategoryId = 2 },
                new Product { ProductId = 8, Barcode = "8938000000008", ProductName = "Hành tây 1kg", Price = 28000, StockQuantity = 100, CategoryId = 2 },
                new Product { ProductId = 9, Barcode = "8938000000009", ProductName = "Bí đỏ hồ lô 1kg", Price = 25000, StockQuantity = 70, CategoryId = 2 },
                // --- Nhóm 3: Trái cây tươi ---
                new Product { ProductId = 10, Barcode = "8938000000010", ProductName = "Táo Fuji nhập khẩu 1kg", Price = 95000, StockQuantity = 60, CategoryId = 3 },
                new Product { ProductId = 11, Barcode = "8938000000011", ProductName = "Cam sành 1kg", Price = 45000, StockQuantity = 85, CategoryId = 3 },
                new Product { ProductId = 12, Barcode = "8938000000012", ProductName = "Chuối Laba 1kg", Price = 35000, StockQuantity = 95, CategoryId = 3 },
                new Product { ProductId = 13, Barcode = "8938000000013", ProductName = "Xoài Cát Hòa Lộc 1kg", Price = 85000, StockQuantity = 50, CategoryId = 3 },
                new Product { ProductId = 14, Barcode = "8938000000014", ProductName = "Dưa hấu 2kg", Price = 40000, StockQuantity = 65, CategoryId = 3 },
                // --- Nhóm 4: Nấm & Rau mầm ---
                new Product { ProductId = 15, Barcode = "8938000000015", ProductName = "Nấm kim châm 200g", Price = 25000, StockQuantity = 75, CategoryId = 4 },
                new Product { ProductId = 16, Barcode = "8938000000016", ProductName = "Nấm đùi gà 300g", Price = 35000, StockQuantity = 60, CategoryId = 4 },
                new Product { ProductId = 17, Barcode = "8938000000017", ProductName = "Rau mầm cải 150g", Price = 30000, StockQuantity = 90, CategoryId = 4 },
                new Product { ProductId = 18, Barcode = "8938000000018", ProductName = "Giá đỗ 300g", Price = 12000, StockQuantity = 140, CategoryId = 4 },
                // --- Nhóm 5: Nông sản khô & Gia vị ---
                new Product { ProductId = 19, Barcode = "8938000000019", ProductName = "Gạo thơm ST25 1kg", Price = 35000, StockQuantity = 200, CategoryId = 5 },
                new Product { ProductId = 20, Barcode = "8938000000020", ProductName = "Hạt điều rang muối 250g", Price = 120000, StockQuantity = 45, CategoryId = 5 }
            ); 
        }
    }
}