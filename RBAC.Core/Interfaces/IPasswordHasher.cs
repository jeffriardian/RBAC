namespace RBAC.Core.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string username, string password);
        bool VerifyPassword(string hashedPassword, string password);
    }
}
