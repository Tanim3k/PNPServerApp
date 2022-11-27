using PNPServerApp.Interfaces;
using PNPServerApp.Models;
using System.Data;
using System.Security.Cryptography;

namespace PNPServerApp.Services
{
    public class UsersService : BaseService, IUsersService
    {

        public UsersService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) 
            : base(httpContextAccessor, httpClientFactory)
        {
        }

        public UserModel? GetUserByUserName(string userName)
        {
            return string.IsNullOrEmpty(userName) ? null : registerUsers.FirstOrDefault(m => m.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public UserModel CreateUser(UserCreateModel request)
        {
            if (registerUsers.Any(m => request.UserName.Equals(m.UserName, StringComparison.OrdinalIgnoreCase))) throw new DuplicateNameException();

            CreatePasswordHash(request.Password, out Byte[] passwordHash, out Byte[] passwordSalt);

            var userDAO = new UserModel
            {
                Id = registerUsers.Count + 1,
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Accounts = new List<AccountModel>()
            };

            registerUsers.Add(userDAO);

            return userDAO;
        }

        public void CreatePasswordHash(string password, out Byte[] passwordHash, out Byte[] passwordSalt)
        {
            using (var mac = new HMACSHA512())
            {
                passwordSalt = mac.Key;
                passwordHash = mac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, Byte[] passwordHash, Byte[] passwordSalt)
        {
            using (var mac = new HMACSHA512(passwordSalt))
            {
                var computePasswordHash = mac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computePasswordHash.SequenceEqual(passwordHash);
            }
        }
    }
}
