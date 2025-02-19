using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.Server.Core.Entities;
using ParkingLotSystem.Server.DataAccess.Contexts;
using ParkingLotSystem.Server.DataAccess.Interfaces;

namespace ParkingLotSystem.Server.DataAccess.Repositories
{
    public class SiteRepository : GenericRepository<Site>, ISiteRepository
    {
        public SiteRepository(ParkingLotSystemDbContext context) : base(context) { }

        public async Task<Site> GetBySiteSecretAsync(string siteSecret)
        {
            return await _context.Sites.FirstOrDefaultAsync(s => s.SiteSecret == siteSecret);
        }
    }
}
