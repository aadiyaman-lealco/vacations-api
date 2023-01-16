using VacationRental.Common.Models;
using VacationRental.Data.Interfaces;
using VacationRental.Services.Interfaces;

namespace VacationRental.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

        public void Create(BookingViewModel entity)
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public bool Exists(int id)
        {
            return _repository.HasKey(id);
        }

        public IEnumerable<BookingViewModel> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<BookingViewModel> GetAllByRentalId(int rentalId)
        {
            return _repository.GetAll(x => x.RentalId == rentalId);
        }

        public BookingViewModel? GetById(int id)
        {
            return _repository.Get(id);
        }

        public int GetLastId()
        {
            return _repository.Count();
        }

        public void Save(BookingViewModel model)
        {
            _repository.Add(model);
        }

        public void Update(BookingViewModel entity)
        {
            _repository.Update(entity);
        }
    }
}
