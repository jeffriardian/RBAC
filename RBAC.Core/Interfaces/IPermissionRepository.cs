using RBAC.Core.DTO;
using RBAC.Core.Entities;
using RBAC.Core.ViewModel;

namespace RBAC.Core.Interfaces
{
    public interface IPermissionRepository
    {
        Task<ResponseViewModel<PermissionEntity>> GetByIdAsync(Guid id);
        Task<ResponseViewModel<PermissionEntity>> GetByNameAsync(string name);
        Task<ResponseViewModel<PermissionEntity>> GetAllAsync();
        Task<ResponseViewModel<PermissionDto>> AddAsync(CreatePermissionDto permission);
        Task<ResponseViewModel<PermissionDto>> UpdateAsync(Guid PermissionId, UpdatePermissionDto permission);
        Task<bool> DeleteAsync(Guid id);
    }
}
