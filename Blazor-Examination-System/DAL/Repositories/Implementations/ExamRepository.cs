using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public class ExamRepository : GenericRepository<Exam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Exam>> GetAllWithSubjectAsync()
    {
        return await _context.Exams
            .Include(e => e.Subject)
            .Include(e => e.Questions)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<Exam?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Exams
            .Include(e => e.Subject)
            .Include(e => e.Questions)
            .ThenInclude(q => q.Choices)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Exam>> GetBySubjectIdAsync(int subjectId)
    {
        return await _context.Exams
            .Where(e => e.SubjectId == subjectId)
            .Include(e => e.Subject)
            .ToListAsync();
    }

    public async Task<List<Exam>> GetActiveExamsNotTakenByStudentAsync(string studentId)
    {
        var takenExamIds = await _context.StudentExams
            .Where(se => se.StudentId == studentId && se.IsCompleted)
            .Select(se => se.ExamId)
            .ToListAsync();

        return await _context.Exams
            .Where(e => e.IsActive && !takenExamIds.Contains(e.Id))
            .Include(e => e.Subject)
            .Include(e => e.Questions)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }
}
