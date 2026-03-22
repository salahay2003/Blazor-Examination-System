using Blazor_Examination_System.BLL.DTOs;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Interfaces;

public interface IStudentExamManager
{
    Task<StudentExam> StartExamAsync(string studentId, int examId);
    Task<StudentAnswer> SubmitAnswerAsync(int studentExamId, int questionId, int choiceId);
    Task<StudentExam> CompleteExamAsync(int studentExamId);
    Task<List<StudentExam>> GetStudentExamsAsync(string studentId);
    Task<ExamResultDto?> GetExamResultAsync(int studentExamId);
    Task<bool> HasStudentTakenExamAsync(string studentId, int examId);
    Task<List<StudentExam>> GetAllResultsAsync();
    Task<StudentExam?> GetActiveExamAsync(string studentId, int examId);
}
