using OrderProcessor.Repository;
using Repository.Infrastructure.Migrations;

namespace OrderProcessor.Migration.Services;

public class OrderSqlMigrationStrategy
    : SqlMigrationStrategyBase<OrderDbContext>
{ }