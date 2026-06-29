using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Registration.Application.Common.Interfaces;
using Registration.Domain.Common;

namespace Registration.Persistence.Interceptors;

/// <summary>
/// Stamps audit columns (CreatedAtUtc/CreatedBy, UpdatedAtUtc/UpdatedBy) on
/// entities implementing <see cref="IAuditableEntity"/> just before they are saved.
/// </summary>
public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTime;
    private readonly ICurrentUserProvider _currentUser;

    public AuditableEntityInterceptor(IDateTimeProvider dateTime, ICurrentUserProvider currentUser)
    {
        _dateTime = dateTime;
        _currentUser = currentUser;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var now = _dateTime.UtcNow;
        var user = _currentUser.UserId;

        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = now;
                entry.Entity.CreatedBy = user;
            }
            else if (entry.State == EntityState.Modified || HasModifiedOwnedEntities(entry))
            {
                entry.Entity.UpdatedAtUtc = now;
                entry.Entity.UpdatedBy = user;
            }
        }
    }

    // Owned value-object edits leave the owner "Unchanged"; detect them so updates are stamped.
    private static bool HasModifiedOwnedEntities(EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry is not null
            && r.TargetEntry.Metadata.IsOwned()
            && r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}
