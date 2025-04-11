using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;

namespace PaymentControl.UseCases.Login
{
    public interface ILoginUseCase
    {
        public Task<ResponseLoginDto> DoLogin(RequestLoginDto request);
    }
}
