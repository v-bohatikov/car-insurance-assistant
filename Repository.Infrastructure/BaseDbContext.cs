using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Converters;
using Repository.Infrastructure.Interceptors;
using System.Reflection;

namespace Repository.Infrastructure;

public class BaseDbContext(
    DbContextOptions options,
    Assembly modelConfigurationsAssembly)
    : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations from the target assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(modelConfigurationsAssembly);
    }
}