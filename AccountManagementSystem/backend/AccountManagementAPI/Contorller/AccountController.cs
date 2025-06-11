using Microsoft.AspNetCore.Mvc;
using AccountManagementAPI.Interfaces;
using AccountManagementAPI.Models;

namespace AccountManagementAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Account account)
        {
            if (string.IsNullOrEmpty(account.Username) || string.IsNullOrEmpty(account.Password) || string.IsNullOrEmpty(account.Email))
            {
                return BadRequest("所有欄位都是必填的。");
            }

            _accountService.Register(account);
            return Ok(new { message = "註冊成功" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var account = _accountService.Login(loginRequest.Username, loginRequest.Password);
            if (account == null)
            {
                return Unauthorized(new { message = "用戶名或密碼錯誤" });
            }

            return Ok(account);
        }

        [HttpGet("accounts")]
        public IActionResult GetAllAccounts()
        {
            return Ok(_accountService.GetAllAccounts());
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
