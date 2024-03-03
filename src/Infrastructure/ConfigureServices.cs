using ApplicationCore.Interfaces;
using Infrastructure.CosmosDB;
using Infrastructure.SQLDatabase;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    private const string CosmosDbSection = "CosmosDb";
    private const string DefaultConnection = "DefaultConnection";
    private const string Account = "Account";
    private const string Key = "Key";
    
    private const string DatabaseId = "ClaimDb";
    private const string ClaimContainerName = "Claim";
    private const string CoverContainerName = "Cover";
    private const string PartitionKey = "/id";
    
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(
            InitializeCosmosClientInstanceAsync(configuration.GetSection(CosmosDbSection)).GetAwaiter().GetResult());
        
        services.AddDbContext<AuditContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DefaultConnection),
                builder => builder.MigrationsAssembly(typeof(AuditContext).Assembly.FullName)));
        
        services
            .AddTransient<IClaimRepository, ClaimRepository>();
        
        services
            .AddTransient<ICoverRepository, CoverRepository>();
        
        return services;
    }
    
    static async Task<CosmosClient> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
    {
        string account = configurationSection.GetSection(Account).Value;
        string key = configurationSection.GetSection(Key).Value;
        
        var client = new CosmosClient(account, key);
        
        var database = await client.CreateDatabaseIfNotExistsAsync(DatabaseId);
        await database.Database.CreateContainerIfNotExistsAsync(ClaimContainerName, PartitionKey);
        await database.Database.CreateContainerIfNotExistsAsync(CoverContainerName, PartitionKey);

        return client;
    }
}
