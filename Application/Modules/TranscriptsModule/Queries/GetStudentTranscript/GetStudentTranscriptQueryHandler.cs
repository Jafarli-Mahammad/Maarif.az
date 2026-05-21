using Application.Repositories;
using Domain.Models.Stables;
using MediatR;

namespace Application.Modules.TranscriptsModule.Queries.GetStudentTranscript
{
    public class GetStudentTranscriptQueryHandler
        : IRequestHandler<GetStudentTranscriptQuery, StudentTranscriptResponseDto>
    {
        private readonly ITranscriptRecordRepository _transcriptRepository;
        private readonly IStudentRepository _studentRepository;

        public GetStudentTranscriptQueryHandler(
            ITranscriptRecordRepository transcriptRepository,
            IStudentRepository studentRepository)
        {
            _transcriptRepository = transcriptRepository;
            _studentRepository = studentRepository;
        }

        public async Task<StudentTranscriptResponseDto> Handle(
            GetStudentTranscriptQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetAsync(
                s => s.Id == request.StudentId, cancellationToken);

            var records = await _transcriptRepository.GetByStudentIdAsync(
                request.StudentId, cancellationToken);

            // Group by academic year + semester
            var semesterGroups = records
                .GroupBy(r => new { r.AcademicYear, r.Semester })
                .OrderByDescending(g => g.Key.AcademicYear)
                .ThenByDescending(g => g.Key.Semester)
                .Select(g =>
                {
                    var semRecords = g.ToList();
                    var totalCredits = semRecords.Sum(r => r.Credits);
                    var earnedCredits = semRecords.Where(r => r.IsPassed).Sum(r => r.Credits);
                    var totalWeightedPoints = semRecords.Sum(r => r.GradePoint * r.Credits);

                    return new SemesterSummaryDto
                    {
                        AcademicYear = g.Key.AcademicYear,
                        Semester = g.Key.Semester.ToString(),
                        DisplayLabel = $"{g.Key.AcademicYear.Split('-')[0]} {g.Key.Semester}",
                        SubjectCount = semRecords.Count,
                        AttendedSubjectCount = semRecords.Count(r => r.ExamScore.HasValue || r.TotalScore > 0),
                        TotalCredits = totalCredits,
                        EarnedCredits = earnedCredits,
                        SemesterGPA = totalCredits > 0
                            ? Math.Round(totalWeightedPoints / totalCredits, 1)
                            : 0,
                        Records = semRecords.Select(r => new TranscriptRecordDto
                        {
                            Id = r.Id,
                            SubjectCategory = r.SubjectCategory == SubjectCategory.Məcburi ? "Məcburi" : "Seçmə",
                            SubjectName = r.SubjectName,
                            Credits = r.Credits,
                            PreExamTotal = r.PreExamTotal,
                            ExamScore = r.ExamScore,
                            TotalScore = r.TotalScore,
                            LetterGrade = r.LetterGrade,
                            IsRetake = r.IsRetake
                        }).ToList()
                    };
                })
                .ToList();

            // Cumulative totals
            var allCredits = records.Sum(r => r.Credits);
            var allEarned = records.Where(r => r.IsPassed).Sum(r => r.Credits);
            var allWeighted = records.Sum(r => r.GradePoint * r.Credits);

            return new StudentTranscriptResponseDto
            {
                StudentId = student.Id,
                StudentFullName = student.FullName,
                StudentNumber = student.StudentNumber,
                Semesters = semesterGroups,
                TotalSubjects = records.Count,
                TotalCredits = allCredits,
                EarnedCredits = allEarned,
                CumulativeGPA = allCredits > 0
                    ? Math.Round(allWeighted / allCredits, 1)
                    : 0
            };
        }
    }
}
