using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Helpers;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Chỉ Admin (quản trị viên) mới được quản trị tài khoản
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public UsersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ: Danh sách tài khoản (không trả về mật khẩu)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.AsNoTracking()
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .ToListAsync();
            return Ok(users);
        }

        // 1b. READ: Danh sách tài khoản có PHÂN TRANG (.Skip / .Take)
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Users.AsNoTracking().OrderBy(u => u.UserId);

            int totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .ToListAsync();

            return Ok(new PagedResult<UserDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Data = items
            });
        }

        // 2. READ: Chi tiết tài khoản
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.AsNoTracking()
                .Where(u => u.UserId == id)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });
            return Ok(user);
        }

        // 3. CREATE: Tạo tài khoản nhân viên mới
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được trống!" });
            }

            bool exists = await _context.Users.AnyAsync(u => u.Username == dto.Username);
            if (exists) return Conflict(new { message = "Tên đăng nhập đã tồn tại!" });

            var user = new User
            {
                Username = dto.Username.Trim(),
                Password = PasswordHelper.Hash(dto.Password), // luôn lưu mật khẩu đã băm
                FullName = dto.FullName,
                Email = dto.Email,
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "Cashier" : dto.Role,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            });
        }

        // 4. UPDATE: Đặt lại mật khẩu
        [HttpPut("{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });
            if (string.IsNullOrWhiteSpace(dto.NewPassword)) return BadRequest(new { message = "Mật khẩu mới không được trống!" });

            user.Password = PasswordHelper.Hash(dto.NewPassword); // băm mật khẩu mới
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. UPDATE: Khóa / Mở khóa tài khoản
        [HttpPut("{id}/toggle-lock")]
        public async Task<IActionResult> ToggleLock(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
            return Ok(new { user.UserId, user.Username, user.IsActive });
        }
    }

    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }

    public class ResetPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}
