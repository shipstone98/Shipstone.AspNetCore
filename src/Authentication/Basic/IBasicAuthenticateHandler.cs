using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Shipstone.AspNetCore.Authentication.Basic;

/// <summary>
/// Defines a method to handle authentication.
/// </summary>
public interface IBasicAuthenticateHandler
{
    /// <summary>
    /// Asynchronously authenticates an existing user with the specified email address and password.
    /// </summary>
    /// <param name="userId">The ID of the user to authenticate.</param>
    /// <param name="password">The password of the user to authenticate.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task{TResult}" /> that represents the asynchronous handle operation. The value of <see cref="Task{TResult}.Result" /> contains the generated claims.</returns>
    /// <exception cref="ArgumentNullException"><c><paramref name="userId" /></c> is <c>null</c> -or- <c><paramref name="password" /></c> is <c>null</c>.</exception>
    /// <exception cref="BasicAuthenticateException">The current user could not be authenticated.</exception>
    /// <exception cref="OperationCanceledException">The cancellation token was canceled.</exception>
    Task<IEnumerable<Claim>> HandleAsync(
        String userId,
        String password,
        CancellationToken cancellationToken
    );
}
