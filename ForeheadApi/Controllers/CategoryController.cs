using Microsoft.AspNetCore.Mvc;

namespace ForeheadApi.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok(Array.Empty<string>());
    }
}