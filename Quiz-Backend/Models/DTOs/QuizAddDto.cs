

using System.ComponentModel.DataAnnotations;

public class QuizAddDto
{
    [RequirementsValidator]
    public string Title { get; set; } = string.Empty;
    [RequirementsValidator]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int durationMinutes { get; set; }
    [Required]
    public Guid adminId { get; set; }
    [Required]
    public Guid categoryId { get; set; }
    [Required]
    public ICollection<QuestionsAddDto>? questions { get; set; }

}