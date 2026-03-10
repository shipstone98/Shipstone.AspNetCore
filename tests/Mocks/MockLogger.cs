using System;
using Microsoft.Extensions.Logging;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockLogger : ILogger
{
    IDisposable? ILogger.BeginScope<TState>(TState state) =>
        throw new NotImplementedException();

    bool ILogger.IsEnabled(LogLevel logLevel) =>
        throw new NotImplementedException();

    void ILogger.Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, String> formatter
    ) =>
        throw new NotImplementedException();
}
