using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Configurations;
using Axidel.Service.Exceptions;
using Axidel.Service.Extensions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.Items;

public class ItemService(IUnitOfWork unitOfWork) : IItemService
{
    public async ValueTask<Item> CreateAsync(Item item)
    {
        var existItem = await unitOfWork.ItemRepository
            .SelectAsync(i => i.Name.ToLower() == item.Name.ToLower());
        if (existItem is not null)
            throw new AlreadyExistException($"An item with the name '{item.Name}' already exists.");

        item.CreatedById = HttpContextHelper.GetUserId;
        var createdItem = await unitOfWork.ItemRepository.InsertAsync(item);
        await unitOfWork.SaveAsync();
        return createdItem;
    }

    public async ValueTask<Item> UpdateAsync(long id, Item item)
    {
        var existItem = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == id)
            ?? throw new NotFoundException($"Item not found with ID={id}");

        var alreadyExistItem = await unitOfWork.ItemRepository
            .SelectAsync(i => i.Id != id && i.Name.ToLower() == item.Name.ToLower());
        if (alreadyExistItem is not null)
            throw new AlreadyExistException($"An item with the name '{item.Name}' already exists.");

        existItem.Name = item.Name;

        var updatedItem = await unitOfWork.ItemRepository.UpdateAsync(existItem);
        await unitOfWork.SaveAsync();
        return updatedItem;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existItem = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == id)
            ?? throw new NotFoundException($"Item not found with ID={id}");

        await unitOfWork.ItemRepository.DeleteAsync(existItem);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<Item>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var items = unitOfWork.ItemRepository
            .Select(includes: ["Collection", "User", "Comments", "Likes", "ItemTags", "CustomFieldValues"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            items = items.Where(i => i.Name.ToLower().Contains(search.ToLower()));

        var pagedItems = items.ToPaginateAsQueryable(@params);
        return await pagedItems.ToListAsync();
    }

    public async ValueTask<IEnumerable<Item>> GetAllAsync()
    {
        return await unitOfWork.ItemRepository
            .Select(includes: ["Collection", "User", "Comments", "Likes", "ItemTags", "CustomFieldValues"])
            .ToListAsync();
    }

    public async ValueTask<Item> GetByIdAsync(long id)
    {
        var existItem = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == id, includes: ["Collection", "User", "Comments", "Likes", "ItemTags", "CustomFieldValues"])
            ?? throw new NotFoundException($"Item not found with ID={id}");

        return existItem;
    }
}