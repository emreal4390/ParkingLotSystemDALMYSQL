using ParkingLotSystem.Server.Core.Entities;
using System.Threading.Tasks;

namespace ParkingLotSystem.Server.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
