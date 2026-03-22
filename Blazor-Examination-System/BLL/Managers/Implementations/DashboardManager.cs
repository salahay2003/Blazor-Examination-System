using Blazor_Examination_System.BLL.DTOs;
using Blazor_Examination_System.BLL.Managers.Interfaces;
using Blazor_Examination_System.DAL.Repositories.Interfaces;
using Blazor_Examination_System.Data;

namespace Blazor_Examination_System.BLL.Managers.Implementations;

public class DashboardManager : IDashboardManager
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IExamRepository _examRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IStudentExamRepository _studentExamRepository;
    private readonly IGenericRepository<ApplicationUser> _userRepository;

    public DashboardManager(
        ISubjectRepository subjectRepository,
        IExamRepository examRepository,
        IQuestionRepository questionRepository,
        IStudentExamRepository studentExamRepository,
        IGenericRepository<ApplicationUser> userRepository)
    {
        _subjectRepository = subjectRepository;
        _examRepository = examRepository;
        _questionRepository = questionRepository;
        _studentExamRepository = studentExamRepository;
        _userRepository = userRepository;
    }

    public async Task<DashboardDto> GetDashboardDataAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        var exams = await _examRepository.GetAllWithSubjectAsync();
        var questions = await _questionRepository.GetAllAsync();
        var allUsers = await _userRepository.GetAllAsync();
        var completedExams = (await _studentExamRepository.GetAllAsync())
            .Where(e => e.CompletedAt.HasValue)
            .ToList();

        // Assuming we count all users as potential students (or use a different approach)
        var studentCount = allUsers.Count;

        var examsPerSubject = exams
            .GroupBy(e => e.SubjectId)
            .Select(g => new SubjectExamCount
            {
                SubjectName = g.First().Subject?.Name ?? "Unknown",
                ExamCount = g.Count()
            })
            .ToList();

        var recentResults = completedExams
            .OrderByDescending(r => r.CompletedAt)
            .Take(5)
            .Select(r => new RecentResult
            {
                StudentName = r.Student?.FullName ?? "Unknown",
                ExamTitle = r.Exam?.Title ?? "Unknown",
                SubjectName = r.Exam?.Subject?.Name ?? "Unknown",
                Score = r.Score,
                TotalMarks = r.Exam?.TotalMarks ?? 0,
                IsPassed = r.IsPassed,
                CompletedAt = r.CompletedAt ?? DateTime.UtcNow
            })
            .ToList();

        var averageScore = completedExams.Any()
            ? (double)(completedExams.Sum(e => (decimal)e.Score) / completedExams.Sum(e => (decimal)e.Exam!.TotalMarks) * 100)
            : 0;

        return new DashboardDto
        {
            TotalSubjects = subjects.Count,
            TotalExams = exams.Count,
            TotalQuestions = questions.Count,
            TotalStudents = studentCount,
            TotalExamsTaken = completedExams.Count,
            AverageScorePercentage = averageScore,
            ExamsPerSubject = examsPerSubject,
            RecentResults = recentResults
        };
    }
}
