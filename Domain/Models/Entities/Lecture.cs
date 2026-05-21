using Domain.Models.Concrates;
using Domain.Models.Stables;

namespace Domain.Models.Entities
{
    /// <summary>
    /// A discrete teaching session within a Lesson.
    /// A Lesson (e.g., "Veb sistemləri") spans a semester;
    /// a Lecture is a single class meeting (e.g., "Week 3 — HTML əsasları").
    /// </summary>
    public class Lecture : AuditableEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int OrderIndex { get; set; }           // Sequence within the lesson
        public string Title { get; set; }              // Topic name
        public string? Description { get; set; }       // Extended description
        public DateTime LectureDate { get; set; }      // Scheduled date
        public int DurationMinutes { get; set; }       // Default 90
        public LectureType Type { get; set; }          // Mühazirə, Seminar, Laboratoriya

        // --- Navigation ---
        public ICollection<LectureMaterial> Materials { get; set; }
    }
}
