using Microsoft.EntityFrameworkCore;

namespace PaymentControl.Entities
{
    [Keyless]
    public class PaymentReceived
    {
        public ServiceEntrance ServiceId { get; set; }
        public DateOnly DataPayment { get; set; }
        public double Value { get; set; }
    }
}
