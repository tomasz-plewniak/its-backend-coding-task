﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.CosmosDB;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : Entity
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;

    public abstract string DatabaseId { get; }
    public abstract string ContainerId { get; }

    public GenericRepository(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
        _container = cosmosClient.GetContainer(DatabaseId, ContainerId);
    }

    public void Dispose()
    {
        _cosmosClient?.Dispose();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var query = _container.GetItemQueryIterator<TEntity>(new QueryDefinition("SELECT * FROM c"));
        var results = new List<TEntity>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }
    
    public async Task<TEntity> GetItemAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
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