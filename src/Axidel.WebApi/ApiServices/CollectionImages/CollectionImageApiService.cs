using AutoMapper;
using Axidel.Service.Services.CollectionImages;
using Axidel.WebApi.Models.CollectionImages;

namespace Axidel.WebApi.ApiServices.CollectionImages;

public class CollectionImageApiService(ICollectionImageService adImageService, IMapper mapper) : ICollectionImageApiService
{
    public async ValueTask<CollectionImageViewModel> CreateAsync(long adId, IFormFile file)
    {
        var createdAdImage = await adImageService.CreateAsync(adId, file);
        return mapper.Map<CollectionImageViewModel>(createdAdImage);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await adImageService.DeleteAsync(id);
    }

    public async ValueTask<CollectionImageViewModel> GetByIdAsync(long id)
    {
        var adImage = await adImageService.GetAsync(id);
        return mapper.Map<CollectionImageViewModel>(adImage);
    }
}