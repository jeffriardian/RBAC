﻿using System.Data;
using Microsoft.EntityFrameworkCore;
using RBAC.Core.DTO.Permission;
using RBAC.Core.DTO.Role;
using RBAC.Core.DTO.User;
using RBAC.Core.Entities;
using RBAC.Core.Interfaces;
using RBAC.Core.ViewModel;
using RBAC.Infrastructure.Data;

namespace RBAC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher; // Interface for password hashing

        public UserRepository(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseViewModel<UserEntity>> GetByIdAsync(Guid id)
        { 
            ResponseViewModel<UserEntity> result = new ResponseViewModel<UserEntity>();
            try
            {
                List<UserEntity> listData = new List<UserEntity>();
                var data = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Id == id);
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

        public async Task<ResponseViewModel<UserEntity>> GetAllAsync()
        {
            ResponseViewModel<UserEntity> result = new ResponseViewModel<UserEntity>();
            try
            {
                var data = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
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

        public async Task<ResponseViewModel<UserDto>> AddAsync(CreateUserDto createUserDto)
        {
            ResponseViewModel<UserDto> result = new ResponseViewModel<UserDto>();
            try
            {
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = createUserDto.Username,
                    Email = createUserDto.Email,
                    PasswordHash = _passwordHasher.HashPassword(createUserDto.Username, createUserDto.PasswordHash), // Hash password before saving
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();


                List<UserDto> listData = new List<UserDto>();
                List<RoleDto> listDataRole = new List<RoleDto>();

                var userRole = new UserRoleEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RoleId = createUserDto.RoleId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();

                var role = await _context.Roles.FindAsync(createUserDto.RoleId);

                var roleDto = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    CreatedAt = role.CreatedAt,
                };

                listDataRole.Add(roleDto);

                var UserDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = role.CreatedAt,
                    UserRoles = listDataRole,
                };

                listData.Add(UserDto);
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

        public async Task<ResponseViewModel<UserDto>> UpdateAsync(Guid UserId, UpdateUserDto userDto)
        {

            ResponseViewModel<UserDto> result = new ResponseViewModel<UserDto>();
            try
            {
                var users = await _context.Users.FindAsync(UserId);
                if (users == null)
                {
                    result.StatusCode = 500;
                    result.Message = "Data not found";
                    return result;
                }

                users.Username = userDto.Username;
                users.Email = userDto.Email;
                users.PasswordHash = _passwordHasher.HashPassword(userDto.Username, userDto.PasswordHash);

                _context.Users.Update(users);
                await _context.SaveChangesAsync();

                List<UserDto> listData = new List<UserDto>();
                List<RoleDto> listDataUserRole = new List<RoleDto>();

                var userUpdate = await _context.Users.FindAsync(UserId);

                //Remove Existing User Role
                var existingUserRoles = _context.UserRoles.Where(rp => rp.UserId == users.Id);
                _context.UserRoles.RemoveRange(existingUserRoles);

                // Add new User Role
                var userRole = new UserRoleEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = users.Id,
                    RoleId = userDto.RoleId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();

                var role = await _context.Roles.FindAsync(userDto.RoleId);

                var roleDto = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    CreatedAt = role.CreatedAt,
                };

                listDataUserRole.Add(roleDto);

                var UserDto = new UserDto
                {
                    Id = userUpdate.Id,
                    Username = userUpdate.Username,
                    Email = userUpdate.Email,
                    UserRoles = listDataUserRole,
                };

                listData.Add(UserDto);
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
            var user = await _context.Users.Include(p => p.UserRoles).ThenInclude(rp => rp.Role).FirstOrDefaultAsync(p => p.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
