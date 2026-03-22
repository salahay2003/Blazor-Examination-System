using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public interface IStudentExamRepository : IGenericRepository<StudentExam>
{
    Task<List<StudentExam>> GetByStudentIdAsync(string studentId);
    Task<StudentExam?> GetByIdWithDetailsAsync(int id);
    Task<bool> HasStudentTakenExamAsync(string studentId, int examId);
    Task<List<StudentExam>> GetAllWithDetailsAsync();
    Task<StudentExam?> GetActiveExamAsync(string studentId, int examId);
    Task<List<StudentExam>> GetCompletedExamsAsync();
}
