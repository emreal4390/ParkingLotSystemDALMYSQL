using System.Threading.Tasks;

namespace ParkingLotSystem.Business.Services
{
    public interface IAuthService
    {
        Task<(string token, string role, int siteID, string siteSecret)> AuthenticateAsync(string email, string password);
    }
}
