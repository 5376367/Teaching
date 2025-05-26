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

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=localhost\\developments;Database=LearningDashboard;Integrated Security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC071D4CF89E");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC077B9978EB");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "Enrolment",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Enrolment__Cours__4316F928"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__Enrolment__Stude__4222D4EF"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId").HasName("PK__Enrolmen__5E57FC839A6639B1");
                        j.ToTable("Enrolments");
                        j.HasIndex(new[] { "CourseId" }, "IX_Enrolments_CourseId");
                        j.HasIndex(new[] { "StudentId" }, "IX_Enrolments_StudentId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
