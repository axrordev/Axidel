using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Collections;
using Axidel.Service.Exceptions;
using Axidel.Service.Helpers;
using Axidel.Service.Services.Assets;
using Microsoft.AspNetCore.Http;

namespace Axidel.Service.Services.CollectionImages;

public class CollectionImageService(IUnitOfWork unitOfWork, IAssetService assetService) : ICollectionImageService
{
    public async ValueTask<CollectionImage> CreateAsync(long collectionId, IFormFile file)
    {
        var exsitCollection = await unitOfWork.CollectionRepository.SelectAsync(c => c.Id == collectionId)
            ?? throw new NotFoundException($"Collection is not found with this ID={collectionId}");

        var createdAsset = await assetService.UploadAsync(file, "images");

        var collectionImage = new CollectionImage
        {
            CollectionId = collectionId,
            ImageId = createdAsset.Id,
            CreatedById = HttpContextHelper.GetUserId
        };
        var createdImage = await unitOfWork.CollectionImageRepository.InsertAsync(collectionImage);
        await unitOfWork.SaveAsync();

        return createdImage;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existImage = await unitOfWork.CollectionImageRepository.SelectAsync(image => image.Id == id)
            ?? throw new NotFoundException($"Image is not found with this ID={id}");

        await unitOfWork.CollectionImageRepository.DeleteAsync(existImage);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<CollectionImage> GetAsync(long id)
    {
        var existImage = await unitOfWork.CollectionImageRepository
            .SelectAsync(expression: image => image.Id == id, includes: ["Image", "Ad"])
            ?? throw new NotFoundException($"Image is not found with this ID={id}");

        return existImage;
    }
}
