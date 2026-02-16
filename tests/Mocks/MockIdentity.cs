using System;
using System.Security.Principal;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockIdentity : IIdentity
{
    internal Func<String?> _nameFunc;

    String? IIdentity.AuthenticationType =>
        throw new NotImplementedException();

    bool IIdentity.IsAuthenticated => throw new NotImplementedException();
    String? IIdentity.Name => this._nameFunc();

    internal MockIdentity() =>
        this._nameFunc = () => throw new NotImplementedException();
}
