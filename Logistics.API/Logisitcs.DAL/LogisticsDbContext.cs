using Logisitcs.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Logisitcs.DAL;

public partial class LogisticsDbContext : DbContext
{
    public LogisticsDbContext()
    {
    }

    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleBoxAssignment> ArticleBoxAssignments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Transportbox> Transportboxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={Environment.CurrentDirectory}\\Fileserver\\logisticsDB.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleGuid);

            entity.ToTable("Article");

            entity.Property(e => e.ArticleGuid).HasColumnName("ArticleGUID");
            entity.Property(e => e.Gtin).HasColumnName("GTIN");
        });

        modelBuilder.Entity<ArticleBoxAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentGuid);

            entity.ToTable("ArticleBoxAssignment");

            entity.Property(e => e.AssignmentGuid).HasColumnName("AssignmentGUID");
            entity.Property(e => e.ArticleGuid).HasColumnName("ArticleGUID");
            entity.Property(e => e.BoxGuid).HasColumnName("BoxGUID");
            entity.Property(e => e.ExpiryDate).HasColumnType("DATE");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleBoxAssignments).HasForeignKey(d => d.ArticleGuid);

            entity.HasOne(d => d.Box).WithMany(p => p.ArticleBoxAssignments).HasForeignKey(d => d.BoxGuid);

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.ArticleBoxAssignments).HasForeignKey(d => d.Status);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectGuid);

            entity.ToTable("Project");

            entity.Property(e => e.ProjectGuid).HasColumnName("ProjectGUID");
            entity.Property(e => e.CreationDate).HasColumnType("DATE");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Transportbox>(entity =>
        {
            entity.HasKey(e => e.BoxGuid);

            entity.ToTable("Transportbox");

            entity.Property(e => e.BoxGuid).HasColumnName("BoxGUID");
            entity.Property(e => e.ProjectGuid).HasColumnName("ProjectGUID");
            entity.Property(e => e.BoxCategory).HasColumnName("BoxCategory");

            entity.HasOne(d => d.Project).WithMany(p => p.Transportboxes).HasForeignKey(d => d.ProjectGuid);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.UserRoleId).HasColumnName("UserRole_Id");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users).HasForeignKey(d => d.UserRoleId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("Role_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}