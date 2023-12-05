namespace Application.AuthenticationHandlers.HashManager
{
    public interface IHashManager
    {
        void CreateHash(string password, out byte[] hash, out byte[] salt);
        bool VerifyHash(string password, byte[] hash, byte[] salt);
    }
}
