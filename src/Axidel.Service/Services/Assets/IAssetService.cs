using Axidel.Domain.Entities.Commons;
using Microsoft.AspNetCore.Http;

namespace Axidel.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(IFormFile file, string fileType);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Asset> GetByIdAsync(long id);
}