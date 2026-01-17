using SentinelDispatch.ResourceService.Data;
using SentinelDispatch.ResourceService.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<ResourceDbContext>("resourcedb");
builder.AddRedisClient("redis");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<IncidentCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var connectionString = builder.Configuration.GetConnectionString("rabbitmq");
        cfg.Host(connectionString);
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Initialize Database and Seed Data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ResourceDbContext>();
    db.Database.EnsureCreated();

    if (!db.Resources.Any())
    {
        db.Resources.AddRange(
            new Resource { Id = Guid.NewGuid(), Name = "Unit-Alpha", Latitude = 40.7128, Longitude = -74.0060, Status = ResourceStatus.Available },
            new Resource { Id = Guid.NewGuid(), Name = "Unit-Bravo", Latitude = 40.7300, Longitude = -74.0100, Status = ResourceStatus.Available },
            new Resource { Id = Guid.NewGuid(), Name = "Unit-Charlie", Latitude = 40.7500, Longitude = -73.9800, Status = ResourceStatus.Busy }
        );
        db.SaveChanges();
    }
}

app.Run();
