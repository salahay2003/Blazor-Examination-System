using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public interface IQuestionRepository : IGenericRepository<Question>
{
    Task<List<Question>> GetByExamIdWithChoicesAsync(int examId);
    Task<Question?> GetByIdWithChoicesAsync(int id);
    Task<int> GetMaxOrderIndexByExamIdAsync(int examId);
}
