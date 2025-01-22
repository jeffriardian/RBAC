using System.Text.Json.Serialization;

namespace RBAC.Core.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    }
}
