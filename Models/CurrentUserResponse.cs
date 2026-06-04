namespace PowerBI_API_UserFlow.Models;

public class CurrentUserResponse
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool IsAuthenticated { get; set; }
    public bool CanViewPowerBiReport { get; set; }
    public List<string> Roles { get; set; } = new();
}