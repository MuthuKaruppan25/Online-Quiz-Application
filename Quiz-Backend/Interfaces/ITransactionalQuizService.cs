public interface ITransactionalQuizService
{
    Task<QuizAddResponse> AddQuiz(QuizAddDto quizAddDto);
}