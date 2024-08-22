using Axidel.WebApi.Models.Permissions;

namespace Axidel.WebApi.Models.Users;

public class LoginViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public IEnumerable<PermissionViewModel> Permissions { get; set; }
}

