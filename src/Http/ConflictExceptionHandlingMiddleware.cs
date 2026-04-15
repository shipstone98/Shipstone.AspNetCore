using Shipstone.Utilities;

namespace Shipstone.AspNetCore.Http;

internal sealed class ConflictExceptionHandlingMiddleware
    : ExceptionHandlingMiddleware<ConflictException>
{
    internal ConflictExceptionHandlingMiddleware(int statusCode)
        : base(statusCode)
    { }
}
