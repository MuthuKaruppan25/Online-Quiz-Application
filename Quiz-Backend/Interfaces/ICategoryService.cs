
using Quiz.Models;

public interface ICategoryService
{
    Task<Category> AddCategory(CategoryAddDto categoryAddDto);
    Task<IEnumerable<Category>> GetCategories();
    Task<PagedResult<QuizData>> GetQuizzesByCategory(Guid categoryId, PageDataDto pageData);
}