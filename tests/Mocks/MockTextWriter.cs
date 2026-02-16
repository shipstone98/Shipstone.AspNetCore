using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockTextWriter : TextWriter
{
    internal Action<bool> _disposeAction;
    internal Action _flushAction;
    internal Action<ReadOnlyMemory<char>> _writeLineAction;

    public sealed override Encoding Encoding =>
        throw new NotImplementedException();

    public sealed override IFormatProvider FormatProvider =>
        throw new NotImplementedException();

    public sealed override String NewLine
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    internal MockTextWriter()
    {
        this._disposeAction = _ => throw new NotImplementedException();
        this._flushAction = () => throw new NotImplementedException();
        this._writeLineAction = _ => throw new NotImplementedException();
    }

    public sealed override void Close() => throw new NotImplementedException();

    protected sealed override void Dispose(bool disposing) =>
        this._disposeAction(disposing);

    public sealed override ValueTask DisposeAsync() =>
        throw new NotImplementedException();

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override void Flush() => throw new NotImplementedException();

    public sealed override Task FlushAsync()
    {
        this._flushAction();
        return Task.CompletedTask;
    }

    public sealed override Task FlushAsync(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    [Obsolete]
    public sealed override Object InitializeLifetimeService() =>
        throw new NotImplementedException();

    public sealed override String ToString() =>
        throw new NotImplementedException();

    public sealed override void Write(bool value) =>
        throw new NotImplementedException();

    public sealed override void Write(char value) =>
        throw new NotImplementedException();

    public sealed override void Write(double value) =>
        throw new NotImplementedException();

    public sealed override void Write(float value) =>
        throw new NotImplementedException();

    public sealed override void Write(int value) =>
        throw new NotImplementedException();

    public sealed override void Write(long value) =>
        throw new NotImplementedException();

    public sealed override void Write(uint value) =>
        throw new NotImplementedException();

    public sealed override void Write(ulong value) =>
        throw new NotImplementedException();

    public sealed override void Write(char[] buffer, int index, int count) =>
        throw new NotImplementedException();

    public sealed override void Write(char[]? buffer) =>
        throw new NotImplementedException();

    public sealed override void Write(Decimal value) =>
        throw new NotImplementedException();

    public sealed override void Write(Object? value) =>
        throw new NotImplementedException();

    public sealed override void Write(ReadOnlySpan<char> buffer) =>
        throw new NotImplementedException();

    public sealed override void Write(
        [StringSyntax("CompositeFormat")] String format,
        Object? arg0
    ) =>
        throw new NotImplementedException();

    public sealed override void Write(
        [StringSyntax("CompositeFormat")]
        String format,
        Object? arg0,
        Object? arg1
    ) =>
        throw new NotImplementedException();

    public sealed override void Write(
        [StringSyntax("CompositeFormat")] String format,
        Object? arg0,
        Object? arg1,
        Object? arg2
    ) =>
        throw new NotImplementedException();

    public sealed override void Write(
        [StringSyntax("CompositeFormat")] String format,
        params Object?[] arg
    ) =>
        throw new NotImplementedException();

    public sealed override void Write(
        [StringSyntax("CompositeFormat")] String format,
        scoped ReadOnlySpan<Object?> arg
    ) =>
        throw new NotImplementedException();

    public sealed override void Write(String? value) =>
        throw new NotImplementedException();

    public sealed override void Write(StringBuilder? value) =>
        throw new NotImplementedException();

    public sealed override Task WriteAsync(char value) =>
        throw new NotImplementedException();

    public sealed override Task WriteAsync(
        char[] buffer,
        int index,
        int count
    ) =>
        throw new NotImplementedException();

    public sealed override Task WriteAsync(
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default
    ) =>
        throw new NotImplementedException();

    public sealed override Task WriteAsync(String? value) =>
        throw new NotImplementedException();

    public sealed override Task WriteAsync(
        StringBuilder? value,
        CancellationToken cancellationToken = default
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine() =>
        throw new NotImplementedException();

    public sealed override void WriteLine(bool value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(char value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(double value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(float value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(int value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(long value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(uint value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(ulong value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(
        char[] buffer,
        int index,
        int count
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(char[]? buffer) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(Decimal value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(Object? value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(ReadOnlySpan<char> buffer) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(
        [StringSyntax("CompositeFormat")] String format,
        Object? arg0
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(
        [StringSyntax("CompositeFormat")] String format,
        Object? arg0,
        Object? arg1
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(
        [StringSyntax("CompositeFormat")] String format,
        Object? arg0,
        Object? arg1,
        Object? arg2
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(
        [StringSyntax("CompositeFormat")] String format,
        params Object?[] arg
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(
        [StringSyntax("CompositeFormat")] String format,
        scoped ReadOnlySpan<Object?> arg
    ) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(String? value) =>
        throw new NotImplementedException();

    public sealed override void WriteLine(StringBuilder? value) =>
        throw new NotImplementedException();

    public sealed override Task WriteLineAsync() =>
        throw new NotImplementedException();

    public sealed override Task WriteLineAsync(char value) =>
        throw new NotImplementedException();

    public sealed override Task WriteLineAsync(
        char[] buffer,
        int index,
        int count
    ) =>
        throw new NotImplementedException();

    public sealed override Task WriteLineAsync(
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default
    )
    {
        this._writeLineAction(buffer);
        return Task.CompletedTask;
    }

    public sealed override Task WriteLineAsync(String? value) =>
        throw new NotImplementedException();

    public sealed override Task WriteLineAsync(
        StringBuilder? value,
        CancellationToken cancellationToken = default
    ) =>
        throw new NotImplementedException();
}
