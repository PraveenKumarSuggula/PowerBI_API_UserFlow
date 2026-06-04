using Microsoft.Extensions.Options;
using PowerBI_API_UserFlow.Models;

namespace PowerBI_API_UserFlow.Services;

public class ReportConfigService
{
    private readonly PowerBiReportsOptions _options;

    public ReportConfigService(IOptions<PowerBiReportsOptions> options)
    {
        _options = options.Value;
    }

    public PowerBiReportConfig GetContractVendorReportConfig()
    {
        var report = _options.ContractVendorReport;

        if (string.IsNullOrWhiteSpace(report.WorkspaceId))
        {
            throw new InvalidOperationException("Power BI WorkspaceId is missing.");
        }

        if (string.IsNullOrWhiteSpace(report.ReportId))
        {
            throw new InvalidOperationException("Power BI ReportId is missing.");
        }

        if (string.IsNullOrWhiteSpace(report.EmbedUrl))
        {
            throw new InvalidOperationException("Power BI EmbedUrl is missing.");
        }

        return report;
    }
}