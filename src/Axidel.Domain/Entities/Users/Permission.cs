using Axidel.Domain.Commons;

namespace Axidel.Domain.Entities.Users;

public class Permission : Auditable
{
    public string Controller { get; set; }
    public string Action { get; set; }
}