using RBAC.Core.DTO.Role;
using RBAC.Core.Entities;
using RBAC.Core.ViewModel;

namespace RBAC.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<ResponseViewModel<RoleEntity>> GetByIdAsync(Guid id);
        Task<ResponseViewModel<RoleEntity>> GetAllAsync();
        Task<ResponseViewModel<RoleDto>> AddAsync(CreateRoleDto role);
        Task<ResponseViewModel<RoleDto>> UpdateAsync(Guid RoleId, UpdateRoleDto role);
        Task<bool> DeleteAsync(Guid id);
    }
}
