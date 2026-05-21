using DataAccessLayer.Extensions;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class LectureEntityTypeConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.ToTable("Lectures");

            builder.HasKey(l => l.Id);

            builder.HasQueryFilter(l => l.DeletedAt == null);

            builder.Property(l => l.Title)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(l => l.Description)
                   .HasMaxLength(4000);

            builder.Property(l => l.OrderIndex)
                   .IsRequired();

            builder.Property(l => l.LectureDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(l => l.DurationMinutes)
                   .IsRequired()
                   .HasDefaultValue(90);

            builder.Property(l => l.Type)
                   .IsRequired()
                   .HasConversion<int>();

            // Relationships
            builder.HasOne(l => l.Lesson)
                   .WithMany(les => les.Lectures)
                   .HasForeignKey(l => l.LessonId)
                   .OnDelete(DeleteBehavior.Cascade);

            // A lecture order index should be unique within its lesson
            builder.HasIndex(l => new { l.LessonId, l.OrderIndex })
                   .IsUniqueWhenNotDeleted();

            builder.HasIndex(l => l.LessonId);

            builder.ConfigureAuditable();
        }
    }
}
