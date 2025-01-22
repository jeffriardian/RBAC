using MediatR;
using Microsoft.AspNetCore.Mvc;
using RBAC.Application.Commands.User;
using RBAC.Application.Queries.User;
using RBAC.Core.DTO.User;

namespace RBAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        [HttpPost("users")]
        public async Task<IActionResult> AddUserAsync([FromBody] CreateUserDto user)
        {
            var result = await sender.Send(new AddUserCommand(user));
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("users/{userId:guid}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            var result = await sender.Send(new GetUserByIdQuery(userId));

            if (result == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            return Ok(result);
        }

        [HttpPut("users/{userId:guid}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserDto user)
        {
            var result = await sender.Send(new UpdateUserCommand(userId, user));
            return Ok(result);
        }

        [HttpDelete("users/{userId:guid}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            var result = await sender.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }
}
