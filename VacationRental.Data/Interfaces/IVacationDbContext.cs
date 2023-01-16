using VacationRental.Common.Models;

namespace VacationRental.Data.Interfaces
{
    public interface IVacationDbContext
    {
        IDictionary<int, RentalViewModel> Rentals { get; }
        IDictionary<int, BookingViewModel> Bookings { get; }
    }
}