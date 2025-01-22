using System.ComponentModel.DataAnnotations;

namespace RBAC.Core.DTO.User
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        public string Username { get; set; } = string.Empty;
        [Required]

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 100 characters.")]
        public string? Email { get; set; }
        [Required]

        [StringLength(100, MinimumLength = 3, ErrorMessage = "PasswordHash must be between 3 and 100 characters.")]
        public string? PasswordHash { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}
