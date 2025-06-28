

public class Question
{
    public Guid guid { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;

    public string OptionA { get; set; } = string.Empty;
    public string OptionB { get; set; } = string.Empty;
    public string OptionC { get; set; } = string.Empty;
    public string OptionD { get; set; } = string.Empty;

    public string CorrectOption { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public string QuizId { get; set; } = string.Empty;
    public QuizData? quiz { get; set; }
}