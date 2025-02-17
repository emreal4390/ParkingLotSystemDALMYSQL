using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.Server.Core.Entities; 

namespace ParkingLotSystem.DataAccess.Contexts
{
    public class ParkingLotSystemDbContext : DbContext
    {
        public ParkingLotSystemDbContext(DbContextOptions<ParkingLotSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Site> Sites { get; set; }
        
    }
}
