using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;
using Quiz.Models;

namespace Quiz.Repositories;

public class UserRepository : Repository<Guid, User>
{
    public UserRepository(QuizContext context) : base(context)
    {
        
    }
    public override async Task<User> Get(Guid key)
    {
        try
        {
            var user = await _quizContext.Users.FirstOrDefaultAsync(u => u.guid == key);
            if (user is null)
            {
                throw new RecordNotFoundException("User Not Found");
            }
            return user;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching User {ex.Message}");
        }
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        try
        {
            return await _quizContext.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Users {ex.Message}");
        }
    }
}