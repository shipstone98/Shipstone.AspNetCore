using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockHttpResponse : HttpResponse
{
    internal Action<Stream> _bodyAction;
    internal Func<Stream> _bodyFunc;
    internal Func<bool> _hasStartedFunc;
    internal Action<int> _statusCodeAction;
    internal Func<int> _statusCodeFunc;

    public sealed override Stream Body
    {
        get => this._bodyFunc();
        set => this._bodyAction(value);
    }

    public sealed override PipeWriter BodyWriter =>
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

    public sealed override IResponseCookies Cookies =>
        throw new NotImplementedException();

    public sealed override bool HasStarted => this._hasStartedFunc();

    public sealed override IHeaderDictionary Headers =>
        throw new NotImplementedException();

    public sealed override HttpContext HttpContext =>
        throw new NotImplementedException();

    public sealed override int StatusCode
    {
        get => this._statusCodeFunc();
        set => this._statusCodeAction(value);
    }

    internal MockHttpResponse()
    {
        this._bodyAction = _ => throw new NotImplementedException();
        this._bodyFunc = () => throw new NotImplementedException();
        this._hasStartedFunc = () => throw new NotImplementedException();
        this._statusCodeAction = _ => throw new NotImplementedException();
        this._statusCodeFunc = () => throw new NotImplementedException();
    }

    public sealed override Task CompleteAsync() =>
        throw new NotImplementedException();

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    public sealed override void OnCompleted(
        Func<Object, Task> callback,
        Object state
    ) =>
        throw new NotImplementedException();

    public sealed override void OnCompleted(Func<Task> callback) =>
        throw new NotImplementedException();

    public sealed override void OnStarting(
        Func<Object, Task> callback,
        Object state
    ) =>
        throw new NotImplementedException();

    public sealed override void OnStarting(Func<Task> callback) =>
        throw new NotImplementedException();

    public sealed override void Redirect([StringSyntax("Uri")] String location) =>
        throw new NotImplementedException();

    public sealed override void Redirect(
        [StringSyntax("Uri")] String location,
        bool permanent
    ) =>
        throw new NotImplementedException();

    public sealed override void RegisterForDispose(IDisposable disposable) =>
        throw new NotImplementedException();

    public sealed override void RegisterForDisposeAsync(IAsyncDisposable disposable) =>
        throw new NotImplementedException();

    public sealed override Task StartAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public sealed override String ToString() =>
        throw new NotImplementedException();
}
