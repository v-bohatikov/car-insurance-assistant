using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel;

namespace Repository.Infrastructure.Interceptors;

public sealed class AuditingSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        SetAuditingData(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        SetAuditingData(eventData);

        return base.SavingChanges(eventData, result);
    }

    private static void SetAuditingData(DbContextEventData eventData)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return;
        }

        var auditableEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);
        foreach (var entity in auditableEntities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    entity.Property(e => e.CreatedOn).CurrentValue = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entity.Property(e => e.ChangedOn).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }
    }
}