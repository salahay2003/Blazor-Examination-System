using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.Components.Dialogs;

public class QuestionDialogResultData
{
    public Question Question { get; set; } = null!;
    public List<Choice> Choices { get; set; } = new();
}
