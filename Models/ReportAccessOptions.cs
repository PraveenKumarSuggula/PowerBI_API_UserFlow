namespace PowerBI_API_UserFlow.Models;

public class ReportAccessOptions
{
    public List<string> AllowedEmails { get; set; } = new();
    public List<string> AllowedRoles { get; set; } = new();
}