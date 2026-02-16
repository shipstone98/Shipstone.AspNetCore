using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Http;

public sealed class HttpServiceCollectionExtensionsTest
{
    [Fact]
    public void TestAddArgumentExceptionHandling_Invalid()
    {
        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                AspNetCore.Http.HttpServiceCollectionExtensions.AddArgumentExceptionHandling(null!));

        // Assert
        Assert.Equal("services", ex.ParamName);
    }

    [InlineData(Int32.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(Int32.MaxValue)]
    [Theory]
    public void TestAddArgumentExceptionHandling_Valid(int statusCode)
    {
        // Arrange
        ICollection<ServiceDescriptor> collection =
            new List<ServiceDescriptor>();

        MockServiceCollection services = new();
        services._addAction = collection.Add;

        // Act
        IServiceCollection result =
            AspNetCore.Http.HttpServiceCollectionExtensions.AddArgumentExceptionHandling(
                services,
                statusCode
            );

        // Assert
        Assert.Same(services, result);

        IEnumerable<ServiceDescriptor> descriptors =
            collection.Where(s =>
                s.ServiceType.IsAssignableTo(typeof (IMiddleware)));

        Assert.Contains(
            descriptors,
            d => d.Lifetime == ServiceLifetime.Singleton
        );
    }

#region AddNcsaCommonLogging method
    [Fact]
    public void TestAddNcsaCommonLogging_Invalid_ServicesNull()
    {
        // Arrange
        using MockTextWriter writer = new();
        writer._disposeAction = _ => { };

        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                AspNetCore.Http.HttpServiceCollectionExtensions.AddNcsaCommonLogging(
                    null!,
                    writer
                ));

        // Assert
        Assert.Equal("services", ex.ParamName);
    }

    [Fact]
    public void TestAddNcsaCommonLogging_Invalid_WriterNull()
    {
        // Arrange
        IServiceCollection services = new MockServiceCollection();

        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                AspNetCore.Http.HttpServiceCollectionExtensions.AddNcsaCommonLogging(
                    services,
                    null!
                ));

        // Assert
        Assert.Equal("writer", ex.ParamName);
    }

    [Fact]
    public void TestAddNcsaCommonLogging_Valid()
    {
        // Arrange
        ICollection<ServiceDescriptor> collection =
            new List<ServiceDescriptor>();

        MockServiceCollection services = new();
        using MockTextWriter writer = new();
        writer._disposeAction = _ => { };
        services._addAction = collection.Add;

        // Act
        IServiceCollection result =
            AspNetCore.Http.HttpServiceCollectionExtensions.AddNcsaCommonLogging(
                services,
                writer
            );

        // Assert
        Assert.Same(services, result);

        IEnumerable<ServiceDescriptor> descriptors =
            collection.Where(s =>
                s.ServiceType.IsAssignableTo(typeof (IMiddleware)));

        Assert.Contains(
            descriptors,
            d => d.Lifetime == ServiceLifetime.Singleton
        );
    }
#endregion

    [Fact]
    public void TestAddPagination_Invalid()
    {
        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                AspNetCore.Http.HttpServiceCollectionExtensions.AddPagination(null!));

        // Assert
        Assert.Equal("services", ex.ParamName);
    }

    [Fact]
    public void TestAddPagination_Valid()
    {
        // Arrange
        ICollection<ServiceDescriptor> collection =
            new List<ServiceDescriptor>();

        MockServiceCollection services = new();
        services._addAction = collection.Add;

        // Act
        IServiceCollection result =
            AspNetCore.Http.HttpServiceCollectionExtensions.AddPagination(services);

        // Assert
        Assert.Same(services, result);

        IEnumerable<ServiceDescriptor> descriptors =
            collection.Where(s =>
                s.ServiceType.IsAssignableTo(typeof (IMiddleware)));

        Assert.Contains(
            descriptors,
            d => d.Lifetime == ServiceLifetime.Scoped
        );
    }
}
