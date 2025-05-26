using System.Collections.Generic;
using System.Reflection.Emit;
using LearningDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningDashboard.Services
{
    public class LearningDashboardContext : DbContext
    {
        public LearningDashboardContext(DbContextOptions<LearningDashboardContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            // Configure Enrolment entity
            modelBuilder.Entity<Enrolment>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });

                entity.Property(e => e.StudentId)
                    .IsRequired();

                entity.Property(e => e.CourseId)
                    .IsRequired();

                // Configure foreign key relationship
                entity.HasOne<Course>()
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}