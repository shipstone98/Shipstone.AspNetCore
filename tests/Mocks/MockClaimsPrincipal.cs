using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockClaimsPrincipal : ClaimsPrincipal
{
    internal Func<IIdentity?> _identityFunc;

    public sealed override IEnumerable<Claim> Claims =>
        throw new NotImplementedException();

    protected sealed override byte[]? CustomSerializationData =>
        throw new NotImplementedException();

    public sealed override IEnumerable<ClaimsIdentity> Identities =>
        throw new NotImplementedException();

    public sealed override IIdentity? Identity => this._identityFunc();

    internal MockClaimsPrincipal() =>
        this._identityFunc = () => throw new NotImplementedException();

    public sealed override void AddIdentities(IEnumerable<ClaimsIdentity> identities) =>
        throw new NotImplementedException();

    public sealed override void AddIdentity(ClaimsIdentity identity) =>
        throw new NotImplementedException();

    public sealed override ClaimsPrincipal Clone() =>
        throw new NotImplementedException();

    protected sealed override ClaimsIdentity CreateClaimsIdentity(BinaryReader reader) =>
        throw new NotImplementedException();

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override IEnumerable<Claim> FindAll(Predicate<Claim> match) =>
        throw new NotImplementedException();

    public sealed override IEnumerable<Claim> FindAll(String type) =>
        throw new NotImplementedException();

    public sealed override Claim? FindFirst(Predicate<Claim> match) =>
        throw new NotImplementedException();

    public sealed override Claim? FindFirst(String type) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    protected sealed override void GetObjectData(
        SerializationInfo info,
        StreamingContext context
    ) =>
        throw new NotImplementedException();

    public sealed override bool HasClaim(Predicate<Claim> match) =>
        throw new NotImplementedException();

    public sealed override bool HasClaim(String type, String value) =>
        throw new NotImplementedException();

    public sealed override bool IsInRole(string role) =>
        throw new NotImplementedException();

    public sealed override String ToString() =>
        throw new NotImplementedException();

    public sealed override void WriteTo(BinaryWriter writer) =>
        throw new NotImplementedException();

    protected sealed override void WriteTo(
        BinaryWriter writer,
        byte[]? userData
    ) =>
        throw new NotImplementedException();
}
