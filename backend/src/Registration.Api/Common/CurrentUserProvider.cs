using Registration.Application.Common.Interfaces;

namespace Registration.Api.Common;

/// <summary>
/// Resolves the current user for audit stamping. With no authentication wired up,
/// it returns a "system" principal, optionally overridden by an "X-User-Id" header.
/// </summary>
public sealed class CurrentUserProvider : ICurrentUserProvider
{
    public const string SystemUser = "system";
    private const string UserHeader = "X-User-Id";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public string UserId
    {
        get
        {
            var headers = _httpContextAccessor.HttpContext?.Request.Headers;
            if (headers is not null
                && headers.TryGetValue(UserHeader, out var value)
                && !string.IsNullOrWhiteSpace(value))
            {
                return value.ToString();
            }

            return SystemUser;
        }
    }
}
