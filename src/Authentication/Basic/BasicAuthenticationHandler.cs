using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Shipstone.AspNetCore.Authentication.Basic;

/// <summary>
/// Represents an <see cref="AuthenticationHandler{TOptions}" /> that can perform Basic authentication.
/// </summary>
public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    private readonly IBasicAuthenticateHandler _handler;

    /// <summary>
    /// Gets the <see cref="IBasicAuthenticateHandler" /> used to authenticate the current user.
    /// </summary>
    /// <value>The <see cref="IBasicAuthenticateHandler" /> used to authenticate the current user.</value>
    public IBasicAuthenticateHandler Handler => this._handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAuthenticationHandler" /> class with the specified properties.
    /// </summary>
    /// <param name="options">The monitor for the options instance.</param>
    /// <param name="logger">The <see cref="ILoggerFactory" />.</param>
    /// <param name="encoder">The <see cref="UrlEncoder" />.</param>
    /// <param name="handler">The <see cref="IBasicAuthenticateHandler" /> to be used to authenticate the current user.</param>
    public BasicAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IBasicAuthenticateHandler handler
    )
        : base(options, logger, encoder)
    {
        ArgumentNullException.ThrowIfNull(handler);
        this._handler = handler;
    }

    /// <inheritdoc />
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        String? authorization = this.Request.Headers.Authorization;

        if (String.IsNullOrWhiteSpace(authorization))
        {
            return AuthenticateResult.NoResult();
        }

        String schemeName = this.Scheme.Name;
        String key = $"{schemeName} ";

        if (!authorization.StartsWith(
            key,
            StringComparison.OrdinalIgnoreCase
        ))
        {
            return AuthenticateResult.NoResult();
        }

        authorization = authorization[key.Length..].Trim();
        ReadOnlySpan<byte> authorizationBytes;

        try
        {
            authorizationBytes =
                Convert.FromBase64String(authorization);
        }

        catch (FormatException)
        {
            return AuthenticateResult.NoResult();
        }

        authorization = Encoding.UTF8.GetString(authorizationBytes);
        int index = authorization.IndexOf(':');

        if (index < 1 || index == authorization.Length - 1)
        {
            return AuthenticateResult.NoResult();
        }

        IEnumerable<Claim> claims;

        try
        {
            claims =
                await this._handler.HandleAsync(
                    authorization[..index],
                    authorization[(index + 1)..],
                    CancellationToken.None
                );
        }

        catch (BasicAuthenticateException ex)
        {
            return AuthenticateResult.Fail(ex);
        }

        ClaimsIdentity identity = new(claims, schemeName);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, schemeName);
        return AuthenticateResult.Success(ticket);
    }

    /// <inheritdoc />
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        HttpResponse response = this.Context.Response;
        response.StatusCode = StatusCodes.Status401Unauthorized;
        response.Headers.WWWAuthenticate = this.Scheme.Name;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        this.Context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }
}
