using Mapster;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Services.Validators.User;

namespace PaymentControl.UseCases.User.Register
{
    public class RegisterUserCase : IRegisterUserCase
    {
        private IUserRepository _userRepository;
        public RegisterUserCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseUserDto> ExecuteRegister(RequestUserDto user)
        {
            await Validate(user);
            var userToRepository = new Entities.User();
            user.Adapt(userToRepository);
            await _userRepository.Add(userToRepository);
            var response = new ResponseUserDto();
            response.Name = user.Name;
            return response;
        }

        private async Task Validate (RequestUserDto user)
        {
            var validator = new RegisterUserValidator();
            var result = await validator.ValidateAsync(user);
            var emailExists = await _userRepository.VerifyEmail(user.Email);
            if (emailExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure
                    (
                        string.Empty,
                        ResourceMessageException.EMAIL_EXISTS
                    ));
            }
            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);               
            }
        }
    }
}
 