using Axidel.Domain.Entities.Tags;

namespace Axidel.Service.Services.ItemTags;

public interface IItemTagService
{
    ValueTask<ItemTag> AddTagToItemAsync(ItemTag itemTag);
    ValueTask<bool> RemoveTagFromItemAsync(long itemId, long tagId);
    ValueTask<IEnumerable<Tag>> GetTagsByItemIdAsync(long itemId);
}