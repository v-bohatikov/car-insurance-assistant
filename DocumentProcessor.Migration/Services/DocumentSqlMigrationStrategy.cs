using DocumentProcessor.Repository;
using Repository.Infrastructure.Migrations;

namespace DocumentProcessor.Migration.Services;

public class DocumentSqlMigrationStrategy
    : SqlMigrationStrategyBase<DocumentDbContext>
{ }