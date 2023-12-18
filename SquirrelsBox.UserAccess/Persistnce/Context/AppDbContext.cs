using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Extensions;
using SquirrelsBox.Session.Domain.Models;

namespace SquirrelsBox.Session.Persistnce.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<DeviceSession> DeviceToken { get; set; }
        public DbSet<UserSession> SessionToken { get; set; }
        public DbSet<AccessSession> AccessSession { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccessSession>(entity =>
            {
                entity.ToTable("sessions");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Code).IsRequired().HasMaxLength(40);
                entity.Property(p => p.Attempt).HasDefaultValue(0);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.LastUpdateDate);

                entity.HasMany(u => u.SessionsTokens) // Correct the property name here
                   .WithOne(st => st.User)
                   .HasForeignKey(st => st.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Adjust the deletion behavior as per your needs

                entity.HasMany(u => u.DevicesTokens) // Correct the property name here
                    .WithOne(dt => dt.User)
                    .HasForeignKey(dt => dt.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<UserSession>(entity =>
            {
                entity.ToTable("users_sessions");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Token).HasMaxLength(255);
                entity.Property(p => p.OldToken).HasMaxLength(255);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()").IsRequired();
                entity.Property(p => p.LastUpdateDate);
                entity.Property(p => p.UserId).IsRequired();
            });

            builder.Entity<DeviceSession>(entity =>
            {
                entity.ToTable("devices_sessions");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Token).HasMaxLength(255);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()").IsRequired();
                entity.Property(p => p.LastUpdateDate);
                entity.Property(p => p.UserId).IsRequired();
            });

            builder.UseSnakeCaseNamingConvention();
        }


    }
}
