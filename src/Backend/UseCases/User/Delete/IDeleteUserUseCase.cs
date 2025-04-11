using PaymentControl.Dtos.Request.User;

namespace PaymentControl.UseCases.User.Delete
{
    public interface IDeleteUserUseCase
    {
        public Task Delete(RequestDeleteUserDto request);
    }
}
