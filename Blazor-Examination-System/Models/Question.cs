using System.ComponentModel.DataAnnotations;

namespace Blazor_Examination_System.Models;

public class Question
{
    public int Id { get; set; }

    [Required, MaxLength(2000)]
    public string Text { get; set; } = string.Empty;

    public int Marks { get; set; } = 1;
    public int OrderIndex { get; set; }

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
    public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
