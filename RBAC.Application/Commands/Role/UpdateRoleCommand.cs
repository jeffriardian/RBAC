using MediatR;
using RBAC.Core.DTO.Role;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.Role
{
    public record UpdateRoleCommand(Guid RoleId, UpdateRoleDto Role) : IRequest<ResponseViewModel<RoleDto>>;
    public class UpdateRoleCommandHandler(IRoleRepository roleRepository)
        : IRequestHandler<UpdateRoleCommand, ResponseViewModel<RoleDto>>
    {
        public async Task<ResponseViewModel<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await roleRepository.UpdateAsync(request.RoleId, request.Role);
        }
    }
}
