using Application.Repositories;
using DataAccessLayer.Migrations;
using Domain.Models.Entities;
using Infrastructure.Concrates;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LectureRepository : AsyncRepository<Lecture>, ILectureRepository
    {
        private readonly DataContext _context;

        public LectureRepository(DataContext db) : base(db)
        {
            _context = db;
        }

        public async Task<IReadOnlyList<Lecture>> GetByLessonIdAsync(int lessonId, CancellationToken ct = default)
        {
            return await _context.Lectures
                .AsNoTracking()
                .Include(l => l.Materials)
                .Where(l => l.LessonId == lessonId)
                .OrderBy(l => l.OrderIndex)
                .ToListAsync(ct);
        }
    }
}
