using AutoMapper;
using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;
using Axidel.Service.Services.Users;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.ApiServices.Users;

public class UserApiService(IUserService userService, IMapper mapper) : IUserApiService
{
    public async ValueTask<UserViewModel> ModifyAsync(long id, UserUpdateModel updateModel)
    {
        var mappedUser = mapper.Map<User>(updateModel);
        var updatedUser = await userService.ModifyAsync(id, mappedUser);
        return mapper.Map<UserViewModel>(updatedUser);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await userService.DeleteAsync(id);
    }

    public async ValueTask<UserViewModel> GetAsync(long id)
    {
        return mapper.Map<UserViewModel>(await userService.GetAsync(id));
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var result = await userService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<UserViewModel>>(result);
    }

    public async ValueTask<UserViewModel> ChangePasswordAsync(string oldPasword, string newPassword, string confirmPassword)
    {
        var result = await userService.ChangePasswordAsync(oldPasword, newPassword, confirmPassword);
        return mapper.Map<UserViewModel>(result);
    }

    public async ValueTask<UserViewModel> ChangeRoleAsync(long userId, long roleId)
    {
        var result = await userService.ChangeRoleAsync(userId, roleId);
        return mapper.Map<UserViewModel>(result);
    }
}
