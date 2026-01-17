namespace SentinelDispatch.IncidentService.Data;

public enum IncidentStatus
{
    Created,
    Dispatched,
    OnScene,
    Closed
}

public class Incident
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public IncidentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
