using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;

namespace Axidel.Service.Services.UserRoles;

public interface IUserRoleService
{
    ValueTask<UserRole> CreateAsync(UserRole userRole);
    ValueTask<UserRole> UpdateAsync(long id, UserRole userRole);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserRole> GetByIdAsync(long id);
    ValueTask<UserRole> GetByNameAsync(string name);
    ValueTask<IEnumerable<UserRole>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IEnumerable<UserRole>> GetAllAsync();
}