using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.ApiServices.Accounts;

public interface IAccountApiService
{
    ValueTask RegisterAsync(UserRegisterModel registerModel);
    ValueTask<LoginViewModel> RegisterVerifyAsync(string email, string code, string password);
    ValueTask<LoginViewModel> LoginAsync(string email, string password);
    ValueTask<bool> SendCodeAsync(string email);
    ValueTask<bool> VerifyAsync(string email, string code);
    ValueTask<UserViewModel> ResetPasswordAsync(string email, string newPassword);
}