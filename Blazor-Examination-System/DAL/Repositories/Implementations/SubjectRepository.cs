using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Examination_System.DAL.Repositories.Interfaces;

public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Subject>> GetAllWithExamsAsync()
    {
        return await _context.Subjects
            .Include(s => s.Exams)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Subject?> GetByIdWithExamsAsync(int id)
    {
        return await _context.Subjects
            .Include(s => s.Exams)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
