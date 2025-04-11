using Mapster;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Services.EncryptManangment;
using PaymentControl.Services.Token;
using PaymentControl.Services.Validators.User;

namespace PaymentControl.UseCases.User.Register
{
    public class RegisterUserCase : IRegisterUserCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptManangment _encryptManangment;
        private readonly ITokenServices _tokenServices;
        private readonly IGenericRepository<Entities.User> _genericRepository;
        public RegisterUserCase(IUserRepository userRepository, IEncryptManangment encryptManangment, ITokenServices tokenService, IGenericRepository<Entities.User> genericRepository)
        {
            _userRepository = userRepository;
            _encryptManangment = encryptManangment;
            _tokenServices = tokenService;
            _genericRepository = genericRepository;
        }

        public async Task<ResponseUserDto> ExecuteRegister(RequestRegisterUserDto user)
        {
            await Validate(user);
            string passwordEncrypted = _encryptManangment.Encrypt(user.Password);
            user.Password = passwordEncrypted;
            var userToRepository = new Entities.User();
            user.Adapt(userToRepository);
            var generatedToken = _tokenServices.TokenGenerator(user.Email);
            await _genericRepository.Add(userToRepository);
            var response = new ResponseUserDto();
            response.Name = user.Name;
            response.Token = generatedToken;
            return response;
        }

        private async Task Validate (RequestRegisterUserDto user)
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
 