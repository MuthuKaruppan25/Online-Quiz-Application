using JobPortal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contexts;
using Quiz.Models;

namespace Quiz.Repositories;

public class AdminRepository : Repository<Guid, Admin>
{
    public AdminRepository(QuizContext context) : base(context) { }

    public override async Task<Admin> Get(Guid key)
    {
        try
        {
            var admin = await _quizContext.Admins
                .Include(a => a.quizDatas!)
                    .ThenInclude(q => q.questions)
                .Include(q => q.quizDatas!)
                    .ThenInclude(a => a.quizAttempts)
                .FirstOrDefaultAsync(a => a.guid == key);

            if (admin is null)
                throw new RecordNotFoundException("Admin Not Found");

            return admin;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Admin: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<Admin>> GetAll()
    {
        try
        {
            return await _quizContext.Admins
                .Include(a => a.quizDatas!)
                    .ThenInclude(q => q.questions)
                .Include(q => q.quizDatas!)
                    .ThenInclude(a => a.quizAttempts)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching Admins: {ex.Message}");
        }
    }
}
