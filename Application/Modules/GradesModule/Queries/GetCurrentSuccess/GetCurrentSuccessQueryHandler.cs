using Application.Repositories;
using Domain.Models.Stables;
using MediatR;
using System.Linq;

namespace Application.Modules.GradesModule.Queries.GetCurrentSuccess
{
    public class GetCurrentSuccessQueryHandler
        : IRequestHandler<GetCurrentSuccessQuery, CurrentSuccessResponseDto>
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IGradingScaleRepository _gradingScaleRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public GetCurrentSuccessQueryHandler(
            IGradeRepository gradeRepository,
            IGradingScaleRepository gradingScaleRepository,
            ILessonRepository lessonRepository,
            IAssignmentRepository assignmentRepository,
            ISubmissionRepository submissionRepository)
        {
            _gradeRepository = gradeRepository;
            _gradingScaleRepository = gradingScaleRepository;
            _lessonRepository = lessonRepository;
            _assignmentRepository = assignmentRepository;
            _submissionRepository = submissionRepository;
        }

        public async Task<CurrentSuccessResponseDto> Handle(
            GetCurrentSuccessQuery request, CancellationToken cancellationToken)
        {
            // Get the lesson with subject and teacher details
            var lesson = await _lessonRepository.GetAsync(
                l => l.Id == request.LessonId, cancellationToken);

            var subject = lesson.Subject;

            // Get or create the grade record
            var grade = await _gradeRepository.GetByStudentAndLessonAsync(
                request.StudentId, request.LessonId, cancellationToken);

            // Get assignments and submissions for progress tracking
            var assignments = _assignmentRepository
                .GetAll(a => a.LessonId == request.LessonId && a.DeletedAt == null)
                .ToList();

            var submissions = _submissionRepository
                .GetAll(s => s.StudentId == request.StudentId
                          && assignments.Select(a => a.Id).Contains(s.AssignmentId)
                          && s.DeletedAt == null)
                .ToList();

            // Calculate assignment progress by category
            var categoryProgress = assignments
                .GroupBy(a => a.Type)
                .Select(g =>
                {
                    var categorySubmissions = submissions
                        .Where(s => g.Select(a => a.Id).Contains(s.AssignmentId))
                        .ToList();

                    return new AssignmentCategoryProgressDto
                    {
                        Category = GetCategoryDisplayName(g.Key),
                        Total = g.Count(),
                        Completed = categorySubmissions.Count(s => s.Grade.HasValue),
                        TotalScore = categorySubmissions.Where(s => s.Grade.HasValue).Sum(s => s.Grade ?? 0),
                        MaxPossibleScore = g.Sum(a => a.MaxGrade)
                    };
                })
                .ToList();

            // Look up letter grade
            string letterGrade = grade?.LetterGrade ?? "-";
            string gradeDescription = "-";
            decimal? gradePoint = grade?.GradePointValue;

            if (grade?.GrandTotal.HasValue == true)
            {
                var scale = await _gradingScaleRepository.GetByScoreAsync(
                    grade.GrandTotal.Value, cancellationToken);
                if (scale != null)
                {
                    letterGrade = scale.LetterGrade;
                    gradeDescription = scale.Description;
                    gradePoint = scale.GradePoint;
                }
            }

            return new CurrentSuccessResponseDto
            {
                SubjectName = subject?.Name ?? "Unknown",
                TeacherName = lesson.Teacher?.FullName ?? "Unknown",
                Credits = subject?.Credits ?? 0,

                AttendanceScore = grade?.AttendanceScore ?? 0,
                AttendanceMaxScore = subject?.AttendanceScore ?? 0,
                PracticalScore = grade?.ManualPracticalScore,
                PracticalMaxScore = subject?.SeminarScore ?? 0,
                FreelanceWorkScore = grade?.FreelanceWork,
                FreelanceWorkMaxScore = subject?.FreeWorkScore ?? 0,
                LaboratoryScore = grade?.LaboratoryScore,
                LaboratoryMaxScore = subject?.LabScore ?? 0,
                MidtermScore = grade?.MidtermScore,

                PreExamTotal = grade?.SemesterTotal ?? 0,
                PreExamMaxTotal = 50,

                ExamScore = grade?.ExamScore,
                ExamMaxScore = subject?.ExamScore ?? 50,

                GrandTotal = grade?.GrandTotal,

                LetterGrade = letterGrade,
                GradeDescription = gradeDescription,
                GradePoint = gradePoint,

                AssignmentProgress = categoryProgress
            };
        }

        private static string GetCategoryDisplayName(AssignmentType type) => type switch
        {
            AssignmentType.FreeWork => "Sərbəst iş",
            AssignmentType.Seminar => "Məşğələ",
            AssignmentType.Laboratory => "Laboratoriya",
            AssignmentType.Kollokvium => "Kollokvium",
            AssignmentType.CourseWork => "Kurs işi",
            _ => "Digər"
        };
    }
}
