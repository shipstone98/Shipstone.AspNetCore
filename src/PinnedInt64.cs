using System;

namespace Shipstone.AspNetCore;

internal readonly struct PinnedInt64
{
    private readonly long _value;

    internal PinnedInt64(long val) => this._value = val;

    internal readonly PinnedInt64 Add(long val)
    {
        try
        {
            checked
            {
                return new(this._value + val);
            }
        }

        catch
        {
            return new(Int64.MaxValue);
        }
    }

    public static implicit operator long(PinnedInt64 i) => i._value;
}
