using MediatR;
using Microsoft.AspNetCore.Mvc;
using RBAC.Application.Commands.Permission;
using RBAC.Application.Queries.Permission;
using RBAC.Core.DTO.Permission;
using RBAC.Core.JWT;

namespace RBAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(ISender sender) : ControllerBase
    {
        [HttpPost("permissions")]
        public async Task<IActionResult> AddPermissionAsync([FromBody] CreatePermissionDto permission)
        {
            var result = await sender.Send(new AddPermissionCommand(permission));
            return Ok(result);
        }

        [HttpGet("permissions")]
        [AuthorizeRolePermission(roles: new[] { "Admin" })]
        public async Task<IActionResult> GetAllPermissionsAsync()
        {
            var result = await sender.Send(new GetAllPermissionsQuery());
            return Ok(result);
        }

        [HttpGet("permissions/{permissionId:guid}")]
        public async Task<IActionResult> GetPermissionByIdAsync([FromRoute] Guid permissionId)
        {
            var result = await sender.Send(new GetPermissionByIdQuery(permissionId));

            if (result == null)
            {
                return NotFound(new { Message = "Permission not found." });
            }

            return Ok(result);
        }

        [HttpPut("permissions/{permissionId:guid}")]
        public async Task<IActionResult> UpdatePermissionAsync([FromRoute] Guid permissionId, [FromBody] UpdatePermissionDto permission)
        {
            var result = await sender.Send(new UpdatePermissionCommand(permissionId, permission));
            return Ok(result);
        }

        [HttpDelete("permissions/{permissionId:guid}")]
        public async Task<IActionResult> DeletePermissionAsync([FromRoute] Guid permissionId)
        {
            var result = await sender.Send(new DeletePermissionCommand(permissionId));
            return Ok(result);
        }
    }
}
