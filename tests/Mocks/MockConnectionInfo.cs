using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockConnectionInfo : ConnectionInfo
{
    internal Func<IPAddress?> _remoteIpAddressFunc;

    public sealed override X509Certificate2? ClientCertificate
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override String Id
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override IPAddress? LocalIpAddress
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override int LocalPort
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override IPAddress? RemoteIpAddress
    {
        get => this._remoteIpAddressFunc();
        set => throw new NotImplementedException();
    }

    public sealed override int RemotePort
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    internal MockConnectionInfo() =>
        this._remoteIpAddressFunc = () => throw new NotImplementedException();

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override Task<X509Certificate2?> GetClientCertificateAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    public sealed override void RequestClose() =>
        throw new NotImplementedException();

    public sealed override String ToString() =>
        throw new NotImplementedException();
}
