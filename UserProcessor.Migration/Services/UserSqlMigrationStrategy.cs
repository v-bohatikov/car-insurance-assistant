using Repository.Infrastructure.Migrations;
using UserProcessor.Repository;

namespace UserProcessor.Migration.Services;

public class UserSqlMigrationStrategy
    : SqlMigrationStrategyBase<UserDbContext>
{ }