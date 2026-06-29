namespace Registration.Domain.Common;

/// <summary>
/// Implemented by entities that carry audit columns. The values are populated by
/// a persistence-layer interceptor, so the Domain never depends on the clock or
/// the current-user accessor directly.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; set; }

    string? CreatedBy { get; set; }

    DateTime? UpdatedAtUtc { get; set; }

    string? UpdatedBy { get; set; }
}
