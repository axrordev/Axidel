using Axidel.WebApi.Models.Permissions;
using Axidel.WebApi.Models.UserRoles;

namespace Axidel.WebApi.Models.UserRolePermissions;

public class UserRolePermissionViewModel
{
    public long Id { get; set; }
    public UserRoleViewModel UserRole { get; set; }
    public PermissionViewModel Permission { get; set; }
}
