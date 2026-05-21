using MediatR;
using System.Collections.Generic;

namespace Application.Modules.GradesModule.Queries.GetCurrentSuccess
{
    /// <summary>
    /// Query to retrieve "Cari Müvəffəqiyyət" (Current Success) dashboard data
    /// for a student in a specific lesson.
    /// Shows real-time GPA, progress, and score breakdown.
    /// </summary>
    public class GetCurrentSuccessQuery : IRequest<CurrentSuccessResponseDto>
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
    }

    public class CurrentSuccessResponseDto
    {
        // Lesson info
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public int Credits { get; set; }

        // Score breakdown
        public int AttendanceScore { get; set; }
        public int AttendanceMaxScore { get; set; }
        public int? PracticalScore { get; set; }
        public int PracticalMaxScore { get; set; }
        public int? FreelanceWorkScore { get; set; }
        public int FreelanceWorkMaxScore { get; set; }
        public int? LaboratoryScore { get; set; }
        public int LaboratoryMaxScore { get; set; }
        public int? MidtermScore { get; set; }

        // Pre-exam total (İmtahan qabağı bal)
        public int PreExamTotal { get; set; }
        public int PreExamMaxTotal { get; set; }       // 50

        // Exam
        public int? ExamScore { get; set; }
        public int ExamMaxScore { get; set; }          // 50

        // Grand total
        public int? GrandTotal { get; set; }

        // Letter grade & GPA
        public string LetterGrade { get; set; }
        public string GradeDescription { get; set; }   // Əla, Yaxşı, etc.
        public decimal? GradePoint { get; set; }

        // Assignment completion stats
        public List<AssignmentCategoryProgressDto> AssignmentProgress { get; set; }
    }

    public class AssignmentCategoryProgressDto
    {
        public string Category { get; set; }           // Sərbəst iş, Məşğələ, Laboratoriya
        public int Completed { get; set; }
        public int Total { get; set; }
        public int TotalScore { get; set; }
        public int MaxPossibleScore { get; set; }
    }
}
