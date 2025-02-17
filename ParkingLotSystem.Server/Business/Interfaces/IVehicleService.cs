using ParkingLotSystem.Server.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotSystem.Business.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetActiveVehiclesAsync(int siteId);
        Task<IEnumerable<Vehicle>> GetVehicleHistoryAsync(int siteId);
        Task<IEnumerable<Vehicle>> GetFilteredVehicleHistoryAsync(int siteId, string? plate, string? ownerName, string? apartmentNo, DateTime? dateFrom, DateTime? dateTo, int? minDuration, int? maxDuration);

        Task AddVehicleAsync(Vehicle vehicle, int siteId);

        Task<bool> ExitVehicleAsync(int vehicleId, int siteId);
    }
}
