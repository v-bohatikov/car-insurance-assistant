using PolicyProcessor.Repository;
using Repository.Infrastructure.Migrations;

namespace PolicyProcessor.Migration.Services;

public class PolicySqlMigrationStrategy
    : SqlMigrationStrategyBase<PolicyDbContext>
{ }