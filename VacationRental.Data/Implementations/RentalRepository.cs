using System.Linq;
using VacationRental.Common.Models;
using VacationRental.Data.Interfaces;

namespace VacationRental.Data.Implementations
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IVacationDbContext _dbContext;

        public RentalRepository(IVacationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(RentalViewModel entity)
        {
            _dbContext.Rentals.Add(entity.Id, entity);
        }

        public int Count()
        {
            return _dbContext.Rentals.Keys.Count;
        }

        public void Delete(int id)
        {
            _dbContext.Rentals.Remove(id);
        }

        public RentalViewModel? Get(int id)
        {
            _dbContext.Rentals.TryGetValue(id, out var data);

            return data;
        }

        public IEnumerable<RentalViewModel> GetAll(Func<RentalViewModel, bool>? filter = null)
        {
            var query = _dbContext.Rentals.Values.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            return query.ToArray();
        }

        public bool HasKey(int id)
        {
            return _dbContext.Rentals.ContainsKey(id);
        }

        public void Update(RentalViewModel entity)
        {
            _dbContext.Rentals[entity.Id] = entity;
        }
    }
}
