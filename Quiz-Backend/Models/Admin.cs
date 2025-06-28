namespace Quiz.Models;

public class Admin
{
    public Guid guid { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User? user { get; set; }
    public ICollection<QuizData>? quizDatas { get; set; }
}