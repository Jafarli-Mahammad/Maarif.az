using MediatR;
using System.Collections.Generic;

namespace Application.Modules.TranscriptsModule.Queries.GetStudentTranscript
{
    /// <summary>
    /// Query to retrieve the full academic transcript for a student.
    /// Returns data grouped by semester.
    /// </summary>
    public class GetStudentTranscriptQuery : IRequest<StudentTranscriptResponseDto>
    {
        public int StudentId { get; set; }
    }

    /// <summary>
    /// Full transcript response, including semester summaries and detailed records.
    /// </summary>
    public class StudentTranscriptResponseDto
    {
        public int StudentId { get; set; }
        public string StudentFullName { get; set; }
        public string StudentNumber { get; set; }

        // Semester-by-semester breakdown
        public List<SemesterSummaryDto> Semesters { get; set; }

        // Cumulative totals
        public int TotalSubjects { get; set; }
        public int TotalCredits { get; set; }
        public int EarnedCredits { get; set; }
        public decimal CumulativeGPA { get; set; }
    }

    /// <summary>
    /// Summary of one semester: totals and individual subject records.
    /// </summary>
    public class SemesterSummaryDto
    {
        public string AcademicYear { get; set; }
        public string Semester { get; set; }          // "Payız" or "Yaz"
        public string DisplayLabel { get; set; }       // e.g., "2025 Payız"

        // Semester aggregates
        public int SubjectCount { get; set; }
        public int AttendedSubjectCount { get; set; }
        public int TotalCredits { get; set; }
        public int EarnedCredits { get; set; }
        public decimal SemesterGPA { get; set; }

        // Individual records
        public List<TranscriptRecordDto> Records { get; set; }
    }

    /// <summary>
    /// One line on the transcript — a single subject in a semester.
    /// </summary>
    public class TranscriptRecordDto
    {
        public int Id { get; set; }
        public string SubjectCategory { get; set; }    // Məcburi / Seçmə
        public string SubjectName { get; set; }
        public int Credits { get; set; }               // Kredit
        public int PreExamTotal { get; set; }          // İmtahan qabağı bal
        public int? ExamScore { get; set; }            // İmtahan balı
        public int TotalScore { get; set; }            // Yekun bal
        public string LetterGrade { get; set; }        // Dərəcə
        public bool IsRetake { get; set; }             // Təkrar Dərs
    }
}
