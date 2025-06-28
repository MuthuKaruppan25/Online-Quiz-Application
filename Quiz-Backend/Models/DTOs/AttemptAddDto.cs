

using System.ComponentModel.DataAnnotations;

public class AttemptAddDto
{
    [Required]
    public Guid AttemptorId { get; set; }
    [Required]
    public string QuizId { get; set; } = string.Empty;
    [Required]
    public int TimeTakenMins { get; set; }
    public bool AutoSubmitted { get; set; }
    [Required]
    public ICollection<AnswerAddDto>? answers { get; set; }
}