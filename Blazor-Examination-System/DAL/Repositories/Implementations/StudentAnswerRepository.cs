using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public class StudentAnswerRepository : GenericRepository<StudentAnswer>, IStudentAnswerRepository
{
    public StudentAnswerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<StudentAnswer>> GetByStudentExamIdAsync(int studentExamId)
    {
        return await _context.StudentAnswers
            .Where(sa => sa.StudentExamId == studentExamId)
            .Include(sa => sa.Question)
            .Include(sa => sa.SelectedChoice)
            .ToListAsync();
    }

    public async Task<StudentAnswer?> GetByStudentExamAndQuestionAsync(int studentExamId, int questionId)
    {
        return await _context.StudentAnswers
            .FirstOrDefaultAsync(sa => sa.StudentExamId == studentExamId && sa.QuestionId == questionId);
    }
}
