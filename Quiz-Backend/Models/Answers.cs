namespace Quiz.Models;

public class Answers
{
    public Guid guid { get; set; } = Guid.NewGuid();
    public Guid QuestionId { get; set; }
    public Question? question { get; set; }
    public string SelectedOption { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public Guid quizAttemptId { get; set; }
    public QuizAttempt? attempt{ get; set; }

}