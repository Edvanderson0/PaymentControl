using Microsoft.EntityFrameworkCore;

namespace PaymentControl.Infraestructure.Data.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly PaymentControlDbContext _context;

        public UserRepository(PaymentControlDbContext context)
        {
            _context = context;
        }

        public async Task Add(Entities.User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> VerifyEmail(string email)
        {
            var ifExists = await _context.Users.AnyAsync(user => user.Email == email);
            return ifExists;
        }
        public async Task<Entities.User> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }
    }
}
