namespace Registration.Domain.Common;

/// <summary>
/// Base class for domain entities, providing identity-based equality.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId> where TId : notnull
{
    protected Entity(TId id) => Id = id;

    /// <summary>Parameterless constructor reserved for the EF Core materializer.</summary>
    protected Entity()
    {
    }

    public TId Id { get; protected set; } = default!;

    public override bool Equals(object? obj) =>
        obj is Entity<TId> other
        && GetType() == other.GetType()
        && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    public override int GetHashCode() => HashCode.Combine(GetType(), Id);

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !Equals(left, right);
}
