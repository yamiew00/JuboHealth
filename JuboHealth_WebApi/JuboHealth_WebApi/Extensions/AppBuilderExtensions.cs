using JuboHealth_WebApi.Services.Implementations;
using JuboHealth_WebApi.Services.Interfaces;

namespace JuboHealth_WebApi.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            //service layer
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
