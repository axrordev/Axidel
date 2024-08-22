using AutoMapper;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Configurations;
using Axidel.Service.Services.Items;
using Axidel.WebApi.Models.Items;

namespace Axidel.WebApi.ApiServices.Items
{
    public class ItemApiService(IItemService itemService, IMapper mapper) : IItemApiService
    {
        public async ValueTask<ItemViewModel> CreateAsync(ItemCreateModel createModel)
        {
            var createdItem = await itemService.CreateAsync(mapper.Map<Item>(createModel));
            return mapper.Map<ItemViewModel>(createdItem);
        }

        public async ValueTask<ItemViewModel> UpdateAsync(long id, ItemUpdateModel updateModel)
        {
            var updatedItem = await itemService.UpdateAsync(id, mapper.Map<Item>(updateModel));
            return mapper.Map<ItemViewModel>(updatedItem);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            return await itemService.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<ItemViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
        {
            var items = await itemService.GetAllAsync(@params, filter, search);
            return mapper.Map<IEnumerable<ItemViewModel>>(items);
        }

        public async ValueTask<IEnumerable<ItemViewModel>> GetAllAsync()
        {
            var items = await itemService.GetAllAsync();
            return mapper.Map<IEnumerable<ItemViewModel>>(items);
        }

        public async ValueTask<ItemViewModel> GetByIdAsync(long id)
        {
            var item = await itemService.GetByIdAsync(id);
            return mapper.Map<ItemViewModel>(item);
        }
    }
}
