using Auditor.Domain.Entities;
using Auditor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using Repository.Infrastructure.Abstractions;

namespace Auditor.Repository;

public class AuditorDbContext(
    DbContextOptions<AuditorDbContext> options,
    IDefaultContainerProvider defaultContainerProvider)
    : BaseNoSqlDbContext(
        options,
        typeof(ModelConfigurationsIndicator).Assembly,
        defaultContainerProvider)
{
    public DbSet<EventNote> Events => Set<EventNote>();
}