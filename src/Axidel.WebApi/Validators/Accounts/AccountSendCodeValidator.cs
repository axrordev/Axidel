using Axidel.WebApi.Helpers;
using FluentValidation;

namespace Axidel.WebApi.Validators.Accounts;

public class AccountSendCodeValidator : AbstractValidator<string>
{
    public AccountSendCodeValidator()
    {
        RuleFor(model => model)
            .NotNull()
            .Must(ValidationHelper.IsValidEmail)
            .WithMessage("Email is not valid");
    }
}
