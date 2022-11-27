using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PNPServerApp.Interfaces;
using PNPServerApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PNPServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IUsersService usersService;

        public AccountController(IAccountService accountService, IUsersService usersService)
        {
            this.accountService = accountService;
            this.usersService = usersService;
        }

        [Authorize]
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(AccountCreateModel accountCreateModel)
        {
            var user = usersService.GetCurrentUser();

            if (user == null) return BadRequest("User not found");
            var account = accountService.CreateAccount(accountCreateModel);

            return account == null ? BadRequest() : Ok(account);
        }

    }
}
