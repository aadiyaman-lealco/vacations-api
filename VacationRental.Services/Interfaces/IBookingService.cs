using VacationRental.Common.Models;

namespace VacationRental.Services.Interfaces
{
    public interface IBookingService : IService<BookingViewModel>
    {
        IEnumerable<BookingViewModel> GetAllByRentalId(int rentalId);
    }
}
