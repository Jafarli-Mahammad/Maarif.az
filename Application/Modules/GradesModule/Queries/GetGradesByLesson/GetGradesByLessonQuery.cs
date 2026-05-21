using MediatR;
using System.Collections.Generic;

namespace Application.Modules.GradesModule.Queries.GetGradesByLesson
{
    /// <summary>
    /// Query to retrieve all student grades for a specific lesson.
    /// Used by teachers to view the grading table.
    /// </summary>
    public class GetGradesByLessonQuery : IRequest<List<GradeResponseDto>>
    {
        public int LessonId { get; set; }
    }

    /// <summary>
    /// Grade data for display — includes student name and computed totals.
    /// </summary>
    public class GradeResponseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentFullName { get; set; }
        public string StudentNumber { get; set; }
        public int LessonId { get; set; }

        // Scores
        public int AttendanceScore { get; set; }
        public int? ManualPracticalScore { get; set; }
        public int? FreelanceWork { get; set; }
        public int? MidtermScore { get; set; }
        public int? LaboratoryScore { get; set; }
        public int? ExamScore { get; set; }

        // Computed
        public int? SemesterTotal { get; set; }
        public int? GrandTotal { get; set; }

        // Letter grade
        public string LetterGrade { get; set; }
        public decimal? GradePointValue { get; set; }
        public bool IsFinalized { get; set; }
    }
}
