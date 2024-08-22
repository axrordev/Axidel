using Axidel.Service.Configurations;
using Axidel.WebApi.Models.Items;

public interface IItemApiService
{
    ValueTask<ItemViewModel> CreateAsync(ItemCreateModel createModel);
    ValueTask<ItemViewModel> UpdateAsync(long id, ItemUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<ItemViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IEnumerable<ItemViewModel>> GetAllAsync();
    ValueTask<ItemViewModel> GetByIdAsync(long id);
}