using VacationRental.Common.Models;
using VacationRental.Data.Interfaces;

namespace VacationRental.Data.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IVacationDbContext _dbContext;

        public BookingRepository(IVacationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(BookingViewModel entity)
        {
            _dbContext.Bookings.Add(entity.Id, entity);
        }

        public int Count()
        {
            return _dbContext.Bookings.Keys.Count;
        }

        public void Delete(int id)
        {
            _dbContext.Bookings.Remove(id);
        }

        public BookingViewModel? Get(int id)
        {
            _dbContext.Bookings.TryGetValue(id, out var data);

            return data;
        }

        public IEnumerable<BookingViewModel> GetAll(Func<BookingViewModel, bool>? filter = null)
        {
            var query = _dbContext.Bookings.Values.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            return query.ToArray();
        }

        public bool HasKey(int id)
        {
            return _dbContext.Bookings.ContainsKey(id);
        }

        public void Update(BookingViewModel entity)
        {
            _dbContext.Bookings[entity.Id] = entity;
        }
    }
}
