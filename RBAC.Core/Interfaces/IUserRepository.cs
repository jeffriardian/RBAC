using RBAC.Core.Entities;

namespace RBAC.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<UserEntity> GetByUsernameAsync(string username);
        Task<List<UserEntity>> GetAllAsync();
        Task AddAsync(UserEntity user);
        Task UpdateAsync(UserEntity user);
        Task DeleteAsync(Guid id);
    }
}
