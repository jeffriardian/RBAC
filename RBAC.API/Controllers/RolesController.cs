using MediatR;
using Microsoft.AspNetCore.Mvc;
using RBAC.Application.Commands.Role;
using RBAC.Application.Queries.Role;
using RBAC.Core.DTO.Role;

namespace RBAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(ISender sender) : ControllerBase
    {
        [HttpPost("roles")]
        public async Task<IActionResult> AddRoleAsync([FromBody] CreateRoleDto role)
        {
            var result = await sender.Send(new AddRoleCommand(role));
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await sender.Send(new GetAllRolesQuery());
            return Ok(result);
        }

        [HttpGet("roles/{roleId:guid}")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] Guid roleId)
        {
            var result = await sender.Send(new GetRoleByIdQuery(roleId));

            if (result == null)
            {
                return NotFound(new { Message = "Role not found." });
            }

            return Ok(result);
        }

        [HttpPut("roles/{roleId:guid}")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute] Guid roleId, [FromBody] UpdateRoleDto role)
        {
            var result = await sender.Send(new UpdateRoleCommand(roleId, role));
            return Ok(result);
        }

        [HttpDelete("roles/{roleId:guid}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] Guid roleId)
        {
            var result = await sender.Send(new DeleteRoleCommand(roleId));
            return Ok(result);
        }
    }
}
