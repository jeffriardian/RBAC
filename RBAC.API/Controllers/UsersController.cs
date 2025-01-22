using MediatR;
using Microsoft.AspNetCore.Mvc;
using RBAC.Application.Commands.User;
using RBAC.Core.Entities;

namespace RBAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender sender;

        public UsersController(ISender sender)
        {
            sender = sender;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserEntity user)
        {
            var result = await sender.Send(new AddUserCommand(user));

            return Ok(result);
        }
    }
}
