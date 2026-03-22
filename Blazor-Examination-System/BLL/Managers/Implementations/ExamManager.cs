using Blazor_Examination_System.BLL.Managers.Interfaces;
using Blazor_Examination_System.DAL.Repositories.Interfaces;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Implementations;

public class ExamManager : IExamManager
{
    private readonly IExamRepository _repository;

    public ExamManager(IExamRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Exam>> GetAllWithSubjectAsync()
    {
        return await _repository.GetAllWithSubjectAsync();
    }

    public async Task<Exam?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Exam?> GetByIdWithDetailsAsync(int id)
    {
        return await _repository.GetByIdWithDetailsAsync(id);
    }

    public async Task<List<Exam>> GetBySubjectIdAsync(int subjectId)
    {
        return await _repository.GetBySubjectIdAsync(subjectId);
    }

    public async Task<Exam> CreateAsync(Exam exam)
    {
        if (string.IsNullOrWhiteSpace(exam.Title))
            throw new ArgumentException("Exam title is required");

        if (exam.DurationInMinutes <= 0)
            throw new ArgumentException("Duration must be greater than 0");

        // ★★★ Auto-fix: if PassingMarks > TotalMarks, adjust it ★★★
        if (exam.PassingMarks > exam.TotalMarks && exam.TotalMarks > 0)
        {
            exam.PassingMarks = Math.Max(1, (int)Math.Ceiling(exam.TotalMarks * 0.6));
            Console.WriteLine($"📊 Auto-fixed PassingMarks on exam creation: {exam.Title}");
            Console.WriteLine($"   TotalMarks: {exam.TotalMarks}, PassingMarks: {exam.PassingMarks}");
        }

        // If no total marks yet, set passing to 0
        if (exam.TotalMarks == 0)
        {
            exam.PassingMarks = 0;
        }

        exam.CreatedAt = DateTime.UtcNow;
        return await _repository.AddAsync(exam);
    }

    public async Task<bool> UpdateAsync(Exam exam)
    {
        var existing = await _repository.GetByIdAsync(exam.Id);
        if (existing == null)
            return false;

        if (exam.DurationInMinutes <= 0)
            throw new ArgumentException("Duration must be greater than 0");

        // ★★★ Auto-fix: if PassingMarks > TotalMarks, adjust it ★★★
        if (exam.PassingMarks > exam.TotalMarks && exam.TotalMarks > 0)
        {
            exam.PassingMarks = Math.Max(1, (int)Math.Ceiling(exam.TotalMarks * 0.6));
            Console.WriteLine($"📊 Auto-fixed PassingMarks on exam update: {exam.Title}");
            Console.WriteLine($"   TotalMarks: {exam.TotalMarks}, PassingMarks: {exam.PassingMarks}");
        }

        existing.Title = exam.Title;
        existing.Description = exam.Description;
        existing.DurationInMinutes = exam.DurationInMinutes;
        existing.TotalMarks = exam.TotalMarks;
        existing.PassingMarks = exam.PassingMarks;
        existing.IsActive = exam.IsActive;
        existing.SubjectId = exam.SubjectId;

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

    public async Task<List<Exam>> GetAvailableExamsForStudentAsync(string studentId)
    {
        return await _repository.GetActiveExamsNotTakenByStudentAsync(studentId);
    }
}
