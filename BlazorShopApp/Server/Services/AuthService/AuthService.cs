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

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            var passwordMatches = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!passwordMatches)
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = "token";
            }


            return response;
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

            return new ServiceResponse<int> { Data = user.Id, Message = "Account registration succesful" };
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }

        }
    }
}
