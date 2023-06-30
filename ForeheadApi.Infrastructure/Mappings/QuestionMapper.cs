using ForeheadApi.Core;
using ForeheadApi.Dtos;
using Riok.Mapperly.Abstractions;

namespace ForeheadApi.Infrastructure.Mappings;

[Mapper()]
public static partial class QuestionMapper
{
    public static partial IQueryable<QuestionDto> ProjectToDto(this IQueryable<Question> questions);
}
