using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockQueryCollection : IQueryCollection
{
    internal Func<String, (bool, StringValues)> _tryGetValueFunc;

    int IQueryCollection.Count => throw new NotImplementedException();

    ICollection<String> IQueryCollection.Keys =>
        throw new NotImplementedException();

    StringValues IQueryCollection.this[String key] =>
        throw new NotImplementedException();

    internal MockQueryCollection() =>
        this._tryGetValueFunc = _ => throw new NotImplementedException();

    IEnumerator IEnumerable.GetEnumerator() =>
        throw new NotImplementedException();

    IEnumerator<KeyValuePair<String, StringValues>> IEnumerable<KeyValuePair<String, StringValues>>.GetEnumerator() =>
        throw new NotImplementedException();

    bool IQueryCollection.ContainsKey(String key) =>
        throw new NotImplementedException();

    bool IQueryCollection.TryGetValue(String key, out StringValues value)
    {
        (bool result, StringValues val) = this._tryGetValueFunc(key);
        value = val;
        return result;
    }
}
