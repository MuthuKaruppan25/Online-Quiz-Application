using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;
using Quiz.Models;

namespace Quiz.Repositories;

public class AnswersRepository : Repository<Guid, Answers>
{
    public AnswersRepository(QuizContext context) : base(context) { }

    public override async Task<Answers> Get(Guid key)
    {
        try
        {
            var ans = await _quizContext.Answers
                .Include(a => a.question)
                .Include(a => a.attempt)
                .FirstOrDefaultAsync(a => a.guid == key);

            if (ans is null)
                throw new RecordNotFoundException("Answer Not Found");

            return ans;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Answer: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<Answers>> GetAll()
    {
        try
        {
            return await _quizContext.Answers
                .Include(a => a.question)
                .Include(a => a.attempt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Answers: {ex.Message}");
        }
    }
}
