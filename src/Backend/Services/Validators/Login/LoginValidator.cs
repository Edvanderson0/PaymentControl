using FluentValidation;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Exceptions;

namespace PaymentControl.Services.Validators.Login
{
    public class LoginValidator : AbstractValidator<RequestLoginDto>
    {
        public LoginValidator()
        {
        }
    }
}
