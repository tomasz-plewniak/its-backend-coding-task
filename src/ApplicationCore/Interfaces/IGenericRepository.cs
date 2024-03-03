using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : Entity
{
    string DatabaseId { get; }
 
    string ContainerId { get; }
    
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task AddItemAsync(TEntity item);

    
    Task<TEntity> GetItemAsync(string id);


    Task DeleteItemAsync(string id);
}
