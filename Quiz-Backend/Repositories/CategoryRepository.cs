using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;
using Quiz.Models;

namespace Quiz.Repositories;

public class CategoryRepository : Repository<Guid, Category>
{
    public CategoryRepository(QuizContext context) : base(context) { }

    public override async Task<Category> Get(Guid key)
    {
        try
        {
            var cat = await _quizContext.Categories
                .Include(c => c.quizDatas)
                .FirstOrDefaultAsync(c => c.guid == key);

            if (cat is null)
                throw new RecordNotFoundException("Category Not Found");

            return cat;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Category: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<Category>> GetAll()
    {
        try
        {
            return await _quizContext.Categories
                .Include(c => c.quizDatas)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Categories: {ex.Message}");
        }
    }
}
