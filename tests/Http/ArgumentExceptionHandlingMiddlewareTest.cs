using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Shipstone.AspNetCore.Http;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Http;

public sealed class ArgumentExceptionHandlingMiddlewareTest
{
    private readonly MockHttpContext _context;
    private readonly IEnumerable<Func<RequestDelegate, RequestDelegate>> _middleware;

    public ArgumentExceptionHandlingMiddlewareTest()
    {
        LinkedList<Func<RequestDelegate, RequestDelegate>> middleware = new();
        MockApplicationBuilder app = new();

        app._useFunc = m =>
        {
            middleware.AddFirst(m);
            return app;
        };

        app.UseArgumentExceptionHandling();
        MockHttpContext context = new();

        context._requestServicesFunc = () =>
        {
            ICollection<ServiceDescriptor> collection =
                new List<ServiceDescriptor>();

            MockServiceCollection services = new();
            services._addAction = collection.Add;
            services._getEnumeratorFunc = collection.GetEnumerator;
            services.AddArgumentExceptionHandling();
            MockMiddlewareFactory middlewareFactory = new();
            services.AddSingleton<IMiddlewareFactory>(middlewareFactory);
            IServiceProvider provider = new MockServiceProvider(services);

            middlewareFactory._createFunc = t =>
                provider.GetService(t) as IMiddleware;

            middlewareFactory._releaseAction = _ => { };
            return provider;
        };

        this._context = context;
        this._middleware = middleware;
    }

#region InvokeAsync method
    [Fact]
    public Task TestInvokeAsync_NotThrown()
    {
        // Arrange
        RequestDelegate next = _ => Task.CompletedTask;

        foreach (Func<RequestDelegate, RequestDelegate> item in this._middleware)
        {
            next = item(next);
        }

        // Act
        return next(this._context);

        // Nothing to assert
    }

    [Fact]
    public async Task TestInvokeAsync_Thrown_NotStarted()
    {
        // Arrange
        RequestDelegate next = _ => throw new ArgumentException();

        foreach (Func<RequestDelegate, RequestDelegate> item in this._middleware)
        {
            next = item(next);
        }

        Nullable<int> statusCode = null;

        this._context._responseFunc = () =>
        {
            MockHttpResponse response = new();
            response._hasStartedFunc = () => false;
            response._statusCodeAction = sc => statusCode = sc;
            return response;
        };

        // Act
        await next(this._context);

        // Assert
        Assert.Equal(400, statusCode);
    }

    [Fact]
    public Task TestInvokeAsync_Thrown_Started()
    {
        // Arrange
        RequestDelegate next = _ => throw new ArgumentException();

        foreach (Func<RequestDelegate, RequestDelegate> item in this._middleware)
        {
            next = item(next);
        }

        this._context._responseFunc = () =>
        {
            MockHttpResponse response = new();
            response._hasStartedFunc = () => true;
            return response;
        };

        // Act
        return next(this._context);

        // Nothing to assert
    }
#endregion
}
