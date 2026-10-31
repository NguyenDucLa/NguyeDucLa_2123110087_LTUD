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
    [Authorize] // Bắt buộc JWT Token
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Ánh xạ Entity -> DTO (không trả nguyên Entity ra ngoài)
        private static CategoryDto ToDto(Category c) => new()
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description
        };

        // 1. READ: Lấy toàn bộ danh sách nhóm hàng (GET /api/categories)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Categories.AsNoTracking().ToListAsync();
            return Ok(list.Select(ToDto).ToList());
        }

        // 1b. READ: Danh sách nhóm hàng có PHÂN TRANG (.Skip / .Take)
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Categories.AsNoTracking().OrderBy(c => c.CategoryId);

            int totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<CategoryDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Data = items.Select(ToDto).ToList()
            });
        }

        // 2. READ: Lấy chi tiết một nhóm hàng theo ID (GET /api/categories/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng trong CSDL!" });
            }
            return Ok(ToDto(category));
        }

        // 3. SEARCH: Tìm kiếm nhóm hàng theo từ khóa (GET /api/categories/search?keyword=...)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }
            // EF Core dịch biểu thức LINQ thành câu lệnh SQL LIKE tương ứng
            var result = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryName.Contains(keyword))
                .ToListAsync();
            return Ok(result.Select(ToDto).ToList());
        }

        // 4. CREATE: Thêm mới nhóm hàng (POST /api/categories)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào SQL Server

            return CreatedAtAction(nameof(GetById), new { id = entity.CategoryId }, ToDto(entity));
        }

        // 5. UPDATE: Cập nhật thông tin nhóm hàng (PUT /api/categories/{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }

            cat.CategoryName = dto.CategoryName;
            cat.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Xóa nhóm hàng theo ID (DELETE /api/categories/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });
            }

            // Chặn xóa nhóm hàng còn sản phẩm bên trong (khóa ngoại Products)
            bool hasProduct = await _context.Products.AnyAsync(p => p.CategoryId == id);
            if (hasProduct)
            {
                return BadRequest(new { message = "Nhóm hàng đang có sản phẩm, không thể xóa!" });
            }

            _context.Categories.Remove(cat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Không thể xóa nhóm hàng vì đang được dữ liệu khác tham chiếu!" });
            }
            return NoContent();
        }

        // ==========================================
        // MÁY CHỦ THỬ NGHIỆM PHÂN QUYỀN
        // ==========================================

        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new { message = "Chào mừng Admin! Bạn có toàn quyền quản trị hệ thống siêu thị mini." });
        }

        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetStaffPos()
        {
            return Ok(new { message = "Màn hình POS Thu ngân sẵn sàng phục vụ bán hàng." });
        }
    }
}