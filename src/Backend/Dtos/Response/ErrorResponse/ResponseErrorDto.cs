namespace PaymentControl.Dtos.Response.ErrorResponse
{
    public class ResponseErrorDto
    {
        public IList<string> Errors { get; set; }
        public ResponseErrorDto(IList<string> errors)
        {
            Errors = errors;
        }
        public ResponseErrorDto(string errors)
        {
            Errors.Add(errors);

        }
    }
}
