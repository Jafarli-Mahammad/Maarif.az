using Domain.Models.Concrates;
using Domain.Models.Stables;

namespace Domain.Models.Entities
{
    public class Lesson : AuditableEntity
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public bool HasLaboratory { get; set; }

        // --- Academic Term ---
        public string? AcademicYear { get; set; }      // e.g., "2025-2026"
        public SemesterType? Semester { get; set; }    // Payız / Yaz

        // --- Navigation ---
        public ICollection<LessonGroup> LessonGroups { get; set; }
        public ICollection<LessonSchedule> LessonSchedules { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<TranscriptRecord> TranscriptRecords { get; set; }
    }
}
