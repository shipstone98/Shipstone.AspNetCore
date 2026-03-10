using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Shipstone.AspNetCore.Authentication.Basic;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Authentication.Basic;

public sealed class AuthenticationBuilderExtensionsTest
{
    [Fact]
    public void TestAddBasic_Invalid()
    {
        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                AuthenticationBuilderExtensions.AddBasic(null!));

        // Assert
        Assert.Equal("builder", ex.ParamName);
    }

    [Fact]
    public void TestAddBasic_Valid()
    {
        // Arrange
        String? scheme = null;
        IServiceCollection services = new MockServiceCollection();
        MockAuthenticationBuilder builder = new(services);

        builder._addSchemeFunc = (s, _) =>
        {
            scheme = s;
            return builder;
        };

        // Act
        AuthenticationBuilder result =
            AuthenticationBuilderExtensions.AddBasic(builder, _ => { });

        // Assert
        Assert.Same(builder, result);
        Assert.Equal(BasicAuthenticationDefaults.AuthenticationScheme, scheme);
    }
}
