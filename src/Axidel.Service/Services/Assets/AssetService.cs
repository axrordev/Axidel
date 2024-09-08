using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Commons;
using Axidel.Service.Exceptions;
using Axidel.Service.Helpers;
using Microsoft.AspNetCore.Http;

namespace Axidel.Service.Services.Assets
{
	public class AssetService(IUnitOfWork unitOfWork) : IAssetService
	{
		public async ValueTask<Asset> UploadAsync(IFormFile file, string fileType)
		{
            // Faylni saqlash papkasini belgilash
            var path = Path.Combine(FilePathHelper.WwwrootPath, fileType);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Fayl nomini unikallash
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(path, uniqueFileName);

            // Faylni yuklash jarayoni
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Fayl ma'lumotlarini Asset obyektida saqlash
            var asset = new Asset
            {
                FileName = file.FileName,
                FilePath = $"/{fileType}/{uniqueFileName}", // Frontendda foydalanish uchun URL qaytariladi
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Ma'lumotlarni bazaga saqlash
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
