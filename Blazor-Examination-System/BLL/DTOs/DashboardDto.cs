namespace Blazor_Examination_System.BLL.DTOs;

public class DashboardDto
{
    public int TotalSubjects { get; set; }
    public int TotalExams { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalStudents { get; set; }
    public int TotalExamsTaken { get; set; }
    public double AverageScorePercentage { get; set; }
    public List<SubjectExamCount> ExamsPerSubject { get; set; } = new();
    public List<RecentResult> RecentResults { get; set; } = new();
}

public class SubjectExamCount
{
    public string SubjectName { get; set; } = string.Empty;
    public int ExamCount { get; set; }
}

public class RecentResult
{
    public string StudentName { get; set; } = string.Empty;
    public string ExamTitle { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public bool IsPassed { get; set; }
    public DateTime CompletedAt { get; set; }
}
