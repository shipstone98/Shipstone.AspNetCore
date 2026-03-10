using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Shipstone.AspNetCore.Authentication.Basic;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockBasicAuthenticateHandler : IBasicAuthenticateHandler
{
    internal Func<String, String, IEnumerable<Claim>> _handleFunc;

    internal MockBasicAuthenticateHandler() =>
        this._handleFunc = (_, _) => throw new NotImplementedException();

    Task<IEnumerable<Claim>> IBasicAuthenticateHandler.HandleAsync(
        String userId,
        String password,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<Claim> result = this._handleFunc(userId, password);
        return Task.FromResult(result);
    }
}
