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
    [Authorize] // Bắt buộc JWT Token (kế thừa cơ chế bảo mật từ Buổi 2)
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Ánh xạ Entity -> DTO
        private static CustomerDto ToDto(Customer c) => new()
        {
            CustomerId = c.CustomerId,
            CustomerName = c.CustomerName,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address,
            RewardPoints = c.RewardPoints,
            MembershipRank = c.MembershipRank
        };

        // 1. READ: Lấy toàn bộ danh sách khách hàng từ SQL Server
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Customers.AsNoTracking().ToListAsync();
            return Ok(list.Select(ToDto).ToList());
        }

        // 1b. READ: Danh sách khách hàng có PHÂN TRANG (.Skip / .Take)
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Customers.AsNoTracking().OrderBy(c => c.CustomerId);

            int totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<CustomerDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Data = items.Select(ToDto).ToList()
            });
        }

        // 2. READ: Lấy chi tiết một khách hàng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng trong CSDL!" });
            }
            return Ok(ToDto(customer));
        }

        // 3. SEARCH: Tìm kiếm theo Tên hoặc Số điện thoại (GET /api/customers/search?keyword=...)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }

            // EF Core dịch biểu thức LINQ thành câu lệnh SQL LIKE tương ứng
            var result = await _context.Customers
                .Where(c => c.CustomerName.Contains(keyword) || c.PhoneNumber.Contains(keyword))
                .AsNoTracking()
                .ToListAsync();
            return Ok(result.Select(ToDto).ToList());
        }

        // 4. CREATE: Thêm mới khách hàng thành viên (POST /api/customers)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Customer
            {
                CustomerName = dto.CustomerName,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                RewardPoints = dto.RewardPoints,
                MembershipRank = dto.MembershipRank,
                CreatedDate = DateTime.Now // Ghi nhận ngày đăng ký thành viên
            };

            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.CustomerId }, ToDto(entity));
        }

        // 5. UPDATE: Cập nhật thông tin và hạng thẻ khách hàng (PUT /api/customers/{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng cần sửa!" });
            }

            customer.CustomerName = dto.CustomerName;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.Address = dto.Address;
            customer.RewardPoints = dto.RewardPoints;
            customer.MembershipRank = dto.MembershipRank;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Xóa khách hàng khỏi hệ thống (DELETE /api/customers/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng cần xóa!" });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
