using PaymentControl.Dtos.Request.User;
using PaymentControl.Exceptions;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Services.Validators.User;

namespace PaymentControl.UseCases.User.Delete
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Entities.User> _genericRepository;
        public DeleteUserUseCase(IUserRepository user, IGenericRepository<Entities.User> genericRepository)
        {
            _userRepository = user;
            _genericRepository = genericRepository; 
        }

        public async Task Delete(RequestDeleteUserDto request)
        {
            await Validate(request);
            var user = await _genericRepository.GetById(request.Id);
            _genericRepository.Delete(user);
        }

        private async Task Validate(RequestDeleteUserDto request)
        {
            var validator = new DeleteUserValidator();
            var result = await validator.ValidateAsync(request);
            var user = await _genericRepository.GetById(request.Id);
            if (user == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure
                    (
                        string.Empty,
                        ResourceMessageException.INVALID_ID
                    ));
            }
            if(result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(errors => errors.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);
            }
        }
    }
}
