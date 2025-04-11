using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;

namespace PaymentControl.UseCases.User.Register
{
    public interface IRegisterUserCase
    {
        public Task<ResponseUserDto> ExecuteRegister(RequestRegisterUserDto user);

    }
}
