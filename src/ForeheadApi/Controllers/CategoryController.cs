using ForeheadApi.Infrastructure;
using ForeheadApi.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForeheadApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ForeheadDbContext foreheadDbContext;

    public CategoryController(ForeheadDbContext foreheadDbContext)
    {
        this.foreheadDbContext = foreheadDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var categories = await foreheadDbContext.Categories.AsNoTracking().ProjectToDto().ToArrayAsync();
        return Ok(categories);
    }

    [HttpGet]
    [Route("{categoryId}/questions")]
    public async Task<IActionResult> GetQuestionForCategory(int categoryId)
    {
        var questions = await foreheadDbContext.Questions.AsNoTracking()
                                                   .Where(x => x.CategoryId == categoryId)
                                                   .ProjectToDto()
                                                   .ToArrayAsync();
        if (questions?.Length > 0)
        {
            return Ok(questions);
        }

        return NotFound();
    }
}