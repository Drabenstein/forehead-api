using ForeheadApi.Core;
using ForeheadApi.Dtos;
using Riok.Mapperly.Abstractions;

namespace ForeheadApi.Infrastructure.Mappings;

[Mapper]
public static partial class CategoryMapper
{
    public static partial IQueryable<CategoryDto> ProjectToDto(this IQueryable<Category> categories);

    public static partial IQueryable<CategoryWithQuestionsDto> ProjectToCategoryWithQuestionsDto(this IQueryable<Category> categories);
}
