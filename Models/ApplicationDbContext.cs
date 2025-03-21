using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SubjectApp.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SubTopic> SubTopics { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Srinitha;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubTopic>(entity =>
        {
            entity.HasKey(e => e.SubTopicId).HasName("PK__SubTopic__3EFE32F02059155E");

            entity.Property(e => e.SubTopicId).HasColumnName("SubTopicID");
            entity.Property(e => e.SubTopicName).HasMaxLength(255);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Subject).WithMany(p => p.SubTopics)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SubTopics__Subje__7F2BE32F");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA388115FC1C8");

            entity.HasIndex(e => e.SubjectName, "UQ__Subjects__4C5A7D55ADE81CE5").IsUnique();

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
