using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class LectureMaterialEntityTypeConfiguration : IEntityTypeConfiguration<LectureMaterial>
    {
        public void Configure(EntityTypeBuilder<LectureMaterial> builder)
        {
            builder.ToTable("LectureMaterials");

            builder.HasKey(m => m.Id);

            builder.HasQueryFilter(m => m.DeletedAt == null);

            builder.Property(m => m.Title)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(m => m.FileName)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(m => m.FilePath)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(m => m.ContentType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(m => m.FileSize)
                   .IsRequired();

            builder.Property(m => m.Type)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(m => m.DownloadCount)
                   .IsRequired()
                   .HasDefaultValue(0);

            // Relationships
            builder.HasOne(m => m.Lecture)
                   .WithMany(l => l.Materials)
                   .HasForeignKey(m => m.LectureId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.LectureId);

            builder.ConfigureAuditable();
        }
    }
}
