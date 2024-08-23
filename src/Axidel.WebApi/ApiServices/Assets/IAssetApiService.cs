using Axidel.WebApi.Models.Assets;

namespace Axidel.WebApi.ApiServices.Assets;

public interface IAssetApiService
{
    ValueTask<AssetViewModel> UploadAsync(IFormFile file, string fileType);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssetViewModel> GetByIdAsync(long id);
}