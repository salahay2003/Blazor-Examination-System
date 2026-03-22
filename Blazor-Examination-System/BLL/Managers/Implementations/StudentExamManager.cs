using Blazor_Examination_System.BLL.DTOs;
using Blazor_Examination_System.BLL.Managers.Interfaces;
using Blazor_Examination_System.DAL.Repositories.Interfaces;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Implementations;

public class StudentExamManager : IStudentExamManager
{
    private readonly IStudentExamRepository _studentExamRepository;
    private readonly IStudentAnswerRepository _studentAnswerRepository;
    private readonly IExamRepository _examRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IChoiceRepository _choiceRepository;

    public StudentExamManager(
        IStudentExamRepository studentExamRepository,
        IStudentAnswerRepository studentAnswerRepository,
        IExamRepository examRepository,
        IQuestionRepository questionRepository,
        IChoiceRepository choiceRepository)
    {
        _studentExamRepository = studentExamRepository;
        _studentAnswerRepository = studentAnswerRepository;
        _examRepository = examRepository;
        _questionRepository = questionRepository;
        _choiceRepository = choiceRepository;
    }

    public async Task<StudentExam> StartExamAsync(string studentId, int examId)
    {
        if (await _studentExamRepository.HasStudentTakenExamAsync(studentId, examId))
            throw new InvalidOperationException("You have already taken this exam");

        var exam = await _examRepository.GetByIdAsync(examId);
        if (exam == null || !exam.IsActive)
            throw new InvalidOperationException("This exam is not available");

        var studentExam = new StudentExam
        {
            StudentId = studentId,
            ExamId = examId,
            StartedAt = DateTime.UtcNow,
            IsCompleted = false
        };

        return await _studentExamRepository.AddAsync(studentExam);
    }

    public async Task<StudentAnswer> SubmitAnswerAsync(int studentExamId, int questionId, int choiceId)
    {
        // Get the choice directly from database to verify IsCorrect
        var allChoices = await _choiceRepository.GetByQuestionIdAsync(questionId);
        var selectedChoice = allChoices.FirstOrDefault(c => c.Id == choiceId);

        if (selectedChoice == null)
            throw new InvalidOperationException($"Choice {choiceId} not found for question {questionId}");

        bool isCorrect = selectedChoice.IsCorrect;

        Console.WriteLine($"📝 Answer: Q{questionId} → Choice {choiceId} (IsCorrect={isCorrect}, Text='{selectedChoice.Text}')");

        // Check if already answered
        var existingAnswer = await _studentAnswerRepository.GetByStudentExamAndQuestionAsync(studentExamId, questionId);

        if (existingAnswer != null)
        {
            existingAnswer.SelectedChoiceId = choiceId;
            existingAnswer.IsCorrect = isCorrect;
            await _studentAnswerRepository.UpdateAsync(existingAnswer);
            return existingAnswer;
        }
        else
        {
            var answer = new StudentAnswer
            {
                StudentExamId = studentExamId,
                QuestionId = questionId,
                SelectedChoiceId = choiceId,
                IsCorrect = isCorrect
            };
            return await _studentAnswerRepository.AddAsync(answer);
        }
    }

    public async Task<StudentExam> CompleteExamAsync(int studentExamId)
    {
        var studentExam = await _studentExamRepository.GetByIdAsync(studentExamId);
        if (studentExam == null)
            throw new InvalidOperationException("Student exam not found");

        if (studentExam.IsCompleted)
            throw new InvalidOperationException("Exam already completed");

        // Get exam with ALL questions and choices
        var exam = await _examRepository.GetByIdWithDetailsAsync(studentExam.ExamId);
        if (exam == null)
            throw new InvalidOperationException("Exam not found");

        // Get student's answers
        var answers = await _studentAnswerRepository.GetByStudentExamIdAsync(studentExamId);

        // ★★★ RECALCULATE TotalMarks from actual questions (not stored value) ★★★
        int actualTotalMarks = exam.Questions.Sum(q => q.Marks);

        // ★★★ Fix PassingMarks if needed ★★★
        int actualPassingMarks = exam.PassingMarks;
        if (actualPassingMarks > actualTotalMarks && actualTotalMarks > 0)
        {
            actualPassingMarks = Math.Max(1, (int)Math.Ceiling(actualTotalMarks * 0.6));

            // Also fix it in the database
            exam.TotalMarks = actualTotalMarks;
            exam.PassingMarks = actualPassingMarks;
            await _examRepository.UpdateAsync(exam);

            Console.WriteLine($"⚠️ AUTO-FIXED: Exam '{exam.Title}'");
            Console.WriteLine($"   TotalMarks: {actualTotalMarks}");
            Console.WriteLine($"   PassingMarks: {actualPassingMarks}");
        }

        Console.WriteLine($"");
        Console.WriteLine($"========================================");
        Console.WriteLine($"📊 SCORING EXAM: {exam.Title}");
        Console.WriteLine($"   StudentExamId: {studentExamId}");
        Console.WriteLine($"   Questions: {exam.Questions.Count}");
        Console.WriteLine($"   Actual TotalMarks: {actualTotalMarks}");
        Console.WriteLine($"   Actual PassingMarks: {actualPassingMarks}");
        Console.WriteLine($"   Student Answers: {answers.Count}");
        Console.WriteLine($"========================================");

        // ★★★ CALCULATE SCORE from source data ★★★
        int totalScore = 0;

        foreach (var question in exam.Questions.OrderBy(q => q.OrderIndex))
        {
            var correctChoice = question.Choices.FirstOrDefault(c => c.IsCorrect);
            var studentAnswer = answers.FirstOrDefault(a => a.QuestionId == question.Id);

            bool isCorrect = false;

            if (studentAnswer != null && correctChoice != null && studentAnswer.SelectedChoiceId.HasValue)
            {
                isCorrect = studentAnswer.SelectedChoiceId.Value == correctChoice.Id;

                // Fix IsCorrect in answer record if needed
                if (studentAnswer.IsCorrect != isCorrect)
                {
                    studentAnswer.IsCorrect = isCorrect;
                    await _studentAnswerRepository.UpdateAsync(studentAnswer);
                }
            }

            if (isCorrect)
            {
                totalScore += question.Marks;
            }

            Console.WriteLine($"   Q{question.OrderIndex + 1}: {(isCorrect ? "✅" : "❌")} '{question.Text}'");
            Console.WriteLine($"      Correct: Id={correctChoice?.Id} '{correctChoice?.Text}'");
            Console.WriteLine($"      Student: Id={studentAnswer?.SelectedChoiceId} → +{(isCorrect ? question.Marks : 0)} marks");
        }

        // ★★★ SET RESULTS ★★★
        studentExam.Score = totalScore;
        studentExam.IsPassed = totalScore >= actualPassingMarks;
        studentExam.IsCompleted = true;
        studentExam.CompletedAt = DateTime.UtcNow;

        await _studentExamRepository.UpdateAsync(studentExam);

        Console.WriteLine($"========================================");
        Console.WriteLine($"   ✅ FINAL: Score={totalScore}/{actualTotalMarks}");
        Console.WriteLine($"   ✅ PASSED: {totalScore} >= {actualPassingMarks} = {studentExam.IsPassed}");
        Console.WriteLine($"========================================");
        Console.WriteLine($"");

        return studentExam;
    }

    public async Task<List<StudentExam>> GetStudentExamsAsync(string studentId)
    {
        var studentExams = await _studentExamRepository.GetByStudentIdAsync(studentId);

        // Ensure IsPassed is correctly calculated based on Score and PassingMarks
        foreach (var exam in studentExams.Where(se => se.IsCompleted))
        {
            if (exam.Exam?.PassingMarks > 0)
            {
                bool correctStatus = exam.Score >= exam.Exam.PassingMarks;
                if (exam.IsPassed != correctStatus)
                {
                    Console.WriteLine($"⚠️ Status Correction: StudentExamId={exam.Id}, OldStatus={exam.IsPassed}, NewStatus={correctStatus}");
                    exam.IsPassed = correctStatus;
                }
            }
        }

        return studentExams;
    }

    public async Task<ExamResultDto?> GetExamResultAsync(int studentExamId)
    {
        var studentExam = await _studentExamRepository.GetByIdWithDetailsAsync(studentExamId);
        if (studentExam == null)
            return null;

        var exam = await _examRepository.GetByIdWithDetailsAsync(studentExam.ExamId);
        if (exam == null)
            return null;

        var answers = await _studentAnswerRepository.GetByStudentExamIdAsync(studentExamId);

        var dto = new ExamResultDto
        {
            StudentExamId = studentExamId,
            ExamTitle = exam.Title,
            SubjectName = exam.Subject?.Name ?? "N/A",
            Score = studentExam.Score,
            TotalMarks = exam.TotalMarks,
            IsPassed = studentExam.IsPassed,
            StartedAt = studentExam.StartedAt,
            CompletedAt = studentExam.CompletedAt,
            Questions = new List<QuestionResultDto>()
        };

        foreach (var question in exam.Questions.OrderBy(q => q.OrderIndex))
        {
            var studentAnswer = answers.FirstOrDefault(a => a.QuestionId == question.Id);
            var correctChoice = question.Choices.FirstOrDefault(c => c.IsCorrect);

            // Recalculate IsCorrect from source
            bool isCorrect = studentAnswer?.SelectedChoiceId != null
                             && correctChoice != null
                             && studentAnswer.SelectedChoiceId == correctChoice.Id;

            var questionDto = new QuestionResultDto
            {
                QuestionText = question.Text,
                Marks = question.Marks,
                IsCorrect = isCorrect,
                SelectedChoiceId = studentAnswer?.SelectedChoiceId,
                Choices = question.Choices.OrderBy(c => c.OrderIndex).Select(c => new ChoiceResultDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    IsCorrect = c.IsCorrect,
                    IsSelected = studentAnswer?.SelectedChoiceId == c.Id
                }).ToList()
            };

            dto.Questions.Add(questionDto);
        }

        return dto;
    }

    public async Task<bool> HasStudentTakenExamAsync(string studentId, int examId)
    {
        return await _studentExamRepository.HasStudentTakenExamAsync(studentId, examId);
    }

    public async Task<List<StudentExam>> GetAllResultsAsync()
    {
        var results = await _studentExamRepository.GetCompletedExamsAsync();

        // Ensure IsPassed is correctly calculated based on Score and PassingMarks
        foreach (var exam in results)
        {
            if (exam.Exam?.PassingMarks > 0)
            {
                bool correctStatus = exam.Score >= exam.Exam.PassingMarks;
                if (exam.IsPassed != correctStatus)
                {
                    Console.WriteLine($"⚠️ Status Correction: StudentExamId={exam.Id}, Student={exam.Student?.Email}, OldStatus={exam.IsPassed}, NewStatus={correctStatus}, Score={exam.Score}/{exam.Exam.TotalMarks}");
                    exam.IsPassed = correctStatus;
                }
            }
        }

        return results;
    }

    public async Task<StudentExam?> GetActiveExamAsync(string studentId, int examId)
    {
        return await _studentExamRepository.GetActiveExamAsync(studentId, examId);
    }
}
