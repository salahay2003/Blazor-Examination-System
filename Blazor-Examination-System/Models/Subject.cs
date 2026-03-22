using System.ComponentModel.DataAnnotations;

namespace Blazor_Examination_System.Models;

public class Subject
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
