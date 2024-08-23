using AutoMapper;
using Axidel.Domain.Entities.Commons;
using Axidel.Service.Services.Assets;
using Axidel.WebApi.Models.Assets;

namespace Axidel.WebApi.ApiServices.Assets;

public class AssetApiService(IAssetService assetService, IMapper mapper) : IAssetApiService
{
    public async ValueTask<AssetViewModel> UploadAsync(IFormFile file, string fileType)
    {
        var createdImage = await assetService.UploadAsync(file, fileType);
        return mapper.Map<AssetViewModel>(createdImage);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await assetService.DeleteAsync(id);
    }

    public async ValueTask<AssetViewModel> GetByIdAsync(long id)
    {
        var image = await assetService.GetByIdAsync(id);
        return mapper.Map<AssetViewModel>(image);
    }
}