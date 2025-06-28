using JobPortal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Quiz.Exceptions;
using Quiz.Models;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryAddDto categoryAddDto)
    {
        try
        {
            var category = await _categoryService.AddCategory(categoryAddDto);
            return Ok(category);
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{categoryId}/quizzes")]
    public async Task<ActionResult<PagedResult<QuizData>>> GetQuizzesByCategory(
        Guid categoryId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _categoryService.GetQuizzesByCategory(categoryId, new PageDataDto
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
}
