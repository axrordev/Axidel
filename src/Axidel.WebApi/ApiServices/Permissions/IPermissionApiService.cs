using Axidel.Service.Configurations;
using Axidel.WebApi.Models.Permissions;

namespace Axidel.WebApi.ApiServices.Permissions;

public interface IPermissionApiService
{
    ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel createModel);
    ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PermissionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}