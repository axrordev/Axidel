using Axidel.Domain.Entities.Collections;
using Axidel.Service.Configurations;

namespace Axidel.Service.Services.Collections;

public interface ICollectionService
{
    ValueTask<Collection> CreateAsync(Collection collection);
    ValueTask<Collection> UpdateAsync(long id, Collection collection);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Collection> GetByIdAsync(long id);
    ValueTask<IEnumerable<Collection>> GetAllAsync(
        PaginationParams @params,
        Filter filter,
        string search = null);
    ValueTask<IEnumerable<Collection>> GetAllAsync();
}