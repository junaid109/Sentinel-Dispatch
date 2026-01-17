namespace SentinelDispatch.Shared.Contracts;

public record IncidentCreated(Guid IncidentId, string Description, string Location, DateTime CreatedAt);
