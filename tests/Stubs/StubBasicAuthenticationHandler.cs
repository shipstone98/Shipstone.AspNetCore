using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Shipstone.AspNetCore.Authentication.Basic;

namespace Shipstone.AspNetCoreTest.Stubs;

internal sealed class StubBasicAuthenticationHandler
    : BasicAuthenticationHandler
{
    internal StubBasicAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IBasicAuthenticateHandler handler
    )
        : base(options, logger, encoder, handler)
    { }

    internal new Task<AuthenticateResult> HandleAuthenticateAsync() =>
        base.HandleAuthenticateAsync();

    internal new Task HandleChallengeAsync(AuthenticationProperties properties) =>
        base.HandleChallengeAsync(properties);

    internal new Task HandleForbiddenAsync(AuthenticationProperties properties) =>
        base.HandleForbiddenAsync(properties);
}
