using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Users;
using Axidel.Service.Configurations;
using Axidel.Service.Exceptions;
using Axidel.Service.Extensions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Axidel.Service.Services.Permissions;

public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
{
    public async ValueTask<Permission> CreateAsync(Permission permission)
    {
        var existPermission = await unitOfWork.PermissionRepository
            .SelectAsync(p =>
                p.Controller.ToLower() == permission.Controller.ToLower() &&
                p.Action.ToLower() == permission.Action.ToLower());

        if (existPermission is not null)
            throw new AlreadyExistException("This permission is already exist");

        permission.CreatedById = HttpContextHelper.GetUserId;
        var createdPermission = await unitOfWork.PermissionRepository.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        return createdPermission;
    }

    public async ValueTask<Permission> UpdateAsync(long id, Permission permission)
    {
        var existPermission = await unitOfWork.PermissionRepository.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This permission is not found with this ID={id}");

        existPermission.Action = permission.Action;
        existPermission.Controller = permission.Controller;
        existPermission.UpdatedById = HttpContextHelper.GetUserId;

        var updatedPermission = await unitOfWork.PermissionRepository.UpdateAsync(existPermission);
        await unitOfWork.SaveAsync();

        return existPermission;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPermission = await unitOfWork.PermissionRepository.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This permission is not found with this ID={id}");

        await unitOfWork.PermissionRepository.DeleteAsync(existPermission);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Permission> GetByIdAsync(long id)
    {
        var existPermission = await unitOfWork.PermissionRepository.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This permission is not found with this ID={id}");

        return existPermission;
    }

    public async ValueTask<IEnumerable<Permission>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = unitOfWork.PermissionRepository.Select().OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            permissions = permissions.Where(permission =>
                permission.Action.ToLower().Contains(search.ToLower()) ||
                permission.Controller.ToLower().Contains(search.ToLower()));

        var pagedPemissions = permissions.ToPaginateAsQueryable(@params);
        return await pagedPemissions.ToListAsync();
    }

    public async ValueTask<IPagedList<Permission>> GetAllAsync(int? page, string search = null)
    {
        var permissions = unitOfWork.PermissionRepository.Select();

        if (!string.IsNullOrWhiteSpace(search))
            permissions = permissions.Where(p =>
                p.Action.ToLower().Contains(search.ToLower()) ||
                p.Controller.ToLower().Contains(search.ToLower()));

        return await permissions.ToPagedListAsync(page ?? 1, 20);
    }

    public async ValueTask<IEnumerable<Permission>> GetAllAsync()
    {
        return await unitOfWork.PermissionRepository.Select().ToListAsync();
    }
}