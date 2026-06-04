using System.Security.Claims;
using Microsoft.Extensions.Options;
using PowerBI_API_UserFlow.Models;

namespace PowerBI_API_UserFlow.Services;

public class ReportAccessService
{
    private readonly ReportAccessOptions _options;
    private readonly CurrentUserService _currentUserService;

    public ReportAccessService(
        IOptions<ReportAccessOptions> options,
        CurrentUserService currentUserService)
    {
        _options = options.Value;
        _currentUserService = currentUserService;
    }

    public bool CanViewContractVendorReport(ClaimsPrincipal user)
    {
        if (user.Identity?.IsAuthenticated != true)
        {
            return false;
        }

        return IsAllowedByEmail(user) || IsAllowedByRole(user);
    }

    private bool IsAllowedByEmail(ClaimsPrincipal user)
    {
        var email = _currentUserService.GetUserEmail(user);

        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return _options.AllowedEmails.Any(allowedEmail =>
            string.Equals(allowedEmail, email, StringComparison.OrdinalIgnoreCase));
    }

    private bool IsAllowedByRole(ClaimsPrincipal user)
    {
        var userRoles = _currentUserService.GetUserRoles(user);

        if (!userRoles.Any())
        {
            return false;
        }

        return userRoles.Any(userRole =>
            _options.AllowedRoles.Any(allowedRole =>
                string.Equals(allowedRole, userRole, StringComparison.OrdinalIgnoreCase)));
    }
}