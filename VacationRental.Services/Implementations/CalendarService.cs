using VacationRental.Common.Models;
using VacationRental.Services.Interfaces;

namespace VacationRental.Services.Implementations
{
    public class CalendarService : ICalendarService
    {
        private readonly IBookingService _bookingService;
        private readonly IRentalService _rentalService;

        public CalendarService(IBookingService bookingService, IRentalService rentalService)
        {
            _bookingService = bookingService;
            _rentalService = rentalService;
        }

        public CalendarViewModel GetDates(int rentalId, DateTime start, int nights)
        {
            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>()
            };

            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes = new List<PreparationTimesViewModel>()
                };

                foreach (var booking in _bookingService.GetAllByRentalId(rentalId))
                {
                    if (booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id, Unit = booking.Units });
                    }

                    if (booking.Start <= date.Date && booking.Start.AddDays(booking.Nights + booking.PreparationTimeInDays) > date.Date)
                    {
                        date.PreparationTimes.Add(new PreparationTimesViewModel { Unit = booking.Units });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
