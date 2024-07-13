namespace Contacts_App.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashed);
        string TokenDecoder(string token);
    }
}
