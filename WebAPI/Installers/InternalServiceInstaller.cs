using Application;
using Infrastructure;

namespace WebAPI.Installers
{
    public static class InternalServiceInstaller
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfrastructure(configuration);
            services.AddCors();
           
            return services;
        }
    }
}
