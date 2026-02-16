using System;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockHttpRequest : HttpRequest
{
    internal Func<String> _methodFunc;
    internal Func<PathString> _pathFunc;
    internal Func<String> _protocolFunc;
    internal Func<IQueryCollection> _queryFunc;
    internal Func<QueryString> _queryStringFunc;

    public sealed override Stream Body
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override PipeReader BodyReader =>
        throw new NotImplementedException();

    public sealed override Nullable<long> ContentLength
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override String? ContentType
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override IRequestCookieCollection Cookies
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override IFormCollection Form
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override bool HasFormContentType =>
        throw new NotImplementedException();

    public sealed override IHeaderDictionary Headers =>
        throw new NotImplementedException();

    public sealed override HostString Host
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override HttpContext HttpContext =>
        throw new NotImplementedException();

    public sealed override bool IsHttps
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override String Method
    {
        get => this._methodFunc();
        set => throw new NotImplementedException();
    }

    public sealed override PathString Path
    {
        get => this._pathFunc();
        set => throw new NotImplementedException();
    }

    public sealed override PathString PathBase
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override String Protocol
    {
        get => this._protocolFunc();
        set => throw new NotImplementedException();
    }

    public sealed override IQueryCollection Query
    {
        get => this._queryFunc();
        set => throw new NotImplementedException();
    }

    public sealed override QueryString QueryString
    {
        get => this._queryStringFunc();
        set => throw new NotImplementedException();
    }

    public sealed override RouteValueDictionary RouteValues
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public sealed override String Scheme
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    internal MockHttpRequest()
    {
        this._methodFunc = () => throw new NotImplementedException();
        this._pathFunc = () => throw new NotImplementedException();
        this._protocolFunc = () => throw new NotImplementedException();
        this._queryFunc = () => throw new NotImplementedException();
        this._queryStringFunc = () => throw new NotImplementedException();
    }

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    public sealed override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public sealed override String ToString() =>
        throw new NotImplementedException();
}
