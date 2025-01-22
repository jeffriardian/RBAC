using System.ComponentModel.DataAnnotations;
using RBAC.Core.DTO.Permission;
using RBAC.Core.Entities;

namespace RBAC.Core.DTO.Role
{
    public class CreateRoleDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }
        public ICollection<PermissionDto> PermissionDtos { get; set; } = new List<PermissionDto>();
    }
}
