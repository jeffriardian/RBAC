namespace RBAC.Core.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
