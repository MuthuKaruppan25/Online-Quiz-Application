using Microsoft.EntityFrameworkCore.Storage;
using Quiz.Contexts;


public class TransactionalQuizService : ITransactionalQuizService
{
    private readonly IRepository<string, QuizData> _quizRepository;
    private readonly IRepository<Guid, Question> _questionRepository;
    private readonly QuizContext _context;
    private readonly QuizMapper _quizMapper;
    private readonly QuestionsMapper _questionsMapper;
    private readonly QuestionResMapper _questionResMapper;

    public TransactionalQuizService(
        IRepository<string, QuizData> quizRepository,
        IRepository<Guid, Question> questionRepository,
        QuizContext context
    )
    {
        _quizRepository = quizRepository;
        _questionRepository = questionRepository;
        _context = context;

        _quizMapper = new QuizMapper();
        _questionsMapper = new QuestionsMapper();
        _questionResMapper = new QuestionResMapper();
    }

    public async Task<QuizAddResponse> AddQuiz(QuizAddDto quizAddDto)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var quizData = _quizMapper.MapToQuiz(quizAddDto);

          
            var savedQuiz = await _quizRepository.Add(quizData);

           
            if (quizAddDto.questions != null && quizAddDto.questions.Any())
            {
                foreach (var questionDto in quizAddDto.questions)
                {
                    var question = _questionsMapper.MaptoQuestion(questionDto, savedQuiz.Id);
                    await _questionRepository.Add(question);
                }
            }

       
            await transaction.CommitAsync();

          
            var response = _questionResMapper.MapToResponse(savedQuiz);
            return response;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while adding quiz: {ex.Message}");
        }
    }
}
