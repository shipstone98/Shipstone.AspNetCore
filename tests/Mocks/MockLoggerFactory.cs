using System;
using Microsoft.Extensions.Logging;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockLoggerFactory : ILoggerFactory
{
    internal Func<String, ILogger> _createLoggerFunc;

    internal MockLoggerFactory() =>
        this._createLoggerFunc = _ => throw new NotImplementedException();

    void IDisposable.Dispose() => throw new NotImplementedException();

    void ILoggerFactory.AddProvider(ILoggerProvider provider) =>
        throw new NotImplementedException();

    ILogger ILoggerFactory.CreateLogger(String categoryName) =>
        this._createLoggerFunc(categoryName);
}
