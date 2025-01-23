using System.Text;
using RBAC.Core.Interfaces;

namespace RBAC.Infrastructure.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string username, string password)
        {
            // Implement password hashing (e.g., using bcrypt, SHA256, etc.)
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password)); // Simplified for demo
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            // Verify password (compare hash)
            var result = hashedPassword == Convert.ToBase64String(Encoding.UTF8.GetBytes(password)); // Simplified for demo
            return result;
        }
    }
}
