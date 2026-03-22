using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Interfaces;

public interface IQuestionManager
{
    Task<List<Question>> GetByExamIdAsync(int examId);
    Task<Question?> GetByIdWithChoicesAsync(int id);
    Task<Question> CreateWithChoicesAsync(Question question, List<Choice> choices);
    Task<bool> UpdateWithChoicesAsync(int questionId, Question updatedQuestion, List<Choice> newChoices);
    Task<bool> DeleteAsync(int id);
}
