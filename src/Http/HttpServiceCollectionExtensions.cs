using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shipstone.AspNetCore.Http;

/// <summary>
/// Provides a set of <c>static</c> methods (<c>Shared</c> in Visual Basic) methods for registering services with objects that implement <see cref="IServiceCollection" />.
/// </summary>
public static class HttpServiceCollectionExtensions
{
    /// <summary>
    /// Registers services required by <see cref="ArgumentException" /> handling services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to register services with.</param>
    /// <param name="statusCode">The HTTP status code to return.</param>
    /// <returns>A reference to <c><paramref name="services" /></c> that can be further used to register services.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="services" /></c> is <c>null</c>.</exception>
    public static IServiceCollection AddArgumentExceptionHandling(
        this IServiceCollection services,
        int statusCode = StatusCodes.Status400BadRequest
    )
    {
        ArgumentNullException.ThrowIfNull(services);

        return services.AddSingleton(_ =>
            new ArgumentExceptionHandlingMiddleware(statusCode));
    }

    /// <summary>
    /// Registers services required by NCSA common logging services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to register services with.</param>
    /// <param name="writer">The <see cref="TextWriter" /> to use when writing logs.</param>
    /// <returns>A reference to <c><paramref name="services" /></c> that can be further used to register services.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="services" /></c> is <c>null</c> -or- <c><paramref name="writer" /></c> is <c>null</c>.</exception>
    public static IServiceCollection AddNcsaCommonLogging(
        this IServiceCollection services,
        TextWriter writer
    )
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(writer);

        return services.AddSingleton(_ =>
            new NcsaCommonLoggingMiddleware(writer));
    }

    /// <summary>
    /// Registers services required by pagination services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to register services with.</param>
    /// <returns>A reference to <c><paramref name="services" /></c> that can be further used to register services.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="services" /></c> is <c>null</c>.</exception>
    public static IServiceCollection AddPagination(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        return services.AddScoped<PaginationMiddleware>();
    }
}