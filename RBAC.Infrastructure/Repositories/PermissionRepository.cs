using Microsoft.EntityFrameworkCore;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Infrastructure.Data;

namespace RBAC.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionEntity> GetByIdAsync(Guid id)
        {
            return await _context.Permissions
                .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PermissionEntity> GetByNameAsync(string permissionName)
        {
            return await _context.Permissions
                .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
                .FirstOrDefaultAsync(p => p.Name == permissionName);
        }

        public async Task<List<PermissionEntity>> GetAllAsync()
        {
            return await _context.Permissions
                .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
            .ToListAsync();
        }

        public async Task AddAsync(PermissionEntity permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PermissionEntity permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var permission = await GetByIdAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }
    }

}
