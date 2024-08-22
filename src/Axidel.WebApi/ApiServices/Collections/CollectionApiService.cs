using AutoMapper;
using Axidel.Domain.Entities.Collections;
using Axidel.Service.Configurations;
using Axidel.Service.Services.Collections;
using Axidel.WebApi.Models.Collections;

namespace Axidel.WebApi.ApiServices.Collections;

public class CollectionApiService(ICollectionService collectionService, IMapper mapper) : ICollectionApiService
{
    public async ValueTask<CollectionViewModel> CreateAsync(CollectionCreateModel createModel)
    {
        var createdCollection = await collectionService.CreateAsync(mapper.Map<Collection>(createModel));
        return mapper.Map<CollectionViewModel>(createdCollection);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await collectionService.DeleteAsync(id);
    }

    public async ValueTask<IEnumerable<CollectionViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter,
        string search = null)
    {
        var result = await collectionService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<CollectionViewModel>>(result);
    }

    public async ValueTask<CollectionViewModel> GetByIdAsync(long id)
    {
        var result = await collectionService.GetByIdAsync(id);
        return mapper.Map<CollectionViewModel>(result);
    }

    public async ValueTask<CollectionViewModel> UpdateAsync(long id, CollectionUpdateModel updateModel)
    {
        var updatedCollection = await collectionService.UpdateAsync(id, mapper.Map<Collection>(updateModel));
        return mapper.Map<CollectionViewModel>(updatedCollection);
    }
}