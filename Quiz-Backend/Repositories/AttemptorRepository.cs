using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;
using Quiz.Models;

namespace Quiz.Repositories;

public class AttenderRepository : Repository<Guid, Attender>
{
    public AttenderRepository(QuizContext context) : base(context) { }

    public override async Task<Attender> Get(Guid key)
    {
        try
        {
            var attender = await _quizContext.Attenders
                .Include(a => a.quizAttempts!)
                    .ThenInclude(a => a.answers)
                .Include(q =>q.quizAttempts!)
                    .ThenInclude(q =>q.quizData!).ThenInclude(q => q.questions)
                .FirstOrDefaultAsync(a => a.guid == key);

            if (attender is null)
                throw new RecordNotFoundException("Attender Not Found");

            return attender;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Attender: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<Attender>> GetAll()
    {
        try
        {
            return await _quizContext.Attenders
                            .Include(a => a.quizAttempts!)
                                .ThenInclude(a => a.answers)
                            .Include(q =>q.quizAttempts!)
                                .ThenInclude(q =>q.quizData!).ThenInclude(q => q.questions)
                            .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Attenders: {ex.Message}");
        }
    }
}
