using RBAC.Core.Entities;

namespace RBAC.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleEntity> GetByIdAsync(Guid id);
        Task<RoleEntity> GetByNameAsync(string name);
        Task<List<RoleEntity>> GetAllAsync();
        Task AddAsync(RoleEntity role);
        Task UpdateAsync(RoleEntity role);
        Task DeleteAsync(Guid id);
    }
}
