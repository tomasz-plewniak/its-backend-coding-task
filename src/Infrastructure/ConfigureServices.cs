using ApplicationCore.Interfaces;
using ApplicationCore.Options;
using Infrastructure.CosmosDB;
using Infrastructure.SQLDatabase;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    private const string DefaultConnection = "DefaultConnection";

    private const string PartitionKey = "/id";
    
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(
            InitializeCosmosClientInstanceAsync(configuration.GetSection(CosmosDbOptions.Section)).GetAwaiter().GetResult());
        
        services.AddDbContext<AuditContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DefaultConnection),
                builder => builder.MigrationsAssembly(typeof(AuditContext).Assembly.FullName)));
        
        services
            .AddTransient<IClaimRepository, ClaimRepository>();
        services
            .AddTransient<ICoverRepository, CoverRepository>();

        services.AddTransient<IAuditerService, AuditerService>();
        
        return services;
    }
    
    static async Task<CosmosClient> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
    {
        CosmosDbOptions cosmosDbOptions = new();
        configurationSection.Bind(cosmosDbOptions);
        
        var client = new CosmosClient(cosmosDbOptions.Account, cosmosDbOptions.Key);
        
        var database = await client.CreateDatabaseIfNotExistsAsync(cosmosDbOptions.DatabaseName);
        
        await database.Database.CreateContainerIfNotExistsAsync(cosmosDbOptions.ClaimContainerName, PartitionKey);
        await database.Database.CreateContainerIfNotExistsAsync(cosmosDbOptions.CoverContainerName, PartitionKey);

        return client;
    }
}
