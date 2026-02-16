using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Shipstone.AspNetCore.Http;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Http;

public sealed class NcsaCommonLoggingMiddlewareTest
{
    private readonly MockMiddlewareFactory _middlewareFactory;
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _provider;
    private readonly MockTextWriter _writer;

    public NcsaCommonLoggingMiddlewareTest()
    {
        LinkedList<Func<RequestDelegate, RequestDelegate>> middleware = new();
        MockApplicationBuilder app = new();

        app._useFunc = m =>
        {
            middleware.AddFirst(m);
            return app;
        };

        app.UseNcsaCommonLogging();
        RequestDelegate next = _ => Task.CompletedTask;

        foreach (Func<RequestDelegate, RequestDelegate> item in middleware)
        {
            next = item(next);
        }

        ICollection<ServiceDescriptor> collection =
            new List<ServiceDescriptor>();

        MockServiceCollection services = new();
        MockTextWriter writer = new();
        services._addAction = collection.Add;
        services._getEnumeratorFunc = collection.GetEnumerator;
        services.AddNcsaCommonLogging(writer);
        MockMiddlewareFactory middlewareFactory = new();
        services.AddSingleton<IMiddlewareFactory>(middlewareFactory);
        this._middlewareFactory = middlewareFactory;
        this._next = next;
        this._provider = new MockServiceProvider(services);
        this._writer = writer;
    }

    [InlineData(null, "127.0.0.1 - johndoe2025 [] \"POST /index.html HTTP/2.0\" 500 12345")]
    [InlineData("?queryKey=queryValue", "127.0.0.1 - johndoe2025 [] \"POST /index.html?queryKey=queryValue HTTP/2.0\" 500 12345")]
    [Theory]
    public async Task TestInvokeAsync(
        String? queryString,
        String resultExpected
    )
    {
#region Arrange
        // Arrange
        const String HOST = "127.0.0.1";
        IPAddress host = IPAddress.Parse(HOST);
        ICollection<char> characters = new List<char>();
        MockHttpContext context = new();
        context._requestServicesFunc = () => this._provider;

        this._middlewareFactory._createFunc = t =>
            this._provider.GetService(t) as IMiddleware;

        this._middlewareFactory._releaseAction = _ => { };

        context._responseFunc = () =>
        {
            MockHttpResponse response = new();

            response._bodyFunc = () =>
            {
                MockStream stream = new();
                stream._lengthFunc = () => 12345;
                return stream;
            };

            response._bodyAction = _ => { };
            response._statusCodeFunc = () => 500;
            return response;
        };

        context._requestFunc = () =>
        {
            MockHttpRequest request = new();
            request._methodFunc = () => "POST";
            request._pathFunc = () => "/index.html";
            request._queryStringFunc = () => new(queryString);
            request._protocolFunc = () => "HTTP/2.0";
            return request;
        };

        context._connectionFunc = () =>
        {
            MockConnectionInfo connection = new();
            connection._remoteIpAddressFunc = () => host;
            return connection;
        };

        context._userFunc = () =>
        {
            MockClaimsPrincipal user = new();

            user._identityFunc = () =>
            {
                MockIdentity identity = new();
                identity._nameFunc = () => "johndoe2025";
                return identity;
            };

            return user;
        };

        this._writer._writeLineAction = b =>
        {
            foreach (char c in b.Span)
            {
                characters.Add(c);
            }
        };

        this._writer._flushAction = () => { };
#endregion

        // Act
        await this._next(context);

        // Assert
        char[] array = new char[characters.Count];
        characters.CopyTo(array, 0);
        String resultActual = new(array);
        int dateStart = resultActual.IndexOf('[') + 1;
        int dateEnd = resultActual.IndexOf(']');
        String dateString = resultActual[dateStart..dateEnd];
        StringBuilder resultExpectedBuilder = new();
        resultExpectedBuilder.Append(resultExpected[0..dateStart]);
        resultExpectedBuilder.Append(dateString);
        resultExpectedBuilder.Append(resultExpected[dateStart..]);
        resultExpected = resultExpectedBuilder.ToString();
        Assert.Equal(resultExpected, resultActual);
    }
}
