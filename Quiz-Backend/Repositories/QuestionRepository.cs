using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;

namespace Quiz.Repositories;

public class QuestionRepository : Repository<Guid, Question>
{
    public QuestionRepository(QuizContext context) : base(context) { }

    public override async Task<Question> Get(Guid key)
    {
        try
        {
            var question = await _quizContext.Questions
                .FirstOrDefaultAsync(q => q.guid == key);

            if (question is null)
                throw new RecordNotFoundException("Question Not Found");

            return question;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Question: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<Question>> GetAll()
    {
        try
        {
            return await _quizContext.Questions.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Questions: {ex.Message}");
        }
    }
}
