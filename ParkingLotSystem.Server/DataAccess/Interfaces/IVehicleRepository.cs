using ParkingLotSystem.Server.Core.Entities;

namespace ParkingLotSystem.Server.DataAccess.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetActiveVehiclesAsync(int siteId);
        Task<IEnumerable<Vehicle>> GetVehicleHistoryAsync(int siteId);

        Task<IEnumerable<Vehicle>> GetFilteredVehicleHistoryAsync(int siteId, string? plate, string? ownerName, string? apartmentNo, DateTime? dateFrom, DateTime? dateTo, int? minDuration, int? maxDuration);

        Task DeleteVehicleAsync(Vehicle vehicle);
        //Task<Vehicle> GetByIdAsync(int vehicleId);
    }
}
