
using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;
using X.PagedList;

namespace Axidel.Service.Services.Permissions;

public interface IPermissionService
{
    ValueTask<Permission> CreateAsync(Permission permission);
    ValueTask<Permission> UpdateAsync(long id, Permission permission);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Permission> GetByIdAsync(long id);
    ValueTask<IEnumerable<Permission>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IPagedList<Permission>> GetAllAsync(int? page, string search = null);
    ValueTask<IEnumerable<Permission>> GetAllAsync();
}