using Blazor_Examination_System.BLL.Managers.Interfaces;
using Blazor_Examination_System.DAL.Repositories.Interfaces;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.BLL.Managers.Implementations;

public class QuestionManager : IQuestionManager
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IChoiceRepository _choiceRepository;
    private readonly IExamRepository _examRepository;

    public QuestionManager(IQuestionRepository questionRepository, IChoiceRepository choiceRepository, IExamRepository examRepository)
    {
        _questionRepository = questionRepository;
        _choiceRepository = choiceRepository;
        _examRepository = examRepository;
    }

    public async Task<List<Question>> GetByExamIdAsync(int examId)
    {
        return await _questionRepository.GetByExamIdWithChoicesAsync(examId);
    }

    public async Task<Question?> GetByIdWithChoicesAsync(int id)
    {
        return await _questionRepository.GetByIdWithChoicesAsync(id);
    }

    public async Task<Question> CreateWithChoicesAsync(Question question, List<Choice> choices)
    {
        if (string.IsNullOrWhiteSpace(question.Text))
            throw new ArgumentException("Question text is required");

        if (choices.Count < 2)
            throw new ArgumentException("At least 2 choices are required");

        var correctCount = choices.Count(c => c.IsCorrect);
        if (correctCount != 1)
            throw new ArgumentException("Exactly one choice must be marked as correct");

        var maxOrderIndex = await _questionRepository.GetMaxOrderIndexByExamIdAsync(question.ExamId);
        question.OrderIndex = maxOrderIndex + 1;

        var savedQuestion = await _questionRepository.AddAsync(question);

        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].QuestionId = savedQuestion.Id;
            choices[i].OrderIndex = i;
        }

        await _choiceRepository.AddRangeAsync(choices);

        await RecalculateExamTotalMarksAsync(question.ExamId);

        return savedQuestion;
    }

    public async Task<bool> UpdateWithChoicesAsync(int questionId, Question updatedQuestion, List<Choice> newChoices)
    {
        var existing = await _questionRepository.GetByIdAsync(questionId);
        if (existing == null)
            return false;

        if (string.IsNullOrWhiteSpace(updatedQuestion.Text))
            throw new ArgumentException("Question text is required");

        if (newChoices.Count < 2)
            throw new ArgumentException("At least 2 choices are required");

        var correctCount = newChoices.Count(c => c.IsCorrect);
        if (correctCount != 1)
            throw new ArgumentException("Exactly one choice must be marked as correct");

        await _choiceRepository.DeleteByQuestionIdAsync(questionId);

        existing.Text = updatedQuestion.Text;
        existing.Marks = updatedQuestion.Marks;

        await _questionRepository.UpdateAsync(existing);

        for (int i = 0; i < newChoices.Count; i++)
        {
            newChoices[i].QuestionId = questionId;
            newChoices[i].OrderIndex = i;
        }

        await _choiceRepository.AddRangeAsync(newChoices);

        await RecalculateExamTotalMarksAsync(existing.ExamId);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _questionRepository.GetByIdAsync(id);
        if (existing == null)
            return false;

        int examId = existing.ExamId;
        await _questionRepository.DeleteAsync(existing);

        await RecalculateExamTotalMarksAsync(examId);

        return true;
    }

        private async Task RecalculateExamTotalMarksAsync(int examId)
        {
            var exam = await _examRepository.GetByIdAsync(examId);
            if (exam == null)
                return;

            var questions = await _questionRepository.GetByExamIdWithChoicesAsync(examId);
            int newTotalMarks = questions.Sum(q => q.Marks);

            // Update TotalMarks
            exam.TotalMarks = newTotalMarks;

            // ★★★ AUTO-FIX PassingMarks if it exceeds TotalMarks ★★★
            if (exam.PassingMarks > newTotalMarks && newTotalMarks > 0)
            {
                // Set passing to 60% of total, minimum 1
                exam.PassingMarks = Math.Max(1, (int)Math.Ceiling(newTotalMarks * 0.6));
                Console.WriteLine($"📊 Auto-fixed PassingMarks: {exam.Title}");
                Console.WriteLine($"   TotalMarks: {newTotalMarks}, PassingMarks: {exam.PassingMarks}");
            }

            // Handle edge case: no questions
            if (newTotalMarks == 0)
            {
                exam.PassingMarks = 0;
            }

            await _examRepository.UpdateAsync(exam);
        }
    }
