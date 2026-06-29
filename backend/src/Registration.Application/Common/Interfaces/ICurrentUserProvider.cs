namespace Registration.Application.Common.Interfaces;

/// <summary>
/// Supplies the identity used to stamp audit columns. With no authentication in
/// place the implementation returns a system principal; the abstraction lets that
/// evolve without touching the Application layer.
/// </summary>
public interface ICurrentUserProvider
{
    /// <summary>The current user identifier, or a system fallback.</summary>
    string UserId { get; }
}
