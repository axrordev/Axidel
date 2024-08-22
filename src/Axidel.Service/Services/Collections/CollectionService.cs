using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Collections;
using Axidel.Service.Configurations;
using Axidel.Service.Exceptions;
using Axidel.Service.Extensions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.Collections;

public class CollectionService(IUnitOfWork unitOfWork) : ICollectionService
{
    public async ValueTask<Collection> CreateAsync(Collection collection)
    {
        var alreadyExistCollection = await unitOfWork.CollectionRepository
            .SelectAsync(c => c.Name.ToLower() == collection.Name.ToLower());
        if (alreadyExistCollection is not null)
            throw new AlreadyExistException($"This Collection is already exist with this name={collection.Name}");

        collection.CreatedById = HttpContextHelper.GetUserId;
        var createdCollection = await unitOfWork.CollectionRepository.InsertAsync(collection);
        await unitOfWork.SaveAsync();
        return createdCollection;
    }

    public async ValueTask<Collection> UpdateAsync(long id, Collection collection)
    {
        var existCollection = await unitOfWork.CollectionRepository.SelectAsync(c => c.Id == id)
             ?? throw new NotFoundException($"This Collection  is not found with this ID={id}");

        var alreadyExistCollection = await unitOfWork.CollectionRepository
            .SelectAsync(c => c.Id != id && c.Name.ToLower() == collection.Name.ToLower());
        if (alreadyExistCollection is not null)
            throw new AlreadyExistException($"This Collection is already exist with this name={collection.Name}");

        existCollection.Name = collection.Name;
        existCollection.Description = collection.Description;
        existCollection.Category = collection.Category;

        var updatedCollection = await unitOfWork.CollectionRepository.UpdateAsync(existCollection);
        await unitOfWork.SaveAsync();
        return updatedCollection;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCollection = await unitOfWork.CollectionRepository.SelectAsync(c => c.Id == id)
             ?? throw new NotFoundException($"This Collection is not found with this ID={id}");

        await unitOfWork.CollectionRepository.DeleteAsync(existCollection);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<Collection>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var collections = unitOfWork.CollectionRepository.Select(includes: ["Items", "CustomFields", "Image", "User"]).OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            collections = collections.Where(c => c.Name.ToLower().Contains(search.ToLower()));

        var pagedCollections = collections.ToPaginateAsQueryable(@params);
        return await pagedCollections.ToListAsync();
    }

    public async ValueTask<IEnumerable<Collection>> GetAllAsync()
    {
        return await unitOfWork.CollectionRepository.Select(includes: ["Items", "CustomFields", "Image", "User"]).ToListAsync();
    }

    public async ValueTask<Collection> GetByIdAsync(long id)
    {
        var existCollection = await unitOfWork.CollectionRepository.SelectAsync(c => c.Id == id, includes: ["Items", "CustomFields", "Image", "User"])
            ?? throw new NotFoundException($"This Collection is not found with this ID={id}");

        return existCollection;
    }
}