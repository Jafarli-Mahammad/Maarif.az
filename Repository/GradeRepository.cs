using Application.Repositories;
using DataAccessLayer.Migrations;
using Domain.Models.Entities;
using Infrastructure.Concrates;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GradeRepository : AsyncRepository<Grade>, IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext db) : base(db)
        {
            _context = db;
        }

        public async Task<IReadOnlyList<Grade>> GetByLessonIdAsync(int lessonId, CancellationToken ct = default)
        {
            return await _context.Grades
                .AsNoTracking()
                .Include(g => g.Student)
                .Where(g => g.LessonId == lessonId)
                .OrderBy(g => g.Student.FullName)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Grade>> GetByStudentIdAsync(int studentId, CancellationToken ct = default)
        {
            return await _context.Grades
                .AsNoTracking()
                .Include(g => g.Lesson)
                    .ThenInclude(l => l.Subject)
                .Where(g => g.StudentId == studentId)
                .ToListAsync(ct);
        }

        public async Task<Grade?> GetByStudentAndLessonAsync(int studentId, int lessonId, CancellationToken ct = default)
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Lesson)
                    .ThenInclude(l => l.Subject)
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.LessonId == lessonId, ct);
        }
    }
}
