using FluentValidation;
using NorthWeird.WebUI.ViewModel;

namespace NorthWeird.WebUI.Validation
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty();

            RuleFor(r => r.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(r => r.Password)
                .Equal(r => r.ConfirmPassword)
                .When(r => !string.IsNullOrEmpty(r.Password))
                .NotEmpty();
        }
    }
}
