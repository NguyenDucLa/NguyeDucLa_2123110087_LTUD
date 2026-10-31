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

            // 12 danh mục sản phẩm của Tiệm Rau củ quả sạch Mộc Garden
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Rau ăn lá", Description = "Rau muống, cải ngọt, xà lách, cải bó xôi..." },
                new Category { CategoryId = 2, CategoryName = "Củ - Quả", Description = "Cà rốt, khoai tây, cà chua, hành tây, bí đỏ..." },
                new Category { CategoryId = 3, CategoryName = "Trái cây tươi", Description = "Táo, cam, chuối, xoài, dưa hấu..." },
                new Category { CategoryId = 4, CategoryName = "Nấm & Rau mầm", Description = "Nấm kim châm, nấm đùi gà, rau mầm, giá đỗ" },
                new Category { CategoryId = 5, CategoryName = "Nông sản khô & Gia vị", Description = "Gạo ST25, hạt điều, các loại hạt và gia vị" },
                new Category { CategoryId = 6, CategoryName = "Rau thơm & Gia vị tươi", Description = "Hành lá, ngò gai, húng quế, sả, ớt, gừng" },
                new Category { CategoryId = 7, CategoryName = "Trái cây nhập khẩu", Description = "Nho Hàn Quốc, cherry Mỹ, kiwi New Zealand..." },
                new Category { CategoryId = 8, CategoryName = "Thực phẩm hữu cơ", Description = "Trứng, sữa, đậu hũ, mật ong, dầu dừa hữu cơ" },
                new Category { CategoryId = 9, CategoryName = "Đồ uống & Nước ép", Description = "Nước ép trái cây, trà matcha, cà phê rang xay" },
                new Category { CategoryId = 10, CategoryName = "Ngũ cốc & Yến mạch", Description = "Yến mạch, gạo lứt, đậu xanh, hạt chia, granola" },
                new Category { CategoryId = 11, CategoryName = "Bơ - Hạt dinh dưỡng", Description = "Bơ sáp, hạnh nhân, óc chó, mắc ca, hạt bí" },
                new Category { CategoryId = 12, CategoryName = "Giỏ quà & Combo", Description = "Combo rau sạch, giỏ quà trái cây, combo nấu lẩu" }
            );

            // Khách hàng thân thiết (địa chỉ đầy đủ nằm gọn trong MỘT cột Address)
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Thị Lan", PhoneNumber = "0901122334", Address = "12 Nguyễn Trãi, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh", RewardPoints = 150, MembershipRank = "Vàng" },
                new Customer { CustomerId = 2, CustomerName = "Trần Văn Hùng", PhoneNumber = "0918877665", Address = "45 Lê Lợi, Phường 4, Quận Gò Vấp, TP. Hồ Chí Minh", RewardPoints = 50, MembershipRank = "Bạc" },
                new Customer { CustomerId = 3, CustomerName = "Lê Thu Hà", PhoneNumber = "0983344556", Address = "78 Trần Hưng Đạo, Phường Cửa Nam, Quận Hoàn Kiếm, TP. Hà Nội", RewardPoints = 10, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 4, CustomerName = "Phạm Minh Quân", PhoneNumber = "0905556677", Address = "23 Nguyễn Văn Cừ, Phường Bồ Đề, Quận Long Biên, TP. Hà Nội", RewardPoints = 80, MembershipRank = "Bạc" },
                new Customer { CustomerId = 5, CustomerName = "Đỗ Thị Mai", PhoneNumber = "0938777888", Address = "150 Điện Biên Phủ, Phường Chính Gián, Quận Thanh Khê, TP. Đà Nẵng", RewardPoints = 220, MembershipRank = "Vàng" },
                new Customer { CustomerId = 6, CustomerName = "Võ Thành Nam", PhoneNumber = "0903667788", Address = "88 Phan Xích Long, Phường 2, Quận Phú Nhuận, TP. Hồ Chí Minh", RewardPoints = 320, MembershipRank = "Kim cương" },
                new Customer { CustomerId = 7, CustomerName = "Bùi Thanh Tuyền", PhoneNumber = "0917888999", Address = "27 Ung Văn Khiêm, Phường 25, Quận Bình Thạnh, TP. Hồ Chí Minh", RewardPoints = 45, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 8, CustomerName = "Hoàng Quốc Bảo", PhoneNumber = "0979223344", Address = "156 Tô Hiến Thành, Phường 13, Quận 10, TP. Hồ Chí Minh", RewardPoints = 190, MembershipRank = "Vàng" },
                new Customer { CustomerId = 9, CustomerName = "Nguyễn Kim Ngân", PhoneNumber = "0908776655", Address = "9 Nguyễn Thị Minh Khai, Phường Đa Kao, Quận 1, TP. Hồ Chí Minh", RewardPoints = 65, MembershipRank = "Bạc" },
                new Customer { CustomerId = 10, CustomerName = "Trịnh Văn Lộc", PhoneNumber = "0932114455", Address = "301 Quang Trung, Phường 10, Quận Gò Vấp, TP. Hồ Chí Minh", RewardPoints = 30, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 11, CustomerName = "Đặng Thùy Dương", PhoneNumber = "0987112233", Address = "74 Hoàng Diệu, Phường 6, Quận 4, TP. Hồ Chí Minh", RewardPoints = 410, MembershipRank = "Kim cương" },
                new Customer { CustomerId = 12, CustomerName = "Lý Hoài Anh", PhoneNumber = "0912667788", Address = "12 Lý Thường Kiệt, Phường Trần Hưng Đạo, Quận Hoàn Kiếm, TP. Hà Nội", RewardPoints = 25, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 13, CustomerName = "Ngô Đức Thịnh", PhoneNumber = "0967334455", Address = "55 Nguyễn Trãi, Phường Thanh Xuân Trung, Quận Thanh Xuân, TP. Hà Nội", RewardPoints = 140, MembershipRank = "Vàng" },
                new Customer { CustomerId = 14, CustomerName = "Phan Thị Bích", PhoneNumber = "0904556677", Address = "18 Trần Duy Hưng, Phường Trung Hòa, Quận Cầu Giấy, TP. Hà Nội", RewardPoints = 70, MembershipRank = "Bạc" },
                new Customer { CustomerId = 15, CustomerName = "Trương Công Định", PhoneNumber = "0945667788", Address = "209 Tây Sơn, Phường Quang Trung, Quận Đống Đa, TP. Hà Nội", RewardPoints = 15, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 16, CustomerName = "Đinh Thị Hồng", PhoneNumber = "0978334499", Address = "46 Mỹ Đình, Phường Mỹ Đình 2, Quận Nam Từ Liêm, TP. Hà Nội", RewardPoints = 260, MembershipRank = "Kim cương" },
                new Customer { CustomerId = 17, CustomerName = "Vũ Minh Trí", PhoneNumber = "0398221144", Address = "33 Lạch Tray, Phường Đằng Giang, Quận Ngô Quyền, TP. Hải Phòng", RewardPoints = 55, MembershipRank = "Bạc" },
                new Customer { CustomerId = 18, CustomerName = "Cao Thị Xuân", PhoneNumber = "0937668877", Address = "120 Hồ Tùng Mậu, Phường Bắc Lý, TP. Đồng Hới, TP. Quảng Bình", RewardPoints = 12, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 19, CustomerName = "Mai Văn Khánh", PhoneNumber = "0905223311", Address = "67 Nguyễn Văn Linh, Phường Nam Dương, Quận Hải Châu, TP. Đà Nẵng", RewardPoints = 175, MembershipRank = "Vàng" },
                new Customer { CustomerId = 20, CustomerName = "Lâm Thanh Hà", PhoneNumber = "0983887766", Address = "25 Bạch Đằng, Phường Thạch Thang, Quận Hải Châu, TP. Đà Nẵng", RewardPoints = 95, MembershipRank = "Bạc" },
                new Customer { CustomerId = 21, CustomerName = "Tạ Quang Huy", PhoneNumber = "0918776655", Address = "14 Trần Phú, Phường Lộc Thọ, TP. Nha Trang, TP. Khánh Hòa", RewardPoints = 40, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 22, CustomerName = "Hồ Ngọc Diệp", PhoneNumber = "0976112233", Address = "8 Nguyễn Tất Thành, Phường Tân Lập, TP. Buôn Ma Thuột, TP. Đắk Lắk", RewardPoints = 130, MembershipRank = "Vàng" },
                new Customer { CustomerId = 23, CustomerName = "Nguyễn Bá Tùng", PhoneNumber = "0932119988", Address = "91 Hùng Vương, Phường Thới Bình, Quận Ninh Kiều, TP. Cần Thơ", RewardPoints = 60, MembershipRank = "Bạc" },
                new Customer { CustomerId = 24, CustomerName = "Trần Mỹ Linh", PhoneNumber = "0903112244", Address = "33 Nguyễn Đình Chiểu, Phường 5, TP. Mỹ Tho, TP. Tiền Giang", RewardPoints = 20, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 25, CustomerName = "Phạm Đức Duy", PhoneNumber = "0967554433", Address = "7 Phạm Văn Đồng, Phường Hiệp Bình Chánh, TP. Thủ Đức, TP. Hồ Chí Minh", RewardPoints = 240, MembershipRank = "Vàng" },
                new Customer { CustomerId = 26, CustomerName = "Lữ Thị Mai", PhoneNumber = "0917443322", Address = "16 Hà Huy Tập, Phường Tân An, TP. Vinh, TP. Nghệ An", RewardPoints = 35, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 27, CustomerName = "Sơn Tùng Vinh", PhoneNumber = "0988114477", Address = "52 Trần Quốc Toản, Phường 8, TP. Đà Lạt, TP. Lâm Đồng", RewardPoints = 110, MembershipRank = "Bạc" },
                new Customer { CustomerId = 28, CustomerName = "Châu Thị Kim", PhoneNumber = "0909224488", Address = "19 Trần Đại Nghĩa, Phường Tân Kiểng, Quận 7, TP. Hồ Chí Minh", RewardPoints = 500, MembershipRank = "Kim cương" },
                new Customer { CustomerId = 29, CustomerName = "Bạch Văn Toàn", PhoneNumber = "0938224411", Address = "63 Quốc lộ 1A, Phường Tân Thới Hiệp, Quận 12, TP. Hồ Chí Minh", RewardPoints = 48, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 30, CustomerName = "Đoàn Thanh Vân", PhoneNumber = "0977778899", Address = "101 Lê Duẩn, Phường Hải Châu 1, Quận Hải Châu, TP. Đà Nẵng", RewardPoints = 205, MembershipRank = "Vàng" }
            );

            // Nạp sẵn 15 tài khoản nhân viên mẫu (3 vai trò: Admin / Cashier / Warehouse)
            // Mật khẩu của TẤT CẢ tài khoản mẫu là "123456" nhưng KHÔNG lưu dạng thô:
            // đây là chuỗi băm PBKDF2 (PasswordHasher - 100.000 vòng, có salt) của "123456".
            const string seedPassword = "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==";
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin01", Password = seedPassword, FullName = "Nguyễn Quản Trị", Role = "Admin", IsActive = true },
                new User { UserId = 2, Username = "admin02", Password = seedPassword, FullName = "Trần Giám Đốc", Role = "Admin", IsActive = true },
                new User { UserId = 3, Username = "cashier01", Password = seedPassword, FullName = "Lê Thu Ngân", Role = "Cashier", IsActive = true },
                new User { UserId = 4, Username = "cashier02", Password = seedPassword, FullName = "Phạm Bán Hàng", Role = "Cashier", IsActive = true },
                new User { UserId = 5, Username = "cashier03", Password = seedPassword, FullName = "Hoàng Thu Ngân", Role = "Cashier", IsActive = true },
                new User { UserId = 6, Username = "cashier04", Password = seedPassword, FullName = "Vũ Thị Quầy", Role = "Cashier", IsActive = true },
                new User { UserId = 7, Username = "cashier05", Password = seedPassword, FullName = "Đỗ Bán Lẻ", Role = "Cashier", IsActive = true },
                new User { UserId = 8, Username = "ware01", Password = seedPassword, FullName = "Ngô Quản Kho", Role = "Warehouse", IsActive = true },
                new User { UserId = 9, Username = "ware02", Password = seedPassword, FullName = "Bùi Kiểm Kê", Role = "Warehouse", IsActive = true },
                new User { UserId = 10, Username = "ware03", Password = seedPassword, FullName = "Dương Thủ Kho", Role = "Warehouse", IsActive = true },
                new User { UserId = 11, Username = "ware04", Password = seedPassword, FullName = "Lý Nhập Hàng", Role = "Warehouse", IsActive = true },
                new User { UserId = 12, Username = "admin_backup", Password = seedPassword, FullName = "Đặng Hỗ Trợ", Role = "Admin", IsActive = true },
                new User { UserId = 13, Username = "cashier06", Password = seedPassword, FullName = "Hồ Ca Chiều", Role = "Cashier", IsActive = true },
                new User { UserId = 14, Username = "ware05", Password = seedPassword, FullName = "Trương Vận Chuyển", Role = "Warehouse", IsActive = true },
                new User { UserId = 15, Username = "supervisor", Password = seedPassword, FullName = "Mai Giám Sát", Role = "Admin", IsActive = true }
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
                new Product { ProductId = 20, Barcode = "8938000000020", ProductName = "Hạt điều rang muối 250g", Price = 120000, StockQuantity = 45, CategoryId = 5 },
                // --- Nhóm 6: Rau thơm & Gia vị tươi ---
                new Product { ProductId = 21, Barcode = "8938000000021", ProductName = "Hành lá 200g", Price = 8000, StockQuantity = 120, CategoryId = 6 },
                new Product { ProductId = 22, Barcode = "8938000000022", ProductName = "Ngò gai 100g", Price = 7000, StockQuantity = 100, CategoryId = 6 },
                new Product { ProductId = 23, Barcode = "8938000000023", ProductName = "Húng quế 100g", Price = 7000, StockQuantity = 95, CategoryId = 6 },
                new Product { ProductId = 24, Barcode = "8938000000024", ProductName = "Sả tươi 300g", Price = 15000, StockQuantity = 80, CategoryId = 6 },
                new Product { ProductId = 25, Barcode = "8938000000025", ProductName = "Ớt hiểm 100g", Price = 12000, StockQuantity = 70, CategoryId = 6 },
                new Product { ProductId = 26, Barcode = "8938000000026", ProductName = "Gừng tươi 250g", Price = 18000, StockQuantity = 60, CategoryId = 6 },
                // --- Nhóm 7: Trái cây nhập khẩu ---
                new Product { ProductId = 27, Barcode = "8938000000027", ProductName = "Nho mẫu đơn Hàn Quốc 500g", Price = 189000, StockQuantity = 35, CategoryId = 7 },
                new Product { ProductId = 28, Barcode = "8938000000028", ProductName = "Cherry Mỹ 500g", Price = 165000, StockQuantity = 40, CategoryId = 7 },
                new Product { ProductId = 29, Barcode = "8938000000029", ProductName = "Kiwi New Zealand 6 trái", Price = 98000, StockQuantity = 45, CategoryId = 7 },
                new Product { ProductId = 30, Barcode = "8938000000030", ProductName = "Lê Hàn Quốc 1kg", Price = 89000, StockQuantity = 50, CategoryId = 7 },
                new Product { ProductId = 31, Barcode = "8938000000031", ProductName = "Táo Envy Mỹ 1kg", Price = 125000, StockQuantity = 42, CategoryId = 7 },
                new Product { ProductId = 32, Barcode = "8938000000032", ProductName = "Cam Úc 1kg", Price = 79000, StockQuantity = 55, CategoryId = 7 },
                // --- Nhóm 8: Thực phẩm hữu cơ ---
                new Product { ProductId = 33, Barcode = "8938000000033", ProductName = "Trứng gà hữu cơ 10 quả", Price = 55000, StockQuantity = 70, CategoryId = 8 },
                new Product { ProductId = 34, Barcode = "8938000000034", ProductName = "Sữa tươi hữu cơ 1L", Price = 62000, StockQuantity = 65, CategoryId = 8 },
                new Product { ProductId = 35, Barcode = "8938000000035", ProductName = "Đậu hũ non hữu cơ 300g", Price = 22000, StockQuantity = 55, CategoryId = 8 },
                new Product { ProductId = 36, Barcode = "8938000000036", ProductName = "Bún gạo lứt hữu cơ 500g", Price = 38000, StockQuantity = 48, CategoryId = 8 },
                new Product { ProductId = 37, Barcode = "8938000000037", ProductName = "Mật ong rừng nguyên chất 500ml", Price = 210000, StockQuantity = 30, CategoryId = 8 },
                new Product { ProductId = 38, Barcode = "8938000000038", ProductName = "Dầu dừa ép lạnh 250ml", Price = 95000, StockQuantity = 40, CategoryId = 8 },
                // --- Nhóm 9: Đồ uống & Nước ép ---
                new Product { ProductId = 39, Barcode = "8938000000039", ProductName = "Nước ép cam nguyên chất 1L", Price = 68000, StockQuantity = 50, CategoryId = 9 },
                new Product { ProductId = 40, Barcode = "8938000000040", ProductName = "Nước ép cần tây 500ml", Price = 45000, StockQuantity = 45, CategoryId = 9 },
                new Product { ProductId = 41, Barcode = "8938000000041", ProductName = "Trà xanh matcha 200g", Price = 120000, StockQuantity = 35, CategoryId = 9 },
                new Product { ProductId = 42, Barcode = "8938000000042", ProductName = "Cà phê rang xay 250g", Price = 110000, StockQuantity = 40, CategoryId = 9 },
                new Product { ProductId = 43, Barcode = "8938000000043", ProductName = "Nước dừa tươi 1L", Price = 35000, StockQuantity = 60, CategoryId = 9 },
                new Product { ProductId = 44, Barcode = "8938000000044", ProductName = "Sữa hạt điều 1L", Price = 75000, StockQuantity = 38, CategoryId = 9 },
                // --- Nhóm 10: Ngũ cốc & Yến mạch ---
                new Product { ProductId = 45, Barcode = "8938000000045", ProductName = "Yến mạch cán dẹt 1kg", Price = 89000, StockQuantity = 55, CategoryId = 10 },
                new Product { ProductId = 46, Barcode = "8938000000046", ProductName = "Gạo lứt huyết rồng 1kg", Price = 48000, StockQuantity = 90, CategoryId = 10 },
                new Product { ProductId = 47, Barcode = "8938000000047", ProductName = "Đậu xanh cà vỏ 500g", Price = 32000, StockQuantity = 75, CategoryId = 10 },
                new Product { ProductId = 48, Barcode = "8938000000048", ProductName = "Hạt chia Úc 500g", Price = 145000, StockQuantity = 42, CategoryId = 10 },
                new Product { ProductId = 49, Barcode = "8938000000049", ProductName = "Hạt lanh vàng 500g", Price = 85000, StockQuantity = 36, CategoryId = 10 },
                new Product { ProductId = 50, Barcode = "8938000000050", ProductName = "Ngũ cốc granola 500g", Price = 135000, StockQuantity = 6, CategoryId = 10 },
                // --- Nhóm 11: Bơ - Hạt dinh dưỡng ---
                new Product { ProductId = 51, Barcode = "8938000000051", ProductName = "Bơ sáp Đắk Lắk 1kg", Price = 65000, StockQuantity = 48, CategoryId = 11 },
                new Product { ProductId = 52, Barcode = "8938000000052", ProductName = "Hạnh nhân rang 250g", Price = 135000, StockQuantity = 40, CategoryId = 11 },
                new Product { ProductId = 53, Barcode = "8938000000053", ProductName = "Óc chó Mỹ 250g", Price = 155000, StockQuantity = 33, CategoryId = 11 },
                new Product { ProductId = 54, Barcode = "8938000000054", ProductName = "Hạt mắc ca 250g", Price = 175000, StockQuantity = 8, CategoryId = 11 },
                new Product { ProductId = 55, Barcode = "8938000000055", ProductName = "Hạt bí rang 250g", Price = 78000, StockQuantity = 45, CategoryId = 11 },
                new Product { ProductId = 56, Barcode = "8938000000056", ProductName = "Đậu phộng rang 500g", Price = 55000, StockQuantity = 60, CategoryId = 11 },
                // --- Nhóm 12: Giỏ quà & Combo ---
                new Product { ProductId = 57, Barcode = "8938000000057", ProductName = "Combo rau sạch 5 loại", Price = 99000, StockQuantity = 25, CategoryId = 12 },
                new Product { ProductId = 58, Barcode = "8938000000058", ProductName = "Giỏ quà trái cây cao cấp", Price = 450000, StockQuantity = 5, CategoryId = 12 },
                new Product { ProductId = 59, Barcode = "8938000000059", ProductName = "Combo ăn sáng dinh dưỡng", Price = 189000, StockQuantity = 20, CategoryId = 12 },
                new Product { ProductId = 60, Barcode = "8938000000060", ProductName = "Combo nấu lẩu rau củ 4 người", Price = 229000, StockQuantity = 9, CategoryId = 12 }
            ); 
        }
    }
}