using System.Security.Claims;

namespace PowerBI_API_UserFlow.Services;

public class CurrentUserService
{
    public string? GetUserEmail(ClaimsPrincipal user)
    {
        return user.FindFirst("preferred_username")?.Value
            ?? user.FindFirst("upn")?.Value
            ?? user.FindFirst("unique_name")?.Value
            ?? user.FindFirst("email")?.Value
            ?? user.FindFirst(ClaimTypes.Email)?.Value
            ?? user.FindFirst(ClaimTypes.Upn)?.Value
            ?? user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn")?.Value
            ?? user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
    }

    public string? GetUserName(ClaimsPrincipal user)
    {
        return user.FindFirst("name")?.Value
            ?? user.FindFirst("given_name")?.Value
            ?? GetUserEmail(user);
    }

    public List<string> GetUserRoles(ClaimsPrincipal user)
    {
        return user.Claims
            .Where(c =>
                c.Type == "roles" ||
                c.Type == ClaimTypes.Role ||
                c.Type.EndsWith("/role", StringComparison.OrdinalIgnoreCase))
            .Select(c => c.Value)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}