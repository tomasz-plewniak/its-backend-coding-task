using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Infrastructure.CosmosDB;

public class CoverRepository : GenericRepository<Cover>, ICoverRepository
{
    public CoverRepository(
        CosmosClient cosmosClient,
        IOptions<CosmosDbOptions> options)
        : base(cosmosClient.GetContainer(options.Value.DatabaseName, options.Value.CoverContainerName))
    {
    }
}