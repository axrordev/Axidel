using Axidel.Service.Configurations;
using Axidel.WebApi.Models.UserRolePermissions;

namespace Axidel.WebApi.ApiServices.UserRolePermissions;

public interface IUserRolePermissionApiService
{
    ValueTask<UserRolePermissionViewModel> CreateAsync(UserRolePermissionCreateModel createModel);
    ValueTask<UserRolePermissionViewModel> UpdateAsync(long id, UserRolePermissionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserRolePermissionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserRolePermissionViewModel>> GetAlByRoleIdAsync(long roleId);
    ValueTask<IEnumerable<UserRolePermissionViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter,
        string search = null);
}