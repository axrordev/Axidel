using Axidel.WebApi.Models.CollectionImages;

namespace Axidel.WebApi.ApiServices.CollectionImages;

public interface ICollectionImageApiService
{
    ValueTask<CollectionImageViewModel> CreateAsync(long adId, IFormFile file);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CollectionImageViewModel> GetByIdAsync(long id);
}
