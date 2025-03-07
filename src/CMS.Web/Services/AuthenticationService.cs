using System.Security.Claims;
using CMS.Interfaces.User;
using CMS.Models.Services;
using CMS.Models.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using IAuthenticationService = CMS.Interfaces.User.IAuthenticationService;

namespace CMS.Web.Services;

public class AuthenticationService(IHttpContextAccessor httpContextAccessor)
    : IAuthenticationService
{
    private readonly HttpContext? _httpContext = httpContextAccessor.HttpContext;

    public AuthenticatedUser? GetAuthenticatedUser()
    {
        if (_httpContext == null || IsAuthenticated())
        {
            throw new AuthenticationFailureException("No authenticated user");
        }

        var serialisedUserDetails = _httpContext.User.Claims
            .FirstOrDefault(x => x.Type == AuthenticatedUserClaimTypes.SerialisedUserClaim);

        if (string.IsNullOrWhiteSpace(serialisedUserDetails?.Value))
        {
            throw new Exception("No serialised user");
        }

        return JsonConvert.DeserializeObject<AuthenticatedUser>(serialisedUserDetails.Value);
    }

    public async Task SignIn(AuthenticatedUser authenticatedUser, bool isPersistent = false)
    {
        if (_httpContext == null)
        {
            throw new NullReferenceException("HttpContext is null");
        }

        if (authenticatedUser == null)
        {
            throw new AuthenticationFailureException("Invalid login attempt.");
        }

        // TODO Use isPersistent
        var identity = new ClaimsIdentity(GetUserClaims(authenticatedUser),
            CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    public async Task SignOut()
    {
        if (_httpContext == null)
        {
            throw new NullReferenceException("HttpContext is null");
        }
        
        await _httpContext.SignOutAsync();
    }

    public bool IsAuthenticated()
    {
        return _httpContext?.User.Identity?.IsAuthenticated == true;
    }

    private IEnumerable<Claim> GetUserClaims(AuthenticatedUser authenticatedUser)
    {
        var claims = new List<Claim>
        {
            new(AuthenticatedUserClaimTypes.SerialisedUserClaim, JsonConvert.SerializeObject(authenticatedUser))
        };

        return claims;
    }
}