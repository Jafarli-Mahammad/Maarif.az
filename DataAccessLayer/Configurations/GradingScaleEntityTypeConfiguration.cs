using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class GradingScaleEntityTypeConfiguration : IEntityTypeConfiguration<GradingScale>
    {
        public void Configure(EntityTypeBuilder<GradingScale> builder)
        {
            builder.ToTable("GradingScales");

            builder.HasKey(gs => gs.Id);

            builder.HasQueryFilter(gs => gs.DeletedAt == null);

            builder.Property(gs => gs.LetterGrade)
                   .IsRequired()
                   .HasMaxLength(5);

            builder.Property(gs => gs.MinScore)
                   .IsRequired()
                   .HasPrecision(5, 2);

            builder.Property(gs => gs.MaxScore)
                   .IsRequired()
                   .HasPrecision(5, 2);

            builder.Property(gs => gs.GradePoint)
                   .IsRequired()
                   .HasPrecision(3, 1);

            builder.Property(gs => gs.Description)
                   .IsRequired()
                   .HasMaxLength(100);

            // Each letter grade should be unique
            builder.HasIndex(gs => gs.LetterGrade)
                   .IsUnique();

            // --- Seed the standard Azerbaijani grading scale ---
            builder.HasData(
                new GradingScale { Id = 1, LetterGrade = "A", MinScore = 91, MaxScore = 100, GradePoint = 4.0m, Description = "Əla", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = 1 },
                new GradingScale { Id = 2, LetterGrade = "B", MinScore = 71, MaxScore = 90, GradePoint = 3.0m, Description = "Yaxşı", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = 1 },
                new GradingScale { Id = 3, LetterGrade = "C", MinScore = 51, MaxScore = 70, GradePoint = 2.0m, Description = "Kafi", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = 1 },
                new GradingScale { Id = 4, LetterGrade = "D", MinScore = 31, MaxScore = 50, GradePoint = 1.0m, Description = "Qeyri-kafi", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = 1 },
                new GradingScale { Id = 5, LetterGrade = "F", MinScore = 0, MaxScore = 30, GradePoint = 0.0m, Description = "Zəif", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = 1 }
            );

            builder.ConfigureAuditable();
        }
    }
}
