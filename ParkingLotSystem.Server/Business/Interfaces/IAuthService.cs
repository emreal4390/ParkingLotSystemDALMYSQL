namespace ParkingLotSystem.Server.Business.Interfaces
{
    public interface IAuthService
    {
        Task<(string token, string role, int siteID, string siteSecret)> AuthenticateAsync(string email, string password);
        
    }
}
