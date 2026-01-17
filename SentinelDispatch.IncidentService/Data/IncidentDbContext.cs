using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace SentinelDispatch.IncidentService.Data;

public class IncidentDbContext : DbContext
{
    public IncidentDbContext(DbContextOptions<IncidentDbContext> options) : base(options)
    {
    }

    public DbSet<Incident> Incidents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configures the MassTransit outbox entities (InboxState, OutboxState, OutboxMessage)
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
