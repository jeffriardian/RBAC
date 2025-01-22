using Microsoft.EntityFrameworkCore;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Infrastructure.Data;

namespace RBAC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .ToListAsync();
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
