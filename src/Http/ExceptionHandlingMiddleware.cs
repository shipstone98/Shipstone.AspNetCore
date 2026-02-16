using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shipstone.AspNetCore.Http;

/// <summary>
/// Enables returning a status code when an exception is thrown.
/// </summary>
/// <typeparam name="TException">The type of the exception to catch.</typeparam>
public abstract class ExceptionHandlingMiddleware<TException>
    : IMiddleware
    where TException : notnull, Exception
{
    private readonly int _statusCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware{TException}" /> class with the specified status code.
    /// </summary>
    /// <param name="statusCode">The HTTP status code to return.</param>
    protected ExceptionHandlingMiddleware(int statusCode) =>
        this._statusCode = statusCode;

    /// <inheritdoc />
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);
        return this.InvokeAsyncCore(context, next);
    }

    private async Task InvokeAsyncCore(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }

        catch (TException)
        {
            HttpResponse response = context.Response;

            if (!response.HasStarted)
            {
                response.StatusCode = this._statusCode;
            }
        }
    }
}
