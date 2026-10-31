using Microsoft.AspNetCore.Identity;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Helpers
{
    /// <summary>
    /// Băm và kiểm tra mật khẩu bằng PasswordHasher của ASP.NET Core
    /// (thuật toán PBKDF2 - HMAC-SHA256, 100.000 vòng lặp, mỗi mật khẩu có salt riêng).
    /// Không bao giờ lưu mật khẩu dạng thô vào cơ sở dữ liệu.
    /// </summary>
    public static class PasswordHelper
    {
        // Chuỗi hash do PasswordHasher sinh ra luôn bắt đầu bằng "AQAAAA"
        // (byte 0x01 = định dạng V3) -> dùng để nhận biết dữ liệu đã được băm hay chưa.
        private const string HashedPrefix = "AQAAAA";

        private static readonly PasswordHasher<User> Hasher = new();
        private static readonly User DummyUser = new();

        /// <summary>Băm mật khẩu trước khi lưu vào CSDL.</summary>
        public static string Hash(string plainPassword) => Hasher.HashPassword(DummyUser, plainPassword);

        /// <summary>Kiểm tra một chuỗi đã được băm hay còn là mật khẩu thô (dữ liệu cũ).</summary>
        public static bool IsHashed(string? value)
            => !string.IsNullOrEmpty(value) && value.StartsWith(HashedPrefix, StringComparison.Ordinal);

        /// <summary>
        /// So khớp mật khẩu người dùng nhập với dữ liệu trong CSDL.
        /// <paramref name="needUpgrade"/> = true khi cần băm lại (dữ liệu cũ lưu thô
        /// hoặc chuẩn băm đã lỗi thời) để hàm gọi tự cập nhật lại vào CSDL.
        /// </summary>
        public static bool Verify(string? storedPassword, string providedPassword, out bool needUpgrade)
        {
            needUpgrade = false;

            if (string.IsNullOrEmpty(storedPassword) || string.IsNullOrEmpty(providedPassword))
                return false;

            // Trường hợp CSDL cũ còn lưu mật khẩu dạng thô
            if (!IsHashed(storedPassword))
            {
                if (storedPassword == providedPassword)
                {
                    needUpgrade = true;
                    return true;
                }
                return false;
            }

            var result = Hasher.VerifyHashedPassword(DummyUser, storedPassword, providedPassword);

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
                needUpgrade = true;

            return result != PasswordVerificationResult.Failed;
        }
    }
}
