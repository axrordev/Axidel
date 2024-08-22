using Axidel.Service.Configurations;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.ApiServices.Users;

public interface IUserApiService
{
    ValueTask<UserViewModel> ModifyAsync(long id, UserUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<UserViewModel> ChangePasswordAsync(string oldPasword, string newPassword, string confirmPassword);
    ValueTask<UserViewModel> ChangeRoleAsync(long userId, long roleId);
}