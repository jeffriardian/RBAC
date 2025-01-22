using RBAC.Core.DTO.Permission;
using RBAC.Core.Entities;

namespace RBAC.Core.DTO.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<PermissionDto> PermissionDtos { get; set; } = new List<PermissionDto>();
    }
}
