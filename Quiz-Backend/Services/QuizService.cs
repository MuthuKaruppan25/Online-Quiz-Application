


using JobPortal.Exceptions;
using Quiz.Contexts;
using Quiz.Models;

public class QuizService : IQuizService
{
    private readonly IRepository<string, QuizData> _quizRepository;
    private readonly IRepository<Guid, Question> _questionRepository;
    QuestionsMapper questionsMapper;
    QuizMapper quizMapper;
    QuestionResMapper questionResMapper;
    public QuizService(
        IRepository<string, QuizData> quizRepository,
        IRepository<Guid, Question> questionRepository
    )
    {
        _quizRepository = quizRepository;
        _questionRepository = questionRepository;
        questionsMapper = new QuestionsMapper();
        quizMapper = new QuizMapper();
        questionResMapper = new QuestionResMapper();

    }
    public async Task<QuizAddResponse> AddQuiz(QuizAddDto quizAddDto)
    {
        try
        {

            var quizData = quizMapper.MapToQuiz(quizAddDto);


            var savedQuiz = await _quizRepository.Add(quizData);


            if (quizAddDto.questions != null && quizAddDto.questions.Any())
            {
                foreach (var questionDto in quizAddDto.questions)
                {
                    var question = questionsMapper.MaptoQuestion(questionDto, savedQuiz.Id);
                    await _questionRepository.Add(question);
                }
            }


            var response = questionResMapper.MapToResponse(savedQuiz);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while adding quiz: {ex.Message}");
        }
    }
    public async Task<QuizData> GetQuizById(string quizId)
    {
        try
        {
            var quiz = await _quizRepository.Get(quizId);
            return quiz;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting quiz: {ex.Message}");
        }
    }
    public async Task<PagedResult<Question>> GetQuestionsByQuizId(string quizId, PageDataDto pageData)
    {
        try
        {
            var quiz = await _quizRepository.Get(quizId);

            if (quiz.questions == null || !quiz.questions.Any())
            {
                throw new RecordNotFoundException("No questions found for this quiz");
            }

            var totalCount = quiz.questions.Count;

            var data = quiz.questions
                           .Skip((pageData.pageNumber - 1) * pageData.pageSize)
                           .Take(pageData.pageSize)
                           .ToList();

            return new PagedResult<Question>
            {
                TotalCount = totalCount,
                Data = data,
                PageNumber = pageData.pageNumber,
                PageSize = pageData.pageSize
            };
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting questions: {ex.Message}");
        }
    }

    public async Task<PagedResult<QuizAttempt>> GetAttemptsByQuizId(string quizId, PageDataDto pageData)
    {
        try
        {
            var quiz = await _quizRepository.Get(quizId);

            if (quiz.quizAttempts == null || !quiz.quizAttempts.Any())
            {
                throw new RecordNotFoundException("No attempts found for this quiz");
            }

            var totalCount = quiz.quizAttempts.Count;

            var data = quiz.quizAttempts
                           .OrderByDescending(a => a.AttemptedAt)
                           .Skip((pageData.pageNumber - 1) * pageData.pageSize)
                           .Take(pageData.pageSize)
                           .ToList();

            return new PagedResult<QuizAttempt>
            {
                TotalCount = totalCount,
                Data = data,
                PageNumber = pageData.pageNumber,
                PageSize = pageData.pageSize
            };
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting attempts: {ex.Message}");
        }
    }

    public async Task<PagedResult<QuizData>> GetAllQuizzes(PageDataDto pageData)
    {
        try
        {
            var allQuizzes = await _quizRepository.GetAll();

            if (allQuizzes == null || !allQuizzes.Any())
            {
                throw new RecordNotFoundException("No quizzes found.");
            }

            var totalCount = allQuizzes.Count();

            var pagedData = allQuizzes
                .OrderByDescending(q => q.CreatedAt)
                .Skip((pageData.pageNumber - 1) * pageData.pageSize)
                .Take(pageData.pageSize)
                .ToList();

            return new PagedResult<QuizData>
            {
                TotalCount = totalCount,
                Data = pagedData,
                PageNumber = pageData.pageNumber,
                PageSize = pageData.pageSize
            };
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching quizzes: {ex.Message}");
        }
    }





}