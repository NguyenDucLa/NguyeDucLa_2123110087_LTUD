using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms
{
    // Dịch vụ HttpClient dùng chung: tự động đính kèm JWT Token từ SessionManager
    public static class ApiClientService
    {
        private const string ApiBaseUrl = "https://localhost:7075/api/";

        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(ApiBaseUrl)
        };

        public static HttpClient Client
        {
            get
            {
                _client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(SessionManager.JwtToken)
                    ? null
                    : new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
                return _client;
            }
        }
    }
}
