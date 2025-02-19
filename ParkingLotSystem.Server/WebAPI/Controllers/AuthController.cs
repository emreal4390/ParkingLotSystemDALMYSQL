using Microsoft.AspNetCore.Mvc;
using ParkingLotSystem.Server.Business.Interfaces;
using ParkingLotSystem.Server.Core.DTOs;

namespace ParkingLotSystem.Server.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var (token, role, siteID, siteSecret) = await _authService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (token == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new
            {
                token,
                role,
                siteID,
                siteSecret
            });
        }
    }
}
