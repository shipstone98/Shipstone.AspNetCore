using System;
using System.Buffers;
using System.IO;
using System.Text.Encodings.Web;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockUrlEncoder : UrlEncoder
{
    public sealed override int MaxOutputCharactersPerInputCharacter =>
        throw new NotImplementedException();

    public sealed override void Encode(
        TextWriter output,
        char[] value,
        int startIndex,
        int characterCount
    ) =>
        throw new NotImplementedException();

    public sealed override void Encode(
        TextWriter output,
        String value,
        int startIndex,
        int characterCount
    ) =>
        throw new NotImplementedException();

    public sealed override OperationStatus Encode(
        ReadOnlySpan<char> source,
        Span<char> destination,
        out int charsConsumed,
        out int charsWritten,
        bool isFinalBlock = true
    ) =>
        throw new NotImplementedException();

    public sealed override string Encode(String value) =>
        throw new NotImplementedException();

    public sealed override OperationStatus EncodeUtf8(
        ReadOnlySpan<byte> utf8Source,
        Span<byte> utf8Destination,
        out int bytesConsumed,
        out int bytesWritten,
        bool isFinalBlock = true
    ) =>
        throw new NotImplementedException();

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override unsafe int FindFirstCharacterToEncode(
        char* text,
        int textLength
    ) =>
        throw new NotImplementedException();

    public sealed override int FindFirstCharacterToEncodeUtf8(ReadOnlySpan<byte> utf8Text) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    public sealed override String? ToString() =>
        throw new NotImplementedException();

    public sealed override unsafe bool TryEncodeUnicodeScalar(
        int unicodeScalar,
        char* buffer,
        int bufferLength,
        out int numberOfCharactersWritten
    ) =>
        throw new NotImplementedException();

    public sealed override bool WillEncode(int unicodeScalar) =>
        throw new NotImplementedException();
}
