using Axidel.Domain.Entities.Collections;
using Microsoft.AspNetCore.Http;

namespace Axidel.Service.Services.CollectionImages;

public interface ICollectionImageService
{
    ValueTask<CollectionImage> CreateAsync(long collectionId, IFormFile file);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CollectionImage> GetAsync(long id);
}
