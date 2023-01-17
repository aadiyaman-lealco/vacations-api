using VacationRental.Common.Models;

namespace VacationRental.Services.Interfaces
{
    public interface ICalendarService
    {
        CalendarViewModel GetDates(int rentalId, DateTime start, int nights);
    }
}
