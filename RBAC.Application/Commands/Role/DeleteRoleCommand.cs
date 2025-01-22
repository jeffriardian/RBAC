using MediatR;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Commands.Role
{
    public record DeleteRoleCommand(Guid RoleId) : IRequest<bool>;

    internal class DeleteRoleCommandHandler(IRoleRepository roleRepository)
        : IRequestHandler<DeleteRoleCommand, bool>
    {
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await roleRepository.DeleteAsync(request.RoleId);
        }
    }
}
