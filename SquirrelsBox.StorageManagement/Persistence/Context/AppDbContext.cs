﻿using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Extensions;
using SquirrelsBox.StorageManagement.Domain.Models;
using System.Reflection.Emit;

namespace SquirrelsBox.StorageManagement.Persistence.Context
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<SharedBox> SharedBox { get; set; }

        public DbSet<Box> Box { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Spec> Spec { get; set; }

        public DbSet<BoxSectionRelationship> BoxSectionRelationship { get; set; }
        public DbSet<SectionItemRelationship> SectionItemRelationship { get; set; }
        public DbSet<ItemSpecRelationship> ItemSpecRelationship { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Box>(entity =>
            {
                entity.ToTable("boxes");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Name).HasMaxLength(60);
                entity.Property(p => p.UserCodeOwner).IsRequired().HasMaxLength(40);
                entity.Property(p => p.Favourite).HasDefaultValue(false);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.LastUpdateDate);
                entity.Property(p => p.State).HasDefaultValue(true);

                entity.HasMany(b => b.BoxSectionsList)
                    .WithOne(bsl => bsl.Box)
                    .HasForeignKey(bsl => bsl.BoxId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(b => b.SharedBoxes)
                    .WithOne(sb => sb.Box)
                    .HasForeignKey(sb => sb.BoxId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Section>(entity =>
            {
                entity.ToTable("sections");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Name).HasMaxLength(60);
                entity.Property(p => p.Color).HasMaxLength(60);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.LastUpdateDate);
                entity.Property(p => p.State).HasDefaultValue(true);

                entity.HasMany(s => s.BoxSectionsList)
                    .WithOne(bsl => bsl.Section)
                    .HasForeignKey(bsl => bsl.SectionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.SectionItemsList)
                    .WithOne(sil => sil.Section)
                    .HasForeignKey(sil => sil.SectionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Item>(entity =>
            {
                entity.ToTable("items");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Name).HasMaxLength(60);
                entity.Property(p => p.Description).HasMaxLength(60);
                entity.Property(p => p.Amount).HasMaxLength(60);
                entity.Property(p => p.ItemPhoto).HasMaxLength(60);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.LastUpdateDate);
                entity.Property(p => p.State).HasDefaultValue(true);

                entity.HasMany(i => i.SectionItemsList)
                    .WithOne(sil => sil.Item)
                    .HasForeignKey(sil => sil.ItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Spec>(entity =>
            {
                entity.ToTable("specs");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.HeaderName).HasMaxLength(60);
                entity.Property(p => p.Value).HasMaxLength(60);
                entity.Property(p => p.ValueType).HasMaxLength(60);
                entity.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.LastUpdateDate);
                entity.Property(p => p.State).HasDefaultValue(true);

                entity.HasMany(ps => ps.ItemSpecsList)
                    .WithOne(psil => psil.Spec)
                    .HasForeignKey(psil => psil.SpecId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BoxSectionRelationship>()
                .HasKey(bsl => new { bsl.BoxId, bsl.SectionId });

            builder.Entity<BoxSectionRelationship>()
                .HasOne(bsl => bsl.Box)
                .WithMany(b => b.BoxSectionsList)
                .HasForeignKey(bsl => bsl.BoxId);

            builder.Entity<BoxSectionRelationship>()
                .HasOne(bsl => bsl.Section)
                .WithMany(s => s.BoxSectionsList)
                .HasForeignKey(bsl => bsl.SectionId);

            builder.Entity<SectionItemRelationship>()
                .HasKey(sil => new { sil.SectionId, sil.ItemId });

            builder.Entity<SectionItemRelationship>()
                .HasOne(sil => sil.Section)
                .WithMany(s => s.SectionItemsList)
                .HasForeignKey(sil => sil.SectionId);

            builder.Entity<SectionItemRelationship>()
                .HasOne(sil => sil.Item)
                .WithMany(i => i.SectionItemsList)
                .HasForeignKey(sil => sil.ItemId);

            builder.Entity<ItemSpecRelationship>()
                .HasKey(psil => new { psil.ItemId, psil.SpecId });

            builder.Entity<ItemSpecRelationship>()
                .HasOne(psil => psil.Item)
                .WithMany(i => i.ItemSpecsList)
                .HasForeignKey(psil => psil.ItemId);

            builder.Entity<ItemSpecRelationship>()
                .HasOne(psil => psil.Spec)
                .WithMany(s => s.ItemSpecsList) 
                .HasForeignKey(psil => psil.SpecId);


            builder.UseSnakeCaseNamingConvention();
        }

        public async Task UpdateBoxSectionRelationship(int boxId, int sectionId, int newBoxId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC UpdateBoxSectionRelationship @p0, @p1, @p2",
                parameters: new object[] { boxId, sectionId, newBoxId }
            );
        }

        public async Task UpdateSectionItemRelationship(int sectionId, int itemId, int newSectionId)
        {
            await Database.ExecuteSqlRawAsync(  
                "EXEC UpdateSectionItemRelationship @p0, @p1, @p2",
                parameters: new object[] { sectionId, itemId, newSectionId }
            );
        }

        public async Task UpdateItemSpecRelationship(int itemId, int specId, int newItemId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC UpdateItemSpecRelationship @p0, @p1, @p2",
                parameters: new object[] { itemId, specId, newItemId }
            );
        }
    }
}
