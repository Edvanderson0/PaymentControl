using FluentValidation;
using PaymentControl.Dtos.Request.User;
using PaymentControl.Exceptions;

namespace PaymentControl.Services.Validators.User
{
    public class ChangeUserPasswordValidator : AbstractValidator<RequestChangeUserPassword>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(password => password.CurrentPassword).NotEmpty().WithMessage(
                ResourceMessageException.PASSWORD_EMPTY);
            RuleFor(password => password.NewPassword).NotEmpty().WithMessage(
                ResourceMessageException.PASSWORD_EMPTY);
            RuleFor(email => email.Email).NotEmpty().WithMessage(
                ResourceMessageException.EMAIL_EMPTY);
            RuleFor(email => email.Email).EmailAddress().WithMessage(
                ResourceMessageException.EMAIL_INVALID);
        }
    }
}
