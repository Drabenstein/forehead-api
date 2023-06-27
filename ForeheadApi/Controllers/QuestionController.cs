using Microsoft.AspNetCore.Mvc;

namespace ForeheadApi.Controllers;

[ApiController]
[Route("questions")]
public class QuestionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetQuestionForCategoryAsync([FromQuery] int categoryId)
    {
        if (categoryId == 0)
        {
            return BadRequest();
        }

        return Ok(Array.Empty<string>());
    }
}
