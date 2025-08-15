using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.ModelViews;

namespace TaskScheduler.API.Domain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountInterface;

        public AccountController(IAccountService accountInterface)
        {
            _accountInterface = accountInterface;
        }

        [HttpPost]
        [Consumes("application/json")]
        [EndpointName("Register")]
        [EndpointSummary("Register Account.")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            await _accountInterface.Register(registerDto);

            return StatusCode(204, new NoContentResponse());
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [EndpointName("Login")]
        [EndpointSummary("Login and Get Token.")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var res = await _accountInterface.Login(loginDto);
            
            return Ok(new OkResponse<string>("", "", res));
        }
    }
}