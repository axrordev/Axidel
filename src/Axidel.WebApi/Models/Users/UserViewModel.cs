using Axidel.WebApi.Models.UserRoles;

namespace Axidel.WebApi.Models.Users;

public record UserViewModel(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    UserRoleViewModel Role);