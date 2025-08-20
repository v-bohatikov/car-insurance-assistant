using Microsoft.EntityFrameworkCore;
using OrderProcessor.Domain.Entities.Orders;
using OrderProcessor.Repository.ModelConfigurations;
using Repository.Infrastructure;

namespace OrderProcessor.Repository;

public class OrderDbContext(DbContextOptions<OrderDbContext> options)
    : BaseDbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}