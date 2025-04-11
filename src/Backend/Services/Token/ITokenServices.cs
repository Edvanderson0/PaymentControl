
namespace PaymentControl.Services.Token
{
    public interface ITokenServices
    {
        public string TokenGenerator(string email);
    }
}
