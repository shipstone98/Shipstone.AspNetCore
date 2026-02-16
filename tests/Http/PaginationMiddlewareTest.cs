using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

using Shipstone.AspNetCore.Http;
using Shipstone.Extensions.Pagination;

using Shipstone.AspNetCoreTest.Mocks;
using Shipstone.Test.Mocks;

namespace Shipstone.AspNetCoreTest.Http;

public sealed class PaginationMiddlewareTest
{
    [InlineData(null, null, 0, 10)]
    [InlineData(null, "abc", 0, 10)]
    [InlineData(null, "-2,147,483,648", 0, 10)]
    [InlineData(null, "-2147483648", 0, 10)]
    [InlineData(null, "-1", 0, 10)]
    [InlineData(null, "0", 0, 10)]
    [InlineData(null, "1", 0, 1)]
    [InlineData(null, "2,147,483,647", 0, 10)]
    [InlineData(null, "2147483647", 0, Int32.MaxValue)]
    [InlineData("abc", null, 0, 10)]
    [InlineData("abc", "abc", 0, 10)]
    [InlineData("abc", "-2,147,483,648", 0, 10)]
    [InlineData("abc", "-2147483648", 0, 10)]
    [InlineData("abc", "-1", 0, 10)]
    [InlineData("abc", "0", 0, 10)]
    [InlineData("abc", "1", 0, 1)]
    [InlineData("abc", "2,147,483,647", 0, 10)]
    [InlineData("abc", "2147483647", 0, Int32.MaxValue)]
    [InlineData("-2,147,483,648", null, 0, 10)]
    [InlineData("-2,147,483,648", "abc", 0, 10)]
    [InlineData("-2,147,483,648", "-2,147,483,648", 0, 10)]
    [InlineData("-2,147,483,648", "-2147483648", 0, 10)]
    [InlineData("-2,147,483,648", "-1", 0, 10)]
    [InlineData("-2,147,483,648", "0", 0, 10)]
    [InlineData("-2,147,483,648", "1", 0, 1)]
    [InlineData("-2,147,483,648", "2,147,483,647", 0, 10)]
    [InlineData("-2,147,483,648", "2147483647", 0, Int32.MaxValue)]
    [InlineData("-2147483648", null, 0, 10)]
    [InlineData("-2147483648", "abc", 0, 10)]
    [InlineData("-2147483648", "-2,147,483,648", 0, 10)]
    [InlineData("-2147483648", "-2147483648", 0, 10)]
    [InlineData("-2147483648", "-1", 0, 10)]
    [InlineData("-2147483648", "0", 0, 10)]
    [InlineData("-2147483648", "1", 0, 1)]
    [InlineData("-2147483648", "2,147,483,647", 0, 10)]
    [InlineData("-2147483648", "2147483647", 0, Int32.MaxValue)]
    [InlineData("-1", null, 0, 10)]
    [InlineData("-1", "abc", 0, 10)]
    [InlineData("-1", "-2,147,483,648", 0, 10)]
    [InlineData("-1", "-2147483648", 0, 10)]
    [InlineData("-1", "-1", 0, 10)]
    [InlineData("-1", "0", 0, 10)]
    [InlineData("-1", "1", 0, 1)]
    [InlineData("-1", "2,147,483,647", 0, 10)]
    [InlineData("-1", "2147483647", 0, Int32.MaxValue)]
    [InlineData("0", null, 0, 10)]
    [InlineData("0", "abc", 0, 10)]
    [InlineData("0", "-2,147,483,648", 0, 10)]
    [InlineData("0", "-2147483648", 0, 10)]
    [InlineData("0", "-1", 0, 10)]
    [InlineData("0", "0", 0, 10)]
    [InlineData("0", "1", 0, 1)]
    [InlineData("0", "2,147,483,647", 0, 10)]
    [InlineData("0", "2147483647", 0, Int32.MaxValue)]
    [InlineData("1", null, 0, 10)]
    [InlineData("1", "abc", 0, 10)]
    [InlineData("1", "-2,147,483,648", 0, 10)]
    [InlineData("1", "-2147483648", 0, 10)]
    [InlineData("1", "-1", 0, 10)]
    [InlineData("1", "0", 0, 10)]
    [InlineData("1", "1", 0, 1)]
    [InlineData("1", "2,147,483,647", 0, 10)]
    [InlineData("1", "2147483647", 0, Int32.MaxValue)]
    [InlineData("2", null, 1, 10)]
    [InlineData("2", "abc", 1, 10)]
    [InlineData("2", "-2,147,483,648", 1, 10)]
    [InlineData("2", "-2147483648", 1, 10)]
    [InlineData("2", "-1", 1, 10)]
    [InlineData("2", "0", 1, 10)]
    [InlineData("2", "1", 1, 1)]
    [InlineData("2", "2,147,483,647", 1, 10)]
    [InlineData("2", "2147483647", 1, Int32.MaxValue)]
    [InlineData("2,147,483,647", null, 0, 10)]
    [InlineData("2,147,483,647", "abc", 0, 10)]
    [InlineData("2,147,483,647", "-2,147,483,648", 0, 10)]
    [InlineData("2,147,483,647", "-2147483648", 0, 10)]
    [InlineData("2,147,483,647", "-1", 0, 10)]
    [InlineData("2,147,483,647", "0", 0, 10)]
    [InlineData("2,147,483,647", "1", 0, 1)]
    [InlineData("2,147,483,647", "2,147,483,647", 0, 10)]
    [InlineData("2,147,483,647", "2147483647", 0, Int32.MaxValue)]
    [InlineData("2147483647", null, Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "abc", Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "-2,147,483,648", Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "-2147483648", Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "-1", Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "0", Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "1", Int32.MaxValue - 1, 1)]
    [InlineData("2147483647", "2,147,483,647", Int32.MaxValue - 1, 10)]
    [InlineData("2147483647", "2147483647", Int32.MaxValue - 1, Int32.MaxValue)]
    [InlineData("2,147,483,648", null, 0, 10)]
    [InlineData("2,147,483,648", "abc", 0, 10)]
    [InlineData("2,147,483,648", "-2,147,483,648", 0, 10)]
    [InlineData("2,147,483,648", "-2147483648", 0, 10)]
    [InlineData("2,147,483,648", "-1", 0, 10)]
    [InlineData("2,147,483,648", "0", 0, 10)]
    [InlineData("2,147,483,648", "1", 0, 1)]
    [InlineData("2,147,483,648", "2,147,483,647", 0, 10)]
    [InlineData("2,147,483,648", "2147483647", 0, Int32.MaxValue)]
    [InlineData("2147483648", null, Int32.MaxValue, 10)]
    [InlineData("2147483648", "abc", Int32.MaxValue, 10)]
    [InlineData("2147483648", "-2,147,483,648", Int32.MaxValue, 10)]
    [InlineData("2147483648", "-2147483648", Int32.MaxValue, 10)]
    [InlineData("2147483648", "-1", Int32.MaxValue, 10)]
    [InlineData("2147483648", "0", Int32.MaxValue, 10)]
    [InlineData("2147483648", "1", Int32.MaxValue, 1)]
    [InlineData("2147483648", "2,147,483,647", Int32.MaxValue, 10)]
    [InlineData("2147483648", "2147483647", Int32.MaxValue, Int32.MaxValue)]
    [Theory]
    public async Task TestInvokeAsync(
        String? indexString,
        String? countString,
        int pageIndex,
        int maxCount
    )
    {
#region Arrange
        // Arrange
        LinkedList<Func<RequestDelegate, RequestDelegate>> middleware = new();
        MockApplicationBuilder app = new();

        app._useFunc = m =>
        {
            middleware.AddFirst(m);
            return app;
        };

        app.UsePagination();
        MockHttpContext context = new();
        RequestDelegate next = _ => Task.CompletedTask;

        foreach (Func<RequestDelegate, RequestDelegate> item in middleware)
        {
            next = item(next);
        }

        IList<ServiceDescriptor> list = new List<ServiceDescriptor>();
        MockServiceCollection services = new();
        services._addAction = list.Add;
        services._countFunc = () => list.Count;
        services._itemFunc = i => list[i];
        services._getEnumeratorFunc = list.GetEnumerator;
        services.AddPagination();
        services.AddPaginationExtensions();
        MockMiddlewareFactory middlewareFactory = new();
        services.AddSingleton<IMiddlewareFactory>(middlewareFactory);
        IServiceProvider provider = new MockServiceProvider(services);

        middlewareFactory._createFunc = t =>
            provider.GetService(t) as IMiddleware;

        middlewareFactory._releaseAction = _ => { };
        context._requestServicesFunc = () => provider;

        context._requestFunc = () =>
        {
            MockHttpRequest request = new();

            request._queryFunc = () =>
            {
                MockQueryCollection query = new();

                query._tryGetValueFunc = k =>
                {
                    if (k.Equals("index"))
                    {
                        return (true, indexString);
                    }

                    if (k.Equals("count"))
                    {
                        return (true, countString);
                    }

                    return (false, null as String);
                };

                return query;
            };

            return request;
        };
#endregion

        // Act
        await next(context);

        // Assert
        IOptionsSnapshot<PaginationOptions> options =
            provider.GetRequiredService<IOptionsSnapshot<PaginationOptions>>();

        Assert.Equal(maxCount, options.Value.MaxCount);
        Assert.Equal(pageIndex, options.Value.PageIndex);
    }
}
