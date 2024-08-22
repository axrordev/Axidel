using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Collections;

namespace Axidel.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsBlocked { get; set; }
    public long RoleId { get; set; }
    public UserRole Role { get; set; }
    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
