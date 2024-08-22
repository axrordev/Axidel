using Axidel.WebApi.Helpers;
using FluentValidation;

namespace Axidel.WebApi.Validators.Accounts;

public class AccountCreateValidator : AbstractValidator<string>
{
    public AccountCreateValidator()
    {
        RuleFor(model => model)
            .NotNull()
            .Must(ValidationHelper.IsValidEmail)
            .WithMessage("Email is not valid");
    }
}
