using SentinelDispatch.IncidentService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSignalR();

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<IncidentDbContext>("incidentdb");

builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<IncidentDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(1);
        o.UseSqlServer();
        o.UseBusOutbox();
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        var connectionString = builder.Configuration.GetConnectionString("rabbitmq");
        cfg.Host(connectionString);
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapHub<SentinelDispatch.IncidentService.Hubs.IncidentHub>("/hub");

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IncidentDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/incidents", async (IncidentDbContext db) =>
{
    return await db.Incidents
        .OrderByDescending(i => i.CreatedAt)
        .ToListAsync();
});

app.MapPost("/incidents", async (IncidentDbContext db, IPublishEndpoint publishEndpoint, Microsoft.AspNetCore.SignalR.IHubContext<SentinelDispatch.IncidentService.Hubs.IncidentHub> hubContext, CreateIncidentRequest request) =>
{
    var incident = new Incident
    {
        Id = Guid.NewGuid(),
        Description = request.Description,
        Location = request.Location,
        Status = IncidentStatus.Created
    };

    db.Incidents.Add(incident);
    
    // Publish message - MassTransit Outbox will capture this
    await publishEndpoint.Publish(new SentinelDispatch.Shared.Contracts.IncidentCreated(incident.Id, incident.Description, incident.Location, incident.CreatedAt));

    await db.SaveChangesAsync();

    // Real-time update
    await hubContext.Clients.All.SendAsync("IncidentReceived", incident);

    return Results.Created($"/incidents/{incident.Id}", incident);
});

app.Run();

record CreateIncidentRequest(string Description, string Location);
