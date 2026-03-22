namespace Blazor_Examination_System.Models;

public class StudentAnswer
{
    public int Id { get; set; }

    public int StudentExamId { get; set; }
    public StudentExam StudentExam { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public int? SelectedChoiceId { get; set; }
    public Choice? SelectedChoice { get; set; }

    public bool IsCorrect { get; set; }
}
