

using Quiz.Models;

public class QuizData
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int durationMinutes { get; set; }
    public Guid adminId{ get; set; }
    public Admin? admin{ get; set; }

    public Guid categoryId { get; set; }
    public Category? category { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<Question>? questions { get; set; }
    public ICollection<QuizAttempt>? quizAttempts { get; set; }
}