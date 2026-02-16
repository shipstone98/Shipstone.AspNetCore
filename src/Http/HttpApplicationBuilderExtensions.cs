using System;
using Microsoft.AspNetCore.Builder;

namespace Shipstone.AspNetCore.Http;

/// <summary>
/// Provides a set of <c>static</c> methods (<c>Shared</c> in Visual Basic) methods for adding middleware to objects that implement <see cref="IApplicationBuilder" />.
/// </summary>
public static class HttpApplicationBuilderExtensions
{
    /// <summary>
    /// Adds <see cref="ArgumentException" /> handling middleware.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder" /> to add middleware to.</param>
    /// <returns>A reference to <c><paramref name="app" /></c> that can be further used to add middleware.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="app" /></c> is <c>null</c>.</exception>
    public static IApplicationBuilder UseArgumentExceptionHandling(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<ArgumentExceptionHandlingMiddleware>();
    }

    /// <summary>
    /// Adds NCSA commong logging middleware.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder" /> to add middleware to.</param>
    /// <returns>A reference to <c><paramref name="app" /></c> that can be further used to add middleware.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="app" /></c> is <c>null</c>.</exception>
    public static IApplicationBuilder UseNcsaCommonLogging(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<NcsaCommonLoggingMiddleware>();
    }

    /// <summary>
    /// Adds pagination middleware.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder" /> to add middleware to.</param>
    /// <returns>A reference to <c><paramref name="app" /></c> that can be further used to add middleware.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="app" /></c> is <c>null</c>.</exception>
    public static IApplicationBuilder UsePagination(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<PaginationMiddleware>();
    }
}
