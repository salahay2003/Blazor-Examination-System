using Blazor_Examination_System.Data;

namespace Blazor_Examination_System.Models;

public class StudentExam
{
    public int Id { get; set; }

    public string StudentId { get; set; } = string.Empty;
    public ApplicationUser Student { get; set; } = null!;

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public int Score { get; set; }
    public bool IsPassed { get; set; }
    public bool IsCompleted { get; set; } = false;

    public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
