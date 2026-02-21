using Microsoft.EntityFrameworkCore.ChangeTracking;
using Arqom.Core.Domain.Entities;

namespace Arqom.Infra.Data.Sql.Commands.Extensions;
public static class ChangeTrackerExtensions
{
    public static List<IAggregateRoot> GetChangedAggregates(this ChangeTracker changeTracker) =>
        changeTracker.Aggreates().Where(IsModified()).Select(c => c.Entity).ToList();

    public static List<IAggregateRoot> GetAggregatesWithEvent(this ChangeTracker changeTracker) =>
            changeTracker.Aggreates()
                                     .Where(IsNotDetached()).Select(c => c.Entity).Where(c => c.GetEvents().Any()).ToList();
    public static IEnumerable<EntityEntry<IAggregateRoot>> Aggreates(this ChangeTracker changeTracker) =>
        changeTracker.Entries<IAggregateRoot>();

    private static Func<EntityEntry<IAggregateRoot>, bool> IsNotDetached() =>
        x => x.State != EntityState.Detached;

    private static Func<EntityEntry<IAggregateRoot>, bool> IsModified()
    {
        return x => x.State == EntityState.Modified ||
                                           x.State == EntityState.Added ||
                                           x.State == EntityState.Deleted;
    }

}
