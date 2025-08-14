using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Converters;

namespace Repository.Infrastructure;

public class BaseDbContext : DbContext
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();
    }
}