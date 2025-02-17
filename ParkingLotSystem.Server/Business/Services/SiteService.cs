using ParkingLotSystem.Business.Interfaces;
using ParkingLotSystem.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingLotSystem.Server.Core.Entities;

namespace ParkingLotSystem.Business.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;

        public SiteService(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        public async Task<IEnumerable<Site>> GetAllSitesAsync()
        {
            return await _siteRepository.GetAllAsync();
        }

        public async Task<Site> GetBySiteSecretAsync(string siteSecret)
        {
            return await _siteRepository.GetBySiteSecretAsync(siteSecret);
        }
    }
}
