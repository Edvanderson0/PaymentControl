using Microsoft.EntityFrameworkCore;
using PaymentControl.Entities;

namespace PaymentControl.Infraestructure.Data
{
    public class PaymentControlDbContext : DbContext
    {
        public PaymentControlDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
