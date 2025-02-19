using ParkingLotSystem.Server.Business.Interfaces;
using ParkingLotSystem.Server.DataAccess.Interfaces;

namespace ParkingLotSystem.Server.Business.Services
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
