using System.ComponentModel.DataAnnotations;

namespace Blazor_Examination_System.Models;

public class Exam
{
    public int Id { get; set; }

    [Required, MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int DurationInMinutes { get; set; } = 60;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public int TotalMarks { get; set; }
    public int PassingMarks { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
}
