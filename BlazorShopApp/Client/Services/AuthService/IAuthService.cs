using BlazorShopApp.Shared;

namespace BlazorShopApp.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister userRegister);
        Task<ServiceResponse<string>> Login(UserLogin userLogin);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword);
    }
}
