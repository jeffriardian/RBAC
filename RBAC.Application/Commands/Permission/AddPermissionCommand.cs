using MediatR;
using RBAC.Application.Events;
using RBAC.Core.DTO.Permission;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.Permission
{
    public record AddPermissionCommand(CreatePermissionDto permission) : IRequest<ResponseViewModel<PermissionDto>>;


    public class AddPermissionCommandHandler(IPermissionRepository permissionRepository, IPublisher mediator)
        : IRequestHandler<AddPermissionCommand, ResponseViewModel<PermissionDto>>
    {
        public async Task<ResponseViewModel<PermissionDto>> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await permissionRepository.AddAsync(request.permission);
            await mediator.Publish(new PermissionCreatedEvent(permission.Data.Select(p=>p.Id).FirstOrDefault()));
            return permission;
        }
    }
}
