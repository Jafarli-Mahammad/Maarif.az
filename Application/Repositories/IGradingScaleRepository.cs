using Domain.Models.Entities;
using Infrastructure.Abstracts;

namespace Application.Repositories
{
    public interface IGradingScaleRepository : IAsyncRepository<GradingScale>
    {
        /// <summary>
        /// Get the grading scale entry that matches a given numeric score.
        /// </summary>
        Task<GradingScale?> GetByScoreAsync(decimal score, CancellationToken ct = default);

        /// <summary>
        /// Get all grading scale entries ordered by MinScore descending.
        /// </summary>
        Task<IReadOnlyList<GradingScale>> GetAllOrderedAsync(CancellationToken ct = default);
    }
}
