using Microsoft.Extensions.DependencyInjection;
using VacationRental.Data.Implementations;
using VacationRental.Data.Interfaces;

namespace VacationRental.Data
{
    public static class RegisterDataLayer
    {
        public static IServiceCollection AddRequiredDataServices(this IServiceCollection services)
        {
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddSingleton<IVacationDbContext, VacationDbContext>();

            return services;
        }
    }
}
