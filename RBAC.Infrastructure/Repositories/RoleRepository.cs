using System.Data;
using Microsoft.EntityFrameworkCore;
using RBAC.Core.DTO.Permission;
using RBAC.Core.DTO.Role;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;
using RBAC.Infrastructure.Data;

namespace RBAC.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseViewModel<RoleEntity>> GetByIdAsync(Guid id)
        {
            ResponseViewModel<RoleEntity> result = new ResponseViewModel<RoleEntity>();
            try
            {
                List<RoleEntity> listData = new List<RoleEntity>();
                var data = await _context.Roles.Include(r => r.UserRoles).ThenInclude(ur => ur.User).FirstOrDefaultAsync(r => r.Id == id);
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

        public async Task<ResponseViewModel<RoleEntity>> GetAllAsync()
        {
            ResponseViewModel<RoleEntity> result = new ResponseViewModel<RoleEntity>();
            try
            {
                var data = await _context.Roles.Include(r => r.UserRoles).ThenInclude(ur => ur.User).ToListAsync();
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

        public async Task<ResponseViewModel<RoleDto>> AddAsync(CreateRoleDto createRoleDto)
        {
            ResponseViewModel<RoleDto> result = new ResponseViewModel<RoleDto>();
            try
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = createRoleDto.Name,
                    Description = createRoleDto.Description,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();


                List<RoleDto> listData = new List<RoleDto>();
                List<PermissionDto> listDataPermission = new List<PermissionDto>();

                if (createRoleDto.PermissionDtos.Count > 0)
                {
                    foreach (var item in createRoleDto.PermissionDtos)
                    {
                        var rolePermission = new RolePermissionEntity
                        {
                            Id = Guid.NewGuid(),
                            RoleId = role.Id,
                            PermissionId = item.Id,
                            CreatedAt = DateTime.UtcNow,
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                        await _context.SaveChangesAsync();

                        var permission = await _context.Permissions.FindAsync(item.Id);

                        var permissionDto = new PermissionDto
                        {
                            Id = item.Id,
                            Name = permission.Name,
                            Description = permission.Description,
                            CreatedAt = permission.CreatedAt,
                        };

                        listDataPermission.Add(permissionDto);
                    }
                }

                var RoleDto = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    CreatedAt = role.CreatedAt,
                    PermissionDtos = listDataPermission,
                };

                listData.Add(RoleDto);
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

        public async Task<ResponseViewModel<RoleDto>> UpdateAsync(Guid RoleId, UpdateRoleDto roleDto)
        {

            ResponseViewModel<RoleDto> result = new ResponseViewModel<RoleDto>();
            try
            {
                var roles = await _context.Roles.FindAsync(RoleId);
                if (roles == null)
                {
                    result.StatusCode = 500;
                    result.Message = "Data not found";
                    return result;
                }

                roles.Name = roleDto.Name;
                roles.Description = roleDto.Description;

                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                List<RoleDto> listData = new List<RoleDto>();
                List<PermissionDto> listDataPermission = new List<PermissionDto>();

                var roleUpdate = await _context.Roles.FindAsync(RoleId);

                if (roleDto.PermissionDtos.Count > 0)
                {
                    // Remove existing RolePermissions for the role
                    var existingRolePermissions = _context.RolePermissions.Where(rp => rp.RoleId == RoleId);
                    _context.RolePermissions.RemoveRange(existingRolePermissions);

                    foreach (var item in roleDto.PermissionDtos)
                    {
                        // Add new RolePermissions
                        var rolePermission = new RolePermissionEntity
                        {
                            Id = Guid.NewGuid(),
                            RoleId = RoleId,
                            PermissionId = item.Id,
                            CreatedAt = DateTime.UtcNow,
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                        await _context.SaveChangesAsync();

                        var permission = await _context.Permissions.FindAsync(item.Id);

                        var permissionDto = new PermissionDto
                        {
                            Id = item.Id,
                            Name = permission.Name,
                            Description = permission.Description,
                            CreatedAt = permission.CreatedAt,
                        };

                        listDataPermission.Add(permissionDto);
                    }
                }

                var RoleDto = new RoleDto
                {
                    Id = roleUpdate.Id,
                    Name = roleUpdate.Name,
                    Description = roleUpdate.Description,
                    CreatedAt = roleUpdate.CreatedAt,
                    PermissionDtos = listDataPermission,
                };

                listData.Add(RoleDto);
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

        public async Task UpdateAsync(RoleEntity role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var role = await _context.Roles.Include(p => p.RolePermissions).ThenInclude(rp => rp.Permission).FirstOrDefaultAsync(p => p.Id == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
