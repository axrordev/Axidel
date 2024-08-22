using AutoMapper;
using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;
using Axidel.Service.Services.Permissions;
using Axidel.WebApi.Models.Permissions;

namespace Axidel.WebApi.ApiServices.Permissions;
public class PermissionApiService(IPermissionService permissionService, IMapper mapper) : IPermissionApiService
{
    public async ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel createModel)
    {
        var createdPermission = await permissionService.CreateAsync(mapper.Map<Permission>(createModel));
        return mapper.Map<PermissionViewModel>(createdPermission);
    }

    public async ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel updateModel)
    {
        var updatedPermission = await permissionService.UpdateAsync(id, mapper.Map<Permission>(updateModel));
        return mapper.Map<PermissionViewModel>(updatedPermission);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await permissionService.DeleteAsync(id);
    }
    public async ValueTask<PermissionViewModel> GetByIdAsync(long id)
    {
        var result = await permissionService.GetByIdAsync(id);
        return mapper.Map<PermissionViewModel>(result);
    }

    public async ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter,
        string search = null)
    {
        var result = await permissionService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<PermissionViewModel>>(result);
    }
}