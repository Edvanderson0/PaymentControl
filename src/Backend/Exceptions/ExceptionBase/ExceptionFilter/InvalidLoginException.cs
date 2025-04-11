using System.Net;

namespace PaymentControl.Exceptions.ExceptionBase.ExceptionFilter
{
    public class InvalidLoginException : PaymentControlExceptions
    {
        private IList<string> _errorMessage;
        public InvalidLoginException(IList<string> message) : base(string.Empty)
        {
            _errorMessage = message;
        }

        public override IList<string> GetErrorMessages()
        {
            return _errorMessage;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.Unauthorized;
        }
    }
}
