namespace SentinelDispatch.ResourceService.Data;

public enum ResourceStatus
{
    Available,
    Busy,
    OutOfService
}

public class Resource
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ResourceStatus Status { get; set; }
}
