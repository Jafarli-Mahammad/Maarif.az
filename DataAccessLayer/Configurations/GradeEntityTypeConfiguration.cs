using DataAccessLayer.Extensions;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class GradeEntityTypeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");

            builder.HasKey(g => g.Id);

            builder.HasQueryFilter(g => g.DeletedAt == null);

            // --- Scores ---
            builder.Property(g => g.ManualPracticalScore).IsRequired(false);
            builder.Property(g => g.FreelanceWork).IsRequired(false);
            builder.Property(g => g.MidtermScore).IsRequired(false);
            builder.Property(g => g.LaboratoryScore).IsRequired(false);
            builder.Property(g => g.ExamScore).IsRequired(false);
            builder.Property(g => g.AttendanceScore).IsRequired().HasDefaultValue(0);

            // --- Grading ---
            builder.Property(g => g.LetterGrade)
                   .HasMaxLength(5);

            builder.Property(g => g.GradePointValue)
                   .HasPrecision(3, 1);

            builder.Property(g => g.IsFinalized)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(g => g.FinalizedAt)
                   .HasColumnType("datetime");

            // --- Computed properties excluded from DB ---
            builder.Ignore(g => g.GrandTotal);

            // --- Relationships ---
            builder.HasOne(g => g.Student)
                   .WithMany(s => s.Grades)
                   .HasForeignKey(g => g.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Lesson)
                   .WithMany(l => l.Grades)
                   .HasForeignKey(g => g.LessonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // A student should only have one grade per lesson
            builder.HasIndex(g => new { g.StudentId, g.LessonId })
                   .IsUniqueWhenNotDeleted();

            builder.HasIndex(g => g.LessonId);

            builder.ConfigureAuditable();
        }
    }
}
