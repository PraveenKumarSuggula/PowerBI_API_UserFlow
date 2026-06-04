using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerBI_API_UserFlow.Models;
using PowerBI_API_UserFlow.Services;

namespace PowerBI_API_UserFlow.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly CurrentUserService _currentUserService;
    private readonly ReportAccessService _reportAccessService;

    public UsersController(
        CurrentUserService currentUserService,
        ReportAccessService reportAccessService)
    {
        _currentUserService = currentUserService;
        _reportAccessService = reportAccessService;
    }

    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var response = new CurrentUserResponse
        {
            Name = _currentUserService.GetUserName(User),
            Email = _currentUserService.GetUserEmail(User),
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            Roles = _currentUserService.GetUserRoles(User),
            CanViewPowerBiReport = _reportAccessService.CanViewContractVendorReport(User)
        };

        return Ok(response);
    }
}