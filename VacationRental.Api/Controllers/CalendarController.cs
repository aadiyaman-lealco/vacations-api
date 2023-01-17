using Microsoft.AspNetCore.Mvc;
using System;
using VacationRental.Common.Models;
using VacationRental.Services.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ICalendarService _calendarService;

        public CalendarController(IRentalService rentalService, ICalendarService calendarService)
        {
            _rentalService = rentalService;
            _calendarService = calendarService;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentalService.Exists(rentalId))
                throw new ApplicationException("Rental not found");

            return _calendarService.GetDates(rentalId, start, nights);
        }
    }
}
