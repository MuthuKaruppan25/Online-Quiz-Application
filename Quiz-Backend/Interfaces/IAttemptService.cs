

using Quiz.Models;

public interface IAttemptService
{
    Task<QuizAttempt> AddAttempt(AttemptAddDto attemptAddDto);
    Task<PagedResult<Answers>> GetAnswersByAttempt(Guid attemptId, PageDataDto pageData);
    Task<QuizAttempt> GetAttemptById(Guid attemptId);
    Task<PagedResult<QuizAttempt>> GetAttemptsByQuizId(
    string quizId,
    PageDataDto pageData);
    Task<(bool Success, string Message)> CheckQuizAlreadyAttempted(Guid attenderId, string quizId);

}