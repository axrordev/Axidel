using AutoMapper;
using Axidel.Domain.Entities.Tags;
using Axidel.Service.Services.ItemTags;
using Axidel.WebApi.Models.Itemtags;
using Axidel.WebApi.Models.Tags;

namespace Axidel.WebApi.ApiServices.ItemTags
{
    public class ItemTagApiService(IItemTagService itemTagService, IMapper mapper) : IItemTagApiService
    {
        public async ValueTask<ItemTagViewModel> AddTagToItemAsync(ItemTagCreateModel createModel)
        {
            var itemTag = await itemTagService.AddTagToItemAsync(mapper.Map<ItemTag>(createModel));
            return mapper.Map<ItemTagViewModel>(itemTag);
        }

        public async ValueTask<bool> RemoveTagFromItemAsync(long itemId, long tagId)
        {
            return await itemTagService.RemoveTagFromItemAsync(itemId, tagId);
        }

        public async ValueTask<IEnumerable<TagViewModel>> GetTagsByItemIdAsync(long itemId)
        {
            var tags = await itemTagService.GetTagsByItemIdAsync(itemId);
            return mapper.Map<IEnumerable<TagViewModel>>(tags);
        }
    }
}
