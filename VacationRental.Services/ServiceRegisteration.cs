using Microsoft.Extensions.DependencyInjection;
using VacationRental.Services.Implementations;
using VacationRental.Services.Interfaces;

namespace VacationRental.Services
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddRequiredServices(this IServiceCollection services)
        {
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<ICalendarService, CalendarService>();
            services.AddTransient<IRentalService, RentalService>();
            return services;
        }
    }
}
