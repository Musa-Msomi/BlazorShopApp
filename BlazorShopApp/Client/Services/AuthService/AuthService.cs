using BlazorShopApp.Shared;
using System.Net.Http.Json;

namespace BlazorShopApp.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<int>> Register(UserRegister userRegister)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);

            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            return response;
        }
    }
}
