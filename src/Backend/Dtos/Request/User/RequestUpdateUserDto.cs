using System.Globalization;

namespace PaymentControl.Dtos.Request.User
{
    public class RequestUpdateUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
