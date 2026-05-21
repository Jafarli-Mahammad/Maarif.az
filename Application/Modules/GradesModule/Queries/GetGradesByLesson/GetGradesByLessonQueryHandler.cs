using Application.Repositories;
using MediatR;

namespace Application.Modules.GradesModule.Queries.GetGradesByLesson
{
    public class GetGradesByLessonQueryHandler : IRequestHandler<GetGradesByLessonQuery, List<GradeResponseDto>>
    {
        private readonly IGradeRepository _gradeRepository;

        public GetGradesByLessonQueryHandler(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<List<GradeResponseDto>> Handle(GetGradesByLessonQuery request, CancellationToken cancellationToken)
        {
            var grades = await _gradeRepository.GetByLessonIdAsync(request.LessonId, cancellationToken);

            return grades.Select(g => new GradeResponseDto
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentFullName = g.Student.FullName,
                StudentNumber = g.Student.StudentNumber,
                LessonId = g.LessonId,
                AttendanceScore = g.AttendanceScore,
                ManualPracticalScore = g.ManualPracticalScore,
                FreelanceWork = g.FreelanceWork,
                MidtermScore = g.MidtermScore,
                LaboratoryScore = g.LaboratoryScore,
                ExamScore = g.ExamScore,
                SemesterTotal = g.SemesterTotal,
                GrandTotal = g.GrandTotal,
                LetterGrade = g.LetterGrade,
                GradePointValue = g.GradePointValue,
                IsFinalized = g.IsFinalized
            }).ToList();
        }
    }
}
