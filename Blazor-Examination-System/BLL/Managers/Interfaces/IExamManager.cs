using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Interfaces;

public interface IExamManager
{
    Task<List<Exam>> GetAllWithSubjectAsync();
    Task<Exam?> GetByIdAsync(int id);
    Task<Exam?> GetByIdWithDetailsAsync(int id);
    Task<List<Exam>> GetBySubjectIdAsync(int subjectId);
    Task<Exam> CreateAsync(Exam exam);
    Task<bool> UpdateAsync(Exam exam);
    Task<bool> DeleteAsync(int id);
    Task<List<Exam>> GetAvailableExamsForStudentAsync(string studentId);
}
