using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.CosmosDB;

public class CoverRepository : GenericRepository<Cover>, ICoverRepository
{
    private readonly Container _container;

    public CoverRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
        _container = cosmosClient.GetContainer(DatabaseId, ContainerId);
    }

    public override string DatabaseId => "ClaimDb";
    
    public override string ContainerId => "Cover";
}
