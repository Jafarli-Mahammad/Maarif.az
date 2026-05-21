using FluentValidation;

namespace Application.Modules.GradesModule.Commands.SubmitGrade
{
    public class SubmitGradeCommandValidator : AbstractValidator<SubmitGradeCommand>
    {
        public SubmitGradeCommandValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0);
            RuleFor(x => x.LessonId).GreaterThan(0);
            RuleFor(x => x.ManualPracticalScore).InclusiveBetween(0, 100).When(x => x.ManualPracticalScore.HasValue);
            RuleFor(x => x.FreelanceWork).InclusiveBetween(0, 100).When(x => x.FreelanceWork.HasValue);
            RuleFor(x => x.MidtermScore).InclusiveBetween(0, 100).When(x => x.MidtermScore.HasValue);
            RuleFor(x => x.LaboratoryScore).InclusiveBetween(0, 100).When(x => x.LaboratoryScore.HasValue);
            RuleFor(x => x.ExamScore).InclusiveBetween(0, 50).When(x => x.ExamScore.HasValue);
        }
    }
}
