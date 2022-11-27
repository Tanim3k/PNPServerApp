using PNPServerApp.Models;

namespace PNPServerApp.Interfaces
{
    public interface IJWTService
    {
        public string CreateToken(UserModel registerUser);
        public string ValidateToken(string token);
    }
}
