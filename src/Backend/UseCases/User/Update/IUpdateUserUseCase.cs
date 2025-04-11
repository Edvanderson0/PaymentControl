using PaymentControl.Dtos.Request.User;

namespace PaymentControl.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task ExecuteUpdate(RequestUpdateUserDto request);
    }
}
