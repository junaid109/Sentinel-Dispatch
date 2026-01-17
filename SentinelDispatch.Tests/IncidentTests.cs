using SentinelDispatch.Shared.Contracts;

namespace SentinelDispatch.Tests;

public class IncidentTests
{
    [Fact]
    public void IncidentCreated_ShouldHaveCorrectProperties()
    {
        var id = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var incident = new IncidentCreated(id, "Test Incident", "40.7, -74.0", date);

        Assert.Equal(id, incident.IncidentId);
        Assert.Equal("Test Incident", incident.Description);
        Assert.Equal("40.7, -74.0", incident.Location);
        Assert.Equal(date, incident.CreatedAt);
    }
}
