using Microsoft.AspNetCore.Mvc;
using Quiz.Models;
using Quiz.Contexts;
using JobPortal.Exceptions;
using Quiz.Exceptions;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Admin>> RegisterAdmin([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            var admin = await _adminService.RegisterAdmin(userRegisterDto);
            return Ok(admin);
        }
        catch (RecordExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{adminId:guid}")]
    public async Task<ActionResult<Admin>> GetAdminById(Guid adminId)
    {
        try
        {
            var admin = await _adminService.GetAdminById(adminId);
            return Ok(admin);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("by-username/{username}")]
    public async Task<ActionResult<Admin>> GetAdminByUsername(string username)
    {
        try
        {
            var admin = await _adminService.GetAdminByUsername(username);
            return Ok(admin);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{adminId:guid}/quizzes")]
    public async Task<ActionResult<PagedResult<QuizData>>> GetQuizzesByAdmin(
        Guid adminId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _adminService.GetQuizzesByAdmin(adminId, new PageDataDto
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            });
            return Ok(result);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet("{adminId}/quiz-attempts-summary")]
    public async Task<ActionResult<AdminQuizAttemptsSummaryResponseDto>> GetQuizAttemptsSummary(Guid adminId)
    {
        try
        {
            var result = await _adminService.GetQuizAttemptsSummary(adminId);
            return Ok(result);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
