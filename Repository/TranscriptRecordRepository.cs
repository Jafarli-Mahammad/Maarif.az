using Application.Repositories;
using DataAccessLayer.Migrations;
using Domain.Models.Entities;
using Domain.Models.Stables;
using Infrastructure.Concrates;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TranscriptRecordRepository : AsyncRepository<TranscriptRecord>, ITranscriptRecordRepository
    {
        private readonly DataContext _context;

        public TranscriptRecordRepository(DataContext db) : base(db)
        {
            _context = db;
        }

        public async Task<IReadOnlyList<TranscriptRecord>> GetByStudentIdAsync(int studentId, CancellationToken ct = default)
        {
            return await _context.TranscriptRecords
                .AsNoTracking()
                .Where(tr => tr.StudentId == studentId)
                .OrderByDescending(tr => tr.AcademicYear)
                .ThenByDescending(tr => tr.Semester)
                .ThenBy(tr => tr.SubjectName)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<TranscriptRecord>> GetByStudentAndTermAsync(
            int studentId, string academicYear, int semester, CancellationToken ct = default)
        {
            return await _context.TranscriptRecords
                .AsNoTracking()
                .Where(tr => tr.StudentId == studentId
                          && tr.AcademicYear == academicYear
                          && tr.Semester == (SemesterType)semester)
                .OrderBy(tr => tr.SubjectName)
                .ToListAsync(ct);
        }
    }
}
