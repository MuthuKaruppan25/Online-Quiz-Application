

public class QuizAddResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int durationMinutes { get; set; }
    public Guid adminId { get; set; }
    public Guid categoryId { get; set; }
}