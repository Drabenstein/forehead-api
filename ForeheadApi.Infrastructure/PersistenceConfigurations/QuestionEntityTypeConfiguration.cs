using ForeheadApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForeheadApi.Infrastructure.PersistenceConfigurations;

public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Text)
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(x => x.HelperText)
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(x => x.AuthorName)
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(x => x.AddedAt)
            .HasColumnType("timestamptz")
            .IsRequired();
    }
}
