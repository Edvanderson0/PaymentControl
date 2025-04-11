using System.Linq.Expressions;

namespace PaymentControl.Infraestructure.Data.Repositories.User
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IList<TEntity>> GetAll();
        public Task Add(TEntity model);
        public Task<TEntity> GetById(int id);
        public void Update(TEntity model);
        public void Delete(TEntity model);
    }
}
