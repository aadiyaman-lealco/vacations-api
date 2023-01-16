namespace VacationRental.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        bool Exists(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        int GetLastId();
    }
}
