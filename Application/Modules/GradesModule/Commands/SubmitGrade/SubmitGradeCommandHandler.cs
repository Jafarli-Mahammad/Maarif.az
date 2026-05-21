using Application.Repositories;
using Domain.Models.Entities;
using MediatR;

namespace Application.Modules.GradesModule.Commands.SubmitGrade
{
    public class SubmitGradeCommandHandler : IRequestHandler<SubmitGradeCommand, bool>
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IGradingScaleRepository _gradingScaleRepository;

        public SubmitGradeCommandHandler(
            IGradeRepository gradeRepository,
            IGradingScaleRepository gradingScaleRepository)
        {
            _gradeRepository = gradeRepository;
            _gradingScaleRepository = gradingScaleRepository;
        }

        public async Task<bool> Handle(SubmitGradeCommand request, CancellationToken cancellationToken)
        {
            // Try to find an existing grade record
            var grade = await _gradeRepository.GetByStudentAndLessonAsync(
                request.StudentId, request.LessonId, cancellationToken);

            if (grade == null)
            {
                // Create new grade
                grade = new Grade
                {
                    StudentId = request.StudentId,
                    LessonId = request.LessonId,
                    ManualPracticalScore = request.ManualPracticalScore,
                    FreelanceWork = request.FreelanceWork,
                    MidtermScore = request.MidtermScore,
                    LaboratoryScore = request.LaboratoryScore,
                    ExamScore = request.ExamScore,
                };

                // Auto-calculate letter grade if we have enough data
                await ApplyLetterGradeAsync(grade, cancellationToken);

                await _gradeRepository.AddAsync(grade, cancellationToken);
            }
            else
            {
                // Update existing grade
                if (grade.IsFinalized)
                    return false; // Cannot modify finalized grades

                grade.ManualPracticalScore = request.ManualPracticalScore ?? grade.ManualPracticalScore;
                grade.FreelanceWork = request.FreelanceWork ?? grade.FreelanceWork;
                grade.MidtermScore = request.MidtermScore ?? grade.MidtermScore;
                grade.LaboratoryScore = request.LaboratoryScore ?? grade.LaboratoryScore;
                grade.ExamScore = request.ExamScore ?? grade.ExamScore;

                await ApplyLetterGradeAsync(grade, cancellationToken);

                await _gradeRepository.EditAsync(grade);
            }

            await _gradeRepository.SaveAsync(cancellationToken);
            return true;
        }

        private async Task ApplyLetterGradeAsync(Grade grade, CancellationToken ct)
        {
            if (grade.GrandTotal.HasValue)
            {
                var scale = await _gradingScaleRepository.GetByScoreAsync(grade.GrandTotal.Value, ct);
                if (scale != null)
                {
                    grade.LetterGrade = scale.LetterGrade;
                    grade.GradePointValue = scale.GradePoint;
                }
            }
        }
    }
}
