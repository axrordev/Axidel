using Axidel.Domain.Commons;

namespace Axidel.Domain.Entities.Users;

public class UserRolePermission : Auditable
{
    public long UserRoleId { get; set; }
    public UserRole UserRole { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}
