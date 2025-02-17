
using ParkingLotSystem.Server.Core.Entities;
using System.Threading.Tasks;

namespace ParkingLotSystem.DataAccess.Interfaces
{
    public interface ISiteRepository : IRepository<Site>
    {
        Task<Site> GetBySiteSecretAsync(string siteSecret);
    }
}
