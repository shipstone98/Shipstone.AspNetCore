using System;

using Shipstone.AspNetCore.Http;

namespace Shipstone.AspNetCoreTest.Mocks;

internal sealed class MockExceptionHandlingMiddleware<TException>
    : ExceptionHandlingMiddleware<TException>
    where TException : notnull, Exception
{
    internal MockExceptionHandlingMiddleware(int statusCode) : base(statusCode)
    { }
}
