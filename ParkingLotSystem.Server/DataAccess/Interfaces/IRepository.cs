using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotSystem.Server.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class  //generic repository pattern kullanarak veri erişim işlemleri için ortak bir yapı sağlar. T yerine user,vehicle,ve user verimodeli gelebeilir.
    {
        Task<IEnumerable<T>> GetAllAsync();  //tüm kayıtları veritabanından getirir liste (IEnumarable) olarak döndürür.
        Task<T> GetByIdAsync(int id);  //Id'ye göre ilgili kayıdı getirir.
        Task AddAsync(T entity);   //veritabanına yeni bir kayıt ekler
        Task UpdateAsync(T entity);  //kayıtı günceller.
        Task DeleteAsync(int id);  //kayıt silme 
    }
}
