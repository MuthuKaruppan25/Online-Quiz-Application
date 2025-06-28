using JobPortal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Quiz.Models;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    public async Task<ActionResult<QuizAddResponse>> AddQuiz([FromBody] QuizAddDto quizAddDto)
    {
        try
        {
            var result = await _quizService.AddQuiz(quizAddDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{quizId}")]
    public async Task<ActionResult<QuizData>> GetQuizById(string quizId)
    {
        try
        {
            var quiz = await _quizService.GetQuizById(quizId);
            return Ok(quiz);
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

    [HttpGet("{quizId}/questions")]
    public async Task<ActionResult<PagedResult<Question>>> GetQuestionsByQuizId(
        string quizId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _quizService.GetQuestionsByQuizId(quizId, new PageDataDto
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

    [HttpGet("{quizId}/attempts")]
    public async Task<ActionResult<PagedResult<QuizAttempt>>> GetAttemptsByQuizId(
        string quizId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _quizService.GetAttemptsByQuizId(quizId, new PageDataDto
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
    [HttpGet("all")]
    public async Task<IActionResult> GetAllQuizzes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var pageData = new PageDataDto
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            var result = await _quizService.GetAllQuizzes(pageData);
            return Ok(result);
        }
        catch (RecordNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
