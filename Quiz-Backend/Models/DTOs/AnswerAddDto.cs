

using System.ComponentModel.DataAnnotations;

public class AnswerAddDto
{
    [Required]
    public Guid QuestionId { get; set; }
    [Required]
    public string SelectedOption { get; set; } = string.Empty;
    
}