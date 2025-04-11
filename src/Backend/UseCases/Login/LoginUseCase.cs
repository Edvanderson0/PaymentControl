using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Services.EncryptManangment;
using PaymentControl.Services.Token;
using PaymentControl.Services.Validators.Login;

namespace PaymentControl.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository  _userRepository;
        private readonly IEncryptManangment _encryptManangment;
        private readonly ITokenServices _tokenServices;
        public LoginUseCase(IUserRepository userRepository, IEncryptManangment encryptManangment, ITokenServices tokenSevice)
        {
            _userRepository = userRepository;
            _encryptManangment = encryptManangment;
            _tokenServices = tokenSevice;
        }
        public async Task<ResponseLoginDto> DoLogin(RequestLoginDto request)
        {
            await Validate(request);
            return new ResponseLoginDto
            {
                Token = _tokenServices.TokenGenerator(request.Email)
            };

        }
        private async Task Validate(RequestLoginDto request)
        {
            var validator = new LoginValidator();
            var result = validator.Validate(request);
            var user = await _userRepository.GetByEmail(request.Email);
            if(user == null || _encryptManangment.Verify(request.Password, user.Password) == false)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("",
                    ResourceMessageException.EMAIL_OR_PASSWORD_INVALID));
            }
            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();  throw new InvalidLoginException(errorMessage);
            }
        }
    }
}
