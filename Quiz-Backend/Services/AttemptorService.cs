using JobPortal.Exceptions;
using Quiz.Contexts;
using Quiz.Exceptions;
using Quiz.Models;

public class AttenderService : IAttemptorService
{
    private readonly IRepository<Guid, User> _userRepository;
    private readonly IRepository<Guid, Attender> _attenderRepository;

    private readonly UserMapper userMapper;
    private readonly AttenderMapper attenderMapper;

    public AttenderService(
        IRepository<Guid, User> userRepository,
        IRepository<Guid, Attender> attenderRepository
    )
    {
        _userRepository = userRepository;
        _attenderRepository = attenderRepository;
        userMapper = new UserMapper();
        attenderMapper = new AttenderMapper();
    }



    public async Task<Attender> RegisterAttemptor(UserRegisterDto userRegisterDto)
    {
        try
        {
            var existingUsers = await _userRepository.GetAll();
            var existingUser = existingUsers.FirstOrDefault(u => u.Username == userRegisterDto.Username);

            if (existingUser != null)
            {
                var attenders = await _attenderRepository.GetAll();
                var existingAttender = attenders.FirstOrDefault(a => a.UserId == existingUser.guid);

                if (existingAttender != null)
                {

                    return existingAttender;
                }
                else
                {

                    var newAttender = attenderMapper.MaptoAttender(userRegisterDto);
                    newAttender.UserId = existingUser.guid;
                    await _attenderRepository.Add(newAttender);
                    return newAttender;
                }
            }

            var user = userMapper.MaptoUser(userRegisterDto);
            await _userRepository.Add(user);

            var attender = attenderMapper.MaptoAttender(userRegisterDto);
            attender.UserId = user.guid;
            await _attenderRepository.Add(attender);

            return attender;
        }
        catch (Exception ex)
        {
            throw new Exception($"Registration cannot be performed: {ex.Message}");
        }
    }
    public async Task<Attender> GetAttenderById(Guid attenderId)
    {
        try
        {
            var attender = await _attenderRepository.Get(attenderId);

            if (attender == null)
                throw new RecordNotFoundException("Attender not found");

            return attender;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching attender: {ex.Message}");
        }
    }
    public async Task<PagedResult<QuizAttempt>> GetAttemptsByAttenderId(Guid attenderId, PageDataDto pageData)
    {
        try
        {
            var attender = await _attenderRepository.Get(attenderId);

            if (attender.quizAttempts == null || !attender.quizAttempts.Any())
            {
                throw new RecordNotFoundException("No attempts found for this attender");
            }

            var totalCount = attender.quizAttempts.Count;

            var data = attender.quizAttempts
                        .OrderByDescending(a => a.AttemptedAt)
                        .Skip((pageData.pageNumber - 1) * pageData.pageSize)
                        .Take(pageData.pageSize)
                        .ToList();

            return new PagedResult<QuizAttempt>
            {
                TotalCount = totalCount,
                Data = data,
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
            throw new Exception($"Error fetching attender attempts: {ex.Message}");
        }
    }
    public async Task<Attender> GetAttenderByUsername(string username)
    {
        try
        {
            // first find the user by username
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new RecordNotFoundException("No user found with this username");

            // then check if that user is linked to an attender
            var attenders = await _attenderRepository.GetAll();
            var attender = attenders.FirstOrDefault(a => a.UserId == user.guid);

            if (attender == null)
                throw new RecordNotFoundException("No attender found for this username");

            return attender;
        }
        catch (RecordNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching attender by username: {ex.Message}");
        }
    }
    public async Task<AttenderIndividualAttemptsResponseDto> GetIndividualAttempts(Guid attenderId)
    {
        try
        {
            var attender = await _attenderRepository.Get(attenderId);

            if (attender.quizAttempts == null || !attender.quizAttempts.Any())
            {
                throw new Exception("No attempts found for this attender.");
            }

            var summaries = attender.quizAttempts.Select(a =>
            {
                var quizData = a.quizData;
                var totalQuestions = quizData?.questions?.Count ?? 1;
                var minScoreForPass = Math.Ceiling(totalQuestions * 0.5);

                return new AttenderIndividualAttemptSummaryDto
                {
                    QuizId = a.QuizId,
                    QuizTitle = quizData?.Title ?? "Unknown Quiz",
                    Score = a.Score,
                    IsAbove50Percent = a.Score >= minScoreForPass,
                    AttemptedAt = a.AttemptedAt
                };
            }).ToList();

            return new AttenderIndividualAttemptsResponseDto
            {
                Attempts = summaries
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error generating individual attempts summary: {ex.Message}");
        }
    }


}
