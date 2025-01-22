using RBAC.Core.Entities;

namespace RBAC.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<UserEntity> GetByUsernameAsync(string username);
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity> AddAsync(UserEntity user);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task<bool> DeleteAsync(Guid id);
    }
}
