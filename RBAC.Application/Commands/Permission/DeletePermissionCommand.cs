using MediatR;
using RBAC.Core.Interfaces;

namespace RBAC.Application.Commands.Permission
{
    public record DeletePermissionCommand(Guid PermissionId) : IRequest<bool>;

    internal class DeletePermissionCommandHandler(IPermissionRepository permissionRepository)
        : IRequestHandler<DeletePermissionCommand, bool>
    {
        public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            return await permissionRepository.DeleteAsync(request.PermissionId);
        }
    }
}
