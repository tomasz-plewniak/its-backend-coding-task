using Infrastructure.SQLDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(
            InitializeCosmosClientInstanceAsync(configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
        
        services.AddDbContext<AuditContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(AuditContext).Assembly.FullName)));

        return services;
    }
    
    static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
    {
        string databaseName = configurationSection.GetSection("DatabaseName").Value;
        string containerName = configurationSection.GetSection("ContainerName").Value;
        string account = configurationSection.GetSection("Account").Value;
        string key = configurationSection.GetSection("Key").Value;
        Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
        CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
        Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        return cosmosDbService;
    }
}