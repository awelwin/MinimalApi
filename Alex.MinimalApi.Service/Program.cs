using Alex.MinimalApi.Service.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection IOC
ServiceConfig.Configure(builder);
var app = builder.Build();

//Request Pipeline
MiddlewareConfig.Configure(app);

//Expose Endpoints - Routing
EndpointConfig.Configure(app);

app.Run();

