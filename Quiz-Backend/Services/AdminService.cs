using JobPortal.Exceptions;
using Quiz.Contexts;
using Quiz.Exceptions;
using Quiz.Models;

public class AdminService : IAdminService
{
    private readonly IRepository<Guid, User> _userRepository;
    private readonly IRepository<Guid, Admin> _adminRepository;

    private readonly UserMapper userMapper;
    private readonly AdminMapper adminMapper;

    public AdminService(
        IRepository<Guid, User> userRepository,
        IRepository<Guid, Admin> adminRepository
    )
    {
        _userRepository = userRepository;
        _adminRepository = adminRepository;
        userMapper = new UserMapper();
        adminMapper = new AdminMapper();
    }
    public async Task<Admin> RegisterAdmin(UserRegisterDto userRegisterDto)
    {
        try
        {
            var existingUsers = await _userRepository.GetAll();
            var existingUser = existingUsers.FirstOrDefault(u => u.Username == userRegisterDto.Username);

            if (existingUser != null)
            {

                var admins = await _adminRepository.GetAll();
                var existingAdmin = admins.FirstOrDefault(a => a.UserId == existingUser.guid);

                if (existingAdmin != null)
                {

                    return existingAdmin;
                }
                else
                {

                    var newAdmin = adminMapper.MaptoAdmin(userRegisterDto);
                    newAdmin.UserId = existingUser.guid;
                    await _adminRepository.Add(newAdmin);
                    return newAdmin;
                }
            }


            var user = userMapper.MaptoUser(userRegisterDto);
            await _userRepository.Add(user);

            var admin = adminMapper.MaptoAdmin(userRegisterDto);
            admin.UserId = user.guid;
            await _adminRepository.Add(admin);

            return admin;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Registration cannot be performed: {ex.Message}");
        }
    }
    public async Task<Admin> GetAdminById(Guid adminId)
    {
        try
        {
            var admin = await _adminRepository.Get(adminId);
            return admin;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching admin by id: {ex.Message}");
        }
    }

    public async Task<Admin> GetAdminByUsername(string username)
    {
        try
        {
            // first get user by username
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new RecordNotFoundException("No user found with this username");

            var admins = await _adminRepository.GetAll();
            var admin = admins.FirstOrDefault(a => a.UserId == user.guid);

            if (admin == null)
                throw new RecordNotFoundException("No admin found for this username");

            return admin;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching admin by username: {ex.Message}");
        }
    }

    public async Task<PagedResult<QuizData>> GetQuizzesByAdmin(Guid adminId, PageDataDto pageData)
    {
        try
        {
            var admin = await _adminRepository.Get(adminId);

            if (admin.quizDatas == null || !admin.quizDatas.Any())
                throw new RecordNotFoundException("No quizzes found for this admin");

            var totalCount = admin.quizDatas.Count;

            var pagedData = admin.quizDatas
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
            throw new Exception($"Error fetching quizzes by admin: {ex.Message}");
        }
    }
    public async Task<AdminQuizAttemptsSummaryResponseDto> GetQuizAttemptsSummary(Guid adminId)
    {
        try
        {
            var admin = await _adminRepository.Get(adminId);

            if (admin.quizDatas == null || !admin.quizDatas.Any())
            {
                throw new Exception("No quizzes found for this admin.");
            }

            var summaries = new List<QuizAttemptsSummaryDto>();

            foreach (var quiz in admin.quizDatas)
            {
                var attempts = quiz.quizAttempts ?? new List<QuizAttempt>();

                int totalAttempts = attempts.Count;

                int attemptsAbove50 = attempts.Count(a => a.Score >= Math.Ceiling((quiz.questions?.Count ?? 1) * 0.5));
                int attemptsBelowOrEqual50 = totalAttempts - attemptsAbove50;

                summaries.Add(new QuizAttemptsSummaryDto
                {
                    QuizId = quiz.Id,
                    QuizTitle = quiz.Title,
                    TotalAttempts = totalAttempts,
                    AttemptsAbove50Percent = attemptsAbove50,
                    AttemptsBelowOrEqual50Percent = attemptsBelowOrEqual50
                });
            }

            return new AdminQuizAttemptsSummaryResponseDto
            {
                QuizSummaries = summaries
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error generating attempts summary: {ex.Message}");
        }
    }



}
