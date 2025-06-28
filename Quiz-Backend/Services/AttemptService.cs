

using JobPortal.Exceptions;
using Quiz.Contexts;
using Quiz.Models;

public class AttemptService : IAttemptService
{
    private readonly IRepository<Guid, Question> _questionRepository;
    private readonly IRepository<Guid, QuizAttempt> _quizAttemptRepository;
    private readonly IRepository<Guid, Answers> _answerRepository;
    AttemptMapper attemptMapper;
    AnswerMapper answerMapper;

    public AttemptService(
        IRepository<Guid, Question> questionRepository,
        IRepository<Guid, QuizAttempt> quizAttemptRepository,
        IRepository<Guid, Answers> answerRepository
    )
    {
        _questionRepository = questionRepository;
        _quizAttemptRepository = quizAttemptRepository;
        _answerRepository = answerRepository;
        attemptMapper = new AttemptMapper();
        answerMapper = new AnswerMapper();
    }
    public async Task<QuizAttempt> AddAttempt(AttemptAddDto attemptAddDto)
    {
        try
        {


            var attempt = attemptMapper.MaptoAttempt(attemptAddDto);


            await _quizAttemptRepository.Add(attempt);

            int score = 0;



            if (attemptAddDto.answers != null && attemptAddDto.answers.Count > 0)
            {
                foreach (var answerDto in attemptAddDto.answers)
                {

                    var question = await _questionRepository.Get(answerDto.QuestionId);

                    if (question == null)
                    {
                        throw new Exception($"Question with id {answerDto.QuestionId} not found.");
                    }

                    bool isCorrect = question.CorrectOption.Equals(answerDto.SelectedOption, StringComparison.OrdinalIgnoreCase);

                    if (isCorrect) score++;


                    var answer = answerMapper.MaptoAnswer(answerDto, isCorrect, attempt.guid);
                    await _answerRepository.Add(answer);
                }
            }


            attempt.Score = score;
            await _quizAttemptRepository.Update(attempt.guid, attempt);

            return attempt;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding attempt: {ex.Message}");
        }
    }

    public async Task<QuizAttempt> GetAttemptById(Guid attemptId)
    {
        try
        {
            var attempt = await _quizAttemptRepository.Get(attemptId);
            return attempt;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting attempt by id: {ex.Message}");
        }
    }

    public async Task<PagedResult<Answers>> GetAnswersByAttempt(Guid attemptId, PageDataDto pageData)
    {
        try
        {
            var attempt = await _quizAttemptRepository.Get(attemptId);

            if (attempt.answers == null || !attempt.answers.Any())
            {
                throw new RecordNotFoundException("No answers found for this attempt");
            }

            var totalCount = attempt.answers.Count;

            var pagedData = attempt.answers
                .OrderBy(a => a.question?.Text) // you can order however you prefer
                .Skip((pageData.pageNumber - 1) * pageData.pageSize)
                .Take(pageData.pageSize)
                .ToList();

            return new PagedResult<Answers>
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
            throw new Exception($"Error fetching answers for attempt: {ex.Message}");
        }
    }
    public async Task<PagedResult<QuizAttempt>> GetAttemptsByQuizId(
    string quizId,
    PageDataDto pageData)
    {
        var all = await _quizAttemptRepository.GetAll();
        var filtered = all.Where(a => a.QuizId == quizId).OrderByDescending(a => a.AttemptedAt).ToList();

        if (!filtered.Any())
            throw new RecordNotFoundException("No attempts found for this quiz");

        var totalCount = filtered.Count;
        var pageItems = filtered
            .Skip((pageData.pageNumber - 1) * pageData.pageSize)
            .Take(pageData.pageSize)
            .ToList();

        return new PagedResult<QuizAttempt>
        {
            TotalCount = totalCount,
            Data = pageItems,
            PageNumber = pageData.pageNumber,
            PageSize = pageData.pageSize
        };
    }

    public async Task<(bool Success, string Message)> CheckQuizAlreadyAttempted(Guid attenderId, string quizId)
    {
        try
        {
            var allAttempts = await _quizAttemptRepository.GetAll();

            var exists = allAttempts.Any(a => a.AttemptorId == attenderId && a.QuizId == quizId);

            if (exists)
            {
                return (true, "Attempt already exists for this user and quiz.");
            }
            else
            {
                return (false, "No attempt found for this user and quiz.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error checking if quiz already attempted: {ex.Message}");
        }
    }



}