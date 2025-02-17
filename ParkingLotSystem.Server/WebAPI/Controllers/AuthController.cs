using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParkingLotSystem.Business.Services;
using ParkingLotSystem.Core.DTOs;

namespace ParkingLotSystem.WebAPI.Controllers
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
