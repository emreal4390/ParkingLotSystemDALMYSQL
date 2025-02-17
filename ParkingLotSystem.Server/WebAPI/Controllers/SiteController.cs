using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotSystem.Business.Interfaces;
using ParkingLotSystem.Server.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotSystem.WebAPI.Controllers
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
