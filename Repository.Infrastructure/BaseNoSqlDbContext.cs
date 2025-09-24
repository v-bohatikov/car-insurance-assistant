using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Converters;
using System.Reflection;
using Repository.Infrastructure.Abstractions;

namespace Repository.Infrastructure;

public class BaseNoSqlDbContext(
    DbContextOptions options,
    Assembly modelConfigurationsAssembly,
    IDefaultContainerProvider containerProvider)
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
        // Specify default container name.
        var defaultContainerName = containerProvider.GetDefaultContainerName();
        modelBuilder.HasDefaultContainer(defaultContainerName);

        // Apply all type configurations from the target assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(modelConfigurationsAssembly);
    }
}