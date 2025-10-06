using BillingProcessor.Repository;
using Repository.Infrastructure.Migrations;

namespace BillingProcessor.Migration.Services;

public class BillingSqlMigrationStrategy
    : SqlMigrationStrategyBase<BillingDbContext>
{ }