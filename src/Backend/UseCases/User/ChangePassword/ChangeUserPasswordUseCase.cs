using PaymentControl.Dtos.Request.User;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Services.EncryptManangment;
using PaymentControl.Services.Validators.User;

namespace PaymentControl.UseCases.User.ChangePassword
{
    public class ChangeUserPasswordUseCase : IChangeUserPasswordUseCase
    {
        private readonly IEncryptManangment _encryptMananger;
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Entities.User> _genericRepository;
        public ChangeUserPasswordUseCase(IEncryptManangment encrypt, IUserRepository user, IGenericRepository<Entities.User> genericRepository)
        {
            _encryptMananger = encrypt;
            _userRepository = user;
            _genericRepository = genericRepository;
        }
        public async Task Execute(RequestChangeUserPassword request)
        {
            await Validate(request);
            var user = await _userRepository.GetByEmail(request.Email);
            user.Password = _encryptMananger.Encrypt(request.NewPassword);
            _genericRepository.Update(user);

        }

        private async Task Validate(RequestChangeUserPassword request)
        {
            var validator = new ChangeUserPasswordValidator();
            var result = await validator.ValidateAsync(request);
            var currentPasswordEncripted = _encryptMananger.Encrypt(request.CurrentPassword);
            var verifyUser = await _userRepository.VerifyEmail(request.Email);
            if (verifyUser == false)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessageException.EMAIL_INVALID));
            }
            else
            {
                var userEncriptedPassword = await _userRepository.GetByEmail(request.Email);
                if (_encryptMananger.Verify(request.CurrentPassword, userEncriptedPassword.Password) == false)
                {
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("password", ResourceMessageException.UNMATCH_PASSWORD));
                }

            }
            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(errors => errors.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessages);
            }
        }
    }
}
