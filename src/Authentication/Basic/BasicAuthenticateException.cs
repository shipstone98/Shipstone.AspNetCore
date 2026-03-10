using System;

namespace Shipstone.AspNetCore.Authentication.Basic;

/// <summary>
/// Represents the exception that is thrown when the current user can not be authenticated.
/// </summary>
public class BasicAuthenticateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAuthenticateException" /> class.
    /// </summary>
    public BasicAuthenticateException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAuthenticateException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error, or <c>null</c>.</param>
    public BasicAuthenticateException(String? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAuthenticateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error, or <c>null</c>.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or <c>null</c>.</param>
    public BasicAuthenticateException(
        String? message,
        Exception? innerException
    )
        : base(message, innerException)
    { }
}
