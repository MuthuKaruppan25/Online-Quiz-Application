using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;

namespace Quiz.Repositories;

public class QuizDataRepository : Repository<string, QuizData>
{
    public QuizDataRepository(QuizContext context) : base(context) { }

    public override async Task<QuizData> Get(string key)
    {
        try
        {
            var quiz = await _quizContext.Quizzes
                .Include(q => q.questions)
                .Include(q => q.quizAttempts)
                .FirstOrDefaultAsync(q => q.Id == key);

            if (quiz is null)
                throw new RecordNotFoundException("Quiz Not Found");

            return quiz;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Quiz: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<QuizData>> GetAll()
    {
        try
        {
            return await _quizContext.Quizzes
                .Include(q => q.questions)
                .Include(q => q.quizAttempts)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Quizzes: {ex.Message}");
        }
    }
    
}
