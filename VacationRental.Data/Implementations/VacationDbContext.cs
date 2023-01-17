using VacationRental.Common.Models;
using VacationRental.Data.Interfaces;

namespace VacationRental.Data.Implementations
{
    public class VacationDbContext : IVacationDbContext
    {
        public VacationDbContext()
        {
            Rentals = new Dictionary<int, RentalViewModel>();
            Bookings = new Dictionary<int, BookingViewModel>();
        }

        public IDictionary<int, RentalViewModel> Rentals { get; }

        public IDictionary<int, BookingViewModel> Bookings { get; }
    }
}
