using MassTransit;
using SentinelDispatch.Shared.Contracts;
using Microsoft.Extensions.Logging;
using SentinelDispatch.ResourceService.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace SentinelDispatch.ResourceService.Consumers;

public class IncidentCreatedConsumer : IConsumer<IncidentCreated>
{
    private readonly ILogger<IncidentCreatedConsumer> _logger;
    private readonly ResourceDbContext _dbContext;
    private readonly IConnectionMultiplexer _redis;

    public IncidentCreatedConsumer(ILogger<IncidentCreatedConsumer> logger, ResourceDbContext dbContext, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _dbContext = dbContext;
        _redis = redis;
    }

    public async Task Consume(ConsumeContext<IncidentCreated> context)
    {
        _logger.LogInformation("Received new incident: {Description} at {Location}", context.Message.Description, context.Message.Location);

        // Find available unit (mock logic: find first available)
        var unit = await _dbContext.Resources
            .FirstOrDefaultAsync(r => r.Status == ResourceStatus.Available);

        if (unit != null)
        {
            _logger.LogInformation("Dispatching unit {UnitName} to incident {IncidentId}", unit.Name, context.Message.IncidentId);
            
            unit.Status = ResourceStatus.Busy;
            await _dbContext.SaveChangesAsync();

            // Update Redis
            var db = _redis.GetDatabase();
            await db.StringSetAsync($"resource:{unit.Id}:status", "Busy");
            await db.GeoAddAsync("resources:geo", new GeoEntry(unit.Longitude, unit.Latitude, unit.Id.ToString()));
            
            _logger.LogInformation("Unit {UnitName} status updated to Busy", unit.Name);
        }
        else
        {
            _logger.LogWarning("No available units for incident {IncidentId}", context.Message.IncidentId);
        }
    }
}
