

public class AttemptAddDto
{
    public Guid AttemptorId { get; set; }
    public string QuizId { get; set; } = string.Empty;
    public int TimeTakenMins { get; set; }
    public bool AutoSubmitted { get; set; }
    public ICollection<AnswerAddDto>? answers { get; set; }
}