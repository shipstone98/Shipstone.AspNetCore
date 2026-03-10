using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockAuthenticationBuilder : AuthenticationBuilder
{
    internal Func<String, Object?, AuthenticationBuilder> _addSchemeFunc;

    public sealed override IServiceCollection Services =>
        throw new NotImplementedException();

    internal MockAuthenticationBuilder(IServiceCollection services)
        : base(services) =>
            this._addSchemeFunc = (_, _) =>
                throw new NotImplementedException();

    public sealed override AuthenticationBuilder AddPolicyScheme(
        String authenticationScheme,
        String? displayName,
        Action<PolicySchemeOptions> configureOptions
    ) =>
        throw new NotImplementedException();

    public sealed override AuthenticationBuilder AddRemoteScheme<TOptions, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(
        String authenticationScheme,
        String? displayName,
        Action<TOptions>? configureOptions
    ) =>
        throw new NotImplementedException();

    public sealed override AuthenticationBuilder AddScheme<TOptions, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(
        String authenticationScheme,
        Action<TOptions>? configureOptions
    ) =>
        this._addSchemeFunc(authenticationScheme, configureOptions);

    public sealed override AuthenticationBuilder AddScheme<TOptions, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(
        String authenticationScheme,
        String? displayName,
        Action<TOptions>? configureOptions
    ) =>
        throw new NotImplementedException();

    public sealed override bool Equals(Object? obj) =>
        throw new NotImplementedException();

    public sealed override int GetHashCode() =>
        throw new NotImplementedException();

    public sealed override String? ToString() =>
        throw new NotImplementedException();
}
