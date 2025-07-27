using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var user = await _authService.RegisterAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var user = await _authService.LoginAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> ViewProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var profile = await _authService.ViewProfileAsync(userId);
            return Ok(profile); // returns only username + email
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameRequestDto request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var updated = await _authService.UpdateUsernameAsync(userId, request.NewUsername);
            return Ok(updated); // returns updated username + email
        }
    }
}
