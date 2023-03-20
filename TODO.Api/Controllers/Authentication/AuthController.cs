using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODO.BL.AuthService;
using TODO.BL.Models;
using TODO.BL.Services.AuthService;

namespace TODO.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok(await _authService.Login(model));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            return Ok(await _authService.Registration(model));
        }
    }
}
