var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");
var redis = builder.AddRedis("redis");
var rabbitmq = builder.AddRabbitMQ("rabbitmq");

var incidentDb = sql.AddDatabase("incidentdb");
var resourceDb = sql.AddDatabase("resourcedb");

var incidentService = builder.AddProject<Projects.SentinelDispatch_IncidentService>("incidentservice")
    .WithReference(incidentDb)
    .WithReference(rabbitmq);

var resourceService = builder.AddProject<Projects.SentinelDispatch_ResourceService>("resourceservice")
    .WithReference(resourceDb)
    .WithReference(redis)
    .WithReference(rabbitmq);

var web = builder.AddProject<Projects.SentinelDispatch_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(incidentService)
    .WithReference(resourceService);

builder.Build().Run();
