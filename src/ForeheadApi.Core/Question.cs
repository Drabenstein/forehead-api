namespace ForeheadApi.Core;

public record Question
{
    public int Id { get; init; }
    public required string Text { get; init; }
    public required string HelperText { get; init; }
    public required string AuthorName { get; init; }
    public DateTimeOffset AddedAt { get; } = DateTimeOffset.Now;

    public int CategoryId { get; init; }
}
