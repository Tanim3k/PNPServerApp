using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PNPServerApp.Interfaces;
using PNPServerApp.Models;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PNPServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUsersService usersService;
        private readonly IJWTService jWTService;

        public AuthController(IConfiguration configuration, IUsersService usersService, IJWTService jWTService)
        {
            this.configuration = configuration;
            this.usersService = usersService;
            this.jWTService = jWTService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]UserCreateModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (user == null || !user.UserName.Any() || !user.Password.Any()) return BadRequest(ModelState);

            var registerUser = usersService.GetUserByUserName(user.UserName);

            if (registerUser == null) return BadRequest(ModelState);

            if (usersService.VerifyPasswordHash(user.Password, registerUser.PasswordHash, registerUser.PasswordSalt))
            {
                string tokenString = jWTService.CreateToken(registerUser);
                return Ok(new { Token = tokenString });
            }

            return BadRequest();
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateModel request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (request == null || !request.UserName.Any() || !request.Password.Any()) return BadRequest(ModelState);

            var user = usersService.CreateUser(request);

            return Ok(user);
        }
    }
}
