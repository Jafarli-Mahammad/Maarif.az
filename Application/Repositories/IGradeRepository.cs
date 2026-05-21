using Domain.Models.Entities;
using Infrastructure.Abstracts;

namespace Application.Repositories
{
    public interface IGradeRepository : IAsyncRepository<Grade>
    {
        /// <summary>
        /// Get all grades for a specific lesson, including student details.
        /// </summary>
        Task<IReadOnlyList<Grade>> GetByLessonIdAsync(int lessonId, CancellationToken ct = default);

        /// <summary>
        /// Get all grades for a specific student across all lessons.
        /// </summary>
        Task<IReadOnlyList<Grade>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);

        /// <summary>
        /// Get a single grade for a student in a specific lesson.
        /// </summary>
        Task<Grade?> GetByStudentAndLessonAsync(int studentId, int lessonId, CancellationToken ct = default);
    }
}
