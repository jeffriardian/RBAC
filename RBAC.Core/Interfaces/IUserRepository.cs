using RBAC.Core.DTO.Role;
using RBAC.Core.DTO.User;
using RBAC.Core.Entities;
using RBAC.Core.ViewModel;

namespace RBAC.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseViewModel<UserEntity>> GetByIdAsync(Guid id);
        Task<ResponseViewModel<UserEntity>> GetAllAsync();
        Task<ResponseViewModel<UserDto>> AddAsync(CreateUserDto user);
        Task<ResponseViewModel<UserDto>> UpdateAsync(Guid UserId, UpdateUserDto user);
        Task<bool> DeleteAsync(Guid id);
    }
}
