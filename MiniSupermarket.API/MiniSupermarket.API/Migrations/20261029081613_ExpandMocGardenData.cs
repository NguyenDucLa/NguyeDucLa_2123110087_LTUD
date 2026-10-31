using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class ExpandMocGardenData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 6, "Rau thơm & Gia vị tươi", "Hành lá, ngò gai, húng quế, sả, ớt, gừng" },
                    { 7, "Trái cây nhập khẩu", "Nho Hàn Quốc, cherry Mỹ, kiwi New Zealand..." },
                    { 8, "Thực phẩm hữu cơ", "Trứng, sữa, đậu hũ, mật ong, dầu dừa hữu cơ" },
                    { 9, "Đồ uống & Nước ép", "Nước ép trái cây, trà matcha, cà phê rang xay" },
                    { 10, "Ngũ cốc & Yến mạch", "Yến mạch, gạo lứt, đậu xanh, hạt chia, granola" },
                    { 11, "Bơ - Hạt dinh dưỡng", "Bơ sáp, hạnh nhân, óc chó, mắc ca, hạt bí" },
                    { 12, "Giỏ quà & Combo", "Combo rau sạch, giỏ quà trái cây, combo nấu lẩu" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 6, "88 Phan Xích Long, Phường 2, Quận Phú Nhuận, TP. Hồ Chí Minh", "Võ Thành Nam", "Kim cương", "0903667788", 320 },
                    { 7, "27 Ung Văn Khiêm, Phường 25, Quận Bình Thạnh, TP. Hồ Chí Minh", "Bùi Thanh Tuyền", "Chuẩn", "0917888999", 45 },
                    { 8, "156 Tô Hiến Thành, Phường 13, Quận 10, TP. Hồ Chí Minh", "Hoàng Quốc Bảo", "Vàng", "0979223344", 190 },
                    { 9, "9 Nguyễn Thị Minh Khai, Phường Đa Kao, Quận 1, TP. Hồ Chí Minh", "Nguyễn Kim Ngân", "Bạc", "0908776655", 65 },
                    { 10, "301 Quang Trung, Phường 10, Quận Gò Vấp, TP. Hồ Chí Minh", "Trịnh Văn Lộc", "Chuẩn", "0932114455", 30 },
                    { 11, "74 Hoàng Diệu, Phường 6, Quận 4, TP. Hồ Chí Minh", "Đặng Thùy Dương", "Kim cương", "0987112233", 410 },
                    { 12, "12 Lý Thường Kiệt, Phường Trần Hưng Đạo, Quận Hoàn Kiếm, TP. Hà Nội", "Lý Hoài Anh", "Chuẩn", "0912667788", 25 },
                    { 13, "55 Nguyễn Trãi, Phường Thanh Xuân Trung, Quận Thanh Xuân, TP. Hà Nội", "Ngô Đức Thịnh", "Vàng", "0967334455", 140 },
                    { 14, "18 Trần Duy Hưng, Phường Trung Hòa, Quận Cầu Giấy, TP. Hà Nội", "Phan Thị Bích", "Bạc", "0904556677", 70 },
                    { 15, "209 Tây Sơn, Phường Quang Trung, Quận Đống Đa, TP. Hà Nội", "Trương Công Định", "Chuẩn", "0945667788", 15 },
                    { 16, "46 Mỹ Đình, Phường Mỹ Đình 2, Quận Nam Từ Liêm, TP. Hà Nội", "Đinh Thị Hồng", "Kim cương", "0978334499", 260 },
                    { 17, "33 Lạch Tray, Phường Đằng Giang, Quận Ngô Quyền, TP. Hải Phòng", "Vũ Minh Trí", "Bạc", "0398221144", 55 },
                    { 18, "120 Hồ Tùng Mậu, Phường Bắc Lý, TP. Đồng Hới, TP. Quảng Bình", "Cao Thị Xuân", "Chuẩn", "0937668877", 12 },
                    { 19, "67 Nguyễn Văn Linh, Phường Nam Dương, Quận Hải Châu, TP. Đà Nẵng", "Mai Văn Khánh", "Vàng", "0905223311", 175 },
                    { 20, "25 Bạch Đằng, Phường Thạch Thang, Quận Hải Châu, TP. Đà Nẵng", "Lâm Thanh Hà", "Bạc", "0983887766", 95 },
                    { 21, "14 Trần Phú, Phường Lộc Thọ, TP. Nha Trang, TP. Khánh Hòa", "Tạ Quang Huy", "Chuẩn", "0918776655", 40 },
                    { 22, "8 Nguyễn Tất Thành, Phường Tân Lập, TP. Buôn Ma Thuột, TP. Đắk Lắk", "Hồ Ngọc Diệp", "Vàng", "0976112233", 130 },
                    { 23, "91 Hùng Vương, Phường Thới Bình, Quận Ninh Kiều, TP. Cần Thơ", "Nguyễn Bá Tùng", "Bạc", "0932119988", 60 },
                    { 24, "33 Nguyễn Đình Chiểu, Phường 5, TP. Mỹ Tho, TP. Tiền Giang", "Trần Mỹ Linh", "Chuẩn", "0903112244", 20 },
                    { 25, "7 Phạm Văn Đồng, Phường Hiệp Bình Chánh, TP. Thủ Đức, TP. Hồ Chí Minh", "Phạm Đức Duy", "Vàng", "0967554433", 240 },
                    { 26, "16 Hà Huy Tập, Phường Tân An, TP. Vinh, TP. Nghệ An", "Lữ Thị Mai", "Chuẩn", "0917443322", 35 },
                    { 27, "52 Trần Quốc Toản, Phường 8, TP. Đà Lạt, TP. Lâm Đồng", "Sơn Tùng Vinh", "Bạc", "0988114477", 110 },
                    { 28, "19 Trần Đại Nghĩa, Phường Tân Kiểng, Quận 7, TP. Hồ Chí Minh", "Châu Thị Kim", "Kim cương", "0909224488", 500 },
                    { 29, "63 Quốc lộ 1A, Phường Tân Thới Hiệp, Quận 12, TP. Hồ Chí Minh", "Bạch Văn Toàn", "Chuẩn", "0938224411", 48 },
                    { 30, "101 Lê Duẩn, Phường Hải Châu 1, Quận Hải Châu, TP. Đà Nẵng", "Đoàn Thanh Vân", "Vàng", "0977778899", 205 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 21, "8938000000021", 6, 8000m, "Hành lá 200g", 120 },
                    { 22, "8938000000022", 6, 7000m, "Ngò gai 100g", 100 },
                    { 23, "8938000000023", 6, 7000m, "Húng quế 100g", 95 },
                    { 24, "8938000000024", 6, 15000m, "Sả tươi 300g", 80 },
                    { 25, "8938000000025", 6, 12000m, "Ớt hiểm 100g", 70 },
                    { 26, "8938000000026", 6, 18000m, "Gừng tươi 250g", 60 },
                    { 27, "8938000000027", 7, 189000m, "Nho mẫu đơn Hàn Quốc 500g", 35 },
                    { 28, "8938000000028", 7, 165000m, "Cherry Mỹ 500g", 40 },
                    { 29, "8938000000029", 7, 98000m, "Kiwi New Zealand 6 trái", 45 },
                    { 30, "8938000000030", 7, 89000m, "Lê Hàn Quốc 1kg", 50 },
                    { 31, "8938000000031", 7, 125000m, "Táo Envy Mỹ 1kg", 42 },
                    { 32, "8938000000032", 7, 79000m, "Cam Úc 1kg", 55 },
                    { 33, "8938000000033", 8, 55000m, "Trứng gà hữu cơ 10 quả", 70 },
                    { 34, "8938000000034", 8, 62000m, "Sữa tươi hữu cơ 1L", 65 },
                    { 35, "8938000000035", 8, 22000m, "Đậu hũ non hữu cơ 300g", 55 },
                    { 36, "8938000000036", 8, 38000m, "Bún gạo lứt hữu cơ 500g", 48 },
                    { 37, "8938000000037", 8, 210000m, "Mật ong rừng nguyên chất 500ml", 30 },
                    { 38, "8938000000038", 8, 95000m, "Dầu dừa ép lạnh 250ml", 40 },
                    { 39, "8938000000039", 9, 68000m, "Nước ép cam nguyên chất 1L", 50 },
                    { 40, "8938000000040", 9, 45000m, "Nước ép cần tây 500ml", 45 },
                    { 41, "8938000000041", 9, 120000m, "Trà xanh matcha 200g", 35 },
                    { 42, "8938000000042", 9, 110000m, "Cà phê rang xay 250g", 40 },
                    { 43, "8938000000043", 9, 35000m, "Nước dừa tươi 1L", 60 },
                    { 44, "8938000000044", 9, 75000m, "Sữa hạt điều 1L", 38 },
                    { 45, "8938000000045", 10, 89000m, "Yến mạch cán dẹt 1kg", 55 },
                    { 46, "8938000000046", 10, 48000m, "Gạo lứt huyết rồng 1kg", 90 },
                    { 47, "8938000000047", 10, 32000m, "Đậu xanh cà vỏ 500g", 75 },
                    { 48, "8938000000048", 10, 145000m, "Hạt chia Úc 500g", 42 },
                    { 49, "8938000000049", 10, 85000m, "Hạt lanh vàng 500g", 36 },
                    { 50, "8938000000050", 10, 135000m, "Ngũ cốc granola 500g", 30 },
                    { 51, "8938000000051", 11, 65000m, "Bơ sáp Đắk Lắk 1kg", 48 },
                    { 52, "8938000000052", 11, 135000m, "Hạnh nhân rang 250g", 40 },
                    { 53, "8938000000053", 11, 155000m, "Óc chó Mỹ 250g", 33 },
                    { 54, "8938000000054", 11, 175000m, "Hạt mắc ca 250g", 28 },
                    { 55, "8938000000055", 11, 78000m, "Hạt bí rang 250g", 45 },
                    { 56, "8938000000056", 11, 55000m, "Đậu phộng rang 500g", 60 },
                    { 57, "8938000000057", 12, 99000m, "Combo rau sạch 5 loại", 25 },
                    { 58, "8938000000058", 12, 450000m, "Giỏ quà trái cây cao cấp", 15 },
                    { 59, "8938000000059", 12, 189000m, "Combo ăn sáng dinh dưỡng", 20 },
                    { 60, "8938000000060", 12, 229000m, "Combo nấu lẩu rau củ 4 người", 18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);
        }
    }
}
