namespace Registration.Domain.Common;

/// <summary>
/// Non-generic marker implemented by aggregate roots so infrastructure can
/// discover and dispatch their domain events without knowing the id type.
/// </summary>
public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
