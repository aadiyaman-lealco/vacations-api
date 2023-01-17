using VacationRental.Common.Models;

namespace VacationRental.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool>? filter = null);
        T? Get(int id);
        bool HasKey(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        int Count();
    }
}
