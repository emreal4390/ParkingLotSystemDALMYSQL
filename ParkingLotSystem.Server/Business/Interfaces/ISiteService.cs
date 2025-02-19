namespace ParkingLotSystem.Server.Business.Interfaces
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetAllSitesAsync();
        Task<Site> GetBySiteSecretAsync(string siteSecret);
    }
}
