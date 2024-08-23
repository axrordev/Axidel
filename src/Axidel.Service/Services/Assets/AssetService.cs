//using Axidel.Data.UnitOfWorks;
//using Axidel.Domain.Entities.Commons;
//using Axidel.Service.Exceptions;
//using Axidel.Service.Helpers;
//using Microsoft.AspNetCore.Http;

//namespace Axidel.Service.Services.Assets;

//public class AssetService(IUnitOfWork unitOfWork) : IAssetService
//{
//    public async ValueTask<Asset> UploadAsync(IFormFile file, string fileType)
//    {
//        var path = Path.Combine(FilePathHelper.WwwrootPath, fileType.ToString());
//        var fileName = file.FileName;

//        if (!Directory.Exists(path))
//            Directory.CreateDirectory(path);

//        var fullPath = Path.Combine(path, fileName);

//        var stream = File.Create(fullPath);
//        await file.CopyToAsync(stream);
//        stream.Close();

//        var asset = new Asset
//        {
//            FileName = fileName,
//            FilePath = fullPath,
//        };

//        await unitOfWork.AssetRepository.InsertAsync(asset);
//        await unitOfWork.SaveAsync();

//        return asset;
//    }

//    public async ValueTask<bool> DeleteAsync(long id)
//    {
//        var existAsset = await unitOfWork.AssetRepository.SelectAsync(file => file.Id == id)
//           ?? throw new NotFoundException("Asset is not found");

//        File.Delete(existAsset.FilePath);

//        existAsset.DeletedAt = DateTime.UtcNow;
//        await unitOfWork.AssetRepository.DeleteAsync(existAsset);
//        await unitOfWork.SaveAsync();

//        return true;
//    }

//    public async ValueTask<Asset> GetByIdAsync(long id)
//    {
//        var existFile = await unitOfWork.AssetRepository.SelectAsync(file => file.Id == id)
//            ?? throw new NotFoundException($"File is not found with this ID={id}");

//        return existFile;
//    }
//}
using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Commons;
using Axidel.Service.Exceptions;
using Axidel.Service.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Axidel.Service.Services.Assets
{
    public class AssetService(IUnitOfWork unitOfWork) : IAssetService
    {

        public async ValueTask<Asset> UploadAsync(IFormFile file, string fileType)
        {
            var path = Path.Combine(FilePathHelper.WwwrootPath, fileType);
            var fileName = file.FileName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var asset = new Asset
            {
                FileName = fileName,
                FilePath = fullPath,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await unitOfWork.AssetRepository.InsertAsync(asset);
            await unitOfWork.SaveAsync();

            return asset;
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var existAsset = await unitOfWork.AssetRepository.SelectAsync(file => file.Id == id)
               ?? throw new NotFoundException("Asset is not found");

            if (File.Exists(existAsset.FilePath))
            {
                File.Delete(existAsset.FilePath);
            }

            existAsset.DeletedAt = DateTime.UtcNow;
            await unitOfWork.AssetRepository.DeleteAsync(existAsset);
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
}
