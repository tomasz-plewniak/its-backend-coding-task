using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IPremiumService, PremiumService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        
        return services;
    }
}