namespace ForeheadApi.Core;

public record Category
{
    private readonly HashSet<Question> questions = new();

    public int Id { get; init; }
    public required string Name { get; init; }
    public required string ImageUrl { get; init; }
    public IReadOnlyCollection<Question> Questions => questions;

    public void AddQuestion(Question question)
    {
        questions.Add(question);
    }

    public void RemoveQuestion(Question question)
    {
        questions.Remove(question);
    }
}
