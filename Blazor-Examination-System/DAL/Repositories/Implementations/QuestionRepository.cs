using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    public QuestionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Question>> GetByExamIdWithChoicesAsync(int examId)
    {
        return await _context.Questions
            .Where(q => q.ExamId == examId)
            .Include(q => q.Choices)
            .OrderBy(q => q.OrderIndex)
            .ToListAsync();
    }

    public async Task<Question?> GetByIdWithChoicesAsync(int id)
    {
        return await _context.Questions
            .Include(q => q.Choices)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<int> GetMaxOrderIndexByExamIdAsync(int examId)
    {
        var max = await _context.Questions
            .Where(q => q.ExamId == examId)
            .MaxAsync(q => (int?)q.OrderIndex);

        return max ?? 0;
    }
}
