using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Xunit;

using Shipstone.AspNetCore.Authentication.Basic;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.AspNetCoreTest.Stubs;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Authentication.Basic;

public sealed class BasicAuthenticationHandlerTest
{
    [Fact]
    public void TestConstructor_Invalid()
    {
        // Arrange
        IOptionsMonitor<BasicAuthenticationOptions> options =
            new MockOptionsMonitor<BasicAuthenticationOptions>();

        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();
        logger._createLoggerFunc = _ => new MockLogger();

        // Act
        ArgumentException ex =
            Assert.Throws<ArgumentNullException>(() =>
                new BasicAuthenticationHandler(
                    options,
                    logger,
                    encoder,
                    null!
                ));

        // Assert
        Assert.Equal("handler", ex.ParamName);
    }

    [Fact]
    public void TestConstructor_Valid()
    {
        // Arrange
        IOptionsMonitor<BasicAuthenticationOptions> options =
            new MockOptionsMonitor<BasicAuthenticationOptions>();

        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();

        IBasicAuthenticateHandler authenticateHandler =
            new MockBasicAuthenticateHandler();

        logger._createLoggerFunc = _ => new MockLogger();

        // Act
        BasicAuthenticationHandler handler =
            new(options, logger, encoder, authenticateHandler);

        // Assert
        Assert.Same(authenticateHandler, handler.Handler);
    }

#region HandleAuthenticateAsync method
    [Fact]
    public async Task TestHandleAuthenticateAsync_Failure()
    {
#region Arrange
        // Arrange
        const String SCHEME = "My authentication scheme";
        Exception ex = new BasicAuthenticateException();
        MockOptionsMonitor<BasicAuthenticationOptions> options = new();
        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();
        MockBasicAuthenticateHandler authenticateHandler = new();
        logger._createLoggerFunc = _ => new MockLogger();

        StubBasicAuthenticationHandler handler =
            new(options, logger, encoder, authenticateHandler);

        AuthenticationScheme scheme =
            new MockAuthenticationScheme(
                SCHEME,
                null,
                typeof (BasicAuthenticationHandler)
            );

        MockHttpContext context = new();
        options._getFunc = _ => new();
        await handler.InitializeAsync(scheme, context);

        context._requestFunc = () =>
        {
            MockHttpRequest request = new();

            request._headersFunc = () =>
            {
                MockHeaderDictionary headers = new();

                headers._itemFunc = k =>
                    $"{SCHEME} am9obi5kb2VAY29udG9zby5jb206UEBzc3cwcmQ=";

                return headers;
            };

            return request;
        };

        authenticateHandler._handleFunc = (_, _) => throw ex;
#endregion

        // Act
        AuthenticateResult result = await handler.HandleAuthenticateAsync();

        // Assert
        Assert.Same(ex, result.Failure);
        Assert.False(result.None);
        Assert.Null(result.Principal);
        Assert.False(result.Succeeded);
        Assert.Null(result.Ticket);
    }

    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Not my authentication scheme ")]
    [InlineData("My authentication scheme abc123")]
    [InlineData("My authentication scheme am9obi5kb2VAY29udG9zby5jb20=")]
    [InlineData("My authentication scheme am9obi5kb2VAY29udG9zby5jb206")]
    [Theory]
    public async Task TestHandleAuthenticateAsync_NoResult(String? authorization)
    {
#region Arrange
        // Arrange
        MockOptionsMonitor<BasicAuthenticationOptions> options = new();
        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();
        MockBasicAuthenticateHandler authenticateHandler = new();
        logger._createLoggerFunc = _ => new MockLogger();

        StubBasicAuthenticationHandler handler =
            new(options, logger, encoder, authenticateHandler);

        AuthenticationScheme scheme =
            new MockAuthenticationScheme(
                "My authentication scheme",
                null,
                typeof (BasicAuthenticationHandler)
            );

        MockHttpContext context = new();
        options._getFunc = _ => new();
        await handler.InitializeAsync(scheme, context);

        context._requestFunc = () =>
        {
            MockHttpRequest request = new();

            request._headersFunc = () =>
            {
                MockHeaderDictionary headers = new();
                headers._itemFunc = k => authorization;
                return headers;
            };

            return request;
        };
#endregion

        // Act
        AuthenticateResult result = await handler.HandleAuthenticateAsync();

        // Assert
        Assert.Null(result.Failure);
        Assert.True(result.None);
        Assert.Null(result.Principal);
        Assert.False(result.Succeeded);
        Assert.Null(result.Ticket);
    }

    [Fact]
    public async Task TestHandleAuthenticateAsync_Success()
    {
#region Arrange
        // Arrange
        const String SCHEME = "My authentication scheme";
        const String EMAIL_ADDRESS = "john.doe@contoso.com";

        IEnumerable<Claim> claims = new Claim[]
        {
            new(ClaimTypes.Email, EMAIL_ADDRESS),
            new(ClaimTypes.Name, EMAIL_ADDRESS)
        };

        MockOptionsMonitor<BasicAuthenticationOptions> options = new();
        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();
        MockBasicAuthenticateHandler authenticateHandler = new();
        logger._createLoggerFunc = _ => new MockLogger();

        StubBasicAuthenticationHandler handler =
            new(options, logger, encoder, authenticateHandler);

        AuthenticationScheme scheme =
            new MockAuthenticationScheme(
                SCHEME,
                null,
                typeof (BasicAuthenticationHandler)
            );

        MockHttpContext context = new();
        options._getFunc = _ => new();
        await handler.InitializeAsync(scheme, context);

        context._requestFunc = () =>
        {
            MockHttpRequest request = new();

            request._headersFunc = () =>
            {
                MockHeaderDictionary headers = new();

                headers._itemFunc = k =>
                    $"{SCHEME} am9obi5kb2VAY29udG9zby5jb206UEBzc3cwcmQ=";

                return headers;
            };

            return request;
        };

        authenticateHandler._handleFunc = (_, _) => claims;
#endregion

        // Act
        AuthenticateResult result = await handler.HandleAuthenticateAsync();

        // Assert
        Assert.Null(result.Failure);
        Assert.False(result.None);
        Assert.Same(result.Ticket!.Principal, result.Principal);
        Assert.True(result.Succeeded);
        Assert.Equal(SCHEME, result.Ticket.AuthenticationScheme);

        Assert.Equal(
            EMAIL_ADDRESS,
            result.Ticket.Principal.Claims
                .First(c => c.Type.Equals(ClaimTypes.Email))
                .Value
        );

        Assert.Equal(
            EMAIL_ADDRESS,
            result.Ticket.Principal.Claims
                .First(c => c.Type.Equals(ClaimTypes.Name))
                .Value
        );

        Assert.True(result.Ticket.Principal.Identity!.IsAuthenticated);
        Assert.Equal(EMAIL_ADDRESS, result.Ticket.Principal.Identity.Name);
    }
#endregion

    [Fact]
    public async Task TestHandleChallengeAsync()
    {
#region Arrange
        // Arrange
        const String SCHEME = "My authentication scheme";
        Nullable<int> statusCode = null;

        IDictionary<String, StringValues> headerDictionary =
            new Dictionary<String, StringValues>();

        MockOptionsMonitor<BasicAuthenticationOptions> options = new();
        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();

        IBasicAuthenticateHandler authenticateHandler =
            new MockBasicAuthenticateHandler();

        logger._createLoggerFunc = _ => new MockLogger();

        StubBasicAuthenticationHandler handler =
            new(options, logger, encoder, authenticateHandler);

        AuthenticationScheme scheme =
            new MockAuthenticationScheme(
                SCHEME,
                null,
                typeof (BasicAuthenticationHandler)
            );

        MockHttpContext context = new();
        options._getFunc = _ => new();
        await handler.InitializeAsync(scheme, context);

        context._responseFunc = () =>
        {
            MockHttpResponse response = new();
            response._statusCodeAction = sc => statusCode = sc;

            response._headersFunc = () =>
            {
                MockHeaderDictionary headers = new();
                headers._itemAction = headerDictionary.Add;
                return headers;
            };

            return response;
        };
#endregion

        // Act
        await handler.HandleChallengeAsync(new AuthenticationProperties { });

        // Assert
        Assert.Equal(StatusCodes.Status401Unauthorized, statusCode);
        Assert.Equal(SCHEME, headerDictionary["WWW-Authenticate"]);
    }

    [Fact]
    public async Task TestHandleForbiddenAsync()
    {
#region Arrange
        // Arrange
        Nullable<int> statusCode = null;
        MockOptionsMonitor<BasicAuthenticationOptions> options = new();
        MockLoggerFactory logger = new();
        UrlEncoder encoder = new MockUrlEncoder();

        IBasicAuthenticateHandler authenticateHandler =
            new MockBasicAuthenticateHandler();

        logger._createLoggerFunc = _ => new MockLogger();

        StubBasicAuthenticationHandler handler =
            new(options, logger, encoder, authenticateHandler);

        AuthenticationScheme scheme =
            new MockAuthenticationScheme(
                "My authentication scheme",
                null,
                typeof (BasicAuthenticationHandler)
            );

        MockHttpContext context = new();
        options._getFunc = _ => new();
        await handler.InitializeAsync(scheme, context);

        context._responseFunc = () =>
        {
            MockHttpResponse response = new();
            response._statusCodeAction = sc => statusCode = sc;
            return response;
        };
#endregion

        // Act
        await handler.HandleForbiddenAsync(new AuthenticationProperties { });

        // Assert
        Assert.Equal(StatusCodes.Status403Forbidden, statusCode);
    }
}
