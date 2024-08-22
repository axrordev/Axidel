using Axidel.Domain.Entities.Items;
using Axidel.Service.Configurations;

namespace Axidel.Service.Services.Items;

public interface IItemService
{
    ValueTask<Item> CreateAsync(Item item);
    ValueTask<Item> UpdateAsync(long id, Item item);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Item>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IEnumerable<Item>> GetAllAsync();
    ValueTask<Item> GetByIdAsync(long id);
}
