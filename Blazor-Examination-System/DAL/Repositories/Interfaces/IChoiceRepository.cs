using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public interface IChoiceRepository : IGenericRepository<Choice>
{
    Task<List<Choice>> GetByQuestionIdAsync(int questionId);
    Task DeleteByQuestionIdAsync(int questionId);
    Task AddRangeAsync(List<Choice> choices);
}
