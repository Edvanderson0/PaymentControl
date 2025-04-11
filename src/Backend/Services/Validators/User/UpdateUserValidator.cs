using FluentValidation;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Request.User;
using PaymentControl.Exceptions;

namespace PaymentControl.Services.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Id).NotEmpty().WithMessage(
                ResourceMessageException.INVALID_ID).GreaterThan(0);
            RuleFor(user => user.Name).NotEmpty().WithMessage
                (ResourceMessageException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage
                (ResourceMessageException.EMAIL_EMPTY);
            RuleFor(user => user.Email).EmailAddress().WithMessage(
                ResourceMessageException.EMAIL_INVALID);
        }
    }
}
