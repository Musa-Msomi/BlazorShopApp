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

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", userChangePassword.Password);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", userLogin);

            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();

            return response;
        }

        public async Task<ServiceResponse<int>> Register(UserRegister userRegister)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);

            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();

            return response;
        }
    }
}
