using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PostMessengerService.Application.Middlewares;

public class UserProviderMiddleware : IUserProviderMiddleware
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProviderMiddleware(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string GetUsername()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
        return username;
    }
}