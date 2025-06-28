using JobPortal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Quiz.Models;

[ApiController]
[Route("api/[controller]")]
public class AttemptController : ControllerBase
{
    private readonly IAttemptService _attemptService;

    public AttemptController(IAttemptService attemptService)
    {
        _attemptService = attemptService;
    }

    [HttpPost]
    public async Task<ActionResult<QuizAttempt>> AddAttempt([FromBody] AttemptAddDto attemptAddDto)
    {
        try
        {
            var result = await _attemptService.AddAttempt(attemptAddDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{attemptId:guid}")]
    public async Task<ActionResult<QuizAttempt>> GetAttemptById(Guid attemptId)
    {
        try
        {
            var result = await _attemptService.GetAttemptById(attemptId);
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

    [HttpGet("{attemptId:guid}/answers")]
    public async Task<ActionResult<PagedResult<Answers>>> GetAnswersByAttempt(
        Guid attemptId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _attemptService.GetAnswersByAttempt(attemptId, new PageDataDto
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
    [HttpGet("by-quiz/{quizId}/paged")]
    public async Task<ActionResult<PagedResult<QuizAttempt>>> GetPagedByQuiz(
    string quizId,
    [FromQuery] PageDataDto pageData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var paged = await _attemptService.GetAttemptsByQuizId(quizId, pageData);
            return Ok(paged);
        }
        catch (RecordNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("check-attempt")]
    public async Task<IActionResult> CheckQuizAlreadyAttempted(Guid attenderId, string quizId)
    {
        try
        {
            var result = await _attemptService.CheckQuizAlreadyAttempted(attenderId, quizId);

            return Ok(new
            {
                success = result.Success,
                message = result.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"Error checking attempt: {ex.Message}"
            });
        }
    }
}
