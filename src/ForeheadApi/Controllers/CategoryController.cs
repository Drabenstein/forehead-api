using ForeheadApi.Auth;
using ForeheadApi.Dtos;
using ForeheadApi.Infrastructure;
using ForeheadApi.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ForeheadApi.Controllers;

[ApiController]
[RequireApiKey]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ForeheadDbContext foreheadDbContext;
    private readonly IMemoryCache memoryCache;

    public CategoryController(ForeheadDbContext foreheadDbContext, IMemoryCache memoryCache)
    {
        this.foreheadDbContext = foreheadDbContext;
        this.memoryCache = memoryCache;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync([FromQuery] bool full = false)
    {
        if (full)
        {
            if (!memoryCache.TryGetValue(CategoryWithQuestionsCacheKey, out CategoryWithQuestionsDto[] categoriesWithQuestions))
            {
                categoriesWithQuestions = await foreheadDbContext.Categories.AsNoTracking().ProjectToCategoryWithQuestionsDto().ToArrayAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                memoryCache.Set(CategoryWithQuestionsCacheKey, categoriesWithQuestions, cacheEntryOptions); 
            }

            return Ok(categoriesWithQuestions);
        }

        var categories = await foreheadDbContext.Categories.AsNoTracking().ProjectToDto().ToArrayAsync();
        return Ok(categories);
    }

    [HttpGet]
    [Route("{categoryId}/questions")]
    [ResponseCache(Duration = 630, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetQuestionForCategory(int categoryId)
    {
        if (!memoryCache.TryGetValue(GetQuestionForCategoryCacheKey(categoryId), out QuestionDto[]? questions))
        {
            questions = await foreheadDbContext.Categories
                                                       .AsNoTracking()
                                                       .Where(x => x.Id == categoryId)
                                                       .SelectMany(x => x.Questions)
                                                       .ProjectToDto()
                                                       .ToArrayAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
            memoryCache.Set(GetQuestionForCategoryCacheKey(categoryId), questions, cacheEntryOptions);
        }

        if (questions?.Length > 0)
        {
            return Ok(questions);
        }

        return NotFound();
    }

    private static string GetQuestionForCategoryCacheKey(int categoryId) => $"questions-{categoryId}";
    private const string CategoryWithQuestionsCacheKey = "categorywq";
}
