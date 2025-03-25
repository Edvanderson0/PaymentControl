namespace PaymentControl.Infraestructure.Data.Repositories.User
{
    public interface IUserRepository
    {
        public Task Add(Entities.User user);
        public Task<bool> VerifyEmail(string email);
    }
}
