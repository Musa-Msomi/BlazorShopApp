using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApp.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;

        public AuthService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<ServiceResponse<int>> Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string email)
        {
            var userExists = await _dataContext.Users.AnyAsync(x => x.Email.ToLower().Equals(email.ToLower()));

            if (userExists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
