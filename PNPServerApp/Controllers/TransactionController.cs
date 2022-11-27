using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PNPServerApp.Interfaces;
using PNPServerApp.Models;

namespace PNPServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IAccountService accountService;
        private readonly IUsersService usersService;

        public TransactionController(ITransactionService transactionService, IAccountService accountService, IUsersService usersService)
        {
            this.transactionService = transactionService;
            this.accountService = accountService;
            this.usersService = usersService;
        }

        [Authorize]
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateTransaction(TransactionCreateModel transactionCreateModel)
        {
            var user = usersService.GetCurrentUser();

            if (user == null) return BadRequest("User not found");
            var account = transactionService.CreateTransaction(transactionCreateModel);

            return account == null ? BadRequest() : Ok(account);
        }

        [Authorize]
        [HttpGet, Route("GetTransaction/{accountId?}")]
        public async Task<IActionResult> GetTransaction(int? accountId)
        {
            var user = usersService.GetCurrentUser();

            if (user == null) return BadRequest("User not found");

            var transactions = transactionService.GetAllTransactions(accountId);

            return Ok(transactions);
        }
    }
}
