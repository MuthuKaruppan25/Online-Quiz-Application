namespace Quiz.Models;

public class QuizAttempt
{
    public Guid guid { get; set; } = Guid.NewGuid();
    public Guid AttemptorId { get; set; }
    public Attender? attender { get; set; }
    public string QuizId { get; set; } = string.Empty;
    public int TimeTakenMins { get; set; }
    public DateTime AttemptedAt { get; set; }
    public bool AutoSubmitted { get; set; } = false;
    public QuizData? quizData { get; set; }
    public int Score { get; set; }
    public ICollection<Answers>? answers { get; set; }

}