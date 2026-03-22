namespace Blazor_Examination_System.BLL.DTOs;

public class ExamResultDto
{
    public int StudentExamId { get; set; }
    public string ExamTitle { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public bool IsPassed { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public double Percentage => TotalMarks > 0 ? Math.Round((double)Score / TotalMarks * 100, 1) : 0;
    public List<QuestionResultDto> Questions { get; set; } = new();
}

public class QuestionResultDto
{
    public string QuestionText { get; set; } = string.Empty;
    public int Marks { get; set; }
    public bool IsCorrect { get; set; }
    public int? SelectedChoiceId { get; set; }
    public List<ChoiceResultDto> Choices { get; set; } = new();
}

public class ChoiceResultDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public bool IsSelected { get; set; }
}
