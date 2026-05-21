using Domain.Models.Entities;
using Infrastructure.Abstracts;

namespace Application.Repositories
{
    public interface ITranscriptRecordRepository : IAsyncRepository<TranscriptRecord>
    {
        /// <summary>
        /// Get all transcript records for a student, grouped by academic year and semester.
        /// </summary>
        Task<IReadOnlyList<TranscriptRecord>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);

        /// <summary>
        /// Get transcript records for a specific student in a specific academic year/semester.
        /// </summary>
        Task<IReadOnlyList<TranscriptRecord>> GetByStudentAndTermAsync(
            int studentId, string academicYear, int semester, CancellationToken ct = default);
    }
}
