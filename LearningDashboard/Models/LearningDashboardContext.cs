using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LearningDashboard.Models;

public partial class LearningDashboardContext : DbContext
{
    public LearningDashboardContext()
    {
    }

    public LearningDashboardContext(DbContextOptions<LearningDashboardContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrolment> Enrolments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\developments;Database=LearningDashboard;Integrated Security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC071D4CF89E");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Enrolment>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK__Enrolmen__5E57FC839A6639B1");

            entity.HasIndex(e => e.CourseId, "IX_Enrolments_CourseId");

            entity.HasIndex(e => e.StudentId, "IX_Enrolments_StudentId");

            entity.Property(e => e.EnrolmentDate)
                .HasDefaultValue(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrolments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Enrolment__Cours__4316F928");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrolments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Enrolment__Stude__4222D4EF");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC077B9978EB");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
