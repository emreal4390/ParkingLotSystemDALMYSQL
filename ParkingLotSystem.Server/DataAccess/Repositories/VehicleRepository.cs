using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.Server.Core.Entities;
using ParkingLotSystem.Server.DataAccess.Contexts;
using ParkingLotSystem.Server.DataAccess.Interfaces;

namespace ParkingLotSystem.Server.DataAccess.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ParkingLotSystemDbContext context) : base(context) { }

        public async Task<IEnumerable<Vehicle>> GetActiveVehiclesAsync(int siteId)
        {
            return await _context.Vehicles
                .Where(v => v.ExitTime == null && v.SiteID == siteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleHistoryAsync(int siteId)
        {
            return await _context.Vehicles
                .Where(v => v.SiteID == siteId)
                .ToListAsync();
        }
        public async Task<Vehicle> GetByIdAsync(int vehicleId)
        {
            return await _context.Vehicles.FindAsync(vehicleId);
        }

        public async Task DeleteVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetFilteredVehicleHistoryAsync(int siteId, string? plate, string? ownerName, string? apartmentNo, DateTime? dateFrom, DateTime? dateTo, int? minDuration, int? maxDuration)
        {
            var vehicles = _context.Vehicles
                .Where(v => v.SiteID == siteId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(plate))
                vehicles = vehicles.Where(v => v.LicensePlate.Contains(plate));

            if (!string.IsNullOrEmpty(ownerName))
                vehicles = vehicles.Where(v => v.OwnerName.Contains(ownerName));

            if (!string.IsNullOrEmpty(apartmentNo))
                vehicles = vehicles.Where(v => v.ApartmentNumber.Contains(apartmentNo));

            if (dateFrom.HasValue && dateTo.HasValue)
                vehicles = vehicles.Where(v => v.EntryTime >= dateFrom && v.ExitTime <= dateTo);

            if (minDuration.HasValue)
                vehicles = vehicles.Where(v => v.ExitTime != null &&
                     EF.Functions.DateDiffMinute(v.EntryTime, v.ExitTime) <= maxDuration);

            if (maxDuration.HasValue)
                vehicles = vehicles.Where(v => v.ExitTime != null &&
                    EF.Functions.DateDiffMinute(v.EntryTime, v.ExitTime) <= maxDuration);

            return await vehicles.ToListAsync();
        }


    }
}
