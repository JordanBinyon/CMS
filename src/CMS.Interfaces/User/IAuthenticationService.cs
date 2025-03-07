using CMS.Models.Services;

namespace CMS.Interfaces.User;

public interface IAuthenticationService
{
    AuthenticatedUser? GetAuthenticatedUser();
    Task SignIn(AuthenticatedUser authenticatedUser, bool isPersistent = false);
    Task SignOut();
}