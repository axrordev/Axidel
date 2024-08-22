using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;
using Axidel.Service.Exceptions;
using Axidel.Service.Extensions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.UserRolePermissions;

public class UserRolePermissionService(IUnitOfWork unitOfWork) : IUserRolePermissionService
{
    public async ValueTask<UserRolePermission> CreateAsync(UserRolePermission userRolePermission)
    {
        var alreadyExistUserRolePermission = await unitOfWork.UserRolePermissionRepository
               .SelectAsync(urp =>
                    urp.UserRoleId == userRolePermission.UserRoleId &&
                    urp.PermissionId == userRolePermission.PermissionId);
        if (alreadyExistUserRolePermission is not null)
            throw new AlreadyExistException($"This User Role Permission is already exist with this Id={userRolePermission.Id}");

        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(r => r.Id == userRolePermission.UserRoleId)
            ?? throw new NotFoundException("User role is not found!");

        var existPermission = await unitOfWork.PermissionRepository
            .SelectAsync(p => p.Id == userRolePermission.PermissionId)
            ?? throw new NotFoundException("Permission is not found!");

        userRolePermission.CreatedById = HttpContextHelper.GetUserId;
        var createdUserRolePermission = await unitOfWork.UserRolePermissionRepository.InsertAsync(userRolePermission);
        await unitOfWork.SaveAsync();
        return createdUserRolePermission;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUserRolePermission = await unitOfWork.UserRolePermissionRepository.SelectAsync(urp => urp.Id == id)
            ?? throw new NotFoundException($"This User Role Permission is not found with this ID={id}");

        await unitOfWork.UserRolePermissionRepository.DeleteAsync(existUserRolePermission);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<UserRolePermission>> GetAlByRoleIdAsync(long roleId)
    {
        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(expression: r => r.Id == roleId)
            ?? throw new NotFoundException("User role is not found!");

        return await unitOfWork.UserRolePermissionRepository
            .Select(expression: p => p.UserRoleId == roleId, includes: ["UserRole", "Permission"])
            .ToListAsync();
    }

    public async ValueTask<IEnumerable<UserRolePermission>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var userRolePermissions = unitOfWork.UserRolePermissionRepository
            .Select(includes: ["UserRole", "Permission"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            userRolePermissions = userRolePermissions.Where(userRolePermission =>
                userRolePermission.UserRole.Name.ToLower().Contains(search.ToLower()) ||
                userRolePermission.Permission.Action.ToLower().Contains(search.ToLower()) ||
                userRolePermission.Permission.Controller.ToLower().Contains(search.ToLower()));

        var pagedUserRoles = userRolePermissions.ToPaginateAsQueryable(@params);
        return await pagedUserRoles.ToListAsync();
    }

    public async ValueTask<IEnumerable<UserRolePermission>> GetAllAsync()
    {
        return await unitOfWork.UserRolePermissionRepository.Select(includes: ["Permission", "UserRole"]).ToListAsync();
    }

    public async ValueTask<UserRolePermission> GetByIdAsync(long id)
    {
        var existUserRolePermission = await unitOfWork.UserRolePermissionRepository
            .SelectAsync(expression: urp => urp.Id == id, includes: ["UserRole", "Permission"])
          ?? throw new NotFoundException($"This User Role Permission is not found with this ID={id}");

        return existUserRolePermission;
    }

    public async ValueTask<UserRolePermission> UpdateAsync(long id, UserRolePermission userRolePermission)
    {
        var existUserRolePermission = await unitOfWork.UserRolePermissionRepository.SelectAsync(urp => urp.Id == id)
               ?? throw new NotFoundException($"This User Role Permission is not found with this ID={id}");

        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(r => r.Id == userRolePermission.UserRoleId)
            ?? throw new NotFoundException("User role is not found!");

        var existPermission = await unitOfWork.PermissionRepository
            .SelectAsync(p => p.Id == userRolePermission.PermissionId)
            ?? throw new NotFoundException("Permission is not found!");

        var alreadyExistUserRolePermission = await unitOfWork.UserRolePermissionRepository
            .SelectAsync(urp => urp.Id != id && urp.UserRoleId == userRolePermission.UserRoleId && urp.PermissionId == userRolePermission.PermissionId);
        if (alreadyExistUserRolePermission is not null)
            throw new AlreadyExistException($"This User Role is already exist with this dates = {userRolePermission.UserRoleId} and {userRolePermission.PermissionId}");

        existUserRolePermission.UserRoleId = userRolePermission.UserRoleId;
        existUserRolePermission.PermissionId = userRolePermission.PermissionId;

        var updatedUserRolePermission = await unitOfWork.UserRolePermissionRepository.UpdateAsync(existUserRolePermission);
        await unitOfWork.SaveAsync();
        return updatedUserRolePermission;
    }
}