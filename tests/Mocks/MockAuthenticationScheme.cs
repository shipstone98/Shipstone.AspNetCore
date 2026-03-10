using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockAuthenticationScheme : AuthenticationScheme
{
    internal MockAuthenticationScheme(
        String name,
        String? displayName,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type handlerType
    )
        : base(name, displayName, handlerType)
    { }

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    public sealed override String? ToString() =>
        throw new NotImplementedException();
}
