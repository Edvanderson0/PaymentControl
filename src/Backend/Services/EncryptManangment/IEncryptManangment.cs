namespace PaymentControl.Services.EncryptManangment
{
    public interface IEncryptManangment
    {
        public string Encrypt(string password);

        public bool Verify(string passwordFromBody, string passwordHashFromDb);
    }
}
