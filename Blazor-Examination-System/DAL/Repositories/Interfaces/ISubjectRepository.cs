using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public interface ISubjectRepository : IGenericRepository<Subject>
{
    Task<List<Subject>> GetAllWithExamsAsync();
    Task<Subject?> GetByIdWithExamsAsync(int id);
}
