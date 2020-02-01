using System.Threading.Tasks;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(passwordBytes);
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHasch(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHasch(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt);
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            var computeHash = hmac.ComputeHash(passwordBytes);

            for (int i = 0; i < userPasswordHash.Length; i++)
            {
                if (userPasswordHash[i] != computeHash[i])
                    return false;
            }

            return true;
        }


        public async Task<bool> UserExists(string username)
        {
            if (await _dataContext.Users.AnyAsync(x => x.Username == username))
                return true;
            
            return false;
        }
    }
}