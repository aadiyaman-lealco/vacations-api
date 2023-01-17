using AutoMapper;
using VacationRental.Common.Models;

namespace VacationRental.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingBindingModel, BookingViewModel>()
                .ReverseMap();

            CreateMap<RentalBindingModel, RentalViewModel>()
                .ReverseMap();
        }
    }
}
