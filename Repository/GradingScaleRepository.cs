using Application.Repositories;
using DataAccessLayer.Migrations;
using Domain.Models.Entities;
using Infrastructure.Concrates;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GradingScaleRepository : AsyncRepository<GradingScale>, IGradingScaleRepository
    {
        private readonly DataContext _context;

        public GradingScaleRepository(DataContext db) : base(db)
        {
            _context = db;
        }

        public async Task<GradingScale?> GetByScoreAsync(decimal score, CancellationToken ct = default)
        {
            return await _context.GradingScales
                .AsNoTracking()
                .FirstOrDefaultAsync(gs => score >= gs.MinScore && score <= gs.MaxScore, ct);
        }

        public async Task<IReadOnlyList<GradingScale>> GetAllOrderedAsync(CancellationToken ct = default)
        {
            return await _context.GradingScales
                .AsNoTracking()
                .OrderByDescending(gs => gs.MinScore)
                .ToListAsync(ct);
        }
    }
}
