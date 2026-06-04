using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerBI_API_UserFlow.Services;

namespace PowerBI_API_UserFlow.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly ReportConfigService _reportConfigService;
    private readonly ReportAccessService _reportAccessService;

    public ReportsController(
        ReportConfigService reportConfigService,
        ReportAccessService reportAccessService)
    {
        _reportConfigService = reportConfigService;
        _reportAccessService = reportAccessService;
    }

    [HttpGet("contract-vendor/config")]
    public IActionResult GetContractVendorReportConfig()
    {

        if (!_reportAccessService.CanViewContractVendorReport(User))
        {
            return Forbid();
        }

        var reportConfig = _reportConfigService.GetContractVendorReportConfig();


        return Ok(new
        {
            workspaceId = reportConfig.WorkspaceId,
            reportId = reportConfig.ReportId,
            embedUrl = reportConfig.EmbedUrl
        });
    }
}