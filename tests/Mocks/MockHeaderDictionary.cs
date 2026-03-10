using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockHeaderDictionary : IHeaderDictionary
{
    internal Action<String, StringValues> _itemAction;
    internal Func<String, StringValues> _itemFunc;

    int ICollection<KeyValuePair<String, StringValues>>.Count =>
        throw new NotImplementedException();

    bool ICollection<KeyValuePair<String, StringValues>>.IsReadOnly =>
        throw new NotImplementedException();

    ICollection<String> IDictionary<String, StringValues>.Keys =>
        throw new NotImplementedException();

    ICollection<StringValues> IDictionary<String, StringValues>.Values =>
        throw new NotImplementedException();

    Nullable<long> IHeaderDictionary.ContentLength
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    StringValues IDictionary<String, StringValues>.this[String key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    StringValues IHeaderDictionary.this[String key]
    {
        get => this._itemFunc(key);
        set => this._itemAction(key, value);
    }

    internal MockHeaderDictionary()
    {
        this._itemAction = (_, _) => throw new NotImplementedException();
        this._itemFunc = _ => throw new NotImplementedException();
    }

    void ICollection<KeyValuePair<String, StringValues>>.Add(KeyValuePair<String, StringValues> item) =>
        throw new NotImplementedException();

    void ICollection<KeyValuePair<String, StringValues>>.Clear() =>
        throw new NotImplementedException();

    bool ICollection<KeyValuePair<String, StringValues>>.Contains(KeyValuePair<String, StringValues> item) =>
        throw new NotImplementedException();

    void ICollection<KeyValuePair<String, StringValues>>.CopyTo(
        KeyValuePair<String, StringValues>[] array,
        int arrayIndex
    ) =>
        throw new NotImplementedException();

    bool ICollection<KeyValuePair<String, StringValues>>.Remove(KeyValuePair<String, StringValues> item) =>
        throw new NotImplementedException();

    void IDictionary<String, StringValues>.Add(String key, StringValues value) =>
        throw new NotImplementedException();

    bool IDictionary<String, StringValues>.ContainsKey(String key) =>
        throw new NotImplementedException();

    bool IDictionary<String, StringValues>.Remove(String key) =>
        throw new NotImplementedException();

    bool IDictionary<String, StringValues>.TryGetValue(
        String key,
        out StringValues value
    ) =>
        throw new NotImplementedException();

    IEnumerator IEnumerable.GetEnumerator() =>
        throw new NotImplementedException();

    IEnumerator<KeyValuePair<String, StringValues>> IEnumerable<KeyValuePair<String, StringValues>>.GetEnumerator() =>
        throw new NotImplementedException();
}
