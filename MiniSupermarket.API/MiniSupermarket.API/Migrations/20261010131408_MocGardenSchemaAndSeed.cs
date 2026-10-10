using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class MocGardenSchemaAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Customers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Rau ăn lá", "Rau muống, cải ngọt, xà lách, cải bó xôi..." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Củ - Quả", "Cà rốt, khoai tây, cà chua, hành tây, bí đỏ..." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Trái cây tươi", "Táo, cam, chuối, xoài, dưa hấu..." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nấm & Rau mầm", "Nấm kim châm, nấm đùi gà, rau mầm, giá đỗ" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nông sản khô & Gia vị", "Gạo ST25, hạt điều, các loại hạt và gia vị" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "City", "CustomerName", "District", "HouseNumber", "Street", "Ward" },
                values: new object[] { "TP. Hồ Chí Minh", "Nguyễn Thị Lan", "Quận 1", "12", "Nguyễn Trãi", "Phường Bến Thành" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "City", "CustomerName", "District", "HouseNumber", "Street", "Ward" },
                values: new object[] { "TP. Hồ Chí Minh", "Trần Văn Hùng", "Quận Gò Vấp", "45", "Lê Lợi", "Phường 4" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "City", "CustomerName", "District", "HouseNumber", "Street", "Ward" },
                values: new object[] { "TP. Hà Nội", "Lê Thu Hà", "Quận Hoàn Kiếm", "78", "Trần Hưng Đạo", "Phường Cửa Nam" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "City", "CustomerName", "District", "HouseNumber", "MembershipRank", "PhoneNumber", "RewardPoints", "Street", "Ward" },
                values: new object[,]
                {
                    { 4, "TP. Hà Nội", "Phạm Minh Quân", "Quận Long Biên", "23", "Bạc", "0905556677", 80, "Nguyễn Văn Cừ", "Phường Bồ Đề" },
                    { 5, "TP. Đà Nẵng", "Đỗ Thị Mai", "Quận Thanh Khê", "150", "Vàng", "0938777888", 220, "Điện Biên Phủ", "Phường Chính Gián" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000001", 15000m, "Rau muống hữu cơ 500g", 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000002", 18000m, "Cải ngọt VietGAP 500g", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000003", 1, 25000m, "Xà lách Romaine 300g", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000004", 1, 22000m, "Rau cải bó xôi 400g", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000005", 2, 20000m, "Cà rốt Đà Lạt 500g", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000006", 2, 35000m, "Khoai tây Đà Lạt 1kg", 130 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000007", 2, 30000m, "Cà chua bi 500g", 110 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8938000000008", 2, 28000m, "Hành tây 1kg", 100 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 9, "8938000000009", 2, 25000m, "Bí đỏ hồ lô 1kg", 70 },
                    { 10, "8938000000010", 3, 95000m, "Táo Fuji nhập khẩu 1kg", 60 },
                    { 11, "8938000000011", 3, 45000m, "Cam sành 1kg", 85 },
                    { 12, "8938000000012", 3, 35000m, "Chuối Laba 1kg", 95 },
                    { 13, "8938000000013", 3, 85000m, "Xoài Cát Hòa Lộc 1kg", 50 },
                    { 14, "8938000000014", 3, 40000m, "Dưa hấu 2kg", 65 },
                    { 15, "8938000000015", 4, 25000m, "Nấm kim châm 200g", 75 },
                    { 16, "8938000000016", 4, 35000m, "Nấm đùi gà 300g", 60 },
                    { 17, "8938000000017", 4, 30000m, "Rau mầm cải 150g", 90 },
                    { 18, "8938000000018", 4, 12000m, "Giá đỗ 300g", 140 },
                    { 19, "8938000000019", 5, 35000m, "Gạo thơm ST25 1kg", 200 },
                    { 20, "8938000000020", 5, 120000m, "Hạt điều rang muối 250g", 45 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[] { 6, "test", "test" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "CustomerName" },
                values: new object[] { "12 Nguyễn Trãi, Q.1", "Nguyễn Văn A" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "CustomerName" },
                values: new object[] { "45 Lê Lợi, Q.3", "Trần Thị B" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName" },
                values: new object[] { "78 Trần Hưng Đạo, Q.5", "Lê Văn C" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935001100011", 22000m, "Bánh Oreo 137g", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Barcode", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935001100028", 35000m, "Kẹo dẻo Haribo 80g", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935002200017", 2, 10000m, "Coca-Cola lon 330ml", 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935002200024", 2, 12000m, "Trà xanh 0 độ 500ml", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935003300013", 3, 32000m, "Sữa tươi Vinamilk 1L", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935003300020", 3, 28000m, "Sữa chua Vinamilk lốc 4", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935004400019", 4, 5000m, "Mì Hảo Hảo gói", 500 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { "8935005500015", 5, 45000m, "Nước mắm Nam Ngư 500ml", 70 });
        }
    }
}
