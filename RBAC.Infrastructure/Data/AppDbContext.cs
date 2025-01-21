using Microsoft.EntityFrameworkCore;
using RBAC.Core.Entities;

namespace RBAC.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<RolePermissionEntity> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserRole configuration
            modelBuilder.Entity<UserRoleEntity>()
                .HasKey(ur => ur.Id);

            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRoleEntity>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            // RolePermission configuration
            modelBuilder.Entity<RolePermissionEntity>()
                .HasKey(rp => rp.Id);

            modelBuilder.Entity<RolePermissionEntity>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermissionEntity>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<RolePermissionEntity>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionId })
                .IsUnique();
        }
}
