using Axidel.WebApi.Models.Itemtags;
using Axidel.WebApi.Models.Tags;

public interface IItemTagApiService
{
    ValueTask<ItemTagViewModel> AddTagToItemAsync(ItemTagCreateModel createModel);
    ValueTask<bool> RemoveTagFromItemAsync(long itemId, long tagId);
    ValueTask<IEnumerable<TagViewModel>> GetTagsByItemIdAsync(long itemId);
}