namespace PaymentControl.Services.EncryptManangment
{
    public class EncryptManangment : IEncryptManangment
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string passwordFromBody, string passwordHashFromDb)
        {
            return BCrypt.Net.BCrypt.Verify(passwordFromBody, passwordHashFromDb);
        }    
    }
}
