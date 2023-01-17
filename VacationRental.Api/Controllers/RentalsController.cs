using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using VacationRental.Api.Models;
using VacationRental.Common.Configurations;
using VacationRental.Common.Models;
using VacationRental.Services.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly CovidConfigs _options;
        private readonly IMapper _mapper;

        public RentalsController(IRentalService rentalService, IOptions<CovidConfigs> options, IMapper mapper)
        {
            _rentalService = rentalService;
            _options = options?.Value;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            if (!_rentalService.Exists(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentalService.GetById(rentalId);
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            if (model.PreparationTimeInDays < _options.MinimumPreparationTimeInDays)
                throw new ApplicationException($"Preparation time in days cannot be lower than {_options.MinimumPreparationTimeInDays}");

            var key = new ResourceIdViewModel { Id = _rentalService.GetLastId() + 1 };

            var mapped = _mapper.Map<RentalViewModel>(model);
            mapped.Id = key.Id;

            _rentalService.Create(mapped);

            return key;
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public RentalViewModel Put(int rentalId, RentalBindingModel model)
        {
            if (!_rentalService.Exists(rentalId))
                throw new ApplicationException("Rental not found");

            var rental = _rentalService.GetById(rentalId);
            
            // Update rental
            rental.PreparationTimeInDays = model.PreparationTimeInDays;
            rental.Units = model.Units;
            _rentalService.Update(rental);

            return rental;
        }
    }
}
