

using System.ComponentModel.DataAnnotations;

public class AdminQuizAttemptsSummaryResponseDto
{

    public List<QuizAttemptsSummaryDto> QuizSummaries { get; set; } = new();
}

public class QuizAttemptsSummaryDto
{

    public string QuizId { get; set; } = string.Empty;

    public string QuizTitle { get; set; } = string.Empty;
    public int TotalAttempts { get; set; }
    public int AttemptsAbove50Percent { get; set; }
    public int AttemptsBelowOrEqual50Percent { get; set; }
}
