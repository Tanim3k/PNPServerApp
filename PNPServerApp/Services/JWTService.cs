using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PNPServerApp.Interfaces;
using PNPServerApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PNPServerApp.Services
{
    public class JWTService : IJWTService
    {

        private readonly IConfigurationSection jwtSettings;

        public JWTService(IConfigurationSection jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public string CreateToken(UserModel registerUser)
        {
            if (registerUser == null) return String.Empty;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: new List<Claim>
                {
                        new Claim(ClaimTypes.Name, registerUser.UserName)
                },
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public string ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                // return user id from JWT token if validation successful
                return userName;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
