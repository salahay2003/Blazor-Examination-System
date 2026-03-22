using Blazor_Examination_System.BLL.Managers.Interfaces;
using Blazor_Examination_System.DAL.Repositories.Interfaces;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Implementations;

public class SubjectManager : ISubjectManager
{
    private readonly ISubjectRepository _repository;

    public SubjectManager(ISubjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Subject>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<List<Subject>> GetAllWithExamsAsync()
    {
        return await _repository.GetAllWithExamsAsync();
    }

    public async Task<Subject?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Subject> CreateAsync(Subject subject)
    {
        if (string.IsNullOrWhiteSpace(subject.Name))
            throw new ArgumentException("Subject name is required");

        subject.CreatedAt = DateTime.UtcNow;
        return await _repository.AddAsync(subject);
    }

    public async Task<bool> UpdateAsync(Subject subject)
    {
        var existing = await _repository.GetByIdAsync(subject.Id);
        if (existing == null)
            return false;

        existing.Name = subject.Name;
        existing.Description = subject.Description;
        existing.IsActive = subject.IsActive;

        await _repository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _repository.DeleteAsync(existing);
        return true;
    }
}
