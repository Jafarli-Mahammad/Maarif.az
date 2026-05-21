using Domain.Models.Entities;
using Infrastructure.Abstracts;

namespace Application.Repositories
{
    public interface ILectureMaterialRepository : IAsyncRepository<LectureMaterial>
    {
        /// <summary>
        /// Get all materials for a specific lecture.
        /// </summary>
        Task<IReadOnlyList<LectureMaterial>> GetByLectureIdAsync(int lectureId, CancellationToken ct = default);
    }
}
