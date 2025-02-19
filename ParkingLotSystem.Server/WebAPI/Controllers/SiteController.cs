using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotSystem.Server.Business.Interfaces;

namespace ParkingLotSystem.Server.WebAPI.Controllers
{
    [Route("api/sites")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Site>>> GetAllSites()
        {
            return Ok(await _siteService.GetAllSitesAsync());
        }
    }
}
