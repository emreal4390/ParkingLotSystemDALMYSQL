using ParkingLotSystem.DataAccess.Contexts;
using ParkingLotSystem.DataAccess.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.Server.Core.Entities;

namespace ParkingLotSystem.DataAccess.Repositories
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
