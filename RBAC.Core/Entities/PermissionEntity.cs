using System.Text.Json.Serialization;

namespace RBAC.Core.Entities
{
    public class PermissionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();
    }
}
