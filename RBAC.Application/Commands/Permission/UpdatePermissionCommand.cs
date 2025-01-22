using MediatR;
using RBAC.Core.DTO;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.Permission
{
    public record UpdatePermissionCommand(Guid PermissionId, UpdatePermissionDto Permission) : IRequest<ResponseViewModel<PermissionDto>>;
    public class UpdatePermissionCommandHandler(IPermissionRepository permissionRepository)
        : IRequestHandler<UpdatePermissionCommand, ResponseViewModel<PermissionDto>>
    {
        public async Task<ResponseViewModel<PermissionDto>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            return await permissionRepository.UpdateAsync(request.PermissionId, request.Permission);
        }
    }
}
