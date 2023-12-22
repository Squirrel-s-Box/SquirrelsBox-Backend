using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Extensions;
using SquirrelsBox.Permissions.Domain.Models;
using System.Reflection.Emit;

namespace SquirrelsBox.Permissions.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<AssignedPermission> AssignedPermissions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Permission>(entity =>
            {
                entity.ToTable("permissions");
                entity.Property(e => e.Collection).HasMaxLength(60);
                entity.Property(e => e.Name).HasMaxLength(60);
                entity.Property(e => e.Description).HasMaxLength(60);
                entity.HasKey(e => e.Id);
            });

            builder.Entity<AssignedPermission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.ToTable("assigned_permissions");
                entity.Property(e => e.UserCode).HasMaxLength(40);
                entity.Property(e => e.ElementId);
                entity.Property(e => e.PermissionId);

                entity.HasOne(e => e.Permission)
                      .WithMany()
                      .HasForeignKey(e => e.PermissionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.UseSnakeCaseNamingConvention();
        }

    }
}
