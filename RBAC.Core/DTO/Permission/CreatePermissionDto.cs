using System.ComponentModel.DataAnnotations;

namespace RBAC.Core.DTO.Permission
{
    public class CreatePermissionDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }
    }
}
