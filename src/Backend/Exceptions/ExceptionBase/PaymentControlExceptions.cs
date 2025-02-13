using System.Net;

namespace PaymentControl.Exceptions.ExceptionBase
{
    public abstract class PaymentControlExceptions : System.Exception
    {
        protected PaymentControlExceptions(string message) : base(message) { }
                                                            //Enviando para o construtor da Exception nativa.
        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();


    }
}
