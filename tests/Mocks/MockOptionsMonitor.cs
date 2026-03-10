using System;
using Microsoft.Extensions.Options;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockOptionsMonitor<TOptions> : IOptionsMonitor<TOptions>
{
    internal Func<String?, TOptions> _getFunc;

    TOptions IOptionsMonitor<TOptions>.CurrentValue =>
        throw new NotImplementedException();

    internal MockOptionsMonitor() =>
        this._getFunc = _ => throw new NotImplementedException();

    TOptions IOptionsMonitor<TOptions>.Get(String? name) =>
        this._getFunc(name);

    IDisposable? IOptionsMonitor<TOptions>.OnChange(Action<TOptions, String?> listener) =>
        throw new NotImplementedException();
}
