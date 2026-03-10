using System;
using Microsoft.AspNetCore.Authentication;

namespace Shipstone.AspNetCore.Authentication.Basic;

/// <summary>
/// Provides a set of <c>static</c> (<c>Shared</c> in Visual Basic) methods for registering services with instances of <see cref="AuthenticationBuilder" />.
/// </summary>
public static class AuthenticationBuilderExtensions
{
    /// <summary>
    /// Enables Basic authentication using the default scheme <see cref="BasicAuthenticationDefaults.AuthenticationScheme" /> for the specified <see cref="AuthenticationBuilder" />.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder" /> to enable Basic authentication for.</param>
    /// <param name="configureBasicAuthentication">A delegate to configure <see cref="BasicAuthenticationOptions" />, or <c>null</c>.</param>
    /// <returns>A reference to <c><paramref name="builder" /></c> that can be further used to enable authentication.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="builder" /></c> is <c>null</c>.</exception>
    public static AuthenticationBuilder AddBasic(
        this AuthenticationBuilder builder,
        Action<BasicAuthenticationOptions>? configureBasicAuthentication = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(
            BasicAuthenticationDefaults.AuthenticationScheme,
            configureBasicAuthentication
        );
    }
}
