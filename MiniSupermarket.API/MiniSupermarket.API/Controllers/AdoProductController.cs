using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    // DTO trả về cho quầy POS
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Route("api/ado-products")]
    [ApiController]
    [Authorize]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =============================================================
        // 1. TRA CỨU MÃ VẠCH SIÊU TỐC — Connected Mode (SqlDataReader)
        // GET: api/ado-products/barcode/8938000000001
        // =============================================================
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return BadRequest(new { message = "Mã vạch không được để trống!" });

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                       FROM Products
                                       WHERE Barcode = @Barcode";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Tham số hóa chống SQL Injection
                    command.Parameters.AddWithValue("@Barcode", barcode.Trim());

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var dto = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                };
                                return Ok(dto);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            }

            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // =============================================================
        // 2. TẢI DANH SÁCH LƯU ĐỆM — Disconnected Mode (DataTable + Adapter)
        // GET: api/ado-products/disconnected-cache
        // =============================================================
        [HttpGet("disconnected-cache")]
        public IActionResult GetProductsDisconnected()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // Bảng ảo trên RAM
            DataTable productTable = new DataTable("CachedProducts");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                       FROM Products
                                       ORDER BY ProductId ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    // Fill() tự mở kết nối -> đổ dữ liệu vào DataTable -> tự đóng kết nối
                    adapter.Fill(productTable);
                }
            }

            // Lúc này kết nối đã đóng hoàn toàn, thao tác độc lập trên RAM
            var resultList = new List<ProductLookupDto>();
            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                });
            }

            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // =============================================================
        // 3. BÀI TẬP MỞ RỘNG: Lọc sản phẩm sắp hết hàng (SqlDataReader)
        // GET: api/ado-products/filter-stock?minStock=50
        // =============================================================
        [HttpGet("filter-stock")]
        public IActionResult FilterByStock([FromQuery] int minStock = 50)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var resultList = new List<ProductLookupDto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                       FROM Products
                                       WHERE StockQuantity <= @MinStock
                                       ORDER BY StockQuantity ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MinStock", minStock);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultList.Add(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            }

            return Ok(new { minStock, total = resultList.Count, data = resultList });
        }
    }
}
