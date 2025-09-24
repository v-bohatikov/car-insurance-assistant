using Microsoft.EntityFrameworkCore;
using OrderProcessor.Domain.Entities.Orders;
using OrderProcessor.Repository.ModelConfigurations;
using Repository.Infrastructure;

namespace OrderProcessor.Repository;

public class OrderDbContext(DbContextOptions<OrderDbContext> options)
    : BaseSqlDbContext(options, typeof(ModelConfigurationsIndicator).Assembly)
{
    public DbSet<Order> Orders => Set<Order>();
}