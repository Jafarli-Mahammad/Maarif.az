using Domain.Models.Entities;
using Infrastructure.Abstracts;

namespace Application.Repositories
{
    public interface ILectureRepository : IAsyncRepository<Lecture>
    {
        /// <summary>
        /// Get all lectures for a lesson, ordered by <see cref="Lecture.OrderIndex"/>.
        /// </summary>
        Task<IReadOnlyList<Lecture>> GetByLessonIdAsync(int lessonId, CancellationToken ct = default);
    }
}
