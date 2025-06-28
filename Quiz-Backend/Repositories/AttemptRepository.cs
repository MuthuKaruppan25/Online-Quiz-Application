using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;
using Quiz.Models;

namespace Quiz.Repositories;

public class QuizAttemptRepository : Repository<Guid, QuizAttempt>
{
    public QuizAttemptRepository(QuizContext context) : base(context) { }

    public override async Task<QuizAttempt> Get(Guid key)
    {
        try
        {
            var attempt = await _quizContext.QuizAttempts
                .Include(a => a.answers)
                .Include(a => a.quizData!).ThenInclude(q => q.questions)
                .FirstOrDefaultAsync(a => a.guid == key);

            if (attempt is null)
                throw new RecordNotFoundException("Quiz Attempt Not Found");

            return attempt;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Quiz Attempt: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<QuizAttempt>> GetAll()
    {
        try
        {
            return await _quizContext.QuizAttempts
                .Include(a => a.answers)
                .Include(a => a.quizData!).ThenInclude(q=>q.questions)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Quiz Attempts: {ex.Message}");
        }
    }
}
