using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public class ChoiceRepository : GenericRepository<Choice>, IChoiceRepository
{
    public ChoiceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Choice>> GetByQuestionIdAsync(int questionId)
    {
        return await _context.Choices
            .Where(c => c.QuestionId == questionId)
            .OrderBy(c => c.OrderIndex)
            .ToListAsync();
    }

    public async Task DeleteByQuestionIdAsync(int questionId)
    {
        var choices = await _context.Choices
            .Where(c => c.QuestionId == questionId)
            .ToListAsync();

        _context.Choices.RemoveRange(choices);
        await SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Choice> choices)
    {
        await _context.Choices.AddRangeAsync(choices);
        await SaveChangesAsync();
    }
}
