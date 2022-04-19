using Microsoft.EntityFrameworkCore;
using DigiAviator.Infrastructure.Data;
using DigiAviator.Infrastructure.Data.Repositories;
using DigiAviator.Core.Contracts;
using DigiAviator.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IFlightPreparationService, FlightPreparationService>();
            services.AddScoped<IMedicalService, MedicalService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<ILogbookService, LogbookService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
