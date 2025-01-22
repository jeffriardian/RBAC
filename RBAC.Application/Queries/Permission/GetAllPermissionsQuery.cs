using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Queries.Permission
{

    public record GetAllPermissionsQuery() : IRequest<ResponseViewModel<PermissionEntity>>;
    public class GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository)
        : IRequestHandler<GetAllPermissionsQuery, ResponseViewModel<PermissionEntity>>
    {
        public async Task<ResponseViewModel<PermissionEntity>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await permissionRepository.GetAllAsync();
        }
    }
}
