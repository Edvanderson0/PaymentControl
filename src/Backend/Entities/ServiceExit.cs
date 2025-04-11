using Microsoft.EntityFrameworkCore;

namespace PaymentControl.Entities
{
    [Keyless]
    public class ServiceExit
    {
        public ServiceEntrance ServiceId { get; set; } = default!;
        public DateOnly ExitDate { get; set; } = default!;
        public int Quantities { get; set; }
    }
}
