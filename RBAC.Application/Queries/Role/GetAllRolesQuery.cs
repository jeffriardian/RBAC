using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Queries.Role
{
    public record GetAllRolesQuery() : IRequest<ResponseViewModel<RoleEntity>>;
    public class GetAllRolesQueryHandler(IRoleRepository roleRepository)
        : IRequestHandler<GetAllRolesQuery, ResponseViewModel<RoleEntity>>
    {
        public async Task<ResponseViewModel<RoleEntity>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.GetAllAsync();
        }
    }
}
