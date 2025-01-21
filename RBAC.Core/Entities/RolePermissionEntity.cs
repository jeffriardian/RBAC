using System.Data;
using System.Security;

namespace RBAC.Core.Entities
{
    public class RolePermissionEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public DateTime CreatedAt { get; set; }

        public RoleEntity Role { get; set; }
        public PermissionEntity Permission { get; set; }
    }
}
