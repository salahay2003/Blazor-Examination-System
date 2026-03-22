using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public interface IStudentAnswerRepository : IGenericRepository<StudentAnswer>
{
    Task<List<StudentAnswer>> GetByStudentExamIdAsync(int studentExamId);
    Task<StudentAnswer?> GetByStudentExamAndQuestionAsync(int studentExamId, int questionId);
}
