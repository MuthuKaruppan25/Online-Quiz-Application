

using Quiz.Models;

public interface IAttemptorService
{
    Task<Attender> RegisterAttemptor(UserRegisterDto userRegisterDto);
    Task<Attender> GetAttenderById(Guid attenderId);
    Task<PagedResult<QuizAttempt>> GetAttemptsByAttenderId(Guid attenderId, PageDataDto pageData);
    Task<Attender> GetAttenderByUsername(string username);
    Task<AttenderIndividualAttemptsResponseDto> GetIndividualAttempts(Guid attenderId);
}