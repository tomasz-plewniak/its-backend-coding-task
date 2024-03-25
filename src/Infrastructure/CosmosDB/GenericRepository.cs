using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.CosmosDB;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : Entity
{
    private readonly Container _container;
    
    public GenericRepository(Container container)
    {
        _container = container;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = _container.GetItemQueryIterator<TEntity>(new QueryDefinition("SELECT * FROM c"));
        var results = new List<TEntity>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync(cancellationToken);

            results.AddRange(response.ToList());
        }

        return results;
    }
    
    public async Task<TEntity> GetItemAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null!;
        }
    }
    
    public Task AddItemAsync(TEntity item)
    {
        return _container.CreateItemAsync(item, new PartitionKey(item.Id));
    }
    
    public Task DeleteItemAsync(string id)
    {
        return _container.DeleteItemAsync<Claim>(id, new PartitionKey(id));
    }
}