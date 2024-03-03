using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.CosmosDB;

public class ClaimRepository : GenericRepository<Claim>, IClaimRepository
{
    private readonly Container _container;

    public ClaimRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
        _container = cosmosClient.GetContainer(DatabaseId, ContainerId);
    }

    public override string DatabaseId => "ClaimDb";
    
    public override string ContainerId => "Claim";
}