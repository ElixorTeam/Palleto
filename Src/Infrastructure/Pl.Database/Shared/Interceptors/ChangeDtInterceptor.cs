using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Pl.Database.Shared.Interceptors;

internal class ChangeDtInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        BeforeSaveTriggers(eventData.Context);
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        BeforeSaveTriggers(eventData.Context);
        return ValueTask.FromResult(result);
    }

    private static void BeforeSaveTriggers(DbContext? context)
    {
        if (context == null) return;

        IEnumerable<EntityEntry> entities = context.ChangeTracker.Entries()
            .Where(x => x.State is EntityState.Modified or EntityState.Added);

        foreach (EntityEntry entityEntry in entities)
        {
            Type entityType = entityEntry.Entity.GetType();

            PropertyInfo? changeDtProperty = entityType.GetProperty(nameof(SqlColumns.ChangeDt));
            if (changeDtProperty?.CanWrite == true)
                changeDtProperty.SetValue(entityEntry.Entity, DateTime.Now);
        }
    }
}