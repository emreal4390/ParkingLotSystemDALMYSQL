using ParkingLotSystem.Business.Interfaces;
using ParkingLotSystem.DataAccess.Interfaces;
using ParkingLotSystem.Server.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ParkingLotSystem.Business.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetActiveVehiclesAsync(int siteId)
        {
            return await _vehicleRepository.GetActiveVehiclesAsync(siteId);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleHistoryAsync(int siteId)
        {
            return await _vehicleRepository.GetVehicleHistoryAsync(siteId);
        }

        public async Task AddVehicleAsync(Vehicle vehicle, int siteId)
        {
            vehicle.SiteID = siteId;
            vehicle.EntryTime = DateTime.UtcNow.AddHours(3);
            await _vehicleRepository.AddAsync(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetFilteredVehicleHistoryAsync(int siteId, string? plate, string? ownerName, string? apartmentNo, DateTime? dateFrom, DateTime? dateTo, int? minDuration, int? maxDuration)
        {
            return await _vehicleRepository.GetFilteredVehicleHistoryAsync(siteId, plate, ownerName, apartmentNo, dateFrom, dateTo, minDuration, maxDuration);
        }

        public async Task<bool> ExitVehicleAsync(int vehicleId, int siteId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null || vehicle.SiteID != siteId)
            {
                return false;
            }

            vehicle.ExitTime = DateTime.UtcNow.AddHours(3);
            await _vehicleRepository.UpdateAsync(vehicle);
            return true;
        }
    }
}
