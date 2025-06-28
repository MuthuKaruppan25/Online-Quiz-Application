using JobPortal.Exceptions;
using Quiz.Contexts;
using Quiz.Exceptions;
using Quiz.Models;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Guid, Category> _categoryRepository;

    public CategoryService(IRepository<Guid, Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> AddCategory(CategoryAddDto categoryAddDto)
    {
        try
        {
            var existingCategories = await _categoryRepository.GetAll();
            if (existingCategories.Any(c => c.Name.Equals(categoryAddDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new RecordExistsException("Category with this name already exists");
            }

            var category = new Category
            {
                Name = categoryAddDto.Name
            };

            await _categoryRepository.Add(category);
            return category;
        }
        catch (RecordExistsException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not add category: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        try
        {
            return await _categoryRepository.GetAll();
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not fetch categories: {ex.Message}");
        }
    }

    public async Task<PagedResult<QuizData>> GetQuizzesByCategory(Guid categoryId, PageDataDto pageData)
    {
        try
        {
            var category = await _categoryRepository.Get(categoryId);

            if (category.quizDatas == null || !category.quizDatas.Any())
            {
                throw new RecordNotFoundException("No quizzes found for this category");
            }

            var totalCount = category.quizDatas.Count;

            var pagedData = category.quizDatas
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
            throw new Exception($"Could not fetch quizzes for category: {ex.Message}");
        }
    }

}
