using Application.Repositories;
using DataAccessLayer.Migrations;
using Domain.Models.Entities;
using Infrastructure.Concrates;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureMaterialRepository : AsyncRepository<LectureMaterial>, ILectureMaterialRepository
    {
        private readonly DataContext _context;

        public LectureMaterialRepository(DataContext db) : base(db)
        {
            _context = db;
        }

        public async Task<IReadOnlyList<LectureMaterial>> GetByLectureIdAsync(int lectureId, CancellationToken ct = default)
        {
            return await _context.LectureMaterials
                .AsNoTracking()
                .Where(m => m.LectureId == lectureId)
                .OrderBy(m => m.Title)
                .ToListAsync(ct);
        }
    }
}
