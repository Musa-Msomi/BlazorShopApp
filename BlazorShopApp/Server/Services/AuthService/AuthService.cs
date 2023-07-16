using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorShopApp.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;

        public AuthService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var userExists = await UserExists(user.Email);
            if (userExists)
            {
                return new ServiceResponse<int> { Success = false, Message = "User already exists" };
            }

            // hashing
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dataContext.Users.Add(user);

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id , Message="Account registration succesful"};
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
