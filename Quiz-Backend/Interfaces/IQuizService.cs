
using Quiz.Models;

public interface IQuizService
{
    Task<QuizAddResponse> AddQuiz(QuizAddDto quizAddDto);
    Task<QuizData> GetQuizById(string quizId);
    Task<PagedResult<Question>> GetQuestionsByQuizId(string quizId, PageDataDto pageData);
    Task<PagedResult<QuizAttempt>> GetAttemptsByQuizId(string quizId, PageDataDto pageData);
    Task<PagedResult<QuizData>> GetAllQuizzes(PageDataDto pageData);

}