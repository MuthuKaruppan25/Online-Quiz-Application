public class AttenderIndividualAttemptSummaryDto
{
    public string QuizId { get; set; } = string.Empty;
    public string QuizTitle { get; set; } = string.Empty;
    public int Score { get; set; }
    public bool IsAbove50Percent { get; set; }
    public DateTime AttemptedAt { get; set; }
}

public class AttenderIndividualAttemptsResponseDto
{
    public List<AttenderIndividualAttemptSummaryDto> Attempts { get; set; } = new();
}
