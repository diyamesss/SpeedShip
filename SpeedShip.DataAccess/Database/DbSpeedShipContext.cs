using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpeedShip.Model.Models;

namespace SpeedShip.DataAccess.Database;

public partial class DbSpeedShipContext : DbContext
{
    public DbSpeedShipContext()
    {
    }

    public DbSpeedShipContext(DbContextOptions<DbSpeedShipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>().HasData(
                        new Product()
                        {
                            ProductId = 1,
                            ProductName = "Godzilla x Kong: The New Empire",
                            Description = "Movie",
                            Price = 500,
                            CreatedBy = "Admin",
                            DateCreated = DateTime.UtcNow
                        },
                        new Product()
                        {
                            ProductId = 2,
                            ProductName = "Don't Move",
                            Description = "Movie",
                            Price = 500,
                            CreatedBy = "Admin",
                            DateCreated = DateTime.UtcNow
                        }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
