using BlazorShopApp.Shared;

namespace BlazorShopApp.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        //Task<ServiceResponse<int>> Login(User user, string password);
    }
}
