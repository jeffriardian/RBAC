namespace RBAC.Core.Entities
{
    public class UserRoleEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserEntity User { get; set; }
        public RoleEntity Role { get; set; }
    }
}
