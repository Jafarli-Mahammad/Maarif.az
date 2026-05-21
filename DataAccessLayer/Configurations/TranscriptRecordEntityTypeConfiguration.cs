using DataAccessLayer.Extensions;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class TranscriptRecordEntityTypeConfiguration : IEntityTypeConfiguration<TranscriptRecord>
    {
        public void Configure(EntityTypeBuilder<TranscriptRecord> builder)
        {
            builder.ToTable("TranscriptRecords");

            builder.HasKey(tr => tr.Id);

            builder.HasQueryFilter(tr => tr.DeletedAt == null);

            // --- Semester Info ---
            builder.Property(tr => tr.AcademicYear)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(tr => tr.Semester)
                   .IsRequired()
                   .HasConversion<int>();

            // --- Snapshotted Subject Data ---
            builder.Property(tr => tr.SubjectName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(tr => tr.SubjectCategory)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(tr => tr.Credits)
                   .IsRequired();

            // --- Scores ---
            builder.Property(tr => tr.PreExamTotal)
                   .IsRequired();

            builder.Property(tr => tr.ExamScore)
                   .IsRequired(false);

            builder.Property(tr => tr.TotalScore)
                   .IsRequired();

            builder.Property(tr => tr.LetterGrade)
                   .IsRequired()
                   .HasMaxLength(5);

            builder.Property(tr => tr.GradePoint)
                   .IsRequired()
                   .HasPrecision(3, 1);

            // --- Status ---
            builder.Property(tr => tr.IsRetake)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(tr => tr.IsPassed)
                   .IsRequired()
                   .HasDefaultValue(false);

            // --- Finalization ---
            builder.Property(tr => tr.FinalizedAt)
                   .HasColumnType("datetime");

            // --- Relationships ---
            builder.HasOne(tr => tr.Student)
                   .WithMany(s => s.TranscriptRecords)
                   .HasForeignKey(tr => tr.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tr => tr.Lesson)
                   .WithMany(l => l.TranscriptRecords)
                   .HasForeignKey(tr => tr.LessonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // A student can only have one transcript record per lesson (unless retake)
            builder.HasIndex(tr => new { tr.StudentId, tr.LessonId, tr.AcademicYear, tr.Semester })
                   .IsUniqueWhenNotDeleted();

            // For transcript queries: all records for one student, ordered by year/semester
            builder.HasIndex(tr => new { tr.StudentId, tr.AcademicYear, tr.Semester });

            builder.HasIndex(tr => tr.LessonId);

            // --- Check constraints ---
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_TranscriptRecord_Scores_NonNegative",
                    "[PreExamTotal] >= 0 AND [TotalScore] >= 0 AND [Credits] >= 0");

                tb.HasCheckConstraint("CK_TranscriptRecord_GradePoint_Range",
                    "[GradePoint] >= 0 AND [GradePoint] <= 4.0");
            });

            builder.ConfigureAuditable();
        }
    }
}
