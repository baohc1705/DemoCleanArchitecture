using System;
using System.Collections.Generic;
using DemoCleanArchitecture.Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;

namespace DemoCleanArchitecture.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<News> News { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasIndex(e => e.DeletedAt, "IX_Menus_DeletedAt");

            entity.HasIndex(e => e.DisplayOrder, "IX_Menus_DisplayOrder");

            entity.HasIndex(e => e.IsActive, "IX_Menus_IsActive");

            entity.HasIndex(e => e.ParentId, "IX_Menus_ParentId");

            entity.HasIndex(e => e.Slug, "UQ_Menus_Slug").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Menus_Parent");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasIndex(e => e.DeletedAt, "IX_News_DeletedAt");

            entity.HasIndex(e => new { e.MenuId, e.Status, e.PublishedAt }, "IX_News_MenuId_Status_PublishedAt").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.MenuId, e.Slug }, "UQ_News_MenuId_Slug").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Slug).HasMaxLength(350);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Menu).WithMany(p => p.News)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_News_Menus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
