using MediatR;

namespace Application.Modules.GradesModule.Commands.SubmitGrade
{
    /// <summary>
    /// Command for a teacher to submit/update a student's grade in a lesson.
    /// </summary>
    public class SubmitGradeCommand : IRequest<bool>
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }

        // Individual score components
        public int? ManualPracticalScore { get; set; }
        public int? FreelanceWork { get; set; }
        public int? MidtermScore { get; set; }
        public int? LaboratoryScore { get; set; }
        public int? ExamScore { get; set; }
    }
}
