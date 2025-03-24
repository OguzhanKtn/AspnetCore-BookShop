using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorage"));
        services.AddSingleton<IAzureStorageService, AzureStorageService>();
        return services;
    }
}
