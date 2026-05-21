using Domain.Models.Concrates;
using Domain.Models.Stables;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    /// <summary>
    /// A finalized, immutable academic record for one student in one lesson.
    /// Used for official transcript generation.
    /// Subject data is snapshotted to preserve historical accuracy
    /// even if the subject is later renamed or restructured.
    /// </summary>
    public class TranscriptRecord : AuditableEntity
    {
        public int Id { get; set; }

        // --- Student & Lesson References ---
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        // --- Semester Info ---
        public string AcademicYear { get; set; }       // e.g., "2025-2026"
        public SemesterType Semester { get; set; }     // Payız / Yaz

        // --- Snapshotted Subject Data ---
        public string SubjectName { get; set; }        // Frozen at finalization
        public SubjectCategory SubjectCategory { get; set; } // Məcburi / Seçmə
        public int Credits { get; set; }               // Kredit

        // --- Scores ---
        public int PreExamTotal { get; set; }          // İmtahan qabağı bal
        public int? ExamScore { get; set; }            // İmtahan balı
        public int TotalScore { get; set; }            // Yekun bal
        public string LetterGrade { get; set; }        // A, B, C, D, F — Dərəcə
        public decimal GradePoint { get; set; }        // 4.0, 3.0, 2.0, 1.0, 0.0

        // --- Status ---
        public bool IsRetake { get; set; }             // Təkrar Dərs
        public bool IsPassed { get; set; }             // Whether student passed

        // --- Finalization ---
        public DateTime? FinalizedAt { get; set; }
        public int? FinalizedByTeacherId { get; set; }

        // ── Computed (Not Stored in DB) ──

        [NotMapped]
        public decimal WeightedPoints => GradePoint * Credits;
    }
}
