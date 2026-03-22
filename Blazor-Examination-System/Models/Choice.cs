using System.ComponentModel.DataAnnotations;

namespace Blazor_Examination_System.Models;

public class Choice
{
    public int Id { get; set; }

    [Required, MaxLength(1000)]
    public string Text { get; set; } = string.Empty;

    public bool IsCorrect { get; set; } = false;
    public int OrderIndex { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
