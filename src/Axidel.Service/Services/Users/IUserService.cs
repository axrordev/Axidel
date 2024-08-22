﻿using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;

namespace Axidel.Service.Services.Users;

public interface IUserService
{
    ValueTask<User> CreateAsync(User user);
    ValueTask<User> ModifyAsync(long id, User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<User> GetAsync(long id);
    ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IEnumerable<User>> GetAllAsync();
    ValueTask<User> ChangePasswordAsync(string oldPasword, string newPassword, string confirmPassword);
    ValueTask<User> ChangeRoleAsync(long userId, long roleId);
}