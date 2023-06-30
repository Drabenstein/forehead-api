using ForeheadApi.Core;
using ForeheadApi.Infrastructure.PersistenceConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ForeheadApi.Infrastructure;

public class ForeheadDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Question> Questions { get; set; }

    public ForeheadDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}
