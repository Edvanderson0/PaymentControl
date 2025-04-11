using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PaymentControl.Infraestructure.Data.Repositories.User
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly PaymentControlDbContext _context;

        public GenericRepository(PaymentControlDbContext context)
        {
            _context = context;
        }

        public async Task<IList<TEntity>> GetAll()
        {
            var entitiesList = await _context.Set<TEntity>().ToListAsync();
            return entitiesList;
        }

        public async Task<bool> VerifyEmail(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }
        public async Task<TEntity> GetByEmail(Expression<Func<TEntity, bool>> predicate)
        {
            var entityByEmail = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return entityByEmail;
        }
        public async Task<TEntity> GetById(int id)
        {
            var entitie = await _context.Set<TEntity>().FindAsync(id);
            return entitie;
        }
        public async Task Add(TEntity model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public void Update(TEntity model)
        {
            var updatedUser = _context.Update(model);
            _context.SaveChanges();
        }

        public void Delete(TEntity model)
        {
            var deleteUser = _context.Remove(model);
            _context.SaveChanges();
        }

    }    
}
