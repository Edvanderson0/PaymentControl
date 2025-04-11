using PaymentControl.Dtos.Request.User;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Services.Validators.User;

namespace PaymentControl.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Entities.User> _genericRepository;
        public UpdateUserUseCase(IUserRepository request, IGenericRepository<Entities.User> genericRepository)
        {
            _userRepository = request;
            _genericRepository = genericRepository;
        }

        public async Task ExecuteUpdate(RequestUpdateUserDto request)
        {
            await Validate(request);
            var user = await _genericRepository.GetById(request.Id);
            user.Email = request.Email;
            user.Name = request.Name;
            _genericRepository.Update(user);
        }

        private async Task Validate(RequestUpdateUserDto request)
        {
            var validator = new UpdateUserValidator();
            var result = await validator.ValidateAsync(request);

            var verifyUser = await _genericRepository.GetById(request.Id);
            if (verifyUser == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("Id", ResourceMessageException.INVALID_USER));
            }
            var verifyEmail = await _userRepository.VerifyEmail(request.Email);
            if (verifyEmail)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessageException.EMAIL_EXISTS));
            }
            if(result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(errors => errors.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);
            }
        }
    }
}
