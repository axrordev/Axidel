using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;
using Axidel.Service.Exceptions;
using Axidel.Service.Extensions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.Users;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async ValueTask<User> CreateAsync(User user)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(u => u.Phone == user.Phone && u.Email == user.Email);
        if (existUser?.Phone is not null)
        {
            throw new AlreadyExistException($"This user is already exist with this phone | Phone={user.Phone}");
        }
        else if (existUser?.Email is not null)
        {
            throw new AlreadyExistException($"This user is already exist with this email | Email={user.Email}");
        }
        else if (existUser?.Phone is not null && existUser?.Email is not null)
        {
            throw new AlreadyExistException($"This user is already exist with this email and phone | Email={user.Email} & Phone={existUser.Phone}");
        }

        var roleWhichIsUser = await unitOfWork.UserRoleRepository.SelectAsync(u => u.Name.ToLower() == "user");

        user.RoleId = roleWhichIsUser.Id;
        user.Password = PasswordHasher.Hash(user.Password);
        var createdUser = await unitOfWork.UserRepository.InsertAsync(user);
        await unitOfWork.SaveAsync();
        return createdUser;
    }

    public async ValueTask<User> ModifyAsync(long id, User user)
    {
        var exsitUser = await unitOfWork.UserRepository.SelectAsync(user => user.Id == id)
            ?? throw new NotFoundException("This user is not found");

        var alreadyExistUser = await unitOfWork.UserRepository
            .SelectAsync(u => u.Phone == user.Phone && u.Id != id);
        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"This user is already exist with this phone | Phone={user.Phone}");

        exsitUser.Email = user.Email;
        exsitUser.Phone = user.Phone;
        exsitUser.LastName = user.LastName;
        exsitUser.FirstName = user.FirstName;
        exsitUser.UpdatedById = HttpContextHelper.GetUserId;

        var updatedUser = await unitOfWork.UserRepository.UpdateAsync(exsitUser);
        await unitOfWork.SaveAsync();

        return updatedUser;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var exsitUser = await unitOfWork.UserRepository.SelectAsync(user => user.Id == id)
            ?? throw new NotFoundException("This user is not found");

        exsitUser.DeletedById = HttpContextHelper.GetUserId;
        await unitOfWork.UserRepository.DeleteAsync(exsitUser);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<User> GetAsync(long id)
    {
        return await unitOfWork.UserRepository
            .SelectAsync(expression: user => user.Id == id, includes: ["Role"])
            ?? throw new NotFoundException("This user is not found");
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.UserRepository.Select(isTracking: false, includes: ["Role"]);

        if (!string.IsNullOrWhiteSpace(search))
            users = users.Where(u =>
                u.Phone.ToString().Contains(search) ||
                u.Email.ToLower().Contains(search.ToLower()) ||
                u.LastName.ToLower().Contains(search.ToLower()) ||
                u.FirstName.ToLower().Contains(search.ToLower()));

        return await users.ToPaginateAsQueryable(@params).OrderBy(filter).ToListAsync();
    }

    public async ValueTask<User> ChangePasswordAsync(string oldPasword, string newPassword, string confirmPassword)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(user => user.Id == HttpContextHelper.GetUserId)
            ?? throw new NotFoundException("User is not found");

        if (!PasswordHasher.Verify(oldPasword, existUser.Password))
            throw new ArgumentIsNotValidException("Old password is invalid");

        if (newPassword != confirmPassword)
            throw new ArgumentIsNotValidException("Password is not match");

        existUser.Password = PasswordHasher.Hash(newPassword);
        await unitOfWork.UserRepository.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }

    public async ValueTask<User> ChangeRoleAsync(long userId, long roleId)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(user => user.Id == userId)
            ?? throw new NotFoundException($"User is not found with this ID={userId}");

        var existRole = await unitOfWork.UserRoleRepository.SelectAsync(role => role.Id == roleId)
            ?? throw new NotFoundException($"Role is not found with this ID={roleId}");

        existUser.RoleId = roleId;
        await unitOfWork.UserRepository.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync()
    {
        return await unitOfWork.UserRepository
            .Select(isTracking: false, includes: ["Role"])
            .ToListAsync();
    }
}