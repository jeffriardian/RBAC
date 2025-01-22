using MediatR;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;

namespace RBAC.Application.Queries.Permission
{
    public record GetPermissionByIdQuery(Guid PermissionId) : IRequest<ResponseViewModel<PermissionEntity>>;

    public class GetPermissionByIdQueryHandler(IPermissionRepository permissionRepository)
        : IRequestHandler<GetPermissionByIdQuery, ResponseViewModel<PermissionEntity>>
    {
        public async Task<ResponseViewModel<PermissionEntity>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            return await permissionRepository.GetByIdAsync(request.PermissionId);
        }
    }
}
