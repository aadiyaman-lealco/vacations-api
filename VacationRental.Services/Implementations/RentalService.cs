using VacationRental.Common.Models;
using VacationRental.Data.Interfaces;
using VacationRental.Services.Interfaces;

namespace VacationRental.Services.Implementations
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _repository;

        public RentalService(IRentalRepository repository)
        {
            _repository = repository;
        }

        public void Create(RentalViewModel entity)
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

        public IEnumerable<RentalViewModel> GetAll()
        {
            return _repository.GetAll();
        }

        public RentalViewModel? GetById(int id)
        {
            return _repository.Get(id);
        }

        public int GetLastId()
        {
            return _repository.Count();
        }

        public void Save(RentalViewModel model)
        {
            _repository.Add(model);
        }

        public void Update(RentalViewModel entity)
        {
            _repository.Update(entity);
        }
    }
}
