using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Models;

public partial class FunewsManagementContext : DbContext
{
    public FunewsManagementContext()
    {
    }

    public FunewsManagementContext(DbContextOptions<FunewsManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<NewsArticle> NewsArticles { get; set; }

    public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-M592O8F\\SQLEXPRESS;Database=FUNewsManagement;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B72BCDE1B");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__Category__Parent__3B75D760");
        });

        modelBuilder.Entity<NewsArticle>(entity =>
        {
            entity.HasKey(e => e.NewsArticleId).HasName("PK__NewsArti__4CD0926CFD6F8E2E");

            entity.ToTable("NewsArticle");

            entity.Property(e => e.NewsArticleId).HasColumnName("NewsArticleID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Headline).HasMaxLength(1000);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NewsSource).HasMaxLength(255);
            entity.Property(e => e.NewsStatus).HasDefaultValue(true);
            entity.Property(e => e.NewsTitle).HasMaxLength(500);
            entity.Property(e => e.UpdatedById).HasColumnName("UpdatedByID");

            entity.HasOne(d => d.Category).WithMany(p => p.NewsArticles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsArtic__Categ__403A8C7D");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.NewsArticleCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsArtic__Creat__412EB0B6");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.NewsArticleUpdatedBies)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK__NewsArtic__Updat__4222D4EF");

            entity.HasMany(d => d.Tags).WithMany(p => p.NewsArticles)
                .UsingEntity<Dictionary<string, object>>(
                    "NewsTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK__NewsTag__TagID__47DBAE45"),
                    l => l.HasOne<NewsArticle>().WithMany()
                        .HasForeignKey("NewsArticleId")
                        .HasConstraintName("FK__NewsTag__NewsArt__46E78A0C"),
                    j =>
                    {
                        j.HasKey("NewsArticleId", "TagId").HasName("PK__NewsTag__9A875DC8051A1B6D");
                        j.ToTable("NewsTag");
                        j.IndexerProperty<int>("NewsArticleId").HasColumnName("NewsArticleID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__SystemAc__349DA5869A1DA1B6");

            entity.ToTable("SystemAccount");

            entity.HasIndex(e => e.AccountEmail, "UQ__SystemAc__FC770D33FB22956D").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountEmail).HasMaxLength(255);
            entity.Property(e => e.AccountName).HasMaxLength(255);
            entity.Property(e => e.AccountPassword).HasMaxLength(255);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__657CFA4C251A6B11");

            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
