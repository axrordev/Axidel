using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Tags;
using Axidel.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.ItemTags;

public class ItemTagService(IUnitOfWork unitOfWork) : IItemTagService
{
    public async ValueTask<ItemTag> AddTagToItemAsync(ItemTag itemTag)
    {
        var existingItemTag = await unitOfWork.ItemTagRepository
            .SelectAsync(it => it.ItemId == itemTag.ItemId && it.TagId == itemTag.TagId);

        if (existingItemTag != null)
            throw new AlreadyExistException("This item is already associated with this tag.");

        var createdItemTag = await unitOfWork.ItemTagRepository.InsertAsync(itemTag);
        await unitOfWork.SaveAsync();
        return createdItemTag;
    }

    public async ValueTask<bool> RemoveTagFromItemAsync(long itemId, long tagId)
    {
        var existingItemTag = await unitOfWork.ItemTagRepository
            .SelectAsync(it => it.ItemId == itemId && it.TagId == tagId)
            ?? throw new NotFoundException("This item-tag association does not exist.");

        await unitOfWork.ItemTagRepository.DeleteAsync(existingItemTag);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<Tag>> GetTagsByItemIdAsync(long itemId)
    {
        return await unitOfWork.TagRepository
            .Select(t => t.ItemTags.Any(it => it.ItemId == itemId))
            .ToListAsync();
    }
}