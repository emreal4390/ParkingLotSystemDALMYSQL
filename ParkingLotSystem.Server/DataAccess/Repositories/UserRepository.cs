using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.Server.Core.Entities;
using ParkingLotSystem.Server.DataAccess.Contexts;
using ParkingLotSystem.Server.DataAccess.Interfaces;

namespace ParkingLotSystem.Server.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ParkingLotSystemDbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Site)  // Kullanıcının Site bilgisi de gelsin
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
