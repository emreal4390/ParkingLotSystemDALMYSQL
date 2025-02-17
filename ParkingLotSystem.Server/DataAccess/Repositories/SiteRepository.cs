using ParkingLotSystem.DataAccess.Contexts;
using ParkingLotSystem.DataAccess.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.Server.Core.Entities;

namespace ParkingLotSystem.DataAccess.Repositories
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
