using Domain.Models.Concrates;
using Domain.Models.Stables;

namespace Domain.Models.Entities
{
    /// <summary>
    /// A downloadable file attached to a specific Lecture.
    /// Covers PDFs, videos, presentations, lab instructions, etc.
    /// </summary>
    public class LectureMaterial : AuditableEntity
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public string Title { get; set; }              // Display name
        public string FileName { get; set; }           // Original uploaded file name
        public string FilePath { get; set; }           // Storage path or URL
        public string ContentType { get; set; }        // MIME type (e.g., "application/pdf")
        public long FileSize { get; set; }             // Size in bytes
        public MaterialType Type { get; set; }         // PDF, Video, Document, etc.
        public int DownloadCount { get; set; }         // Track popularity
    }
}
