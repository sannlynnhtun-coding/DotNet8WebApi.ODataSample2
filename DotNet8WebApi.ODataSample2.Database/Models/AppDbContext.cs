using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.ODataSample2.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ODataSample2;User ID=sa;Password=sasa@123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC27D75E35CE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC279DBC5378");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Company).WithMany(p => p.Products)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Products__Compan__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
