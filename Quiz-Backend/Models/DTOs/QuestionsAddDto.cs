

using System.ComponentModel.DataAnnotations;

public class QuestionsAddDto
{
    [TextValidator]
    public string Text { get; set; } = string.Empty;

    [TextValidator]
    public string OptionA { get; set; } = string.Empty;
    [TextValidator]
    public string OptionB { get; set; } = string.Empty;
    [TextValidator]
    public string OptionC { get; set; } = string.Empty;
    [TextValidator]
    public string OptionD { get; set; } = string.Empty;

    [Required]
    public string CorrectOption { get; set; } = string.Empty;

    [RequirementsValidator]
    public string Explanation { get; set; } = string.Empty;
}