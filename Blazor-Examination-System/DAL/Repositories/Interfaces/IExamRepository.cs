using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public interface IExamRepository : IGenericRepository<Exam>
{
    Task<List<Exam>> GetAllWithSubjectAsync();
    Task<Exam?> GetByIdWithDetailsAsync(int id);
    Task<List<Exam>> GetBySubjectIdAsync(int subjectId);
    Task<List<Exam>> GetActiveExamsNotTakenByStudentAsync(string studentId);
}
