using JobPortal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Quiz.Models;

[ApiController]
[Route("api/[controller]")]
public class AttenderController : ControllerBase
{
    private readonly IAttemptorService _attenderService;

    public AttenderController(IAttemptorService attenderService)
    {
        _attenderService = attenderService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Attender>> RegisterAttender([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            var attender = await _attenderService.RegisterAttemptor(userRegisterDto);
            return Ok(attender);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{attenderId:guid}")]
    public async Task<ActionResult<Attender>> GetAttenderById(Guid attenderId)
    {
        try
        {
            var attender = await _attenderService.GetAttenderById(attenderId);
            return Ok(attender);
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
    public async Task<ActionResult<Attender>> GetAttenderByUsername(string username)
    {
        try
        {
            var attender = await _attenderService.GetAttenderByUsername(username);
            return Ok(attender);
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

    [HttpGet("{attenderId:guid}/attempts")]
    public async Task<ActionResult<PagedResult<QuizAttempt>>> GetAttemptsByAttenderId(
        Guid attenderId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _attenderService.GetAttemptsByAttenderId(attenderId, new PageDataDto
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
    [HttpGet("{attenderId}/individual-attempts")]
    public async Task<ActionResult<AttenderIndividualAttemptsResponseDto>> GetIndividualAttempts(Guid attenderId)
    {
        try
        {
            var result = await _attenderService.GetIndividualAttempts(attenderId);
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
