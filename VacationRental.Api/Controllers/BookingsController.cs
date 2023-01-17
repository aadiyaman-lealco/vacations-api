using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using VacationRental.Api.Models;
using VacationRental.Common.Models;
using VacationRental.Services.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;

        public BookingsController(IBookingService bookingService, IRentalService rentalService, IMapper mapper)
        {
            _bookingService = bookingService;
            _rentalService = rentalService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            if (!_bookingService.Exists(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookingService.GetById(bookingId);
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");

            var rental = _rentalService.GetById(model.RentalId);
            if (rental == null)
                throw new ApplicationException("Rental not found");

            //Add prepartion days
            var totalLockDays = model.Nights + rental.PreparationTimeInDays;
            for (var i = 0; i < totalLockDays; i++)
            {
                var count = 0;
                foreach (var booking in _bookingService.GetAllByRentalId(model.RentalId))
                {
                    if ((booking.Start <= model.Start.Date && booking.Start.AddDays(totalLockDays) > model.Start.Date)
                        || (booking.Start < model.Start.AddDays(totalLockDays) && booking.Start.AddDays(totalLockDays) >= model.Start.AddDays(totalLockDays))
                        || (booking.Start > model.Start && booking.Start.AddDays(totalLockDays) < model.Start.AddDays(totalLockDays)))
                    {
                        count++;
                    }
                }

                if (count >= rental.Units)
                {
                    throw new ApplicationException("Not available");
                }
            }

            var key = new ResourceIdViewModel { Id = _bookingService.GetLastId() + 1 };

            var mapped = _mapper.Map<BookingViewModel>(model);
            mapped.Id = key.Id;

            _bookingService.Create(mapped);

            return key;
        }
    }
}
