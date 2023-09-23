namespace ForeheadApi.Dtos;

public class CategoryWithQuestionsDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public QuestionDto[]? Questions { get; set; }
}
