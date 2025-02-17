
using ParkingLotSystem.Server.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotSystem.Business.Interfaces
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetAllSitesAsync();
        Task<Site> GetBySiteSecretAsync(string siteSecret);
    }
}
