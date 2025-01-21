using RBAC.Core.Entities;

namespace RBAC.Core.Interfaces
{
    public interface IPermissionRepository
    {
        Task<PermissionEntity> GetByIdAsync(Guid id);
        Task<PermissionEntity> GetByNameAsync(string name);
        Task<List<PermissionEntity>> GetAllAsync();
        Task AddAsync(PermissionEntity permission);
        Task UpdateAsync(PermissionEntity permission);
        Task DeleteAsync(Guid id);
    }
}
