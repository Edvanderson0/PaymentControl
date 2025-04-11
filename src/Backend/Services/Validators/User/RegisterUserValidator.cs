using FluentValidation;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Exceptions;
namespace PaymentControl.Services.Validators.User
{
    public class RegisterUserValidator : AbstractValidator<RequestUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
            RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceMessageException.PASSWORD_EMPTY);
        }
    }
}
