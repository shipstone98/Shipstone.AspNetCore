using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

using Shipstone.AspNetCore.Http;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Http;

public sealed class ExceptionHandlingMiddlewareTest
{
    [InlineData(Int32.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(Int32.MaxValue)]
    [Theory]
    public void TestConstructor(int statusCode)
    {
        // Act
        _ = new MockExceptionHandlingMiddleware<MockException>(statusCode);

        // Nothing to assert
    }

#region InvokeAsync method
    [Fact]
    public async Task TestInvokeAsync_Invalid_ContextNull()
    {
        // Arrange
        ExceptionHandlingMiddleware<MockException> middleware =
            new MockExceptionHandlingMiddleware<MockException>(500);

        // Act
        ArgumentException ex =
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                middleware.InvokeAsync(
                    null!,
                    _ => throw new NotImplementedException()
                ));

        // Assert
        Assert.Equal("context", ex.ParamName);
    }

    [Fact]
    public async Task TestInvokeAsync_Invalid_NextNull()
    {
        // Arrange
        ExceptionHandlingMiddleware<MockException> middleware =
            new MockExceptionHandlingMiddleware<MockException>(500);

        HttpContext context = new MockHttpContext();

        // Act
        ArgumentException ex =
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                middleware.InvokeAsync(context, null!));

        // Assert
        Assert.Equal("next", ex.ParamName);
    }

#region Valid arguments
    [Fact]
    public Task TestInvokeAsync_Valid_NotThrown()
    {
        // Arrange
        ExceptionHandlingMiddleware<MockException> middleware =
            new MockExceptionHandlingMiddleware<MockException>(500);

        HttpContext context = new MockHttpContext();
        RequestDelegate next = _ => Task.CompletedTask;

        // Act
        return middleware.InvokeAsync(context, next);

        // Nothing to assert
    }

    [Fact]
    public async Task TestInvokeAsync_Valid_Thrown_NotStarted()
    {
        // Arrange
        const int STATUS_CODE = 500;

        ExceptionHandlingMiddleware<MockException> middleware =
            new MockExceptionHandlingMiddleware<MockException>(STATUS_CODE);

        MockHttpContext context = new();
        RequestDelegate next = _ => throw new MockException();
        Nullable<int> statusCode = null;

        context._responseFunc = () =>
        {
            MockHttpResponse response = new();
            response._hasStartedFunc = () => false;
            response._statusCodeAction = sc => statusCode = sc;
            return response;
        };

        // Act
        await middleware.InvokeAsync(context, next);

        // Assert
        Assert.Equal(STATUS_CODE, statusCode);
    }

    [Fact]
    public Task TestInvokeAsync_Valid_Thrown_Started()
    {
        // Arrange
        ExceptionHandlingMiddleware<MockException> middleware =
            new MockExceptionHandlingMiddleware<MockException>(500);

        MockHttpContext context = new();
        RequestDelegate next = _ => throw new MockException();

        context._responseFunc = () =>
        {
            MockHttpResponse response = new();
            response._hasStartedFunc = () => true;
            return response;
        };

        // Act
        return middleware.InvokeAsync(context, next);

        // Nothing to assert
    }
#endregion
#endregion
}
