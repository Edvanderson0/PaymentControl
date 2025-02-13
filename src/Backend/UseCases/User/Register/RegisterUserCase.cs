using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase;
using PaymentControl.Services.LoginServices;

namespace PaymentControl.UseCases.User.Register
{
    public class RegisterUserCase : IRegisterUserCase
    {
        public ResponseUserDto ExecuteRegister(RequestUserDto user)
        {
            Validate(user);
            var response = new ResponseUserDto();
            response.Name = user.Name;
            return response;
        }

        private static void Validate (RequestUserDto user)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(user);
            
            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);               
            }
        }
    }
}
 