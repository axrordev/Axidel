using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Commons;
using Axidel.Service.Exceptions;
using Axidel.Service.Helpers;
using Microsoft.AspNetCore.Http;

namespace Axidel.Service.Services.Assets;

public class AssetService(IUnitOfWork unitOfWork) : IAssetService
{
    public async ValueTask<Asset> UploadAsync(IFormFile file, string fileType)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, fileType);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, file.FileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        await fileStream.WriteAsync(bytes);

        var asset = new Asset
        {
            FilePath = fullPath,
            FileName = file.FileName,
            CreatedById = HttpContextHelper.GetUserId
        };

        var createdAsset = await unitOfWork.AssetRepository.InsertAsync(asset);
        await unitOfWork.SaveAsync();

        return createdAsset;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existFile = await unitOfWork.AssetRepository.SelectAsync(file => file.Id == id)
            ?? throw new NotFoundException($"File is not found with this ID={id}");

        await unitOfWork.AssetRepository.DeleteAsync(existFile);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Asset> GetByIdAsync(long id)
    {
        var existFile = await unitOfWork.AssetRepository.SelectAsync(file => file.Id == id)
            ?? throw new NotFoundException($"File is not found with this ID={id}");

        return existFile;
    }
}