using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Ánh xạ Entity -> DTO (kèm tên nhóm hàng)
        private static ProductDto ToDto(Product p) => new()
        {
            ProductId = p.ProductId,
            Barcode = p.Barcode,
            ProductName = p.ProductName,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.CategoryName ?? string.Empty
        };

        // 1. READ: Toàn bộ sản phẩm — Eager Loading kèm Nhóm hàng (.Include)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Products
                .Include(p => p.Category) // Nạp kèm thông tin danh mục
                .AsNoTracking()
                .ToListAsync();
            return Ok(list.Select(ToDto).ToList());
        }

        // 1b. READ: Danh sách sản phẩm có PHÂN TRANG (.Skip / .Take)
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .OrderBy(p => p.ProductId);

            int totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<ProductDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Data = items.Select(ToDto).ToList()
            });
        }

        // 2. READ: Chi tiết theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            return Ok(ToDto(product));
        }

        // 3. SEARCH: Tìm theo tên hoặc mã vạch
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });

            var result = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductName.Contains(keyword) || p.Barcode.Contains(keyword))
                .AsNoTracking()
                .ToListAsync();
            return Ok(result.Select(ToDto).ToList());
        }

        // 4. READ: Tra cứu theo mã vạch Barcode (phục vụ quét mã ở quầy POS)
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Barcode == barcode);

            if (product == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm có mã vạch này!" });
            return Ok(ToDto(product));
        }

        // 5. READ: Lọc sản phẩm theo nhóm hàng
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var list = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
            return Ok(list.Select(ToDto).ToList());
        }

        // 6. CREATE: Thêm sản phẩm (Admin & Thủ kho)
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Product
            {
                Barcode = dto.Barcode,
                ProductName = dto.ProductName,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            var created = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstAsync(p => p.ProductId == entity.ProductId);

            return CreatedAtAction(nameof(GetById), new { id = entity.ProductId }, ToDto(created));
        }

        // 7. UPDATE: Cập nhật sản phẩm (Admin & Thủ kho)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound(new { message = "Không tìm thấy sản phẩm cần sửa!" });

            product.Barcode = dto.Barcode;
            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 8. DELETE: Xóa sản phẩm (Admin & Thủ kho)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound(new { message = "Không tìm thấy sản phẩm cần xóa!" });

            // Chặn xóa sản phẩm đã phát sinh hóa đơn (khóa ngoại OrderDetails)
            bool usedInOrder = await _context.OrderDetails.AnyAsync(d => d.ProductId == id);
            if (usedInOrder)
            {
                return BadRequest(new { message = "Sản phẩm đã có trong hóa đơn, không thể xóa!" });
            }

            _context.Products.Remove(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Không thể xóa sản phẩm vì đang được dữ liệu khác tham chiếu!" });
            }
            return NoContent();
        }
    }
}
