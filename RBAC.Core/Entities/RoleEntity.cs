using System.Text.Json.Serialization;

namespace RBAC.Core.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
        [JsonIgnore]
        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();
    }
}
