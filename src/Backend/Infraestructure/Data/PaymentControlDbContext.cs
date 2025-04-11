using Microsoft.EntityFrameworkCore;
using PaymentControl.Entities;

namespace PaymentControl.Infraestructure.Data
{
    public class PaymentControlDbContext : DbContext
    {
        public PaymentControlDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<ServiceEntrance> Services { get; set; }
        public DbSet<ServiceExit> ServiceOutputs { get; set; }
        public DbSet<PaymentReceived> PaymentsReceived { get; set; }
    }
}
