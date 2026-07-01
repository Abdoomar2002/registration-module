using Microsoft.AspNetCore.Http;

namespace Registration.Presentation.Middleware;

/// <summary>
/// Adds security headers to every response: prevents MIME-type sniffing,
/// clickjacking, referrer leakage, and disables unnecessary browser features.
/// </summary>
public sealed class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Response.Headers;

        // Prevent MIME-type sniffing (e.g. treating a JSON response as HTML).
        headers["X-Content-Type-Options"] = "nosniff";

        // Prevent this page from being embedded in an iframe (clickjacking).
        headers["X-Frame-Options"] = "DENY";

        // Limit referrer information sent to third-party origins.
        headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

        // Disable browser features the API does not use.
        headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";

        // Basic Content-Security-Policy for API responses (no inline scripts/styles needed).
        headers["Content-Security-Policy"] = "default-src 'none'; frame-ancestors 'none'";

        await _next(context);
    }
}
