using Quiz.Models;

public class AttemptMapper
{
    public QuizAttempt MaptoAttempt(AttemptAddDto attemptAddDto)
    {
        var attempt = new QuizAttempt
        {
            guid = Guid.NewGuid(),
            AttemptorId = attemptAddDto.AttemptorId,
            QuizId = attemptAddDto.QuizId,
            TimeTakenMins = attemptAddDto.TimeTakenMins,
            AutoSubmitted = attemptAddDto.AutoSubmitted,
            Score = 0,
            AttemptedAt = DateTime.UtcNow
        };

        return attempt;
    }
}
