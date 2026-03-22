using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public class StudentExamRepository : GenericRepository<StudentExam>, IStudentExamRepository
{
    public StudentExamRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<StudentExam>> GetByStudentIdAsync(string studentId)
    {
        return await _context.StudentExams
            .Where(se => se.StudentId == studentId)
            .Include(se => se.Exam)
            .ThenInclude(e => e.Subject)
            .OrderByDescending(se => se.StartedAt)
            .ToListAsync();
    }

    public async Task<StudentExam?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.StudentExams
            .Include(se => se.Exam)
            .ThenInclude(e => e.Subject)
            .Include(se => se.StudentAnswers)
            .ThenInclude(sa => sa.Question)
            .Include(se => se.StudentAnswers)
            .ThenInclude(sa => sa.SelectedChoice)
            .FirstOrDefaultAsync(se => se.Id == id);
    }

    public async Task<bool> HasStudentTakenExamAsync(string studentId, int examId)
    {
        return await _context.StudentExams
            .AnyAsync(se => se.StudentId == studentId && se.ExamId == examId && se.IsCompleted);
    }

    public async Task<List<StudentExam>> GetAllWithDetailsAsync()
    {
        return await _context.StudentExams
            .Where(se => se.IsCompleted)
            .Include(se => se.Student)
            .Include(se => se.Exam)
            .ThenInclude(e => e.Subject)
            .OrderByDescending(se => se.CompletedAt)
            .ToListAsync();
    }

    public async Task<StudentExam?> GetActiveExamAsync(string studentId, int examId)
    {
        return await _context.StudentExams
            .Where(se => se.StudentId == studentId && se.ExamId == examId && !se.IsCompleted)
            .Include(se => se.Exam)
            .FirstOrDefaultAsync();
    }

    public async Task<List<StudentExam>> GetCompletedExamsAsync()
    {
        return await _context.StudentExams
            .Where(se => se.IsCompleted)
            .Include(se => se.Student)
            .Include(se => se.Exam)
            .ThenInclude(e => e.Subject)
            .OrderByDescending(se => se.CompletedAt)
            .ToListAsync();
    }
}
