

public class QuizAddDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int durationMinutes { get; set; }
    public Guid adminId { get; set; }
    public Guid categoryId { get; set; }
    public ICollection<QuestionsAddDto>? questions { get; set; }

}