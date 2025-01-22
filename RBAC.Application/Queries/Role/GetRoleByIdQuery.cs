using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Queries.Role
{
    public record GetRoleByIdQuery(Guid RoleId) : IRequest<ResponseViewModel<RoleEntity>>;

    public class GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        : IRequestHandler<GetRoleByIdQuery, ResponseViewModel<RoleEntity>>
    {
        public async Task<ResponseViewModel<RoleEntity>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.GetByIdAsync(request.RoleId);
        }
    }
}
