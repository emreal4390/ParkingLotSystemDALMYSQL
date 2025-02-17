using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotSystem.Business.Interfaces;
using ParkingLotSystem.Server.Core.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParkingLotSystem.WebAPI.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("active")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetActiveVehicles()
        {
            int? userSiteId = GetUserSiteIdFromToken();
            if (userSiteId == null)
                return Unauthorized("Yetkilendirme hatası: Site bilgisi eksik!");

            var vehicles = await _vehicleService.GetActiveVehiclesAsync(userSiteId.Value);
            return Ok(vehicles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
                return BadRequest("Araç bilgisi eksik.");

            int? userSiteId = GetUserSiteIdFromToken();
            if (userSiteId == null)
                return Unauthorized("Yetkilendirme hatası: Site bilgisi eksik!");

            await _vehicleService.AddVehicleAsync(vehicle, userSiteId.Value);

            return CreatedAtAction(nameof(GetActiveVehicles), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}/exit")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ExitVehicle(int id)
        {
            int? userSiteId = GetUserSiteIdFromToken();
            if (userSiteId == null)
                return Unauthorized("Yetkilendirme hatası: Site bilgisi eksik!");

            bool success = await _vehicleService.ExitVehicleAsync(id, userSiteId.Value);
            if (!success)
                return NotFound("Araç bulunamadı veya yetkisiz işlem.");

            return NoContent();
        }

        [HttpGet("history")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicleHistory()
        {
            int? userSiteId = GetUserSiteIdFromToken();
            if (userSiteId == null)
                return Unauthorized("Yetkilendirme hatası: Site bilgisi eksik!");

            var vehicles = await _vehicleService.GetVehicleHistoryAsync(userSiteId.Value);
            return Ok(vehicles);
        }

        [HttpGet("history/filter")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetFilteredVehicleHistory(
            [FromQuery] string? plate,
            [FromQuery] string? ownerName,
            [FromQuery] string? apartmentNo,
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] int? minDuration,
            [FromQuery] int? maxDuration)
        {
            int? userSiteId = GetUserSiteIdFromToken();
            if (userSiteId == null)
                return Unauthorized("Yetkilendirme hatası: Site bilgisi eksik!");

            var vehicles = await _vehicleService.GetFilteredVehicleHistoryAsync(userSiteId.Value, plate, ownerName, apartmentNo, dateFrom, dateTo, minDuration, maxDuration);
            return Ok(vehicles);
        }

        private int? GetUserSiteIdFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var siteIdClaim = identity?.FindFirst("SiteID");
            return int.TryParse(siteIdClaim?.Value, out int siteId) ? siteId : (int?)null;
        }
    }
}
