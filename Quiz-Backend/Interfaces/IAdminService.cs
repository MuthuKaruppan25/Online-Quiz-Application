
namespace Quiz.Contexts;

using Quiz.Models;

public interface IAdminService
{
    Task<Admin> RegisterAdmin(UserRegisterDto userRegisterDto);
    Task<Admin> GetAdminById(Guid adminId);
    Task<Admin> GetAdminByUsername(string username);
    Task<PagedResult<QuizData>> GetQuizzesByAdmin(Guid adminId, PageDataDto pageData);
    Task<AdminQuizAttemptsSummaryResponseDto> GetQuizAttemptsSummary(Guid adminId);
}