using MediatR;
using RBAC.Application.Events;
using RBAC.Core.DTO.Role;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Commands.Role
{
    public record AddRoleCommand(CreateRoleDto role) : IRequest<ResponseViewModel<RoleDto>>;


    public class AddRoleCommandHandler(IRoleRepository roleRepository, IPublisher mediator)
        : IRequestHandler<AddRoleCommand, ResponseViewModel<RoleDto>>
    {
        public async Task<ResponseViewModel<RoleDto>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.AddAsync(request.role);
            await mediator.Publish(new RoleCreatedEvent(role.Data.Select(p=>p.Id).FirstOrDefault()));
            return role;
        }
    }
}
