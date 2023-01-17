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
            // Get rental details
            var rental = _rentalService.GetById(rentalId);
            if (rental == null)
            {
                throw new ApplicationException($"Rantal '{rentalId}' not found");
            }

            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>()
            };

            // Get bookings of the rental
            var bookingList = _bookingService.GetAllByRentalId(rentalId);

            // Generate the calender response
            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes = new List<PreparationTimesViewModel>()
                };

                foreach (var booking in bookingList)
                {
                    var dateOfNight = booking.Start.AddDays(booking.Nights);
                    if (booking.Start <= date.Date && dateOfNight > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id, Unit = 1 });
                    }

                    var dateOfPrep = dateOfNight.AddDays(rental.PreparationTimeInDays);
                    if (dateOfNight <= date.Date && dateOfPrep > date.Date)
                    {
                        date.PreparationTimes.Add(new PreparationTimesViewModel { Unit = 1 });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
