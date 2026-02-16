using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Xunit;

using Shipstone.AspNetCore.Http;

using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Http;

public sealed class HttpApplicationBuilderExtensionsTest
{
    [Fact]
    public void TestUseArgumentExceptionHandling_Invalid()
    {
        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                HttpApplicationBuilderExtensions.UseArgumentExceptionHandling(null!));

        // Assert
        Assert.Equal("app", ex.ParamName);
    }

    [Fact]
    public void TestUseArgumentExceptionHandling_Valid()
    {
        // Arrange
        ICollection<Object> middleware = new List<Object>();
        MockApplicationBuilder app = new();

        app._useFunc = m =>
        {
            middleware.Add(m);
            return app;
        };

        // Act
        IApplicationBuilder result =
            HttpApplicationBuilderExtensions.UseArgumentExceptionHandling(app);

        // Assert
        Assert.Same(app, result);
        Assert.NotEmpty(middleware);
    }

    [Fact]
    public void TestUseNcsaCommonLogging_Invalid()
    {
        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                HttpApplicationBuilderExtensions.UseNcsaCommonLogging(null!));

        // Assert
        Assert.Equal("app", ex.ParamName);
    }

    [Fact]
    public void TestUseNcsaCommonLogging_Valid()
    {
        // Arrange
        ICollection<Object> middleware = new List<Object>();
        MockApplicationBuilder app = new();

        app._useFunc = m =>
        {
            middleware.Add(m);
            return app;
        };

        // Act
        IApplicationBuilder result =
            HttpApplicationBuilderExtensions.UseNcsaCommonLogging(app);

        // Assert
        Assert.Same(app, result);
        Assert.NotEmpty(middleware);
    }

    [Fact]
    public void TestUsePagination_Invalid()
    {
        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                HttpApplicationBuilderExtensions.UsePagination(null!));

        // Assert
        Assert.Equal("app", ex.ParamName);
    }

    [Fact]
    public void TestUsePagination_Valid()
    {
        // Arrange
        ICollection<Object> middleware = new List<Object>();
        MockApplicationBuilder app = new();

        app._useFunc = m =>
        {
            middleware.Add(m);
            return app;
        };

        // Act
        IApplicationBuilder result =
            HttpApplicationBuilderExtensions.UsePagination(app);

        // Assert
        Assert.Same(app, result);
        Assert.NotEmpty(middleware);
    }
}
