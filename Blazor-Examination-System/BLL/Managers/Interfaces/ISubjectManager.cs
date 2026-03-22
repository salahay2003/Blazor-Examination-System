using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Interfaces;

public interface ISubjectManager
{
    Task<List<Subject>> GetAllAsync();
    Task<List<Subject>> GetAllWithExamsAsync();
    Task<Subject?> GetByIdAsync(int id);
    Task<Subject> CreateAsync(Subject subject);
    Task<bool> UpdateAsync(Subject subject);
    Task<bool> DeleteAsync(int id);
}
