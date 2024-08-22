using Axidel.Service.Services.UserRolePermissions;
using Axidel.Service.Services.UserRoles;

namespace Axidel.WebApi.Helpers;

public static class ServiceHelper
{
    public static IUserRoleService UserRoleService { get; set; }
    public static IUserRolePermissionService UserRolePermissionService { get; set; }
}
