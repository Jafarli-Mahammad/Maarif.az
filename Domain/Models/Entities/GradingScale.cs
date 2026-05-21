using Domain.Models.Concrates;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Reference data: the Azerbaijani university standard grading scale.
    /// Maps numeric score ranges to letter grades and GPA points.
    /// This table is seeded once and used for lookups.
    /// </summary>
    public class GradingScale : AuditableEntity
    {
        public int Id { get; set; }

        public string LetterGrade { get; set; }        // A, B, C, D, F
        public decimal MinScore { get; set; }          // Lower bound (inclusive)
        public decimal MaxScore { get; set; }          // Upper bound (inclusive)
        public decimal GradePoint { get; set; }        // GPA equivalent (4.0, 3.0, 2.0, 1.0, 0.0)
        public string Description { get; set; }        // "Əla", "Yaxşı", "Kafi", "Qeyri-kafi", "Zəif"
    }
}
