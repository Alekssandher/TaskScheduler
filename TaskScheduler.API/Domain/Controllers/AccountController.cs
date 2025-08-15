using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler.API.Domain.DTOs;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.ModelViews;

namespace TaskScheduler.API.Domain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(ModelViews.BadRequest), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(InternalError), StatusCodes.Status500InternalServerError, "application/problem+json")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountInterface;
        public AccountController(IAccountService accountInterface)
        {
            _accountInterface = accountInterface;
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        [EndpointName("Register")]
        [EndpointSummary("Register Account.")]
        [ProducesResponseType(typeof(Created), StatusCodes.Status201Created, "application/json")]
        [EndpointDescription("Register your account.")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            await _accountInterface.Register(registerDto);

            return Created();
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [EndpointName("Login")]
        [EndpointSummary("Login and Get Token.")]
        [ProducesResponseType(typeof(OkResponse<IReadOnlyList<MyTaskResponse>>), StatusCodes.Status200OK, "application/json")]
        [EndpointDescription("Login and get the user token.")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var res = await _accountInterface.Login(loginDto);

            return Ok(new OkResponse<string>("", "", res));
        }
    }
}