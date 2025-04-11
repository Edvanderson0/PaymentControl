using System.Net;

namespace PaymentControl.Exceptions.ExceptionBase.ExceptionFilter
{
    public class ValidatorErrorException : PaymentControlExceptions
    {
        private readonly IList<string> _errorMessage;
        public ValidatorErrorException(IList<string> message) : base(string.Empty)
        {
            _errorMessage = message;
        }

        public override IList<string> GetErrorMessages()
        {
            return _errorMessage;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
