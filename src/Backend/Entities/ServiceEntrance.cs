namespace PaymentControl.Entities
{
    public class ServiceEntrance
    {
        public int Id { get; set; }
        public Clients IdClients { get; set; } = default!;
        public string ServiceType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateOnly EntranceDate { get; set; }
        public double UnitValue { get; set; }
        public double TotalValue { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
