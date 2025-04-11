using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentControl.Dtos.Response.ErrorResponse;

namespace PaymentControl.Exceptions.ExceptionBase.ExceptionFilter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            if(context.Exception is PaymentControlExceptions myPaymentControl)
            {
                ExceptionHandler(context, myPaymentControl);
            }
            else
            {
                UnknowException(context);
            }

        }
        private static void ExceptionHandler(ExceptionContext context, PaymentControlExceptions myPaymentControlException)
        {
            context.HttpContext.Response.StatusCode = (int)myPaymentControlException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorDto(myPaymentControlException.GetErrorMessages()));
        }
        private static void UnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorDto(ResourceMessageException.UNKNOWN_ERROR));
        }

    }

}
