using Axidel.Domain.Commons;

namespace Axidel.Domain.Entities.Users;

public class UserRole : Auditable
{
    public string Name { get; set; }
}
