using PaymentControl.Dtos.Request.User;

namespace PaymentControl.UseCases.User.ChangePassword
{
    public interface IChangeUserPasswordUseCase
    {
        public Task Execute(RequestChangeUserPassword changePassword);
    }
}
