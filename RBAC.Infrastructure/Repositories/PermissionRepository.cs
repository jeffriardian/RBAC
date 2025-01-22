using Microsoft.EntityFrameworkCore;
using RBAC.Core.DTO.Permission;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;
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

        public async Task<ResponseViewModel<PermissionEntity>> GetByIdAsync(Guid id)
        {
            ResponseViewModel<PermissionEntity> result = new ResponseViewModel<PermissionEntity>();
            try
            {
                List<PermissionEntity> listData = new List<PermissionEntity>();
                var data = await _context.Permissions.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role).FirstOrDefaultAsync(p => p.Id == id);
                listData.Add(data);
                result.Data = listData;
                result.StatusCode = 200;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResponseViewModel<PermissionEntity>> GetByNameAsync(string permissionName)
        {
            ResponseViewModel<PermissionEntity> result = new ResponseViewModel<PermissionEntity>();
            try
            {
                List<PermissionEntity> listData = new List<PermissionEntity>();
                var data = await _context.Permissions.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role).FirstOrDefaultAsync(p => p.Name == permissionName);
                listData.Add(data);
                result.Data = listData;
                result.StatusCode = 200;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResponseViewModel<PermissionEntity>> GetAllAsync()
        {
            ResponseViewModel<PermissionEntity> result = new ResponseViewModel<PermissionEntity>();
            try
            {
                var data = await _context.Permissions.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role).ToListAsync();
                result.Data = data;
                result.StatusCode = 200;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResponseViewModel<PermissionDto>> AddAsync(CreatePermissionDto createPermissionDto)
        {
            ResponseViewModel<PermissionDto> result = new ResponseViewModel<PermissionDto>();
            try
            {
                var permission = new PermissionEntity
                {
                    Id = Guid.NewGuid(),
                    Name = createPermissionDto.Name,
                    Description = createPermissionDto.Description,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Permissions.AddAsync(permission);
                await _context.SaveChangesAsync();

                List<PermissionDto> listData = new List<PermissionDto>();

                var PermissionDto = new PermissionDto
                {
                    Id = permission.Id,
                    Name = permission.Name,
                    Description = permission.Description,
                    CreatedAt = permission.CreatedAt
                };

                listData.Add(PermissionDto);
                result.Data = listData;
                result.StatusCode = 200;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResponseViewModel<PermissionDto>> UpdateAsync(Guid PermissionId, UpdatePermissionDto permissionDto)
        {

            ResponseViewModel<PermissionDto> result = new ResponseViewModel<PermissionDto>();
            try
            {
                var permission = await _context.Permissions.FindAsync(PermissionId);
                if (permission == null)
                {
                    result.StatusCode = 500;
                    result.Message= "Data not found";
                    return result;
                }

                permission.Name = permissionDto.Name;
                permission.Description = permissionDto.Description;

                _context.Permissions.Update(permission);
                await _context.SaveChangesAsync();

                List<PermissionDto> listData = new List<PermissionDto>();

                var PermissionDto = new PermissionDto
                {
                    Id = permission.Id,
                    Name = permission.Name,
                    Description = permission.Description,
                    CreatedAt = permission.CreatedAt
                };

                listData.Add(PermissionDto);
                result.Data = listData;
                result.StatusCode = 200;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var permission = await _context.Permissions.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role).FirstOrDefaultAsync(p => p.Id == id); ;
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }

}
